using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class LopAoDto
    {
        public int MaLopAo { get; set; }

        public string? TenLopAo { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public int? MaMonHoc { get; set; }

        public virtual ICollection<ChiTietDotThiDto> ChiTietDotThis { get; set; } = new List<ChiTietDotThiDto>();

        public virtual MonHocDto? MaMonHocNavigation { get; set; }

        public override string ToString()
        {
            return TenLopAo ?? "Không tồn tại tên lớp ảo";
        }
    }
}
