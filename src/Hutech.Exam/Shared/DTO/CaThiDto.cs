using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class CaThiDto
    {
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

        public string? MatMa { get; set; }

        public bool Approved { get; set; }

        public virtual ICollection<ChiTietCaThiDto> ChiTietCaThis { get; set; } = new List<ChiTietCaThiDto>();

        public virtual ChiTietDotThiDto MaChiTietDotThiNavigation { get; set; } = null!;

        public override string ToString()
        {
            return TenCaThi ?? "Không tồn tại tên ca thi";
        }
    }
}
