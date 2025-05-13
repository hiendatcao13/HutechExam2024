using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauTraLoiController(CauTraLoiService cauTraLoiService) : Controller
    {
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;

        [HttpGet("SelectOne")]
        public async Task<ActionResult<CauTraLoiDto>> SelectOne([FromQuery] int ma_cau_tra_loi)
        {
            return Ok(await _cauTraLoiService.SelectBy_MaCauHoi(ma_cau_tra_loi));
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] CauTraLoiDto cauTraLoiDto)
        {
            int id = await _cauTraLoiService.Insert(cauTraLoiDto.MaCauHoi, cauTraLoiDto.ThuTu, cauTraLoiDto.NoiDung ?? "", cauTraLoiDto.LaDapAn, cauTraLoiDto.HoanVi);
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] CauTraLoiDto cauTraLoiDto)
        {
            await _cauTraLoiService.Update(cauTraLoiDto.MaCauTraLoi, cauTraLoiDto.MaCauHoi, cauTraLoiDto.ThuTu, cauTraLoiDto.NoiDung, cauTraLoiDto.LaDapAn, cauTraLoiDto.HoanVi);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_cau_hoi)
        {
            await _cauTraLoiService.Remove(ma_cau_hoi);
            return Ok();
        }
    }
}
