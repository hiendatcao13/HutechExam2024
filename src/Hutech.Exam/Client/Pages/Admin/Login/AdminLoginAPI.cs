using Hutech.Exam.Shared;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using MudBlazor;

namespace Hutech.Exam.Client.Pages.Admin.Login
{
    public partial class AdminLogin
    {
        private async Task<UserSession?> LoginAPI(string username, string password)
        {
            var json = JsonSerializer.Serialize(new { username, password });
            var response = await Http.PutAsync($"api/User/Login", new StringContent(json, Encoding.UTF8, "application/json"));
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserSession>();
            }
            return null;
        }
    }
}
