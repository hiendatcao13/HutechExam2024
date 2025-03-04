using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Admin
{
    public partial class AdminLogin
    {
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        IJSRuntime? js { get; set; }
        [Inject]
        Blazored.SessionStorage.ISessionStorageService? sessionStorage { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        UserDto? user { get; set; }
        UserSession? userSession { get; set; }
        private string? username = "";
        private string? password = "";
        private string FAILED_MESSSAGE = "Không thể xác thực người dùng";
        private const string SUCCESS_MESSAGE = "Đăng nhập thành công!";
        protected override async Task OnInitializedAsync()
        {
            user = new();
            //nếu đã tồn tại người dùng đăng nhập trước đó, xóa token đi, không cho phép chuyển trang giống sv vì vấn đề bảo mật
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null && authenticationState != null)
            {
                if (authenticationStateProvider != null)
                {
                    var customAuthStateProvider1 = (CustomAuthenticationStateProvider)authenticationStateProvider;
                    await customAuthStateProvider1.UpdateAuthenticationState(null);
                    navManager?.NavigateTo("/admin", true);
                }
            }
            await base.OnInitializedAsync();
        }
        private async Task enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await onClickDangNhap();
            }
        }
        private async Task onClickDangNhap()
        {
            if (httpClient != null && username != "" && password != "")
            {

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var loginResponse = await httpClient.PostAsync($"api/Admin/Verify?loginName={username}&password={password}", null);

                // Kiểm tra xem yêu cầu có thành công không
                if (loginResponse.IsSuccessStatusCode && authenticationStateProvider != null && navManager != null && sessionStorage != null)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await loginResponse.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị mình muốn
                    userSession = JsonSerializer.Deserialize<UserSession>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                    // Cập nhật trạng thái của UserSession lại, chuyển từ Anyousmous thành người có danh tính
                    await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);
                    // lưu vào cache
                    if(sessionStorage != null && userSession!= null && userSession.NavigateUser != null)
                        await sessionStorage.SetItemAsync<UserDto>("user", userSession.NavigateUser);
                    Snackbar.Add(SUCCESS_MESSAGE, MudBlazor.Severity.Success);
                    navManager.NavigateTo("/control", true);
                }
                else if ((loginResponse.StatusCode == HttpStatusCode.Unauthorized || !loginResponse.IsSuccessStatusCode) && js != null)
                {
                    Snackbar.Add(FAILED_MESSSAGE, MudBlazor.Severity.Error);
                    return;
                }
            }
        }
    }
}

