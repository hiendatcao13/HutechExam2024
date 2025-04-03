using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Hubs
{
    public class CaThiHub : Hub
    {
        // hàm yêu cầu load lại dữ liệu khi các admin làm việc với nhau
        public async Task SendMessage(int ca_thi)
        {
            await Clients.All.SendAsync("ReloadingComponent", ca_thi);
        }
    }
}
