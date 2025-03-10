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

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class Exam
    {
        private const int GIAY_CAP_NHAT = 10; // tự động lưu bài sau n giây (2p). Với bonus time, vui lòng chỉnh trang info (THOI_GIAN_TRUOC_THI) 
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
        private SinhVienDto? sinhVien { get; set; }
        private CaThiDto? caThi { get; set; }
        private static ChiTietCaThiDto? chiTietCaThi { get; set; }
        private List<CustomDeThi>? customDeThis { get; set; }
        private static List<ChiTietBaiThiDto>? chiTietBaiThis { get; set; }
        private static List<ChiTietBaiThiDto>? dsBaiThi_Update { get; set; } // lưu ds các câu sv vừa mới trả lời về server
        private static List<int>? cau_da_chons { get; set; } // lưu vết các đáp án đã khoanh trước đó
        private List<int>? cau_da_chons_tagA { get; set; }// lưu vết các đáp án đã khoanh trước đó cho tag Answer button
        private List<string>? alphabet { get; set; }
        public static List<int>? listDapAn { get; set; }// lưu vết các đáp án sinh viên chọn
        private System.Timers.Timer? timer { get; set; }
        private string? displayTime { get; set; }
        private HubConnection? hubConnection { get; set; } // cập nhật tình trạng đang thi, đã hoàn thành thi của thí sinh, ca thi
        private bool is_pause { get; set; } // cập nhật trạng thái dừng ca thi của thí sinh
        private List<bool>? isDisableAudio { get; set; }

        private const string SUBMIT_MESSAGE = "Bạn có chắc chắn muốn nộp bài?";
        private async Task checkPage()
        {
            if ((myData == null || myData.ChiTietCaThi == null || myData.SinhVien == null) && js != null)
            {
                await js.InvokeVoidAsync("alert", "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại");
                navManager?.NavigateTo("/info");
                return;
            }
            if(myData != null && myData.ChiTietCaThi != null)
            {
                khoiTaoBanDau();
                chiTietCaThi = myData.ChiTietCaThi;
                caThi = myData.ChiTietCaThi.MaCaThiNavigation;
                sinhVien = myData.SinhVien;
                await Start();
                Time(); // xử lí countdown
            }
        }
        protected override async Task OnInitializedAsync()
        {
            alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
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
            await checkPage();
            // chế độ focus page
            if (js != null)
            {
                js?.InvokeVoidAsync("focusPage", DotNetObjectReference.Create(this));
            }
            await base.OnInitializedAsync();
        }
        private async Task onClickNopBai()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, SUBMIT_MESSAGE },
                { x => x.ButtonText, "Nộp bài" },
                { x => x.Color, Color.Warning },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, ketThucThoiGianLamBai)   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            await Dialog.ShowAsync<Simple_Dialog>("Kết thúc bài làm", parameters, options);
        }
        
        [JSInvokable]
        public async Task ketThucThoiGianLamBai()
        {
            await UpdateChiTietBaiThi();
            // Cập nhật cho quản trị viên biết sinh đã hoàn thành bài thi
            if (isConnectHub() && chiTietCaThi != null && chiTietCaThi.MaCaThi != null)
                await sendMessage((int)chiTietCaThi.MaCaThi);
            if (myData != null)
            {
                myData.ChiTietBaiThis = chiTietBaiThis;
                myData.ListDapAnKhoanh = listDapAn;
            }

            navManager?.NavigateTo("/result");
        }
        private void khoiTaoBanDau()
        {
            chiTietBaiThis = new();
            sinhVien = new();
            if (myData != null)
                sinhVien = myData.SinhVien;
            caThi = new();
            chiTietCaThi = new();
            listDapAn = new List<int>();
            cau_da_chons = new List<int>();
            cau_da_chons_tagA = new List<int>();
            dsBaiThi_Update = new();
            customDeThis = new();
        }
        private async Task Start()
        {
            is_pause = false;
            await InitialConnectionHub();
            if (myData != null && myData.ChiTietCaThi != null)
            {
                chiTietCaThi = myData.ChiTietCaThi;
                await getDeThi(chiTietCaThi.MaDeThi);
                await modifyNhomCauHoi();
            }
            chiTietBaiThis = new();
            isDisableAudio = new List<bool>();
            // Cập nhật cho quản trị viên biết sinh viên đang thi
            if (isConnectHub() && chiTietCaThi != null && chiTietCaThi.MaCaThi != null)
                await sendMessage((int)chiTietCaThi.MaCaThi);
            // Nếu đã vào thi trước đó và treo máy tiếp tục thi thì chỉ lấy lại chi tiet bài thi, ko insert
            if (myData != null && myData.ChiTietCaThi != null && myData.ChiTietCaThi.DaThi)
            {
                await InsertChiTietBaiThi_DaVaoThiTruocDo();
                ProcessTiepTucThi();
            }
        }
        private void Time()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            DateTime currentTime = DateTime.Now;
            int tong_so_giay = 0;
            // cập nhật thời gian thi còn lại cho sinh viên nếu bị out
            int? thoi_gian_con_lai = (int?)thoiGianConLai();
            if (caThi != null && chiTietCaThi != null && myData != null && thoi_gian_con_lai != null)
            {
                tong_so_giay += (caThi.ThoiGianThi + chiTietCaThi.GioCongThem + myData.BonusTime - (int)thoi_gian_con_lai) * 60;
                //js?.InvokeVoidAsync("alert", "thoi gian con lai: " + thoi_gian_con_lai + ", tong_so_giay: " + tong_so_giay);
                tong_so_giay = (tong_so_giay > (caThi.ThoiGianThi * 60)) ? (caThi.ThoiGianThi * 60) : tong_so_giay;
            }
            displayTime = FormatTime(tong_so_giay);
            int so_giay_hien_tai = tong_so_giay;
            timer.Elapsed += async (sender, e) =>
            {
                so_giay_hien_tai--;
                // cứ mỗi n giây thì hệ thống tự động lưu bài của SV
                if(so_giay_hien_tai % GIAY_CAP_NHAT == 0)
                {
                    await UpdateChiTietBaiThi();
                }
                if (so_giay_hien_tai == 60)
                {
                    js?.InvokeVoidAsync("changeColorTime"); // đổi màu đồng hồ thành đỏ khi gần kết thúc
                }
                if (so_giay_hien_tai >= 0)
                {
                    displayTime = FormatTime(so_giay_hien_tai);
                    await InvokeAsync(StateHasChanged); // Cập nhật giao diện người dùng
                }
                else
                {
                    timer.Stop(); // Dừng timer khi countdown kết thúc
                    await ketThucThoiGianLamBai();
                }
            };
        }

        // Xử lí việc thí sinh bị out ra khi đang làm bài
        private void ProcessTiepTucThi()
        {
            DateTime? thoi_gian = chiTietBaiThis?.Max(p => p.NgayCapNhat);
            thoi_gian = thoi_gian?.AddSeconds(GIAY_CAP_NHAT);
            if(chiTietBaiThis != null && thoi_gian != null)
            {
                foreach(var item in chiTietBaiThis)
                {
                    if(item.CauTraLoi != null && customDeThis != null && cau_da_chons != null && cau_da_chons_tagA != null)
                    {
                        cau_da_chons.Add((int)item.CauTraLoi);
                        int STT = 1;
                        foreach (var chiTietDeThi in customDeThis)
                        {
                            if(chiTietDeThi.MaNhom == item.MaNhom && chiTietDeThi.MaCauHoi == item.MaCauHoi)
                                cau_da_chons_tagA.Add(STT);

                            // cập nhật lại danh sách sinh viên đã khoanh
                            if (listDapAn != null)
                                listDapAn.Add((int)item.CauTraLoi);
                            STT++;
                        }
                    }
                }
            }
        }
        private double? thoiGianConLai()
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
            hubConnection?.DisposeAsync();
        }

    }
}
