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

            var (statusCode, message, errorCode, errorDetails) = exception switch
            {
                NotFoundException =>
                    ((int)HttpStatusCode.NotFound, exception.Message, "RESOURCE_NOT_FOUND", null),

                UnauthorizedAccessException =>
                    ((int)HttpStatusCode.Unauthorized, "Bạn không có quyền truy cập.", "UNAUTHORIZED", null),

                AppException appEx =>
                    ((int)appEx.ErrorCode.StatusCode, appEx.ErrorCode.Message, appEx.ErrorCode.Code.ToString(), null),

                _ =>
                    ((int)HttpStatusCode.InternalServerError, "Đã có lỗi xảy ra. Vui lòng thử lại sau.", "INTERNAL_SERVER_ERROR",
                        env.IsDevelopment() ? exception.ToString() : null)
            };

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
