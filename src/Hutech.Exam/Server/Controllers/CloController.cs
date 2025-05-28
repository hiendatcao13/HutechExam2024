using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.Clo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/clos")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CloController(CloService cloService) : Controller
    {
        private readonly CloService _cloService = cloService;

        private const string NOT_FOUND = "Không tìm thấy CLO";
        private const string SUCCESS = "Lấy CLO thành công";

        //////////////////CREATE//////////////////////////

        [HttpPost]
        public async Task<ActionResult<CloDto>> Insert([FromBody] CloCreateRequest clo)
        {
            try
            {
                var id = await _cloService.Insert(clo.MaMonHoc, clo.MaSoClo, clo.TieuDe, clo.NoiDung, clo.TieuChi, clo.SoCau);
                return Ok(APIResponse<CloDto>.SuccessResponse(data: await _cloService.SelectOne(id), message: "Thêm CLO thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Thêm CLO không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////READ////////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<CloDto>> SelectOne([FromRoute] int id)
        {
            var result = await _cloService.SelectOne(id);
            if(result.MaClo == 0)
            {
                return NotFound(APIResponse<CloDto>.NotFoundResponse(message: NOT_FOUND));
            }    
            return Ok(APIResponse<CloDto>.SuccessResponse(data: result, message: SUCCESS));
        }

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<CloDto>>> SelectBy_MaMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(APIResponse<List<CloDto>>.SuccessResponse(data: await _cloService.SelectBy_MaMonHoc(maMonHoc), message: SUCCESS));
        }

        //////////////////UDATE///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<CloDto>> Update([FromRoute] int id, [FromBody] CloUpdateRequest clo)
        {
            try
            {
                var result = await _cloService.Update(id, clo.MaMonHoc, clo.MaSoClo, clo.TieuDe, clo.NoiDung, clo.TieuChi, clo.SoCau);
                if(!result)
                {
                    return NotFound(APIResponse<CloDto>.NotFoundResponse(message: NOT_FOUND));
                }
                return Ok(APIResponse<CloDto>.SuccessResponse(data: await _cloService.SelectOne(id), message: "Cập nhật CLO thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Cập nhật CLO không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _cloService.Remove(id);
                if(!result)
                {
                    return NotFound(APIResponse<CloDto>.NotFoundResponse(message: NOT_FOUND));
                }    
                return Ok();
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CloDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CloDto>.ErrorResponse(message: "Xóa CLO không thành công", errorDetails: ex.Message));
            }
        }


        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////



    }
}
