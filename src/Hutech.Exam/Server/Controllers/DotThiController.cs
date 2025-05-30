using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
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
                await NotifyChangeDotThiToAdmin(id, 0);
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
        public async Task<ActionResult<List<DotThiDto>>> GetAll()
        {
            return Ok(APIResponse<List<DotThiDto>>.SuccessResponse(data: await _dotThiService.GetAll(), message: "Lấy danh sách đợt thi thành công"));
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
                if(!result)
                {
                    return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: "Không tìm thấy đợt thi cần cập nhật"));
                }    
                await NotifyChangeDotThiToAdmin(id, 1);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<DotThiDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _dotThiService.Remove(id);
                if(!result)
                {
                    return NotFound(APIResponse<DotThiDto>.NotFoundResponse(message: "Không tìm thấy đợt thi cần xóa"));
                }    
                await NotifyChangeDotThiToAdmin(id, 2);
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

        private async Task NotifyChangeDotThiToAdmin(int ma_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertDotThi" : (function == 1) ? "UpdateDotThi" : "DeleteDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_dot_thi);
        }
    }
}
