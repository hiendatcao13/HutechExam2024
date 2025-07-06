using Hutech.Exam.Server.Exceptions;
using Hutech.Exam.Shared.API;
using Hutech.Exam.Shared.DTO.API;
using Hutech.Exam.Shared.DTO.API.Response;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace Hutech.Exam.Server.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;
            string errorCode = "UNKNOWN_ERROR";
            string? errorDetails = null;

            switch (exception)
            {
                case SqlException sqlEx:
                    (statusCode, message, errorCode) = sqlEx.Number switch
                    {
                        2601 or 2627 => (StatusCodes.Status409Conflict, "Dữ liệu bị trùng khóa hoặc vi phạm trường unique", "DUPLICATE_KEY"),
                        547 => (StatusCodes.Status400BadRequest, "Vi phạm khóa ngoại", "FOREIGN_KEY_VIOLATION"),
                        8152 => (StatusCodes.Status400BadRequest, "Dữ liệu quá dài", "DATA_TOO_LONG"),
                        _ => (StatusCodes.Status500InternalServerError, "Lỗi cơ sở dữ liệu không xác định", "UNKNOWN_SQL_ERROR")
                    };
                    errorDetails = env.IsDevelopment() ? sqlEx.ToString() : null;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    message = "Bạn không có quyền truy cập.";
                    errorCode = "UNAUTHORIZED";
                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = exception.Message;
                    errorCode = "RESOURCE_NOT_FOUND";
                    break;

                case AppException appEx:
                    statusCode = (int)appEx.ErrorCode.StatusCode;
                    message = appEx.ErrorCode.Message!;
                    errorCode = appEx.ErrorCode.Code.ToString();
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "Đã có lỗi xảy ra. Vui lòng thử lại sau.";
                    errorCode = "INTERNAL_SERVER_ERROR";
                    errorDetails = env.IsDevelopment() ? exception.ToString() : null;// nếu đang chạy phát triển thì có thể xem lỗi được
                    break;
            }

            var response = new APIResponse<object>(
                success: false,
                message: message,
                data: null,
                statusCode: (HttpStatusCode)statusCode,
                errorCode: errorCode,
                errorDetails: errorDetails
            );

            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(result);
        }
    }
}
