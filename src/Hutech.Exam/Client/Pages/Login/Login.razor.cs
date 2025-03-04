using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Hutech.Exam.Client.Authentication;
using System.Net;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components.Web;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
namespace Hutech.Exam.Client.Pages.Login
{

    public partial class Login
    {
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        private SinhVienDto? sv { get; set; }
        private UserSession? userSession { get; set; }
        private string? ma_so_sinh_vien = "";
        private string? password = "";
        private HubConnection? hubConnection { get; set; }


        private const string FAILED_MESSSAGE = "Không thể xác thực người dùng hoặc tài khoản đang được người khác sử dụng.Vui lòng kiểm tra lại và báo cho người giám sát nếu cần thiết";
        private const string SUCCESS_MESSAGE = "Đăng nhập thành công!";
        private const string ERROR_MESSAGE = "Username và password không trùng khớp";

        protected override async Task OnInitializedAsync()
        {
            Start();
            sv = new();
            //nếu đã tồn tại người dùng đăng nhập trước đó, chuyển trang
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null && authenticationState != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var authState = await authenticationState;
                ma_so_sinh_vien = authState?.User.Identity?.Name;
                navManager?.NavigateTo("/info", true);
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
            if (ma_so_sinh_vien == password && ma_so_sinh_vien != "" && httpClient != null)
            {

                // Gửi yêu cầu HTTP POST đến API và nhận phản hồi
                var loginResponse = await httpClient.GetAsync($"api/User/Verify?ma_so_sinh_vien={ma_so_sinh_vien}");

                // Kiểm tra xem yêu cầu có thành công không
                if (loginResponse.IsSuccessStatusCode && authenticationStateProvider != null && navManager != null)
                {
                    // Đọc kết quả từ phản hồi
                    var resultString = await loginResponse.Content.ReadAsStringAsync();

                    // Chuyển đổi kết quả từ chuỗi JSON thành giá trị mình muốn
                    userSession = JsonSerializer.Deserialize<UserSession>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;

                    // Cập nhật trạng thái của UserSession lại, chuyển từ Anyousmous thành người có danh tính
                    await customAuthenticationStateProvider.UpdateAuthenticationState(userSession);
                    sv = userSession?.NavigateSinhVien;
                    // Cập nhật cho quản trị viên biết sinh viên đã đăng nhập
                    if (isConnectHub() && sv != null)
                        await sendMessage(sv.MaSinhVien);
                    Snackbar.Add(SUCCESS_MESSAGE, Severity.Success);
                    await Task.Delay(1000);
                    navManager.NavigateTo("/info", true);
                }
                else if (loginResponse.StatusCode == HttpStatusCode.Unauthorized || !loginResponse.IsSuccessStatusCode)
                {
                    Snackbar.Add(FAILED_MESSSAGE, Severity.Error);
                    return;
                }
            }
            else
            {
                Snackbar.Add(ERROR_MESSAGE, Severity.Error);
                return;
            }
        }

        private async void Start()
        {
            if (navManager != null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri("/ChiTietCaThiHub"))
                    .Build();

                await hubConnection.StartAsync();
            }
        }
        private bool isConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task sendMessage(long ma_sinh_vien)
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
