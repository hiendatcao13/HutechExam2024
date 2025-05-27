using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request
{
    public class CauTraLoiRequest
    {
        public int MaCauTraLoi { get; set; }

        public int MaCauHoi { get; set; }

        public int ThuTu { get; set; }

        public string? NoiDung { get; set; }

        public bool LaDapAn { get; set; }

        public bool HoanVi { get; set; }
    }
}
