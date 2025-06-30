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

        // loại bỏ ApprovedComments, ApprovedDate
        public int MaCaThi { get; set; }

        public string? TenCaThi { get; set; }

        public int MaChiTietDotThi { get; set; }

        public DateTime ThoiGianBatDau { get; set; }

        public int MaDeThi { get; set; }

        public bool IsActivated { get; set; }

        public DateTime? ActivatedDate { get; set; }

        public int ThoiGianThi { get; set; }

        public bool KetThuc { get; set; }

        public DateTime? ThoiDiemKetThuc { get; set; }

        [JsonIgnore] // không trả về json cho client, chỉ sử dụng cho server
        public string? MatMa { get; set; }

        public bool Approved { get; set; }

        public virtual ICollection<ChiTietCaThiDto> ChiTietCaThis { get; set; } = new List<ChiTietCaThiDto>();

        public virtual ChiTietDotThiDto MaChiTietDotThiNavigation { get; set; } = null!;

        //Phần custom thêm
        public int TongSV { get; set; }

        public override string ToString()
        {
            return TenCaThi ?? "Không tồn tại tên ca thi";
        }

        public CaThiDto(int maCaThi, string? tenCaThi, int maChiTietDotThi, DateTime thoiGianBatDau, int maDeThi, bool isActivated, DateTime? activatedDate, int thoiGianThi, bool ketThuc, DateTime? thoiDiemKetThuc, bool approved, ICollection<ChiTietCaThiDto> chiTietCaThis, ChiTietDotThiDto maChiTietDotThiNavigation)
        {
            MaCaThi = maCaThi;
            TenCaThi = tenCaThi;
            MaChiTietDotThi = maChiTietDotThi;
            ThoiGianBatDau = thoiGianBatDau;
            MaDeThi = maDeThi;
            IsActivated = isActivated;
            ActivatedDate = activatedDate;
            ThoiGianThi = thoiGianThi;
            KetThuc = ketThuc;
            ThoiDiemKetThuc = thoiDiemKetThuc;
            Approved = approved;
            ChiTietCaThis = chiTietCaThis;
            MaChiTietDotThiNavigation = maChiTietDotThiNavigation;
        }

        public CaThiDto(CaThiDto other)
        {
            MaCaThi = other.MaCaThi;
            TenCaThi = other.TenCaThi;
            MaChiTietDotThi = other.MaChiTietDotThi;
            ThoiGianBatDau = other.ThoiGianBatDau;
            MaDeThi = other.MaDeThi;
            IsActivated= other.IsActivated;
            ActivatedDate = other.ActivatedDate;
            ThoiGianThi = other.ThoiGianThi;
            KetThuc = other.KetThuc;
            ThoiDiemKetThuc = other.ThoiDiemKetThuc;
            Approved = other.Approved;
            ChiTietCaThis = other.ChiTietCaThis;
            MaChiTietDotThiNavigation = other.MaChiTietDotThiNavigation;
        }
    }
}
