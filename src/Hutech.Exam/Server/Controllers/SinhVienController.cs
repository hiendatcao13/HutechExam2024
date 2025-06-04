using System.Data.SqlClient;
using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.SinhVien;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/sinhviens")]
    [ApiController]
    [Authorize]
    public class SinhVienController(SinhVienService sinhVienService, IHubContext<AdminHub> adminHub, IHubContext<SinhVienHub> sinhVienHub, RedisService redisService, JwtAuthenticationManager jwtAuthenticationManager) : Controller
    {
        private readonly SinhVienService _sinhVienService = sinhVienService;

        private readonly IHubContext<AdminHub> _adminHub = adminHub;
        private readonly IHubContext<SinhVienHub> _sinhVienHub = sinhVienHub;

        private readonly RedisService _redisService = redisService;

        private const int SO_PHUT_TOI_THIEU = 150; // số phút tối thiểu sinh viên có thể đăng nhập lần kế tiếp nếu sv quên đăng xuất

        private readonly JwtAuthenticationManager _jwtAuthenticationManager = jwtAuthenticationManager;

        //////////////////GET///////////////////////////

        [HttpGet("filter-by-mssv")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SinhVienDto>> SelectBy_MSSV([FromQuery] string maSoSinhVien)
        {
            var result = await _sinhVienService.SelectBy_ma_so_sinh_vien(maSoSinhVien);
            if(result == null || string.IsNullOrEmpty(result.MaSoSinhVien))
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Không tìm thấy sinh viên với mã số sinh viên đã cung cấp."));
            }    
            return Ok(APIResponse<SinhVienDto>.SuccessResponse(data: result, message: "Đã tìm thấy sinh viên thành công"));
        }

        [HttpGet("filter-by-lop-paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Paged<SinhVienDto>>> SelectBy_MaLop_Paged([FromQuery] int maLop, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _sinhVienService.SelectBy_ma_lop_Paged(maLop, pageNumber, pageSize);
            return Ok(APIResponse<Paged<SinhVienDto>>.SuccessResponse(data: result, message: "Lấy danh sách sinh viên theo lớp thành công"));
        }

        [HttpGet("filter-by-lop-search-paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Paged<SinhVienDto>>> SelectBy_MaLop_Paged_Search([FromQuery] int maLop, [FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _sinhVienService.SelectBy_ma_lop_Search_Paged(maLop, keyword, pageNumber, pageSize);
            return Ok(APIResponse<Paged<SinhVienDto>>.SuccessResponse(data: result, message: "Lấy danh sách sinh viên theo lớp thành công"));
        }

        //////////////////POST///////////////////////////

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SinhVienDto>> Insert([FromBody] SinhVienCreateRequest sinhVien)
        {
            try
            {
                var id = await _sinhVienService.Insert(sinhVien);
                return Ok(APIResponse<SinhVienDto>.SuccessResponse(data: await _sinhVienService.SelectOne(id), message: "Thêm sinh viên thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<SinhVienDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Thêm sinh viên không thành công", errorDetails: ex.Message));
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Verify([FromBody] SinhVienAuthenticationRequest account)
        {
            var userSession = await _jwtAuthenticationManager.GenerateJwtTokenSinhVien(account.Username);
            if (userSession != null && userSession.NavigateSinhVien != null && CheckLogin(userSession.NavigateSinhVien))
            {
                await UpdateLogin(userSession.NavigateSinhVien.MaSinhVien);
                return Ok(APIResponse<UserSession>.SuccessResponse(data: userSession, message: "Xác thực thành công"));
            }
            else
            {
                return Unauthorized(APIResponse<UserSession>.UnauthorizedResponse(message: "Không thể xác thực người dùng hoặc tài khoản đang được người khác sử dụng.Vui lòng kiểm tra lại và báo cho người giám sát nếu cần thiết"));
            }
        }

        [HttpPost("{id}/logout")]
        public async Task<ActionResult> UpdateLogout([FromRoute] int id)
        {
            await _sinhVienService.Logout(id, DateTime.Now);
            await NotifyAuthenticationToAdmin(id, false, DateTime.Now);
            return Ok(APIResponse<SinhVienDto>.SuccessResponse("Đăng xuất tài khoản thí sinh thành công"));
        }

        //////////////////PUT///////////////////////////

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SinhVienDto>> Update([FromRoute] long id, [FromBody] SinhVienUpdateRequest sinhVien)
        {
            try
            {
                var result = await _sinhVienService.Update(id, sinhVien);
                if (!result)
                {
                    return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Không tìm thấy sinh viên cần cập nhật"));
                }
                else
                {
                    return Ok(APIResponse<SinhVienDto>.SuccessResponse(data: await _sinhVienService.SelectOne(id), message: "Cập nhật sinh viên thành công"));
                }
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<SinhVienDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Cập nhật sinh viên không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////PATCH///////////////////////////

        [HttpPatch("{id}/reset-login")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ResetLogin([FromRoute] int id)
        {
            // liên quan redis nên tốt nhất là try catch
            try
            {
                await NotifyLogOutToSV(id);
                return Ok(APIResponse<SinhVienDto>.SuccessResponse("Đăng xuất tài khoản cho thí sinh thành công"));
            }
            catch(Exception ex)
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Cố lỗi khi cố gắng truy cập redis", errorDetails: ex.Message));
            }
        }

        [HttpPatch("{id}/submit-exam")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SubmitExam([FromRoute] int id)
        {
            try
            {
                await NotifyNopBaiToSV(id);
                return Ok(APIResponse<SinhVienDto>.SuccessResponse("Nộp bài cho thí sinh thành công"));
            }
            catch(Exception ex)
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Có lỗi khi cố gắng truy cập vào redis", errorDetails: ex.Message));
            }
        }

        //////////////////DELTE///////////////////////////

        [HttpDelete("{id}/force")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ForceDelete([FromRoute] int id)
        {
            try
            {
                var result = await _sinhVienService.ForceRemove(id);
                if (!result)
                {
                    return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Không tìm thấy sinh viên cần xóa"));
                }
                return Ok(APIResponse<SinhVienDto>.SuccessResponse(message: "Xóa sinh viên thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<SinhVienDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Xóa sinh viên không thành công", errorDetails: ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _sinhVienService.Remove(id);
                if (!result)
                {
                    return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Không tìm thấy sinh viên cần xóa"));
                }
                return Ok(APIResponse<SinhVienDto>.SuccessResponse(message: "Xóa sinh viên thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<SinhVienDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<SinhVienDto>.ErrorResponse(message: "Xóa sinh viên không thành công", errorDetails: ex.Message));
            }
        }

        //////////////////OTHERS///////////////////////////





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
                await _sinhVienHub.Clients.Client(connectionId).SendAsync("ResetLogin");
            }    
        }

        private async Task NotifyNopBaiToSV(long ma_sinh_vien)
        {
            string? connectionId = await GetConnectionIdAsync(ma_sinh_vien);
            if (connectionId != null)
            {
                await _sinhVienHub.Clients.Client(connectionId).SendAsync("SubmitExam");
            }
        }
    }
}
