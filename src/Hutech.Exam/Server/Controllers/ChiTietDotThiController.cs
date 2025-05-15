using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ChiTietDotThiController(ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService,
        MonHocService monHocService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly ChiTietDotThiService _chiTietDotThiService = chiTietDotThiService;
        private readonly LopAoService _lopAoService = lopAoService;
        private readonly MonHocService _monHocService = monHocService;
        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        [HttpGet("SelectOne")]
        public async Task<ActionResult<ChiTietDotThiDto>> SelectOne([FromQuery] int ma_chi_tiet_dot_thi)
        {
            var chiTietDotThi = await _chiTietDotThiService.SelectOne(ma_chi_tiet_dot_thi);
            chiTietDotThi.MaLopAoNavigation = await GetLopAo(chiTietDotThi.MaLopAo);
            chiTietDotThi.MaLopAoNavigation.MaMonHocNavigation = await GetMonHoc(chiTietDotThi.MaLopAoNavigation.MaMonHoc ?? -1);
            return Ok(chiTietDotThi);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] ChiTietDotThiDto chiTietDotThi)
        {
            var id = await _chiTietDotThiService.Insert(chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
            await NotifyChangeChiTietDotThiToAdmin(id, 0);
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] ChiTietDotThiDto chiTietDotThi)
        {
            await _chiTietDotThiService.Update(chiTietDotThi.MaChiTietDotThi, chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
            await NotifyChangeChiTietDotThiToAdmin(chiTietDotThi.MaChiTietDotThi, 1);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_chi_tiet_dot_thi)
        {
            await _chiTietDotThiService.Remove(ma_chi_tiet_dot_thi);
            await NotifyChangeChiTietDotThiToAdmin(ma_chi_tiet_dot_thi, 2);
            return Ok();
        }



        [HttpGet("LanThis_SelectBy_MaDotThiMaLopAo")]
        public async Task<ActionResult<List<string>>> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int ma_dot_thi, [FromQuery] int ma_lop_ao)
        {
            List<int> result = [];
            var chiTietDotThis = await SelectBy_MaDotThiMaLopAo(ma_dot_thi, ma_lop_ao);
            foreach (var item in chiTietDotThis)
            {
                result.Add(item.LanThi);
            }
            return Ok(result);
        }
        [HttpGet("SelectBy_MaDotThi")]
        public async Task<ActionResult<List<string>>> SelectBy_MaDotThi([FromQuery] int ma_dot_thi)
        {
            var result = await _chiTietDotThiService.SelectBy_MaDotThi(ma_dot_thi);
            foreach (var item in result)
            {
                item.MaLopAoNavigation = await GetLopAo(item.MaLopAo);
                item.MaLopAoNavigation.MaMonHocNavigation = await GetMonHoc(item.MaLopAoNavigation.MaMonHoc ?? -1);
            }
            return Ok(result);
        }



        private async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThiMaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            return await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao);
        }
        private async Task<LopAoDto> GetLopAo(int ma_lop_ao)
        {
            return await _lopAoService.SelectOne(ma_lop_ao);
        }
        private async Task<MonHocDto> GetMonHoc(int ma_mon_hoc)
        {
            return await _monHocService.SelectOne(ma_mon_hoc);
        }


        private async Task NotifyChangeChiTietDotThiToAdmin(int ma_chi_tiet_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCTDotThi" : (function == 1) ? "UpdateCTDotThi" : "DeleteCTDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_chi_tiet_dot_thi);
        }
    }
}
