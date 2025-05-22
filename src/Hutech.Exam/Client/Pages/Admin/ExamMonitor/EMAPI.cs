using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
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
            var response = await Http.GetAsync($"api/CaThi/SelectOne?ma_ca_thi={ma_ca_thi}");
            return await response.Content.ReadFromJsonAsync<CaThiDto?>();
        }
        private async Task<(List<ChiTietCaThiDto>, int, int)?> ChiTietCaThis_SelectBy_MaCaThi_PagedAPI(int ma_ca_thi, int pageNumber, int pageSize)
        {
            var response = await Http.GetAsync($"api/ChiTietCaThi/SelectBy_MaCaThi_Paged?ma_ca_thi={ma_ca_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ChiTietCaThiPageResult>();
                if (result != null)
                {
                    return (result.Data ?? [], result.TotalRecords, result.TotalPages);
                }
            }
            return null;
        }
        private async Task<(List<ChiTietCaThiDto>, int, int)?> ChiTietCaThis_SelectBy_MaCaThi_Search_PagedAPI(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            var response = await Http.GetAsync($"api/ChiTietCaThi/SelectBy_MaCaThi_Search_Paged?ma_ca_thi={ma_ca_thi}&keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ChiTietCaThiPageResult>();
                if(result != null)
                {
                    return (result.Data ?? [], result.TotalRecords, result.TotalPages);
                }
            }
            return null;
        }
        private async Task<bool> ResetLoginAPI(SinhVienDto sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            var response = await Http.PutAsync($"api/SinhVien/ResetLogin", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add(SUCCESS_RESETLOGIN, MudBlazor.Severity.Success);
                return true;
            }
            Snackbar.Add(ERROR_RESETLOGIN, MudBlazor.Severity.Error);
            return false;
        }
        private async Task<bool> NopBaiAPI(SinhVienDto sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            var response = await Http.PutAsync($"api/SinhVien/SubmitExam", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        private async Task<List<KhoaDto>?> Khoas_GetAllAPI()
        {
            var response = await Http.GetAsync($"api/Khoa/GetAllKhoa");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<KhoaDto>?>();
            }
            return null;
        }
        private async Task<byte[]?> GetExcelFileAPI(List<ChiTietCaThiDto> chiTietCaThis)
        {
            var jsonString = JsonSerializer.Serialize(chiTietCaThis);
            var response = await Http.PostAsync("api/CaThi/GenerateExcelFile", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<byte[]>();
            }
            return Array.Empty<byte>();
        }
    }
}
