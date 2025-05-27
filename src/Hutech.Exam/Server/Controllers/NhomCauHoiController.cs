using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.NhomCauHoi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/nhomcauhois")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class NhomCauHoiController(NhomCauHoiService nhomCauHoiService) : Controller
    {
        private readonly NhomCauHoiService _nhomCauHoiService = nhomCauHoiService;

        //////////////////CRUD///////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<NhomCauHoiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _nhomCauHoiService.SelectOne(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] NhomCauHoiCreateRequest nhomCauHoi)
        {
            var id = await _nhomCauHoiService.Insert(nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha, nhomCauHoi.SoCauLay, nhomCauHoi.LaCauHoiNhom);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] NhomCauHoiUpdateRequest nhomCauHoi)
        {
            await _nhomCauHoiService.Update(id, nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _nhomCauHoiService.Remove(id);
            return Ok();
        }

        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-dethi")]
        public async Task<IActionResult> SelectAllBy_MaDeThi([FromQuery] int maDeThi)
        {
            var result = await _nhomCauHoiService.SelectAllBy_MaDeThi(maDeThi);
            return Ok(result);
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////


    }
}
