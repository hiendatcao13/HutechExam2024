using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi
{

    [MessagePackObject]
    public class ChiTietBaiThiRequest
    {
        [Key(0)]
        public long MaChiTietBaiThi { get; set; }

        [Key(1)]
        public int MaChiTietCaThi { get; set; }

        [Key(2)]
        public long MaDeHv { get; set; }

        [Key(3)]
        public int MaNhom { get; set; }

        [Key(4)]
        public int MaClo { get; set; }

        [Key(5)]
        public int MaCauHoi { get; set; }

        [Key(6)]
        public int? CauTraLoi { get; set; }

        [Key(7)]
        public DateTime NgayTao { get; set; }

        [Key(8)]
        public DateTime? NgayCapNhat { get; set; }
        
        [Key(9)]
        public bool? KetQua { get; set; }

        [Key(10)]
        public int ThuTu { get; set; }

        public ChiTietBaiThiRequest() { }
        public ChiTietBaiThiRequest(long maChiTietBaiThi, int maChiTietCaThi, long maDeHv, int maNhom, int maClo, int maCauHoi, int? cauTraLoi, DateTime ngayTao, DateTime? ngayCapNhat, bool? ketQua, int thuTu)
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
        }
    }
}
