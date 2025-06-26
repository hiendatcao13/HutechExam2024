using Hutech.Exam.Client.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Client.Pages.Admin.ExamReview
{
    public partial class ExamReview
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;


        List<MonHocDto>? subjects = [];
        MonHocDto? selectedSubject;

        List<DeThiDto>? exams = [];
        DeThiDto? selectedExam;

        //thống kê report
        List<CustomThongKeCauHoi> customQuestionReports = [];

        // thống kê điểm
        List<CustomThongKeDiem> customScoreReports = [];
        int totalStudent, totalStudentLessEqual1, totalStudentLess5 = 0;
        double averageScore = 0;

        #endregion

        #region Initial Methods
        protected async override Task OnInitializedAsync()
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

        #region Other Methods

        private async Task ReportScoreAsync(int maDeThi)
        {
            customScoreReports = await ScoreReport_SelectBy_ExamAPI(maDeThi);

            List<double> diems = customScoreReports.Select(_ => _.Diem).ToList();
            totalStudent = diems.Count();
            averageScore = (diems.Count != 0) ? diems.Average() : 0;
            totalStudentLessEqual1 = diems.Count(d => d <= 1);
            totalStudentLess5 = diems.Count(d => d < 5);
        }

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
