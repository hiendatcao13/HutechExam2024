using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class LoiDeThiDto
    {
        public int MaLoi { get; set; }

        public int? MaCauHoi { get; set; }

        public int? MaCauTraLoi { get; set; }

        public int MaDe { get; set; }

        public string? NoiDung { get; set; }

        public Guid UserId { get; set; }
    }
}
