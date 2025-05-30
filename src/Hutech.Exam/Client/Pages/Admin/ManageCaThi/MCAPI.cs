using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;


namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
    {
        private async Task<List<DotThiDto>?> DotThis_GetAllAPI()
        {
            var response = await SenderAPI.GetAsync<List<DotThiDto>>("api/dotthis");
            return (response.Success) ? response.Data : null;
        }

        private async Task<List<MonHocDto>?> MonHocs_GetAllAPI()
        {
            var response = await SenderAPI.GetAsync<List<MonHocDto>>("api/monhocs");
            return (response.Success) ? response.Data : null;
        }

        private async Task<List<LopAoDto>?> LopAos_SelectBy_MaMonHocAPI(int ma_mon_hoc)
        {
            var response = await SenderAPI.GetAsync<List<LopAoDto>>($"api/lopaos/filter-by-monhoc?maMonHoc={ma_mon_hoc}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<List<ChiTietDotThiDto>?> ChiTietDotThis_SelectBy_MaDotThi_MaLopAoAPI(int ma_dot_thi, int ma_lop_ao)
        {
            var response = await SenderAPI.GetAsync<List<ChiTietDotThiDto>>($"api/chitietdotthis/filter-by-dotthi-lopao?maDotThi={ma_dot_thi}&maLopAo={ma_lop_ao}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<ChiTietDotThiDto?> ChiTietDotThis_SelectBy_MaDotThi_MaLopAo_LanThiAPI(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            var response = await SenderAPI.GetAsync<ChiTietDotThiDto>($"api/chitietdotthis/filter-by-dotthi-lopao-lanthi?maDotThi={ma_dot_thi}&maLopAo={ma_lop_ao}&lanThi={lan_thi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<List<CaThiDto>?> CaThis_SelectBy_MaDotThi_MaLopAo_LanThiAPI(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            var response = await SenderAPI.GetAsync<List<CaThiDto>>($"api/cathis/filter-by-dotthi-lopao-lanthi?maDotThi={ma_dot_thi}&maLopAo={ma_lop_ao}&lanThi={lan_thi}");
            return (response.Success) ? response.Data : null;
        }
        
    }
}
