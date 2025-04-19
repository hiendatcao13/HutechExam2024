using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomDeThi
    {
        public int MaNhom { get; set; }
        public int MaCauHoi { get; set; }
        public int MaClo { get; set; }
        public string? NoiDungCauHoiNhomCha { get; set; }
        public string? NoiDungCauHoiNhom { get; set; }
        public string? NoiDungCauHoi { get; set; }
        public int KieuNoiDungCauHoi { get; set; }
        public Dictionary<int, string?>? CauTraLois { get; set; }
        public string? GhiChu { get; set; }
        // xem đề thi có bỏ hiển thị chương phần hay không
        public bool BoChuongPhan { get; set; }

    }
}
