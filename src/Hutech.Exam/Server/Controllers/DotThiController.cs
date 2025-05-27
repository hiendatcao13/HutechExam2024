using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.DotThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/dotthis")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DotThiController(DotThiService dotThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly DotThiService _dotThiService = dotThiService;

        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        //////////////////CRUD///////////////////////////

        [HttpGet]
        public async Task<ActionResult<List<DotThiDto>>> GetAll()
        {
            return Ok(await _dotThiService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DotThiDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _dotThiService.SelectOne(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] DotThiCreateRequest dotThi)
        {
            var id = await _dotThiService.Insert(dotThi.TenDotThi, dotThi.ThoiGianBatDau, dotThi.ThoiGianKetThuc, dotThi.NamHoc);
            await NotifyChangeDotThiToAdmin(id, 0);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] DotThiUpdateRequest dotThi)
        {
            await _dotThiService.Update(id, dotThi.TenDotThi, dotThi.ThoiGianBatDau, dotThi.ThoiGianKetThuc, dotThi.NamHoc);
            await NotifyChangeDotThiToAdmin(id, 1);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            await _dotThiService.Remove(id);
            await NotifyChangeDotThiToAdmin(id, 2);
            return Ok();
        }

        //////////////////FILTER///////////////////////////

        //////////////////OTHERS///////////////////////////

        //////////////////PRIVATE///////////////////////////

        private async Task NotifyChangeDotThiToAdmin(int ma_dot_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertDotThi" : (function == 1) ? "UpdateDotThi" : "DeleteDotThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_dot_thi);
        }
    }
}
