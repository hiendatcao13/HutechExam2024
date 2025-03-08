﻿using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;

namespace Hutech.Exam.Client.Pages
{
    public partial class Info
    {
        private const int THOI_GIAN_TRUOC_THI = 1; // thí sinh được phép thi trước n phút so với ca thi (1p) - BONUS TIME
        private const int THOI_GIAN_SAU_THI = 15; // thí sinh không được phép thi sau n phút so với ca thi (15p) 
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        ApplicationDataService? myData { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        IJSRuntime? js { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        private SinhVienDto? sinhVien { get; set; }
        private CaThiDto? caThi { get; set; }
        private MonHocDto? monHoc { get; set; }
        private List<ChiTietCaThiDto>? chiTietCaThis { get; set; }
        private System.Timers.Timer? timer { get; set; }
        private string? displayTime { get; set; }
        private ChiTietCaThiDto? selectedCTCaThi { get; set; }
        private HubConnection? hubConnection { get; set; }

        private const string LOGOUT_MESSAGE = "Bạn có chắc chắn muốn đăng xuất?";
        private const string NOT_CHOOSE_CA_THI = "Vui lòng chọn ca thi!";
        private const string NOT_ACTIVATED_CA_THI = "Ca thi này hiện chưa được kích hoạt hoặc dừng tạm thời. Vui lòng liên hệ quản trị để kích hoạt ca thi";
        private const string NOT_ARRIVED_TIME = "Ca thi này hiện chưa đến thời gian làm bài. Vui lòng thí sinh chờ đợi đến giờ thi";
        private const string EXPIRED_TIME = "Ca thi này hiện quá giờ làm bài. Vui lòng thí sinh liên hệ với quản trị viên";
        private const string ENTER_EXAM = "Bắt đầu thi.Chúc bạn sớm hoàn thành kết quả tốt nhất";
        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                navManager?.NavigateTo("/");
            }
            await Start();
            Time();
            await base.OnInitializedAsync();
        }
        private async Task getThongTinChiTietCaThi(long ma_sinh_vien)
        {
            // kiểm tra tham số
            if (ma_sinh_vien == -1)
                return;
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.GetAsync($"api/Info/GetThongTinChiTietCaThi?ma_sinh_vien={ma_sinh_vien}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietCaThis = JsonSerializer.Deserialize<List<ChiTietCaThiDto>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (chiTietCaThis != null && myData != null)
                    sinhVien = myData.sinhVien = chiTietCaThis[0]?.MaSinhVienNavigation;
            }
        }

        private async Task onClickDangXuat()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, LOGOUT_MESSAGE },
                { x => x.ButtonText, "Logout" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, handleDangXuat)   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            await Dialog.ShowAsync<Simple_Dialog>("Đăng xuất", parameters, options);
        }
        private async Task handleDangXuat()
        {
            if (authenticationStateProvider != null)
            {
                await UpdateLogout();
                // Cập nhật cho quản trị viên biết sinh viên đã đăng xuất
                if (isConnectHub() && sinhVien != null)
                {
                    await sendMessage(sinhVien.MaSinhVien);
                }
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                navManager?.NavigateTo("/", true);
            }
        }
        private async Task UpdateLogout()
        {
            if (httpClient != null && myData != null && myData.sinhVien != null)
                await httpClient.GetAsync($"api/User/UpdateLogout?ma_sinh_vien={myData.sinhVien.MaSinhVien}");
        }
        private async Task OnClickBatDauThi()
        {
            if (selectedCTCaThi == null)
            {
                Snackbar.Add(NOT_CHOOSE_CA_THI, Severity.Info);
                return;
            }
            if (caThi != null && (caThi.IsActivated == false || caThi.KetThuc == true))
            {
                Snackbar.Add(NOT_ACTIVATED_CA_THI, Severity.Error);
                return;
            }
            string formatTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); // vì cách hiển thị của DateTimeNow dạng local dd/MM trong khi sql lưu dạng MM/dd hoặc ngc lại
            string formatThoiGianThi = (caThi != null) ? caThi.ThoiGianBatDau.ToString("dd/MM/yyyy HH:mm:ss") : "";

            DateTime currentTime = DateTime.Now;
            DateTime thoiGianThi = new DateTime();

            DateTime.TryParse(formatThoiGianThi, out thoiGianThi);
            DateTime.TryParse(formatTime, out currentTime);

            if (caThi != null && DateTime.Compare(thoiGianThi, currentTime.AddMinutes(THOI_GIAN_TRUOC_THI)) > 0 && selectedCTCaThi != null && !selectedCTCaThi.DaThi)
            {
                Snackbar.Add(NOT_ARRIVED_TIME, Severity.Error);
                return;
            }
            if (caThi != null && DateTime.Compare(thoiGianThi.AddMinutes(THOI_GIAN_SAU_THI), currentTime) < 0 && selectedCTCaThi != null && !selectedCTCaThi.DaThi)
            {
                Snackbar.Add(EXPIRED_TIME, Severity.Error);
                return;
            }
            await HandleUpdateBatDau();
            Snackbar.Add(ENTER_EXAM, Severity.Success);
            if (myData != null)
                myData.chiTietCaThi = selectedCTCaThi;
            await Task.Delay(1000);
            navManager?.NavigateTo("/exam");
        }
        private async Task HandleUpdateBatDau()
        {
            if (selectedCTCaThi != null)
                selectedCTCaThi.ThoiGianBatDau = DateTime.Now;
            var jsonString = JsonSerializer.Serialize(selectedCTCaThi);
            if (httpClient != null)
                await httpClient.PostAsync("api/Info/UpdateBatDauThi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
        private async Task Start()
        {
            await initialHubConnection();
            sinhVien = new();
            caThi = new();
            displayTime = DateTime.Now.ToString("hh:mm:ss tt");
            chiTietCaThis = new();
            if (myData != null)
                myData.bonusTime = THOI_GIAN_TRUOC_THI;
            var authState = (authenticationState != null) ? await authenticationState : null;
            // lấy thông tin mã sinh viên từ claim
            long ma_sinh_vien = -1;
            // chuyển đổi string thành long
            if (authState != null && authState.User.Identity != null)
                long.TryParse(authState.User.Identity.Name, out ma_sinh_vien);
            await getThongTinChiTietCaThi(ma_sinh_vien);
        }
        private void Time()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += (sender, e) =>
            {
                displayTime = DateTime.Now.ToString("hh:mm:ss tt");
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            };
        }
        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();
            hubConnection?.DisposeAsync();
        }
        private async Task initialHubConnection()
        {
            if (navManager != null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri("/ChiTietCaThiHub"))
                    .Build();
                hubConnection.On("ReceiveMessage", () =>
                {
                    callLoadData();
                    StateHasChanged();
                });
                hubConnection.On<long>("ReceiveMessageResetLogin", (ma_so_sv) =>
                {
                    if (sinhVien != null && ma_so_sv == sinhVien.MaSinhVien)
                        resetLogin();
                });
                await hubConnection.StartAsync();
            }
        }
        private void callLoadData()
        {
            Task.Run(async () =>
            {
                if (sinhVien != null)
                    await getThongTinChiTietCaThi(sinhVien.MaSinhVien);
                StateHasChanged();
            });
        }
        private void resetLogin()
        {
            Task.Run(async () =>
            {
                if (authenticationStateProvider != null)
                {
                    var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                    await customAuthStateProvider.UpdateAuthenticationState(null);
                    navManager?.NavigateTo("/", true);
                }
            });
        }
        private bool isConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task sendMessage(long ma_sinh_vien)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMSV", ma_sinh_vien);
        }
    }
}
