﻿using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Configurations;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Hutech.Exam.Server.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveMinutes;
        private const int DEFAULT_EXPIRE_MINUTES = 20;
        public CacheAttribute(int timeToLiveMinutes = DEFAULT_EXPIRE_MINUTES)
        {
            _timeToLiveMinutes = timeToLiveMinutes;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();
            // cache có chưa? Nếu không thì chạy vào xử lí controller, nếu đã có thì không cần vào các hàm controller và lấy cache
            if (!cacheConfiguration.Enabled)
            {
                await next(); // vào controller
                return;
            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            string? cacheResponse = null;
            try
            {
                cacheResponse = await cacheService.GetCacheResponseAsync<string>(cacheKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] GetCache failed: {ex.Message}");
            }

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var excutedContext = await next();
            try
            {
                if (excutedContext.Result is OkObjectResult objectResult && objectResult.Value != null)
                    await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromMinutes(_timeToLiveMinutes));
                else if (excutedContext.Result is ObjectResult okObjectResult && okObjectResult.Value != null)
                    await cacheService.SetCacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromMinutes(_timeToLiveMinutes));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] SetCache failed: {ex.Message}");
            }

        }
        // lấy các parameter của controller
        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
