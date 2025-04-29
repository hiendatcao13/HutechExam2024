using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class CloDto
    {
        public int MaClo { get; set; }

        public int MaMonHoc { get; set; }

        public string MaSoClo { get; set; } = null!;

        public string TieuDe { get; set; } = null!;

        public string? NoiDung { get; set; }

        public int TieuChi { get; set; }

        public int SoCau { get; set; }

        public virtual ICollection<CauHoiDto> CauHois { get; set; } = new List<CauHoiDto>();
        public virtual MonHocDto MaMonHocNavigation { get; set; } = null!;
    }
}
