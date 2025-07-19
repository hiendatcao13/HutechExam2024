using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.Enums
{
    public enum KieuHanhDong
    {
        // Thay đổi trạng thái ca thi
        [Display(Name = "Kích hoạt ca thi")]
        KichHoatCaThi = 1,

        [Display(Name = "Hủy kích hoạt ca thi")]
        HuyKichHoatCaThi = 2,

        [Display(Name = "Dừng ca thi")]
        DungCaThi = 3,

        [Display(Name = "Kết thúc ca thi")]
        KetThucCaThi = 4,

        //Các hành động đối với sinh viên
        [Display(Name = "Reset đăng nhập cho thí sinh")]
        ResetDangNhap = 5,

        [Display(Name = "Cộng giờ cho thí sinh")]
        CongGioChoThiSinh = 6,

        [Display(Name = "Thêm thí sinh khẩn cấp")]
        ThemThiSinhKhanCap = 7,

        [Display(Name = "Xóa thí sinh khỏi ca thi")]
        XoaThiSinh = 8,

        [Display(Name = "Gán đề thi")]
        GanDeThi = 9,

        [Display(Name = "Duyệt ca thi")]
        DuyetCaThi = 10,

    }
}
