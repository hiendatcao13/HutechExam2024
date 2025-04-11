using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ChiTietDotThiController : Controller
    {
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly IHubContext<MainHub> _mainHub;
        public ChiTietDotThiController(ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService, 
            MonHocService monHocService, IHubContext<MainHub> mainHub)
        {
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _mainHub = mainHub;
        }
        [HttpGet("LanThis_SelectBy_MaDotThiMaLopAo")]
        public async Task<ActionResult<List<string>>> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int ma_dot_thi, [FromQuery] int ma_lop_ao)
        {
            List<string> result = [];
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
            foreach(var item in result)
            {
                item.MaLopAoNavigation = await GetLopAo(item.MaLopAo);
                item.MaLopAoNavigation.MaMonHocNavigation = await GetMonHoc(item.MaLopAoNavigation.MaMonHoc ?? -1);
            }
            return Ok(result);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] ChiTietDotThiDto chiTietDotThi)
        {
            var id = await _chiTietDotThiService.Insert(chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
            await NotifyChangeChiTietDotThiToAdmin();
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] ChiTietDotThiDto chiTietDotThi)
        {
            await _chiTietDotThiService.Update(chiTietDotThi.MaChiTietDotThi, chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
            await NotifyChangeChiTietDotThiToAdmin();
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_chi_tiet_dot_thi)
        {
            await _chiTietDotThiService.Remove(ma_chi_tiet_dot_thi);
            await NotifyDeleteChiTietDotThiToAdmin();
            return Ok();
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


        private async Task NotifyChangeChiTietDotThiToAdmin()
        {
            await _mainHub.Clients.Group("admin").SendAsync("ChangeChiTietDotThi");
        }
        private async Task NotifyDeleteChiTietDotThiToAdmin()
        {
            await _mainHub.Clients.Group("admin").SendAsync("DeleteChiTietDotThi");
        }
    }
}
