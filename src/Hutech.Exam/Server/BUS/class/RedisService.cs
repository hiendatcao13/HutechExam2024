using AutoMapper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.BUS
{
    public class RedisService(DeThiHoanViService deThiHoanViService, CustomDeThiService customDeThiService, IResponseCacheService cacheService, ILogger<RedisService> logger)
    {
        private readonly DeThiHoanViService _deThiHoanViService = deThiHoanViService;
        private readonly CustomDeThiService _customDeThiService = customDeThiService;

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
                var cacheData = await _cacheService.GetCacheResponseAsync<string>(cacheKey);

                return string.IsNullOrEmpty(cacheData) ? string.Empty : cacheData.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error retrieving connectionId: {message}", ex.Message);
                return string.Empty;
            }
        }

        public async Task SetFailSubmitAsync(int ma_chi_tiet_ca_thi, int so_lan_fail)
        {
            try
            {
                var cacheKey = $"failsubmit:{ma_chi_tiet_ca_thi}";
                await _cacheService.SetCacheResponseAsync(cacheKey, so_lan_fail, TimeSpan.FromMinutes(150));
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error setting failsubmit: {message}", ex.Message);
            }
        }

        public async Task<int> GetFailSubmitAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"failsubmit:{ma_chi_tiet_ca_thi}";
                var cacheData = await _cacheService.GetCacheResponseAsync<int>(cacheKey);

                return cacheData;
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error retrieving failsubmit: {message}", ex.Message);
                return 0;
            }
        }
        public async Task RemoveSubmitAsync(int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"failsubmit:{ma_chi_tiet_ca_thi}";
                await _cacheService.RemoveCacheResponseAsync(cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error removing failsubmit: {message}", ex.Message);
            }
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cacheService.RemoveCacheResponseAsync(key);
        }

        public async Task SetHashAsync(string key, string field, object value, TimeSpan? expiration = null)
        {
            try
            {
                await _cacheService.SetHashAsync(key, field, value, expiration);
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error setting hash: {message}", ex.Message);
            }
        }

        public async Task<Dictionary<int, TData>?> GetHashAsync<TData>(string key)
        {
            try
            {
                return await _cacheService.GetAllFieldsFromHashAsync<int,TData>(key);
            }
            catch (Exception ex)
            {
                _logger.LogError("[Redis] Error getting hash: {message}", ex.Message);
                return default;
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

        public async Task SetChiTietBaiThi(int key, ChiTietBaiThiRequest value, int ma_chi_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_ca_thi}";
                await _cacheService.SetHashAsync(cacheKey, key.ToString(), value, TimeSpan.FromMinutes(180));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while setting ChiTietBaiThi.");
            }
        }

        public async Task RemoveChiTietBaiThiAsync(int ma_chi_tiet_tiet_ca_thi)
        {
            try
            {
                var cacheKey = $"ChiTietBaiThi:{ma_chi_tiet_tiet_ca_thi}";
                await _cacheService.RemoveCacheResponseAsync(cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while removing ChiTietBaiThi.");
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
                Dictionary<int, int> listDapAn = await _deThiHoanViService.DapAn(maDeHV);

                // Lưu vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, listDapAn, TimeSpan.FromMinutes(150));

                return listDapAn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving DapAn.");
                return await _deThiHoanViService.DapAn(maDeHV);
            }

        }

        public async Task<List<CustomDeThi>> GetDeThi([FromRoute] int id)
        {
            try
            {
                var cacheKey = $"DeThi:{id}";

                // Kiểm tra xem có dữ liệu trong cache không
                var cachedData = await _cacheService.GetCacheResponseAsync<List<CustomDeThi>>(cacheKey);

                if (cachedData != null && cachedData.Count != 0)
                {
                    return cachedData;
                }

                // Nếu không có, thực hiện logic lấy dữ liệu từ database
                List<CustomDeThi> result = await _customDeThiService.GetDeThi(id);

                // Lưu vào cache
                await _cacheService.SetCacheResponseAsync(cacheKey, result, TimeSpan.FromMinutes(150));

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "[Redis] An error occurred while retrieving DeThi.");
                return await _customDeThiService.GetDeThi(id);
            }
        }
    }
}
