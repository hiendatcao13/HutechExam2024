using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task onClickMBLuu()
        {
            if (displayChiTietCaThi != null && MBCongGio != null && MBCongGio.thoiGianCongThem != null)
            {
                displayChiTietCaThi.ThoiDiemCong = DateTime.Now;
                displayChiTietCaThi.LyDoCong = MBCongGio.lyDoCong;
                displayChiTietCaThi.GioCongThem = (int)MBCongGio.thoiGianCongThem;
                await congGioSinhVien(displayChiTietCaThi);
            }
            onClickMBThoat();
        }

        private void onClickMBThoat()
        {
            isShowMessageBox = false;
            StateHasChanged();
        }
        private void onClickThoatMBThemSV()
        {
            isShowMBThemSV = false;
            StateHasChanged();
        }

        private async Task onClickCongGioThem(ChiTietCaThiDto chiTietCaThi)
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Cộng giờ thêm được dùng trong trường hợp thí sinh bị treo máy hoặc nguyên nhân thích đáng khác");
            if (result && js != null)
            {
                isShowMessageBox = true;
                displayChiTietCaThi = chiTietCaThi;
            }
        }


    }
}
