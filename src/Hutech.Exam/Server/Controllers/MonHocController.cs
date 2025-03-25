using Hutech.Exam.Server.BUS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MonHocController : Controller
    {
        private readonly MonHocService _monHocService;
        public MonHocController(MonHocService monHocService)
        {
            _monHocService = monHocService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _monHocService.GetAll());
        }
    }
}
