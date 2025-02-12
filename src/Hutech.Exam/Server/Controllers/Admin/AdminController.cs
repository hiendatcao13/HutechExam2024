using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly DotThiService _dotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly SinhVienService _sinhVienService;
        private readonly ChiTietBaiThiService _chiTietBaiThiService;
        public AdminController(UserService userService, CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, DotThiService dotThiService,
            LopAoService lopAoService, MonHocService monHocService, ChiTietCaThiService chiTietCaThiService, SinhVienService sinhVienService, ChiTietBaiThiService chiTietBaiThiService)
        {
            _userService = userService;
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _dotThiService = dotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _chiTietCaThiService = chiTietCaThiService;
            _sinhVienService = sinhVienService;
            _chiTietBaiThiService = chiTietBaiThiService;
        }
        //API Manager & Control
        [HttpPost("Verify")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Verify([FromQuery] string loginName, [FromQuery] string password)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_userService);
            var userSession = await JwtAuthencationManager.GenerateJwtToken(loginName, password);
            if (userSession == null)
            {
                return Unauthorized();
            }
            else
            {
                //UpdateLogin(string username, string password);
                return userSession;
            }
        }
        [HttpPost("getThongTinUser")]
        public async Task<ActionResult<UserDto>> getThongTinUser([FromQuery] string loginName)
        {
            return await _userService.SelectByLoginName(loginName);
        }
        [HttpGet("UpdateTinhTrangCaThi")]
        public async Task<ActionResult> UpdateTinhTrangCaThi([FromQuery] int ma_ca_thi, [FromQuery] bool isActived)
        {
            await _caThiService.ca_thi_Activate(ma_ca_thi, isActived);
            return Ok();
        }
        [HttpGet("KetThucCaThi")]
        public async Task<ActionResult<bool>> KetThucCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> chiTietCaThis = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach(var item in chiTietCaThis)
            {
                if (!item.DaHoanThanh) // Yêu cầu kết thúc ca thi chỉ thực hiện khi các thí sinh đã hoàn thành bài thi
                    return Ok(false);
            }
            await _caThiService.ca_thi_Ketthuc(ma_ca_thi);
            await this.UpdateTinhTrangCaThi(ma_ca_thi, false);
            return Ok(true);
        }
        [HttpGet("HuyKichHoatCaThi")]
        public async Task<ActionResult> HuyKichHoatCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> chiTietCaThis = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach(var item in chiTietCaThis)
            {
                if (!item.DaHoanThanh) // cập nhật những thí sinh đang thi, chưa thi thành đã hoàn thành
                {
                    item.ThoiGianKetThuc = DateTime.Now;
                    await _chiTietCaThiService.UpdateKetThuc(item);
                }
                List<ChiTietBaiThiDto> chiTietBaiThis = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(item.MaChiTietCaThi);
                await this.removeListCTBT(chiTietBaiThis);
                await this.UpdateTinhTrangCaThi(ma_ca_thi, false);
            }
            return Ok();
        }
        [HttpGet("GetAllCaThi")]
        public async Task<ActionResult<List<CaThiDto>>> GetAllCaThi()
        {
            List<CaThiDto> result = await _caThiService.ca_thi_GetAll();
            foreach (var item in result)
                item.MaChiTietDotThiNavigation = await getThongTinChiTietDotThi(item.MaChiTietDotThi);
            //return new ApiResponse<List<CaThi>>(result);
            return result;
        }
        [HttpGet("GetThongTinCaThi")]
        public async Task<ActionResult<CaThiDto>> GetThongTinCaThi([FromQuery] int ma_ca_thi)
        {
            return await _caThiService.SelectOne(ma_ca_thi);
        }
        //API Monitor
        [HttpGet("GetThongTinCTCaThiTheoMaCaThi")]
        public async Task<ActionResult<List<ChiTietCaThiDto>>> GetThongTinCTCaThiTheoMaCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> result = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach (var item in result)
                if(item.MaSinhVien != null)
                    item.MaSinhVienNavigation = await getThongTinSinhVien((long)item.MaSinhVien);
            return result;
        }
        [HttpPost("UpdateLogoutSinhVien")]
        public async Task<ActionResult> UpdateLogoutSinhVien(long ma_sinh_vien)
        {
            await _sinhVienService.Logout(ma_sinh_vien, DateTime.Now);
            return Ok();
        }
        [HttpPost("CongGioSinhVien")]
        public async Task<ActionResult> CongGioSinhVien([FromBody]ChiTietCaThiDto chiTietCaThi)
        {
            await _chiTietCaThiService.CongGio(chiTietCaThi);
            return Ok();
        }
        [HttpGet("GetThongTinSinhVien")]
        public async Task<ActionResult<SinhVienDto>> GetThongTinSinhVien([FromQuery] string ma_so_sinh_vien)
        {
            SinhVienDto sinhVien = await _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
            return sinhVien;
        }
        [HttpDelete("DeleteCaThi")]
        public async Task<ActionResult> DeleteCaThi([FromQuery] int ma_ca_thi)
        {
            await _caThiService.Remove(ma_ca_thi);
            return Ok();
        }
        [HttpPost("UpdateCaThi")]
        public async Task<ActionResult> UpdateCaThi([FromBody] CaThiDto caThi)
        {
            await _caThiService.Update(caThi.MaCaThi, caThi.TenCaThi ?? "", caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi);
            return Ok();
        }
        private async Task<SinhVienDto> getThongTinSinhVien(long ma_sinh_vien)
        {
            return await _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private async Task<ChiTietDotThiDto> getThongTinChiTietDotThi(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto chiTietDotThi = await _chiTietDotThiService.SelectOne(ma_chi_tiet_dot_thi);
            chiTietDotThi.MaDotThiNavigation = await getThongTinDotThi(chiTietDotThi.MaDotThi);
            chiTietDotThi.MaLopAoNavigation = await getThongTinLopAo(chiTietDotThi.MaLopAo);
            return chiTietDotThi;
        }
        private async Task<DotThiDto> getThongTinDotThi(int ma_dot_thi)
        {
            return await _dotThiService.SelectOne(ma_dot_thi);
        }
        private async Task<LopAoDto> getThongTinLopAo(int ma_lop_ao)
        {
            LopAoDto lopAo = await _lopAoService.SelectOne(ma_lop_ao);
            lopAo.MaMonHocNavigation = await getThongTinMonHoc(ma_lop_ao);
            return lopAo;
        }
        private async Task<MonHocDto> getThongTinMonHoc(int ma_mon_hoc)
        {
            return await _monHocService.SelectOne(ma_mon_hoc);
        }
        private async Task removeListCTBT(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            foreach (var item in chiTietBaiThis)
                await _chiTietBaiThiService.Delete(item.MaChiTietBaiThi);
        }
    }
}
