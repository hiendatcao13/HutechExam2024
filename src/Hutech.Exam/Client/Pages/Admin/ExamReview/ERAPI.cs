using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Client.Pages.Admin.ExamReview
{
    public partial class ExamReview
    {
        private async Task<(List<MonHocDto>?, int, int)> Subjects_GetAll_PagedAPI(int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<MonHocDto>>($"api/monhocs?pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<(List<DeThiDto>?, int, int)> Exams_SelectBy_SubjectId_PagedAPI(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<DeThiDto>>($"api/dethis/filter-by-monhoc?maMonHoc={ma_mon_hoc}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<List<CustomThongKeCauHoi>> QuestionReport_SelectBy_ExamAPI(long maDeThi)
        {
            var response = await SenderAPI.GetAsync<List<CustomThongKeCauHoi>>($"api/dethis/{maDeThi}/report-cauhoi");
            return (response.Success && response.Data != null) ? response.Data : [];
        }

        private async Task<List<CustomThongKeDiem>> ScoreReport_SelectBy_ExamAPI(long maDeThi)
        {
            var response = await SenderAPI.GetAsync<List<CustomThongKeDiem>>($"api/dethis/{maDeThi}/report");
            return (response.Success && response.Data != null) ? response.Data : [];
        }

        private async Task<CustomThongKeCapBacSV> StudentLevelReport_SelectBy_ExamAPI(long maDeThi)
        {
            var response = await SenderAPI.GetAsync<CustomThongKeCapBacSV>($"api/dethis/{maDeThi}/report-capbacsv");
            return (response.Success && response.Data != null) ? response.Data : new();
        }

    }
}
