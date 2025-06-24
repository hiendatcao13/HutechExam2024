using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.User
{
    public class UserUpdateRequest
    {
		public int MaRole { get; set; }

		public string LoginName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public bool IsDeleted { get; set; }

		public bool IsLockedOut { get; set; }

		public string Comment { get; set; } = string.Empty;
    }
}
