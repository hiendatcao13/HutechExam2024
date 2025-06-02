using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Page
{
    public class DotThiPage
    {
        public List<DotThiDto>? Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
