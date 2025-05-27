using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Hutech.Exam.Shared.DTO.Request.Custom
{
    [MessagePackObject]
    public class SubmitRequest
    {
        public SubmitRequest()
        {
        }

        public SubmitRequest(long maSinhVien, int maChiTietCaThi, long maDeThiHoanVi, Dictionary<int, int?> dapAnKhoanhs, DateTime thoiGianNopBai)
        {
            MaSinhVien = maSinhVien;
            MaChiTietCaThi = maChiTietCaThi;
            MaDeThiHoanVi = maDeThiHoanVi;
            DapAnKhoanhs = dapAnKhoanhs;
            ThoiGianNopBai = thoiGianNopBai;
        }

        [Key(0)] public long MaSinhVien { get; set; }

        [Key(1)] public int MaChiTietCaThi { get; set; }

        [Key(2)] public long MaDeThiHoanVi { get; set; }

        [Key(3)] public Dictionary<int, int?> DapAnKhoanhs { get; set; } = [];

        [Key(4)] public DateTime ThoiGianNopBai { get; set; } = DateTime.Now;
    }
}
