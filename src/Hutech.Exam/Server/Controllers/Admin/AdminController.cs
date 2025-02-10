using Hutech.Exam.Server.Authentication;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared;
using Hutech.Exam.Shared.API;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

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
        public ActionResult<UserSession> Verify([FromQuery] string loginName, [FromQuery] string password)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_userService);
            var userSession = JwtAuthencationManager.GenerateJwtToken(loginName, password);
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
        public ActionResult<UserDto> getThongTinUser([FromQuery] string loginName)
        {
            return _userService.SelectByLoginName(loginName);
        }
        [HttpGet("UpdateTinhTrangCaThi")]
        public ActionResult UpdateTinhTrangCaThi([FromQuery] int ma_ca_thi, [FromQuery] bool isActived)
        {
            _caThiService.ca_thi_Activate(ma_ca_thi, isActived);
            return Ok();
        }
        [HttpGet("KetThucCaThi")]
        public ActionResult<bool> KetThucCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> chiTietCaThis = _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach(var item in chiTietCaThis)
            {
                if (!item.DaHoanThanh) // Yêu cầu kết thúc ca thi chỉ thực hiện khi các thí sinh đã hoàn thành bài thi
                    return Ok(false);
            }
            _caThiService.ca_thi_Ketthuc(ma_ca_thi);
            this.UpdateTinhTrangCaThi(ma_ca_thi, false);
            return Ok(true);
        }
        [HttpGet("HuyKichHoatCaThi")]
        public ActionResult HuyKichHoatCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> chiTietCaThis = _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach(var item in chiTietCaThis)
            {
                if (!item.DaHoanThanh) // cập nhật những thí sinh đang thi, chưa thi thành đã hoàn thành
                {
                    item.ThoiGianKetThuc = DateTime.Now;
                    _chiTietCaThiService.UpdateKetThuc(item);
                }
                List<ChiTietBaiThiDto> chiTietBaiThis = _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(item.MaChiTietCaThi);
                this.removeListCTBT(chiTietBaiThis);
                this.UpdateTinhTrangCaThi(ma_ca_thi, false);
            }
            return Ok();
        }
        [HttpGet("GetAllCaThi")]
        public ActionResult<List<CaThiDto>> GetAllCaThi()
        {
            List<CaThiDto> result = _caThiService.ca_thi_GetAll();
            foreach (var item in result)
                item.MaChiTietDotThiNavigation = getThongTinChiTietDotThi(item.MaChiTietDotThi);
            //return new ApiResponse<List<CaThi>>(result);
            return result;
        }
        [HttpGet("GetThongTinCaThi")]
        public ActionResult<CaThiDto> GetThongTinCaThi([FromQuery] int ma_ca_thi)
        {
            return _caThiService.SelectOne(ma_ca_thi);
        }
        //API Monitor
        [HttpGet("GetThongTinCTCaThiTheoMaCaThi")]
        public ActionResult<List<ChiTietCaThiDto>> GetThongTinCTCaThiTheoMaCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> result = _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach (var item in result)
                if(item.MaSinhVien != null)
                    item.MaSinhVienNavigation = getThongTinSinhVien((long)item.MaSinhVien);
            return result;
        }
        [HttpPost("UpdateLogoutSinhVien")]
        public ActionResult UpdateLogoutSinhVien(long ma_sinh_vien)
        {
            _sinhVienService.Logout(ma_sinh_vien, DateTime.Now);
            return Ok();
        }
        [HttpPost("CongGioSinhVien")]
        public ActionResult CongGioSinhVien([FromBody]ChiTietCaThiDto chiTietCaThi)
        {
            _chiTietCaThiService.CongGio(chiTietCaThi);
            return Ok();
        }
        [HttpGet("GetThongTinSinhVien")]
        public ActionResult<SinhVienDto> GetThongTinSinhVien([FromQuery] string ma_so_sinh_vien)
        {
            SinhVienDto sinhVien = _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
            return sinhVien;
        }
        [HttpDelete("DeleteCaThi")]
        public ActionResult DeleteCaThi([FromQuery] int ma_ca_thi)
        {
            _caThiService.Remove(ma_ca_thi);
            return Ok();
        }
        [HttpPost("UpdateCaThi")]
        public ActionResult UpdateCaThi([FromBody] CaThiDto caThi)
        {
            _caThiService.Update(caThi.MaCaThi, caThi.TenCaThi ?? "", caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi);
            return Ok();
        }
        private SinhVienDto getThongTinSinhVien(long ma_sinh_vien)
        {
            return _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private ChiTietDotThiDto getThongTinChiTietDotThi(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto chiTietDotThi = _chiTietDotThiService.SelectOne(ma_chi_tiet_dot_thi);
            chiTietDotThi.MaDotThiNavigation = getThongTinDotThi(chiTietDotThi.MaDotThi);
            chiTietDotThi.MaLopAoNavigation = getThongTinLopAo(chiTietDotThi.MaLopAo);
            return chiTietDotThi;
        }
        private DotThiDto getThongTinDotThi(int ma_dot_thi)
        {
            return _dotThiService.SelectOne(ma_dot_thi);
        }
        private LopAoDto getThongTinLopAo(int ma_lop_ao)
        {
            LopAoDto lopAo = _lopAoService.SelectOne(ma_lop_ao);
            lopAo.MaMonHocNavigation = getThongTinMonHoc(ma_lop_ao);
            return lopAo;
        }
        private MonHocDto getThongTinMonHoc(int ma_mon_hoc)
        {
            return _monHocService.SelectOne(ma_mon_hoc);
        }
        private void removeListCTBT(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            foreach (var item in chiTietBaiThis)
                _chiTietBaiThiService.Delete(item.MaChiTietBaiThi);
        }
    }
}
