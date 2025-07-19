using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.Request.ChiTietDeThiHoanVi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietdethihoanvis")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class ChiTietDeThiHoanViController(ChiTietDeThiHoanViService chiTietDeThiHoanViService) : Controller
    {
        #region Private Fields

        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService = chiTietDeThiHoanViService;

        #endregion

        #region Get Methods



        #endregion

        #region Post Methods

        [HttpPost("batch")]
        public async Task<IActionResult> Insert_Batch([FromQuery] int maDeThi, [FromQuery] string kyHieuDe, [FromQuery] int soLuongDe, [FromBody] List<ChiTietDeThiHoanViCreateBatchRequest> chiTietDeThiHoanVis)
        {
            await _chiTietDeThiHoanViService.Insert_Batch(maDeThi, kyHieuDe, soLuongDe, chiTietDeThiHoanVis);
            return Ok();
        }

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
