using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class Exam
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private ApplicationDataService MyData { get; set; } = default!;
        [Inject] private StudentHubService StudentHub { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IJSRuntime Js { get; set; } = default!;

        private SinhVienDto? sinhVien = new();
        private CaThiDto? caThi = new();
        private System.Timers.Timer? timer;
        private string? displayTime;
        private HubConnection? hubConnection; // cập nhật tình trạng đang thi, đã hoàn thành thi của thí sinh, ca thi
        private bool is_pause = false; // cập nhật trạng thái dừng ca thi của thí sinh
        private List<bool>? isDisableAudio = [];
        private List<CustomDeThi>? customDeThis = [];
        private List<int>? cau_da_chons_tagA = [];// lưu vết các đáp án đã khoanh trước đó cho tag Answer button
        private readonly List<string>? alphabet = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"];

        private static ChiTietCaThiDto? chiTietCaThi = new();
        private static List<CustomChiTietBaiThi>? chiTietBaiThis = [];
        private static List<CustomChiTietBaiThi>? dsBaiThi_Update = []; // lưu ds các câu sv vừa mới trả lời về server
        private static List<int>? cau_da_chons = []; // lưu vết các đáp án đã khoanh trước đó
        public static List<int>? listDapAn = [];// lưu vết các đáp án sinh viên chọn (public)

        private const int GIAY_CAP_NHAT = 10; // tự động lưu bài sau n giây (2p). Với bonus time, vui lòng chỉnh trang info (THOI_GIAN_TRUOC_THI) 
        private const string SUBMIT_MESSAGE = "Bạn có chắc chắn muốn nộp bài?";
        private const string ERROR_FETCH_DETHI = "Không thể lấy đề thi. Vui lòng kiểm tra SV đã có đề thi chưa hoặc hệ thống lỗi";
        private const string ERROR_FETCH_BAILAM = "Không thể lấy bài làm trước hoặc hệ thống lỗi";
        private const string ERROR_UPDATE_BAILAM = "Lưu bài không thành công";
        private const string DONG_BANG_CA_THI = "Quản trị viên đang tạm thời dừng ca thi này. Thí sinh vui lòng chờ trong giây lát";
        private const string RESET_LOGIN = "Quản trị viên đã tạm thời đăng xuất bạn khỏi ca thi này. Vui lòng đăng nhập lại để tiếp tục thi";
        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private const string ADMIN_NOP_BAI = "Bài thi của bạn được quản trị viên yêu cầu nộp bài";
        private async Task checkPage()
        {
            if (MyData.ChiTietCaThi.MaChiTietCaThi == 0 || MyData.SinhVien.MaSinhVien == 0)
            {
                Snackbar.Add(ERROR_PAGE, Severity.Error);
                await Task.Delay(1000);
                Nav?.NavigateTo("/info");
                return;
            }
            KhoiTaoBanDau();
            chiTietCaThi = MyData.ChiTietCaThi;
            caThi = MyData.ChiTietCaThi.MaCaThiNavigation;
            sinhVien = MyData.SinhVien;
            await Start();
            Time(); // xử lí countdown
        }
        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = await customAuthStateProvider.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            else
                Nav?.NavigateTo("/");
            await checkPage();
            // chế độ focus page
            Js?.InvokeVoidAsync("focusPage", DotNetObjectReference.Create(this));
            await base.OnInitializedAsync();
        }
        private async Task OnClickNopBai()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, SUBMIT_MESSAGE },
                { x => x.ButtonText, "Nộp bài" },
                { x => x.Color, Color.Warning },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, KetThucThoiGianLamBai)   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            await Dialog.ShowAsync<Simple_Dialog>("Kết thúc bài làm", parameters, options);
        }

        [JSInvokable]
        public async Task KetThucThoiGianLamBai()
        {
            await UpdateChiTietBaiThiAPI();

            if (chiTietBaiThis != null && listDapAn != null)
            {
                MyData.ChiTietBaiThis = chiTietBaiThis;
                MyData.ListDapAnKhoanh = listDapAn;
            }
            Nav?.NavigateTo("/result");
        }
        private void KhoiTaoBanDau()
        {
            sinhVien = MyData.SinhVien;
        }
        private async Task Start()
        {
            await CreateHubConnection();
            chiTietCaThi = MyData.ChiTietCaThi;
            customDeThis = MyData.CustomDeThis = await GetDeThiAPI(chiTietCaThi.MaDeThi) ?? [];
            await ModifyNhomCauHoi();

            // Nếu đã vào thi trước đó và treo máy tiếp tục thi thì chỉ lấy lại chi tiet bài thi, ko insert
            if (MyData.ChiTietCaThi != null && MyData.ChiTietCaThi.DaThi)
            {
                await GetBaiThi_DaThi();
                ProcessTiepTucThi();
            }
        }
        private async Task GetBaiThi_DaThi()
        {
            if (chiTietCaThi != null)
                chiTietBaiThis = await GetBaiThi_DaThiAPI(chiTietCaThi.MaChiTietCaThi) ?? [];
        }
        private void Time()
        {
            timer = new System.Timers.Timer
            {
                Interval = 1000, // 1000 = 1ms
                AutoReset = true,
                Enabled = true
            };
            DateTime currentTime = DateTime.Now;
            int tong_so_giay = 0;
            // cập nhật thời gian thi còn lại cho sinh viên nếu bị out
            int? thoi_gian_con_lai = (int?)ThoiGianConLai();
            if (caThi != null && chiTietCaThi != null && thoi_gian_con_lai != null)
            {
                tong_so_giay += (caThi.ThoiGianThi + chiTietCaThi.GioCongThem + MyData.BonusTime - (int)thoi_gian_con_lai) * 60;
                //js?.InvokeVoidAsync("alert", "thoi gian con lai: " + thoi_gian_con_lai + ", tong_so_giay: " + tong_so_giay);
                tong_so_giay = (tong_so_giay > (caThi.ThoiGianThi * 60)) ? (caThi.ThoiGianThi * 60) : tong_so_giay;
            }
            displayTime = FormatTime(tong_so_giay);
            int so_giay_hien_tai = tong_so_giay;
            timer.Elapsed += async (sender, e) =>
            {
                so_giay_hien_tai--;
                // cứ mỗi n giây thì hệ thống tự động lưu bài của SV
                if (so_giay_hien_tai % GIAY_CAP_NHAT == 0)
                {
                    await UpdateChiTietBaiThiAPI();
                }
                if (so_giay_hien_tai == 60)
                {
                    await Js.InvokeVoidAsync("changeColorTime"); // đổi màu đồng hồ thành đỏ khi gần kết thúc
                }
                if (so_giay_hien_tai >= 0)
                {
                    displayTime = FormatTime(so_giay_hien_tai);
                    await InvokeAsync(StateHasChanged); // Cập nhật giao diện người dùng
                }
                else
                {
                    timer.Stop(); // Dừng timer khi countdown kết thúc
                    await KetThucThoiGianLamBai();
                }
            };
        }

        // Xử lí việc thí sinh bị out ra khi đang làm bài
        private void ProcessTiepTucThi()
        {
            DateTime? thoi_gian = chiTietBaiThis?.Max(p => p.NgayCapNhat);
            thoi_gian = thoi_gian?.AddSeconds(GIAY_CAP_NHAT);
            if (chiTietBaiThis != null && thoi_gian != null)
            {
                foreach (var item in chiTietBaiThis)
                {
                    if (item.CauTraLoi != null && customDeThis != null && cau_da_chons != null && cau_da_chons_tagA != null)
                    {
                        cau_da_chons.Add((int)item.CauTraLoi);
                        int STT = 1;
                        foreach (var chiTietDeThi in customDeThis)
                        {
                            if (chiTietDeThi.MaNhom == item.MaNhom && chiTietDeThi.MaCauHoi == item.MaCauHoi)
                                cau_da_chons_tagA.Add(STT);

                            // cập nhật lại danh sách sinh viên đã khoanh
                            listDapAn?.Add((int)item.CauTraLoi);
                            STT++;
                        }
                    }
                }
            }
        }
        private double? ThoiGianConLai()
        {
            if (caThi == null) return null;

            DateTime currentTime = DateTime.Now;
            DateTime thoiGianBatDau = caThi.ThoiGianBatDau;

            // Ensure both dates are in the same format
            string currentTimeFormatted = currentTime.ToString("MM/dd/yyyy HH:mm:ss");
            string thoiGianBatDauFormatted = thoiGianBatDau.ToString("MM/dd/yyyy HH:mm:ss");

            DateTime.TryParse(currentTimeFormatted, out currentTime);
            DateTime.TryParse(thoiGianBatDauFormatted, out thoiGianBatDau);

            TimeSpan? result = currentTime - thoiGianBatDau;
            return result?.TotalMinutes;
        }
        private string FormatTime(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";// format từ giây sang phút/giây với D2 là 2 số nguyên
        }
        public void Dispose()
        {
            timer?.Dispose();
        }

    }
}
