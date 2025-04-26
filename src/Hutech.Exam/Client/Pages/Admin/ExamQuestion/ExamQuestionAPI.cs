using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public partial class ExamQuestion
    {
        private async Task<List<MonHocDto>?> GetAllMonHocAPI()
        {
            var response = await Http.GetAsync("api/MonHoc/GetAll");
            return await response.Content.ReadFromJsonAsync<List<MonHocDto>?>();
        }
        private async Task<List<DeThiDto>?> GetAllDeThiAPIByMaMonHocAPI(int ma_mon_hoc)
        {
            var response = await Http.GetAsync($"api/DeThi/SelectByMonHoc?ma_mon_hoc={ma_mon_hoc}");
            return await response.Content.ReadFromJsonAsync<List<DeThiDto>?>();
        }
    }
}
