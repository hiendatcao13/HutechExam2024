using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.API
{
    // Custom lại thay vì dùng Exception
    public class ErrorCode
    {
        //Viết các code bắt Exception khi cập nhật đối tượng (MSSV bị trùng - USER_EXISTED,...) (lỗi input - HttpStatus.BadRequest, không tìm thấy - NotFound)
        //(unathenticated - unauthorized, unauthorized - forbidden)
        public static ErrorCode UncategorizedException() => new ErrorCode(9999, "uncategorized exception", HttpStatusCode.InternalServerError);

        public int code;
        public string? message;
        public HttpStatusCode statusCode;

        public ErrorCode(int code, string message, HttpStatusCode statusCode)
        {
            this.code = code;
            this.message = message;
            this.statusCode = statusCode;
        }
    }
}
