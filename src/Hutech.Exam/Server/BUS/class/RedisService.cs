using Hutech.Exam.Shared.DTO.Request;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Server.BUS
{
    public class RedisService(ChiTietBaiThiService chiTietBaiThiService, CauTraLoiService cauTraLoiService, IResponseCacheService cacheService, ILogger<RedisService> logger)
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;
        private readonly IResponseCacheService _cacheService = cacheService;

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
        public async Task<List<int>> GetDapAnKhoanhAsync(int ma_chi_tiet_ca_thi)
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

                var data = chiTietBaiThiJson.Values.ToList(); // convert Dictionary<string, object> -> List<object>
                var chiTietBaiThis = data.OfType<ChiTietBaiThiRequest>().ToList();

                if (chiTietBaiThis == null || chiTietBaiThis.Count == 0)
                {
                    _logger.LogWarning("[Redis] Deserialized list is null or empty for key: {Key}", cacheKey);
                    return [];
                }

                return _chiTietBaiThiService.GetDapAnKhoanh_SelectByListCTBT(chiTietBaiThis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving ChiTietBaiThi.");
                return [];
            }
        }
        // dành cho thí sinh lúc nộp bài thi
        public async Task<List<ChiTietBaiThiRequest>> GetChiTietBaiLamAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var chiTietBaiThiJson = await _cacheService.GetCacheResponseAsync(cacheKey);

                if (string.IsNullOrEmpty(chiTietBaiThiJson) || string.IsNullOrWhiteSpace(chiTietBaiThiJson))
                {
                    _logger.LogWarning("[Redis] No data found for key: {Key}", cacheKey);
                    return [];
                }
                var chiTietBaiThis = JsonSerializer.Deserialize<List<ChiTietBaiThiRequest>>(chiTietBaiThiJson.ToString());
                if (chiTietBaiThis == null || chiTietBaiThis.Count == 0)
                {
                    _logger.LogWarning("[Redis] Deserialized list is null or empty for key: {Key}", cacheKey);
                    return [];
                }
                return chiTietBaiThis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving ChiTietBaiThi.");
                return [];
            }
        }
        public async Task<List<int>> GetDapAn(int maDeHV)
        {
            try
            {
                var cacheKey = $"DapAn:{maDeHV}";

                // Kiểm tra xem có dữ liệu trong cache không
                var cachedData = await _cacheService.GetCacheResponseAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    // Nếu có, deserialization và trả về dữ liệu
                    var result = JsonSerializer.Deserialize<List<int>>(cachedData.ToString());
                    if (result == null || result.Count == 0)
                    {
                        _logger.LogWarning("[Redis] Deserialized list is null or empty for key: {Key}", cacheKey);
                        return [];
                    }

                    return result;
                }

                // Nếu không có, thực hiện logic lấy dữ liệu từ database
                List<int> listDapAn = await _cauTraLoiService.SelectBy_MaDeHV_DapAn(maDeHV);

                // Lưu vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(120));

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
