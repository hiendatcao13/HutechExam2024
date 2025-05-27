using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.CauHoi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cauhois")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauHoiController(CauHoiService cauHoiService) : Controller
    {
        private readonly CauHoiService _cauHoiService = cauHoiService;

        //////////////////CRUD///////////////////////////
        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] CauHoiCreateRequest cauHoi)
        {
            int result = await _cauHoiService.Insert(cauHoi.MaClo, cauHoi.MaNhom, cauHoi.TieuDe, cauHoi.KieuNoiDung, cauHoi.NoiDung, cauHoi.GhiChu, cauHoi.HoanVi);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CauHoiUpdateRequest cauHoi)
        {
            await _cauHoiService.Update(id, cauHoi.MaNhom, cauHoi.MaClo, cauHoi.TieuDe, cauHoi.KieuNoiDung, cauHoi.NoiDung, cauHoi.GhiChu, cauHoi.HoanVi);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            await _cauHoiService.Remove(id);
            return Ok();
        }

        //////////////////FITER///////////////////////////
        [HttpGet("filter-by-nhomcauhoi")]
        public async Task<ActionResult<List<CauHoiDto>>> SelectBy_MaNhom([FromQuery] int maNhomCauHoi)
        {
            return Ok(await _cauHoiService.SelectBy_MaNhom(maNhomCauHoi));
        }

        //////////////////OTHER///////////////////////////


        //////////////////PRIVATE///////////////////////////
    }
}
