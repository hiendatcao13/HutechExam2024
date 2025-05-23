﻿using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CauHoiController(CauHoiService cauHoiService, CloService cloService, CauTraLoiService cauTraLoiService) : Controller
    {
        private readonly CauHoiService _cauHoiService = cauHoiService;
        private readonly CloService _cloService = cloService;
        private readonly CauTraLoiService _cauTraLoiService = cauTraLoiService;

        //[HttpGet("SelectOne")]
        //public async Task<ActionResult<CauHoiDto>> SelectOne([FromQuery] int ma_cau_hoi)
        //{
        //    var result = await _cauHoiService.SelectOne(ma_cau_hoi);
        //    result.MaCloNavigation = await GetThongTinClo(result.MaClo);
        //    result.CauTraLois = await GetThongTinCauTraLois(result.MaCauHoi);
        //    return Ok(result);
        //}
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] CauHoiRequest cauHoi)
        {
            int result = await _cauHoiService.Insert(cauHoi.MaClo, cauHoi.MaNhom, cauHoi.TieuDe ?? "", cauHoi.KieuNoiDung, cauHoi.NoiDung ?? "", cauHoi.GhiChu ?? "", cauHoi.HoanVi ?? false);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] CauHoiRequest cauHoi)
        {
            await _cauHoiService.Update(cauHoi.MaCauHoi, cauHoi.MaNhom, cauHoi.MaClo, cauHoi.TieuDe ?? "", cauHoi.KieuNoiDung, cauHoi.NoiDung ?? "", cauHoi.GhiChu ?? "", cauHoi.HoanVi ?? false);
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
            return Ok(await _cauHoiService.SelectBy_MaNhom(ma_nhom));
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
