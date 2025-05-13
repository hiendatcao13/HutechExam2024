using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KhoaController(KhoaService khoaService) : Controller
    {
        private readonly KhoaService _khoaService = khoaService;

        [HttpGet("GetAllKhoa")]
        public async Task<ActionResult<List<KhoaDto>>> GetAllKhoa()
        {
            return Ok(await _khoaService.GetAll());
        }
    }
}
