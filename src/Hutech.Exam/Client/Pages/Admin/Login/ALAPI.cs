using Hutech.Exam.Shared;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using MudBlazor;
using Hutech.Exam.Shared.DTO.Request.User;

namespace Hutech.Exam.Client.Pages.Admin.Login
{
    public partial class AdminLogin
    {
        private async Task<UserSession?> LoginAPI(UserAuthenticationRequest account)
        {
            var json = JsonSerializer.Serialize(account);
            var response = await Http.PutAsync($"api/users/login", new StringContent(json, Encoding.UTF8, "application/json"));
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserSession>();
            }
            return null;
        }
    }
}
