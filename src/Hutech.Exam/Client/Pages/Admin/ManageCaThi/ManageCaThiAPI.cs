using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;


namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
    {
        private async Task<List<DotThiDto>?> GetAllDotThiAPI()
        {
            var response = await Http.GetAsync("api/DotThi/GetAll");
            return await response.Content.ReadFromJsonAsync<List<DotThiDto>?>();
        }
        private async Task<List<MonHocDto>?> GetAllMonHocAPI()
        {
            var response = await Http.GetAsync("api/MonHoc/GetAll");
            return await response.Content.ReadFromJsonAsync<List<MonHocDto>?>();
        }
        private async Task<List<LopAoDto>?> LopAo_SelectedByMonHocAPI(int ma_mon_hoc)
        {
            var response = await Http.GetAsync($"api/LopAo/SelectBy_MaMonHoc?ma_mon_hoc={ma_mon_hoc}");
            return await response.Content.ReadFromJsonAsync<List<LopAoDto>?>();
        }
        private async Task<List<string>?> LanThis_SelectBy_MaDotThi_MaLopAoAPI(int ma_dot_thi, int ma_lop_ao)
        {
            var response = await Http.GetAsync($"api/ChiTietDotThi/LanThis_SelectBy_MaDotThiMaLopAo?ma_dot_thi={ma_dot_thi}&ma_lop_ao={ma_lop_ao}");
            return await response.Content.ReadFromJsonAsync<List<string>?>();
        }
        private async Task<List<CaThiDto>?> GetAllCaThi_SelectBy_MaDotThi_MaLopAo_LanThiAPI(int ma_dot_thi, int ma_lop_ao, string lan_thi)
        {
            var response = await Http.GetAsync($"api/CaThi/SelectBy_MaDotThi_MaLopAo_LanThi?ma_dot_thi={ma_dot_thi}&ma_lop_ao={ma_lop_ao}&lan_thi={lan_thi}");
            return await response.Content.ReadFromJsonAsync<List<CaThiDto>?>();
        }
        
    }
}
