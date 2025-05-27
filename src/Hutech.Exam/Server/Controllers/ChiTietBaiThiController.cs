using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
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
        public async Task<ActionResult<List<ChiTietBaiThiDto>>> SelectBy_ma_chi_tiet_ca_thi([FromQuery] int maChiTietCaThi)
        {
            return Ok(await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(maChiTietCaThi));
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////

        
    }
}
