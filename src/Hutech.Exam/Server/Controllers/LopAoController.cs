using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.LopAo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/lopaos")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LopAoController(LopAoService lopAoService) : Controller
    {
        private readonly LopAoService _lopAoService = lopAoService;

        //////////////////CRUD///////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<LopAoDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _lopAoService.SelectOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] LopAoCreateRequest lopAo)
        {
            return Ok(await _lopAoService.Insert(lopAo.TenLopAo, lopAo.NgayBatDau, lopAo.MaMonHoc));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] int id, [FromBody] LopAoUpdateRequest lopAo)
        {
            return Ok(await _lopAoService.Update(id, lopAo.TenLopAo, lopAo.NgayBatDau, lopAo.MaMonHoc));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            return Ok(await _lopAoService.Remove(id));
        }

        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-monhoc")]
        public async Task<ActionResult<List<LopAoDto>>> SelectBy_MaMonHoc([FromQuery] int maMonHoc)
        {
            return Ok(await _lopAoService.SelectBy_ma_mon_hoc(maMonHoc));
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
