using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SinhVienController(SinhVienService sinhVienService, IHubContext<AdminHub> mainHub, IConnectionMultiplexer redis) : Controller
    {
        private readonly SinhVienService _sinhVienService = sinhVienService;
        private readonly IHubContext<AdminHub> _mainHub = mainHub;
        private readonly IDatabase _redisDb = redis.GetDatabase();


        private const int SO_PHUT_TOI_THIEU = 150; // số phút tối thiểu sinh viên có thể đăng nhập lần kế tiếp nếu sv quên đăng xuất

        [HttpPut("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Verify([FromBody] string ma_so_sinh_vien)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_sinhVienService);
            var userSession = await JwtAuthencationManager.GenerateJwtToken(ma_so_sinh_vien);
            if (userSession != null && userSession.NavigateSinhVien != null && checkLogin(userSession.NavigateSinhVien))
            {
                await UpdateLogin(userSession.NavigateSinhVien.MaSinhVien);
                return Ok(userSession);
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPut("UpdateLogout")]
        public async Task<ActionResult> UpdateLogout([FromBody] SinhVienDto sinhVien)
        {
            DateTime current_time = DateTime.Now;
            await _sinhVienService.Logout(sinhVien.MaSinhVien, current_time);
            await NotifyAuthenticationToAdmin(sinhVien.MaSinhVien, false, current_time);
            return Ok();
        }
        [HttpGet("SelectBy_MSSV")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SinhVienDto>> SelectBy_MSSV([FromQuery] string ma_so_sinh_vien)
        {
            return Ok(await _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien));
        }
        [HttpPut("ResetLogin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ResetLogin([FromBody] SinhVienDto sinhVien)
        {
            await NotifyLogOutToSV(sinhVien.MaSinhVien);
            return Ok();
        }
        [HttpPut("SubmitExam")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SubmitExam([FromBody] SinhVienDto sinhVien)
        {
            await NotifyNopBaiToSV(sinhVien.MaSinhVien);
            return Ok();
        }








        private async Task UpdateLogin(long ma_sinh_vien)
        {
            DateTime current_time = DateTime.Now;
            await _sinhVienService.Login(ma_sinh_vien, current_time);
            await NotifyAuthenticationToAdmin(ma_sinh_vien, true, current_time);
        }
        private bool checkLogin(SinhVienDto sinhVien)
        {
            // đã có máy đăng nhập trước đó
            if (sinhVien.IsLoggedIn == true)
            {
                // sinh viên quên đăng xuất và được truy cập vào sau n phút -> được vào
                if (sinhVien.LastLoggedIn != null && sinhVien.LastLoggedIn.Value.AddMinutes(SO_PHUT_TOI_THIEU) < DateTime.Now)
                    return true;
                else
                    return false;
            }
            return true;
        }




        private async Task<string?> GetConnectionIdAsync(long ma_sinh_vien)
        {
            try
            {
                var key = $"connection:{ma_sinh_vien}";
                var connectionId = await _redisDb.StringGetAsync(key);

                return connectionId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private async Task NotifyAuthenticationToAdmin(long ma_sinh_vien, bool isLogin, DateTime thoi_gian)
        {
            await _mainHub.Clients.Group("admin").SendAsync("SV_Authentication", ma_sinh_vien, isLogin, thoi_gian);
        }
        private async Task NotifyLogOutToSV(long ma_sinh_vien)
        {
            string? connectionId = await GetConnectionIdAsync(ma_sinh_vien);
            if(connectionId != null)
            {
                await _mainHub.Clients.Client(connectionId).SendAsync("ResetLogin");
            }    
        }
        private async Task NotifyNopBaiToSV(long ma_sinh_vien)
        {
            string? connectionId = await GetConnectionIdAsync(ma_sinh_vien);
            if (connectionId != null)
            {
                await _mainHub.Clients.Client(connectionId).SendAsync("SubmitExam");
            }
        }
    }
}
