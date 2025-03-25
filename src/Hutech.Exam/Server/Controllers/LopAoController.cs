using Hutech.Exam.Server.BUS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LopAoController : Controller
    {
        private readonly LopAoService _lopAoService;
        public LopAoController(LopAoService lopAoService)
        {
            _lopAoService = lopAoService;
        }
        [HttpGet("SelectBy_MaMonHoc")]
        public async Task<IActionResult> SelectBy_MaMonHoc([FromQuery] int ma_mon_hoc)
        {
            return Ok(await _lopAoService.SelectBy_ma_mon_hoc(ma_mon_hoc));
        }
    }
}
