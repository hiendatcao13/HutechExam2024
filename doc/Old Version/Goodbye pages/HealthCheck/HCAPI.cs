using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Client.Pages.Admin.HealthCheck
{
    public partial class HealthCheck
    {

        private async Task<List<CustomHealthCheck>> GetHealthCheckConnectionAPI()
        {
            var response = await SenderAPI.GetAsync<List<CustomHealthCheck>>($"api/healths");
            return (response.Success && response.Data != null) ? response.Data : [];
        }

        private async Task<List<CustomThongKeDoPhanManh>> GetHealthCheckFragmentAPI()
        {
            var response = await SenderAPI.GetAsync<List<CustomThongKeDoPhanManh>>($"api/healths/fragments");
            return (response.Success && response.Data != null) ? response.Data : [];
        }

        private async Task<bool> RebuildOrReorganizeIndexAPI()
        {
            var response = await SenderAPI.PutAsync<bool>("api/healths/reorganize-index", null);
            return (response.Success);
        }
    }
}
