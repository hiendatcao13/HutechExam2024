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
            var response = await SenderAPI.PostAsync<UserSession>($"api/users/login", account);
            return (response.Success) ? response.Data : null;
        }
    }
}
