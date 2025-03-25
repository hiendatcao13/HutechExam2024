using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class DanhMucCaThiTrongNgayDto
    {
        public int MaCaTrongNgay { get; set; }

        public string TenCaTrongNgay { get; set; } = null!;

        public int GioBatDau { get; set; }

        public int PhutBatDau { get; set; }

        public int GioKetThuc { get; set; }

        public int PhutKetThuc { get; set; }

        public int CaThiCode { get; set; }

        public override string ToString()
        {
            return TenCaTrongNgay;
        }
    }
}
