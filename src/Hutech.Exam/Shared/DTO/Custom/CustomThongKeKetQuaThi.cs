using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomThongKeKetQuaThi
    {
        [JsonPropertyName("ma_de_thi")]
        public Guid MaDeThi { get; set; }
        // phần này lưu vào phần ghi chú, do bên họ mã đề là GUID, mình là int

        [JsonPropertyName("ten_de_thi")]
        public string TenDeThi { get; set; } = string.Empty;

        [JsonPropertyName("ngay_thi")]
        public DateTime NgayThi { get; set; }

        [JsonPropertyName("tong_so_sinh_vien")]
        public int TongSoSinhVien { get; set; }

        [JsonPropertyName("question_stats")]
        public List<CustomThongKeKetQuaCauHoi> KetQuaCauHois { get; set; } = [];
    }

    public class CustomThongKeKetQuaCauHoi
    {
        [JsonPropertyName("ma_cau_hoi")]
        public Guid MaCauHoi { get; set; }
        // phần này lưu vào phần ghi chú, do bên họ mã đề là GUID, mình là int

        [JsonPropertyName("so_lan_lam")]
        public int SoSVThamGia { get; set; }

        [JsonPropertyName("so_lan_dung")]
        public int SoLanDung { get; set; }

        [JsonPropertyName("top_27_correct")]
        public int SLSinhVienTopDung { get; set; }

        [JsonPropertyName("top_27_total")]
        public int TongSLSinhVienTop { get; set; }

        [JsonPropertyName("bottom_27_correct")]
        public int SLSinhVienBottomDung { get; set; }

        [JsonPropertyName("bottom_27_total")]
        public int TongSQLSinhVinBottom { get; set; }
    }
}
