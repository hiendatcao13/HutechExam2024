using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class LopDto
    {
        public LopDto()
        {
        }

        public int MaLop { get; set; }

        public string? TenLop { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public int? MaKhoa { get; set; }

        public virtual KhoaDto? MaKhoaNavigation { get; set; }

        public override string ToString()
        {
            return TenLop ?? "Không tồn tại tên lớp";
        }

        public LopDto(int maLop, string? tenLop, DateTime? ngayBatDau, int? maKhoa, KhoaDto? maKhoaNavigation)
        {
            MaLop = maLop;
            TenLop = tenLop;
            NgayBatDau = ngayBatDau;
            MaKhoa = maKhoa;
            MaKhoaNavigation = maKhoaNavigation;
        }

        public LopDto(LopDto other)
        {
            MaLop = other.MaLop;
            TenLop = other.TenLop;
            NgayBatDau = other.NgayBatDau;
            MaKhoa = other.MaKhoa;
            MaKhoaNavigation = other.MaKhoaNavigation;
        }
    }
}
