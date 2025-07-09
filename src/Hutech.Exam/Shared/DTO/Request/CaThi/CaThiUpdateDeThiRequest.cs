using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.CaThi
{
    public class CaThiUpdateDeThiRequest
    {
        public bool IsOrderMSSV { get; set; }

        public string LichSuHoatDong { get; set; } = string.Empty;

        public List<Guid> DsDeThis { get; set; } = [];
    }
}
