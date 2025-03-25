using Azure;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        //private async Task onClickSuaSV(ChiTietCaThiDto chiTietCaThi)
        //{
        //    await getAllDeThi();
        //    displayCTCTMBSuaSV = chiTietCaThi;
        //    isShowMBSuaSV = true;
        //    StateHasChanged();
        //}
        //private async Task getAllDeThi()
        //{
        //    HttpResponseMessage? response = null;
        //    response = await Http.GetAsync($"api/ExamMonitor/GetAllDeThi?ma_ca_thi={caThi?.MaCaThi}");
        //    if (response != null && response.IsSuccessStatusCode)
        //    {
        //        var resultString = await response.Content.ReadAsStringAsync();
        //        listMaDes = JsonSerializer.Deserialize<List<long>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //    }
        //}
        //private void onClickThoatMBSuaSV()
        //{
        //    isShowMBSuaSV = false;
        //    StateHasChanged();
        //}
        //private async Task onCLickLuuMBSuaSV()
        //{
        //    if(displayCTCTMBSuaSV != null && displayCTCTMBSuaSV.MaSinhVienNavigation != null && MBSuaSV != null)
        //    {
        //        displayCTCTMBSuaSV.MaSinhVienNavigation.MaSoSinhVien = MBSuaSV.mssv;
        //        displayCTCTMBSuaSV.MaSinhVienNavigation.HoVaTenLot = MBSuaSV.hoVaTenLot;
        //        displayCTCTMBSuaSV.MaSinhVienNavigation.TenSinhVien = MBSuaSV.tenSV;
        //        displayCTCTMBSuaSV.MaDeThi = MBSuaSV.selected_ma_de;
        //        var jsonString = JsonSerializer.Serialize(displayCTCTMBSuaSV);
        //        HttpResponseMessage? response = null;
        //        response = await Http.PostAsync($"api/ExamMonitor/UpdateCTCT", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        //    }
        //    if (IsConnectHub() && caThi != null)
        //        await SendMessage(caThi.MaCaThi);
        //    onClickThoatMBSuaSV();
        //}
    }
}
