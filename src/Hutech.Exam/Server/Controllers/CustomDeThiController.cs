using Hutech.Exam.Server.Attributes;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomDeThiController(CustomDeThiService customDeThiService, IResponseCacheService cacheService) : Controller
    {
        private readonly CustomDeThiService _customDeThiService = customDeThiService;
        private readonly IResponseCacheService _cacheService = cacheService;

        [HttpGet("GetDeThi")]
        public async Task<ActionResult<List<CustomDeThi>>> GetDeThi([FromQuery] long ma_de_thi_hoan_vi)
        {
            var cacheKey = $"DeThi:{ma_de_thi_hoan_vi}";

            // Kiểm tra xem có dữ liệu trong cache không
            var cachedData = await _cacheService.GetCacheResponseAsync<List<CustomDeThi>>(cacheKey);

            if (cachedData != null && cachedData.Count != 0)
            {
                return Ok(cachedData);
            }

            // Nếu không có, thực hiện logic lấy dữ liệu từ database
            cachedData = await _customDeThiService.GetDeThi(ma_de_thi_hoan_vi);

            // Lưu vào cache
            await _cacheService.SetCacheResponseAsync(cacheKey, cachedData, TimeSpan.FromMinutes(150));

            return Ok(cachedData);
        }
    }
}
