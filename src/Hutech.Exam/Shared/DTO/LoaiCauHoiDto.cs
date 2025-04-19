using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class LoaiCauHoiDto
    {
        public int MaLoaiCauHoi { get; set; }

        public int KieuNoiDung { get; set; }

        public string? NoiDung { get; set; }

        public string? GhiChu { get; set; }
    }
}
