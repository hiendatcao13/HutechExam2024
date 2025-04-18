﻿using Hutech.Exam.Client.Components.MessageBox;
using Hutech.Exam.Client.Pages.Admin.ManageCaThi;
using MudBlazor;


namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task OnClickThemSV()
        {
            Snackbar.Add(ALERT_ADDSV, Severity.Info);
            string?[] content_texts = [caThi?.TenCaThi ?? "", caThi?.ThoiGianBatDau.ToString() ?? "", caThi?.ThoiGianThi.ToString() ?? ""];
            var parameters = new DialogParameters<ThemSVDialog>
            {
                { x => x.contexts, content_texts },
                { x => x.maMSSVs, GetMSSVs() },
                { x => x.maDeHVs, GetMaDeThis() },
                { x => x.ma_ca_thi, caThi?.MaCaThi},
                { x => x.lop, await GetLop()}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<ThemSVDialog>("Thêm SV khẩn cấp", parameters, options);
            if (IsConnectHub())
                await SendMessage(caThi?.MaCaThi ?? -1);
        }
        private List<long> GetMaDeThis()
        {
            List<long> result = [];
            if(chiTietCaThis != null)
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
            if(chiTietCaThis != null)
            {
                foreach(var item in chiTietCaThis)
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
