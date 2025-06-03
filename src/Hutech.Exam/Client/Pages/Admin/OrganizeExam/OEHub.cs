using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        private async Task CreateHubConnection()
        {
            //hubConnection = await AdminHub.GetConnectionAsync();
            //// thao tác với đợt thi
            //hubConnection.On<int>("InsertDotThi", async (ma_dot_thi) =>
            //{
            //    await CallLoadInsertDotThi(ma_dot_thi);
            //    StateHasChanged();
            //});
            //hubConnection.On<int>("UpdateDotThi", async (ma_dot_thi) =>
            //{
            //    await CallLoadUpdateDotThi(ma_dot_thi);
            //    StateHasChanged();
            //});
            //hubConnection.On<int>("DeleteDotThi", (ma_dot_thi) =>
            //{
            //    CallLoadDeleteDotThi(ma_dot_thi);
            //    StateHasChanged();
            //});

            //// Thao tác với chi tiết đợt thi
            //hubConnection.On<int>("InsertCTDotThi", async (ma_chi_tiet_dot_thi) =>
            //{
            //    await CallLoadInserCTDotThi(ma_chi_tiet_dot_thi);
            //    StateHasChanged();
            //});
            //hubConnection.On<int>("UpdateCTDotThi", async (ma_chi_tiet_dot_thi) =>
            //{
            //    await CallLoadUpdateCTDotThi(ma_chi_tiet_dot_thi);
            //    StateHasChanged();
            //});
            //hubConnection.On<int>("DeleteCTDotThi", (ma_chi_tiet_dot_thi) =>
            //{
            //    CallLoadDeleteCTDotThi(ma_chi_tiet_dot_thi);
            //    StateHasChanged();
            //});

            //// Thao tác với ca thi
            //hubConnection.On<int>("InsertCaThi", async (ma_ca_thi) =>
            //{
            //    await CallLoadInsertCaThi(ma_ca_thi);
            //    StateHasChanged();
            //});
            //hubConnection.On<int>("UpdateCaThi", async (ma_ca_thi) =>
            //{
            //    await CallLoadUpdateCaThi(ma_ca_thi);
            //    StateHasChanged();
            //});
            //hubConnection.On<int>("DeleteCaThi", (ma_ca_thi) =>
            //{
            //    CallLoadDeleteCaThi(ma_ca_thi);
            //    StateHasChanged();
            //});

            //1 số thành phần khác không thuộc ở trang này
        }


        private async Task CallLoadInsertCaThi(int ma_ca_thi)
        {
            CaThiDto? caThi = await CaThi_SelectOneAPI(ma_ca_thi);
            if(caThi != null && caThi.MaChiTietDotThi == selectedChiTietDotThi?.MaChiTietDotThi)
                caThis?.Add(caThi);
        }
        private async Task CallLoadUpdateCaThi(int ma_ca_thi)
        {
            // kiểm tra xem có tồn tại mã ca thi trong phiên làm việc không
            CaThiDto? existingCaThi = caThis?.FirstOrDefault(x => x.MaCaThi == ma_ca_thi);
            if(existingCaThi != null)
            {
                CaThiDto? caThi = await CaThi_SelectOneAPI(ma_ca_thi);
                if (caThi != null)
                {
                    caThis?.Remove(existingCaThi);
                    caThis?.Add(caThi);
                }
            }
        }
        private void CallLoadDeleteCaThi(int ma_ca_thi)
        {
            // kiểm tra xem có tồn tại mã ca thi trong phiên làm việc không
            CaThiDto? existingCaThi = caThis?.FirstOrDefault(x => x.MaCaThi == ma_ca_thi);
            if (existingCaThi != null)
                caThis?.Remove(existingCaThi);
        }

        private async Task CallLoadInserCTDotThi(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto? chiTietDotThi = await ChiTietDotThi_SelectOneAPI(ma_chi_tiet_dot_thi);
            if (chiTietDotThi != null && chiTietDotThi.MaDotThi == selectedDotThi?.MaDotThi)
                chiTietDotThis?.Add(chiTietDotThi);
        }
        private async Task CallLoadUpdateCTDotThi(int ma_chi_tiet_dot_thi)
        {
            // kiểm tra xem có tồn tại mã chi tiết đợt thi trong phiên làm việc không
            ChiTietDotThiDto? existingChiTietDotThi = chiTietDotThis?.FirstOrDefault(x => x.MaChiTietDotThi == ma_chi_tiet_dot_thi);
            if (existingChiTietDotThi != null)
            {
                ChiTietDotThiDto? chiTietDotThi = await ChiTietDotThi_SelectOneAPI(ma_chi_tiet_dot_thi);
                if (chiTietDotThi != null)
                {
                    chiTietDotThis?.Remove(existingChiTietDotThi);
                    chiTietDotThis?.Add(chiTietDotThi);
                }
            }
        }
        private void CallLoadDeleteCTDotThi(int ma_chi_tiet_dot_thi)
        {
            // kiểm tra xem có tồn tại mã chi tiết đợt thi trong phiên làm việc không
            ChiTietDotThiDto? existingChiTietDotThi = chiTietDotThis?.FirstOrDefault(x => x.MaChiTietDotThi == ma_chi_tiet_dot_thi);
            if (existingChiTietDotThi != null)
                chiTietDotThis?.Remove(existingChiTietDotThi);
        }

        private async Task CallLoadInsertDotThi(int ma_dot_thi)
        {
            DotThiDto? dotThi = await DotThi_SelectOneAPI(ma_dot_thi);
            if (dotThi != null)
                dotThis?.Add(dotThi);
        }
        private async Task CallLoadUpdateDotThi(int ma_dot_thi)
        {
            // kiểm tra xem có tồn tại mã đợt thi trong phiên làm việc không
            DotThiDto? existingDotThi = dotThis?.FirstOrDefault(x => x.MaDotThi == ma_dot_thi);
            if (existingDotThi != null)
            {
                DotThiDto? dotThi = await DotThi_SelectOneAPI(ma_dot_thi);
                if (dotThi != null)
                {
                    dotThis?.Remove(existingDotThi);
                    dotThis?.Add(dotThi);
                }
            }
        }
        private void CallLoadDeleteDotThi(int ma_dot_thi)
        {
            // kiểm tra xem có tồn tại mã đợt thi trong phiên làm việc không
            DotThiDto? existingDotThi = dotThis?.FirstOrDefault(x => x.MaDotThi == ma_dot_thi);
            if (existingDotThi != null)
                dotThis?.Remove(existingDotThi);
        }


    }
}
