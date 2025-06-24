using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.User
{
    public class UserUpdatePasswordRequest
    {
        public string Password { get; set; } = string.Empty;

        public DateTime LastPasswordChangedDate = DateTime.Now;
    }
}
