using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class MonHocDto
    {
        public MonHocDto()
        {
        }

        public int MaMonHoc { get; set; }

        public string? MaSoMonHoc { get; set; }

        public string? TenMonHoc { get; set; }

        public virtual ICollection<LopAoDto> LopAos { get; set; } = new List<LopAoDto>();
        public virtual ICollection<CloDto> Clos { get; set; } = new List<CloDto>();

        public override string ToString()
        {
            return TenMonHoc ?? "Không tồn tại tên môn học";
        }

        public MonHocDto(int maMonHoc, string? maSoMonHoc, string? tenMonHoc, ICollection<LopAoDto> lopAos, ICollection<CloDto> clos)
        {
            MaMonHoc = maMonHoc;
            MaSoMonHoc = maSoMonHoc;
            TenMonHoc = tenMonHoc;
            LopAos = lopAos;
            Clos = clos;
        }

        public MonHocDto(MonHocDto other)
        {
            MaMonHoc = other.MaMonHoc;
            MaSoMonHoc = other.MaSoMonHoc;
            TenMonHoc = other.TenMonHoc;
            LopAos = other.LopAos;
            Clos = other.Clos;
        }
    }
}
