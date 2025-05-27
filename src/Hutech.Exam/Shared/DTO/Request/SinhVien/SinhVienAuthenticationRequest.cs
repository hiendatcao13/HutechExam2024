using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Request.SinhVien
{
    public class SinhVienAuthenticationRequest
    {
        public SinhVienAuthenticationRequest()
        {
        }

        public SinhVienAuthenticationRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
