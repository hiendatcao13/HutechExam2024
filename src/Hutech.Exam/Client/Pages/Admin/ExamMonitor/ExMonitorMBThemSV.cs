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
            if (result != null && !result.Canceled && result.Data != null && examSessionDetails != null)
            {
                var newChiTietCaThi = (ChiTietCaThiDto)result.Data;
                examSessionDetails.Add(newChiTietCaThi);

                // cập nhật lại ca thi
                examSession = await ExamSession_SelectOneAPI(examSession!.MaCaThi);
                await SessionStorage.SetItemAsync("CaThi", examSession);
            }
        }
        private async Task<DialogResult?> OpenAddStudentDialogAsync()
        {

            Snackbar.Add(ALERT_ADDSV, Severity.Info);
            string?[] content_texts = [examSession?.TenCaThi ?? "", examSession?.ThoiGianBatDau.ToString() ?? "", examSession?.ThoiGianThi.ToString() ?? ""];
            var parameters = new DialogParameters<AddStudentDialog>
            {
                { x => x.StudentCodes, GetAllStudentCodes() },
                { x => x.ShuffleExams, GetAllShuffleExamIds() },
                { x => x.examSessionId, examSession?.MaCaThi},
                { x => x.ExamSession, examSession},
                { x => x.classRoom, await GetClassRoom()}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<AddStudentDialog>("Thêm SV khẩn cấp", parameters, options);
            return await dialog.Result;
        }

        private List<long> GetAllShuffleExamIds()
        {
            HashSet<long> uniqueIds = new HashSet<long>();
            if (examSessionDetails != null)
            {
                foreach (var item in examSessionDetails)
                {
                    if (item.MaDeThi != null)
                        uniqueIds.Add((long)item.MaDeThi); // HashSet tự loại bỏ trùng lặp
                }
            }
            return uniqueIds.ToList();
        }
        private List<string> GetAllStudentCodes()
        {
            List<string> result = [];
            if (examSessionDetails != null)
            {
                foreach (var item in examSessionDetails)
                {
                    if (item.MaSinhVienNavigation != null && item.MaSinhVienNavigation.MaSoSinhVien != null)
                        result.Add((string)item.MaSinhVienNavigation.MaSoSinhVien);
                }
            }
            return result;
        }
        private async Task<string?> GetClassRoom()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataME>("storedDataME");
            return storedData.LopAo?.TenLopAo;
        }
    }
}
