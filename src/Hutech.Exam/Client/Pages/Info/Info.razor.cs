using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;
using AutoMapper;

namespace Hutech.Exam.Client.Pages.Info
{
    public partial class Info
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private ApplicationDataService MyData { get; set; } = default!;
        [Inject] private StudentHubService StudentHub { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private IMapper Mapper { get; set; } = default!;

        private SinhVienDto? sinhVien = new();
        private CaThiDto? caThi = new();
        private MonHocDto? monHoc = new();
        private ChiTietCaThiDto? selectedCTCaThi = new();
        private List<ChiTietCaThiDto> chiTietCaThis = [];
        private System.Timers.Timer? timer;
        private HubConnection? hubConnection;
        private string? displayTime;

        private const int THOI_GIAN_TRUOC_THI = 1; // thí sinh được phép thi trước n phút so với ca thi (1p) - BONUS TIME
        private const int THOI_GIAN_SAU_THI = 15; // thí sinh không được phép thi sau n phút so với ca thi (15p) 
        private const string LOGOUT_MESSAGE = "Bạn có chắc chắn muốn đăng xuất?";
        private const string NOT_CHOOSE_CA_THI = "Vui lòng chọn ca thi!";
        private const string NOT_ACTIVATED_CA_THI = "Ca thi này hiện chưa được kích hoạt hoặc dừng tạm thời. Vui lòng liên hệ quản trị để kích hoạt ca thi";
        private const string NOT_ARRIVED_TIME = "Ca thi này hiện chưa đến thời gian làm bài. Vui lòng thí sinh chờ đợi đến giờ thi";
        private const string EXPIRED_TIME = "Ca thi này hiện quá giờ làm bài. Vui lòng thí sinh liên hệ với quản trị viên";
        private const string ENTER_EXAM = "Bắt đầu thi.Chúc bạn sớm hoàn thành kết quả tốt nhất";
        private const string HAS_NO_MADETHI = "Thí sinh tạm thời chưa có đề thi được phân công. Vui lòng liên hệ với quản trị viên";
        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            else
                Nav.NavigateTo("/");
            await Start();
            Time();
            await base.OnInitializedAsync();
        }
        private async Task GetThongTinChiTietCaThi(long ma_sinh_vien)
        {
            var getThongTinCTCT_gan_nhat = await GetChiTietCaThiAPI(ma_sinh_vien);
            if (getThongTinCTCT_gan_nhat != null && getThongTinCTCT_gan_nhat.MaSinhVienNavigation != null)
            {
                chiTietCaThis.Add(getThongTinCTCT_gan_nhat);
                sinhVien = MyData.SinhVien = getThongTinCTCT_gan_nhat.MaSinhVienNavigation;
            }
        }
        private async Task OnClickDangXuat()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, LOGOUT_MESSAGE },
                { x => x.ButtonText, "Đăng xuất" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDangXuat())   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Thoát tài khoản", parameters, options);
        }
        private async Task HandleDangXuat()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }
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
            if (selectedCTCaThi.MaDeThi == null)
            {
                Snackbar.Add(HAS_NO_MADETHI, Severity.Error);
                return;
            }
            string formatTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); // vì cách hiển thị của DateTimeNow dạng local dd/MM trong khi sql lưu dạng MM/dd hoặc ngc lại
            string formatThoiGianThi = caThi != null ? caThi.ThoiGianBatDau.ToString("dd/MM/yyyy HH:mm:ss") : "";

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
            Snackbar.Add(ENTER_EXAM, Severity.Success);
            if (selectedCTCaThi != null)
            {
                //await UpdateBatDauThiAPI(selectedCTCaThi);
                MyData.ChiTietCaThi = selectedCTCaThi;
            }    
            Nav?.NavigateTo("/test");
        }
        private async Task Start()
        {
            await GetThongTinSV();
            await CreateHubConnection();
            displayTime = DateTime.Now.ToString("hh:mm:ss tt");
            MyData.BonusTime = THOI_GIAN_TRUOC_THI;
        }
        private async Task GetThongTinSV()
        {
            var authState = AuthenticationState != null ? await AuthenticationState : null;
            // lấy thông tin mã sinh viên từ claim
            long ma_sinh_vien = -1;
            // chuyển đổi string thành long
            if (authState != null && authState.User.Identity != null)
                long.TryParse(authState.User.Identity.Name, out ma_sinh_vien);
            await GetThongTinChiTietCaThi(ma_sinh_vien);
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
        private async Task CreateHubConnection()
        {
            hubConnection = await StudentHub.GetConnectionAsync(sinhVien?.MaSinhVien ?? -1);

            hubConnection.On("ResetLogin", async () =>
            {
               await HandleDangXuat();
            });
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
