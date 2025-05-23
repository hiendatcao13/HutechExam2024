﻿using Hutech.Exam.Server.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRedisController(IResponseCacheService responseCacheService) : ControllerBase
    {
        private readonly IResponseCacheService _responseCacheService = responseCacheService;

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            // pay attention, API name is lowercase, ex: testredis
            // pay attention, it will delete this path "testredis/Create" if u want another path just fill "TestRedis/NameYourPathUWantDelete"
            await _responseCacheService.RemoveCacheResponseAsync(HttpContext.Request.Path);
            return Ok();
        }
    }
}
