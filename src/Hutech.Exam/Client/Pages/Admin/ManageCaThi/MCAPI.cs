﻿using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
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

        private async Task<ChiTietDotThiDto?> ChiTietDotThis_SelectBy_MaDotThi_MaLopAo_LanThiAPI(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            var response = await SenderAPI.GetAsync<ChiTietDotThiDto>($"api/chitietdotthis/filter-by-dotthi-lopao-lanthi?maDotThi={ma_dot_thi}&maLopAo={ma_lop_ao}&lanThi={lan_thi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<(List<CaThiDto>?, int, int)> CaThis_SelectBy_MaChiTietDotThi_PagedAPI(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<CaThiDto>>($"api/cathis/filter-by-chitietdotthi-paged?maChiTietDotThi={ma_chi_tiet_dot_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }

        private async Task<(List<CaThiDto>?, int, int)> CaThis_SelectBy_MaChiTietDotThi_Search_PagedAPI(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<CaThiDto>>($"api/cathis/filter-by-chitietdotthi-search-paged?maChiTietDotThi={ma_chi_tiet_dot_thi}&keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : ([], 0, 0);
        }
        
    }
}
