using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ConnectionIdController(IConnectionMultiplexer redis) : Controller
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();

        [HttpPut("SetConnectionId")]
        public async Task<IActionResult> SetConnectionIdAsync([FromBody]DataMessage data)
        {
            try
            {
                var key = $"connection:{data.MaSinhVien}";
                await _redisDb.StringSetAsync(key, data.ConnectionId, TimeSpan.FromMinutes(150));
                return Ok("ConnectionId updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] Error setting connection: {ex.Message}");
                return StatusCode(500, "Failed to set ConnectionId");
            }
        }
        [HttpGet("GetConnectionId")]
        [Authorize]
        public async Task<IActionResult> GetConnectionIdAsync([FromQuery]long ma_sinh_vien)
        {
            try
            {
                var key = $"connection:{ma_sinh_vien}";
                var connectionId = await _redisDb.StringGetAsync(key);

                if (connectionId.IsNullOrEmpty)
                    return NotFound("No connection found");

                return Ok(connectionId.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] Error retrieving connection: {ex.Message}");
                return StatusCode(500, "Failed to get ConnectionId");
            }
        }

        public class DataMessage
        {
            public string ConnectionId { get; set; } = default!;
            public long MaSinhVien { get; set; }
        }
    }
}
