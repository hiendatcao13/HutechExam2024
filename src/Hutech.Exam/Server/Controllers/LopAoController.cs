using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.LopAo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/lopaos")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LopAoController(LopAoService lopAoService) : Controller
    {
        private readonly LopAoService _lopAoService = lopAoService;

        //////////////////CREATE//////////////////////////

        [HttpPost]
        public async Task<ActionResult<LopAoDto>> Insert([FromBody] LopAoCreateRequest lopAo)
        {
            try
            {
                var id = await _lopAoService.Insert(lopAo.TenLopAo, lopAo.NgayBatDau, lopAo.MaMonHoc);
                return Ok(APIResponse<LopAoDto>.SuccessResponse(data: await _lopAoService.SelectOne(id), message: "Thêm lớp ảo thành công"))
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopAoDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopAoDto>.ErrorResponse(message: "Thêm lớp ảo không thành công", errorDetails: ex.Message));
            }
        }


        //////////////////READ////////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<LopAoDto>> SelectOne([FromRoute] int id)
        {
            var result = await _lopAoService.SelectOne(id);
            if(result.MaLopAo == 0)
            {
                return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: "Không tìm thấy lớp ảo"));
            }    
            return Ok(APIResponse<LopAoDto>.SuccessResponse(data: result, message: "Lấy lớp ảo thành công"));
        }

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<LopAoDto>>> SelectBy_MaMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(APIResponse<List<LopAoDto>>.SuccessResponse(data: await _lopAoService.SelectBy_ma_mon_hoc(maMonHoc), message: "Lấy danh sách môn học thành công"));
        }

        //////////////////UDATE///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<LopAoDto>> Update([FromRoute] int id, [FromBody] LopAoUpdateRequest lopAo)
        {
            try
            {
                var result = await _lopAoService.Update(id, lopAo.TenLopAo, lopAo.NgayBatDau, lopAo.MaMonHoc);
                if(!result)
                {
                    return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: "Không tìm thấy lớp ảo"));
                }
                return Ok(APIResponse<LopAoDto>.SuccessResponse(data: await _lopAoService.SelectOne(id), message: "Cập nhật lớp ảo thành công"))
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopAoDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopAoDto>.ErrorResponse(message: "Cập nhật lớp ảo không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _lopAoService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<LopAoDto>.NotFoundResponse(message: "Không tìm thấy lớp ảo cần xóa"));
                }
                return Ok(APIResponse<LopAoDto>.SuccessResponse("Xóa lớp ảo thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<LopAoDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<LopAoDto>.ErrorResponse(message: "Xóa lớp ảo không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
