using Hutech.Exam.Shared;
using MudBlazor;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Login
{
    public partial class Login
    {
        private async Task<UserSession?> LoginAPI(string username)
        {
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);
            var json = JsonSerializer.Serialize(username);
            var response = await Http.PutAsync("api/SinhVien/Login", new StringContent(json, Encoding.UTF8, "application/json"));
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<UserSession?>();
            return null;
        }
    }
}
