using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Pages.Admin.ManageExamSession.Dialog;
using Hutech.Exam.Client.Components.Dialogs;
using System.Text.Json;
using Hutech.Exam.Shared.DTO.Request.Audit;
using System.Security.Claims;

namespace Hutech.Exam.Client.Pages.Admin.ManageExamSession
{
    public partial class ManageExamSession
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private ChiTietDotThiDto? examBatchDetail;

        private List<CaThiDto>? examSessions;

        private List<DotThiDto>? examBatchs; // combobox

        private List<MonHocDto>? subjects; // combobox

        private List<LopAoDto>? examRooms; // combobox

        private readonly List<int> attemptNumber = [1, 2, 3, 4, 5];

        private string roleName = string.Empty;


        private const string VerifyPassMessage = "Vui lòng nhập mật khẩu cho ca thi";
        private const string NotAprrovedMessage = "Ca thi chưa được duyệt. Vui lòng liên hệ phòng trung tâm CNTT";
        private const string NotContainsExamMessage = "Ca thi chưa được gán đề thi. Vui lòng liên hệ phòng khảo thí";
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
            await StartAsync();
            await base.OnInitializedAsync();
        }

        private async Task StartAsync()
        {
            await GetRoleName();
            examBatchs = await ExamBatchs_GetAllAPI();
            subjects = await Subjects_GetAllAPI();

            await GetItemsInSessionStorageAsync();

            //await CreateHubConnection();
        }

        private async Task GetRoleName()
        {
            var authState = AuthenticationState != null ? await AuthenticationState : null;
            if (authState != null && authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
            {
                roleName = authState.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
            }
        }

        #endregion

        #region Fetch Methods
        private async Task FetchExamSessionAsync()
        {
            if (selectedExamBatch != null && selectedExamRoom != null && selectedAttemptNumber != 0 && selectedSubject != null)
            {
                examBatchDetail = await ExamBatchDetails_SelectBy_ExamBatchId_ExamRoomId_AttempNumberAPI(selectedExamBatch.MaDotThi, selectedExamRoom.MaLopAo, selectedAttemptNumber);
                if (examBatchDetail != null)
                {
                    (examSessions, totalPages, totalRecords) = await ExamSessions_SelectBy_ExamBatchDetailId_PagedAPI(examBatchDetail.MaChiTietDotThi, currentPage, rowsPerPage);
                    CreateFakeData();

                    await SetItemsInSessionStorageAsync();
                }
            }
            else
                examSessions = [];
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickEditExamSessionAsync(CaThiDto examSession)
        {
            if(examSession.DaDuyet == false)
            {
                Snackbar.Add(NotAprrovedMessage, Severity.Warning);
                return;
            }   
            if(examSession.DaGanDe == false)
            {
                Snackbar.Add(NotContainsExamMessage, Severity.Warning);
                return;
            }    
            if (!await VerifyPassword(examSession))
            {
                return;
            }

            var result = await OpenExamSessionStatusDialogAsync(examSession);


            if (result != null && !result.Canceled && result.Data != null)
            {
                UpdateExamSession((CaThiDto)result.Data);
            }
        }

        private async Task OnClickViewHistoryAsync(CaThiDto examSession)
        {
            if (string.IsNullOrWhiteSpace(examSession!.LichSuHoatDong))
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


        private async Task OnClickExamSessionDetailAsync(CaThiDto examSession)
        {
            if (examSession.DaDuyet == false)
            {
                Snackbar.Add(NotAprrovedMessage, Severity.Warning);
                return;
            }
            if (examSession.DaGanDe == false)
            {
                Snackbar.Add(NotContainsExamMessage, Severity.Warning);
                return;
            }
            if (!await VerifyPassword(examSession))
            {
                return;
            }

            await SessionStorage.SetItemAsync("CaThi", examSession);

            Nav.NavigateTo($"admin/monitor?ma_ca_thi={examSession.MaCaThi}");

        }

        #endregion

        #region Dialog Methods

        private async Task<DialogResult?> OpenPasswordDialogAsync(CaThiDto examSession)
        {
            Func<string, Task<bool>> verifyDelegate = async (password) =>
            {
                return await VerifyPasswordAPI(examSession.MaCaThi, password);
            };

            var parameters = new DialogParameters<Password_Dialog>
            {
                { x => x.ContentText, VerifyPassMessage },
                { x => x.ButtonText, "OK" },
                { x => x.OnVerifyPassword, (Func<string, Task<bool>>)verifyDelegate },
                { x => x.RecognizeCode, $"ExamSession{examSession.MaCaThi}" },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<Password_Dialog>("XÁC THỰC", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenExamSessionStatusDialogAsync(CaThiDto examSession)
        {

            var parameters = new DialogParameters<ExamSessionStatusDialog>
            {
                { x => x.ExamSession, examSession },
            };

            var options = new DialogOptions { CloseButton = false, MaxWidth = MaxWidth.ExtraExtraLarge, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<ExamSessionStatusDialog>("THÔNG TIN CA THI", parameters, options);
            return await dialog.Result;
        }

        #endregion

        #region SessionStorage Methods
        private async Task SetItemsInSessionStorageAsync()
        {
            var selectedData = new StoredDataME
            {
                DotThi = selectedExamBatch,
                MonHoc = selectedSubject,
                LopAo = selectedExamRoom,
                LanThi = selectedAttemptNumber
            };
            await SessionStorage.SetItemAsync("storedDataMC", selectedData);
        }
        private async Task GetItemsInSessionStorageAsync()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataME>("storedDataMC");
            if (storedData != null)
            {
                selectedExamBatch = storedData.DotThi;
                selectedSubject = storedData.MonHoc;
                selectedExamRoom = storedData.LopAo;
                selectedAttemptNumber = storedData.LanThi;
            }
            await FetchExamSessionAsync();
        }

        #endregion

        #region Other Methods

        private async Task<bool> VerifyPassword(CaThiDto examSession)
        {
            var result = await OpenPasswordDialogAsync(examSession);

            if (result != null && !result.Canceled && result.Data != null)
            {
                return Convert.ToBoolean(result.Data);
            }

            return false;
        }


        private void UpdateExamSession(CaThiDto caThi)
        {
            if (caThi == null || examSessions == null) return;

            var index = examSessions.FindIndex(p => p.MaCaThi == caThi.MaCaThi);
            if (index != -1)
            {
                examSessions[index] = caThi;
            }
        }

        private void CreateFakeData()
        {
            if (examSessions != null && examSessions.Count != 0)
            {
                int count_fake = totalRecords - examSessions.Count;
                bool isFake = totalRecords > examSessions.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        examSessions.Add(new CaThiDto());
                }
            }
        }

        private void PadEmptyRows(List<CaThiDto>? newCaThi)
        {
            if (newCaThi == null || newCaThi.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage * rowsPerPage;
            if (examSessions != null && examSessions.Count != 0)
            {
                for (int i = 0; i < newCaThi.Count; i++)
                {

                    examSessions[startRow++] = newCaThi[i];
                }
            }
            StateHasChanged();

        }

        #endregion
    }
}
