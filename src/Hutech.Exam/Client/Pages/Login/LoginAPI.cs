using System.Text.Json;
using System.Text;
using Hutech.Exam.Shared;
using MudBlazor;
using System.Net.Http.Json;
using Hutech.Exam.Shared.DTO.Request.SinhVien;

namespace Hutech.Exam.Client.Pages.Login
{
    public partial class Login
    {
        private async Task<UserSession?> LoginAPI(SinhVienAuthenticationRequest account)
        {
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);
            var json = JsonSerializer.Serialize(account);
            var response = await Http.PutAsync("api/sinhviens/login", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<UserSession?>();
            return null;
        }
    }
}
