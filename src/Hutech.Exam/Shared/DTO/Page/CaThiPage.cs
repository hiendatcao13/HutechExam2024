using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Page
{
    public class CaThiPage
    {
        public List<CaThiDto>? Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
