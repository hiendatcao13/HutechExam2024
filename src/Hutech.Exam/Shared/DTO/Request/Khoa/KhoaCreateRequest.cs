using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.Khoa
{
    public class KhoaCreateRequest
    {

        public string TenKhoa { get; set; } = string.Empty;

        public DateTime NgayThanhLap { get; set; }
    }
}
