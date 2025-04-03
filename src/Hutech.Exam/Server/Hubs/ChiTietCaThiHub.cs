using Microsoft.AspNetCore.SignalR;
namespace Hutech.Exam.Server.Hubs
{
    public class ChiTietCaThiHub : Hub
    {
        // hàm yêu cầu load lại dữ liệu khi các admin làm việc với nhau
        public async Task SendMessage(int chi_tiet_ca_thi)
        {
            await Clients.All.SendAsync("ReloadingComponent", chi_tiet_ca_thi);
        }
        // value ma_sinh_vien để cập nhật thông tin sinh viên đã đăng nhập hay chưa đăng nhập
        public async Task SendMessageMSV(long ma_sinh_vien)
        {
            await Clients.All.SendAsync("SVAuthentication", ma_sinh_vien);
        }
        // hàm kiểm soát khi người giám sát reset tài khoản trên các máy đang đang nhập 
        public async Task SendMessageResetLogin(long ma_sinh_vien)
        {
            await Clients.All.SendAsync("ResetLogin", ma_sinh_vien);
        }
        // value ma_chi_tiet_ca_thi để cập nhật thông tin của bất kì 1 thí sinh nào đang vào thi hãy đã thi hoàn tất
        public async Task SendMessageMCT(int ma_ca_thi)
        {
            await Clients.All.SendAsync("ReceiveMessageMCT", ma_ca_thi);
        }
        // trường hợp dừng ca thi, thông báo cho thí sinh biết hoặc load lại tình trạng ca thi khi có sự thay đổi tình trạng ca thi
        public async Task SendMessageStatusCaThi(int ma_ca_thi)
        {
            await Clients.All.SendAsync("ReceiveMessageStatusCaThi", ma_ca_thi);
        }

    }
}
