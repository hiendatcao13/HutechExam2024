using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.Models
{
    public class CustomDeThi
    {
        public int MaNhom { get; set; }
        public int MaCauHoi { get; set; }
        public string? NoiDungCauHoiNhomCha { get; set; }
        public string? NoiDungCauHoiNhom { get; set; }
        public string? NoiDungCauHoi { get; set; }
        public int KieuNoiDungCauHoi { get; set; }
        public Dictionary<int, string?>? CauTraLois { get; set; }
        public string? GhiChu { get; set;}
        // xem đề thi có bỏ hiển thị chương phần hay không
        public bool boChuongPhan { get; set; }

    }
}
