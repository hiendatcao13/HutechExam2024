using System.Data.SqlClient;
using System.Threading.Tasks;
using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.User;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class UserController(UserService userService, JwtAuthenticationManager jwtAuthenticationManager) : Controller
    {
        #region Private Fields

        private readonly UserService _userService = userService;

        private readonly JwtAuthenticationManager _jwtAuthenticationManager = jwtAuthenticationManager;

        private const string NotFoundMessage = "Không tìm thấy người dùng";

        #endregion

        #region Get Methods

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll_Paged([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _userService.GetAll_Paged(pageNumber, pageSize);
            return Ok(APIResponse<Paged<UserDto>>.SuccessResponse(data: result, message: "Lấy danh sách người dùng thành công"));
        }

        [HttpGet("supervisor")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> GetAll_GiamThi_Paged([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _userService.GetAll_GiamThi_Paged(pageNumber, pageSize);
            return Ok(APIResponse<Paged<UserDto>>.SuccessResponse(data: result, message: "Lấy danh sách người dùng thành công"));
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll_Search_Paged([FromQuery] string keyword, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _userService.GetAll_Search_Paged(keyword, pageNumber, pageSize);
            return Ok(APIResponse<Paged<UserDto>>.SuccessResponse(data: result, message: "Lấy danh sách người dùng thành công"));
        }

        [HttpGet("search/supervisor")]
        [Authorize(Roles = "DaoTao")]
        public async Task<IActionResult> GetAll_Search_GiamThi_Paged([FromQuery] string keyword, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _userService.GetAll_Search_GiamThi_Paged(keyword, pageNumber, pageSize);
            return Ok(APIResponse<Paged<UserDto>>.SuccessResponse(data: result, message: "Lấy danh sách người dùng thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationRequest account)
        {
            var userSession = await _jwtAuthenticationManager.GenerateJwtTokenAdmin(account.Username, account.Password);
            if (userSession != null && userSession.NavigateUser != null)
            {
                if (userSession.NavigateUser.DaKhoa)
                {
                    return BadRequest(APIResponse<UserSession>.ErrorResponse(message: "Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên!"));
                }
                if (userSession.NavigateUser.DaXoa)
                {
                    return BadRequest(APIResponse<UserSession>.ErrorResponse(message: "Tài khoản đã bị xóa. Vui lòng liên hệ quản trị viên!"));
                }
                await UpdateLoginSuccess(userSession.NavigateUser.MaNguoiDung);
                return Ok(APIResponse<UserSession>.SuccessResponse(data: userSession, message: "Xác thực danh tính thành công"));
            }
            else
            {
                return Unauthorized(APIResponse<UserSession>.ErrorResponse(message: "Tài khoản không tồn tại"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Insert([FromBody] UserCreateRequest user)
        {
            bool isExist = await _userService.CheckExistName(user.LoginName, user.Email);
            if (isExist)
            {
                return BadRequest(APIResponse<UserDto>.ErrorResponse(message: "Tên đăng nhập hoặc email đã tồn tại. Vui lòng kiểm tra"));
            }
            var id = await _userService.Insert(user);
            return Ok(APIResponse<UserDto>.SuccessResponse(data: await _userService.SelectOne(id), message: "Thêm người dùng thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserUpdateRequest user)
        {
            bool isExist = await _userService.CheckExistName(user.LoginName, user.Email);
            if (isExist)
            {
                return BadRequest(APIResponse<UserDto>.ErrorResponse(message: "Tên đăng nhập hoặc email đã tồn tại. Vui lòng kiểm tra"));
            }

            var result = await _userService.Update(id, user);
            if (!result)
            {
                return BadRequest(APIResponse<UserDto>.ErrorResponse(message: "Không tìm thấy người dùng cần cập nhật"));
            }
            else
            {
                return Ok(APIResponse<UserDto>.SuccessResponse(data: await _userService.SelectOne(id), message: "Cập nhật người dùng thành công"));
            }
        }


        #endregion

        #region Patch Methods

        [HttpPatch("{id:Guid}/change-password")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid id, [FromBody] UserUpdatePasswordRequest user)
        {
            var result = await _userService.UpdatePassword(id, user);
            if (!result)
            {
                return BadRequest(APIResponse<UserDto>.ErrorResponse(message: "Không tìm thấy người dùng cần cập nhật mật khẩu"));
            }
            return Ok(APIResponse<UserDto>.SuccessResponse(message: "Cập nhật mậu khẩu người dùng thành công"));
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.Remove(id);
            if (!result)
            {
                return BadRequest(APIResponse<UserDto>.ErrorResponse(message: "Xóa người dùng không thành công hoặc đang dính phải ràng buộc khóa ngoại"));
            }
            return Ok(APIResponse<UserDto>.SuccessResponse(message: "Xóa người dùng thành công"));
        }

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
