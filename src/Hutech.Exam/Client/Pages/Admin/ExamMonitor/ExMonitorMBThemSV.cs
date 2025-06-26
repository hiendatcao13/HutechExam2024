using Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog;
using Hutech.Exam.Client.Pages.Admin.ManageExamSession;
using Hutech.Exam.Shared.DTO;
using MudBlazor;


namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task OnClickAddStudentAsync()
        {
            var result = await OpenAddStudentDialogAsync();
            if (result != null && !result.Canceled && result.Data != null && chiTietCaThis != null)
            {
                var newChiTietCaThi = (ChiTietCaThiDto)result.Data;
                chiTietCaThis.Add(newChiTietCaThi);
            }
        }
        private async Task<DialogResult?> OpenAddStudentDialogAsync()
        {
            
            Snackbar.Add(ALERT_ADDSV, Severity.Info);
            string?[] content_texts = [caThi?.TenCaThi ?? "", caThi?.ThoiGianBatDau.ToString() ?? "", caThi?.ThoiGianThi.ToString() ?? ""];
            var parameters = new DialogParameters<AddStudentDialog>
            {
                { x => x.StudentCodes, GetAllStudentCodes() },
                { x => x.ShuffleExams, GetAllShuffleExamIds() },
                { x => x.examSessionId, caThi?.MaCaThi},
                { x => x.classRoom, await GetClassRoom()}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<AddStudentDialog>("Thêm SV khẩn cấp", parameters, options);
            return await dialog.Result;
        }

        private List<long> GetAllShuffleExamIds()
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
        private List<string> GetAllStudentCodes()
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
        private async Task<string?> GetClassRoom()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataME>("storedDataMC");
            return storedData.LopAo?.TenLopAo;
        }
    }
}
