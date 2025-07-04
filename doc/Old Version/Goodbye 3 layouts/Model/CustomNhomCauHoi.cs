using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomNhomCauHoi
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
        public virtual ICollection<CustomNhomCauHoi> NhomCauHoiCons { get; set; } = [];

        public virtual DeThiDto MaDeThiNavigation { get; set; } = null!;

        public override string ToString()
        {
            return TenNhom;
        }
    }
}
