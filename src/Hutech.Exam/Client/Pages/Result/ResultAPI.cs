using System.Text.Json;
using System.Text;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        private async Task<bool> UpdateLogoutAPI(long ma_sinh_vien)
        {
            var response = await Http.PutAsync($"api/sinhvien/{ma_sinh_vien}/logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}
