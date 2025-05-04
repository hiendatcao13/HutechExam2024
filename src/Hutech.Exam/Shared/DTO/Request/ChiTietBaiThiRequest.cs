using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request
{
    public class ChiTietBaiThiRequest
    {
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
    }
}
