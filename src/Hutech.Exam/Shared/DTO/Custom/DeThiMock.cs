using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class DeThiMock
    {
        public string TenDeThi { get; set; } = null!;

        public string? KyHieuDe { get; set; }

        [JsonPropertyName("MaDeThi")]
        public Guid? Guid { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
