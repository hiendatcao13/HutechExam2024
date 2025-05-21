using Hutech.Exam.Server.Exceptions;
using Hutech.Exam.Shared.API;
using Hutech.Exam.Shared.DTO.API;
using Hutech.Exam.Shared.DTO.API.Response;
using System.Net;
using System.Text.Json;

namespace Hutech.Exam.Server.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Xử lý ngoại lệ và trả về phản hồi lỗi phù hợp
                await HandleExceptionAsync(context, ex);
            }
        }
        //AppException
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

            // Xác định mã trạng thái HTTP phù hợp dựa trên loại ngoại lệ
            var (statusCode, message, errorCode, errorDetails) = exception switch
            {
                NotFoundException =>
                    ((int)HttpStatusCode.NotFound, exception.Message, "RESOURCE_NOT_FOUND", exception.StackTrace),
                UnauthorizedAccessException =>
                    ((int)HttpStatusCode.Unauthorized, exception.Message, "UNAUTHORIZED", exception.StackTrace),
                AppException appEx =>
                    ((int)appEx.ErrorCode.StatusCode, appEx.ErrorCode.Message, appEx.ErrorCode.Code.ToString(), exception.StackTrace),
                _ =>
                    ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.", "INTERNAL_SERVER_ERROR", exception.StackTrace)
            };

            // Trả về phản hồi lỗi theo cấu trúc ResponseAPI
            var response = new ResponseAPI<object>(
                success: false,
                message: message,
                data: null,
                statusCode: (HttpStatusCode)statusCode,
                errorCode: errorCode,
                errorDetails: errorDetails
            );

            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }
    }
}
