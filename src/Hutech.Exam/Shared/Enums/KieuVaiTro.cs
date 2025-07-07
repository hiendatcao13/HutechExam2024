using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.Enums
{
    public enum KieuVaiTro
    {
        [Display(Name = "Quản trị viên")]
        Admin = 1,

        [Display(Name = "Phòng Đào tạo")]
        KhaoThi = 2,

        [Display(Name = "Phòng Khảo thí")]
        DaoTao = 3,

        [Display(Name = "Trung tâm CNTT")]
        CNTT = 4,

        [Display(Name = "Giám thị")]
        GiamThi = 5
    }
}
