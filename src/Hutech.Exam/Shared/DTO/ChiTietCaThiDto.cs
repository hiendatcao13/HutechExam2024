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

        public ChiTietCaThiDto() { }

        public ChiTietCaThiDto(ChiTietCaThiDto chiTietCaThiDto)
        {
            MaChiTietCaThi = chiTietCaThiDto.MaChiTietCaThi;
            MaCaThi = chiTietCaThiDto.MaCaThi;
            MaSinhVien = chiTietCaThiDto.MaSinhVien;
            MaDeThi = chiTietCaThiDto.MaDeThi;
            ThoiGianBatDau = chiTietCaThiDto.ThoiGianBatDau;
            ThoiGianKetThuc = chiTietCaThiDto.ThoiGianKetThuc;
            DaThi = chiTietCaThiDto.DaThi;
            DaHoanThanh = chiTietCaThiDto.DaHoanThanh;
            Diem = chiTietCaThiDto.Diem;
            TongSoCau = chiTietCaThiDto.TongSoCau;
            SoCauDung = chiTietCaThiDto.SoCauDung;
            GioCongThem = chiTietCaThiDto.GioCongThem;
            ThoiDiemCong = chiTietCaThiDto.ThoiDiemCong;
            LyDoCong = chiTietCaThiDto.LyDoCong;
        }
    }
}
