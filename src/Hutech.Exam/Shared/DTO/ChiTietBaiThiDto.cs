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

        public long MaDeThi { get; set; }

        public Guid MaNhom { get; set; }

        public Guid MaCauHoi { get; set; }

        public Guid? CauTraLoi { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public bool? KetQua { get; set; }

        public int ThuTu { get; set; }

        public virtual ChiTietCaThiDto MaChiTietCaThiNavigation { get; set; } = null!;
    }
}
