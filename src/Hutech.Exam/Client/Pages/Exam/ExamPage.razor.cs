using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Client.Authentication;
using MudBlazor;
using System.Net.Http.Headers;
using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.API;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage : IAsyncDisposable
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private ApplicationDataService MyData { get; set; } = default!;

        [Inject] private StudentHubService StudentHub { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private IJSRuntime Js { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;


        // các biến binding cho UI
        private DotNetObjectReference<ExamPage> _objRef { get; set; } = default!;

        private List<CustomDeThi>? CustomExams { get; set; } = [];

        private SinhVienDto Students { get; set; } = default!;

        private CaThiDto ExamSession { get; set; } = default!;

        private ChiTietCaThiDto ExamSessionDetail { get; set; } = default!;

        private Dictionary<int, (int, int?)> SelectedAnswers { get; set; } = []; // lưu vết các câu hỏi đã chọn hay chưa chọn của sinh viên

        private Dictionary<int, AudioInfo> AudioListeneds { get; set; } = []; // lưu vết các câu hỏi đã nghe audio của sinh viên (để hiển thị số lần nghe audio cho sinh viên)

        // các biến chỉ trong backend
        private HubConnection? _hubConnection; // cập nhật tình trạng đang thi, đã hoàn thành thi của thí sinh, ca thi

        private Dictionary<int, ChiTietBaiThiRequest> _dsThiSinhDaKhoanh = []; // lưu vết các câu hỏi đã chọn và câu trả lời đã chọn của sinh viên

        private int _thuTuTraLoi = 0; // thứ tự trả lời câu hỏi cho sv

        private readonly string[] _alphabet = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"];

        private bool _shouldRender = false; // tránh render vô tội vạ bị khựng khi onclick mặc dù UI không thay đổi gì

        const string SUBMIT_MESSAGE = "Bạn có chắc chắn muốn nộp bài?";
        const string DONG_BANG_CA_THI = "Quản trị viên đang tạm thời dừng ca thi này. Thí sinh vui lòng chờ trong giây lát";
        const string RESET_LOGIN = "Quản trị viên đã tạm thời đăng xuất bạn khỏi ca thi này. Vui lòng đăng nhập lại để tiếp tục thi";
        const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        const string ADMIN_NOP_BAI = "Bài thi của bạn được quản trị viên yêu cầu nộp bài";

        #endregion

        #region Initial Methods

        private async Task CheckPageAsync()
        {
            if (MyData.ChiTietCaThi.MaChiTietCaThi == 0 || MyData.SinhVien.MaSinhVien == 0)
            {
                Snackbar.Add(ERROR_PAGE, Severity.Error);
                await Task.Delay(1000);
                Nav?.NavigateTo("/info");
                return;
            }
            ExamSessionDetail = MyData.ChiTietCaThi;
            ExamSession = MyData.ChiTietCaThi.MaCaThiNavigation ?? new();
            Students = MyData.SinhVien;

            await StartAsync();
        }
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
                    Nav?.NavigateTo("/");
                await CheckPageAsync();
                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                Snackbar.Add("Hệ thống server đang gặp sự cố. Vui lòng liên hệ người giám sát", Severity.Error);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _objRef = DotNetObjectReference.Create(this);
                await Js.InvokeVoidAsync("window.focusWatcher.init", _objRef);

                await Js.InvokeVoidAsync("window.antiCopy.enable");
            }
        }

        protected override bool ShouldRender()
        {
            if (_shouldRender)
            {
                _shouldRender = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TheSateHasChanged()
        {
            _shouldRender = true;
            StateHasChanged(); // Hoặc StateHasChanged(); nếu bạn đang ở UI thread
            _shouldRender = false; // Reset lại sau khi render nếu cần
        }

        private async Task StartAsync()
        {
            //Time(); // xử lí countdown
            await CreateHubConnectionAsync();

            CustomExams = await ShuffleExam_SelectOneAPI(ExamSessionDetail.MaDeThi ?? -1) ?? [];
            Console.WriteLine("Số lượng câu hỏi" + CustomExams.Count);
            ModifyGroupQuestion();

            // Nếu đã vào thi trước đó và treo máy tiếp tục thi thì chỉ lấy lại chi tiet bài thi, ko insert
            if (MyData.ChiTietCaThi.DaThi)
            {
                await GetExamSubmissionAsync();
            }

            TheSateHasChanged();
        }

        #endregion

        #region Continue Test Methods

        private async Task GetExamSubmissionAsync()
        {
            // lấy ds bài thi đã khoanh trước đó
            var _dsDapAnTiepTucThi = await StudentHub.RequestTiepTucThi(ExamSessionDetail.MaChiTietCaThi);

            // xử lí câu đáp án đã khoanh
            foreach (var item in _dsDapAnTiepTucThi)
            {
                _dsThiSinhDaKhoanh.Add(item.Key, item.Value);
                SelectedAnswers[item.Key] = (SelectedAnswers[item.Key].Item1, item.Value.CauTraLoi);
            }
        }

        private double? CalculateRemainingTime()
        {
            if (ExamSession == null)
                return null;

            DateTime currentTime = DateTime.Now;
            DateTime thoiGianBatDau = ExamSession.ThoiGianBatDau;

            TimeSpan? result = currentTime - thoiGianBatDau;
            return result?.TotalMinutes;
        }

        #endregion

        #region Time Methods
        private int CalculateTotalSeconds()
        {
            int tong_so_giay = 0;
            int? thoi_gian_con_lai = (int?)CalculateRemainingTime();

            if (ExamSession != null && ExamSessionDetail != null && thoi_gian_con_lai != null)
            {
                tong_so_giay += (ExamSession.ThoiGianThi + ExamSessionDetail.GioCongThem + MyData.BonusTime - (int)thoi_gian_con_lai) * 60;
                tong_so_giay = (tong_so_giay > (ExamSession.ThoiGianThi * 60)) ? (ExamSession.ThoiGianThi * 60) : tong_so_giay;
            }

            Console.WriteLine("Tong so giay" + tong_so_giay);
            return tong_so_giay;
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickSubmitAsync()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, SUBMIT_MESSAGE },
                { x => x.ButtonText, "Nộp bài" },
                { x => x.Color, Color.Warning },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, EndTimeSubmissionAsync)   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            await Dialog.ShowAsync<Simple_Dialog>("Kết thúc bài làm", parameters, options);
        }

        private async Task OnClickAnswerAsync(CustomDeThi deThi, int? ma_cau_tra_loi)
        {
            // xanh phần tô
            SelectedAnswers[deThi.MaCauHoi] = (SelectedAnswers[deThi.MaCauHoi].Item1, ma_cau_tra_loi);

            TheSateHasChanged();

            ChiTietBaiThiRequest chiTietBai;
            if (!_dsThiSinhDaKhoanh.ContainsKey(deThi.MaCauHoi))
            {
                chiTietBai = new(-1, ExamSessionDetail.MaChiTietCaThi, ExamSessionDetail.MaDeThi ?? -1, deThi.MaNhom, deThi.MaCLO, deThi.MaCauHoi, ma_cau_tra_loi, DateTime.Now, null, null, ++_thuTuTraLoi);
            }
            else
            {
                chiTietBai = new(-1, ExamSessionDetail.MaChiTietCaThi, ExamSessionDetail.MaDeThi ?? -1, deThi.MaNhom, deThi.MaCLO, deThi.MaCauHoi, ma_cau_tra_loi, _dsThiSinhDaKhoanh[deThi.MaCauHoi].NgayTao, DateTime.Now, null, _dsThiSinhDaKhoanh[deThi.MaCauHoi].ThuTu);
            }

            _dsThiSinhDaKhoanh[deThi.MaCauHoi] = chiTietBai;
            //gửi bài cho server qua signalR
            await StudentHub.SendMessageChiTietBaiThi(chiTietBai);
        }

        #endregion

        #region Other Methods

        private async Task ScrollToQuestionAsync(int questionIndex)
        {
            await Js.InvokeVoidAsync("scrollToElement", $"question-{questionIndex}");
        }
        public async ValueTask DisposeAsync()
        {
            await Js.InvokeVoidAsync("window.focusWatcher.dispose");

            //await StudentHub.LeaveGroup(CaThi.MaCaThi); // rời khỏi nhóm thi
        }

        #endregion


    }

    public class AudioInfo
    {
        public string? AudioUrl { get; set; }
        public string? AudioText { get; set; }
    }
}
