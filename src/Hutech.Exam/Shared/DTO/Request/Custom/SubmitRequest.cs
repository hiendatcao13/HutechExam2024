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

        public SubmitRequest(long maSinhVien, int maChiTietCaThi, long maDeThiHoanVi, Dictionary<int, int?> dapAnKhoanhs, DateTime thoiGianNopBai, bool isLanDau = true, bool isRecoverySubmit = false, Dictionary<int, ChiTietBaiThiRequest>? dsDapAnDuPhong = null)
        {
            MaSinhVien = maSinhVien;
            MaChiTietCaThi = maChiTietCaThi;
            MaDeThiHoanVi = maDeThiHoanVi;
            DapAnKhoanhs = dapAnKhoanhs;
            ThoiGianNopBai = thoiGianNopBai;
            IsLanDau = isLanDau;
            DsDapAnDuPhong = dsDapAnDuPhong;
            IsRecoverySubmit = isRecoverySubmit;
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
        public bool IsLanDau { get; set; } = true; // đánh dấu là đã gửi kết quả cho sinh viên rồi

        [Key(6)]
        public Dictionary<int, ChiTietBaiThiRequest>? DsDapAnDuPhong { get; set; } = null; // chỉ có mặt khi số lần gửi thất bại của thí sinh quá nhiều

        [Key(7)]
        public bool IsRecoverySubmit { get; set; } = false; // đánh dấu là lần gửi này là có data dữ liệu sinh viên khoanh trước đó
    }
}
