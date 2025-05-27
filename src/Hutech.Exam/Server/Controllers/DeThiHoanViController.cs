using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dethihoanvis")]
    [ApiController]
    [Authorize]
    public class DeThiHoanViController(CustomDeThiService customDeThiService, DeThiHoanViService deThiHoanViService, IResponseCacheService cacheService) : Controller
    {
        private readonly CustomDeThiService _customDeThiService = customDeThiService; // định dạng theo format riêng của đề thi
        private readonly DeThiHoanViService _deThiHoanViService = deThiHoanViService; // định dạng theo format của đề thi hoàn vi

        private readonly IResponseCacheService _cacheService = cacheService;

        //////////////////CRUD///////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CustomDeThi>>> GetDeThi([FromRoute] int id)
        {
            var cacheKey = $"DeThi:{id}";

            // Kiểm tra xem có dữ liệu trong cache không
            var cachedData = await _cacheService.GetCacheResponseAsync<List<CustomDeThi>>(cacheKey);

            if (cachedData != null && cachedData.Count != 0)
            {
                return Ok(cachedData);
            }

            // Nếu không có, thực hiện logic lấy dữ liệu từ database
            cachedData = await _customDeThiService.GetDeThi(id);

            // Lưu vào cache
            await _cacheService.SetCacheResponseAsync(cacheKey, cachedData, TimeSpan.FromMinutes(150));

            return Ok(cachedData);
        }

        [HttpGet("{id}/dap-an")]
        public async Task<ActionResult<Dictionary<int, int>>> SelectByMaDeHV_DapAn([FromRoute] long id)
        {
            return Ok(await _deThiHoanViService.DapAn(id));
        }

        //////////////////FILTER///////////////////////////

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////

    }
}
