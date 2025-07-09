using Hutech.Exam.Client.Pages.Exam.Dialog;
using Hutech.Exam.Shared.DTO.Request.Custom;
using Microsoft.JSInterop;
using MudBlazor;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage
    {
        public async Task<DialogResult?> OpenLostFocusDialogAsync()
        {
            var parameters = new DialogParameters<LostFocusDialog>{};

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await Dialog.ShowAsync<LostFocusDialog>("Phát hiện rời khỏi trang thi", parameters, options);
            return await dialog.Result;
        }


        [JSInvokable]
        public async Task OnFocusLostAsync()
        {
            var result = await OpenLostFocusDialogAsync();
            TheSateHasChanged();
            if (result != null && !result.Canceled && result.Data != null)
            {
                if(result.Data is bool and true)
                {
                    await EndTimeSubmissionAsync();
                }    
            }    
        }

        [JSInvokable]
        public async Task EndTimeSubmissionAsync() // kết thúc thời gian làm bài
        {
            var DsKhoanh = SelectedAnswers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Item2);
            await StudentHub.RequestSubmit( new SubmitRequest { MaSinhVien = Students.MaSinhVien, MaChiTietCaThi = ExamSessionDetail.MaChiTietCaThi, MaDeThi = ExamSessionDetail.MaDeThi ?? -1, DapAnKhoanhs = DsKhoanh, ThoiGianNopBai = DateTime.Now });

            Nav?.NavigateTo("/result");
        }
    }
}
