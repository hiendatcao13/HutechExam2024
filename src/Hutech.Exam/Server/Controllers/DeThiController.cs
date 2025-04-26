using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DeThiController : Controller
    {
        private readonly DeThiService _deThiService;
        public DeThiController(DeThiService deThiService)
        {
            _deThiService = deThiService;
        }
        [HttpGet("SelectOne")]
        public async Task<ActionResult<DeThiDto>> SelectOne([FromQuery]int ma_de_thi)
        {
            return Ok(await _deThiService.SelectOne(ma_de_thi));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DeThiDto>>> GetAll()
        {
            return Ok(await _deThiService.GetAll());
        }
        [HttpGet("SelectByMonHoc")]
        public async Task<ActionResult<List<DeThiDto>>> SelectByMonHoc([FromQuery] int ma_mon_hoc)
        {
            return Ok(await _deThiService.SelectByMonHoc(ma_mon_hoc));
        }
    }
}
