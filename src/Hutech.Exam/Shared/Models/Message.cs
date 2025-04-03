using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.Models
{
    public class Message
    {
        public string? ToUserId { get; set; }
        public string? FromUserId { get; set; }
        public string? MessageText { get; set; }
    }
}
