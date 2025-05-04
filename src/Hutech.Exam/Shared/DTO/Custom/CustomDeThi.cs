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
        public int MaNhomCha { get; set; }
        public string? MaSoCLO { get; set; }
        public string? NoiDungCauHoiNhomCha { get; set; }
        public string? NoiDungCauHoiNhom { get; set; }
        public string? NoiDungCauHoi { get; set; }
        public int KieuNoiDungCauHoi { get; set; }
        public Dictionary<int, string?>? CauTraLois { get; set; }
        // ghi chú ở đây sử dụng cho mục đích hiển thị số lần nghe của audio theo từng cá nhân (xử lí riêng ở trang exam)
        public string? GhiChu { get; set; }

    }
}
