using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietbaithis")]
    [ApiController]
    [Authorize]
    public class ChiTietBaiThiController(ChiTietBaiThiService chiTietBaiThiService) : Controller
    {
        private readonly ChiTietBaiThiService _chiTietBaiThiService = chiTietBaiThiService;

        //////////////////CRUD///////////////////////////

        //-- Phần CRUD được sử dụng vào trong Redis Service giao tiếp qua SignalR không phải api

        //////////////////FILTER///////////////////////////
        
        [HttpGet("filter-by-chitietcathi")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse<List<ChiTietBaiThiDto>>>> SelectBy_ma_chi_tiet_ca_thi([FromQuery] int maChiTietCaThi)
        {
            var result = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(maChiTietCaThi);
            return Ok(APIResponse<List<ChiTietBaiThiDto>>.SuccessResponse(data: result, message: "Lấy chi tiết bài thi thành công"));
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////

        
    }
}
