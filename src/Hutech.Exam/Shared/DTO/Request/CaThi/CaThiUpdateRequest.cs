using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CaThi
{
    public class CaThiUpdateRequest
    {
        public string TenCaThi { get; set; } = string.Empty;

        public int MaChiTietDotThi { get; set; }

        public DateTime ThoiGianBatDau { get; set; }

        public int ThoiGianThi { get; set; }

        public string MatMa { get; set; } = string.Empty;

        public CaThiUpdateRequest() { }

    }
}
