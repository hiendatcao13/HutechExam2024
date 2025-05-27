using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.User
{
    public class UserAuthenticationRequest
    {
        public UserAuthenticationRequest()
        {
        }

        public UserAuthenticationRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
