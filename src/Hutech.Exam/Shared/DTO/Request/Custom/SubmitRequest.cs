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

        public SubmitRequest(long maSinhVien, int maChiTietCaThi, long maDeThiHoanVi, Dictionary<int, int?> dapAnKhoanhs, DateTime thoiGianNopBai, bool isLanDau = false, int soLanFail = 0)
        {
            MaSinhVien = maSinhVien;
            MaChiTietCaThi = maChiTietCaThi;
            MaDeThiHoanVi = maDeThiHoanVi;
            DapAnKhoanhs = dapAnKhoanhs;
            ThoiGianNopBai = thoiGianNopBai;
            IsLanDau = isLanDau;
            SoLanFail = soLanFail;
        }

        [Key(0)] 
        public long MaSinhVien { get; set; }

        [Key(1)] 
        public int MaChiTietCaThi { get; set; }

        [Key(2)] 
        public long MaDeThiHoanVi { get; set; }

        [Key(3)] 
        public Dictionary<int, int?> DapAnKhoanhs { get; set; } = [];

        [Key(4)] 
        public DateTime ThoiGianNopBai { get; set; } = DateTime.Now;

        [Key(5)]
        public bool IsLanDau { get; set; } = false; // đánh dấu là đã gửi kết quả cho sinh viên rồi

        [Key(6)] 
        public int SoLanFail { get; set; } // số lần bị đẩy vào hàng đợi lại

    }
}
