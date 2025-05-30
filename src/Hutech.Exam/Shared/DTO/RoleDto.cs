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

        public int MaRole { get; set; }

        public string TenRole { get; set; } = null!;

        public string? MoTa { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public RoleDto(int maRole, string tenRole, string? moTa, ICollection<User> users)
        {
            MaRole = maRole;
            TenRole = tenRole;
            MoTa = moTa;
            Users = users;
        }

        public RoleDto(RoleDto other)
        {
            MaRole = other.MaRole;
            TenRole = other.TenRole;
            MoTa = other.MoTa;
            Users = other.Users;
        }
    }
}
