using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CloController : Controller
    {
        private readonly CloService _cloService;
        public CloController(CloService cloService)
        {
            _cloService = cloService;
        }

        [HttpGet("SelectOne")]
        public async Task<ActionResult<CloDto>> SelectOne([FromQuery] int ma_clo)
        {
            return Ok(await _cloService.SelectOne(ma_clo));
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] CloDto cloDto)
        {
            return Ok(await _cloService.Insert(cloDto.MaMonHoc, cloDto.MaSoClo, cloDto.TieuDe ?? "", cloDto.NoiDung ?? "", cloDto.TieuChi, cloDto.SoCau));
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] CloDto cloDto)
        {
            await _cloService.Update(cloDto.MaClo, cloDto.MaMonHoc, cloDto.MaSoClo, cloDto.TieuDe ?? "", cloDto.NoiDung ?? "", cloDto.TieuChi, cloDto.SoCau);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult<int>> Remove([FromQuery] int ma_clo)
        {
            await _cloService.Remove(ma_clo);
            return Ok();
        }
        [HttpGet("SelectBy_MaMonHoc")]
        public async Task<ActionResult<List<CloDto>>> SelectBy_MaMonHoc([FromQuery] int ma_mon_hoc)
        {
            return Ok(await _cloService.SelectBy_MaMonHoc(ma_mon_hoc));
        }
    }
}
