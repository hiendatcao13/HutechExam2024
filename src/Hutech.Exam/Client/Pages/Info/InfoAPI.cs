﻿using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request;
using MudBlazor;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Info
{
    public partial class Info
    {
        private async Task<ChiTietCaThiDto?> GetChiTietCaThiAPI(long ma_sinh_vien)
        {
            // kiểm tra tham số
            if (ma_sinh_vien == -1)
                return null;
            var response = await Http.GetAsync($"api/ChiTietCaThi/SelectBy_MSSVThi?ma_sinh_vien={ma_sinh_vien}");
            return await response.Content.ReadFromJsonAsync<ChiTietCaThiDto?>();
        }
        private async Task<bool> UpdateLogoutAPI(SinhVienDto sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            var response = await Http.PutAsync($"api/SinhVien/UpdateLogout", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        private async Task<bool> UpdateBatDauThiAPI(ChiTietCaThiDto chiTietCaThi)
        {
            chiTietCaThi.ThoiGianBatDau = DateTime.Now;
            var jsonString = JsonSerializer.Serialize(Mapper.Map<ChiTietCaThiRequest>(chiTietCaThi));
            var response = await Http.PutAsync("api/ChiTietCaThi/UpdateBatDauThi", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
