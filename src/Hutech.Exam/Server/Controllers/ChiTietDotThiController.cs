using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ChiTietDotThiController : Controller
    {
        private readonly ChiTietDotThiService _chiTietDotThiService;
        public ChiTietDotThiController(ChiTietDotThiService chiTietDotThiService)
        {
            _chiTietDotThiService = chiTietDotThiService;
        }
        [HttpGet("LanThis_SelectBy_MaDotThiMaLopAo")]
        public async Task<ActionResult<List<string>>> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int ma_dot_thi, [FromQuery] int ma_lop_ao)
        {
            List<string> result = [];
            var chiTietDotThis = await SelectBy_MaDotThiMaLopAo(ma_dot_thi, ma_lop_ao);
            foreach (var item in chiTietDotThis)
            {
                result.Add(item.LanThi);
            }
            return Ok(result);
        }




        private async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThiMaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            return await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao);
        }

    }
}
