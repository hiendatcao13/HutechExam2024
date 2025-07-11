using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietcathis")]
    [ApiController]
    [Authorize]
    public class ChiTietCaThiController(ChiTietCaThiService chiTietCaThiService, IHubContext<AdminHub> adminHub) : Controller
    {
        #region Private Fields

        private readonly ChiTietCaThiService _chiTietCaThiService = chiTietCaThiService;
        private readonly IHubContext<AdminHub> _adminHub = adminHub;

        private const string NotFoundMessage = "Không tìm thấy chi tiết ca thi";

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.SelectOne(id);
            if (result.MaChiTietCaThi == 0)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết ca thi"));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: result, message: "Lấy chi tiết ca thi thành công"));
        }

        [HttpGet("filter-by-sinhvien")]
        [Authorize(Roles = "SinhVien")]
        public async Task<IActionResult> SelectBy_MSSVThi([FromQuery] long maSinhVien)
        {
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaSinhVienThi(maSinhVien), message: "Lấy chi tiết ca thi thành công"));
        }

        [HttpGet("filter-by-cathi-paged")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectBy_MaCaThi_Paged([FromQuery] int maCaThi, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(APIResponse<Paged<ChiTietCaThiDto>>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaCaThi_Paged(maCaThi, pageNumber, pageSize), message: "Lấy chi tiết ca thi thành công"));
        }


        [HttpGet("filter-by-cathi-search-paged")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectBy_MaCaThi_Search_Paged([FromQuery] int maCaThi, [FromQuery] string keyword, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(APIResponse<Paged<ChiTietCaThiDto>>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaCaThi_Search_Paged(maCaThi, keyword, pageNumber, pageSize), message: "Lấy chi tiết ca thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "QuanTri")] // thêm SV khẩn cấp
        public async Task<IActionResult> Insert([FromBody] ChiTietCaThiCreateRequest chiTietCaThi)
        {
            var id = await _chiTietCaThiService.Insert(chiTietCaThi);
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Thêm chi tiết ca thi thành công"));
        }

        [HttpPost("batch")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> InsertBatch([FromBody] List<ChiTietCaThiCreateBatchRequest> chiTietCaThis)
        {
            await _chiTietCaThiService.Insert_Batch(chiTietCaThis);
            return Ok(APIResponse<List<ChiTietCaThiDto>>.SuccessResponse(message: "Thêm danh sách chi tiết ca thi thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChiTietCaThiUpdateRequest chiTietCaThi)
        {
            var result = await _chiTietCaThiService.Update(id, chiTietCaThi);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Cập nhật chi tiết ca thi thành công"));
        }

        #endregion

        #region Patch Methods

        [HttpPatch("{id:int}/cong-gio")]
        [Authorize(Roles = "QuanTri")]
        public async Task<ActionResult<ChiTietCaThiDto>> CongGioSinhVien([FromRoute] int id, [FromQuery] int gioCongThem)
        {
            var result = await _chiTietCaThiService.CongGio(id, gioCongThem);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Cộng giờ cho thí sinh thành công"));

        }

        [HttpPatch("{id:int}/bat-dau-thi")]//------------------API cho thí sinh----------------------
        public async Task<ActionResult> UpdateBatDauThi([FromRoute] int id)
        {
            await _chiTietCaThiService.UpdateBatDau(id, DateTime.Now);
            await NotifSVStatusThiToAdmin(id, true, DateTime.Now);
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(message: "Cập nhật trạng thái bắt đầu thi cho thí sinh thành công"));
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(message: "Xóa chi tiết ca thi thành công"));
        }

        [HttpDelete("{id:int}/force")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(message: "Xóa chi tiết ca thi thành công"));
        }


        #endregion

        #region Private Methods

        private async Task NotifSVStatusThiToAdmin(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian)
        {
            // 0: bắt đầu thi, 1: kết thúc thi
            await _adminHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian, -1);
        }


        #endregion

    }
}