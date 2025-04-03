using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Hubs
{
    public class AdminHub : Hub
    {
        // đóng băng ca thi
        public async Task FreezeCaThiToGroup(string ma_ca_thi)
        {
            await Clients.Group(ma_ca_thi).SendAsync("FreezeCaThi");
        }
        // reset đăng nhập cho thí sinh
        public async Task ResetLogin(string ma_sinh_vien)
        {
            await Clients.User(ma_sinh_vien).SendAsync("ResetLogin");
        }
    }
}
