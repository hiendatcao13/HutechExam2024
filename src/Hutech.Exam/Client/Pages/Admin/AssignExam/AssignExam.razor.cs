using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;
using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Client.Pages.Admin.AssignExam.Dialog;

namespace Hutech.Exam.Client.Pages.Admin.AssignExam
{
    public partial class AssignExam
    {
        #region Private Fields

        [Parameter][SupplyParameterFromQuery] public string? ma_ca_thi { get; set; }
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [Inject] private IJSRuntime Js { get; set; } = default!;


        CaThiDto examSession = new();

        List<MonHocDto>? subjects = [];
        MonHocDto? selectedSubject;

        List<DeThiDto>? exams = [];
        DeThiDto? selectedExam;

        // mockAPI đề thi mới
        private List<DeThiDto> selectNewExams = [];

        List<DeThiDto> selectedExams = [];


        private const string NO_SELECT_OBJECT_DETHI = "Vui lòng chọn đề thi";
        private const string NO_SELECT_OBJECT_MONHOC = "Vui lòng chọn môn học";
        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private const string DELETE_DETHI_MESSAGE = "Bạn có chắc chắn muốn xóa đề thi này không? Chỉ có thể xóa an toàn";

        #endregion

        #region Initial Methods
        protected async override Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                bool isConvert = int.TryParse(ma_ca_thi, out int maCaThi);
                if (!isConvert)
                {
                    Snackbar.Add(ERROR_PAGE, Severity.Error);
                    Nav.NavigateTo("/admin/control");
                    return;
                }

                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                examSession = await ExamSession_SelectOneAPI(maCaThi);
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
            await FetchSubjectAsync();
        }

        #endregion

        #region Fetch Methods
        private async Task FetchSubjectAsync()
        {
            (subjects, totalPages_Subject, totalRecords_Subject) = await Subjects_GetAll_PagedAPI(currentPage_Subject, rowsPerPage_Subject);
            CreateFakeData_MonHoc();

            selectedSubject = null;
            selectedExam = null;
        }

        private async Task FetchExamAsync()
        {
            if (selectedSubject != null)
            {
                (exams, totalPages_Exam, totalRecords_Exam) = await Exams_SelectBy_SubjectId_PagedAPI(selectedSubject.MaMonHoc, currentPage_Exam, rowsPerPage_Exam);
                CreateFakeData_DeThi();
            }

            selectedExam = null;
        }
        #endregion

        #region OnClick Methods

        private async Task OnClickAddExamAsync()
        {
            if(selectedSubject == null)
            {
                Snackbar.Add(NO_SELECT_OBJECT_MONHOC, Severity.Warning);
                return;
            }    
            var result = await OpenNewExamDialogAsync();
            if(result != null && !result.Canceled && result.Data != null)
            {
                // async dữ liệu mới thêm đề vào
                await FetchExamAsync();
            }    
        }

        private async Task OnClickEditExamAsync()
        {
            var result = await OpenEditExamDialogAsync();
            if (result != null && !result.Canceled && subjects != null && result.Data != null)
            {
                var newdeThi = (DeThiDto)result.Data;
                if (exams != null && selectedExam != null)
                {
                    int index = exams.FindIndex(m => m.MaDeThi == newdeThi.MaDeThi);
                    if (index != -1)
                    {
                        exams[index] = newdeThi;
                        selectedExam = newdeThi;
                    }
                }
            }
        }

        private async Task OnClickDeleteExamAsync()
        {
            if (selectedExam == null)
            {
                Snackbar.Add(NO_SELECT_OBJECT_DETHI, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_DETHI_MESSAGE },
                { x => x.IsOnlySafeDetlet, true },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamAsync())   },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA ĐỀ THI", parameters, options);
        }

        private void OnClickChooseExam(bool value, DeThiDto deThi)
        {
            deThi.DaChon = value;
            if(value)
            {
                selectedExams.Add(deThi);
                return;
            }
            selectedExams.Remove(deThi);
        }

        private async Task OnClickAssignExamAsync()
        {
            if (selectedSubject == null)
            {
                Snackbar.Add(NO_SELECT_OBJECT_MONHOC, Severity.Warning);
                return;
            }
            if (selectedExams.Count == 0)
            {
                Snackbar.Add(NO_SELECT_OBJECT_DETHI, Severity.Error);
                return;
            }

            var result = await OpenAssignExamDialogAsync();

            if(result != null && !result.Canceled && result.Data != null)
            {

            }    
        }

        #endregion

        #region HandleOnClick Methods

        private async Task HandleDeleteExamAsync()
        {
            if (selectedExam != null)
            {
                var result = await Exam_ForceDeleteAPI(selectedExam.MaDeThi);

                if (result)
                {
                    exams?.Remove(selectedExam);
                    selectedExam = null;
                }
            }
        }

        #endregion

        #region Dialog Methods

        private async Task<DialogResult?> OpenNewExamDialogAsync()
        {
            if(selectedSubject == null)
            {
                Snackbar.Add("Vui lòng chọn môn học trước khi thêm đề thi", Severity.Warning);
                return null;
            }    

            var parameters = new DialogParameters<NewExamDialog>
            {
                { x => x.ExamSession, examSession },
                { x => x.Subject, selectedSubject }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<NewExamDialog>("THÊM ĐỀ THI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenEditExamDialogAsync()
        {
            var parameters = new DialogParameters<EditExamDialog>
            {
                { x => x.Exam, selectedExam }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<EditExamDialog>("SỬA ĐỀ THI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenAssignExamDialogAsync()
        {
            var parameters = new DialogParameters<AssignExamDialog>
            {
                { x => x.Exams, selectedExams },
                { x => x.ExamSession, examSession }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<AssignExamDialog>("GÁN ĐỀ THI", parameters, options);
            return await dialog.Result;
        }

        #endregion


        #region Other Methods


        private void CreateFakeData_MonHoc()
        {
            if (subjects != null && subjects.Count != 0)
            {
                int count_fake = totalRecords_Subject - subjects.Count;
                bool isFake = totalRecords_Subject > subjects.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        subjects.Add(new MonHocDto());
                }
            }
        }

        private void CreateFakeData_DeThi()
        {
            if (exams != null && exams.Count != 0)
            {
                int count_fake = totalRecords_Exam - exams.Count;
                bool isFake = totalRecords_Exam > exams.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        exams.Add(new DeThiDto());
                }
            }
        }
        private void PadEmptyRows(List<MonHocDto>? newMonHocs)
        {
            if (newMonHocs == null || newMonHocs.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Subject * rowsPerPage_Subject;
            if (subjects != null && subjects.Count != 0)
            {
                for (int i = 0; i < newMonHocs.Count; i++)
                {

                    subjects[startRow++] = newMonHocs[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<DeThiDto>? newDeThis)
        {
            if (newDeThis == null || newDeThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Exam * rowsPerPage_Exam;
            if (exams != null && exams.Count != 0)
            {
                for (int i = 0; i < newDeThis.Count; i++)
                {

                    exams[startRow++] = newDeThis[i];
                }
            }
            StateHasChanged();
        }

        #endregion
    }
}
