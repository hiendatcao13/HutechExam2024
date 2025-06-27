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


        private const string VERIFY_PASSWORD = "Vui lòng nhập mật khẩu cho ca thi";
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
            examBatchs = await ExamBatchs_GetAllAPI();
            subjects = await Subjects_GetAllAPI();

            await GetItemsInSessionStorageAsync();

            //await CreateHubConnection();
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

        private async Task OnClickExamSessionDetailAsync(CaThiDto examSession)
        {
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
            var parameters = new DialogParameters<Password_Dialog>
            {
                { x => x.ContentText, VERIFY_PASSWORD },
                { x => x.ButtonText, "OK" },
                { x => x.PlainPassword, examSession.MatMa },
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
