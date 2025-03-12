using Hutech.Exam.Client.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class Exam
    {
        private async Task InitialConnectionHub()
        {
            if (Nav != null && MyData != null)
            {
                // mở để cập nhật trạng thái thi của sinh viên
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(Nav.ToAbsoluteUri("/ChiTietCaThiHub"))
                .Build();
                // trường hợp dừng ca thi
                hubConnection.On<int>("ReceiveMessageStatusCaThi", (ma_ca_thi) =>
                {
                    if(chiTietCaThi != null && ma_ca_thi == chiTietCaThi.MaCaThi)
                    {
                        CallLoadData();
                        StateHasChanged();
                    }
                });
                hubConnection.On<long>("ReceiveMessageResetLogin", (ma_so_sv) =>
                {
                    if (sinhVien != null && ma_so_sv == sinhVien.MaSinhVien)
                        resetLogin();
                });
                await hubConnection.StartAsync();
            }
        }
        private bool IsConnectHub() => hubConnection?.State == HubConnectionState.Connected;


        private async Task SendMessage(int ma_ca_thi)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMCT", ma_ca_thi);
        }
        private async Task SendMessage(long ma_sinh_vien)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMSV", ma_sinh_vien);
        }
        private void CallLoadData()
        {
            Task.Run(async () =>
            {
                await UpdateChiTietBaiThiAPI();
                bool result = await IsActiveCaThiAPI(chiTietCaThi?.MaCaThi ?? -1);
                if (!result)
                {
                    Snackbar.Add(DONG_BANG_CA_THI, MudBlazor.Severity.Warning);
                    Nav?.NavigateTo("/info");
                }
            });
        }
        private void resetLogin()
        {
            Task.Run(async () =>
            {
                if (AuthenticationStateProvider != null)
                {
                    var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                    await customAuthStateProvider.UpdateAuthenticationState(null);
                    Nav?.NavigateTo("/", true);
                }
            });
        }
    }
}
