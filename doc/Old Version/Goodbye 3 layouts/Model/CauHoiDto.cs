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

        public int ThuTu { get; set; }

        public Guid? Guild { get; set; }

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

        public CauHoiDto(CauHoiDto other)
        {
            MaCauHoi = other.MaCauHoi;
            MaNhom = other.MaNhom;
            MaClo = other.MaClo;
            TieuDe = other.TieuDe;
            KieuNoiDung = other.KieuNoiDung;
            NoiDung = other.NoiDung;
            ThuTu = other.ThuTu;
            GhiChu = other.GhiChu;
            HoanVi = other.HoanVi;
            CauTraLois = other.CauTraLois;
            ChiTietDeThiHoanVis = other.ChiTietDeThiHoanVis;
            MaCloNavigation = other.MaCloNavigation;
            MaNhomNavigation = other.MaNhomNavigation;
        }

        public CauHoiDto(int maCauHoi, int maNhom, int maClo, string? tieuDe, int kieuNoiDung, string? noiDung, int thuTu, string? ghiChu, bool? hoanVi, ICollection<CauTraLoiDto> cauTraLois, ICollection<ChiTietDeThiHoanViDto> chiTietDeThiHoanVis, CloDto maCloNavigation, NhomCauHoiDto maNhomNavigation)
        {
            MaCauHoi = maCauHoi;
            MaNhom = maNhom;
            MaClo = maClo;
            TieuDe = tieuDe;
            KieuNoiDung = kieuNoiDung;
            NoiDung = noiDung;
            ThuTu = thuTu;
            GhiChu = ghiChu;
            HoanVi = hoanVi;
            CauTraLois = cauTraLois;
            ChiTietDeThiHoanVis = chiTietDeThiHoanVis;
            MaCloNavigation = maCloNavigation;
            MaNhomNavigation = maNhomNavigation;
        }
    }
}
