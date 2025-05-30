using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class SinhVienDto
    {
        public SinhVienDto()
        {
        }

        public long MaSinhVien { get; set; }

        public string? HoVaTenLot { get; set; }

        public string? TenSinhVien { get; set; }

        public short? GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public int? MaLop { get; set; }

        public string? DiaChi { get; set; }

        public string? Email { get; set; }

        public string? DienThoai { get; set; }

        public string? MaSoSinhVien { get; set; }

        public Guid? StudentId { get; set; }

        public bool? IsLoggedIn { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public DateTime? LastLoggedOut { get; set; }

        public byte[]? Photo { get; set; }

        public virtual ICollection<ChiTietCaThiDto> ChiTietCaThis { get; set; } = new List<ChiTietCaThiDto>();

        public override string ToString()
        {
            return $"{MaSinhVien} - {HoVaTenLot} {TenSinhVien}";
        }

        public SinhVienDto(long maSinhVien, string? hoVaTenLot, string? tenSinhVien, short? gioiTinh, DateTime? ngaySinh, int? maLop, string? diaChi, string? email, string? dienThoai, string? maSoSinhVien, Guid? studentId, bool? isLoggedIn, DateTime? lastLoggedIn, DateTime? lastLoggedOut, byte[]? photo, ICollection<ChiTietCaThiDto> chiTietCaThis)
        {
            MaSinhVien = maSinhVien;
            HoVaTenLot = hoVaTenLot;
            TenSinhVien = tenSinhVien;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            MaLop = maLop;
            DiaChi = diaChi;
            Email = email;
            DienThoai = dienThoai;
            MaSoSinhVien = maSoSinhVien;
            StudentId = studentId;
            IsLoggedIn = isLoggedIn;
            LastLoggedIn = lastLoggedIn;
            LastLoggedOut = lastLoggedOut;
            Photo = photo;
            ChiTietCaThis = chiTietCaThis;
        }

        public SinhVienDto(SinhVienDto other)
        {
            MaSinhVien = other.MaSinhVien;
            HoVaTenLot = other.HoVaTenLot;
            TenSinhVien = other.TenSinhVien;
            GioiTinh = other.GioiTinh;
            NgaySinh = other.NgaySinh;
            MaLop = other.MaLop;
            DiaChi = other.DiaChi;
            Email = other.Email;
            DienThoai = other.DienThoai;
            MaSoSinhVien = other.MaSoSinhVien;
            StudentId = other.StudentId;
            IsLoggedIn = other.IsLoggedIn;
            LastLoggedIn = other.LastLoggedIn;
            LastLoggedOut = other.LastLoggedOut;
            Photo = other.Photo;
            ChiTietCaThis = other.ChiTietCaThis;
        }
    }
}
