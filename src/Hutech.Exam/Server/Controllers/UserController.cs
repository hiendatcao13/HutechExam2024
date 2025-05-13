using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        [HttpPut("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Login([FromBody] AccountRequest account)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_userService);
            var userSession = await JwtAuthencationManager.GenerateJwtToken(account.Username, account.Password);
            if (userSession != null && userSession.NavigateUser != null)
            {
                if (userSession.NavigateUser.IsLockedOut)
                {
                    return BadRequest("Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên!");
                }
                if(userSession.NavigateUser.IsDeleted)
                {
                    return BadRequest("Tài khoản đã bị xóa. Vui lòng liên hệ quản trị viên!");
                }
                await UpdateLoginSuccess(userSession.NavigateUser.UserId);
                return userSession;
            }
            else
            {
                return Unauthorized();
            }
        }




        private async Task UpdateLoginSuccess(Guid userId)
        {
            await _userService.LoginSuccess(userId);
        }
        public async Task<int> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            return await _userService.UpdateLastActivity(userId, lastActivityDate);
        }
    }

    public class AccountRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
