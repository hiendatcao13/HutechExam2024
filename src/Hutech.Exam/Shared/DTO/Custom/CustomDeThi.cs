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
        public Guid MaNhom { get; set; }

        [Key(1)]
        public Guid MaCauHoi { get; set; }

        [Key(2)]
        public Guid MaNhomCha { get; set; }

        [Key(3)]
        public string? NoiDungCauHoiNhomCha { get; set; }

        [Key(4)]
        public string? NoiDungCauHoiNhom { get; set; }

        [Key(5)]
        public string? NoiDungCauHoi { get; set; }

        [Key(6)]
        public int KieuNoiDungCauHoiNhom { get; set; }

        [Key(7)]
        public int KieuNoiDungCauHoi { get; set; } = -1;

        [Key(8)]
        public Dictionary<Guid, string?>? CauTraLois { get; set; }
        // ghi chú ở đây sử dụng cho mục đích hiển thị số lần nghe của audio theo từng cá nhân (xử lí riêng ở trang exam)

        [Key(9)]
        public string? HoanViCauTraLoi { get; set; }

        [Key(10)]
        public string? GhiChu { get; set; }

        [Key(11)]
        public int ThuTuCauHoi { get; set; } // custom thêm để hiện thị thứ tự câu hỏi

    }
}
