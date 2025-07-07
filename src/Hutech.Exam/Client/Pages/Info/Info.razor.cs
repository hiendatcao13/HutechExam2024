using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Client.API;
using System.Security.Claims;

namespace Hutech.Exam.Client.Pages.Info
{
    public partial class Info
    {
        #region Private Fields
        [Inject] HttpClient Http { get; set; } = default!;

        [Inject] NavigationManager Nav { get; set; } = default!;

        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] ApplicationDataService MyData { get; set; } = default!;

        [Inject] StudentHubService StudentHub { get; set; } = default!;

        [Inject] ISnackbar Snackbar { get; set; } = default!;

        [Inject] IDialogService Dialog { get; set; } = default!;

        [Inject] ISenderAPI SenderAPI { get; set; } = default!;

        [CascadingParameter] Task<AuthenticationState>? AuthenticationState { get; set; }

        // biến binding UI
        private SinhVienDto? Student { get; set; }

        private CaThiDto? ExamSession { get; set; }

        private List<ChiTietCaThiDto> ExamSessionDetails { get; set; } = [];

        private MonHocDto? Subject { get; set; }

        private string? DisplayTime { get; set; }

        private ChiTietCaThiDto? SelectedExamSessionDetail { get; set; }

        // biến nội bộ

        private System.Timers.Timer? _timer;

        private HubConnection? _hubConnection;

        private const int TimeBeforeExam = 1; // thí sinh được phép thi trước n phút so với ca thi (1p) - BONUS TIME
        private const int TimeAfterExam = 15; // thí sinh không được phép thi sau n phút so với ca thi (15p) 
        private const string LogoutMessage = "Bạn có chắc chắn muốn đăng xuất?";
        private const string NoChooseMessage = "Vui lòng chọn ca thi!";
        private const string NoActivedExamSessionMessage = "Ca thi này hiện chưa được kích hoạt hoặc dừng tạm thời. Vui lòng liên hệ quản trị để kích hoạt ca thi";
        private const string NotArrviedTimeMessage = "Ca thi này hiện chưa đến thời gian làm bài. Vui lòng thí sinh chờ đợi đến giờ thi";
        private const string ExpireTimeMessage = "Ca thi này hiện quá giờ làm bài. Vui lòng thí sinh liên hệ với quản trị viên";
        private const string EnterExamMessage = "Bắt đầu thi.Chúc bạn sớm hoàn thành kết quả tốt nhất";
        private const string HasNoExamMessage = "Thí sinh tạm thời chưa có đề thi được phân công. Vui lòng liên hệ với quản trị viên";

        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            try
            {
                //xác thực người dùng
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                var token = await customAuthStateProvider.GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                    Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                else
                    Nav.NavigateTo("/");
                await StartAsync();
                HandleTime();
                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                Snackbar.Add("Hệ thống server đang gặp sự cố. Vui lòng liên hệ người giám sát", Severity.Error);
            }
        }

        private async Task GetExamSessionInformationAsync(long studentId)
        {
            var getThongTinCTCT_gan_nhat = await GetChiTietCaThiAPI(studentId);
            if (getThongTinCTCT_gan_nhat != null && getThongTinCTCT_gan_nhat.MaSinhVienNavigation != null)
            {
                ExamSessionDetails.Add(getThongTinCTCT_gan_nhat);
                Student = MyData.SinhVien = getThongTinCTCT_gan_nhat.MaSinhVienNavigation;
            }
        }
        private async Task OnClickLogoutAsync()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, LogoutMessage },
                { x => x.ButtonText, "Đăng xuất" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleLogoutAsync())   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Thoát tài khoản", parameters, options);
        }
        private async Task HandleLogoutAsync()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien.MaSinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }
        }

        private async Task OnClickStartExamAsync()
        {
            if (SelectedExamSessionDetail == null)
            {
                Snackbar.Add(NoChooseMessage, Severity.Info);
                return;
            }
            if (ExamSession != null && (ExamSession.KichHoat == false || ExamSession.KetThuc == true))
            {
                Snackbar.Add(NoActivedExamSessionMessage, Severity.Error);
                return;
            }
            if (SelectedExamSessionDetail.MaDeThi == null || SelectedExamSessionDetail.MaDeThi == 0)
            {
                Snackbar.Add(HasNoExamMessage, Severity.Error);
                return;
            }
            DateTime currentTime = DateTime.Now; // vì cách hiển thị của DateTimeNow dạng local dd/MM trong khi sql lưu dạng MM/dd hoặc ngc lại
            DateTime thoiGianThi = ExamSession?.ThoiGianBatDau ?? DateTime.Now;

            if (ExamSession != null && DateTime.Compare(thoiGianThi, currentTime.AddMinutes(TimeBeforeExam)) > 0 && SelectedExamSessionDetail != null && !SelectedExamSessionDetail.DaThi)
            {
                Snackbar.Add(NotArrviedTimeMessage, Severity.Error);
                return;
            }
            if (ExamSession != null && DateTime.Compare(thoiGianThi.AddMinutes(TimeAfterExam), currentTime) < 0 && SelectedExamSessionDetail != null && !SelectedExamSessionDetail.DaThi)
            {
                Snackbar.Add(ExpireTimeMessage, Severity.Error);
                return;
            }
            Console.WriteLine($"Thời gian thi: {thoiGianThi} và thời gian hiện tại: {currentTime}");
            Snackbar.Add(EnterExamMessage, Severity.Success);
            if (SelectedExamSessionDetail != null)
            {
                if(!SelectedExamSessionDetail.DaThi)
                {
                    await UpdateBatDauThiAPI(SelectedExamSessionDetail.MaChiTietCaThi);
                }
                MyData.ChiTietCaThi = SelectedExamSessionDetail;
            }
            Nav.NavigateTo("/test");
        }

        private async Task StartAsync()
        {
            await GetStudentInfoAsync();
            await CreateHubConnectionAsync();
            DisplayTime = DateTime.Now.ToString("hh:mm:ss tt");
            MyData.BonusTime = TimeBeforeExam;
        }

        private async Task GetStudentInfoAsync()
        {
            var authState = AuthenticationState != null ? await AuthenticationState : null;
            // lấy thông tin mã sinh viên từ claim
            long ma_sinh_vien = -1;
            // chuyển đổi string thành long
            if (authState != null && authState.User.Identity != null)
                long.TryParse(authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out ma_sinh_vien);

            await GetExamSessionInformationAsync(ma_sinh_vien);
        }

        private void HandleTime()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000; // 1000 = 1ms
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += (sender, e) =>
            {
                DisplayTime = DateTime.Now.ToString("hh:mm:ss tt");
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            };
        }
        private async Task CreateHubConnectionAsync()
        {
            _hubConnection = await StudentHub.GetConnectionAsync(Student?.MaSinhVien ?? -1);

            _hubConnection.On("ResetLogin", async () =>
            {
               await HandleLogoutAsync();
            });
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
        #endregion
    }
}
