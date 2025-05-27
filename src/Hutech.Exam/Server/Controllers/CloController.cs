using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.Clo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/clos")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CloController(CloService cloService) : Controller
    {
        private readonly CloService _cloService = cloService;

        //////////////////CRUD///////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<CloDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _cloService.SelectOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] CloCreateRequest clo)
        {
            return Ok(await _cloService.Insert(clo.MaMonHoc, clo.MaSoClo, clo.TieuDe, clo.NoiDung, clo.TieuChi, clo.SoCau));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CloUpdateRequest clo)
        {
            await _cloService.Update(id, clo.MaMonHoc, clo.MaSoClo, clo.TieuDe, clo.NoiDung, clo.TieuChi, clo.SoCau);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            await _cloService.Remove(id);
            return Ok();
        }

        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<CloDto>>> SelectBy_MaMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(await _cloService.SelectBy_MaMonHoc(maMonHoc));
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////



    }
}
