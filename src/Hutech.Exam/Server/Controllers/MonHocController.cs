using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MonHocController(MonHocService monHocService) : Controller
    {
        private readonly MonHocService _monHocService = monHocService;

        [HttpGet("SelectOne")]
        public async Task<ActionResult<MonHocDto>> SelectOne([FromQuery] int ma_mon_hoc)
        {
            return Ok(await _monHocService.SelectOne(ma_mon_hoc));
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<MonHocDto>>> GetAll()
        {
            return Ok(await _monHocService.GetAll());
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] MonHocDto monHoc)
        {
            var result = await _monHocService.Insert(monHoc.MaSoMonHoc ?? "", monHoc.TenMonHoc ?? "");
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update([FromBody] MonHocDto monHoc)
        {
            var result = await _monHocService.Update(monHoc.MaMonHoc, monHoc.MaSoMonHoc ?? "", monHoc.TenMonHoc ?? "");
            return Ok(result);
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult<bool>> Remove([FromQuery] int ma_mon_hoc)
        {
            var result = await _monHocService.Remove(ma_mon_hoc);
            return Ok(result);
        }
    }
}
