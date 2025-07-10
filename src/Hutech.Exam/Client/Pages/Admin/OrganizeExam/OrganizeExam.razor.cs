using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;
using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Pages.Admin.ManageExamSession;
using System.Security.Claims;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Shared.Enums;
using Hutech.Exam.Shared.DTO.Request.Audit;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private List<DotThiDto>? examBatchs = [];
        private List<ChiTietDotThiDto>? examBatchDetails = [];
        private List<CaThiDto>? examSessions = [];

        private string? name;
        private Guid userId;
        string roleName = string.Empty;

        private const string NO_CHOOSE_OBJECT = "Vui lòng chọn 1 đối tượng để tiếp tục!";
        private const string WAITING_DELETE = "Việc xóa thực thể sẽ tốn thời gian tùy thuộc vào độ phức tạp của dữ liệu. Vui lòng chờ...";
        private const string DELETE_DOTTHI_MESSAGE = "Bạn có chắc chắn muốn xóa đợt thi này không? Mối quan hệ phụ thuộc: CHITIETDOTTHI &rarr; CATHI &rarr; CHITIETCATHI &rarr; CHITIETBAITHI, AUDIOLISTENED";
        private const string DELETE_CATHI_MESSAGE = "Bạn có chắc chắn muốn xóa ca thi này không? Mối quan hệ phụ thuộc: CHITIETCATHI &rarr; CHITIETBAITHI, AUDIOLISTENED";
        private const string DELETE_CTDOTTHI_MESSAGE = "Bạn có chắc chắn muốn xóa chi tiết đợt thi này không? Mối quan hệ phụ thuộc: CATHI &rarr; CHITIETCATHI &rarr; CHITIETBAITHI, AUDIOLISTENED";
        private const string CONFIRM_APPROVE = "Bạn có chắc chắn muốn duyệt cho ca thi này không? Sau khi duyệt, ca thi sẽ không thể thay đổi được nữa. Nếu muốn thay đổi, bạn phải xóa và tạo lại ca thi mới.";

        #endregion

        #region Initial Methods

        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                Nav.NavigateTo("/admin", true);
            }
            //await CreateHubConnection();
            await GetItemsInSessionStorageAsync();
            await base.OnInitializedAsync();
        }

        private async Task StartAsync()
        {
            await GetIdentityUserName();
            (examBatchs, totalPages_ExamBatch, totalRecords_ExamBatch) = await ExamBatchs_GetAllAPI(currentPage_ExamBatch, rowsPerPage_ExamBatch);
            CreateFakeData_DT();
        }

        private async Task GetIdentityUserName()
        {
            var authState = AuthenticationState != null ? await AuthenticationState : null;
            if (authState != null && authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
            {
                name = authState.User.FindFirst(ClaimTypes.Name)?.Value;
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

        #endregion

        #region Fetch Methods

        private async Task FetchAllDataAsync()
        {
            if (selectedExamBatch != null)
            {
                await FetchExamBatchDetailAsync();
                await FetchExamSessionAsync();
            }
        }
        private async Task FetchExamSessionAsync()
        {
            if (selectedExamBatchDetail != null)
            {
                (examSessions, totalPages_ExamSession, totalRecords_ExamSession) = await ExamSessions_SelectBy_ExamBatchDetailId_PagedAPI(selectedExamBatchDetail.MaChiTietDotThi, currentPage_ExamSession, rowsPerPage_ExamSession);
                CreateFakeData_CT();
            }
        }

        private async Task FetchExamBatchDetailAsync()
        {
            if (selectedExamBatch != null)
            {
                (examBatchDetails, totalPages_ExamBatchDetail, totalRecords_ExamBatchDetail) = await ExamBatchDetailDetail_SelectBy_ExamBatchId_PagedAPI(selectedExamBatch.MaDotThi, currentPage_ExamBatchDetail, rowsPerPage_ExamBatchDetail);
                CreateFakeData_CTDT();
            }
        }

        #endregion

        #region SessionStorage Methods

        private async Task GetItemsInSessionStorageAsync()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataOE>("storedDataOE");
            await StartAsync();
            if (storedData != null)
            {
                selectedExamBatch = storedData.DotThi;
                selectedExamBatchDetail = storedData.ChiTietDotThi;
            }
            await FetchAllDataAsync();
        }

        private async Task SetItemsInSessionStorageAsync()
        {
            var selectedData = new StoredDataME
            {
                DotThi = selectedExamBatch,
                MonHoc = selectedExamBatchDetail?.MaLopAoNavigation.MaMonHocNavigation,
                LopAo = selectedExamBatchDetail?.MaLopAoNavigation,
                LanThi = selectedExamBatchDetail?.LanThi ?? 0
            };
            await SessionStorage.SetItemAsync("storedDataEM", selectedData);
        }
        private async Task SaveDataAsync()
        {
            var selectedData = new StoredDataOE
            {
                DotThi = selectedExamBatch,
                ChiTietDotThi = selectedExamBatchDetail
            };
            await SessionStorage.SetItemAsync("storedDataOE", selectedData);
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickAddExamBatchAsync()
        {
            var result = await OpenExamBatchDialogAsync(false);

            if (result != null && !result.Canceled && result.Data != null)
            {
                var newDotThi = (DotThiDto)result.Data;
                examBatchs?.Insert(0, newDotThi); // Thêm vào đầu danh sách
            }
        }


        private async Task OnClickEditExamBatchAsync()
        {
            var result = await OpenExamBatchDialogAsync(true);

            if (result != null && !result.Canceled && result.Data != null && examBatchs != null)
            {
                var newDotThi = (DotThiDto)result.Data;

                int index = examBatchs.FindIndex(dt => dt.MaDotThi == newDotThi.MaDotThi);
                if (index != -1)
                {
                    examBatchs[index] = newDotThi; // Cập nhật đợt thi trong danh sách
                    selectedExamBatch = examBatchs[index];
                }
            }
        }

        private async Task OnClickDeleteExamBatchAsync()
        {
            if (selectedExamBatch == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_DOTTHI_MESSAGE },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamBatchAsync(true))   },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamBatchAsync(false))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA ĐỢT THI", parameters, options);
        }

        private async Task HandleDeleteExamBatchAsync(bool isForce)
        {
            bool result = (isForce) ? await ExamBatch_ForceDeleteAPI(selectedExamBatch?.MaDotThi ?? -1) : await ExamBatch_DeleteAPI(selectedExamBatch?.MaDotThi ?? -1);
            if (result && selectedExamBatch != null)
            {
                Snackbar.Add(WAITING_DELETE, Severity.Warning);
                examBatchs?.Remove(selectedExamBatch);
                selectedExamBatch = null;
                examBatchDetails = [];
                examSessions = [];
            }
        }

        private async Task OnClickAddExamBatchDetailAsync()
        {
            var result = await OpenExamBatchDetailDialogAsync(false);

            if (result != null && !result.Canceled && result.Data != null && examBatchDetails != null)
            {
                var newChiTietDotThi = (ChiTietDotThiDto)result.Data;
                examBatchDetails.Insert(0, newChiTietDotThi); // Thêm vào đầu danh sách
            }
        }
        private async Task OnClickEditExamBatchDetailAsync()
        {
            var result = await OpenExamBatchDetailDialogAsync(true);

            if (result != null && !result.Canceled && result.Data != null && examBatchDetails != null)
            {
                var newChiTietDotThi = (ChiTietDotThiDto)result.Data;
                int index = examBatchDetails.FindIndex(ct => ct.MaChiTietDotThi == newChiTietDotThi.MaChiTietDotThi);
                if (index != -1)
                {
                    examBatchDetails[index] = newChiTietDotThi; // Cập nhật chi tiết đợt thi trong danh sách
                    selectedExamBatchDetail = examBatchDetails[index];
                }
            }
        }

        private async Task OnClickDeleteExamBatchDetailAsync()
        {
            if (selectedExamBatchDetail == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_CTDOTTHI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamBatchDetailAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamBatchDetailAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CHI TIẾT ĐỢT THI", parameters, options);
        }

        private async Task OnClickAddExamSessionAsync()
        {
            var result = await OpenExamSessionDialogAsync(false);
            if (result != null && !result.Canceled && result.Data != null && examSessions != null)
            {
                var newCaThi = (CaThiDto)result.Data;
                examSessions.Insert(0, newCaThi); // Thêm vào cuối danh sách
            }
        }

        private async Task OnClickEditExamSessionAsync()
        {
            if (selectedExamSession == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }

            var result = await OpenExamSessionDialogAsync(true);
            if (result != null && !result.Canceled && result.Data != null && examSessions != null)
            {
                var newCaThi = (CaThiDto)result.Data;
                int index = examSessions.FindIndex(ct => ct.MaCaThi == newCaThi.MaCaThi);
                if (index != -1)
                {
                    examSessions[index] = newCaThi; // Cập nhật ca thi trong danh sách
                    selectedExamSession = examSessions[index];
                }
            }
        }

        private async Task OnClickDeleteExamSessionAsync()
        {
            if (selectedExamSession == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_CATHI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamSessionAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamSessionAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CA THI", parameters, options);
        }

        private async Task OnClickShowExamBatchDetailAsync(CaThiDto examSession)
        {
            await SaveDataAsync();
            // set Ca Thi cho trang EM, ko tốn API lấy lại
            await SessionStorage.SetItemAsync("CaThi", examSession);
            // set cho trang Manage Exam 
            await SetItemsInSessionStorageAsync();
            Nav.NavigateTo($"admin/monitor?ma_ca_thi={examSession.MaCaThi}");
        }

        private void OnClickUpdateExamAsync(CaThiDto examSession)
        {
            Nav.NavigateTo($"/admin/approved-exam?ma_ca_thi={examSession.MaCaThi}");
            //var result = await OpenUpdateExamDialogAsync(caThi);
            //// lấy data là mã đề thi mới cho ca thi
            //if (result != null && result.Data != null && !result.Canceled && examSessions != null)
            //{
            //    int index = examSessions.FindIndex(ct => ct.MaCaThi == caThi.MaCaThi);
            //    if (index != -1)
            //    {
            //        examSessions[index].MaDeThi = (int)result.Data;
            //    }
            //}
        }

        private async Task OnClickUploadStudentListToExamSessionAsync()
        {

            var parameters = new DialogParameters<AddStudentExamSessionExcelDialog> { };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<AddStudentExamSessionExcelDialog>("THÊM DANH SÁCH SINH VIÊN VÀO CA THI", parameters, options);
        }

        private async Task OnClickApproveExamSession(CaThiDto examSession)
        {
            selectedExamSession = examSession;
            if (examSession.DaGanDe == false)
            {
                Snackbar.Add("Không thể duyệt ca thi chưa được gán đề thi!", Severity.Error);
                return;
            }

            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, CONFIRM_APPROVE },
                { x => x.ButtonText, "Xác nhận" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleApprove())   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Đăng xuất", parameters, options);

        }

        #endregion

        #region HandleOnClick Methods

        private async Task HandleDeleteExamBatchDetailAsync(bool isForce)
        {
            bool result = (isForce) ? await ExamBatchDetail_ForceDeleteAPI(selectedExamBatchDetail?.MaChiTietDotThi ?? -1) : await ExamBatchDetail_DeleteAPI(selectedExamBatchDetail?.MaChiTietDotThi ?? -1);
            if (result && selectedExamBatchDetail != null)
            {
                Snackbar.Add(WAITING_DELETE, Severity.Warning);
                examBatchDetails?.Remove(selectedExamBatchDetail);
                selectedExamBatchDetail = null;
                examSessions = [];
            }
        }

        private async Task HandleDeleteExamSessionAsync(bool isForce)
        {
            bool result = (isForce) ? await ExamSession_ForceDeleteAPI(selectedExamSession?.MaCaThi ?? -1) : await ExamSession_DeleteAPI(selectedExamSession?.MaCaThi ?? -1);
            if (result && selectedExamSession != null)
            {
                Snackbar.Add(WAITING_DELETE, Severity.Warning);
                examSessions?.Remove(selectedExamSession);
                selectedExamSession = null;
            }
        }

        private async Task HandleApprove()
        {
            var reason = await OpenAuditDialogAsync(KieuHanhDong.DuyetDeThi);
            if (reason != null && !reason.Canceled && reason.Data != null)
            {
                string jsonText = CreateActionHistory(KieuHanhDong.XoaThiSinh, "", reason.Data.ToString()!);
                var result = await ExamSession_UpdateApproveAPI(selectedExamSession!.MaCaThi, jsonText);
                if(result != null)
                {
                    int index = examSessions!.FindIndex(ct => ct.MaCaThi == result.MaCaThi);
                    if (index != -1)
                    {
                        examSessions[index] = result; // Cập nhật ca thi trong danh sách
                        selectedExamSession = examSessions[index];
                    }
                }
            }    
        }



        #endregion

        #region Dialog Methods

        private async Task<DialogResult?> OpenExamBatchDialogAsync(bool isEdit)
        {
            var parameters = new DialogParameters<ExamBatchDialog>
            {
                { x => x.ExamBatch, selectedExamBatch ?? new() },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<ExamBatchDialog>((isEdit) ? "SỬA ĐỢT THI" : "THÊM ĐỢT THI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenExamBatchDetailDialogAsync(bool isEdit)
        {
            if (selectedExamBatch == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return null;
            }
            var parameters = new DialogParameters<ExamBatchDetailDialog>
            {
                { x => x.ExamBatchName, selectedExamBatch.TenDotThi ?? "Không có DL tên"},
                { x => x.ExamBatchId , selectedExamBatch.MaDotThi },
                { x => x.IsEdit, isEdit },
                { x => x.ExamBatchDetail, selectedExamBatchDetail }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<ExamBatchDetailDialog>((isEdit) ? "SỬA CHI TIẾT ĐỢT THI" : "THÊM CHI TIẾT ĐỢT THI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenExamSessionDialogAsync(bool isEdit)
        {
            if (selectedExamBatchDetail == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return null;
            }
            var parameters = new DialogParameters<ExamSessionDialog>
            {
                { x => x.ExamBatchId, selectedExamBatchDetail.MaChiTietDotThi },
                { x => x.ExamBatchName, selectedExamBatch?.TenDotThi ?? "Không có DL tên"},
                { x => x.ExamClassroomName , selectedExamBatchDetail.MaLopAoNavigation.TenLopAo },
                { x => x.SubjectName, selectedExamBatchDetail.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc },
                { x => x.AttemptNumber, selectedExamBatchDetail.LanThi },
                { x => x.IsEdit, isEdit },
                { x => x.ExamSession, selectedExamSession }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<ExamSessionDialog>((isEdit) ? "SỬA CA THI" : "THÊM CA THI", parameters, options);
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

        //private async Task<DialogResult?> OpenUpdateExamDialogAsync(CaThiDto caThi)
        //{
        //    if (selectedExamBatchDetail == null)
        //    {
        //        Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
        //        return DialogResult.Cancel();
        //    }
        //    var parameters = new DialogParameters<EditExamDialog>
        //    {
        //        { x => x.ExamBatchDetailId, selectedExamBatchDetail.MaChiTietDotThi },
        //        { x => x.ExamBatchName, selectedExamBatch?.TenDotThi ?? "Không có DL tên"},
        //        { x => x.ExamRoomName , selectedExamBatchDetail.MaLopAoNavigation.TenLopAo },
        //        { x => x.SubjectName, selectedExamBatchDetail.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc },
        //        { x => x.AttemptNumber, selectedExamBatchDetail.LanThi },
        //        { x => x.ExamSession, caThi }
        //    };
        //    var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
        //    var dialog = await Dialog.ShowAsync<EditExamDialog>("UPDATE ĐỀ THI", parameters, options);
        //    return await dialog.Result;
        //}

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
            if (!string.IsNullOrWhiteSpace(selectedExamSession!.LichSuHoatDong))
            {
                var history = System.Text.Json.JsonSerializer.Deserialize<List<LichSuHoatDong>>(selectedExamSession.LichSuHoatDong);
                if (history != null && history.Count > 0)
                {
                    result = history;
                }
            }

            result.Add(actionHistory);
            return JsonSerializer.Serialize(result);
        }

        private void PadEmptyRows(List<DotThiDto>? newDotThis)
        {
            if (newDotThis == null || newDotThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_ExamBatch * rowsPerPage_ExamBatch;
            if (examBatchs != null && examBatchs.Count != 0)
            {
                for (int i = 0; i < newDotThis.Count; i++)
                {

                    examBatchs[startRow++] = newDotThis[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<ChiTietDotThiDto>? newChiTietDotThis)
        {
            if (newChiTietDotThis == null || newChiTietDotThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_ExamBatchDetail * rowsPerPage_ExamBatchDetail;
            if (examBatchDetails != null && examBatchDetails.Count != 0)
            {
                for (int i = 0; i < newChiTietDotThis.Count; i++)
                {

                    examBatchDetails[startRow++] = newChiTietDotThis[i];
                }
            }
            StateHasChanged();

        }

        private void PadEmptyRows(List<CaThiDto>? newCaThis)
        {
            if (newCaThis == null || newCaThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_ExamSession * rowsPerPage_ExamSession;
            if (examSessions != null && examSessions.Count != 0)
            {
                for (int i = 0; i < newCaThis.Count; i++)
                {

                    examSessions[startRow++] = newCaThis[i];
                }
            }
            StateHasChanged();

        }

        private void CreateFakeData_DT()
        {
            if (examBatchs != null && examBatchs.Count != 0)
            {
                int count_fake = totalRecords_ExamBatch - examBatchs.Count;
                bool isFake = totalRecords_ExamBatch > examBatchs.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        examBatchs.Add(new DotThiDto());
                }
            }
        }

        private void CreateFakeData_CTDT()
        {
            if (examBatchDetails != null && examBatchDetails.Count != 0)
            {
                int count_fake = totalRecords_ExamBatchDetail - examBatchDetails.Count;
                bool isFake = totalRecords_ExamBatchDetail > examBatchDetails.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        examBatchDetails.Add(new ChiTietDotThiDto());
                }
            }
        }

        private void CreateFakeData_CT()
        {
            if (examSessions != null && examSessions.Count != 0)
            {
                int count_fake = totalRecords_ExamSession - examSessions.Count;
                bool isFake = totalRecords_ExamSession > examSessions.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        examSessions.Add(new CaThiDto());
                }
            }
        }

        #endregion
    }
}
