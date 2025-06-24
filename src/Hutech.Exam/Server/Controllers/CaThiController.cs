using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cathis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaThiController(CaThiService caThiService, IHubContext<AdminHub> adminHub, IHubContext<SinhVienHub> sinhVienHub) : Controller
    {
        #region Private Fields
        private readonly CaThiService _caThiService = caThiService;

        private readonly IHubContext<AdminHub> _adminHub = adminHub;
        private readonly IHubContext<SinhVienHub> _sinhVienHub = sinhVienHub;
        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CaThiDto>> SelectOne([FromRoute] int id)
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
        public async Task<ActionResult<bool>> IsActiveCaThi([FromRoute] int id)
        {
            CaThiDto caThi = await _caThiService.SelectOne(id);
            return Ok(APIResponse<bool>.SuccessResponse(data: caThi.IsActivated, message: "Lấy thông tin ca thi thành công"));
        }

        [HttpGet("filter-by-dotthi-lopao-lanthi")]
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_MaDotThi_MaLopAo_LanThi([FromQuery] int maDotThi, [FromQuery] int maLopAo, [FromQuery] int lanThi)
        {
            var result = await _caThiService.SelectBy_MaDotThi_MaLop_LanThi(maDotThi, maLopAo, lanThi);
            return Ok(APIResponse<List<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));

        }

        [HttpGet("filter-by-chitietdotthi-paged")]
        public async Task<ActionResult<Paged<CaThiDto>>> SelectBy_ma_chi_tiet_dot_thi_Paged([FromQuery] int maChiTietDotThi, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi_Paged(maChiTietDotThi, pageNumber, pageSize);
            return Ok(APIResponse<Paged<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        [HttpGet("filter-by-chitietdotthi-search-paged")]
        public async Task<ActionResult<Paged<CaThiDto>>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged([FromQuery] int maChiTietDotThi, [FromQuery] string keyword, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi_Search_Paged(maChiTietDotThi, keyword, pageNumber, pageSize);
            return Ok(APIResponse<Paged<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<ActionResult<CaThiDto>> Insert([FromBody] CaThiCreateRequest caThi)
        {
            try
            {
                var id = await _caThiService.Insert(caThi);
                await NotifyChangeCaThiToAdmin(id, 0);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), "Thêm ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Thêm ca thi không thành công", errorDetails: ex.Message));
            }

        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CaThiDto>> Update([FromRoute] int id, [FromBody] CaThiUpdateRequest caThi)
        {
            try
            {
                var result = await _caThiService.Update(id, caThi);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần cập nhật"));
                }
                await NotifyChangeCaThiToAdmin(id, 1);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật ca thi thành công"));

            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Cập nhật ca thi không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Patch Methods

        [HttpPatch("{id:int}/active")]
        public async Task<ActionResult> KichHoatCaThi([FromRoute] int id)
        {
            try
            {
                var result = await _caThiService.Activate(id, true);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần kích hoạt"));
                }
                await NotifyChangeCaThiToAdmin(id, 1);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Kích hoạt ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Kích hoạt ca thi không thành công", errorDetails: ex.Message));
            }
        }

        [HttpPatch("{id:int}/deactive")]
        public async Task<ActionResult> HuyKichHoatCaThi([FromRoute] int id)
        {
            try
            {
                var result = await _caThiService.HuyKichHoat(id);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần hủy kích hoạt"));
                }
                await NotifyChangeStatusCaThiToSV(id);
                await NotifyChangeCaThiToAdmin(id, 1);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Hủy kích hoạt ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<object>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Hủy kích hoạt ca thi không thành công", errorDetails: ex.Message));
            }
        }

        [HttpPatch("{id:int}/pause")]
        public async Task<ActionResult> DungCaThi([FromRoute] int id)
        {
            try
            {
                var result = await _caThiService.Activate(id, false);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần dừng"));
                }
                await NotifyChangeStatusCaThiToSV(id);
                await NotifyChangeCaThiToAdmin(id, 1);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Dừng ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<object>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Dừng ca thi không thành công", errorDetails: ex.Message));
            }
        }

        [HttpPatch("{id:int}/finish")]
        public async Task<ActionResult> KetThucCaThi([FromRoute] int id)
        {
            try
            {
                var result = await _caThiService.Ketthuc(id);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần kết thúc"));
                }
                await NotifyChangeStatusCaThiToSV(id);
                await NotifyChangeCaThiToAdmin(id, 1);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Kết thúc ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<object>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Kết thúc ca thi không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        public async Task<ActionResult<CaThiDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _caThiService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần xóa"));
                }
                await NotifyChangeCaThiToAdmin(id, 2);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Xóa ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<object>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Xóa ca thi không thành công hoặc đang dính phải ràng buộc khóa ngoại", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id}/force")]
        public async Task<ActionResult<CaThiDto>> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _caThiService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi cần xóa"));
                }
                await NotifyChangeCaThiToAdmin(id, 2);
                return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Xóa ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<object>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Xóa ca thi không thành công", errorDetails: ex.Message));
            }
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
