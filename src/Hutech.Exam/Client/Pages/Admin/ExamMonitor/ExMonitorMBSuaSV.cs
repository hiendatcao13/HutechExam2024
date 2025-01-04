using Azure;
using Hutech.Exam.Shared.Models;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task onClickSuaSV(ChiTietCaThi chiTietCaThi)
        {
            await getAllDeThi();
            displayCTCTMBSuaSV = chiTietCaThi;
            isShowMBSuaSV = true;
            StateHasChanged();
        }
        private async Task getAllDeThi()
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.GetAsync($"api/ExamMonitor/GetAllDeThi?ma_ca_thi={ma_ca_thi}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                listMaDes = JsonSerializer.Deserialize<List<long>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private void onClickThoatMBSuaSV()
        {
            isShowMBSuaSV = false;
            StateHasChanged();
        }
        private async Task onCLickLuuMBSuaSV()
        {
            if(displayCTCTMBSuaSV != null && displayCTCTMBSuaSV.MaSinhVienNavigation != null && MBSuaSV != null)
            {
                displayCTCTMBSuaSV.MaSinhVienNavigation.MaSoSinhVien = MBSuaSV.mssv;
                displayCTCTMBSuaSV.MaSinhVienNavigation.HoVaTenLot = MBSuaSV.hoVaTenLot;
                displayCTCTMBSuaSV.MaSinhVienNavigation.TenSinhVien = MBSuaSV.tenSV;
                displayCTCTMBSuaSV.MaDeThi = MBSuaSV.selected_ma_de;
                var jsonString = JsonSerializer.Serialize(displayCTCTMBSuaSV);
                HttpResponseMessage? response = null;
                if (httpClient != null)
                    response = await httpClient.PostAsync($"api/ExamMonitor/UpdateCTCT", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            }
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
            onClickThoatMBSuaSV();
        }
    }
}
