using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;
namespace Hutech.Exam.Client.Pages.Info
{
    public partial class Info
    {
        private async Task<ChiTietCaThiDto?> GetChiTietCaThiAPI(long ma_sinh_vien)
        {

            var response = await Http.GetAsync($"api/chitietcathis/filter-by-sinhvien?maSinhVien={ma_sinh_vien}");
            return await response.Content.ReadFromJsonAsync<ChiTietCaThiDto?>();
        }
        private async Task<bool> UpdateLogoutAPI(long ma_sinh_vien)
        {
            var response = await Http.PutAsync($"api/sinhviens/{ma_sinh_vien}/logout", null);
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> UpdateBatDauThiAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await Http.PutAsync($"api/chitietcathis/{ma_chi_tiet_ca_thi}/bat-dau-thi", null);
            return response.IsSuccessStatusCode;
        }
    }
}
