using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietDotThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietdotthis")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class ChiTietDotThiController(ChiTietDotThiService chiTietDotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        #region Private Fields

        private readonly ChiTietDotThiService _chiTietDotThiService = chiTietDotThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        private const string NotFoundMessage = "Không tìm thấy chi tiết đợt thi";

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _chiTietDotThiService.SelectOne(id);
            if (result.MaChiTietDotThi == 0)
            {
                return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết đợt thi"));
            }
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: result, message: "Lấy chi tiết đợt thi thành công"));
        }

        [HttpGet("filter-by-dotthi")]
        public async Task<IActionResult> SelectBy_MaDotThi([FromQuery] int maDotThi)
        {
            return Ok(APIResponse<List<ChiTietDotThiDto>>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi(maDotThi), message: "Lấy danh sách chi tiết đợt thi thành công"));
        }

        [HttpGet("filter-by-dotthi-paged")]
        public async Task<IActionResult> SelectBy_MaDotThi_Paged([FromQuery] int maDotThi, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(APIResponse<Paged<ChiTietDotThiDto>>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi_Paged(maDotThi, pageNumber, pageSize), message: "Lấy danh sách chi tiết đợt thi thành công"));
        }


        [HttpGet("filter-by-dotthi-lopao")]
        public async Task<IActionResult> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int maDotThi, [FromQuery] int maLopAo)
        {
            return Ok(APIResponse<List<ChiTietDotThiDto>>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo(maDotThi, maLopAo), message: "Lấy danh sách chi tiết đợt thi thành công"));
        }

        [HttpGet("filter-by-dotthi-lopao-lanthi")]
        public async Task<IActionResult> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int maDotThi, [FromQuery] int maLopAo, [FromQuery] int lanThi)
        {
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo_LanThi(maDotThi, maLopAo, lanThi), message: "Lấy tiết đợt thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Insert([FromBody] ChiTietDotThiCreateRequest chiTietDotThi)
        {
            var id = await _chiTietDotThiService.Insert(chiTietDotThi);
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: await _chiTietDotThiService.SelectOne(id), message: "Thêm chi tiết đợt thi thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChiTietDotThiUpdateRequest chiTietDotThi)
        {
            var result = await _chiTietDotThiService.Update(id, chiTietDotThi);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: await _chiTietDotThiService.SelectOne(id), message: "Cập nhật chi tiết đợt thi thành công"));
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _chiTietDotThiService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(message: "Xóa chi tiết đợt thi thành công"));
        }

        [HttpDelete("{id:int}/force")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _chiTietDotThiService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(message: "Xóa chi tiết đợt thi thành công"));
        }

        #endregion

        #region Private Methods

        private async Task NotifyChangeChiTietDotThiToAdmin(int ma_chi_tiet_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCTDotThi" : (function == 1) ? "UpdateCTDotThi" : "DeleteCTDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_chi_tiet_dot_thi);
        }

        #endregion

    }
}
