using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        private async Task<List<DotThiDto>?> GetAllDotThiAPI()
        {
            var response = await Http.GetAsync("api/DotThi/GetAll");
            return await response.Content.ReadFromJsonAsync<List<DotThiDto>?>();
        }
        private async Task<List<ChiTietDotThiDto>?> GetCTDotThi_MaDotThiAPI(int ma_dot_thi)
        {
            var response = await Http.GetAsync($"api/ChiTietDotThi/SelectBy_MaDotThi?ma_dot_thi={ma_dot_thi}");
            return await response.Content.ReadFromJsonAsync<List<ChiTietDotThiDto>?>();
        }
        private async Task<List<CaThiDto>?> GetCaThi_MaChiTietDotThiAPI(int ma_chi_tiet_dot_thi)
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
