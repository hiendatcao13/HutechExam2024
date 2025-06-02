using Hutech.Exam.Shared.DTO;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        private async Task<CaThiDto?> CaThi_SelectOneAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.GetAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<ChiTietDotThiDto?> ChiTietDotThi_SelectOneAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await SenderAPI.GetAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_ca_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<DotThiDto?> DotThi_SelectOneAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.GetAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}");
            return (response.Success) ? response.Data : null;
        }



        private async Task<List<DotThiDto>?> DotThis_GetAllAPI()
        {
            var response = await SenderAPI.GetAsync<List<DotThiDto>>("api/dotthis");
            return (response.Success) ? response.Data : null;
        }
        private async Task<List<ChiTietDotThiDto>?> ChiTietDotThis_SelectBy_MaDotThiAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.GetAsync<List<ChiTietDotThiDto>>($"api/chitietdotthis/filter-by-dotthi?maDotThi={ma_dot_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<List<CaThiDto>?> CaThis_SelectBy_MaChiTietDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await SenderAPI.GetAsync<List<CaThiDto>>($"api/cathis/filter-by-chitietdotthi?maChiTietDotThi={ma_chi_tiet_dot_thi}");
            return (response.Success) ? response.Data : null;
        }




        private async Task<bool> DeleteDotThiAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}");
            return response.Success;
        }
        private async Task<bool> DeleteCTDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_dot_thi}");
            return response.Success;
        }
        private async Task<bool> DeleteCaThiAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.DeleteAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return response.Success;
        }
    }
}
