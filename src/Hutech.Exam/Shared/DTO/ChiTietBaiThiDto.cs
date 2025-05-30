using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietBaiThiDto
    {
        public ChiTietBaiThiDto()
        {
        }

        public long MaChiTietBaiThi { get; set; }

        public int MaChiTietCaThi { get; set; }

        public long MaDeHv { get; set; }

        public int MaNhom { get; set; }
        public int MaClo { get; set; }

        public int MaCauHoi { get; set; }

        public int? CauTraLoi { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public bool? KetQua { get; set; }

        public int ThuTu { get; set; }

        public virtual ChiTietCaThiDto MaChiTietCaThiNavigation { get; set; } = null!;

        public ChiTietBaiThiDto(long maChiTietBaiThi, int maChiTietCaThi, long maDeHv, int maNhom, int maClo, int maCauHoi, int? cauTraLoi, DateTime ngayTao, DateTime? ngayCapNhat, bool? ketQua, int thuTu, ChiTietCaThiDto maChiTietCaThiNavigation)
        {
            MaChiTietBaiThi = maChiTietBaiThi;
            MaChiTietCaThi = maChiTietCaThi;
            MaDeHv = maDeHv;
            MaNhom = maNhom;
            MaClo = maClo;
            MaCauHoi = maCauHoi;
            CauTraLoi = cauTraLoi;
            NgayTao = ngayTao;
            NgayCapNhat = ngayCapNhat;
            KetQua = ketQua;
            ThuTu = thuTu;
            MaChiTietCaThiNavigation = maChiTietCaThiNavigation;
        }

        public ChiTietBaiThiDto(ChiTietBaiThiDto other)
        {
            MaChiTietBaiThi = other.MaChiTietBaiThi;
            MaChiTietCaThi = other.MaChiTietCaThi;
            MaDeHv = other.MaDeHv;
            MaNhom = other.MaNhom;
            MaClo = other.MaClo;
            MaCauHoi = other.MaCauHoi;
            CauTraLoi = other.CauTraLoi;
            NgayTao = other.NgayTao;
            NgayCapNhat = other.NgayCapNhat;
            KetQua = other.KetQua;
            ThuTu = other.ThuTu;
            MaChiTietCaThiNavigation = other.MaChiTietCaThiNavigation;
        }
    }
}
