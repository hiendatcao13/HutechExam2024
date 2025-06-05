using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dethis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DeThiController(DeThiService deThiService) : Controller
    {
        private readonly DeThiService _deThiService = deThiService;


        //////////////////POST//////////////////////////

        [HttpPost]
        public async Task<ActionResult<DeThiDto>> Insert([FromBody] DeThiCreateRequest deThi)
        {
            try
            {
                var id = await _deThiService.Insert(deThi);
                return Ok(APIResponse<DeThiDto>.SuccessResponse(data: await _deThiService.SelectOne(id), message: "Thêm đề thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<DeThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<DeThiDto>.ErrorResponse(message: "Thêm đề thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////GET////////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<DeThiDto>>> GetAll()
        {
            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: await _deThiService.GetAll(), message: "Lấy danh sách đề thi thành công"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeThiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _deThiService.SelectOne(id);
            if(result.MaDeThi == 0)
            {
                return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: "Không tìm thấy đề thi"));
            }    
            return Ok(APIResponse<DeThiDto>.SuccessResponse(data: result, message: "Lấy đề thi thành công"));
        }

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<DeThiDto>>> SelectByMonHoc([FromQuery] int maMonHoc, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var pagedResult = await _deThiService.SelectByMonHoc_Paged(maMonHoc, pageNumber.Value, pageSize.Value);
                return Ok(APIResponse<Paged<DeThiDto>>.SuccessResponse(pagedResult, message: "Lấy danh sách đề thi thành công"));
            }
            return Ok(APIResponse<List<DeThiDto>>.SuccessResponse(data: await _deThiService.SelectByMonHoc(maMonHoc), message: "Lấy danh sách đề thi thành công"));
        }



        //////////////////PUT///////////////////////////

        [HttpPost("{id}")]
        public async Task<ActionResult<DeThiDto>> Update([FromRoute] int id, [FromBody] DeThiUpdateRequest deThi)
        {
            try
            {
                var result = await _deThiService.Update(id, deThi);
                if (!result)
                {
                    return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: "Không tìm thấy đề thi để cập nhật"));
                }
                return Ok(APIResponse<DeThiDto>.SuccessResponse(data: await _deThiService.SelectOne(id), message: "Cập nhật đề thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<DeThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<DeThiDto>.ErrorResponse(message: "Cập nhật đề thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeThiDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _deThiService.Delete(id);
                if (!result)
                {
                    return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: "Không tìm thấy đề thi để xóa"));
                }
                return Ok(APIResponse<DeThiDto>.SuccessResponse(message: "Xóa đề thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<DeThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<DeThiDto>.ErrorResponse(message: "Xóa đề thi không thành công hoặc đang dính phải ràng buộc khóa ngoại", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id}/force")]
        public async Task<ActionResult<DeThiDto>> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _deThiService.ForceDelete(id);
                if (!result)
                {
                    return NotFound(APIResponse<DeThiDto>.NotFoundResponse(message: "Không tìm thấy đề thi để xóa"));
                }
                return Ok(APIResponse<DeThiDto>.SuccessResponse(message: "Xóa đề thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<DeThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<DeThiDto>.ErrorResponse(message: "Xóa đề thi không thành công hoặc đang dính phải ràng buộc khóa ngoại", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////



    }
}
