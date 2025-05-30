using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class ChiTietDeThiDto
    {
        public ChiTietDeThiDto()
        {
        }

        public int MaNhom { get; set; }

        public int MaCauHoi { get; set; }

        public int ThuTu { get; set; }

        public ChiTietDeThiDto(int maNhom, int maCauHoi, int thuTu)
        {
            MaNhom = maNhom;
            MaCauHoi = maCauHoi;
            ThuTu = thuTu;
        }

        public ChiTietDeThiDto(ChiTietDeThiDto other)
        {
            MaNhom = other.MaNhom;
            MaCauHoi = other.MaCauHoi;
            ThuTu = other.ThuTu;
        }
    }
}
