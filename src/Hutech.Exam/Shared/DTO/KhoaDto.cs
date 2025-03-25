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

        public virtual ICollection<LopDto> Lops { get; set; } = new List<LopDto>();

        public override string ToString()
        {
            return TenKhoa ?? "Không tồn tại tên khoa";
        }
    }
}
