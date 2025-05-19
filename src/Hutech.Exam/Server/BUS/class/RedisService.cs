using AutoMapper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Server.BUS
{
    public class RedisService(ChiTietBaiThiService chiTietBaiThiService, CauTraLoiService cauTraLoiService, IResponseCacheService cacheService, ILogger<RedisService> logger, IMapper mapper)
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;
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
                var cacheData = await _cacheService.GetCacheResponseAsync(cacheKey);

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

                var messageJson = Encoding.UTF8.GetString(message);

                // Deserialize vào đối tượng chiTietBaiThi
                var chiTietBaiThi = JsonSerializer.Deserialize<ChiTietBaiThiRequest>(messageJson);

                if (chiTietBaiThi == null)
                {
                    _logger.LogError("[Redis] Error deserializing message: {Message}", messageJson);
                    throw new Exception("Error deserializing message.");
                }

                var cacheKey = $"ChiTietBaiThi:{chiTietBaiThi.MaChiTietCaThi}";

                // Lưu chuỗi JSON vào Redis với TTL 150 phút
                await _cacheService.SetHashAsync(cacheKey, chiTietBaiThi.MaCauHoi + "", messageJson, TimeSpan.FromMinutes(150));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while processing the message.");
                throw;
            }
        }
        // dành cho thí sinh tiếp tục thi khi bị treo máy
        public async Task<Dictionary<int, int>> GetDapAnKhoanhAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var chiTietBaiThiJson = await _cacheService.GetAllFieldsFromHashAsync(cacheKey);

                if (chiTietBaiThiJson == null)
                {
                    _logger.LogWarning("[Redis] No data found for key: {Key}", cacheKey);
                    return [];
                }

                var data = chiTietBaiThiJson.Values
                    .Select(json => JsonSerializer.Deserialize<ChiTietBaiThiRequest>(json + ""))
                    .Where(deserialized => deserialized != null) // To ensure we don't add null values to the list
                    .ToList();

                if (data == null || data.Count == 0)
                {
                    _logger.LogWarning("[Redis] Deserialized list is null or empty for key: {Key}", cacheKey);
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
        public async Task<List<bool>> GetDungSaiAsync(int ma_chi_tiet_ca_thi, long ma_de_hoan_vi, byte[] message)
        {
            try
            {
                if (message == null || message.Length == 0)
                {
                    _logger.LogError("[Redis] Received empty or null message.");
                    throw new ArgumentException("Message cannot be null or empty.");
                }

                var messageJson = Encoding.UTF8.GetString(message);

                //TODO: xử lí phần int int ở đây
                // Deserialize vào đối tượng chiTietBaiThi
                var chiTietBaiThi = JsonSerializer.Deserialize<(int, int)>(messageJson);

                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var chiTietBaiThiJson = await _cacheService.GetAllFieldsFromHashAsync(cacheKey);

                if (chiTietBaiThiJson == null)
                {
                    _logger.LogWarning("[Redis] No data found for key: {Key}", cacheKey);
                    return [];
                }

                Dictionary<int, int> dapAns = await GetDapAnAsync(ma_de_hoan_vi);

                Dictionary<int, ChiTietBaiThiRequest> data = [];
                foreach (var item in chiTietBaiThiJson)
                {
                    var deserialized = JsonSerializer.Deserialize<ChiTietBaiThiRequest>(item.Value + "");
                    if (deserialized != null)
                    {
                        data[deserialized.MaCauHoi] = deserialized;
                    }
                }

                if (data == null || data.Count == 0)
                {
                    _logger.LogWarning("[Redis] Deserialized list is null or empty for key: {Key}", cacheKey);
                    return [];
                }

                await _chiTietBaiThiService.Insert_Batch(_mapper.Map<List<ChiTietBaiThiDto>>(data.Values.ToList()));

                return _chiTietBaiThiService.GetDungSai_SelectByListCTBT_DapAn(data, dapAns);
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
                var cachedData = await _cacheService.GetCacheResponseAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    // Nếu có, deserialization và trả về dữ liệu
                    var result = JsonSerializer.Deserialize<Dictionary<int, int>>(cachedData.ToString());
                    if (result == null || result.Count == 0)
                    {
                        _logger.LogWarning("[Redis] Deserialized list is null or empty for key: {Key}", cacheKey);
                        return [];
                    }

                    return result;
                }

                // Nếu không có, thực hiện logic lấy dữ liệu từ database
                Dictionary<int, int> listDapAn = await _cauTraLoiService.SelectBy_MaDeHV_DapAn(maDeHV);

                // Lưu vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(150));

                return listDapAn;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving DapAn.");
                return [];
            }
        }
    }
}
