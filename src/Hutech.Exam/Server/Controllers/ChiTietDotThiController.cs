using System.Data.SqlClient;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietDotThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietdotthis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ChiTietDotThiController(ChiTietDotThiService chiTietDotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly ChiTietDotThiService _chiTietDotThiService = chiTietDotThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        //////////////////POST//////////////////////////

        [HttpPost]
        public async Task<ActionResult<ChiTietDotThiDto>> Insert([FromBody] ChiTietDotThiCreateRequest chiTietDotThi)
        {
            try
            {
                var id = await _chiTietDotThiService.Insert(chiTietDotThi);
                await NotifyChangeChiTietDotThiToAdmin(id, 0);
                return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: await _chiTietDotThiService.SelectOne(id), message: "Thêm chi tiết đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<ChiTietDotThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<ChiTietDotThiDto>.ErrorResponse(message: "Thêm chi tiết đợt thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////GET////////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDotThiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _chiTietDotThiService.SelectOne(id);
            if(result.MaChiTietDotThi == 0)
            {
                return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết đợt thi"));
            }
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: result, message: "Lấy chi tiết đợt thi thành công"));
        }

        [HttpGet("filter-by-dotthi")]
        public async Task<ActionResult<List<ChiTietDotThiDto>>> SelectBy_MaDotThi([FromQuery] int maDotThi)
        {
            return Ok(APIResponse<List<ChiTietDotThiDto>>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi(maDotThi), message: "Lấy danh sách chi tiết đợt thi thành công"));
        }

        [HttpGet("filter-by-dotthi-paged")]
        public async Task<ActionResult<ChiTietDotThiPage>> SelectBy_MaDotThi_Paged([FromQuery] int maDotThi, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(APIResponse<ChiTietDotThiPage>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi_Paged(maDotThi, pageNumber, pageSize), message: "Lấy danh sách chi tiết đợt thi thành công"));
        }


        [HttpGet("filter-by-dotthi-lopao")]
        public async Task<ActionResult<List<ChiTietDotThiDto>>> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int maDotThi, [FromQuery] int maLopAo)
        {
            return Ok(APIResponse<List<ChiTietDotThiDto>>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo(maDotThi, maLopAo), message: "Lấy danh sách chi tiết đợt thi thành công"));
        }

        [HttpGet("filter-by-dotthi-lopao-lanthi")]
        public async Task<ActionResult<ChiTietDotThiDto>> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int maDotThi, [FromQuery] int maLopAo, [FromQuery] int lanThi)
        {
            return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo_LanThi(maDotThi, maLopAo, lanThi), message: "Lấy tiết đợt thi thành công"));
        }

        //////////////////PUT///////////////////////////

        [HttpPut("{id}")]
        public async Task<ActionResult<ChiTietDotThiDto>> Update([FromRoute] int id, [FromBody] ChiTietDotThiUpdateRequest chiTietDotThi)
        {
            try
            {
                var result = await _chiTietDotThiService.Update(id, chiTietDotThi);
                if(!result)
                {
                    return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết đợt thi cần cập nhật"));
                }    
                await NotifyChangeChiTietDotThiToAdmin(id, 1);
                return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(data: await _chiTietDotThiService.SelectOne(id), message: "Cập nhật chi tiết đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<ChiTietDotThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<ChiTietDotThiDto>.ErrorResponse(message: "Cập nhật chi tiết đợt thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        //////////////////DELETE//////////////////////////

        [HttpDelete("{id}")]
        public async Task<ActionResult<ChiTietDotThiDto>> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _chiTietDotThiService.Remove(id);
                if(!result)
                {
                    return NotFound(APIResponse<ChiTietDotThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết đợt thi cần xóa"));
                }    
                await NotifyChangeChiTietDotThiToAdmin(id, 2);
                return Ok(APIResponse<ChiTietDotThiDto>.SuccessResponse(message: "Xóa chi tiết đợt thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<ChiTietDotThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<ChiTietDotThiDto>.ErrorResponse(message: "Xóa chi tiết đợt thi không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


        private async Task NotifyChangeChiTietDotThiToAdmin(int ma_chi_tiet_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCTDotThi" : (function == 1) ? "UpdateCTDotThi" : "DeleteCTDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_chi_tiet_dot_thi);
        }
    }
}
