using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using Hutech.Exam.Client.API;

namespace Hutech.Exam.Client.Pages.Admin.Login
{
    public partial class AdminLogin
    {
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }


        private UserSession? userSession;
        private string username = "";
        private string password = "";

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
        private async Task EnterAsync(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await OnClickLoginAsync();
            }
        }
        private async Task OnClickLoginAsync()
        {
            if(username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                Snackbar.Add(EMPTY_MESSAGE, Severity.Error);
                return;
            }
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);

            userSession = await LoginAPI( new Shared.DTO.Request.User.UserAuthenticationRequest { Username = username, Password = password});
            if (userSession == null) 
            {
                return;
            }

            var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);

            await SessionStorage.SetItemAsStringAsync("Name", userSession.Name);
            Nav.NavigateTo("/admin/home", true);
        }
    }
}

