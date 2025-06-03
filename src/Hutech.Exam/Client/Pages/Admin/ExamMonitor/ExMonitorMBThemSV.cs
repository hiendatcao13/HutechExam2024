using Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog;
using Hutech.Exam.Client.Pages.Admin.ManageCaThi;
using Hutech.Exam.Client.Pages.Result;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using MudBlazor;


namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task OnClickThemSV()
        {
            var result = await OpenThemSVDialog();
            if (result != null && !result.Canceled && result.Data != null && chiTietCaThis != null)
            {
                var newChiTietCaThi = (ChiTietCaThiDto)result.Data;
                chiTietCaThis.Add(newChiTietCaThi);
            }
        }
        private async Task<DialogResult?> OpenThemSVDialog()
        {
            
            Snackbar.Add(ALERT_ADDSV, Severity.Info);
            string?[] content_texts = [caThi?.TenCaThi ?? "", caThi?.ThoiGianBatDau.ToString() ?? "", caThi?.ThoiGianThi.ToString() ?? ""];
            var parameters = new DialogParameters<ThemSVDialog>
            {
                { x => x.maMSSVs, GetMSSVs() },
                { x => x.maDeHVs, GetMaDeThis() },
                { x => x.ma_ca_thi, caThi?.MaCaThi},
                { x => x.lop, await GetLop()}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<ThemSVDialog>("Thêm SV khẩn cấp", parameters, options);
            return await dialog.Result;
        }

        private List<long> GetMaDeThis()
        {
            List<long> result = [];
            if (chiTietCaThis != null)
            {
                foreach (var item in chiTietCaThis)
                {
                    if (item.MaDeThi != null)
                        result.Add((long)item.MaDeThi);
                }
            }
            return result;
        }
        private List<string> GetMSSVs()
        {
            List<string> result = [];
            if (chiTietCaThis != null)
            {
                foreach (var item in chiTietCaThis)
                {
                    if (item.MaSinhVienNavigation != null && item.MaSinhVienNavigation.MaSoSinhVien != null)
                        result.Add((string)item.MaSinhVienNavigation.MaSoSinhVien);
                }
            }
            return result;
        }
        private async Task<string?> GetLop()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataMC>("storedDataMC");
            return storedData.LopAo?.TenLopAo;
        }
    }
}
