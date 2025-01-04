using Hutech.Exam.Server.Exceptions;
using Hutech.Exam.Shared.API;
using System.Net;
using System.Text.Json;

namespace Hutech.Exam.Server.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
                                                                      //AppException
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //var response = new ApiResponse<string>
            //{
            //    code = exception.errCode.code,
            //    message = exception.errCode.message,
            //    result = null
            //};

            //var result = JsonSerializer.Serialize(response);

            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)exception.errCode.statusCode;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
