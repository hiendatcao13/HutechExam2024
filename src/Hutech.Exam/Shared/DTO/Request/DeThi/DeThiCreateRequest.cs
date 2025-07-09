using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.DeThi
{
    public class DeThiCreateRequest
    {
        public int MaMonHoc { get; set; }

        public string TenDeThi { get; set; } = null!;

        public Guid Guid { get; set; }

        public string KyHieuDe { get; set; } = null!;

        public DateTime NgayTao { get; set; }

    }
}
