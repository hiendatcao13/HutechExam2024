using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.MonHoc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/monhocs")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MonHocController(MonHocService monHocService) : Controller
    {
        private readonly MonHocService _monHocService = monHocService;

        //////////////////CREATE//////////////////////////

        [HttpPost]
        public async Task<ActionResult<MonHocDto>> Insert([FromBody] MonHocCreateRequest monHoc)
        {
            try
            {
                var id = await _monHocService.Insert(monHoc.MaSoMonHoc, monHoc.TenMonHoc);
                return Ok(APIResponse<MonHocDto>.SuccessResponse(data: await _monHocService.SelectOne(id), message: "Thêm môn học thành công"))
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<MonHocDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<MonHocDto>.ErrorResponse(message: "Thêm môn học không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////READ////////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<MonHocDto>>> GetAll()
        {
            return Ok(APIResponse<List<MonHocDto>>.SuccessResponse(data: await _monHocService.GetAll(), message: "Lấy danh sách môn học thành công"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonHocDto>> SelectOne([FromRoute] int id)
        {
            var result = await _monHocService.SelectOne(id);
            if(result.MaMonHoc == 0)
            {
                return NotFound(APIResponse<MonHocDto>.NotFoundResponse(message: "Không tìm thấy môn học"));
            }
            return Ok(APIResponse<MonHocDto>.SuccessResponse(data: result, message: "Lấy môn học thành công"));
        }

        //////////////////UDATE///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<MonHocDto>> Update([FromRoute] int id, [FromBody] MonHocUpdateRequest monHoc)
        {
            try
            {
                var result = await _monHocService.Update(id, monHoc.MaSoMonHoc, monHoc.TenMonHoc);
                if(!result)
                {
                    return NotFound(APIResponse<MonHocDto>.NotFoundResponse(message: "Không tìm thấy môn học"));
                }    
                return Ok(APIResponse<MonHocDto>.SuccessResponse(data: await _monHocService.SelectOne(id), message: "Cập nhật môn học thành công"))
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<MonHocDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<MonHocDto>.ErrorResponse(message: "Cập nhật môn học không thành công", errorDetails: ex.Message));
            }
        }


        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult<MonHocDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _monHocService.Remove(id);
                if(!result)
                {
                    return NotFound(APIResponse<MonHocDto>.NotFoundResponse(message: "Không tìm thấy môn học"));
                }
                return Ok(APIResponse<MonHocDto>.SuccessResponse(message: "Xóa môn học thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<MonHocDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<MonHocDto>.ErrorResponse(message: "Xóa môn học không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
