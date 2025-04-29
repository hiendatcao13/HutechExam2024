using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauHoiController : Controller
    {
        private readonly CauHoiService _cauHoiService;
        private readonly CloService _cloService;
        private readonly CauTraLoiService _cauTraLoiService;
        public CauHoiController(CauHoiService cauHoiService, CloService cloService, CauTraLoiService cauTraLoiService)
        {
            _cauHoiService = cauHoiService;
            _cloService = cloService;
            _cauTraLoiService = cauTraLoiService;
        }
        [HttpGet("SelectOne")]
        public async Task<ActionResult<CauHoiDto>> SelectOne([FromQuery] int ma_cau_hoi)
        {
            var result = await _cauHoiService.SelectOne(ma_cau_hoi);
            result.MaCloNavigation = await GetThongTinClo(result.MaClo);
            result.CauTraLois = await GetThongTinCauTraLois(result.MaCauHoi);
            return Ok(result);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] CauHoiDto cauHoiDto)
        {
            int result = await _cauHoiService.Insert(cauHoiDto.MaClo, cauHoiDto.MaNhom, cauHoiDto.TieuDe ?? "", cauHoiDto.KieuNoiDung, cauHoiDto.NoiDung ?? "", cauHoiDto.GhiChu ?? "", cauHoiDto.HoanVi ?? false);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] CauHoiDto cauHoiDto)
        {
            await _cauHoiService.Update(cauHoiDto.MaCauHoi, cauHoiDto.MaNhom, cauHoiDto.MaClo, cauHoiDto.TieuDe ?? "", cauHoiDto.KieuNoiDung, cauHoiDto.NoiDung ?? "", cauHoiDto.GhiChu ?? "", cauHoiDto.HoanVi ?? false);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult<int>> Remove([FromQuery] int ma_cau_hoi)
        {
            await _cauHoiService.Remove(ma_cau_hoi);
            return Ok();
        }
        [HttpGet("SelectBy_MaNhom")]
        public async Task<ActionResult<List<CauHoiDto>>> SelectBy_MaNhom([FromQuery] int ma_nhom)
        {
            var result = await _cauHoiService.SelectBy_MaNhom(ma_nhom);
            foreach (var item in result)
            {
                item.MaCloNavigation = await GetThongTinClo(item.MaClo);
                item.CauTraLois = await GetThongTinCauTraLois(item.MaCauHoi);
            }
            return Ok(result);
        }




        private async Task<CloDto> GetThongTinClo(int ma_clo)
        {
            return await _cloService.SelectOne(ma_clo);
        }
        private async Task<List<CauTraLoiDto>> GetThongTinCauTraLois(int ma_cau_hoi)
        {
            return await _cauTraLoiService.SelectBy_MaCauHoi(ma_cau_hoi);
        }
    }
}
