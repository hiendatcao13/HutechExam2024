using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CauHoi
{
    public class CauHoiUpdateRequest
    {
        public CauHoiUpdateRequest()
        {
        }

        public CauHoiUpdateRequest(int maNhom, int maClo, string tieuDe, int kieuNoiDung, string noiDung, string ghiChu, bool hoanVi)
        {
            MaNhom = maNhom;
            MaClo = maClo;
            TieuDe = tieuDe;
            KieuNoiDung = kieuNoiDung;
            NoiDung = noiDung;
            GhiChu = ghiChu;
            HoanVi = hoanVi;
        }

        public int MaNhom { get; set; }

        public int MaClo { get; set; }

        public string TieuDe { get; set; } = string.Empty;

        public int KieuNoiDung { get; set; }

        public string NoiDung { get; set; } = string.Empty;

        public string GhiChu { get; set; } = string.Empty;

        public bool HoanVi { get; set; }
    }
}
