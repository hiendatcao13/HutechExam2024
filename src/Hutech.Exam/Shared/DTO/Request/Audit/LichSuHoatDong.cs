using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hutech.Exam.Shared.Enums;

namespace Hutech.Exam.Shared.DTO.Request.Audit
{
    public class LichSuHoatDong
    {
        public KieuHanhDong HanhDong { get; set; }

        public string ChiTiet { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public string NguoiThucHien { get; set; } = string.Empty;

        public string LyDo { get; set; } = string.Empty;

        public DateTime ThoiDiem { get; set; } = DateTime.Now;
    }
}
