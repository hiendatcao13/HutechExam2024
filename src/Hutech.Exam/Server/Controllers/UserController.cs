﻿using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private static int SO_PHUT_TOI_THIEU = 150; // số phút tối thiểu sinh viên có thể đăng nhập lần kế tiếp nếu sv quên đăng xuất
        private readonly SinhVienService _sinhVienService;
        public UserController(SinhVienService sinhVienService) 
        {
            _sinhVienService = sinhVienService;
        }

        [HttpGet("Verify")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Verify([FromQuery]string ma_so_sinh_vien)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_sinhVienService);
            var userSession = await JwtAuthencationManager.GenerateJwtToken(ma_so_sinh_vien);
            if(userSession != null && userSession.NavigateSinhVien!= null && checkLogin(userSession.NavigateSinhVien))
            {
                await UpdateLogin(userSession.NavigateSinhVien.MaSinhVien);
                return userSession;
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool checkLogin(SinhVienDto sinhVien)
        {
            // đã có máy đăng nhập trước đó
            if (sinhVien.IsLoggedIn == true)
            {
                Console.WriteLine("Hello");
                // sinh viên quên đăng xuất và được truy cập vào sau n phút -> được vào
                if (sinhVien.LastLoggedIn != null && sinhVien.LastLoggedIn.Value.AddMinutes(SO_PHUT_TOI_THIEU) < DateTime.Now)
                    return true;
                else
                    return false;
            }
            return true;
        }
        private async Task UpdateLogin(long ma_sinh_vien)
        {
            await _sinhVienService.Login(ma_sinh_vien, DateTime.Now);
        }
        [HttpGet("UpdateLogout")]
        public async Task<ActionResult> UpdateLogout([FromQuery] long ma_sinh_vien)
        {
            await _sinhVienService.Logout(ma_sinh_vien, DateTime.Now);
            return Ok();
        }
    }
}
