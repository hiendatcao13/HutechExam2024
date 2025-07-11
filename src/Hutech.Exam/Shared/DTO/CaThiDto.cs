using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class CaThiDto
    {
        public CaThiDto()
        {
        }

        public int MaCaThi { get; set; }

        public string? TenCaThi { get; set; }

        public int MaChiTietDotThi { get; set; }

        public DateTime ThoiGianBatDau { get; set; }

        public bool DaGanDe { get; set; }

        public bool KichHoat { get; set; }

        public DateTime? ThoiGianKichHoat { get; set; }

        public int ThoiGianThi { get; set; }

        public bool KetThuc { get; set; }

        public DateTime? ThoiDiemKetThuc { get; set; }

        [JsonIgnore] // không trả về json cho client, chỉ sử dụng cho server
        public string? MatMa { get; set; }

        public bool DaDuyet { get; set; }

        public string? LichSuHoatDong { get; set; }

        public virtual ICollection<ChiTietCaThiDto> ChiTietCaThis { get; set; } = new List<ChiTietCaThiDto>();

        public virtual ChiTietDotThiDto? MaChiTietDotThiNavigation { get; set; }

        //Phần custom thêm
        public int TongSV { get; set; }

        public override string ToString()
        {
            return TenCaThi ?? "Không tồn tại tên ca thi";
        }

        public CaThiDto(CaThiDto other)
        {
            MaCaThi = other.MaCaThi;
            TenCaThi = other.TenCaThi;
            MaChiTietDotThi = other.MaChiTietDotThi;
            ThoiGianBatDau = other.ThoiGianBatDau;
            DaGanDe = other.DaGanDe;
            KichHoat= other.KichHoat;
            ThoiGianKichHoat = other.ThoiGianKichHoat;
            ThoiGianThi = other.ThoiGianThi;
            KetThuc = other.KetThuc;
            ThoiDiemKetThuc = other.ThoiDiemKetThuc;
            DaDuyet = other.DaDuyet;
            ChiTietCaThis = other.ChiTietCaThis;
            MaChiTietDotThiNavigation = other.MaChiTietDotThiNavigation;
        }
    }
}
