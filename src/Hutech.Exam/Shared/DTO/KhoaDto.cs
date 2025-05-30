using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class KhoaDto
    {
        public KhoaDto()
        {
        }

        public int MaKhoa { get; set; }

        public string? TenKhoa { get; set; }

        public DateTime? NgayThanhLap { get; set; }

        public virtual ICollection<LopDto> Lops { get; set; } = new List<LopDto>();

        public override string ToString()
        {
            return TenKhoa ?? "Không tồn tại tên khoa";
        }

        public KhoaDto(int maKhoa, string? tenKhoa, DateTime? ngayThanhLap, ICollection<LopDto> lops)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
            NgayThanhLap = ngayThanhLap;
            Lops = lops;
        }

        public KhoaDto(KhoaDto other)
        {
            MaKhoa = other.MaKhoa;
            TenKhoa = other.TenKhoa;
            NgayThanhLap = other.NgayThanhLap;
            Lops = other.Lops;
        }
    }
}
