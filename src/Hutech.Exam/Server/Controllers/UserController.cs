﻿using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController(UserService userService, JwtAuthenticationManager jwtAuthenticationManager) : Controller
    {
        #region Private Fields

        private readonly UserService _userService = userService;

        private readonly JwtAuthenticationManager _jwtAuthenticationManager = jwtAuthenticationManager;

        #endregion

        #region Get Methods



        #endregion

        #region Post Methods

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Login([FromBody] UserAuthenticationRequest account)
        {
            var userSession = await _jwtAuthenticationManager.GenerateJwtTokenAdmin(account.Username, account.Password);
            if (userSession != null && userSession.NavigateUser != null)
            {
                if (userSession.NavigateUser.IsLockedOut)
                {
                    return BadRequest(APIResponse<UserSession>.ErrorResponse(message: "Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên!"));
                }
                if (userSession.NavigateUser.IsDeleted)
                {
                    return BadRequest(APIResponse<UserSession>.ErrorResponse(message: "Tài khoản đã bị xóa. Vui lòng liên hệ quản trị viên!"));
                }
                await UpdateLoginSuccess(userSession.NavigateUser.UserId);
                return Ok(APIResponse<UserSession>.SuccessResponse(data: userSession, message: "Xác thực danh tính thành công"));
            }
            else
            {
                return Unauthorized(APIResponse<UserSession>.ErrorResponse(message: "Tài khoản không tồn tại"));
            }
        }

        #endregion

        #region Put Methods



        #endregion

        #region Patch Methods



        #endregion

        #region Delete Methods



        #endregion

        #region Private Methods

        private async Task UpdateLoginSuccess(Guid userId)
        {
            await _userService.LoginSuccess(userId);
        }

        public async Task<bool> UpdateLastActivity(Guid userId, DateTime lastActivityDate)
        {
            return await _userService.UpdateLastActivity(userId, lastActivityDate);
        }

        #endregion

    }
}
