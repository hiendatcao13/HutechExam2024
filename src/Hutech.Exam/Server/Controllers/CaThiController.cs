﻿using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cathis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaThiController(CaThiService caThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly CaThiService _caThiService = caThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        //////////////////POST//////////////////////////

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


        //////////////////GET////////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<CaThiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _caThiService.SelectOne(id);

            if (result.MaCaThi == 0)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi"));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(result, "Lấy ca thi thành công"));
        }

        [HttpGet("{id}/is-active")]
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

        [HttpGet("filter-by-chitietdotthi")]
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_ma_chi_tiet_dot_thi([FromQuery] int maChiTietDotThi)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi(maChiTietDotThi);
            return Ok(APIResponse<List<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        //////////////////PUT///////////////////////////

        [HttpPut("{id}")]
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

        //////////////////PATCH///////////////////////////


        [HttpPatch("{id}/active")]
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

        [HttpPatch("{id}/deactive")]
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

        [HttpPatch("{id}/pause")]
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

        [HttpPatch("{id}/finish")]
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

        //////////////////DELETE//////////////////////////

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
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Xóa ca thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PRIVATE///////////////////////////
        private async Task NotifyChangeStatusCaThiToSV(int ma_ca_thi)
        {
            await _mainHub.Clients.Group(ma_ca_thi + "").SendAsync("ChangeStatusCaThi");
        }

        // các admin khác cũng nhận được sự thay đổi của TT ca thi
        private async Task NotifyChangeCaThiToAdmin(int ma_ca_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCaThi" : (function == 1) ? "UpdateCaThi" : "DeleteCaThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_ca_thi);
        }
    }
}
