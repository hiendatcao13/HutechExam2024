using Hutech.Exam.Client.Authentication;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class Exam
    {
        private async Task CreateHubConnection()
        {
            hubConnection = new HubConnectionBuilder()
                    .WithUrl(Nav.ToAbsoluteUri("/MainHub"), options =>
                    {
                        options.Transports = HttpTransportType.WebSockets; // Ưu tiên WebSockets nếu có thể
                    })
                    .WithAutomaticReconnect() // Tự động kết nối lại nếu mất mạng
                    .Build();

            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(5000); // Chờ 5s trước khi thử kết nối lại
                await CreateHubConnection(); // Thử kết nối lại
            };

            // trường hợp thay đổi tình trạng ca thi
            hubConnection.On<int>("ChangeStatusCaThi", async (ma_ca_thi) =>
            {
                if (chiTietCaThi != null && ma_ca_thi == chiTietCaThi.MaCaThi)
                {
                    await CallLoadData();
                }
            });
            hubConnection.On<long>("ResetLogin", async (ma_sinh_vien) =>
            {
                if(sinhVien != null && sinhVien.MaSinhVien == ma_sinh_vien)
                await HandleDangXuat();
            });
            await hubConnection.StartAsync();

            //tham gia vào group lớp
            await hubConnection.InvokeAsync("JoinGroupLop", sinhVien?.MaLop ?? -1);
        }
        private bool IsConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task HandleDangXuat()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }
        }
        private async Task SendMessage(int ma_ca_thi)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMCT", ma_ca_thi);
        }
        private async Task CallLoadData()
        {
            await UpdateChiTietBaiThiAPI();
            bool result = await IsActiveCaThiAPI(chiTietCaThi?.MaCaThi ?? -1);
            if (!result)
            {
                Snackbar.Add(DONG_BANG_CA_THI, MudBlazor.Severity.Warning);
                Nav?.NavigateTo("/info");
            }
        }
    }
}
