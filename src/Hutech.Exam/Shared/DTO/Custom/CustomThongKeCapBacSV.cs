using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomThongKeCapBacSV
    {
        public Guid? GuidMaDe { get; set; }

        public string TenDeThi { get; set; } = string.Empty;

        public DateTime NgayThi { get; set; }

        public int TongSVThamGia { get; set; } 

        public List<ThongKeCapBacCauHoi> ThongKeCapBacCauHois { get; set; } = [];
    }

    public class ThongKeCapBacCauHoi
    {
        public int? MaCauHoi { get; set; }

        public Guid? GuidCauHoi { get; set; }

        public int TongSLThamGia { get; set; }

        public int TongSLDung { get; set; }

        public int TongSVTop { get; set; }

        public int TongSVTopDung { get; set; }

        public int TongSVBottom { get; set; }

        public int TongSVBottomDung { get; set; }
    }
}
