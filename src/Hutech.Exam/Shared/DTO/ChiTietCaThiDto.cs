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

        // không nằm trong database, chỉ thêm vào
        public string? LyDoCong { get; set; }
        public string? KyHieuDe { get; set; }

        public string? TenLop { get; set; }

        public virtual ICollection<ChiTietBaiThiDto> ChiTietBaiThis { get; set; } = new List<ChiTietBaiThiDto>();

        public virtual CaThiDto? MaCaThiNavigation { get; set; }

        public virtual SinhVienDto? MaSinhVienNavigation { get; set; }

        public ChiTietCaThiDto(ChiTietCaThiDto other)
        {
            MaChiTietCaThi = other.MaChiTietCaThi;
            MaCaThi = other.MaCaThi;
            MaSinhVien = other.MaSinhVien;
            MaDeThi = other.MaDeThi;
            KyHieuDe = other.KyHieuDe;
            ThoiGianBatDau = other.ThoiGianBatDau;
            ThoiGianKetThuc = other.ThoiGianKetThuc;
            DaThi = other.DaThi;
            DaHoanThanh = other.DaHoanThanh;
            Diem = other.Diem;
            TongSoCau = other.TongSoCau;
            SoCauDung = other.SoCauDung;
            GioCongThem = other.GioCongThem;
            ThoiDiemCong = other.ThoiDiemCong;
        }
    }
}
