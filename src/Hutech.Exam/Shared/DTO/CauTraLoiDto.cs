using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class CauTraLoiDto
    {
        public CauTraLoiDto()
        {
        }

        public int MaCauTraLoi { get; set; }

        public int MaCauHoi { get; set; }

        public int ThuTu { get; set; }

        public string? NoiDung { get; set; }

        public bool LaDapAn { get; set; }

        public bool HoanVi { get; set; }

        public virtual CauHoiDto MaCauHoiNavigation { get; set; } = null!;

        public CauTraLoiDto(int maCauTraLoi, int maCauHoi, int thuTu, string? noiDung, bool laDapAn, bool hoanVi, CauHoiDto maCauHoiNavigation)
        {
            MaCauTraLoi = maCauTraLoi;
            MaCauHoi = maCauHoi;
            ThuTu = thuTu;
            NoiDung = noiDung;
            LaDapAn = laDapAn;
            HoanVi = hoanVi;
            MaCauHoiNavigation = maCauHoiNavigation;
        }

        public CauTraLoiDto(CauTraLoiDto other)
        {
            MaCauTraLoi = other.MaCauTraLoi;
            MaCauHoi = other.MaCauHoi;
            ThuTu = other.ThuTu;
            NoiDung = other.NoiDung;
            LaDapAn = other.LaDapAn;
            HoanVi = other.HoanVi;
            MaCauHoiNavigation = other.MaCauHoiNavigation;
        }
    }
}
