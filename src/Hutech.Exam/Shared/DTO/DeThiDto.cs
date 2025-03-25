using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class DeThiDto
    {
        public int MaDeThi { get; set; }

        public int MaMonHoc { get; set; }

        public string TenDeThi { get; set; } = null!;

        public DateTime NgayTao { get; set; }

        public int NguoiTao { get; set; }

        public string? GhiChu { get; set; }

        public bool? LuuTam { get; set; }

        public bool DaDuyet { get; set; }

        public int? TongSoDeHoanVi { get; set; }

        public bool BoChuongPhan { get; set; }

        public virtual ICollection<DeThiHoanViDto> TblDeThiHoanVis { get; set; } = new List<DeThiHoanViDto>();

        public virtual ICollection<NhomCauHoiDto> TblNhomCauHois { get; set; } = new List<NhomCauHoiDto>();

        public override string ToString()
        {
            return TenDeThi;
        }
    }
}
