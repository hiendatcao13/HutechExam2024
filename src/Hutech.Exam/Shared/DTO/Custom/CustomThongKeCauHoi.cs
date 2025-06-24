using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomThongKeCauHoi
    {
        public int MaCauHoi { get; set; }

        public int MaNhom { get; set; }

        public string MaSoCLO { get; set; } = string.Empty;

        public int TongSLDung { get; set; }

        public int TongSLTraLoi { get; set; }

        public double PhanTram { get; set; }

        public int TongSLSai => TongSLTraLoi - TongSLDung;

        public string MaCauHoiCLO => $"{MaCauHoi} - {MaSoCLO}";

    }
}
