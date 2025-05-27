using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        private async Task<CaThiDto?> CaThi_SelectOneAPI(int ma_ca_thi)
        {
            var response = await Http.GetAsync($"api/cathis/{ma_ca_thi}");
            return await response.Content.ReadFromJsonAsync<CaThiDto?>();
        }
        private async Task<ChiTietDotThiDto?> ChiTietDotThi_SelectOneAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await Http.GetAsync($"api/chitietdotthis/{ma_chi_tiet_ca_thi}");
            return await response.Content.ReadFromJsonAsync<ChiTietDotThiDto?>();
        }
        private async Task<DotThiDto?> DotThi_SelectOneAPI(int ma_dot_thi)
        {
            var response = await Http.GetAsync($"api/dotthis/{ma_dot_thi}");
            return await response.Content.ReadFromJsonAsync<DotThiDto?>();
        }



        private async Task<List<DotThiDto>?> DotThis_GetAllAPI()
        {
            var response = await Http.GetAsync("api/dotthis");
            return await response.Content.ReadFromJsonAsync<List<DotThiDto>?>();
        }
        private async Task<List<ChiTietDotThiDto>?> ChiTietDotThis_SelectBy_MaDotThiAPI(int ma_dot_thi)
        {
            var response = await Http.GetAsync($"api/chitietdotthis/filter-by-dotthi?maDotThi={ma_dot_thi}");
            return await response.Content.ReadFromJsonAsync<List<ChiTietDotThiDto>?>();
        }
        private async Task<List<CaThiDto>?> CaThis_SelectBy_MaChiTietDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await Http.GetAsync($"api/cathis/filter-by-chitietdotthi?maChiTietDotThi={ma_chi_tiet_dot_thi}");
            return await response.Content.ReadFromJsonAsync<List<CaThiDto>?>();
        }




        private async Task<bool> DeleteDotThiAPI(int ma_dot_thi)
        {
            var response = await Http.DeleteAsync($"api/dotthis/{ma_dot_thi}");
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> DeleteCTDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await Http.DeleteAsync($"api/chitietdotthis/{ma_chi_tiet_dot_thi}");
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> DeleteCaThiAPI(int ma_ca_thi)
        {
            var response = await Http.DeleteAsync($"api/cathis/{ma_ca_thi}");
            return response.IsSuccessStatusCode;
        }
    }
}
