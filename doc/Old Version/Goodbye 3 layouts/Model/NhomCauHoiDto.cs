using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class NhomCauHoiDto
    {
        public NhomCauHoiDto()
        {
        }

        public int MaNhom { get; set; }

        public int MaDeThi { get; set; }

        public string TenNhom { get; set; } = null!;

        public int KieuNoiDung { get; set; }

        public string? NoiDung { get; set; }

        public int SoCauHoi { get; set; }

        public bool HoanVi { get; set; }

        public int ThuTu { get; set; }

        public int MaNhomCha { get; set; }

        public int SoCauLay { get; set; }

        public bool? LaCauHoiNhom { get; set; }
        public virtual ICollection<CauHoiDto> CauHois { get; set; } = new List<CauHoiDto>();

        public virtual DeThiDto MaDeThiNavigation { get; set; } = null!;

        public override string ToString()
        {
            return TenNhom;
        }

        public NhomCauHoiDto(int maNhom, int maDeThi, string tenNhom, int kieuNoiDung, string? noiDung, int soCauHoi, bool hoanVi, int thuTu, int maNhomCha, int soCauLay, bool? laCauHoiNhom, ICollection<CauHoiDto> cauHois, DeThiDto maDeThiNavigation)
        {
            MaNhom = maNhom;
            MaDeThi = maDeThi;
            TenNhom = tenNhom;
            KieuNoiDung = kieuNoiDung;
            NoiDung = noiDung;
            SoCauHoi = soCauHoi;
            HoanVi = hoanVi;
            ThuTu = thuTu;
            MaNhomCha = maNhomCha;
            SoCauLay = soCauLay;
            LaCauHoiNhom = laCauHoiNhom;
            CauHois = cauHois;
            MaDeThiNavigation = maDeThiNavigation;
        }

        public NhomCauHoiDto(NhomCauHoiDto other)
        {
            MaNhom = other.MaNhom;
            MaDeThi = other.MaDeThi;
            TenNhom = other.TenNhom;
            KieuNoiDung = other.KieuNoiDung;
            NoiDung = other.NoiDung;
            SoCauHoi = other.SoCauHoi;
            HoanVi = other.HoanVi;
            ThuTu = other.ThuTu;
            MaNhomCha = other.MaNhomCha;
            SoCauLay = other.SoCauLay;
            LaCauHoiNhom = other.LaCauHoiNhom;
            CauHois = other.CauHois;
            MaDeThiNavigation = other.MaDeThiNavigation;
        }
    }
}
