using Hutech.Exam.Client.Pages.Exam.Dialog;
using Hutech.Exam.Shared.DTO.Request;
using Microsoft.JSInterop;
using MudBlazor;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage
    {
        public async Task<DialogResult?> OpenLostFocusDialog()
        {
            var parameters = new DialogParameters<LostFocusDialog>{};

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await Dialog.ShowAsync<LostFocusDialog>("Phát hiện rời khỏi trang thi", parameters, options);
            return await dialog.Result;
        }


        [JSInvokable]
        public async Task OnFocusLost()
        {
            var result = await OpenLostFocusDialog();
            if (result != null && !result.Canceled && result.Data != null)
            {
                if(result.Data is bool and true)
                {
                    await KetThucThoiGianLamBai();
                }    
            }    
        }

        [JSInvokable]
        public async Task KetThucThoiGianLamBai()
        {
            await StudentHub.RequestSubmit( new SubmitRequest { MaSinhVien = SinhVien.MaSinhVien, MaChiTietCaThi = ChiTietCaThi.MaChiTietCaThi, MaDeThiHoanVi = ChiTietCaThi.MaDeThi ?? -1, DapAnKhoanhs = DSKhoanhDapAn, ThoiGianNopBai = DateTime.Now });

            Nav?.NavigateTo("/result");
        }
    }
}
