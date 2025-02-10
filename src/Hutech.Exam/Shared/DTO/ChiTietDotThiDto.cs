using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietDotThiDto
    {
        public int MaChiTietDotThi { get; set; }

        public string TenChiTietDotThi { get; set; } = null!;

        public int MaLopAo { get; set; }

        public int MaDotThi { get; set; }

        public string LanThi { get; set; } = null!;

        public virtual ICollection<CaThi> CaThis { get; set; } = new List<CaThi>();

        public virtual DotThi MaDotThiNavigation { get; set; } = null!;

        public virtual LopAo MaLopAoNavigation { get; set; } = null!;
    }
}
