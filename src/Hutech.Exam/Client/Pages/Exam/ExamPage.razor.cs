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

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage : IDisposable
    {
        [Inject] HttpClient Http { get; set; } = default!;
        [Inject] ApplicationDataService MyData { get; set; } = default!;
        [Inject] StudentHubService StudentHub { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] NavigationManager Nav { get; set; } = default!;
        [Inject] IJSRuntime Js { get; set; } = default!;

        SinhVienDto? sinhVien = new();
        CaThiDto? caThi = new();
        System.Timers.Timer? timer;
        string? displayTime;
        HubConnection? hubConnection; // cập nhật tình trạng đang thi, đã hoàn thành thi của thí sinh, ca thi
        bool is_pause = false; // cập nhật trạng thái dừng ca thi của thí sinh
        List<bool>? isDisableAudio = [];
        List<CustomDeThi>? customDeThis = [];
        List<int>? cau_da_chons_tagA = [];// lưu vết các đáp án đã khoanh trước đó cho tag Answer button
        readonly string[] alphabet = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"];

        static ChiTietCaThiDto? chiTietCaThi = new();
        static List<ChiTietBaiThiRequest>? chiTietBaiThis = [];
        static List<ChiTietBaiThiRequest>? dsBaiThi_Update = []; // lưu ds các câu sv vừa mới trả lời về server
        static List<int>? cau_da_chons = []; // lưu vết các đáp án đã khoanh trước đó
        public static List<int>? listDapAn = [];// lưu vết các đáp án sinh viên chọn (public)


        const int GIAY_CAP_NHAT = 10; // tự động lưu bài sau n giây (2p). Với bonus time, vui lòng chỉnh trang info (THOI_GIAN_TRUOC_THI) 
        const string SUBMIT_MESSAGE = "Bạn có chắc chắn muốn nộp bài?";
        const string ERROR_FETCH_DETHI = "Không thể lấy đề thi. Vui lòng kiểm tra SV đã có đề thi chưa hoặc hệ thống lỗi";
        const string ERROR_FETCH_BAILAM = "Không thể lấy bài làm trước hoặc hệ thống lỗi";
        const string ERROR_UPDATE_BAILAM = "Lưu bài không thành công";
        const string DONG_BANG_CA_THI = "Quản trị viên đang tạm thời dừng ca thi này. Thí sinh vui lòng chờ trong giây lát";
        const string RESET_LOGIN = "Quản trị viên đã tạm thời đăng xuất bạn khỏi ca thi này. Vui lòng đăng nhập lại để tiếp tục thi";
        const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        const string ADMIN_NOP_BAI = "Bài thi của bạn được quản trị viên yêu cầu nộp bài";

        private async Task CheckPage()
        {
            if (MyData.ChiTietCaThi.MaChiTietCaThi == 0 || MyData.SinhVien.MaSinhVien == 0)
            {
                Snackbar.Add(ERROR_PAGE, Severity.Error);
                await Task.Delay(1000);
                Nav?.NavigateTo("/info");
                return;
            }
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
            await CheckPage();
            // chế độ focus page
            Js?.InvokeVoidAsync("focusPage", DotNetObjectReference.Create(this));
            await base.OnInitializedAsync();
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
                tong_so_giay = (tong_so_giay > (caThi.ThoiGianThi * 60)) ? (caThi.ThoiGianThi * 60) : tong_so_giay;
            }
            displayTime = FormatTime(tong_so_giay);
            int so_giay_hien_tai = tong_so_giay; // tránh sv đổi thời gian
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
            if (chiTietBaiThis != null)
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
        [JSInvokable] // Đánh dấu hàm để có thể gọi từ JavaScript
        public static Task<int> GetDapAnFromJavaScript(int vi_tri_cau_hoi, int ma_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
        {
            // Xử lý giá trị được truyền từ JavaScript
            if (listDapAn != null)
                listDapAn.Add(ma_cau_tra_loi);

            ChiTietBaiThiRequest? findChiTietBaiThi = chiTietBaiThis?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
            ChiTietBaiThiRequest tempChiTietBaiThi = getPropertyCTBT(vi_tri_cau_hoi, ma_cau_tra_loi, ma_nhom, ma_cau_hoi);

            // trường hợp thí sinh sửa đáp án của câu đã trả lời trước đó
            if (findChiTietBaiThi != null && chiTietBaiThis != null)
            {
                findChiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
                //tempChiTietBaiThi.ThuTu = 0; // biến này để đánh dấu cho server biết câu này đã được insert, chỉ cần update
            }
            else
                chiTietBaiThis?.Add(tempChiTietBaiThi);

            // trường hợp sinh viên lại khoanh lại đáp án nhiều lần trong 1 lần lưu
            ChiTietBaiThiRequest? chiTietBaiThi = dsBaiThi_Update?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
            if (chiTietBaiThi != null)
            {
                chiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
            }
            else
            {
                dsBaiThi_Update?.Add(tempChiTietBaiThi);
            }

            return Task.FromResult<int>(ma_cau_tra_loi);
        }

        private static ChiTietBaiThiRequest getPropertyCTBT(int vi_tri_cau_hoi, int ma_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
        {
            ChiTietBaiThiRequest chiTietBaiThi = new();
            if (chiTietCaThi != null && chiTietCaThi.MaDeThi != null)
            {
                chiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
                chiTietBaiThi.MaCauHoi = ma_cau_hoi;
                chiTietBaiThi.MaNhom = ma_nhom;
                chiTietBaiThi.ThuTu = vi_tri_cau_hoi;
                chiTietBaiThi.MaChiTietCaThi = chiTietCaThi.MaChiTietCaThi;
                chiTietBaiThi.MaDeHv = (long)chiTietCaThi.MaDeThi;
            }
            return chiTietBaiThi;
        }








        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
