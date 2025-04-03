using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task<List<ChiTietCaThiDto>?> GetThongTinChiTietCaThiAPI(int ma_ca_thi)
        {
            var response = await Http.GetAsync($"api/ChiTietCaThi/SelectBy_MaCaThi?ma_ca_thi={ma_ca_thi}");
            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ChiTietCaThiDto>?>();
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
        private async Task<List<KhoaDto>?> GetAllKhoaAPI()
        {
            var response = await Http.GetAsync($"api/Khoa/GetAllKhoa");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<KhoaDto>?>();
            }
            return null;
        }
    }
}
