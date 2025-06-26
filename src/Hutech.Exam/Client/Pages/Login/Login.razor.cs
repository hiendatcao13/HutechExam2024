using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.DTO.Request.SinhVien;
namespace Hutech.Exam.Client.Pages.Login
{

    public partial class Login
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        // biến binding với UI
        private string? Username { get; set; } = string.Empty;
        private string? Password { get; set; } = string.Empty;

        // biến nội bộ
        private UserSession? _userSession;

        private const string FAILED_MESSSAGE = "Không thể xác thực người dùng hoặc tài khoản đang được người khác sử dụng.Vui lòng kiểm tra lại và báo cho người giám sát nếu cần thiết";
        private const string ERROR_MESSAGE = "Username và password không trùng khớp";
        private const string LOADING_MESSAGE = "Đang xác thực thông tin...";

        #endregion

        #region Methods

        protected override async Task OnInitializedAsync()
        {
            //nếu đã tồn tại người dùng đăng nhập trước đó, chuyển trang
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if (!string.IsNullOrWhiteSpace(token) && AuthenticationState != null)
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var authState = await AuthenticationState;
                Username = authState?.User.Identity?.Name;
                Nav?.NavigateTo("/info", true);
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
            if (string.IsNullOrEmpty(Username) || Username != Password)
            {
                Snackbar.Add(ERROR_MESSAGE, Severity.Error);
                return;
            }
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);

            _userSession = await LoginAPI(new SinhVienAuthenticationRequest { Username = Username, Password = Password});

            if (_userSession == null)
            {
                return;
            }

            var customAuthenticationStateProvider = AuthenticationStateProvider as CustomAuthenticationStateProvider;
            if (customAuthenticationStateProvider == null)
            {
                Snackbar.Add(FAILED_MESSSAGE, Severity.Error);
                return;
            }

            // Cập nhật trạng thái xác thực
            await customAuthenticationStateProvider.UpdateAuthenticationState(_userSession);

            Nav.NavigateTo("/info", true);
        }

        #endregion
    }
}
