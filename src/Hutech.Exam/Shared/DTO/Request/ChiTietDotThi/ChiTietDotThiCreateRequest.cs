using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietDotThi
{
    public class ChiTietDotThiCreateRequest
    {
        public ChiTietDotThiCreateRequest()
        {
        }

        public ChiTietDotThiCreateRequest(string tenChiTietDotThi, int maLopAo, int maDotThi, int lanThi)
        {
            TenChiTietDotThi = tenChiTietDotThi;
            MaLopAo = maLopAo;
            MaDotThi = maDotThi;
            LanThi = lanThi;
        }

        public string TenChiTietDotThi { get; set; } = string.Empty;

        public int MaLopAo { get; set; }

        public int MaDotThi { get; set; }

        public int LanThi { get; set; }

    }
}
