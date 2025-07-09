using Hutech.Exam.Client.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO.Custom;
using System.Text.Json;
using Microsoft.JSInterop;

namespace Hutech.Exam.Client.Pages.Admin.ExamReview
{
    public partial class ExamReview
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [Inject] private IJSRuntime Js { get; set; } = default!;


        List<MonHocDto>? subjects = [];
        MonHocDto? selectedSubject;

        List<DeThiDto>? exams = [];
        DeThiDto? selectedExam;

        //thống kê report câu hỏi
        List<CustomThongKeCauHoi> customQuestionReports = [];

        //Thống kê cấp bậc sv top, bottom
        CustomThongKeCapBacSV customStudentLevelReport = new();
        bool isFirstRenderLevelReport = true;

        // thống kê điểm
        List<CustomThongKeDiem> customScoreReports = [];
        int totalStudent, totalStudentLessEqual1, totalStudentLess5 = 0;
        double averageScore = 0;


        private const string NO_SELECT_OBJECT_DETHI = "Vui lòng chọn đề thi";

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

        private async Task FetchLevelStudentReportAsync()
        {
            if(selectedExam == null)
            {
                Snackbar.Add(NO_SELECT_OBJECT_DETHI, MudBlazor.Severity.Warning);
                return;
            }
            
            customStudentLevelReport = await StudentLevelReport_SelectBy_ExamAPI(selectedExam.MaDeThi);
   
            foreach(var item in  customQuestionReports)
            {
                item.CustomThongKeCapBacCauHoi = customStudentLevelReport.ThongKeCapBacCauHois.FirstOrDefault(r => r.MaCauHoi == item.MaCauHoi);
            }    
        }

        #endregion

        private async Task OnClickDownload()
        {
            if(customStudentLevelReport.TongSVThamGia == 0)
            {
                await FetchLevelStudentReportAsync();
            } 

            // bỏ trường MaCauHoi, chỉ lấy Guid cho đề
            customStudentLevelReport.ThongKeCapBacCauHois.ForEach(_ => _.MaCauHoi = null);
            // nhớ là có import file Dlinh thì xóa cái này
            customStudentLevelReport.ThongKeCapBacCauHois.ForEach(_ => _.GuidCauHoi = Guid.NewGuid());
            var json = JsonSerializer.Serialize(customStudentLevelReport, new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });

            // Gọi JS để tải file
            await Js.InvokeVoidAsync("downloadFileFromText", "student.txt", json);
        }    

        #region Other Methods

        private async Task ReportScoreAsync(long maDeThi)
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
