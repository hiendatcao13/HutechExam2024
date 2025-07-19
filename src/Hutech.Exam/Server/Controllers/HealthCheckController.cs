using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/healths")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class HealthCheckController(HealthCheckService healthCheckService, SystemService systemService, RedisService redisService) : Controller
    {
        #region Private Fields
        private readonly HealthCheckService _healthCheckService = healthCheckService;
        private readonly SystemService _systemService = systemService;
        private readonly RedisService _redisService = redisService;
        #endregion

        #region GET Methods
        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            var entries = report.Entries;

            var result = new List<CustomHealthCheck>();

            result.AddRange(entries.Select(entry => new CustomHealthCheck
            {
                ServiceName = entry.Key,
                Status = entry.Value.Status.ToString(),
                Description = entry.Value.Description,
                Exception = entry.Value.Exception?.Message,
                Duration = entry.Value.Duration.ToString()
            }));

            return Ok(APIResponse<List<CustomHealthCheck>>.SuccessResponse(data: result, message: "Kiểm tra hệ thống thành công"));
        }

        [HttpGet("fragments")]
        public async Task<IActionResult> ThongKeDoPhanManh()
        {
            var result = await _systemService.ThongKeDoPhanManh();
            return Ok(APIResponse<List<CustomThongKeDoPhanManh>>.SuccessResponse(data: result, message: "Lấy dữ liệu thống kê thành công"));
        }


        #endregion

        #region PUT Methods
        [HttpPut("reorganize-index")]
        public async Task<IActionResult> RebuildOrReorganizeChiMuc()
        {
            var result = await _systemService.RebuildOrReorganizeChiMuc();
            if (!result)
            {
                return BadRequest(APIResponse<bool>.ErrorResponse(message: "Tái tạo lại chỉ mục thất bại hoặc không có chỉ mục cần được tái tạo"));
            }
            return Ok(APIResponse<bool>.SuccessResponse(message: "Tái tạo lại chỉ mục thành công"));
        }
        #endregion
    }
}
