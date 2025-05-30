using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietDeThiHoanViDto
    {
        public ChiTietDeThiHoanViDto()
        {
        }

        public long MaDeHv { get; set; }

        public int MaNhom { get; set; }

        public int MaCauHoi { get; set; }

        public int ThuTu { get; set; }

        public string? HoanViTraLoi { get; set; }

        public int? DapAn { get; set; }

        public virtual NhomCauHoiHoanViDto Ma { get; set; } = null!;

        public virtual CauHoiDto MaCauHoiNavigation { get; set; } = null!;

        public ChiTietDeThiHoanViDto(long maDeHv, int maNhom, int maCauHoi, int thuTu, string? hoanViTraLoi, int? dapAn, NhomCauHoiHoanViDto ma, CauHoiDto maCauHoiNavigation)
        {
            MaDeHv = maDeHv;
            MaNhom = maNhom;
            MaCauHoi = maCauHoi;
            ThuTu = thuTu;
            HoanViTraLoi = hoanViTraLoi;
            DapAn = dapAn;
            Ma = ma;
            MaCauHoiNavigation = maCauHoiNavigation;
        }

        public ChiTietDeThiHoanViDto(ChiTietDeThiHoanViDto other)
        {
            MaDeHv = other.MaDeHv;
            MaNhom = other.MaNhom;
            MaCauHoi = other.MaCauHoi;
            ThuTu = other.ThuTu;
            HoanViTraLoi = other.HoanViTraLoi;
            DapAn = other.DapAn;
            Ma = other.Ma;
            MaCauHoiNavigation = other.MaCauHoiNavigation;
        }
    }
}
