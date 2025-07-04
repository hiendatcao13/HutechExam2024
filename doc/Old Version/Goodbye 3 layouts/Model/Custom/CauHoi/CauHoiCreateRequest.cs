using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CauHoi
{
    public class CauHoiCreateRequest
    {
        public int MaNhom { get; set; }

        public int MaClo { get; set; }

        public string TieuDe { get; set; } = string.Empty;

        public int KieuNoiDung { get; set; }

        public string NoiDung { get; set; } = string.Empty;

        public int ThuTu { get; set; }

        public string GhiChu { get; set; } = string.Empty;

        public bool HoanVi { get; set; }

        public CauHoiCreateRequest() { }


    }
}
