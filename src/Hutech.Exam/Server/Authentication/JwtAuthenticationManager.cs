using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Server.Configurations;
using Microsoft.Extensions.Options;

namespace Hutech.Exam.Server.Authentication
{
    public class JwtAuthenticationManager(IOptions<JwtConfiguration> jwtConfig, SinhVienService sinhVienService, UserService userService)
    {

        private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;

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
                new Claim(ClaimTypes.Role, "User"), // nhận biết là admin hay sinh viên
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
                Role = "User"
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
            List<string> user = await _userService.Login(username);
            if (user == null || user.Count == 0)
            {
                return null;
            }
            // Kiểm tra mật khẩu có đúng không ?
            if(!VerifyPassword(password, user[2]))
            {
                await UpdateLoginFail(Guid.Parse(user[0]));
                return null;
            }
            UserDto navigateUser = await _userService.SelectByLoginName(username);
            //// kiểm tra xem tài khoản có bị khóa hoặc bị xóa không ?
            //if(navigateUser.IsLockedOut || navigateUser.IsDeleted)
            //{
            //    return null;
            //}
            /*Tạo JWT token*/
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(_jwtConfig.TokenValidityMinutes_Admin);
            var tokenKey = Encoding.ASCII.GetBytes(_jwtConfig.SecurityKey);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user[0]), // lưu id
                new(ClaimTypes.Name, user[1]), // lưu tên
                new(ClaimTypes.Role, "Admin") // nhận biết là admin hay sinh viên
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
                Name = user[1],
                Username = user[0],
                Token = token,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                NavigateUser = navigateUser,
                Role = "Admin"
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
