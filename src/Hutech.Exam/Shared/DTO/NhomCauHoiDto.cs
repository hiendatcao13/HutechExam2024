using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class NhomCauHoiDto
    {
        public int MaNhom { get; set; }

        public int MaDeThi { get; set; }

        public string TenNhom { get; set; } = null!;

        public string? NoiDung { get; set; }

        public int SoCauHoi { get; set; }

        public bool HoanVi { get; set; }

        public int ThuTu { get; set; }

        public int MaNhomCha { get; set; }

        public int SoCauLay { get; set; }

        public bool? LaCauHoiNhom { get; set; }

        public virtual TblDeThi MaDeThiNavigation { get; set; } = null!;
    }
}
