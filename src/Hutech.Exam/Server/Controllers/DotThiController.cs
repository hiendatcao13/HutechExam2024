using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DotThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dotthis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DotThiController(DotThiService dotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly DotThiService _dotThiService = dotThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        //////////////////POST//////////////////////////

        [HttpPost]
        public async Task<ActionResult<DotThiDto>> Insert([FromBody] DotThiCreateRequest dotThi)
        {
            try
            {
                var id = await _dotThiService.Insert(dotThi);
                return Ok(APIResponse<DotThiDto>.SuccessResponse(data: await _dotThiService.SelectOne(id), message: "Thêm đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Thêm đợt thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////GET////////////////////////////

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var pagedResult = await _dotThiService.GetAll_Paged(pageNumber.Value, pageSize.Value);
                return Ok(APIResponse<Paged<DotThiDto>>.SuccessResponse(pagedResult, "Lấy danh sách đợt thi thành công"));
            }
            var list = await _dotThiService.GetAll();
            return Ok(APIResponse<List<DotThiDto>>.SuccessResponse(list, "Lấy danh sách đợt thi thành công"));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DotThiDto>> SelectOne([FromRoute] int id)
        {
            return Ok(APIResponse<DotThiDto>.SuccessResponse(data: await _dotThiService.SelectOne(id), message: "Lấy đợt thi thành công"));
        }

        //////////////////PUT///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<DotThiDto>> Update([FromRoute] int id, [FromBody] DotThiUpdateRequest dotThi)
        {
            try
            {
                var result = await _dotThiService.Update(id, dotThi);
                if (!result)
                {
                    return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: "Không tìm thấy đợt thi cần cập nhật"));
                }
                return Ok(APIResponse<DotThiDto>.SuccessResponse(data: await _dotThiService.SelectOne(id), message: "Cập nhật đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Cập nhật đợt thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}/force")]
        public async Task<ActionResult<DotThiDto>> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _dotThiService.ForceRemove(id);
                if (!result)
                {
                    return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: "Không tìm thấy đợt thi cần xóa"));
                }
                return Ok(APIResponse<DotThiDto>.SuccessResponse(message: "Xóa đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Xóa đợt thi không thành công hoặc đang dính phải ràng buộc khóa ngoại", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DotThiDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _dotThiService.Remove(id);
                if (!result)
                {
                    return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: "Không tìm thấy đợt thi cần xóa"));
                }
                return Ok(APIResponse<DotThiDto>.SuccessResponse(message: "Xóa đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Xóa đợt thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////

    }
}
