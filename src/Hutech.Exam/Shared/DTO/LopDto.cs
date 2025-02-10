using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class LopDto
    {
        public int MaLop { get; set; }

        public string? TenLop { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public int? MaKhoa { get; set; }

        public virtual KhoaDto? MaKhoaNavigation { get; set; }
    }
}
