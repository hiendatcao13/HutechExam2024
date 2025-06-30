using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO.Custom
{
    public class CustomHealthCheck
    {
        public CustomHealthCheck()
        {
        }

        public string ServiceName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? Description {  get; set; }

        public string? Exception {  get; set; }

        public string Duration { get; set; } = string.Empty;

        public CustomHealthCheck(string serviceName, string status, string? description, string? exception, string duration)
        {
            ServiceName = serviceName;
            Status = status;
            Description = description;
            Exception = exception;
            Duration = duration;
        }
    }
}
