using Hutech.Exam.Shared;
using MudBlazor;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Login
{
    public partial class Login
    {
        private async Task<UserSession?> LoginAPI(string username)
        {
            Snackbar.Add(LOADING_MESSAGE, Severity.Info);
            var response = await Http.GetAsync($"api/SinhVien/Login?ma_so_sinh_vien={username}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserSession?>();
            }
            return null;
        }
    }
}
