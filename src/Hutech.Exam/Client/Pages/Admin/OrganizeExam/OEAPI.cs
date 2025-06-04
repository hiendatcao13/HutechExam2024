using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
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

        private async Task<(List<DotThiDto>?, int, int)> DotThis_GetAllAPI(int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<DotThiDto>>($"api/dotthis?pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<(List<ChiTietDotThiDto>?, int, int)> ChiTietDotThis_SelectBy_MaDotThi_PagedAPI(int ma_dot_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<ChiTietDotThiDto>>($"api/chitietdotthis/filter-by-dotthi-paged?maDotThi={ma_dot_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<(List<CaThiDto>?, int, int)> CaThis_SelectBy_MaChiTietDotThi_PagedAPI(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<CaThiDto>>($"api/cathis/filter-by-chitietdotthi-paged?maChiTietDotThi={ma_chi_tiet_dot_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }



        private async Task<bool> DeleteDotThiAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}");
            return response.Success;
        }

        private async Task<bool> ForceDeleteDotThiAPI(int ma_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<DotThiDto>($"api/dotthis/{ma_dot_thi}/force");
            return response.Success;
        }

        private async Task<bool> DeleteCTDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_dot_thi}");
            return response.Success;
        }

        private async Task<bool> ForceDeleteCTDotThiAPI(int ma_chi_tiet_dot_thi)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietDotThiDto>($"api/chitietdotthis/{ma_chi_tiet_dot_thi}/force");
            return response.Success;
        }

        private async Task<bool> DeleteCaThiAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.DeleteAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return response.Success;
        }

        private async Task<bool> ForceDeleteCaThiAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.DeleteAsync<CaThiDto>($"api/cathis/{ma_ca_thi}/force");
            return response.Success;
        }

    }
}
