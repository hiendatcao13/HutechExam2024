using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.Lop
{
    public class LopUpdateRequest
    {

        public string TenLop { get; set; } = string.Empty;

        public DateTime NgayBatDau { get; set; }

        public int MaKhoa { get; set; }
    }
}
