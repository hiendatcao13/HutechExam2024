using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.DotThi
{
    public class DotThiCreateRequest
    {
        public DotThiCreateRequest()
        {
        }

        public DotThiCreateRequest(string tenDotThi, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, int namHoc)
        {
            TenDotThi = tenDotThi;
            ThoiGianBatDau = thoiGianBatDau;
            ThoiGianKetThuc = thoiGianKetThuc;
            NamHoc = namHoc;
        }

        public string TenDotThi { get; set; } = string.Empty;

        public DateTime ThoiGianBatDau { get; set; }

        public DateTime ThoiGianKetThuc { get; set; }

        public int NamHoc { get; set; }
    }
}
