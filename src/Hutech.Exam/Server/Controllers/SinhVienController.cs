using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SinhVienController : Controller
    {
        private readonly SinhVienService _sinhVienService;
        private static int SO_PHUT_TOI_THIEU = 150; // số phút tối thiểu sinh viên có thể đăng nhập lần kế tiếp nếu sv quên đăng xuất

        public SinhVienController(SinhVienService sinhVienService)
        {
            _sinhVienService = sinhVienService;
        }
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
            await _sinhVienService.Logout(sinhVien.MaSinhVien, DateTime.Now);
            return Ok();
        }
        [HttpGet("SelectBy_MSSV")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SinhVienDto>> SelectBy_MSSV([FromQuery] string ma_so_sinh_vien)
        {
            return await _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
        }









        private async Task UpdateLogin(long ma_sinh_vien)
        {
            await _sinhVienService.Login(ma_sinh_vien, DateTime.Now);
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
    }
}
