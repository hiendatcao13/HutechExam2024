using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dethihoanvis")]
    [ApiController]
    [Authorize]
    public class DeThiHoanViController(CustomDeThiService customDeThiService, DeThiHoanViService deThiHoanViService, RedisService redisService, IResponseCacheService cacheService) : Controller
    {
        #region Private Fields

        private readonly CustomDeThiService _customDeThiService = customDeThiService; // định dạng theo format riêng của đề thi

        private readonly DeThiHoanViService _deThiHoanViService = deThiHoanViService; // định dạng theo format của đề thi hoàn vi

        private readonly RedisService _redisService = redisService; // dịch vụ Redis để lưu trữ kết nối và thông tin khác

        private readonly IResponseCacheService _cacheService = cacheService;

        #endregion

        #region Get Methods

        [HttpGet("{id}")]//-------------API cho thí sinh----------------------
        public async Task<ActionResult<List<CustomDeThi>>> GetDeThi([FromRoute] int id)
        {
            return Ok(APIResponse<List<CustomDeThi>>.SuccessResponse(data: await _redisService.GetDeThi(id), message: "Lấy đề thi thành công"));
        }

        [HttpGet("{id}/dap-an")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dictionary<int, int>>> SelectByMaDeHV_DapAn([FromRoute] long id)
        {
            return Ok(APIResponse<Dictionary<int, int>>.SuccessResponse(data: await _redisService.GetDapAnAsync(id), message: "Lấy danh sách đáp án thành công"));
        }

        #endregion

        #region Post Methods



        #endregion

        #region Put Methods



        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods



        #endregion

        #region Private Methods


        #endregion

    }
}
