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
    public class DotThiController(DotThiService dotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly DotThiService _dotThiService = dotThiService;
        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DotThiDto>>> GetAll()
        {
            return Ok(await _dotThiService.GetAll());
        }
        [HttpGet("SelectOne")]
        public async Task<ActionResult<DotThiDto>> SelectOne([FromQuery] int ma_dot_thi)
        {
            return Ok(await _dotThiService.SelectOne(ma_dot_thi));
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] DotThiDto dotThi)
        {
            var id = -1;
            if(dotThi.TenDotThi != null && dotThi.ThoiGianBatDau != null && dotThi.ThoiGianKetThuc != null && dotThi.NamHoc != null)
                id = await _dotThiService.Insert(dotThi.TenDotThi, (DateTime)dotThi.ThoiGianBatDau, (DateTime)dotThi.ThoiGianKetThuc, (int)dotThi.NamHoc);
            await NotifyChangeDotThiToAdmin(id, 0);
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] DotThiDto dotThi)
        {
            if (dotThi.TenDotThi != null && dotThi.ThoiGianBatDau != null && dotThi.ThoiGianKetThuc != null && dotThi.NamHoc != null)
                await _dotThiService.Update(dotThi.MaDotThi, dotThi.TenDotThi, (DateTime)dotThi.ThoiGianBatDau, (DateTime)dotThi.ThoiGianKetThuc, (int)dotThi.NamHoc);
            await NotifyChangeDotThiToAdmin(dotThi.MaDotThi, 1);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_dot_thi)
        {
            await _dotThiService.Remove(ma_dot_thi);
            await NotifyChangeDotThiToAdmin(ma_dot_thi, 2);
            return Ok();
        }



        private async Task NotifyChangeDotThiToAdmin(int ma_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertDotThi" : (function == 1) ? "UpdateDotThi" : "DeleteDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_dot_thi);
        }
    }
}
