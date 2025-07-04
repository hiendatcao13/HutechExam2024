using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class SinhVienDuPhongDto
    {
        public SinhVienDuPhongDto()
        {
        }

        public long MaSinhVienDuPhong { get; set; }

        public long MaSinhVien { get; set; }

        public int MaLopAo { get; set; }

        public int MaCaThi { get; set; }

        public SinhVienDuPhongDto(long maSinhVienDuPhong, long maSinhVien, int maLopAo, int maCaThi)
        {
            MaSinhVienDuPhong = maSinhVienDuPhong;
            MaSinhVien = maSinhVien;
            MaLopAo = maLopAo;
            MaCaThi = maCaThi;
        }

        public SinhVienDuPhongDto(SinhVienDuPhongDto other)
        {
            MaSinhVienDuPhong = other.MaSinhVienDuPhong;
            MaSinhVien = other.MaSinhVien;
            MaLopAo = other.MaLopAo;
            MaCaThi = other.MaCaThi;
        }
    }
}
