using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi;
using MessagePack;

namespace Hutech.Exam.Shared.DTO.Request.Custom
{
    [MessagePackObject]
    public class SubmitRequest
    {
        public SubmitRequest()
        {
        }

        public SubmitRequest(long maSinhVien, int maChiTietCaThi, long maDeThi, Dictionary<Guid, Guid?> dapAnKhoanhs, DateTime thoiGianNopBai, bool isLanDau = true)
        {
            MaSinhVien = maSinhVien;
            MaChiTietCaThi = maChiTietCaThi;
            MaDeThi = maDeThi;
            DapAnKhoanhs = dapAnKhoanhs;
            ThoiGianNopBai = thoiGianNopBai;
            IsLanDau = isLanDau;
        }

        [Key(0)] 
        public long MaSinhVien { get; set; }

        [Key(1)] 
        public int MaChiTietCaThi { get; set; }

        [Key(2)] 
        public long MaDeThi { get; set; }

        [Key(3)] 
        public Dictionary<Guid, Guid?> DapAnKhoanhs { get; set; } = [];

        [Key(4)] 
        public DateTime ThoiGianNopBai { get; set; } = DateTime.Now;

        [Key(5)]
        public bool IsLanDau { get; set; } = true; // đánh dấu là đã gửi kết quả cho sinh viên rồi
    }
}
