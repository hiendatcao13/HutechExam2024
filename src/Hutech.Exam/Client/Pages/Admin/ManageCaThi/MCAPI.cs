using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;


namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
    {
        private async Task<List<DotThiDto>?> DotThis_GetAllAPI()
        {
            var response = await Http.GetAsync("api/dotthis");
            return await response.Content.ReadFromJsonAsync<List<DotThiDto>?>();
        }
        private async Task<List<MonHocDto>?> MonHocs_GetAllAPI()
        {
            var response = await Http.GetAsync("api/monhocs");
            return await response.Content.ReadFromJsonAsync<List<MonHocDto>?>();
        }
        private async Task<List<LopAoDto>?> LopAos_SelectBy_MaMonHocAPI(int ma_mon_hoc)
        {
            var response = await Http.GetAsync($"api/lopaos/filter-by-monhoc?maMonHoc={ma_mon_hoc}");
            return await response.Content.ReadFromJsonAsync<List<LopAoDto>?>();
        }
        private async Task<List<ChiTietDotThiDto>?> ChiTietDotThis_SelectBy_MaDotThi_MaLopAoAPI(int ma_dot_thi, int ma_lop_ao)
        {
            var response = await Http.GetAsync($"api/chitietdotthis/filter-by-dotthi-lopao?maDotThi={ma_dot_thi}&maLopAo={ma_lop_ao}");
            return await response.Content.ReadFromJsonAsync<List<ChiTietDotThiDto>?>();
        }
        private async Task<List<CaThiDto>?> CaThis_SelectBy_MaDotThi_MaLopAo_LanThiAPI(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            var response = await Http.GetAsync($"api/cathis/filter-by-dotthi-lopao-lanthi?maDotThi={ma_dot_thi}&maLopAo={ma_lop_ao}&lanThi={lan_thi}");
            return await response.Content.ReadFromJsonAsync<List<CaThiDto>?>();
        }
        
    }
}
