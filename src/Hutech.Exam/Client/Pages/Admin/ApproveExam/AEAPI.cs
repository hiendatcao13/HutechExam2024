using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.DeThi;

namespace Hutech.Exam.Client.Pages.Admin.ApproveExam
{
    public partial class ApproveExam
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

        private async Task<CaThiDto> ExamSession_SelectOneAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.GetAsync<CaThiDto>($"api/cathi/{ma_ca_thi}");
            return (response.Success && response.Data != null) ? (response.Data) : new();
        }


        // MockAPI và save batch
        private async Task<List<DeThiDto>> Exams_GetAllAPI()
        {
            var response = await SenderAPI.GetAsync<List<DeThiDto>>("api/dethis/mock-api");
            return (response.Success && response.Data != null) ? response.Data : [];
        }

        private async Task<bool> Exam_SaveBatchAPI(List<DeThiDto> deThis)
        {
            var response = await SenderAPI.PostAsync<DeThiDto>("api/dethis/batch", deThis);
            return response.Success;
        }



        public async Task<bool> Exam_DeleteAPI(long ma_de_thi)
        {
            var response = await SenderAPI.DeleteAsync<DeThiDto>($"api/dethis/{ma_de_thi}");
            return response.Success;
        }

        public async Task<bool> Exam_ForceDeleteAPI(long ma_de_thi)
        {
            var response = await SenderAPI.DeleteAsync<DeThiDto>($"api/dethis/{ma_de_thi}/force");
            return response.Success;
        }
    }
}
