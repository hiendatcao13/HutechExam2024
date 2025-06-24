using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.User
{
    public class UserCreateRequest
    {
		public int MaRole { get; set; }

		public string LoginName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public DateTime DateCreated = DateTime.Now;

		public string Comment { get; set; } = string.Empty;
    }
}
