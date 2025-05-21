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
        private async Task<List<CustomDeThi>?> GetDeThiAPI(long? ma_de_hoan_vi)
        {
            var response = await Http.GetAsync($"api/CustomDeThi/GetDeThi?ma_de_thi_hoan_vi={ma_de_hoan_vi}");
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
        private async Task<List<ChiTietBaiThiRequest>?> GetBaiThi_DaThiAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await Http.GetAsync($"api/CustomChiTietBaiThi/GetBaiThi_DaThi?ma_chi_tiet_ca_thi={ma_chi_tiet_ca_thi}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ChiTietBaiThiRequest>>();
            }
            else
            {
                Snackbar.Add(ERROR_FETCH_BAILAM, Severity.Error);
                return null;
            }
        }

        private async Task<int> GetSoLanNgheAPI(int ma_chi_tiet_ca_thi, int ma_nhom, string? filename = null)
        {
            AudioListenedDto audio = new()
            {
                MaChiTietCaThi = ma_chi_tiet_ca_thi,
                MaNhom = ma_nhom,
                FileName = filename
            };
            var jsonString = JsonSerializer.Serialize(audio);
            var response = await Http.PutAsync($"api/AudioListened/GetSoLanNghe", new StringContent(jsonString, Encoding.UTF8, "application/json"));

            return await response.Content.ReadFromJsonAsync<int>();
        }
        private async Task<bool> UpdateLogoutAPI(SinhVienDto sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            var response = await Http.PutAsync($"api/SinhVien/UpdateLogout", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
