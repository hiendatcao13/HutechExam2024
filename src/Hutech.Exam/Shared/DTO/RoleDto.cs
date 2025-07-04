using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public partial class RoleDto
    {
        public RoleDto()
        {
        }

        public int MaVaiTro { get; set; }

        public string TenVaiTro { get; set; } = null!;

        public string? MoTa { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
