using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class MonHocDto
    {
        public int MaMonHoc { get; set; }

        public string? MaSoMonHoc { get; set; }

        public string? TenMonHoc { get; set; }

        public virtual ICollection<LopAo> LopAos { get; set; } = new List<LopAo>();
    }
}
