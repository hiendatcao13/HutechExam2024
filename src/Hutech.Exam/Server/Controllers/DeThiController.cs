using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dethis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DeThiController(DeThiService deThiService) : Controller
    {
        private readonly DeThiService _deThiService = deThiService;

        //////////////////CRUD///////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<DeThiDto>>> GetAll()
        {
            return Ok(await _deThiService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeThiDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _deThiService.SelectOne(id));
        }


        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<DeThiDto>>> SelectByMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(await _deThiService.SelectByMonHoc(maMonHoc));
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////



    }
}
