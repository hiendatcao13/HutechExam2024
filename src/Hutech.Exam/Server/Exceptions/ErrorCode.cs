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
        public int Code { get; }
        public string? Message { get; }
        public HttpStatusCode StatusCode { get; }

        private ErrorCode(int code, string message, HttpStatusCode statusCode)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        // Lỗi hệ thống
        public static readonly ErrorCode Uncategorized = new(9999, "Uncategorized exception", HttpStatusCode.InternalServerError);

        // Lỗi nghiệp vụ
        public static readonly ErrorCode UserAlreadyExists = new(1001, "User already exists", HttpStatusCode.BadRequest);
        public static readonly ErrorCode UserNotFound = new(1002, "User not found", HttpStatusCode.NotFound);
        public static readonly ErrorCode InvalidInput = new(1003, "Invalid input data", HttpStatusCode.BadRequest);

        // Lỗi bảo mật
        public static readonly ErrorCode Unauthorized = new(2001, "Unauthorized access", HttpStatusCode.Unauthorized);
        public static readonly ErrorCode Forbidden = new(2002, "Forbidden resource", HttpStatusCode.Forbidden);

        // Cho phép custom nếu cần
        public static ErrorCode Custom(int code, string message, HttpStatusCode statusCode) =>
            new(code, message, statusCode);
    }
}
