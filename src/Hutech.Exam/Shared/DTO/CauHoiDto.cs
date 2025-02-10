using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class CauHoiDto
    {
        public int MaCauHoi { get; set; }

        public string? TieuDe { get; set; }

        public int KieuNoiDung { get; set; }

        public string? NoiDung { get; set; }

        public string? GhiChu { get; set; }

        public bool? HoanVi { get; set; }

        public virtual ICollection<CauTraLoiDto> TblCauTraLois { get; set; } = new List<CauTraLoiDto>();
    }
}
