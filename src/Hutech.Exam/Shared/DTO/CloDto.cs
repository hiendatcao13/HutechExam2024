using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class CloDto
    {
        public CloDto()
        {
        }

        public int MaClo { get; set; }

        public int MaMonHoc { get; set; }

        public string MaSoClo { get; set; } = null!;

        public string TieuDe { get; set; } = null!;

        public string? NoiDung { get; set; }

        public int TieuChi { get; set; }

        public int SoCau { get; set; }

        public virtual ICollection<CauHoiDto> CauHois { get; set; } = new List<CauHoiDto>();
        public virtual MonHocDto MaMonHocNavigation { get; set; } = null!;

        public CloDto(int maClo, int maMonHoc, string maSoClo, string tieuDe, string? noiDung, int tieuChi, int soCau, ICollection<CauHoiDto> cauHois, MonHocDto maMonHocNavigation)
        {
            MaClo = maClo;
            MaMonHoc = maMonHoc;
            MaSoClo = maSoClo;
            TieuDe = tieuDe;
            NoiDung = noiDung;
            TieuChi = tieuChi;
            SoCau = soCau;
            CauHois = cauHois;
            MaMonHocNavigation = maMonHocNavigation;
        }

        public CloDto(CloDto other)
        {
            MaClo = other.MaClo;
            MaMonHoc = other.MaMonHoc;
            MaSoClo = other.MaSoClo;
            TieuDe = other.TieuDe;
            NoiDung = other.NoiDung;
            TieuChi = other.TieuChi;
            SoCau = other.SoCau;
            CauHois = other.CauHois;
            MaMonHocNavigation = other.MaMonHocNavigation;
        }
    }
}
