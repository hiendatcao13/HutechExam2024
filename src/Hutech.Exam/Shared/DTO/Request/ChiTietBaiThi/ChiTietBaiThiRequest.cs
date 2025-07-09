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
        public long MaDeThi { get; set; }

        [Key(3)]
        public Guid MaNhom { get; set; }

        [Key(4)]
        public Guid MaCauHoi { get; set; }

        [Key(5)]
        public Guid? CauTraLoi { get; set; }

        [Key(6)]
        public DateTime NgayTao { get; set; }

        [Key(7)]
        public DateTime? NgayCapNhat { get; set; }
        
        [Key(8)]
        public bool? KetQua { get; set; }

        [Key(9)]
        public int ThuTu { get; set; }

        public ChiTietBaiThiRequest(long maChiTietBaiThi, int maChiTietCaThi, long maDeThi, Guid maNhom, Guid maCauHoi, Guid? cauTraLoi, DateTime ngayTao, DateTime? ngayCapNhat, bool? ketQua, int thuTu)
        {
            MaChiTietBaiThi = maChiTietBaiThi;
            MaChiTietCaThi = maChiTietCaThi;
            MaDeThi = maDeThi;
            MaNhom = maNhom;
            MaCauHoi = maCauHoi;
            CauTraLoi = cauTraLoi;
            NgayTao = ngayTao;
            NgayCapNhat = ngayCapNhat;
            KetQua = ketQua;
            ThuTu = thuTu;
        }
    }
}
