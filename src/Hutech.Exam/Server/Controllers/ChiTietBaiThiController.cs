using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietbaithis")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class ChiTietBaiThiController(ChiTietBaiThiService chiTietBaiThiService) : Controller
    {
        #region Private Fields

        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;

        #endregion

        #region Get Methods

        [HttpGet("filter-by-chitietcathi")]
        public async Task<IActionResult> SelectBy_ma_chi_tiet_ca_thi([FromQuery] int maChiTietCaThi)
        {
            var result = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(maChiTietCaThi);
            return Ok(APIResponse<List<ChiTietBaiThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách chi tiết bài thi thành công"));
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
