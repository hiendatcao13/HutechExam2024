using AutoMapper;
using Hutech.Exam.Client.Pages.Result;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
using MessagePack;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static MudBlazor.CategoryTypes;

namespace Hutech.Exam.Server.BUS
{
    public class RedisService(ChiTietBaiThiService chiTietBaiThiService, CauTraLoiService cauTraLoiService, ChiTietCaThiService chiTietCaThiService, IResponseCacheService cacheService, IHubContext<SinhVienHub> sinhVienHub, ILogger<RedisService> logger, IMapper mapper)
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;
        private readonly ChiTietCaThiService _chiTietCaThiService = chiTietCaThiService;
        private readonly IHubContext<SinhVienHub> _sinhVienHub = sinhVienHub;
        private readonly IResponseCacheService _cacheService = cacheService;
        private readonly IMapper _mapper = mapper;

        private readonly ILogger _logger = logger;
        public async Task SetConnectionIdAsync(long ma_sinh_vien, string connectionId)
        {
            try
            {
                var cacheKey = $"connection:{ma_sinh_vien}";
                await _cacheService.SetCacheResponseAsync(cacheKey, connectionId, TimeSpan.FromMinutes(150));
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error setting connectionId: {message}", ex.Message);
            }
        }
        public async Task<string> GetConnectionIdAsync(long ma_sinh_vien)
        {
            try
            {
                var cacheKey = $"connection:{ma_sinh_vien}";
                var cacheData = await _cacheService.GetCacheResponseAsync<string>(cacheKey);

                return string.IsNullOrEmpty(cacheData) ? string.Empty : cacheData.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error retrieving connectionId: {message}", ex.Message);
                return string.Empty;
            }
        }
        // lấy bài thi từ RabbitMQ và lưu vào Redis
        public async Task SetChiTietBaiLamAsync(byte[] message)
        {
            try
            {
                if (message == null || message.Length == 0)
                {
                    _logger.LogError("[Redis] Received empty or null message.");
                    throw new ArgumentException("Message cannot be null or empty.");
                }

                // Deserialize vào đối tượng chiTietBaiThi
                var chiTietBaiThi = MessagePackSerializer.Deserialize<ChiTietBaiThiRequest>(message);

                if (chiTietBaiThi == null)
                {
                    _logger.LogError("[Redis] Error deserializing message: {Message}", message);
                    throw new Exception("Error deserializing message.");
                }

                var cacheKey = $"ChiTietBaiThi:{chiTietBaiThi.MaChiTietCaThi}";

                // Lưu chuỗi JSON vào Redis với TTL 150 phút
                await _cacheService.SetHashAsync(cacheKey, chiTietBaiThi.MaCauHoi + "", chiTietBaiThi, TimeSpan.FromMinutes(150));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while processing the message.");
                throw; // ném ngoại lệ để rabbitMQ bắt và đẩy thông điệp vào hàng đợi
            }
        }
        // dành cho thí sinh tiếp tục thi khi bị treo máy
        public async Task<Dictionary<int, int>> GetDapAnKhoanhAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var cacheData = await _cacheService.GetAllFieldsFromHashAsync<int, ChiTietBaiThiRequest>(cacheKey); // key: field, value: byte[]

                if (cacheData == null || cacheData.Count == 0)
                {
                    _logger.LogWarning("[Redis] No data found for key: {Key}", cacheKey);
                    return [];
                }

                var data = cacheData.Values.ToList();

                if (data.Count == 0)
                {
                    _logger.LogWarning("[Redis] No data.");
                    return [];
                }

                return _chiTietBaiThiService.GetDapAnKhoanh_SelectByListCTBT(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving ChiTietBaiThi.");
                return [];
            }
        }
        // dành cho thí sinh lúc nộp bài thi
        public async Task HandleSubmit(byte[] message)
        {
            try
            {
                (long ma_sinh_vien, int ma_chi_tiet_ca_thi, long ma_de_hoan_vi) = MessagePackSerializer.Deserialize<(long, int, long)>(message);

                Dictionary<int, int> dapAns = await GetDapAnAsync(ma_de_hoan_vi);

                Dictionary<int, ChiTietBaiThiRequest> data = await GetChiTietBaiThiAsync(ma_chi_tiet_ca_thi);

                // tiến hành lưu vào database TVP chiTietBaiThi
                await _chiTietBaiThiService.Insert_Batch(_mapper.Map<List<ChiTietBaiThiDto>>(data.Values.ToList()));

                // xóa dữ liệu khi đã lưu xong thành công
                await _cacheService.RemoveCacheResponseAsync($"ChiTietBaiThi:{ma_chi_tiet_ca_thi}");

                // xử lí chỉ trả ds đúng sai
                (List<bool> listDungSai, int so_cau_dung, double diem) = _chiTietBaiThiService.GetDungSai_SelectByListCTBT_DapAn(data, dapAns);
                _logger.LogInformation("SV ma: {ma_sinh_vien} co diem {diem}, socaudung {so_cau_dung} tren tong_so_cau {tong_so_cau}", ma_sinh_vien, diem, so_cau_dung, dapAns.Count);

                // tiến hành lưu đáp án vào database
                await _chiTietCaThiService.UpdateKetThuc(ma_chi_tiet_ca_thi, DateTime.Now, diem, so_cau_dung, listDungSai.Count);

                // gửi thông điệp cho thí sinh
                var connectionId = await GetConnectionIdAsync(ma_sinh_vien);

                if (!string.IsNullOrEmpty(connectionId))
                {
                    await _sinhVienHub.Clients.Client(connectionId).SendAsync("DeliverDapAn", listDungSai, so_cau_dung, diem);
                }
                else
                {
                    _logger.LogWarning("[Redis] No connectionId found for ma_sinh_vien: {ma_sinh_vien}", ma_sinh_vien);
                }
            }
            catch (Exception ex) // có thể catch SqlException ở đây
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving ChiTietBaiThi.");
                throw; // ném ra để rabbitMQ đẩy lại vào hàng đợi, xử lí lại
            }
        }
        public async Task<Dictionary<int, ChiTietBaiThiRequest>> GetChiTietBaiThiAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var cacheData = await _cacheService.GetAllFieldsFromHashAsync<int, ChiTietBaiThiRequest>(cacheKey);

                if (cacheData == null || cacheData.Count == 0)
                {
                    _logger.LogWarning("[Redis] No data found for key: {Key}", cacheKey);
                    return [];
                }

                Dictionary<int, ChiTietBaiThiRequest> data = [];

                foreach (var item in cacheData)
                {
                    data[Convert.ToInt32(item.Key)] = item.Value;
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving ChiTietBaiThi.");
                return [];
            }
        }
        public async Task<Dictionary<int, int>> GetDapAnAsync(long maDeHV)
        {
            try
            {
                var cacheKey = $"DapAn:{maDeHV}";

                // Kiểm tra xem có dữ liệu trong cache không
                var cachedData = await _cacheService.GetCacheResponseAsync<Dictionary<int, int>>(cacheKey);

                if (cachedData != null && cachedData.Count != 0)
                {
                    return cachedData;
                }

                // Nếu không có, thực hiện logic lấy dữ liệu từ database
                Dictionary<int, int> listDapAn = await _cauTraLoiService.SelectBy_MaDeHV_DapAn(maDeHV);

                // Lưu vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(150));

                return listDapAn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving DapAn.");
                return [];
            }
        }
    }
}
