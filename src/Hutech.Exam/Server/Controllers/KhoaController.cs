using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/khoas")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KhoaController(KhoaService khoaService) : Controller
    {
        private readonly KhoaService _khoaService = khoaService;

        //////////////////CRUD///////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<KhoaDto>>> GetAllKhoa()
        {
            return Ok(await _khoaService.GetAll());
        }

        //////////////////FILTER///////////////////////////

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
