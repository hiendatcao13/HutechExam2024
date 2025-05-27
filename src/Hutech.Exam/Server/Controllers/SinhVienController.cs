using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.SinhVien;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/sinhviens")]
    [ApiController]
    [Authorize]
    public class SinhVienController(SinhVienService sinhVienService, IHubContext<AdminHub> adminHub, RedisService redisService) : Controller
    {
        private readonly SinhVienService _sinhVienService = sinhVienService;

        private readonly IHubContext<AdminHub> _adminHub = adminHub;

        private readonly RedisService _redisService = redisService;

        private const int SO_PHUT_TOI_THIEU = 150; // số phút tối thiểu sinh viên có thể đăng nhập lần kế tiếp nếu sv quên đăng xuất

        //////////////////CRUD///////////////////////////

        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-mssv")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SinhVienDto>> SelectBy_MSSV([FromQuery] string maSoSinhVien)
        {
            return Ok(await _sinhVienService.SelectBy_ma_so_sinh_vien(maSoSinhVien));
        }

        //////////////////OTHERS///////////////////////////

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Verify([FromBody] SinhVienAuthenticationRequest account)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_sinhVienService);
            var userSession = await JwtAuthencationManager.GenerateJwtToken(account.Username);
            if (userSession != null && userSession.NavigateSinhVien != null && CheckLogin(userSession.NavigateSinhVien))
            {
                await UpdateLogin(userSession.NavigateSinhVien.MaSinhVien);
                return Ok(userSession);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}/logout")]
        public async Task<ActionResult> UpdateLogout([FromRoute] int id)
        {
            await _sinhVienService.Logout(id, DateTime.Now);
            await NotifyAuthenticationToAdmin(id, false, DateTime.Now);
            return Ok();
        }

        [HttpPut("{id}/reset-login")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ResetLogin([FromRoute] int id)
        {
            await NotifyLogOutToSV(id);
            return Ok();
        }

        [HttpPut("{id}/submit-exam")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SubmitExam([FromRoute] int id)
        {
            await NotifyNopBaiToSV(id);
            return Ok();
        }

        //////////////////PRIVATE///////////////////////////

        private async Task UpdateLogin(long ma_sinh_vien)
        {
            DateTime current_time = DateTime.Now;
            await _sinhVienService.Login(ma_sinh_vien, current_time);
            await NotifyAuthenticationToAdmin(ma_sinh_vien, true, current_time);
        }

        private bool CheckLogin(SinhVienDto sinhVien)
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
            return await _redisService.GetConnectionIdAsync(ma_sinh_vien);
        }

        private async Task NotifyAuthenticationToAdmin(long ma_sinh_vien, bool isLogin, DateTime thoi_gian)
        {
           
            await _adminHub.Clients.Group("admin").SendAsync("SV_Authentication", ma_sinh_vien, isLogin, thoi_gian);
        }

        private async Task NotifyLogOutToSV(long ma_sinh_vien)
        {
            string? connectionId = await GetConnectionIdAsync(ma_sinh_vien);
            if(connectionId != null)
            {
                await _adminHub.Clients.Client(connectionId).SendAsync("ResetLogin");
            }    
        }

        private async Task NotifyNopBaiToSV(long ma_sinh_vien)
        {
            string? connectionId = await GetConnectionIdAsync(ma_sinh_vien);
            if (connectionId != null)
            {
                await _adminHub.Clients.Client(connectionId).SendAsync("SubmitExam");
            }
        }
    }
}
