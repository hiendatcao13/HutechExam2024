using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class CauHoiDto
    {
        public int MaCauHoi { get; set; }

        public int MaNhom { get; set; }

        public int MaClo { get; set; }

        public string? TieuDe { get; set; }

        public int KieuNoiDung { get; set; }

        public string? NoiDung { get; set; }

        public string? GhiChu { get; set; }

        public bool? HoanVi { get; set; }

        public virtual ICollection<CauTraLoiDto> CauTraLois { get; set; } = new List<CauTraLoiDto>();

        public virtual ICollection<ChiTietDeThiHoanViDto> ChiTietDeThiHoanVis { get; set; } = new List<ChiTietDeThiHoanViDto>();

        public virtual CloDto MaCloNavigation { get; set; } = null!;

        public virtual NhomCauHoiDto MaNhomNavigation { get; set; } = null!;

        public override string ToString()
        {
            return NoiDung ?? "Không có tiêu đề";
        }

        public CauHoiDto() { }

        public CauHoiDto(CauHoiDto cauHoi)
        {
            MaCauHoi = cauHoi.MaCauHoi;
            MaNhom = cauHoi.MaNhom;
            MaClo = cauHoi.MaClo;
            TieuDe = cauHoi.TieuDe;
            KieuNoiDung = cauHoi.KieuNoiDung;
            NoiDung = cauHoi.NoiDung;
            GhiChu = cauHoi.GhiChu;
            HoanVi = cauHoi.HoanVi;
            CauTraLois = cauHoi.CauTraLois;
            ChiTietDeThiHoanVis = cauHoi.ChiTietDeThiHoanVis;
            MaCloNavigation = cauHoi.MaCloNavigation;
            MaNhomNavigation = cauHoi.MaNhomNavigation;
        }

    }
}
