using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CaThiController : Controller
    {
        private readonly CaThiService _caThiService;
        public CaThiController(CaThiService caThiService)
        {
            _caThiService = caThiService;
        }
        [HttpGet("IsActiveCaThi")]
        public async Task<ActionResult<bool>> IsActiveCaThi([FromQuery] int ma_ca_thi)
        {
            CaThiDto caThi = await _caThiService.SelectOne(ma_ca_thi);
            return (caThi.IsActivated);
        }
    }
}
