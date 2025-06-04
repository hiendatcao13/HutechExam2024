using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.SinhVien
{
    public class SinhVienCreateRequest
    {

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
    }
}
