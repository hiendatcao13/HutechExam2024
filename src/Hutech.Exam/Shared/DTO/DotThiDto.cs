using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class DotThiDto
    {
        public int MaDotThi { get; set; }

        public string? TenDotThi { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public int? NamHoc { get; set; }

        public virtual ICollection<ChiTietDotThiDto> ChiTietDotThis { get; set; } = new List<ChiTietDotThiDto>();
    }
}
