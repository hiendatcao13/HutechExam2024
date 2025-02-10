using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class SinhVienDto
    {
        public long MaSinhVien { get; set; }

        public string? HoVaTenLot { get; set; }

        public string? TenSinhVien { get; set; }

        public short? GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public int? MaLop { get; set; }

        public string? DiaChi { get; set; }

        public string? Email { get; set; }

        public string? DienThoai { get; set; }

        public string? MaSoSinhVien { get; set; }

        public Guid? StudentId { get; set; }

        public bool? IsLoggedIn { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public DateTime? LastLoggedOut { get; set; }

        public byte[]? Photo { get; set; }

        public virtual ICollection<ChiTietCaThiDto> ChiTietCaThis { get; set; } = new List<ChiTietCaThiDto>();
    }
}
