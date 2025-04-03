using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Hubs
{
    public class SinhVienHub : Hub
    {
        public async Task JoinGroupCaThi(string ma_sinh_vien, string ma_ca_thi)
        {
            await Groups.AddToGroupAsync(ma_sinh_vien, ma_ca_thi);
        }
        public async Task LeaveGroupCaThi(string ma_sinh_vien, string ma_ca_thi)
        {
            await Groups.RemoveFromGroupAsync(ma_sinh_vien, ma_ca_thi);
        }
        
    }
}
