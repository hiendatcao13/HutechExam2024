﻿using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        // curentPage mặc định của MudTable là 0, trang đầu là 0
        private async Task<CaThiDto?> ExamSession_SelectOneAPI(int ma_ca_thi)
        {
            var response = await SenderAPI.GetAsync<CaThiDto>($"api/cathis/{ma_ca_thi}");
            return (response.Success) ? response.Data : null;
        }
        private async Task<(List<ChiTietCaThiDto>, int, int)> ExamSessionDetails_SelectBy_ExamSessionId_PagedAPI(int ma_ca_thi, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<ChiTietCaThiDto>>($"api/chitietcathis/filter-by-cathi-paged?maCaThi={ma_ca_thi}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalRecords, response.Data.TotalPages) : ([], 0, 0);
        }
        private async Task<(List<ChiTietCaThiDto>, int, int)> ExamSessionDetails_SelectBy_ExamSessionId_Search_PagedAPI(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<ChiTietCaThiDto>>($"api/chitietcathis/filter-by-cathi-search-paged?maCaThi={ma_ca_thi}&keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalRecords, response.Data.TotalPages) : ([], 0, 0);
        }

        private async Task<bool> ResetLoginAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PatchAsync<SinhVienDto>($"api/sinhviens/{ma_sinh_vien}/reset-login", null);
            return response.Success;
        }

        private async Task<bool> SubmitAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PutAsync<SinhVienDto>($"api/sinhviens/{ma_sinh_vien}/submit-exam", null);
            return response.Success;
        }

        private async Task<byte[]?> GetExcelFileAPI(int maCaThi, CaThiExportFileRequest request)
        {
            var response = await SenderAPI.PostAsync<byte[]>($"api/cathis/{maCaThi}/export-excel", request);
            return (response.Success) ? response.Data : [];
        }

        private async Task<byte[]?> GetPdfFileAPI(int maCaThi, CaThiExportFileRequest request)
        {
            var response = await SenderAPI.PostAsync<byte[]>($"api/cathis/{maCaThi}/export-pdf", request);
            return (response.Success) ? response.Data : [];
        }

        private async Task<bool> ExamSessionDetail_DeleteAPI(int examSessionId)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietCaThiDto>($"api/chitietcathis/{examSessionId}");
            return response.Success;
        }

        private async Task<bool> ExamSessionDetail_ForceDeleteAPI(int examSessionId)
        {
            var response = await SenderAPI.DeleteAsync<ChiTietCaThiDto>($"api/chitietcathis/{examSessionId}/force");
            return response.Success;
        }

        private async Task<bool> ExamSession_UpdateAudit(int examSessionId, string history_action)
        {
            var response = await SenderAPI.PatchAsync<CaThiDto>($"api/cathis/{examSessionId}/update-audit", history_action);
            return response.Success;
        }
    }
}
