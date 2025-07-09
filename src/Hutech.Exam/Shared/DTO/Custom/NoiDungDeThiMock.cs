using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class NoiDungDeThiMock
    {
        public Guid MaDeThi { get; set; }

        public string TenDeThi { get; set; } = string.Empty;

        [JsonPropertyName("HHHHHHHHHHHHHHHHHH")]
        public DateTime NgayTao { get; set; } = DateTime.Now;

        public List<Phan> Phans { get; set; } = [];

    }

    public class Phan
    {
        public Guid MaPhan { get; set; }

        public Guid? MaPhanCha { get; set; }

        public string TenPhan { get; set; } = string.Empty;

        public int KieuNoiDung { get; set; } = -1;

        public string NoiDung { get; set; } = string.Empty;

        public int SoLuongCauHoi { get; set; }

        public bool LaCauHoiNhom { get; set; } = false;

        public List<CauHoi> CauHois { get; set; } = [];
        
    }

    public class CauHoi
    {
        public Guid MaCauHoi { get; set; }

        public string NoiDung { get; set; } = string.Empty;

        public List<CauTraLoi> CauTraLois { get; set; } = [];
    }

    public class CauTraLoi
    {
        public Guid MaCauTraLoi { get; set; }

        public string NoiDung { get; set; } = string.Empty;

        public bool LaDapAn { get; set; } = false;
    }
}
