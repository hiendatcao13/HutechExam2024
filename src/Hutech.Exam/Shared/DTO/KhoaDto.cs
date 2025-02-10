using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class KhoaDto
    {
        public int MaKhoa { get; set; }

        public string? TenKhoa { get; set; }

        public DateTime? NgayThanhLap { get; set; }

        public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
    }
}
