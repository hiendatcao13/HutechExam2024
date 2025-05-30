using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class LoaiCauHoiDto
    {
        public LoaiCauHoiDto()
        {
        }

        public int MaLoaiCauHoi { get; set; }

        public int KieuNoiDung { get; set; }

        public string? NoiDung { get; set; }

        public string? GhiChu { get; set; }

        public LoaiCauHoiDto(int maLoaiCauHoi, int kieuNoiDung, string? noiDung, string? ghiChu)
        {
            MaLoaiCauHoi = maLoaiCauHoi;
            KieuNoiDung = kieuNoiDung;
            NoiDung = noiDung;
            GhiChu = ghiChu;
        }

        public LoaiCauHoiDto(LoaiCauHoiDto other)
        {
            MaLoaiCauHoi = other.MaLoaiCauHoi;
            KieuNoiDung = other.KieuNoiDung;
            NoiDung = other.NoiDung;
            GhiChu = other.GhiChu;
        }
    }
}
