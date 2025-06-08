using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomThongTinMaDeThi
    {
        public int MaChuong { get; set; }

        public int MaNhom { get; set; }

        public bool HoanViNhom { get; set; }

        public int ThuTuNhom { get; set; }

        public int? MaCauHoi { get; set; }

        public bool? HoanViCauHoi { get; set; }

        public int? DapAn { get; set; }

        public int? ThuTuCauHoi { get; set; }

        public List<int>? CauTraLoiKhongHoanVi { get; set; } = [];
    }
}
