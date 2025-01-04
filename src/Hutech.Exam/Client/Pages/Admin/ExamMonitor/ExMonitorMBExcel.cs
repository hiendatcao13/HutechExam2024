using Hutech.Exam.Client.Pages.Admin.MessageBox;
using Hutech.Exam.Shared.Models;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task getAllKhoa()
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.GetAsync($"api/ExamMonitor/GetAllKhoa");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                listKhoa = JsonSerializer.Deserialize<List<Khoa>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
        private async Task showMBThemSVExcel()
        {
            isShowMBExcel = true;
            await getAllKhoa();
            StateHasChanged();
        }
        private void onClickThoatMBThemSVExcel()
        {
            isShowMBExcel = false;
            if(MBThemSVExcel != null && MBThemSVExcel.sinhViens != null)
            {
                MBThemSVExcel.sinhViens.Clear();
            }
            StateHasChanged();
        }
        private async Task InsertListSV(int ma_khoa, List<SinhVien> sinhViens)
        {
            var jsonString = JsonSerializer.Serialize(sinhViens);
            HttpResponseMessage? response = null;
            bool isLoading = false;
            if (httpClient != null)
                response = await httpClient.PostAsync($"api/ExamMonitor/InsertListSV?ma_khoa={ma_khoa}&ma_ca_thi={ma_ca_thi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                isLoading = JsonSerializer.Deserialize<bool>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            if (isLoading) { js?.InvokeVoidAsync("alert", "Thêm sinh viên hoàn tất"); }
            else { js?.InvokeVoidAsync("alert", "Thêm sinh viên thất bại"); }
        }
        private async Task onClickLuuMBThemSVExcel()
        {
            if(MBThemSVExcel != null && MBThemSVExcel.sinhViens != null)
            {
                await InsertListSV(MBThemSVExcel.ma_khoa_selected, MBThemSVExcel.sinhViens);
            }
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
            onClickThoatMBThemSVExcel();
        }
    }
}
