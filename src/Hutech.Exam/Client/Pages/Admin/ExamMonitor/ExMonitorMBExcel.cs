using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog
{
    public partial class ExamMonitor
    {
        //private async Task ShowMBThemSVExcel()
        //{
        //    isShowMBExcel = true;
        //    listKhoa = await GetAllKhoaAPI();
        //    StateHasChanged();
        //}
        //private void OnClickThoatMBThemSVExcel()
        //{
        //    isShowMBExcel = false;
        //    if(MBThemSVExcel != null && MBThemSVExcel.sinhViens != null)
        //    {
        //        MBThemSVExcel.sinhViens.Clear();
        //    }
        //    StateHasChanged();
        //}
        //private async Task InsertListSV(int ma_khoa, List<SinhVienDto> sinhViens)
        //{
        //    var jsonString = JsonSerializer.Serialize(sinhViens);
        //    HttpResponseMessage? response = null;
        //    bool isLoading = false;
        //    response = await Http.PostAsync($"api/ExamMonitor/InsertListSV?ma_khoa={ma_khoa}&ma_ca_thi={caThi?.MaCaThi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        //    if (response != null && response.IsSuccessStatusCode)
        //    {
        //        var resultString = await response.Content.ReadAsStringAsync();
        //        isLoading = JsonSerializer.Deserialize<bool>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //    }
        //    //if (isLoading)
        //    //    Snackbar.Add(SUCCESS_ADDSV, MudBlazor.Severity.Success);
        //    //else
        //    //    Snackbar.Add(ERROR_ADDSV, MudBlazor.Severity.Error);
        //}
        //private async Task OnClickLuuMBThemSVExcel()
        //{
        //    if(MBThemSVExcel != null && MBThemSVExcel.sinhViens != null)
        //    {
        //        await InsertListSV(MBThemSVExcel.ma_khoa_selected, MBThemSVExcel.sinhViens);
        //    }
        //    if (IsConnectHub() && caThi != null)
        //        await SendMessage(caThi.MaCaThi);
        //    OnClickThoatMBThemSVExcel();
        //}
    }
}
