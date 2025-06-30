using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomThongKeDoPhanManh
    {
        public string SchemeName { get; set; } = string.Empty;

        public string TableName { get; set; } = string.Empty;

        public string IndexName { get; set; } = string.Empty;

        public double DoPhanManh { get; set; }

        public long SoLuongTrang {  get; set; }
    }
}
