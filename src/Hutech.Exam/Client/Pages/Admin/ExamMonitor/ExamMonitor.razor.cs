using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;
using OfficeOpenXml;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Http.Connections;
using Hutech.Exam.Client.DAL;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        [Parameter][SupplyParameterFromQuery] public string? ma_ca_thi { get; set; }
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        [Inject] private IJSRuntime Js { get; set; } = default!;
        [Inject] private AdminHubService AdminHub { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        private CaThiDto? caThi;
        private List<ChiTietCaThiDto>? chiTietCaThis;
        private HubConnection? hubConnection;

        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private const string SUCCESS_RESETLOGIN = "Reset đăng nhập cho thí sinh thành công";
        private const string ERROR_RESETLOGIN = "Reset đăng nhập cho thí sinh thất bại";
        private const string SUCCESS_NOPBAI = "Nộp bài của thí sinh thành công";
        private const string ERROR_NOPBAI = "Nộp bài của thí sinh thất bại";
        private const string ALERT_ADDSV = "Thêm thí sinh được dùng cho việc khẩn cấp. Hãy đảm bảo MSSV thí sinh đã tồn tại trong hệ thống";
        private const string MISSINGINFO_RESETLOGIN = "Tính năng reset login không thể hoạt động khi thiếu thông tin mã lớp";
        private const string WAITING_DOWNLOADEXCEL = "Đang tải xuống file excel. Hãy chờ trong giây lát";

        private const string FAILED_RESETLOGIN = "Không thể reset đăng nhập cho thí sinh khi thí sinên không đăng nhập vào hệ thống thi";
        private const string FAILED_CONGGIO = "Không thể cộng giờ cho thí sinh khi thí sinh chưa thi";
        private const string FAILED_NOPBAI = "Không thể bắt thí sinh nộp bài khi thí sinh chưa thi hoặc thí sinh không đăng nhập";

        private const string UPDATE_CA_THI = "Ca thi này đã được cập nhật theo yêu cầu của quản trị viên";
        private const string DELETE_CA_THI = "Ca thi này đã được xóa theo yêu cầu của quản trị viên. Vui lòng rời khỏi trang ...";

        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                int maCaThi = -1;
                caThi = await SessionStorage.GetItemAsync<CaThiDto>("CaThi");
                bool isConvert = int.TryParse(ma_ca_thi, out maCaThi);
                if (ma_ca_thi == null || caThi == null && !isConvert || (isConvert && caThi != null && caThi.MaCaThi != maCaThi))
                {
                    Snackbar.Add(ERROR_PAGE, Severity.Error);
                    Nav.NavigateTo("/admin/control");
                    return;
                }
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                Nav.NavigateTo("/admin", true);
            }
            await Start();
            await base.OnInitializedAsync();
        }
        private async void OnClickResetLogin(SinhVienDto? sinhVien)
        {
            if (sinhVien != null && sinhVien.IsLoggedIn == true)
            {
                var parameters = new DialogParameters<Simple_Dialog>
                {
                    { x => x.ContentText, $"Thí sinh đăng nhập lần cuối vào lúc {sinhVien.LastLoggedIn}. Hãy cân nhắc thời gian trên và chắc chắn rằng sinh viên không gian lận" },
                    { x => x.ButtonText, "Reset" },
                    { x => x.Color, Color.Warning },
                    { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleResetLogin(sinhVien))   }
                };

                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

                await Dialog.ShowAsync<Simple_Dialog>("Đăng xuất cho thí sinh", parameters, options);

            }
            else
                Snackbar.Add(FAILED_RESETLOGIN, Severity.Error);
        }
        private async Task HandleResetLogin(SinhVienDto sinhVien)
        {
            if (sinhVien.MaLop == null)
            {
                Snackbar.Add(MISSINGINFO_RESETLOGIN, Severity.Error);
                return;
            }
            await ResetLoginAPI(sinhVien);
        }
        private async Task OnClickCongGioThem(ChiTietCaThiDto chiTietCaThi)
        {
            if(chiTietCaThi != null && chiTietCaThi.DaThi == false && chiTietCaThi.MaSinhVienNavigation != null && chiTietCaThi.MaSinhVienNavigation.IsLoggedIn == false)
            {
                Snackbar.Add(FAILED_CONGGIO, Severity.Error);
                return;
            }
            var parameters = new DialogParameters<CongGioDialog>
            {
                { x => x.chiTietCaThi, chiTietCaThi },
                { x => x.caThi, caThi }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<CongGioDialog>("Cộng giờ thi", parameters, options);
        }
        private async Task OnClickNopBai(ChiTietCaThiDto chiTietCaThi)
        {
            var parameters = new DialogParameters<Simple_Dialog>
                {
                    { x => x.ContentText, $"Nộp bài của thí sinh. Vui lòng chắc chắn thao tác này chỉ thực hiện khi thí sinh đang trong quá trình thi và đăng nhập. Nếu muốn tính điểm, chọn 'Check Điểm'" },
                    { x => x.ButtonText, "Nộp bài" },
                    { x => x.Color, Color.Warning },
                    { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleNopBai(chiTietCaThi))   }
                };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Nộp bài làm thí sinh", parameters, options);

        }
        private async Task HandleNopBai(ChiTietCaThiDto chiTietCaThi)
        {
            SinhVienDto? sinhVien = chiTietCaThi.MaSinhVienNavigation;
            if ((chiTietCaThi != null && chiTietCaThi.DaThi == false) || sinhVien == null || sinhVien.IsLoggedIn == false)
            {
                Snackbar.Add(FAILED_NOPBAI, Severity.Error);
                return;
            }   
            bool result = await NopBaiAPI(sinhVien);
            if (result)
                Snackbar.Add(SUCCESS_NOPBAI, Severity.Success);
            else
                Snackbar.Add(ERROR_NOPBAI, Severity.Error);
        }

        private async Task Refresh()
        {
            chiTietCaThis = await ChiTietCaThis_SelectBy_MaCaThiAPI(caThi?.MaCaThi ?? -1);
        }
        private async Task OnClickDownloadExcel()
        {
            if (chiTietCaThis != null)
            {
                Snackbar.Add(WAITING_DOWNLOADEXCEL, Severity.Info);
                var excelData = await GetExcelFileAPI(chiTietCaThis);
                if (excelData != null)
                {
                    var base64 = Convert.ToBase64String(excelData);
                    var fileName = $"Bảng điểm ca thi {caThi?.MaCaThi}.xlsx";

                    // Tạo link tải xuống
                    await Js.InvokeVoidAsync("downloadFile", fileName, base64);
                }
            }
        }

        private async Task Start()
        {
            chiTietCaThis = await ChiTietCaThis_SelectBy_MaCaThiAPI(caThi?.MaCaThi ?? -1);
            await CreateHubConnection();
        }

    }
}
