using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Audit;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cathis")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class CaThiController(CaThiService caThiService, BcryptService bcryptService, IHubContext<AdminHub> adminHub, IHubContext<SinhVienHub> sinhVienHub) : Controller
    {
        #region Private Fields
        private readonly CaThiService _caThiService = caThiService;
        private readonly BcryptService _bcryptService = bcryptService;

        private readonly IHubContext<AdminHub> _adminHub = adminHub;
        private readonly IHubContext<SinhVienHub> _sinhVienHub = sinhVienHub;

        private const string NotFoundMessage = "Không tìm thấy ca thi";
        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _caThiService.SelectOne(id);

            if (result.MaCaThi == 0)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi"));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(result, "Lấy ca thi thành công"));
        }

        [HttpGet("{id:int}/is-active")]
        [Authorize] //----------------Đánh dấu thí sinh có thể truy cập
        public async Task<IActionResult> IsActiveCaThi([FromRoute] int id)
        {
            CaThiDto caThi = await _caThiService.SelectOne(id);
            return Ok(APIResponse<bool>.SuccessResponse(data: caThi.KichHoat, message: "Lấy thông tin ca thi thành công"));
        }

        [HttpGet("filter-by-dotthi-lopao-lanthi")]
        public async Task<IActionResult> SelectBy_MaDotThi_MaLopAo_LanThi([FromQuery] int maDotThi, [FromQuery] int maLopAo, [FromQuery] int lanThi)
        {
            var result = await _caThiService.SelectBy_MaDotThi_MaLop_LanThi(maDotThi, maLopAo, lanThi);
            return Ok(APIResponse<List<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));

        }

        [HttpGet("filter-by-chitietdotthi-paged")]
        public async Task<IActionResult> SelectBy_ma_chi_tiet_dot_thi_Paged([FromQuery] int maChiTietDotThi, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi_Paged(maChiTietDotThi, pageNumber, pageSize);
            return Ok(APIResponse<Paged<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        [HttpGet("filter-by-chitietdotthi-search-paged")]
        public async Task<IActionResult> SelectBy_ma_chi_tiet_dot_thi_Search_Paged([FromQuery] int maChiTietDotThi, [FromQuery] string keyword, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi_Search_Paged(maChiTietDotThi, keyword, pageNumber, pageSize);
            return Ok(APIResponse<Paged<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Insert([FromBody] CaThiCreateRequest caThi)
        {
            var id = await _caThiService.Insert(caThi);
            await NotifyChangeCaThiToAdmin(id, 0);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), "Thêm ca thi thành công"));
        }

        [HttpPost("{id:int}/verify")]
        public async Task<IActionResult> VerifyPassword([FromRoute] int id, [FromBody] string password)
        {
            var examSession = await _caThiService.SelectOne(id);
            var hashPassword = examSession.MatMa;
            if (string.IsNullOrWhiteSpace(hashPassword) || _bcryptService.VerifyPassword(password, hashPassword) == true)
            {
                return Ok(APIResponse<bool>.SuccessResponse(data: true, message: "Xác thực thành công"));
            }
            else
            {
                return BadRequest(APIResponse<bool>.ErrorResponse(message: "Xác không thành công. Vui lòng liên hệ quản trị viên"));
            }
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CaThiUpdateRequest caThi)
        {
            var result = await _caThiService.Update(id, caThi);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật ca thi thành công"));
        }

        #endregion

        #region Patch Methods

        [HttpPatch("{id:int}/active")]
        public async Task<IActionResult> KichHoatCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.Activate(id, true, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Kích hoạt ca thi thành công"));

        }

        [HttpPatch("{id:int}/deactive")]
        public async Task<IActionResult> HuyKichHoatCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.HuyKichHoat(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Hủy kích hoạt ca thi thành công"));
        }

        [HttpPatch("{id:int}/pause")]
        public async Task<IActionResult> DungCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.Activate(id, false, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Dừng ca thi thành công"));
        }

        [HttpPatch("{id:int}/finish")]
        public async Task<IActionResult> KetThucCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.Ketthuc(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Kết thúc ca thi thành công"));
        }

        [HttpPatch("{id:int}/update-dethi")]
        [Authorize(Roles = "KhaoThi")]
        public async Task<IActionResult> UpdateDeThi([FromRoute] int id, [FromBody] CaThiUpdateDeThiRequest request)
        {
            var result = await _caThiService.UpdateDeThi(id, request);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật ca thi thành công"));
        }

        [HttpPatch("{id:int}/update-audit")]
        public async Task<IActionResult> UpdateAudit([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.UpdateLichSuHoatDong(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật lịch sử hoạt động ca thi thành công"));
        }

        [HttpPatch("{id:int}/duyet-de")]
        [Authorize(Roles = "CNTT")]
        public async Task<IActionResult> DuyetDeThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.DuyetDe(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Duyệt đề thi thành công"));
        }

        [HttpPatch("{id:int}/all-reset-login")]
        public async Task<IActionResult> ResetAllLogin([FromRoute] int id)
        {
            var result = await _caThiService.UpdateAllResetLogin(id);
            if(!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Reset đăng nhập toàn bộ thí sinh thành công"));
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _caThiService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 2);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Xóa ca thi thành công"));
        }

        [HttpDelete("{id}/force")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _caThiService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 2);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Xóa ca thi thành công"));
        }


        #endregion

        #region Private Methods
        private async Task NotifyChangeStatusCaThiToSV(int ma_ca_thi)
        {
            await _sinhVienHub.Clients.Group(ma_ca_thi + "").SendAsync("ChangeStatusCaThi");
        }

        // các admin khác cũng nhận được sự thay đổi của TT ca thi
        private async Task NotifyChangeCaThiToAdmin(int ma_ca_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCaThi" : (function == 1) ? "UpdateCaThi" : "DeleteCaThi";
            await _adminHub.Clients.Group("admin").SendAsync(message, ma_ca_thi);
        }
        #endregion

    }
}
