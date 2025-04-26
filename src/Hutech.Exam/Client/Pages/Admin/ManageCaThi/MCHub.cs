using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
    {
        private async Task CreateHubConnection()
        {
            hubConnection = await AdminHub.GetConnectionAsync();

            hubConnection.On<int>("UpdateCaThi", async (ma_ca_thi) =>
            {
                if(caThis != null && caThis.Exists(x => x.MaCaThi == ma_ca_thi))
                    await CallLoadUpdateCaThi(ma_ca_thi);
                StateHasChanged();
            });
        }

        private async Task CallLoadUpdateCaThi(int ma_ca_thi)
        {
            // kiểm tra xem có tồn tại mã ca thi trong phiên làm việc không
            CaThiDto? existingCaThi = caThis?.FirstOrDefault(x => x.MaCaThi == ma_ca_thi);
            if (existingCaThi != null)
            {
                CaThiDto? caThi = await CaThi_SelectOneAPI(ma_ca_thi);
                if (caThi != null)
                {
                    caThis?.Remove(existingCaThi);
                    caThis?.Add(caThi);
                }
            }
        }
    }
}
