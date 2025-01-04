using Microsoft.AspNetCore.Components;

namespace Hutech.Exam.Client.Pages.Admin.MessageBox
{
    public partial class MBThemSV
    {
        [Parameter]
        public string? tenCaThi { get; set; }
        [Parameter]
        public string? tenMonThi { get; set; }
        [Parameter]
        public DateTime? ngayThi { get; set; }
        [Parameter]
        public int? thoiLuongThi { get; set; }
        public bool? is_existMSSV { get; set; }
        public string? tenSinhVien { get; set; }
        public string? hoTenLot { get; set; }
        public string? MSSV { get; set; }
        public string? lop { get; set; }
        public bool isMale = false; 
        [Parameter]
        public EventCallback onClickLuu { get; set; }
        [Parameter]
        public EventCallback onClickThoat { get; set; }
        [Parameter]
        public EventCallback onClickCheck { get; set; }
        [Parameter]
        public EventCallback onClickTaoMoi { get; set; }

        private void onKeyDownInput()
        {
            is_existMSSV = null;
            hoTenLot = tenSinhVien = "";
            StateHasChanged();
        }
        public void handleCheck()
        {
            onClickCheck.InvokeAsync();
            StateHasChanged();
        }
        public void onClickMale()
        {
            isMale = true;
        }
    }
}
