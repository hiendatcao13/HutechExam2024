using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components.Web;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Http.Connections;
namespace Hutech.Exam.Client.Pages.Login
{

    public partial class Login
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private ApplicationDataService MyData { get; set; } = default!;

        private UserSession? userSession;
        private string? username = "";
        private string? password = "";

        private const string FAILED_MESSSAGE = "Không thể xác thực người dùng hoặc tài khoản đang được người khác sử dụng.Vui lòng kiểm tra lại và báo cho người giám sát nếu cần thiết";
        private const string ERROR_MESSAGE = "Username và password không trùng khớp";
        private const string LOADING_MESSAGE = "Đang xác thực thông tin...";

        protected override async Task OnInitializedAsync()
        {
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

            Nav.NavigateTo("/info", true);
        }
    }
}
