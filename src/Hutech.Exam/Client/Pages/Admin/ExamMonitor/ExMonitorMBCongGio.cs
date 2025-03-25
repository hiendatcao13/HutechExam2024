using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Client.Components.MessageBox;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task OnClickCongGioThem(ChiTietCaThiDto chiTietCaThi)
        {
            var parameters = new DialogParameters<CongGioDialog>
            {
                { x => x.chiTietCaThi, chiTietCaThi },
                { x => x.caThi, caThi }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<CongGioDialog>("Cộng giờ thi", parameters, options);
        }
    }
}
