using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.ChiTietCaThi
{
    public class ChiTietCaThiCreateBatchRequest
    {
        public int MaCaThi { get; set; }

        public string MaSoSinhVien { get; set; } = string.Empty;

        public long? MaDeThi { get; set; }
    }
}
