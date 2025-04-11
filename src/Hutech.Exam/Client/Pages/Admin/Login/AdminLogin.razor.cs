using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;

namespace Hutech.Exam.Client.Pages.Admin.Login
{
    public partial class AdminLogin
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        private UserSession? userSession;
        private string username = "";
        private string password = "";

        private const string FAILED_MESSSAGE = "Không thể xác thực người dùng hoặc tài khoản bị tạm khóa!";
        private const string LOADING_MESSAGE = "Đang xác thực thông tin...";
        private const string EMPTY_MESSAGE = "Vui lòng điền đầy đủ tên đăng nhập và mật khẩu!";


        protected override async Task OnInitializedAsync()
        {
            //nếu đã tồn tại người dùng đăng nhập trước đó, xóa token đi, không cho phép chuyển trang giống sv vì vấn đề bảo mật
            var customAuthStateProvider = (AuthenticationStateProvider != null) ? (CustomAuthenticationStateProvider)AuthenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && Http != null && AuthenticationState != null)
            {
                if (AuthenticationStateProvider != null)
                {
                    var customAuthStateProvider1 = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                    await customAuthStateProvider1.UpdateAuthenticationState(null);
                    Nav.NavigateTo("/admin", true);
                }
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
            if(username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                Snackbar.Add(EMPTY_MESSAGE, Severity.Error);
                return;
            }
            userSession = await LoginAPI(username, password);
            if (userSession == null) 
            {
                Snackbar.Add(FAILED_MESSSAGE, Severity.Error);
                return;
            }

            var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);

            await SessionStorage.SetItemAsStringAsync("Name", userSession.Name);
            Nav.NavigateTo("/admin/control", true);
        }
    }
}

