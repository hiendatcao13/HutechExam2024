using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cathis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaThiController(CaThiService caThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly CaThiService _caThiService = caThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        //////////////////CRUD///////////////////////////
        [HttpGet("{id}")]
        public async Task<ActionResult<CaThiDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _caThiService.SelectOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] CaThiCreateRequest caThi)
        {
            var id = await _caThiService.Insert(caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi);
            await NotifyChangeCaThiToAdmin(id, 0);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id,[FromBody] CaThiUpdateRequest caThi)
        {
            await _caThiService.Update(id, caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _caThiService.Remove(id);
            await NotifyChangeCaThiToAdmin(id, 2);
            return Ok();
        }

        //////////////////FILTER///////////////////////////
        [HttpGet("filter-by-dotthi-lopao-lanthi")]
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_MaDotThi_MaLopAo_LanThi([FromQuery] int maDotThi, [FromQuery] int maLopAo, [FromQuery] int lanThi)
        {
            return Ok(await _caThiService.SelectBy_MaDotThi_MaLop_LanThi(maDotThi, maLopAo, lanThi));

        }

        [HttpGet("filter-by-chitietdotthi")]
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_ma_chi_tiet_dot_thi([FromQuery] int maChiTietDotThi)
        {
            return Ok(await _caThiService.SelectBy_ma_chi_tiet_dot_thi(maChiTietDotThi));
        }

        //////////////////OTHERS///////////////////////////
        [HttpGet("{id}/is-active")]
        [Authorize]
        public async Task<ActionResult<bool>> IsActiveCaThi([FromRoute] int id)
        {
            CaThiDto caThi = await _caThiService.SelectOne(id);
            return (caThi.IsActivated);
        }


        [HttpPut("{id}/active")]
        public async Task<ActionResult> KichHoatCaThi([FromRoute] int id)
        {
            await _caThiService.Activate(id, true);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok();
        }

        [HttpPut("{id}/deactive")]
        public async Task<ActionResult> HuyKichHoatCaThi([FromRoute] int id)
        {
            await _caThiService.HuyKichHoat(id);
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok();
        }

        [HttpPut("{id}/pause")]
        public async Task<ActionResult> DungCaThi([FromRoute] int id)
        {
            await _caThiService.Activate(id, false);
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok();
        }

        [HttpPut("{id}/finish")]
        public async Task<ActionResult> KetThucCaThi([FromRoute] int id)
        {
            await _caThiService.Ketthuc(id);
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok();
        }


        //////////////////PRIVATE///////////////////////////
        private async Task NotifyChangeStatusCaThiToSV(int ma_ca_thi)
        {
            await _mainHub.Clients.Group(ma_ca_thi + "").SendAsync("ChangeStatusCaThi");
        }

        // các admin khác cũng nhận được sự thay đổi của TT ca thi
        private async Task NotifyChangeCaThiToAdmin(int ma_ca_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCaThi" : (function == 1) ? "UpdateCaThi" : "DeleteCaThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_ca_thi);
        }
    }
}
