using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.Request;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace Hutech.Exam.Server.Hubs
{
    public class AdminHub : Hub
    {
        // hub cho admin tham gia và rời tham gia nhóm "admin"
        // group này chỉ hoạt động ở trang monitor - giám sát ca thi cần real-time
        public async Task JoinGroupAdminMonitor()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "admin");
        }
        public async Task LeaveGroupAdminMonitor()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "admin");
        }


        // các thông điệp được minh họa tại đây vì nó được gọi trực tiếp

        // admin gửi thông điệp đóng băng ca thi, kích, hủy kích và kết thúc ca thi cho cả SV và Admin
        public async Task ChangeStatusCaThi(int ma_ca_thi)
        {
            await Clients.Group("student").SendAsync("ChangeStatusCaThi");
        }
        public async Task ChangeStatusCaThiAdmin()
        {
            await Clients.Group("admin").SendAsync("ChangeStatusCaThi");
        }

        // admin gửi thông điệp đăng xuất thiết bị
        public async Task ResetLogin(int ma_lop, long ma_sinh_vien)
        {
            await Clients.Group(ma_lop + "").SendAsync("ResetLogin", ma_sinh_vien);
        }

        // thí sinh gửi thông điệp cập nhật trạng thái đăng nhập / đăng xuất cho admin
        public async Task SV_Authentication(long ma_sinh_vien)
        {
            await Clients.Group("admin").SendAsync("SV_Authentication", ma_sinh_vien);
        }

        // thí sinh gửi thông điệp cập nhật trạng thái thi cho admin
        public async Task SV_Status(int ma_chi_tiet_ca_thi)
        {
            await Clients.Group("admin").SendAsync("SV_Status", ma_chi_tiet_ca_thi);
        }
    }
}
