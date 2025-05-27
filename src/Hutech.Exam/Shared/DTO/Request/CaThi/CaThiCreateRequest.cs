using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CaThi
{
    public class CaThiCreateRequest
    {
        public string TenCaThi { get; set; } = string.Empty;

        public int MaChiTietDotThi { get; set; }

        public DateTime ThoiGianBatDau { get; set; }

        public int MaDeThi { get; set; }

        public int ThoiGianThi { get; set; }

        public CaThiCreateRequest() { }

        public CaThiCreateRequest(string tenCaThi, int maChiTietDotThi, DateTime thoiGianBatDau, int maDeThi, int thoiGianThi)
        {
            TenCaThi = tenCaThi;
            MaChiTietDotThi = maChiTietDotThi;
            ThoiGianBatDau = thoiGianBatDau;
            MaDeThi = maDeThi;
            ThoiGianThi = thoiGianThi;
        }

    }
}
