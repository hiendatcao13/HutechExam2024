using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.Models;
using System.Numerics;

namespace Hutech.Exam.Client.DAL
{
    public class ApplicationDataService
    {
        //ma_sinh_vien có kiểu dữ liệu là BigInt, identity(1,1)
        public SinhVienDto? sinhVien { get; set; }
        //sv thi ca nào
        public ChiTietCaThiDto? chiTietCaThi{ get; set; } 
        public List<ChiTietBaiThiDto>? chiTietBaiThis { get; set;}
        public List<CustomDeThi>? customDeThis { get; set; }
        public List<int>? listDapAnKhoanh { get; set; }
        public int bonusTime { get; set; }

    }
}
