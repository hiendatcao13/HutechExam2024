using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietCaThiDto
    {
        public ChiTietCaThiDto() {  }
        public int MaChiTietCaThi { get; set; }

        public int? MaCaThi { get; set; }

        public long? MaSinhVien { get; set; }

        public long? MaDeThi { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public bool DaThi { get; set; }

        public bool DaHoanThanh { get; set; }

        public double Diem { get; set; }

        public int? TongSoCau { get; set; }

        public int? SoCauDung { get; set; }

        public int GioCongThem { get; set; }

        public DateTime? ThoiDiemCong { get; set; }

        public string? LyDoCong { get; set; }

        public virtual ICollection<ChiTietBaiThiDto> ChiTietBaiThis { get; set; } = new List<ChiTietBaiThiDto>();

        public virtual CaThiDto? MaCaThiNavigation { get; set; }

        public virtual SinhVienDto? MaSinhVienNavigation { get; set; }

        public ChiTietCaThiDto(ChiTietCaThiDto other)
        {
            MaChiTietCaThi = other.MaChiTietCaThi;
            MaCaThi = other.MaCaThi;
            MaSinhVien = other.MaSinhVien;
            MaDeThi = other.MaDeThi;
            ThoiGianBatDau = other.ThoiGianBatDau;
            ThoiGianKetThuc = other.ThoiGianKetThuc;
            DaThi = other.DaThi;
            DaHoanThanh = other.DaHoanThanh;
            Diem = other.Diem;
            TongSoCau = other.TongSoCau;
            SoCauDung = other.SoCauDung;
            GioCongThem = other.GioCongThem;
            ThoiDiemCong = other.ThoiDiemCong;
            LyDoCong = other.LyDoCong;
        }

        public ChiTietCaThiDto(int maChiTietCaThi, int? maCaThi, long? maSinhVien, long? maDeThi, DateTime? thoiGianBatDau, DateTime? thoiGianKetThuc, bool daThi, bool daHoanThanh, double diem, int? tongSoCau, int? soCauDung, int gioCongThem, DateTime? thoiDiemCong, string? lyDoCong, ICollection<ChiTietBaiThiDto> chiTietBaiThis, CaThiDto? maCaThiNavigation, SinhVienDto? maSinhVienNavigation)
        {
            MaChiTietCaThi = maChiTietCaThi;
            MaCaThi = maCaThi;
            MaSinhVien = maSinhVien;
            MaDeThi = maDeThi;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
            DaThi = daThi;
            DaHoanThanh = daHoanThanh;
            Diem = diem;
            TongSoCau = tongSoCau;
            SoCauDung = soCauDung;
            GioCongThem = gioCongThem;
            ThoiDiemCong = thoiDiemCong;
            LyDoCong = lyDoCong;
            ChiTietBaiThis = chiTietBaiThis;
            MaCaThiNavigation = maCaThiNavigation;
            MaSinhVienNavigation = maSinhVienNavigation;
        }
    }
}
