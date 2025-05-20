using System.Text.Json;
using System.Text;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        private async Task<bool> UpdateLogoutAPI(SinhVienDto sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            var response = await Http.PutAsync($"api/SinhVien/UpdateLogout", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
