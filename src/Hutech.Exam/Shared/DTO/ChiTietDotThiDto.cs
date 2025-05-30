using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietDotThiDto
    {
        public ChiTietDotThiDto()
        {
        }

        public int MaChiTietDotThi { get; set; }

        public string TenChiTietDotThi { get; set; } = null!;

        public int MaLopAo { get; set; }

        public int MaDotThi { get; set; }

        public int LanThi { get; set; }

        public virtual ICollection<CaThiDto> CaThis { get; set; } = new List<CaThiDto>();

        public virtual DotThiDto MaDotThiNavigation { get; set; } = null!;

        public virtual LopAoDto MaLopAoNavigation { get; set; } = null!;

        public override string ToString()
        {
            return TenChiTietDotThi;
        }

        public ChiTietDotThiDto(int maChiTietDotThi, string tenChiTietDotThi, int maLopAo, int maDotThi, int lanThi, ICollection<CaThiDto> caThis, DotThiDto maDotThiNavigation, LopAoDto maLopAoNavigation)
        {
            MaChiTietDotThi = maChiTietDotThi;
            TenChiTietDotThi = tenChiTietDotThi;
            MaLopAo = maLopAo;
            MaDotThi = maDotThi;
            LanThi = lanThi;
            CaThis = caThis;
            MaDotThiNavigation = maDotThiNavigation;
            MaLopAoNavigation = maLopAoNavigation;
        }

        public ChiTietDotThiDto(ChiTietDotThiDto other)
        {
            MaChiTietDotThi = other.MaChiTietDotThi;
            TenChiTietDotThi = other.TenChiTietDotThi;
            MaLopAo = other.MaLopAo;
            MaDotThi = other.MaDotThi;
            LanThi = other.LanThi;
            CaThis = other.CaThis;
            MaDotThiNavigation = other.MaDotThiNavigation;
            MaLopAoNavigation = other.MaLopAoNavigation;
        }
    }
}
