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
    public class DotThiController : Controller
    {
        private readonly DotThiService _dotThiService;
        private readonly IHubContext<MainHub> _mainHub;

        public DotThiController(DotThiService dotThiService, IHubContext<MainHub> mainHub) 
        {
            _dotThiService = dotThiService;
            _mainHub = mainHub;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DotThiDto>>> GetAll()
        {
            return Ok(await _dotThiService.GetAll());
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] DotThiDto dotThi)
        {
            var id = -1;
            if(dotThi.TenDotThi != null && dotThi.ThoiGianBatDau != null && dotThi.ThoiGianKetThuc != null && dotThi.NamHoc != null)
                id = await _dotThiService.Insert(dotThi.TenDotThi, (DateTime)dotThi.ThoiGianBatDau, (DateTime)dotThi.ThoiGianKetThuc, (int)dotThi.NamHoc);
            await NotifyChangeDotThiToAdmin();
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] DotThiDto dotThi)
        {
            if (dotThi.TenDotThi != null && dotThi.ThoiGianBatDau != null && dotThi.ThoiGianKetThuc != null && dotThi.NamHoc != null)
                await _dotThiService.Update(dotThi.MaDotThi, dotThi.TenDotThi, (DateTime)dotThi.ThoiGianBatDau, (DateTime)dotThi.ThoiGianKetThuc, (int)dotThi.NamHoc);
            await NotifyChangeDotThiToAdmin();
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_dot_thi)
        {
            await _dotThiService.Remove(ma_dot_thi);
            await NotifyDeleteDotThiToAdmin();
            return Ok();
        }



        private async Task NotifyChangeDotThiToAdmin()
        {
            await _mainHub.Clients.Group("admin").SendAsync("ChangeDotThi");
        }
        private async Task NotifyDeleteDotThiToAdmin()
        {
            await _mainHub.Clients.Group("admin").SendAsync("DeleteDotThi");
        }
    }
}
