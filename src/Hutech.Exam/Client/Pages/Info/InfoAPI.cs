using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using MudBlazor;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Info
{
    public partial class Info
    {
        private async Task<ChiTietCaThiDto?> GetChiTietCaThiAPI(long ma_sinh_vien)
        {
            // kiểm tra tham số
            if (ma_sinh_vien == -1)
                return null;
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
