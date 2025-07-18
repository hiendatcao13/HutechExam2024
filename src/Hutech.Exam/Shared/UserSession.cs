﻿using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared
{
    public class UserSession
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string[]? Roles { get; set; } // phân biệt sinh viên và admin/monitor ( "" or "Admin")
        public string? Token { get; set; }
        public int ExpireIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
        public SinhVienDto? NavigateSinhVien { get; set; }
        public UserDto? NavigateUser { get; set; }
    }
}
