using Hutech.Exam.Shared;
using MudBlazor;
using Hutech.Exam.Shared.DTO.Request.SinhVien;

namespace Hutech.Exam.Client.Pages.Login
{
    public partial class Login
    {
        private async Task<UserSession?> LoginAPI(SinhVienAuthenticationRequest account)
        {
            var response = await SenderAPI.PostAsync<UserSession>("api/sinhviens/login", account);
            return (response.Success) ? response.Data : null;
        }
    }
}
