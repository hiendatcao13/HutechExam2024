using Hutech.Exam.Client.Pages.Exam.Dialog;
using Hutech.Exam.Shared.DTO.Request.Custom;
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
            var DsKhoanh = DSKhoanhDapAn.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Item2);
            await StudentHub.RequestSubmit( new SubmitRequest { MaSinhVien = SinhVien.MaSinhVien, MaChiTietCaThi = ChiTietCaThi.MaChiTietCaThi, MaDeThiHoanVi = ChiTietCaThi.MaDeThi ?? -1, DapAnKhoanhs = DsKhoanh, ThoiGianNopBai = DateTime.Now });

            // lưu dự phòng ở đây
            await SaveData(DsKhoanh);

            Nav?.NavigateTo("/result");
        }

        // save data cho trường hợp lỗi nặng
        public async Task SaveData(Dictionary<int, int?> dsKhoanh)
        {
            var selectData = new SubmitRequest { IsLanDau = false, MaSinhVien = SinhVien.MaSinhVien, MaChiTietCaThi = ChiTietCaThi.MaChiTietCaThi, MaDeThiHoanVi = ChiTietCaThi.MaDeThi ?? -1, DapAnKhoanhs = dsKhoanh, ThoiGianNopBai = DateTime.Now, DsDapAnDuPhong = _dsThiSinhDaKhoanh, IsRecoverySubmit = true };
            await SessionStorage.SetItemAsync("SubmitRequest", selectData);
        }
    }
}
