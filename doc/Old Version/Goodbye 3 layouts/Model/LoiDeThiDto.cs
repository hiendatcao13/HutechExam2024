using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class LoiDeThiDto
    {
        public LoiDeThiDto()
        {
        }

        public int MaLoi { get; set; }

        public int? MaCauHoi { get; set; }

        public int? MaCauTraLoi { get; set; }

        public int MaDe { get; set; }

        public string? NoiDung { get; set; }

        public Guid UserId { get; set; }

        public LoiDeThiDto(int maLoi, int? maCauHoi, int? maCauTraLoi, int maDe, string? noiDung, Guid userId)
        {
            MaLoi = maLoi;
            MaCauHoi = maCauHoi;
            MaCauTraLoi = maCauTraLoi;
            MaDe = maDe;
            NoiDung = noiDung;
            UserId = userId;
        }

        public LoiDeThiDto(LoiDeThiDto other)
        {
            MaLoi = other.MaLoi;
            MaCauHoi = other.MaCauHoi;
            MaCauTraLoi = other.MaCauTraLoi;
            MaDe = other.MaDe;
            NoiDung = other.NoiDung;
            UserId = other.UserId;
        }
    }
}
