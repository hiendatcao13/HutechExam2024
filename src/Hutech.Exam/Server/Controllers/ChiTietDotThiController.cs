using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietDotThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietdotthis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ChiTietDotThiController(ChiTietDotThiService chiTietDotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly ChiTietDotThiService _chiTietDotThiService = chiTietDotThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;


        //////////////////CRUD///////////////////////////

        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDotThiDto>> SelectOne([FromRoute] int id)
        {
            return Ok( await _chiTietDotThiService.SelectOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] ChiTietDotThiCreateRequest chiTietDotThi)
        {
            var id = await _chiTietDotThiService.Insert(chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
            await NotifyChangeChiTietDotThiToAdmin(id, 0);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ChiTietDotThiUpdateRequest chiTietDotThi)
        {
            await _chiTietDotThiService.Update(id, chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi);
            await NotifyChangeChiTietDotThiToAdmin(id, 1);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _chiTietDotThiService.Remove(id);
            await NotifyChangeChiTietDotThiToAdmin(id, 2);
            return Ok();
        }

        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-dotthi")]
        public async Task<ActionResult<List<string>>> SelectBy_MaDotThi([FromQuery] int maDotThi)
        {
            return Ok( await _chiTietDotThiService.SelectBy_MaDotThi(maDotThi));
        }

        [HttpGet("filter-by-dotthi-lopao")]
        public async Task<ActionResult<List<ChiTietDotThiDto>>> LanThis_SelectBy_MaDotThiMaLopAo([FromQuery] int maDotThi, [FromQuery] int maLopAo)
        {
            return Ok(await SelectBy_MaDotThiMaLopAo(maDotThi, maLopAo));
        }

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////

        private async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThiMaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            return await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao);
        }


        private async Task NotifyChangeChiTietDotThiToAdmin(int ma_chi_tiet_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCTDotThi" : (function == 1) ? "UpdateCTDotThi" : "DeleteCTDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_chi_tiet_dot_thi);
        }
    }
}
