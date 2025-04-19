using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class NhomCauHoiHoanViDto
    {
        public long MaDeHv { get; set; }

        public int MaNhom { get; set; }

        public int ThuTu { get; set; }

        public virtual DeThiHoanViDto MaDeHvNavigation { get; set; } = null!;

        public virtual ICollection<ChiTietDeThiHoanViDto> ChiTietDeThiHoanVis { get; set; } = new List<ChiTietDeThiHoanViDto>();
    }
}
