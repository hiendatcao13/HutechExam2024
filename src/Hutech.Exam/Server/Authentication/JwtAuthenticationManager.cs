using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace Hutech.Exam.Server.Authentication
{
    public class JwtAuthenticationManager
    {

        public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN4jGzQRyTZ3gdXkt7FeBJPLLD";
        public const int JWT_TOKEN_VALIDITY_MINS_SV = 150; // thời gian cho sinh viên giữ token là 2 tiếng rưỡi
        public const int JWT_TOKEN_VALIDITY_MINS_ADMIN = 1440; // thời gian cho admin giữ token là 1 ngày

        private readonly SinhVienService _sinhVienService;
        private readonly UserService _userService;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        public JwtAuthenticationManager(SinhVienService sinhVienService)
        {
            _sinhVienService = sinhVienService;
            _userService = null!;
        }
        //Overloading for admin, monitor
        public JwtAuthenticationManager(UserService userService)
        {
            _userService = userService;
            _sinhVienService = null!;
        }
        public async Task<UserSession?> GenerateJwtToken(string username)
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
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS_SV);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
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
        public async Task<UserSession?> GenerateJwtToken(string username, string password)
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
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS_ADMIN);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
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
