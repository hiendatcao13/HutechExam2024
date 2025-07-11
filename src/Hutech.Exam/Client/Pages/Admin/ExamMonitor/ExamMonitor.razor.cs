using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;
using Microsoft.JSInterop;
using Hutech.Exam.Client.DAL;
using Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog;
using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.Enums;
using Hutech.Exam.Shared.DTO.Request.Audit;
using System.Security.Claims;
using System.Text.Json;
using Hutech.Exam.Client.Pages.Admin.ManageExamSession;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor : IAsyncDisposable
    {
        #region Private Fields
        [Parameter][SupplyParameterFromQuery] public string? ma_ca_thi { get; set; }

        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [CascadingParameter] Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [Inject] private IJSRuntime Js { get; set; } = default!;

        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private CaThiDto? examSession;
        private List<ChiTietCaThiDto>? examSessionDetails = [];
        private HubConnection? hubConnection;

        private string? name;
        private Guid userId;
        private string roleName = string.Empty;
        private StoredDataME? storedDataME;

        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private const string SUCCESS_NOPBAI = "Nộp bài của thí sinh thành công";
        private const string ERROR_NOPBAI = "Nộp bài của thí sinh thất bại";
        private const string ALERT_ADDSV = "Thêm thí sinh được dùng cho việc khẩn cấp. Hãy đảm bảo MSSV thí sinh đã tồn tại trong hệ thống";
        private const string WAITING_DOWNLOADEXCEL = "Đang tải xuống file excel. Hãy chờ trong giây lát";
        private const string ERROR_MOCKTEST = "Không thể xem bài của thí sinh khi thí sinh chưa nộp bài";

        private const string FAILED_RESETLOGIN = "Không thể reset đăng nhập cho thí sinh khi thí sinh không đăng nhập vào hệ thống thi";
        private const string FAILED_CONGGIO = "Không thể cộng giờ cho thí sinh khi thí sinh chưa thi hoặc đã thi";
        private const string FAILED_NOPBAI = "Không thể bắt thí sinh nộp bài khi thí sinh chưa thi hoặc thí sinh không đăng nhập";

        private const string UPDATE_CA_THI = "Ca thi này đã được cập nhật theo yêu cầu của quản trị viên";
        private const string DELETE_CA_THI = "Ca thi này đã được xóa theo yêu cầu của quản trị viên. Vui lòng rời khỏi trang ...";

        private const string CONFIRM_DELETE_STUDENT = "Bạn có chắc chắn muốn xóa sinh viên khỏi ca này không? Mối quan hệ phụ thuộc: CHITIETBAITHI, AUDIOLISTENED ";
        private const string WAITING_DELETE = "Việc xóa thực thể sẽ tốn thời gian tùy thuộc vào độ phức tạp của dữ liệu. Vui lòng chờ...";
        #endregion

        #region Initial Methods

        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                examSession = await SessionStorage.GetItemAsync<CaThiDto>("CaThi");
                bool isConvert = int.TryParse(ma_ca_thi, out int maCaThi);
                if (ma_ca_thi == null || examSession == null && !isConvert || (isConvert && examSession != null && examSession.MaCaThi != maCaThi))
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
            await StartAsync();
            await base.OnInitializedAsync();
        }

        private async Task StartAsync()
        {
            await GetIdentityUserName();
            CreateSchedule();
            (examSessionDetails, totalRecords, totalPages) = await ExamSessionDetails_SelectBy_ExamSessionId_PagedAPI(examSession?.MaCaThi ?? -1, currentPage, rowsPerPage);
            CreateFakeData();
            storedDataME = await SessionStorage.GetItemAsync<StoredDataME>("storedDataME");

            await CreateHubConnectionAsync();
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickResetLoginAsync(SinhVienDto? sinhVien)
        {
            if (sinhVien != null && sinhVien.IsLoggedIn == true)
            {
                var parameters = new DialogParameters<Simple_Dialog>
                {
                    { x => x.ContentText, $"Thí sinh đăng nhập lần cuối vào lúc {sinhVien.LastLoggedIn}. Hãy cân nhắc thời gian trên và chắc chắn rằng sinh viên không gian lận" },
                    { x => x.ButtonText, "Reset" },
                    { x => x.Color, Color.Warning },
                    { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleResetLoginAsync(sinhVien))   }
                };

                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

                await Dialog.ShowAsync<Simple_Dialog>("RESET ĐĂNG NHẬP", parameters, options);

            }
            else
                Snackbar.Add(FAILED_RESETLOGIN, Severity.Error);
        }

        private async Task OnClickAddTimeAsync(ChiTietCaThiDto chiTietCaThi)
        {
            var result = await OpenAddTimeDialogAsync(chiTietCaThi);
            if (result != null && !result.Canceled && result.Data != null && examSessionDetails != null)
            {
                var newChiTietCaThi = (ChiTietCaThiDto)result.Data;
                int index = examSessionDetails.FindIndex(p => p.MaChiTietCaThi == newChiTietCaThi.MaChiTietCaThi);
                examSessionDetails[index] = newChiTietCaThi;

                var sinhVien = chiTietCaThi.MaSinhVienNavigation;
                //cập nhật audit cho ca thi
                var jsonText = CreateActionHistory(KieuHanhDong.CongGioChoThiSinh, $"MSSV {sinhVien?.MaSoSinhVien} thí sinh {sinhVien?.HoVaTenLot} {sinhVien?.TenSinhVien}", newChiTietCaThi.LyDoCong!);
                if (await ExamSession_UpdateAudit(examSession?.MaCaThi ?? -1, jsonText))
                {
                    await UpdateViewHistory(jsonText);
                }
            }
        }

        private async Task OnClickSubmitAsync(ChiTietCaThiDto chiTietCaThi)
        {
            SinhVienDto? sinhVien = chiTietCaThi.MaSinhVienNavigation;
            if ((chiTietCaThi != null && chiTietCaThi.DaThi == false) || chiTietCaThi == null || sinhVien == null || sinhVien.IsLoggedIn == false)
            {
                Snackbar.Add(FAILED_NOPBAI, Severity.Error);
                return;
            }
            var parameters = new DialogParameters<Simple_Dialog>
                {
                    { x => x.ContentText, $"Nộp bài của thí sinh. Vui lòng chắc chắn thao tác này chỉ thực hiện khi thí sinh đang trong quá trình thi và đăng nhập. Nếu muốn tính điểm, chọn 'Check Điểm'" },
                    { x => x.ButtonText, "Nộp bài" },
                    { x => x.Color, Color.Warning },
                    { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleSubmitAsync(chiTietCaThi))   }
                };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Nộp bài làm thí sinh", parameters, options);

        }

        private async Task OnClickDownloadExcelAsync()
        {
            if (examSessionDetails != null && examSession != null && storedDataME != null)
            {
                Snackbar.Add(WAITING_DOWNLOADEXCEL, Severity.Info);
                var excelData = await GetExcelFileAPI(examSession.MaCaThi, new Shared.DTO.Request.CaThi.CaThiExportFileRequest { 
                    CaThi = examSession, 
                    ChiTietCaThis = examSessionDetails, 
                    MonHoc = storedDataME.MonHoc!, 
                    DotThi = storedDataME.DotThi!, 
                    TenPhongThi = storedDataME.LopAo!.TenLopAo!
                });
                if (excelData != null)
                {
                    var base64 = Convert.ToBase64String(excelData);
                    var fileName = $"Bảng điểm ca thi {examSession?.MaCaThi}.xlsx";

                    // Tạo link tải xuống
                    await Js.InvokeVoidAsync("downloadFile", fileName, base64);
                }
            }
        }

        private async Task OnClickDownloadPdfAsync()
        {
            if (examSessionDetails != null && examSession != null && storedDataME != null)
            {
                Snackbar.Add("Đang tạo file PDF...", Severity.Info);
                var pdfData = await GetPdfFileAPI(examSession.MaCaThi, new Shared.DTO.Request.CaThi.CaThiExportFileRequest
                {
                    CaThi = examSession,
                    ChiTietCaThis = examSessionDetails,
                    MonHoc = storedDataME.MonHoc!,
                    DotThi = storedDataME.DotThi!,
                    TenPhongThi = storedDataME.LopAo!.TenLopAo!
                });

                if (pdfData != null)
                {
                    var base64 = Convert.ToBase64String(pdfData);
                    var fileName = $"Bảng điểm ca thi {examSession?.MaCaThi}.pdf";

                    // Tạo link tải xuống
                    await Js.InvokeVoidAsync("downloadFile", fileName, base64);
                }
            }
        }

        private async Task OnClickRefreshAsync()
        {
            searchString = string.Empty;
            (examSessionDetails, totalRecords, totalPages) = await ExamSessionDetails_SelectBy_ExamSessionId_PagedAPI(examSession?.MaCaThi ?? -1, currentPage, rowsPerPage);
            examSession = await ExamSession_SelectOneAPI(examSession!.MaCaThi);
        }

        private async Task OnClickViewExamSubmissionDetailAsync(ChiTietCaThiDto chiTietCaThi)
        {
            if (chiTietCaThi.Diem == -1 || !chiTietCaThi.DaHoanThanh)
            {
                Snackbar.Add(ERROR_MOCKTEST, Severity.Error);
                return;
            }

            await Js.InvokeVoidAsync("openInNewTab", $"/monitor/mocktest?ma_chi_tiet_ca_thi={chiTietCaThi.MaChiTietCaThi}");
        }

        private async Task OnClickViewHistoryAsync()
        {
            if(string.IsNullOrWhiteSpace(examSession!.LichSuHoatDong))
            {
                Snackbar.Add("Không có lịch sử hoạt động nào để hiển thị", Severity.Warning);
                return;
            }    

            var parameters = new DialogParameters<ViewHistory_Dialog>
            {
                { x => x.HistoryVersions, JsonSerializer.Deserialize<List<LichSuHoatDong>>(examSession.LichSuHoatDong) },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<ViewHistory_Dialog>("XEM LỊCH SỬ", parameters, options);
        }

        private async Task OnClickDeleteExamSessionDetailAsync(ChiTietCaThiDto examSessionDetail)
        {
            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, CONFIRM_DELETE_STUDENT },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamSessionDetailAsync(examSessionDetail, true))   },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamSessionDetailAsync(examSessionDetail, false))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CHI TIẾT CA THI", parameters, options);
        }

        private async Task HandleDeleteExamSessionDetailAsync(ChiTietCaThiDto examSessionDetail, bool isForce)
        {
            // nếu ca thi đó chưa được kích hoạt lần nào và là phòng đào tạo, admin thì không cần audit
            if(examSession != null && examSession.ThoiGianKichHoat == null && examSession.KichHoat == false && (roleName.Contains(KieuVaiTro.DaoTao.ToString()) || roleName.Contains(KieuVaiTro.Admin.ToString())))
            {
                bool result = (isForce) ? await ExamSessionDetail_ForceDeleteAPI(examSessionDetail.MaChiTietCaThi) : await ExamSessionDetail_DeleteAPI(examSessionDetail.MaChiTietCaThi);
                if (result)
                {
                    Snackbar.Add(WAITING_DELETE, Severity.Warning);
                    examSessionDetails?.Remove(examSessionDetail);
                }
                return;
            }

            var reason = await OpenAuditDialogAsync(KieuHanhDong.XoaThiSinh);
            if (reason != null && !reason.Canceled && reason.Data != null)
            {
                string jsonText = CreateActionHistory(KieuHanhDong.XoaThiSinh, $"MSSV {examSessionDetail.MaSinhVienNavigation?.MaSoSinhVien} thí sinh {examSessionDetail.MaSinhVienNavigation?.HoVaTenLot} {examSessionDetail.MaSinhVienNavigation?.TenSinhVien}", reason.Data.ToString()!);
                if (await ExamSession_UpdateAudit(examSession?.MaCaThi ?? -1, jsonText))
                {
                    await UpdateViewHistory(jsonText);
                }

                bool result = (isForce) ? await ExamSessionDetail_ForceDeleteAPI(examSessionDetail.MaChiTietCaThi) : await ExamSessionDetail_DeleteAPI(examSessionDetail.MaChiTietCaThi);
                if (result)
                {
                    Snackbar.Add(WAITING_DELETE, Severity.Warning);
                    examSessionDetails?.Remove(examSessionDetail);
                }
            }
        }

        #endregion

        #region HandleOnClick Methods
        private async Task HandleResetLoginAsync(SinhVienDto sinhVien)
        {
            // Khi ca thi kích hoạt, hoạt động reset cần phải ghi audit
            if (examSession!.KichHoat)
            {
                var reason = await OpenAuditDialogAsync(KieuHanhDong.ResetDangNhap);
                if (reason != null && !reason.Canceled && reason.Data != null)
                {
                    string jsonText = CreateActionHistory(KieuHanhDong.ResetDangNhap, $"MSSV {sinhVien.MaSoSinhVien} thí sinh {sinhVien.HoVaTenLot} {sinhVien.TenSinhVien}", reason.Data.ToString()!);

                    var result = await ResetLoginAPI(sinhVien.MaSinhVien);
                    if (result && examSessionDetails != null)
                    {
                        int index = examSessionDetails.FindIndex(k => k.MaSinhVien == sinhVien.MaSinhVien);
                        if (index != -1)
                        {
                            examSessionDetails[index].MaSinhVienNavigation!.IsLoggedIn = false;
                            examSessionDetails[index].MaSinhVienNavigation!.LastLoggedIn = DateTime.Now;
                        }

                        if (await ExamSession_UpdateAudit(examSession?.MaCaThi ?? -1, jsonText))
                        {
                            await UpdateViewHistory(jsonText);
                        }
                    }

                }
            }

        }

        private async Task HandleSubmitAsync(ChiTietCaThiDto chiTietCaThi)
        {
            SinhVienDto? sinhVien = chiTietCaThi.MaSinhVienNavigation;
            bool result = await SubmitAPI(sinhVien?.MaSinhVien ?? -1);
            if (result)
                Snackbar.Add(SUCCESS_NOPBAI, Severity.Success);
            else
                Snackbar.Add(ERROR_NOPBAI, Severity.Error);
        }

        #endregion

        #region Dialog Methods

        private async Task<DialogResult?> OpenAddTimeDialogAsync(ChiTietCaThiDto chiTietCaThi)
        {
            if (chiTietCaThi.DaThi == false)
            {
                Snackbar.Add(FAILED_CONGGIO, Severity.Error);
                return null;
            }

            if(chiTietCaThi.ThoiDiemCong != null || chiTietCaThi.GioCongThem != 0)
            {
                Snackbar.Add("Không thể cộng giờ nhiều lần khi đã thêm trước đó", Severity.Error);
                return null;
            }   
            
            var parameters = new DialogParameters<AddTimeDialog>
            {
                { x => x.ChiTietCaThi, chiTietCaThi },
                { x => x.CaThi, examSession }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<AddTimeDialog>("CỘNG GIỜ THI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenAuditDialogAsync(KieuHanhDong kieuHanhDong)
        {
            var parameters = new DialogParameters<Audit_Dialog>
            {
                { x => x.Action, kieuHanhDong },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<Audit_Dialog>("LỊCH SỬ HOẠT ĐỘNG", parameters, options);
            return await dialog.Result;
        }

        #endregion

        #region Other Methods

        private string CreateActionHistory(KieuHanhDong kieuHanhDong, string chiTiet, string lyDo)
        {
            var updateHistory = new LichSuHoatDong()
            {
                HanhDong = kieuHanhDong,
                ChiTiet = chiTiet,
                UserId = userId,
                NguoiThucHien = name ?? string.Empty,
                LyDo = lyDo
            };

            var jsonText = ConvertActionHistory(updateHistory);
            return jsonText;
        }

        private string ConvertActionHistory(LichSuHoatDong actionHistory)
        {
            List<LichSuHoatDong> result = [];
            if(!string.IsNullOrWhiteSpace(examSession!.LichSuHoatDong))
            {
                var history = System.Text.Json.JsonSerializer.Deserialize<List<LichSuHoatDong>>(examSession.LichSuHoatDong);
                if (history != null && history.Count > 0)
                {
                    result = history;
                }
            }

            result.Add(actionHistory);
            return JsonSerializer.Serialize(result);
        }

        private async Task GetIdentityUserName()
        {
            var authState = AuthenticationState != null ? await AuthenticationState : null;
            if (authState != null && authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
            {
                name = await SessionStorage.GetItemAsStringAsync("Name");
                Guid.TryParse(authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
                foreach (var claim in authState.User.Claims)
                {
                    if (claim.Type == ClaimTypes.Role)
                    {
                        roleName += claim.Value + ",";
                    }
                }
            }
        }

        private void CreateFakeData()
        {
            if (examSessionDetails != null && examSessionDetails.Count != 0)
            {
                int count_fake = totalRecords - examSessionDetails.Count;
                bool isFake = totalRecords > examSessionDetails.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        examSessionDetails.Add(new ChiTietCaThiDto());
                }
            }
        }

        private void PadEmptyRows(List<ChiTietCaThiDto> newChiTietCaThi)
        {
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage * rowsPerPage;
            if (examSessionDetails != null && examSessionDetails.Count != 0)
            {
                for (int i = 0; i < newChiTietCaThi.Count; i++)
                {
                    examSessionDetails[startRow++] = newChiTietCaThi[i];
                }
            }
            StateHasChanged();

        }

        private async Task UpdateViewHistory(string jsonText)
        {
            examSession!.LichSuHoatDong = jsonText;
            await SessionStorage.SetItemAsync("CaThi", examSession);
        }

        public async ValueTask DisposeAsync()
        {
            await AdminHub.DisconnectionAsync();
        }

        #endregion

    }
}
