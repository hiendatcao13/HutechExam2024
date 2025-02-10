using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class MenuDto
    {
        public int MenuId { get; set; }

        public string MenuTitle { get; set; } = null!;

        public string? MenuDescription { get; set; }

        public string? MenuUrl { get; set; }

        public string? MenuValuepath { get; set; }

        public int? MenuParentId { get; set; }

        public int MenuOrder { get; set; }
    }
}
