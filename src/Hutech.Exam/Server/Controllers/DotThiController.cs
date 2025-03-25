using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DotThiController : Controller
    {
        private readonly DotThiService _dotThiService;

        public DotThiController(DotThiService dotThiService) 
        {
            _dotThiService = dotThiService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DotThiDto>>> GetAll()
        {
            return Ok(await _dotThiService.GetAll());
        }
    }
}
