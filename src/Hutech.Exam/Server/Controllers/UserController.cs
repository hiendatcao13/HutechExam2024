using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        //////////////////CRUD///////////////////////////

        //////////////////FILTER///////////////////////////

        //////////////////OTHERS///////////////////////////

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Login([FromBody] UserAuthenticationRequest account)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_userService);
            var userSession = await JwtAuthencationManager.GenerateJwtToken(account.Username, account.Password);
            if (userSession != null && userSession.NavigateUser != null)
            {
                if (userSession.NavigateUser.IsLockedOut)
                {
                    return BadRequest("Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên!");
                }
                if (userSession.NavigateUser.IsDeleted)
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

        //////////////////PRIVATE///////////////////////////

        private async Task UpdateLoginSuccess(Guid userId)
        {
            await _userService.LoginSuccess(userId);
        }

        public async Task<int> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            return await _userService.UpdateLastActivity(userId, lastActivityDate);
        }
    }
}
