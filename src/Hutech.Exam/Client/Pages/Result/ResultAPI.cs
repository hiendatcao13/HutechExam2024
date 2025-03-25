using System.Text.Json;
using System.Text;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        private async Task<bool> UpdateKetThucAPI(ChiTietCaThiDto chiTietCaThi)
        {
            var jsonString = JsonSerializer.Serialize(chiTietCaThi);
            var response = await Http.PutAsync("api/ChiTietCaThi/UpdateKetThuc", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> UpdateLogoutAPI(SinhVienDto sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            var response = await Http.PutAsync($"api/SinhVien/UpdateLogout", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        private async Task<List<bool?>?> GetListDungSaiAPI(List<int?> dapAnKhoanh, long ma_de_hv)
        {
            var json = JsonSerializer.Serialize(dapAnKhoanh);
            var response = await Http.PutAsync($"api/CustomChiTietBaiThi/GetListDungSai?ma_de_hv={ma_de_hv}", new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadFromJsonAsync<List<bool?>>();
        }
    }
}
