using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class NhomCauHoiController : Controller
    {
        private readonly NhomCauHoiService _nhomCauHoiService;
        public NhomCauHoiController(NhomCauHoiService nhomCauHoiService)
        {
            _nhomCauHoiService = nhomCauHoiService;
        }
        [HttpGet("SelectOne")]
        public async Task<ActionResult<NhomCauHoiDto>> SelectOne([FromQuery] int ma_nhom)
        {
            var result = await _nhomCauHoiService.SelectOne(ma_nhom);
            return Ok(result);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] NhomCauHoiDto nhomCauHoi)
        {
            var id = await _nhomCauHoiService.Insert(nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha, nhomCauHoi.SoCauLay, nhomCauHoi.LaCauHoiNhom ?? false);
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] NhomCauHoiDto nhomCauHoi)
        {
            await _nhomCauHoiService.Update(nhomCauHoi.MaNhom, nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_nhom)
        {
            await _nhomCauHoiService.Remove(ma_nhom);
            return Ok();
        }
        [HttpGet("SelectAllBy_MaDeThi")]
        public async Task<IActionResult> SelectAllBy_MaDeThi([FromQuery]int ma_de_thi)
        {
            var result = await _nhomCauHoiService.SelectAllBy_MaDeThi(ma_de_thi);
            return Ok(result);
        }
    }
}
