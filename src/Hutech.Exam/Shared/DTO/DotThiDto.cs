using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class DotThiDto
    {
        public DotThiDto()
        {
        }

        public int MaDotThi { get; set; }

        public string? TenDotThi { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public int? NamHoc { get; set; }

        public virtual ICollection<ChiTietDotThiDto> ChiTietDotThis { get; set; } = new List<ChiTietDotThiDto>();

        override public string ToString()
        {
            return TenDotThi ?? "Không tồn tại tên đợt thi";
        }

        public DotThiDto(int maDotThi, string? tenDotThi, DateTime? thoiGianBatDau, DateTime? thoiGianKetThuc, int? namHoc, ICollection<ChiTietDotThiDto> chiTietDotThis)
        {
            MaDotThi = maDotThi;
            TenDotThi = tenDotThi;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
            NamHoc = namHoc;
            ChiTietDotThis = chiTietDotThis;
        }

        public DotThiDto(DotThiDto other)
        {
            MaDotThi = other.MaDotThi;
            TenDotThi = other.TenDotThi;
            ThoiGianBatDau = other.ThoiGianBatDau;
            ThoiGianKetThuc = other.ThoiGianKetThuc;
            NamHoc = other.NamHoc;
            ChiTietDotThis = other.ChiTietDotThis;
        }
    }
}
