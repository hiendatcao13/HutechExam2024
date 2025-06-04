using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Page
{
    public class Paged<TData>
    {
        public List<TData> Data { get; set; } = default!;
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
