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
        //[JSInvokable] // Đánh dấu hàm để có thể gọi từ JavaScript
        //public static Task<int> GetDapAnFromJavaScript(int vi_tri_cau_hoi, int ma_cau_tra_loi, int ma_nhom, int ma_cau_hoi)
        //{
        //    // Xử lý giá trị được truyền từ JavaScript
        //    if (listDapAn != null)
        //        listDapAn.Add(ma_cau_tra_loi);

        //    ChiTietBaiThiRequest? findChiTietBaiThi = chiTietBaiThis?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
        //    ChiTietBaiThiRequest tempChiTietBaiThi = GetPropertyCTBT(vi_tri_cau_hoi, ma_cau_tra_loi, ma_nhom, ma_cau_hoi);

        //    // trường hợp thí sinh sửa đáp án của câu đã trả lời trước đó
        //    if (findChiTietBaiThi != null && chiTietBaiThis != null)
        //    {
        //        findChiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
        //        //tempChiTietBaiThi.ThuTu = 0; // biến này để đánh dấu cho server biết câu này đã được insert, chỉ cần update
        //    }
        //    else
        //        chiTietBaiThis?.Add(tempChiTietBaiThi);

        //    // trường hợp sinh viên lại khoanh lại đáp án nhiều lần trong 1 lần lưu
        //    ChiTietBaiThiRequest? chiTietBaiThi = dsBaiThi_Update?.FirstOrDefault(p => p.MaNhom == ma_nhom && p.MaCauHoi == ma_cau_hoi);
        //    if (chiTietBaiThi != null)
        //    {
        //        chiTietBaiThi.CauTraLoi = ma_cau_tra_loi;
        //    }
        //    else
        //    {
        //        dsBaiThi_Update?.Add(tempChiTietBaiThi);
        //    }

        //    return Task.FromResult<int>(ma_cau_tra_loi);
        //}
        [JSInvokable]
        public async Task KetThucThoiGianLamBai()
        {
            await UpdateChiTietBaiThiAPI();

            if (chiTietBaiThis != null && listDapAn != null)
            {
                MyData.ChiTietBaiThis = chiTietBaiThis;
                MyData.ListDapAnKhoanh = listDapAn;
            }
            Nav?.NavigateTo("/result");
        }
    }
}
