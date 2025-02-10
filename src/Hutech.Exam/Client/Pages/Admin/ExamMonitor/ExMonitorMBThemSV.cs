using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task onClickLuuMBThemSV()
        {
            if (httpClient != null)
                await httpClient.PostAsync($"api/ExamMonitor/InsertCTCT?ma_ca_thi={ma_ca_thi}&ma_sinh_vien={sinhVienMBThemSV?.MaSinhVien}", null);
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
            onClickThoatMBThemSV();
        }
        private void onChangeInputMSSV(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "" && chiTietCaThis != null)
                    displayChiTietCaThis = chiTietCaThis.ToList();
                else if (displayChiTietCaThis != null && chiTietCaThis != null)
                {
                    displayChiTietCaThis.Clear();
                    string temp = "" + e.Value.ToString();
                    var item = chiTietCaThis.Where(p =>
                        p.MaSinhVienNavigation != null &&
                        p.MaSinhVienNavigation.MaSoSinhVien != null &&
                        p.MaSinhVienNavigation.MaSoSinhVien.Contains(temp)
                    ).ToList();
                    displayChiTietCaThis.AddRange(item);
                }
            }
            StateHasChanged();
        }
        private async Task onClickCheckSV()
        {
            if (chiTietCaThis != null && MBThemSV != null)
            {
                if (MBThemSV.MSSV == null)
                {
                    js?.InvokeVoidAsync("alert", "Vui lòng nhập đầy đủ thông tin");
                    return;
                }
                SinhVienDto? sinhVien = chiTietCaThis.FirstOrDefault(p => p.MaSinhVienNavigation?.MaSoSinhVien == MBThemSV.MSSV)?.MaSinhVienNavigation;
                if (sinhVien != null)
                {
                    js?.InvokeVoidAsync("alert", "Sinh viên này đã nằm trong ca thi. Vui lòng kiểm tra");
                    return;
                }
            }
            HttpResponseMessage? response = null;
            if (httpClient != null && MBThemSV != null)
                response = await httpClient.GetAsync($"api/Admin/GetThongTinSinhVien?ma_so_sinh_vien={MBThemSV.MSSV}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                sinhVienMBThemSV = JsonSerializer.Deserialize<SinhVienDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            if (sinhVienMBThemSV != null && sinhVienMBThemSV.TenSinhVien != null && MBThemSV != null)
            {
                MBThemSV.is_existMSSV = true;
                MBThemSV.hoTenLot = sinhVienMBThemSV.HoVaTenLot;
                MBThemSV.tenSinhVien = sinhVienMBThemSV.TenSinhVien;
            }
            else if (MBThemSV != null)
                MBThemSV.is_existMSSV = false;
            StateHasChanged();
        }
        private async Task onClickThemSV()
        {
            bool confirmResult = await js.InvokeAsync<bool>("confirm", "Bạn muốn thêm sinh viên bằng file excel?");
            if (!confirmResult)
            {
                if (MBThemSV != null)
                    MBThemSV.is_existMSSV = null;
                isShowMBThemSV = true;
            }
            else
            {
                await showMBThemSVExcel();
            }
            StateHasChanged();
        }
        private async Task taoMoiSV(string? ten_lop, SinhVien sinhVien)
        {
            var jsonString = JsonSerializer.Serialize(sinhVien);
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.PostAsync($"api/ExamMonitor/InsertSV?ten_lop={ten_lop}&ma_ca_thi={ma_ca_thi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
        private async Task onClickTaoMoiSV()
        {
            if(MBThemSV != null && (MBThemSV.tenSinhVien.IsNullOrEmpty() || MBThemSV.hoTenLot.IsNullOrEmpty() || MBThemSV.MSSV.IsNullOrEmpty()))
            {
                js?.InvokeVoidAsync("alert", "Vui lòng nhập đầy đủ thông tin");
                return;
            }
            SinhVien sv = new SinhVien();
            if (MBThemSV != null)
            {
                MBThemSV.lop = (MBThemSV.lop.IsNullOrEmpty()) ? "" : MBThemSV.lop?.ToUpper();
                sv.HoVaTenLot = MBThemSV.hoTenLot; sv.TenSinhVien = MBThemSV.tenSinhVien;
                sv.GioiTinh = (short?)((MBThemSV.isMale) ? 0 : 1);
                sv.MaSoSinhVien = MBThemSV.MSSV;
                await taoMoiSV(MBThemSV.lop, sv);
            }
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
            onClickThoatMBThemSV();
        }
    }
}
