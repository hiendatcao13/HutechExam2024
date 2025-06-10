using Hutech.Exam.Shared.DTO.Request;

namespace Hutech.Exam.Server.BUS
{
    public class ExamRecoveryService(RedisService redisService, ILogger<ExamRecoveryService> logger)
    {
        private readonly RedisService _redisService = redisService;
        private readonly ILogger _logger = logger;

        // dành cho thí sinh tiếp tục thi khi bị treo máy
        public async Task<Dictionary<int, ChiTietBaiThiRequest>> GetDapAnKhoanhAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                var cacheData = await _redisService.GetHashAsync<ChiTietBaiThiRequest>(cacheKey); // key: field, value: byte[]

                if (cacheData == null || cacheData.Count == 0)
                {
                    _logger.LogWarning("[Redis] No data found for key chitietbailam: {Key} when student request exam recovery", cacheKey);
                    return [];
                }

                return cacheData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving ChiTietBaiThi.");
                return [];
            }
        }
    }
}
