using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        // curentPage mặc định của MudTable là 0, trang đầu là 0
        private async Task<CaThiDto?> CaThi_SelectOneAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.GetAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<(List<ChiTietCaThiDto>, int, int)> ChiTietCaThis_SelectBy_MaCaThi_PagedAPI(int ma_ca_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<ChiTietCaThiPage>($"api/chitietcathis/filter-by-cathi-paged?maCaThi={ma_ca_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalRecords, response.Data.TotalPages) : ([], 0, 0);
        }
        private async Task<(List<ChiTietCaThiDto>, int, int)> ChiTietCaThis_SelectBy_MaCaThi_Search_PagedAPI(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<ChiTietCaThiPage>($"api/chitietcathis/filter-by-cathi-search-paged?maCaThi={ma_ca_thi}&keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalRecords, response.Data.TotalPages) : ([], 0, 0);
        }
        private async Task<bool> ResetLoginAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PatchAsync<SinhVienDto>($"api/sinhviens/{ma_ca_thi}/reset-login", null);
            return response.Success;
        }
        private async Task<bool> NopBaiAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PutAsync<SinhVienDto>($"api/sinhviens/{ma_sinh_vien}/submit-exam", null);
            return response.Success;
        }
        private async Task<List<KhoaDto>?> Khoas_GetAllAPI()
        {
            var response = await SenderAPI.GetAsync<List<KhoaDto>>($"api/khoas");
            return (response.Success) ? response.Data : null;
        }
        private async Task<byte[]?> GetExcelFileAPI(List<ChiTietCaThiDto> chiTietCaThis)
        {
            var response = await SenderAPI.PostAsync<byte[]>("api/chitietcathis/export-excel", chiTietCaThis);
            return (response.Success) ? response.Data : [];
        }
    }
}
