using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components.Web;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
namespace Hutech.Exam.Client.Pages.Login
{

    public partial class Login
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        private SinhVienDto? student;
        private UserSession? userSession;
        private HubConnection? hubConnection;
        private string? username = "";
        private string? password = "";

        private const string FAILED_MESSSAGE = "Không thể xác thực người dùng hoặc tài khoản đang được người khác sử dụng.Vui lòng kiểm tra lại và báo cho người giám sát nếu cần thiết";
        private const string ERROR_MESSAGE = "Username và password không trùng khớp";
        private const string LOADING_MESSAGE = "Đang xác thực thông tin...";

        protected override async Task OnInitializedAsync()
        {
            await Start();
            student = new();
            //nếu đã tồn tại người dùng đăng nhập trước đó, chuyển trang
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if (!string.IsNullOrWhiteSpace(token) && AuthenticationState != null)
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var authState = await AuthenticationState;
                username = authState?.User.Identity?.Name;
                Nav?.NavigateTo("/info", true);
            }
            await base.OnInitializedAsync();
        }
        private async Task Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await OnClickDangNhap();
            }
        }
        private async Task OnClickDangNhap()
        {
            if (string.IsNullOrEmpty(username) || username != password)
            {
                Snackbar.Add(ERROR_MESSAGE, Severity.Error);
                return;
            }

            userSession = await LoginAPI(username);

            if (userSession == null)
            {
                Snackbar.Add(FAILED_MESSSAGE, Severity.Error);
                return;
            }

            var customAuthenticationStateProvider = AuthenticationStateProvider as CustomAuthenticationStateProvider;
            if (customAuthenticationStateProvider == null)
            {
                Snackbar.Add(FAILED_MESSSAGE, Severity.Error);
                return;
            }

            // Cập nhật trạng thái xác thực
            await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);

            student = userSession.NavigateSinhVien;

            // Thông báo cho quản trị viên nếu có kết nối
            if (IsConnectHub() && student != null)
            {
                await SendMessage(student.MaSinhVien);
            }

            Nav.NavigateTo("/info", true);
        }

        private async Task Start()
        {
            if (Nav != null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(Nav.ToAbsoluteUri("/ChiTietCaThiHub"))
                    .Build();

                await hubConnection.StartAsync();
            }
        }
        private bool IsConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task SendMessage(long ma_sinh_vien)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMSV", ma_sinh_vien);
        }

        public void Dispose()
        {
            hubConnection?.DisposeAsync();
        }
    }
}
