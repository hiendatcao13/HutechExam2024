﻿using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.NhomCauHoi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/nhomcauhois")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class NhomCauHoiController(NhomCauHoiService nhomCauHoiService) : Controller
    {
        private readonly NhomCauHoiService _nhomCauHoiService = nhomCauHoiService;

        //////////////////POST//////////////////////////

        [HttpPost]
        public async Task<ActionResult<NhomCauHoiDto>> Insert([FromBody] NhomCauHoiCreateRequest nhomCauHoi)
        {
            try
            {
                var id = await _nhomCauHoiService.Insert(nhomCauHoi);
                return Ok(APIResponse<NhomCauHoiDto>.SuccessResponse(data: await _nhomCauHoiService.SelectOne(id), message: "Thêm nhóm câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<NhomCauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<NhomCauHoiDto>.ErrorResponse(message: "Thêm nhóm câu hỏi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////CREATE////////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<NhomCauHoiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _nhomCauHoiService.SelectOne(id);
            if(result.MaNhom == 0)
            {
                return NotFound(APIResponse<NhomCauHoiDto>.NotFoundResponse(message: "Không tìm thấy nhóm câu hỏi"));
            }    
            return Ok(APIResponse<NhomCauHoiDto>.SuccessResponse(data: result, message: "Lấy nhóm câu hỏi thành công"));
        }

        [HttpGet("filter-by-dethi")]
        public async Task<ActionResult<List<NhomCauHoiDto>>> SelectAllBy_MaDeThi([FromQuery] int maDeThi)
        {
            return Ok(APIResponse<List<NhomCauHoiDto>>.SuccessResponse(data: await _nhomCauHoiService.SelectAllBy_MaDeThi(maDeThi), message: "Lấy danh sách nhóm câu hỏi thành công"));
        }

        //////////////////PUT///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<NhomCauHoiDto>> Update([FromRoute] int id, [FromBody] NhomCauHoiUpdateRequest nhomCauHoi)
        {
            try
            {
                var result = await _nhomCauHoiService.Update(id, nhomCauHoi);
                if(!result)
                {
                    return NotFound(APIResponse<NhomCauHoiDto>.NotFoundResponse(message: "Không tìm thấy nhóm câu hỏi cần cập nhật"));
                }
                return Ok(APIResponse<NhomCauHoiDto>.SuccessResponse(data: await _nhomCauHoiService.SelectOne(id), message: "Cập nhật nhóm câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<NhomCauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<NhomCauHoiDto>.ErrorResponse(message: "Cập nhật nhóm câu hỏi không thành công", errorDetails: ex.Message));
            }

        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _nhomCauHoiService.Remove(id);
                if(!result)
                {
                    return NotFound(APIResponse<NhomCauHoiDto>.NotFoundResponse(message: "Không tìm thấy nhóm câu hỏi cần xóa"));
                }
                return Ok(APIResponse<NhomCauHoiDto>.SuccessResponse(message: "Xóa nhóm câu hỏi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<NhomCauHoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<NhomCauHoiDto>.ErrorResponse(message: "Xóa nhóm câu hỏi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
