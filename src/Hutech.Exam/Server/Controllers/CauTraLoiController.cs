using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO.Request.CauTraLoi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cautralois")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauTraLoiController(CauTraLoiService cauTraLoiService) : Controller
    {
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;

        //////////////////GET///////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<CauTraLoiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _cauTraLoiService.SelectOne(id);
            if(result.MaCauTraLoi == 0)
            {
                return NotFound(APIResponse<CauTraLoiDto>.NotFoundResponse(message: "Không tìm thấy câu trả lời"));
            }
            return Ok(APIResponse<CauTraLoiDto>.SuccessResponse(data: result, message: "Lấy câu trả lời thành công"));
        }

        //////////////////POST//////////////////////////

        [HttpPost]
        public async Task<ActionResult<CauTraLoiDto>> Insert([FromBody] CauTraLoiCreateRequest cauTraLoi)
        {
            try
            {
                int id = await _cauTraLoiService.Insert(cauTraLoi);
                return Ok(APIResponse<CauTraLoiDto>.SuccessResponse(data: await _cauTraLoiService.SelectOne(id), message: "Thêm câu trả lời thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauTraLoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauTraLoiDto>.ErrorResponse(message: "Thêm câu trả lời không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////GET////////////////////////////

        [HttpGet("filter-by-cauhoi")]
        public async Task<ActionResult<List<CauTraLoiDto>>> SelectBy_MaCauHoi([FromQuery] int maCauHoi)
        {
            var result = _cauTraLoiService.SelectBy_MaCauHoi(maCauHoi);
            return Ok(APIResponse<List<CauTraLoiDto>>.SuccessResponse(data: await _cauTraLoiService.SelectBy_MaCauHoi(maCauHoi), message: "Lấy danh sách câu trả lời thành công"));
        }

        //////////////////PUT///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<CauTraLoiDto>> Update([FromRoute] int id, [FromBody] CauTraLoiUpdateRequest cauTraLoi)
        {
            try
            {
                var result = await _cauTraLoiService.Update(id, cauTraLoi);
                if(!result)
                {
                    return NotFound(APIResponse<CauTraLoiDto>.NotFoundResponse(message: "Không tìm thấy câu trả lời cần cập nhật"));
                }    
                return Ok(APIResponse<CauTraLoiDto>.SuccessResponse(data: await _cauTraLoiService.SelectOne(id), message: "Cập nhật câu trả lời thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauTraLoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauTraLoiDto>.ErrorResponse(message: "Cập nhật câu trả lời không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult<CauTraLoiDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _cauTraLoiService.Remove(id);
                if(!result)
                {
                    return NotFound(APIResponse<CauTraLoiDto>.NotFoundResponse(message: "Không tìm thấy câu trả lời cần xóa"));
                }    
                return Ok(APIResponse<CauTraLoiDto>.SuccessResponse(message: "Xóa câu trả lời thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauTraLoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauTraLoiDto>.ErrorResponse(message: "Xóa câu trả lời không thành công", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id}/force")]
        public async Task<ActionResult<CauTraLoiDto>> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _cauTraLoiService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<CauTraLoiDto>.NotFoundResponse(message: "Không tìm thấy câu trả lời cần xóa"));
                }
                return Ok(APIResponse<CauTraLoiDto>.SuccessResponse(message: "Xóa câu trả lời thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CauTraLoiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CauTraLoiDto>.ErrorResponse(message: "Xóa câu trả lời không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PRIVATE//////////////////////////
    }
}
