using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request
{
    public class SVKhanCapRequest
    {
        public string MaSoSinhVien { get; set; } = string.Empty;

        public int MaCaThi { get; set; }

        public long MaDeThi { get; set; }

    }
}
