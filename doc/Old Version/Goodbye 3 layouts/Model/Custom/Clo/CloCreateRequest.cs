using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.Clo
{
    public class CloCreateRequest
    {
        public CloCreateRequest()
        {
        }

        public CloCreateRequest(int maMonHoc, string maSoClo, string tieuDe, string noiDung, int tieuChi, int soCau)
        {
            MaMonHoc = maMonHoc;
            MaSoClo = maSoClo;
            TieuDe = tieuDe;
            NoiDung = noiDung;
            TieuChi = tieuChi;
            SoCau = soCau;
        }

        public int MaMonHoc { get; set; }

        public string MaSoClo { get; set; } = string.Empty;

        public string TieuDe { get; set; } = string.Empty;

        public string NoiDung { get; set; } = string.Empty;

        public int TieuChi { get; set; }

        public int SoCau { get; set; }
    }
}
