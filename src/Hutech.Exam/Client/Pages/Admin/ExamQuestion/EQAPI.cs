using Hutech.Exam.Shared.DTO;
using System.Data;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public partial class ExamQuestion
    {
        private async Task<NhomCauHoiDto?> NhomCauHoi_SelectOneAPI(int ma_nhom)
        {
            var response = await Http.GetAsync($"api/nhomcauhois/{ma_nhom}");
            return await response.Content.ReadFromJsonAsync<NhomCauHoiDto?>();
        }
        private async Task<CauHoiDto?> CauHoi_SelectOneAPI(int ma_cau_hoi)
        {
            var response = await Http.GetAsync($"api/cauhois/{ma_cau_hoi}");
            return await response.Content.ReadFromJsonAsync<CauHoiDto?>();
        }
        private async Task<List<CauTraLoiDto>?> CauTraLoi_SelectOneAPI(int ma_cau_tra_loi)
        {
            var response = await Http.GetAsync($"api/cautralois/{ma_cau_tra_loi}");
            return await response.Content.ReadFromJsonAsync<List<CauTraLoiDto>?>();
        }
        private async Task<List<MonHocDto>?> MonHocs_GetAllAPI()
        {
            var response = await Http.GetAsync("api/monhocs");
            return await response.Content.ReadFromJsonAsync<List<MonHocDto>?>();
        }
        private async Task<List<DeThiDto>?> DeThis_SelectBy_MaMonHocAPI(int ma_mon_hoc)
        {
            var response = await Http.GetAsync($"api/dethis/filter-monhoc?maMonHoc={ma_mon_hoc}");
            return await response.Content.ReadFromJsonAsync<List<DeThiDto>?>();
        }
        private async Task<List<NhomCauHoiDto>?> NhomCauHois_SelectAllBy_MaDeThiAPI(int ma_de_thi)
        {
            var response = await Http.GetAsync($"api/nhomcauhois/filter-dethi?maDeThi={ma_de_thi}");
            return await response.Content.ReadFromJsonAsync<List<NhomCauHoiDto>?>();
        }
        public async Task<List<CauHoiDto>?> CauHois_SelectBy_MaNhomAPI(int ma_nhom)
        {
            var response = await Http.GetAsync($"api/cauhois/filter-nhomcauhoi?maNhomCauHoi={ma_nhom}");
            return await response.Content.ReadFromJsonAsync<List<CauHoiDto>?>();
        }
    }
}
