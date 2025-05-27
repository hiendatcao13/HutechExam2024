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
            var response = await Http.GetAsync($"api/dethihoanvis/{ma_de_hoan_vi}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CustomDeThi>?>();
            }
            else
            {
                Snackbar.Add(ERROR_FETCH_DETHI, Severity.Error);
                return null;
            }
        }

        private async Task<int> GetSoLanNgheAPI(AudioListenedDto audio)
        {
            var jsonString = JsonSerializer.Serialize(audio);
            var response = await Http.PutAsync($"api/audios", new StringContent(jsonString, Encoding.UTF8, "application/json"));

            return await response.Content.ReadFromJsonAsync<int>();
        }
        private async Task<bool> UpdateLogoutAPI(long ma_sinh_vien)
        {
            var response = await Http.PutAsync($"api/sinhviens/{ma_sinh_vien}/logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}
