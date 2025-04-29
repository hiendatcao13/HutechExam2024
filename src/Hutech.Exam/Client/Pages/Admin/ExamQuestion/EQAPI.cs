using Hutech.Exam.Shared.DTO;
using System.Data;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public partial class ExamQuestion
    {
        private async Task<NhomCauHoiDto?> NhomCauHoi_SelectOneAPI(int ma_nhom)
        {
            var response = await Http.GetAsync($"api/NhomCauHoi/SelectOne?ma_nhom={ma_nhom}");
            return await response.Content.ReadFromJsonAsync<NhomCauHoiDto?>();
        }
        private async Task<CauHoiDto?> CauHoi_SelectOneAPI(int ma_cau_hoi)
        {
            var response = await Http.GetAsync($"api/CauHoi/SelectOne?ma_cau_hoi={ma_cau_hoi}");
            return await response.Content.ReadFromJsonAsync<CauHoiDto?>();
        }
        private async Task<List<CauTraLoiDto>?> CauTraLoi_SelectOneAPI(int ma_cau_tra_loi)
        {
            var response = await Http.GetAsync($"api/CauTraLoi/SelectOne?ma_cau_tra_loi={ma_cau_tra_loi}");
            return await response.Content.ReadFromJsonAsync<List<CauTraLoiDto>?>();
        }
        private async Task<List<MonHocDto>?> MonHocs_GetAllAPI()
        {
            var response = await Http.GetAsync("api/MonHoc/GetAll");
            return await response.Content.ReadFromJsonAsync<List<MonHocDto>?>();
        }
        private async Task<List<DeThiDto>?> DeThis_SelectBy_MaMonHocAPI(int ma_mon_hoc)
        {
            var response = await Http.GetAsync($"api/DeThi/SelectByMonHoc?ma_mon_hoc={ma_mon_hoc}");
            return await response.Content.ReadFromJsonAsync<List<DeThiDto>?>();
        }
        private async Task<List<NhomCauHoiDto>?> NhomCauHois_SelectAllBy_MaDeThiAPI(int ma_de_thi)
        {
            var response = await Http.GetAsync($"api/NhomCauHoi/SelectAllBy_MaDeThi?ma_de_thi={ma_de_thi}");
            return await response.Content.ReadFromJsonAsync<List<NhomCauHoiDto>?>();
        }
        public async Task<List<CauHoiDto>?> CauHois_SelectBy_MaNhomAPI(int ma_nhom)
        {
            var response = await Http.GetAsync($"api/CauHoi/SelectBy_MaNhom?ma_nhom={ma_nhom}");
            return await response.Content.ReadFromJsonAsync<List<CauHoiDto>?>();
        }
    }
}
