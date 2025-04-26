using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        private async Task<CaThiDto?> CaThi_SelectOneAPI(int ma_ca_thi)
        {
            var response = await Http.GetAsync($"api/CaThi/SelectOne?ma_ca_thi={ma_ca_thi}");
            return await response.Content.ReadFromJsonAsync<CaThiDto?>();
        }
        private async Task<ChiTietDotThiDto?> ChiTietDotThi_SelectOneAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await Http.GetAsync($"api/ChiTietDotThi/SelectOne?ma_chi_tiet_ca_thi={ma_chi_tiet_ca_thi}");
            return await response.Content.ReadFromJsonAsync<ChiTietDotThiDto?>();
        }
        private async Task<DotThiDto?> DotThi_SelectOneAPI(int ma_dot_thi)
        {
            var response = await Http.GetAsync($"api/DotThi/SelectOne?ma_dot_thi={ma_dot_thi}");
            return await response.Content.ReadFromJsonAsync<DotThiDto?>();
        }






        private async Task<List<DotThiDto>?> DotThis_GetAllAPI()
        {
            var response = await Http.GetAsync("api/DotThi/GetAll");
            return await response.Content.ReadFromJsonAsync<List<DotThiDto>?>();
        }
        private async Task<List<ChiTietDotThiDto>?> ChiTietDotThis_SelectBy_MaDotThiAPI(int ma_dot_thi)
        {
            var response = await Http.GetAsync($"api/ChiTietDotThi/SelectBy_MaDotThi?ma_dot_thi={ma_dot_thi}");
            return await response.Content.ReadFromJsonAsync<List<ChiTietDotThiDto>?>();
        }
        private async Task<List<CaThiDto>?> CaThis_SelectBy_MaChiTietDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await Http.GetAsync($"api/CaThi/SelectBy_ma_chi_tiet_dot_thi?ma_chi_tiet_dot_thi={ma_chi_tiet_dot_thi}");
            return await response.Content.ReadFromJsonAsync<List<CaThiDto>?>();
        }




        private async Task<bool> DeleteDotThiAPI(int ma_dot_thi)
        {
            var response = await Http.DeleteAsync($"api/DotThi/Remove?ma_dot_thi={ma_dot_thi}");
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> DeleteCTDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await Http.DeleteAsync($"api/ChiTietDotThi/Remove?ma_chi_tiet_dot_thi={ma_chi_tiet_dot_thi}");
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> DeleteCaThiAPI(int ma_ca_thi)
        {
            var response = await Http.DeleteAsync($"api/CaThi/Remove?ma_ca_thi={ma_ca_thi}");
            return response.IsSuccessStatusCode;
        }
    }
}
