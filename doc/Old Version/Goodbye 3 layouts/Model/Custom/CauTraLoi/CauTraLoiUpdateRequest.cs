using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CauTraLoi
{
    public class CauTraLoiUpdateRequest
    {
        public CauTraLoiUpdateRequest()
        {
        }

        public CauTraLoiUpdateRequest(int maCauHoi, int thuTu, string noiDung, bool laDapAn, bool hoanVi)
        {
            MaCauHoi = maCauHoi;
            ThuTu = thuTu;
            NoiDung = noiDung;
            LaDapAn = laDapAn;
            HoanVi = hoanVi;
        }

        public int MaCauHoi { get; set; }

        public int ThuTu { get; set; }

        public string NoiDung { get; set; } = string.Empty;

        public bool LaDapAn { get; set; }

        public bool HoanVi { get; set; }
    }
}
