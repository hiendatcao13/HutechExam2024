using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        private async Task<CaThiDto?> ExamSession_SelectOneAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.GetAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<ChiTietDotThiDto?> ExamBatchDetail_SelectOneAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await SenderAPI.GetAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_ca_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<DotThiDto?> ExamBatch_SelectOneAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.GetAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<(List<DotThiDto>?, int, int)> ExamBatchs_GetAllAPI(int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<DotThiDto>>($"api/dotthis?pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<(List<ChiTietDotThiDto>?, int, int)> ExamBatchDetailDetail_SelectBy_ExamBatchId_PagedAPI(int ma_dot_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<ChiTietDotThiDto>>($"api/chitietdotthis/filter-by-dotthi-paged?maDotThi={ma_dot_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<(List<CaThiDto>?, int, int)> ExamSessions_SelectBy_ExamBatchDetailId_PagedAPI(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<CaThiDto>>($"api/cathis/filter-by-chitietdotthi-paged?maChiTietDotThi={ma_chi_tiet_dot_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<(List<CaThiDto>?, int, int)> ExamSessions_SelectBy_ExamBatchDetailId_Search_PagedAPI(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<CaThiDto>>($"api/cathis/filter-by-chitietdotthi-paged?maChiTietDotThi={ma_chi_tiet_dot_thi}&keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<CaThiDto> ExamSession_UpdateApprove(int ma_ca_thi, string lichSuHoatDong)
        {
            var response = await SenderAPI.PutAsync<CaThiDto>($"api/cathis/{ma_ca_thi}/duyet-de", lichSuHoatDong);
            return (response.Success && response.Data != null) ? response.Data : new();
        }



        private async Task<bool> ExamBatch_DeleteAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}");
            return response.Success;
        }

        private async Task<bool> ExamBatch_ForceDeleteAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}/force");
            return response.Success;
        }

        private async Task<bool> ExamBatchDetail_DeleteAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_dot_thi}");
            return response.Success;
        }

        private async Task<bool> ExamBatchDetail_ForceDeleteAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_dot_thi}/force");
            return response.Success;
        }

        private async Task<bool> ExamSession_DeleteAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.DeleteAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return response.Success;
        }

        private async Task<bool> ExamSession_ForceDeleteAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.DeleteAsync<CaThiDto>($"api/cathis/{ma_ca_thi}/force");
            return response.Success;
        }

    }
}
