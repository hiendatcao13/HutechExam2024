using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CaThi
{
    public class CaThiExportFileRequest
    {
        public required CaThiDto CaThi { get; set; }

        public List<ChiTietCaThiDto> ChiTietCaThis { get; set; } = [];

        public required MonHocDto MonHoc { get; set; }

        public string TenPhongThi { get; set; } = string.Empty;

        public required DotThiDto DotThi { get; set; }
    }
}
