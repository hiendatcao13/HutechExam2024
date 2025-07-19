using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Server.Configurations;
using Microsoft.Extensions.Options;
using Hutech.Exam.Shared.Models;
using AutoMapper;

namespace Hutech.Exam.Server.Authentication
{
    public class JwtAuthenticationManager(IOptions<JwtConfiguration> jwtConfig, IMapper mapper, SinhVienService sinhVienService, UserService userService)
    {

        private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;
        private readonly IMapper _mapper = mapper;

        private readonly SinhVienService _sinhVienService = sinhVienService;
        private readonly UserService _userService = userService;

        public async Task<UserSession?> GenerateJwtTokenSinhVien(string username)
        {
            //username chính là ma_so_sinh_vien
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            /*Xác thực sinh viên có tồn tại trong database không ?*/
            SinhVienDto sinhVien = await _sinhVienService.SelectBy_ma_so_sinh_vien(username);
            if (sinhVien == null || sinhVien.MaSoSinhVien == null)
            {
                return null;
            }
            /*Tạo JWT token*/
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(_jwtConfig.TokenValidityMinutes_Student);
            var tokenKey = Encoding.ASCII.GetBytes(_jwtConfig.SecurityKey);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                // claim lưu là mã sinh viên
                new Claim(ClaimTypes.Name, sinhVien.MaSinhVien.ToString()),
                new Claim(ClaimTypes.Role, "SinhVien"), // nhận biết là sinh viên hay nhóm quản trị nội bộ
                new Claim(ClaimTypes.NameIdentifier, sinhVien.MaSinhVien.ToString())
            });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var sigingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = sigingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            /*Trả dữ liệu về UserSession*/
            var userSession = new UserSession
            {
                Name = sinhVien.HoVaTenLot + sinhVien.TenSinhVien + "",
                Username = sinhVien.MaSinhVien.ToString(),
                Token = token,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                NavigateSinhVien = sinhVien,
                Roles = ["SinhVien"]
            };
            return userSession;
        }
        // Overloading for monitor, admin
        public async Task<UserSession?> GenerateJwtTokenAdmin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            /*Xác thực user có tồn tại trong database không ?*/
            User user = await _userService.Login(username);
            if (user == null || string.IsNullOrEmpty(user.LoginName))
            {
                return null;
            }
            // Kiểm tra mật khẩu có đúng không ?
            if (!VerifyPassword(password, user.Password))
            {
                await UpdateLoginFail(user.UserId);
                return null;
            }

            /*Tạo JWT token*/
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(_jwtConfig.TokenValidityMinutes_Admin);
            var tokenKey = Encoding.ASCII.GetBytes(_jwtConfig.SecurityKey);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()), // lưu id
                new(ClaimTypes.Name, user.Name), // lưu tên
                new(ClaimTypes.Role, "QuanTri"), // nhận biết là sinh viên hay nhóm quản trị nội bộ
                new(ClaimTypes.Role, user.MaRoleNavigation.TenRole) // lưu vai trò
            });
            var sigingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = sigingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            /*Trả dữ liệu về UserSession*/
            var userSession = new UserSession
            {
                Name = user.Name,
                Username = user.UserId.ToString(),
                Token = token,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                NavigateUser = _mapper.Map<UserDto>(user),
                Roles = ["QuanTri", user.MaRoleNavigation.TenRole]
            };
            return userSession;
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        private async Task UpdateLoginFail(Guid userId)
        {
            await _userService.LoginFail(userId);
        }
    }
}
