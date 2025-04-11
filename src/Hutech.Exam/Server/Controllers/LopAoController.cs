using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
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
        [HttpGet("SelectOne")]
        public async Task<ActionResult<LopAoDto>> SelectOne([FromQuery] int ma_lop_ao)
        {
            return Ok(await _lopAoService.SelectOne(ma_lop_ao));
        }
        [HttpGet("SelectBy_MaMonHoc")]
        public async Task<ActionResult<List<LopAoDto>>> SelectBy_MaMonHoc([FromQuery] int ma_mon_hoc)
        {
            return Ok(await _lopAoService.SelectBy_ma_mon_hoc(ma_mon_hoc));
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] LopAoDto lopAo)
        {
            return Ok(await _lopAoService.Insert(lopAo.TenLopAo ?? "", lopAo.NgayBatDau ?? DateTime.Now, lopAo.MaMonHoc ?? -1));
        }
        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update([FromBody] LopAoDto lopAo)
        {
            return Ok(await _lopAoService.Update(lopAo.MaLopAo, lopAo.TenLopAo ?? "", lopAo.NgayBatDau ?? DateTime.Now, lopAo.MaMonHoc ?? -1));
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult<bool>> Remove([FromQuery] int ma_lop_ao)
        {
            return Ok(await _lopAoService.Remove(ma_lop_ao));
        }
    }
}
