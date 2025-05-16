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

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage : IAsyncDisposable
    {
        [Inject] HttpClient Http { get; set; } = default!;
        [Inject] ApplicationDataService MyData { get; set; } = default!;
        [Inject] StudentHubService StudentHub { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] NavigationManager Nav { get; set; } = default!;
        [Inject] IJSRuntime Js { get; set; } = default!;
        private DotNetObjectReference<ExamPage> objRef = default!;

        SinhVienDto? sinhVien = new();
        CaThiDto? caThi = new();
        System.Timers.Timer? timer;
        string? displayTime;
        HubConnection? hubConnection; // cập nhật tình trạng đang thi, đã hoàn thành thi của thí sinh, ca thi
        List<bool>? isDisableAudio = [];
        List<CustomDeThi>? customDeThis = [];
        List<int>? cau_da_chons_tagA = [];// lưu vết các đáp án đã khoanh trước đó cho tag Answer button
        readonly string[] alphabet = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"];

        ChiTietCaThiDto chiTietCaThi = new();
        List<ChiTietBaiThiUpdate> chiTietBaiThiUpdate = new(); // lưu vết các câu hỏi đã chọn và câu trả lời đã chọn của sinh viên



        int so_thu_tu_tra_loi = 0; // thứ tự trả lời câu hỏi cho sv






        static List<ChiTietBaiThiRequest>? chiTietBaiThis = [];
        static List<ChiTietBaiThiRequest>? dsBaiThi_Update = []; // lưu ds các câu sv vừa mới trả lời về server
        static List<int>? cau_da_chons = []; // lưu vết các đáp án đã khoanh trước đó
        public static List<int>? listDapAn = [];// lưu vết các đáp án sinh viên chọn (public)


        const int QUY_DINH_CAP_NHAT_DAP_AN = -1; // quy định được áp dụng vào thứ tự của chi tiết bài thi
        const int QUY_DINH_XOA_DAP_AN = -2; // quy định được áp dụng vào thứ tự của chi tiết bài thi

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
            await base.OnInitializedAsync();
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        objRef = DotNetObjectReference.Create(this);
        //        await Js.InvokeVoidAsync("window.focusWatcher.init", objRef);
        //    }
        //}

        private async Task Start()
        {
            await CreateHubConnection();
            chiTietCaThi = MyData.ChiTietCaThi;
            customDeThis = MyData.CustomDeThis = await GetDeThiAPI(chiTietCaThi.MaDeThi) ?? [];
            await ModifyNhomCauHoi();

            // Nếu đã vào thi trước đó và treo máy tiếp tục thi thì chỉ lấy lại chi tiet bài thi, ko insert
            if (MyData.ChiTietCaThi.DaThi)
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




        private ChiTietBaiThiRequest GetPropertyCTBT(int vi_tri_cau_hoi, int ma_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
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
        
        private async Task OnClickDapAn(CustomDeThi deThi, int ma_cau_tra_loi)
        {
            ChiTietBaiThiRequest chiTietBai;
            var findChiTietBai = chiTietBaiThiUpdate.FirstOrDefault(p => p.MaCauHoi == deThi.MaCauHoi);
            if(findChiTietBai == null)
            {
                chiTietBai = new(-1, chiTietCaThi.MaChiTietCaThi, chiTietCaThi.MaDeThi ?? -1, deThi.MaNhom, deThi.MaCLO, deThi.MaCauHoi, ma_cau_tra_loi, DateTime.Now, null, null, ++so_thu_tu_tra_loi);
                chiTietBaiThiUpdate.Add(new ChiTietBaiThiUpdate(deThi.MaCauHoi, ma_cau_tra_loi, so_thu_tu_tra_loi, DateTime.Now));
            }
            else
            {
                chiTietBai = new(-1, chiTietCaThi.MaChiTietCaThi, chiTietCaThi.MaDeThi ?? -1, deThi.MaNhom, deThi.MaCLO, deThi.MaCauHoi, ma_cau_tra_loi, findChiTietBai.NgayTao, DateTime.Now, null, findChiTietBai.ThuTu);
            }
            //gửi bài cho server qua signalR
            await StudentHub.SendMessageChiTietBaiThi(chiTietBai);
        }

        //private async Task OnClickDapAnCheckBox(CustomDeThi deThi, int ma_cau_tra_loi, bool isChoose)
        //{
        //    ChiTietBaiThiRequest chiTietBai;
        //    if(!isChoose)
        //    {

        //    }    


        //    if (cau_tra_loi_da_chons.Contains(ma_cau_tra_loi))
        //    {
        //        //update câu trả lời
        //        chiTietBai = new(-1, chiTietCaThi.MaChiTietCaThi, chiTietCaThi.MaDeThi ?? -1, deThi.MaNhom, deThi.MaCLO, deThi.MaCauHoi, ma_cau_tra_loi, DateTime.Now, DateTime.Now, null, QUY_DINH_CAP_NHAT_DAP_AN);
        //    }
        //    else
        //    {
        //        cau_tra_loi_da_chons.Add(ma_cau_tra_loi);
        //        chiTietBai = new(-1, chiTietCaThi.MaChiTietCaThi, chiTietCaThi.MaDeThi ?? -1, deThi.MaNhom, deThi.MaCLO, deThi.MaCauHoi, ma_cau_tra_loi, DateTime.Now, null, null, ++so_thu_tu_tra_loi);
        //    }
        //}
        //private async Task OnClickNhapDapAnFieldText()
        //{
        //}





        public async ValueTask DisposeAsync()
        {
            await Js.InvokeVoidAsync("window.focusWatcher.dispose");
            timer?.Dispose();
        }
    }
    //TODO: Nên xét theo dictionary cho nhanh thay vì List đổi lại DIctionary<mã câu hỏi, ChiTietBaiThiUpdate>
    public class ChiTietBaiThiUpdate
    {
        public int MaCauHoi { get; set; }
        public int CauTraLoi { get; set; }
        public int ThuTu { get; set; }
        public DateTime NgayTao { get; set; }

        public ChiTietBaiThiUpdate(int maCauHoi, int cauTraLoi, int thuTu, DateTime ngayTao)
        {
            MaCauHoi = maCauHoi;
            CauTraLoi = cauTraLoi;
            ThuTu = thuTu;
            NgayTao = ngayTao;
        }
    }
}
