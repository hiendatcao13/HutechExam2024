using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietDeThiHoanViDto
    {
        public long MaDeHv { get; set; }

        public int MaNhom { get; set; }

        public int MaCauHoi { get; set; }

        public int ThuTu { get; set; }

        public string? HoanViTraLoi { get; set; }

        public int? DapAn { get; set; }

        public virtual NhomCauHoiHoanViDto Ma { get; set; } = null!;

        public virtual CauHoiDto MaCauHoiNavigation { get; set; } = null!;
    }
}
