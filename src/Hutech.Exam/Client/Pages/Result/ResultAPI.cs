using System.Text.Json;
using System.Text;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.API;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        private async Task<bool> UpdateLogoutAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PostAsync<SinhVienDto>($"api/sinhviens/{ma_sinh_vien}/logout", null);
            return response.Success;
        }
    }
}
