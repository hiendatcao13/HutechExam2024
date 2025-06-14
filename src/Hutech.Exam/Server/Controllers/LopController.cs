﻿using System.Data.SqlClient;
using System.Threading.Tasks;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Lop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/lops")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LopController(LopService lopService) : Controller
    {
        #region Private Fields

        private readonly LopService _lopService = lopService;

        #endregion

        #region Get Methods

        [HttpGet("{id}")]
        public async Task<ActionResult<LopDto>> SelectOne([FromRoute] int id)
        {
            var result = await _lopService.SelectOne(id);
            if (result == null || result.MaLop == 0)
            {
                return NotFound(APIResponse<LopDto>.NotFoundResponse(message: "Không tìm thấy lớp học"));
            }
            return Ok(APIResponse<LopDto>.SuccessResponse(data: result, message: "Lấy lớp học thành công"));
        }


        [HttpGet("filter-by-khoa")]
        public async Task<ActionResult<Paged<LopDto>>> SelectBy_ma_khoa_Paged([FromQuery] int maKhoa, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(APIResponse<Paged<LopDto>>.SuccessResponse(data: await _lopService.SelectBy_ma_khoa_Paged(maKhoa, pageNumber, pageSize), message: "Lấy danh sách lớp thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        public async Task<ActionResult<LopDto>> Insert([FromBody] LopCreateRequest lop)
        {
            try
            {
                var id = await _lopService.Insert(lop);
                return Ok(APIResponse<LopDto>.SuccessResponse(data: await _lopService.SelectOne(id), message: "Thêm lớp học thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopDto>.ErrorResponse(message: "Thêm lớp học không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Put Methods

        [HttpPut("{id}")]
        public async Task<ActionResult<LopDto>> Update([FromRoute] int id, [FromBody] LopUpdateRequest lop)
        {
            try
            {
                var result = await _lopService.Update(id, lop);
                if (!result)
                {
                    return NotFound(APIResponse<LopDto>.NotFoundResponse(message: "Không tìm thấy lớp học"));
                }
                return Ok(APIResponse<LopDto>.SuccessResponse(data: await _lopService.SelectOne(id), message: "Cập nhật lớp học thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopDto>.ErrorResponse(message: "Cập nhật lớp học không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        public async Task<ActionResult<LopDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _lopService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<LopDto>.NotFoundResponse(message: "Không tìm thấy lớp học"));
                }
                return Ok(APIResponse<LopDto>.SuccessResponse(message: "Xóa lớp học thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopDto>.ErrorResponse(message: "Xóa lớp học không thành công", errorDetails: ex.Message));
            }

        }

        [HttpDelete("{id}/force")]
        public async Task<ActionResult<LopDto>> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _lopService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<LopDto>.NotFoundResponse(message: "Không tìm thấy lớp học"));
                }
                return Ok(APIResponse<LopDto>.SuccessResponse(message: "Xóa lớp học thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopDto>.ErrorResponse(message: "Xóa lớp học không thành công", errorDetails: ex.Message));
            }

        }

        #endregion

        #region Private Methods


        #endregion

    }
}
