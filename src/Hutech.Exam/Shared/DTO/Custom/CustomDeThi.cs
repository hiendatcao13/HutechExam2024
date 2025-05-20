using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Hutech.Exam.Shared.DTO.Custom
{
    [MessagePackObject]
    public class CustomDeThi
    {
        [Key(0)]
        public int MaNhom { get; set; }

        [Key(1)]
        public int MaCauHoi { get; set; }

        [Key(2)]
        public int MaNhomCha { get; set; }

        [Key(3)]
        public int MaCLO { get; set; }

        [Key(4)]
        public string? MaSoCLO { get; set; }

        [Key(5)]
        public string? NoiDungCauHoiNhomCha { get; set; }

        [Key(6)]
        public string? NoiDungCauHoiNhom { get; set; }

        [Key(7)]
        public string? NoiDungCauHoi { get; set; }

        [Key(8)]
        public int KieuNoiDungCauHoiNhom { get; set; }

        [Key(9)]
        public int KieuNoiDungCauHoi { get; set; }

        [Key(10)]
        public Dictionary<int, string?>? CauTraLois { get; set; }
        // ghi chú ở đây sử dụng cho mục đích hiển thị số lần nghe của audio theo từng cá nhân (xử lí riêng ở trang exam)

        [Key(11)]
        public string? HoanViCauTraLoi { get; set; }

        [Key(12)]
        public string? GhiChu { get; set; }

        [Key(13)]
        public long ThuTuCauHoi { get; set; }

    }
}
