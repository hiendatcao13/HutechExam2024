using System.Data.SqlClient;
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

        //////////////////CREATE//////////////////////////

        [HttpPost]
        public async Task<ActionResult<NhomCauHoiDto>> Insert([FromBody] NhomCauHoiCreateRequest nhomCauHoi)
        {
            try
            {
                var id = await _nhomCauHoiService.Insert(nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha, nhomCauHoi.SoCauLay, nhomCauHoi.LaCauHoiNhom);
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

        //////////////////READ////////////////////////////

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

        //////////////////UDATE///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] NhomCauHoiUpdateRequest nhomCauHoi)
        {
            //try
            //{
            //    var result = await _nhomCauHoiService.Update(id, nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha);
            //    return Ok();
            //}
            
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _nhomCauHoiService.Remove(id);
            return Ok();
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
