using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.API
{
    public class ApiResponse<T>
    {
        public int code { get; set; } = 1000; // mặc định 1000 là thành công 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? result { get; set; }

        //default constructor
        public ApiResponse() { }
        //constructor thành công
        public ApiResponse(T? result)
        {
            this.result = result;
        }
        //constructor lỗi
        public ApiResponse(int code, string? message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
