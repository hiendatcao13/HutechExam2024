using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using StackExchange.Redis;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage
    {
        private async Task<List<CustomDeThi>?> GetDeThiAPI(long ma_de_hoan_vi)
        {
            var response = await SenderAPI.GetAsync<List<CustomDeThi>>($"api/dethihoanvis/{ma_de_hoan_vi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<int> GetSoLanNgheAPI(AudioListenedDto audio)
        {
            var response = await SenderAPI.PutAsync<int>($"api/audios", audio);
            return (response.Success) ? response.Data : -1;
        }
        private async Task<bool> UpdateLogoutAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PostAsync<SinhVienDto>($"api/sinhviens/{ma_sinh_vien}/logout", null);
            return response.Success;
        }
    }
}
