using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChiTietCaThiController : Controller
    {
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly CaThiService _caThiService;
        private readonly SinhVienService _sinhVienService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly DotThiService _dotThiService;
        private readonly IHubContext<MainHub> _mainHub;
        

        public ChiTietCaThiController(ChiTietCaThiService chiTietCaThiService, CaThiService caThiService,
            SinhVienService sinhVienService, ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService,
            MonHocService monHocService, DotThiService dotThiService, IHubContext<MainHub> mainHub)
        {
            _chiTietCaThiService = chiTietCaThiService;
            _caThiService = caThiService;
            _sinhVienService = sinhVienService;
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _dotThiService = dotThiService;
            _mainHub = mainHub;
        }
        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Insert([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            if (chiTietCaThi.MaCaThi == null || chiTietCaThi.MaSinhVien == null || chiTietCaThi.MaDeThi == null)
                return BadRequest("Thông tin bị thiếu xót. Vui lòng kiểm tra");
            if(_sinhVienService.SelectOne((long)chiTietCaThi.MaSinhVien) == null)
                return BadRequest("Sinh viên hiện không tồn tại trong hệ thống. Vui lòng thêm sinh viên trước!");
            await _chiTietCaThiService.Insert((int)chiTietCaThi.MaCaThi, (long)chiTietCaThi.MaSinhVien, (long)chiTietCaThi.MaDeThi, 0);
            return Ok();
        }

        [HttpGet("SelectBy_MSSVThi")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectBy_MSSVThi([FromQuery] long ma_sinh_vien)
        {
            List<ChiTietCaThiDto> result = await _chiTietCaThiService.SelectBy_MaSinhVienThi(ma_sinh_vien, DateTime.Now);
            foreach (var item in result)
            {
                item.MaCaThiNavigation = (item.MaCaThi != null) ? await GetCaThi((int)item.MaCaThi) : null;
                item.MaSinhVienNavigation = await GetSinhVien(ma_sinh_vien);
            }
            // Chỉ lấy ca thi gần sát với thời gian hiện tại - tránh lấy nhiều ca thi về
            ChiTietCaThiDto? chi_tiet_ca_thi_gan_nhat = result.OrderBy(p => Math.Abs((p.MaCaThiNavigation.ThoiGianBatDau - DateTime.Now).TotalMinutes)).FirstOrDefault();
            //TH thí sinh không có ca thi
            if (chi_tiet_ca_thi_gan_nhat == null)
            {
                return new ChiTietCaThiDto { MaSinhVienNavigation = await GetSinhVien(ma_sinh_vien)};
            }
            return chi_tiet_ca_thi_gan_nhat;
        }
        [HttpPut("UpdateBatDauThi")]
        public async Task<ActionResult> UpdateBatDauThi([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            chiTietCaThi.ThoiGianBatDau = DateTime.Now;
            await _chiTietCaThiService.UpdateBatDau(chiTietCaThi);
            await NotifSVStatusThiToAdmin(chiTietCaThi.MaChiTietCaThi, true, chiTietCaThi.ThoiGianBatDau ?? DateTime.Now);
            return Ok();
        }
        [HttpPut("UpdateKetThucThi")]
        public async Task<ActionResult> UpdateKetThucThi([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            chiTietCaThi.ThoiGianKetThuc = DateTime.Now;
            await _chiTietCaThiService.UpdateKetThuc(chiTietCaThi);
            await NotifSVStatusThiToAdmin(chiTietCaThi.MaChiTietCaThi, false, chiTietCaThi.ThoiGianBatDau ?? DateTime.Now);
            return Ok();
        }
        [HttpGet("SelectBy_MaCaThi")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ChiTietCaThiDto>>> GetThongTinCTCaThiTheoMaCaThi([FromQuery] int ma_ca_thi)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            List<ChiTietCaThiDto> result = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach (var item in result)
                if(item.MaSinhVien != null)
                    item.MaSinhVienNavigation = await GetThongTinSinhVien((long)item.MaSinhVien);
            return result;
        }
        [HttpPut("CongGioSinhVien")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CongGioSinhVien([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            await _chiTietCaThiService.CongGio(chiTietCaThi.MaChiTietCaThi, chiTietCaThi.GioCongThem, chiTietCaThi.ThoiDiemCong ?? DateTime.Now, chiTietCaThi.LyDoCong ?? "");
            // báo cho tất cả admin
            await NotifyCongGioSVToAdmin(chiTietCaThi.MaChiTietCaThi);
            return Ok(true);
        }


        private async Task<SinhVienDto> GetThongTinSinhVien(long ma_sinh_vien)
        {
            return await _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private async Task<SinhVienDto?> GetSinhVien(long ma_sinh_vien)
        {
            return await _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private async Task<CaThiDto> GetCaThi(int ma_ca_thi)
        {
            CaThiDto caThi = await _caThiService.SelectOne(ma_ca_thi);
            caThi.MaChiTietDotThiNavigation = await GetChiTietDotThi(caThi.MaChiTietDotThi);
            return caThi;
        }
        private async Task<ChiTietDotThiDto> GetChiTietDotThi(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto chiTietDotThi = await _chiTietDotThiService.SelectOne(ma_chi_tiet_dot_thi);
            chiTietDotThi.MaDotThiNavigation = await GetDotThi(chiTietDotThi.MaDotThi);
            chiTietDotThi.MaLopAoNavigation = await GetLopAo(chiTietDotThi.MaLopAo);
            return chiTietDotThi;
        }
        private async Task<DotThiDto> GetDotThi(int ma_dot_thi)
        {
            return await _dotThiService.SelectOne(ma_dot_thi);
        }
        private async Task<LopAoDto> GetLopAo(int ma_lop_ao)
        {
            LopAoDto lopAo = await _lopAoService.SelectOne(ma_lop_ao);
            lopAo.MaMonHocNavigation = await GetMonHoc(ma_lop_ao);
            return lopAo;
        }
        private async Task<MonHocDto> GetMonHoc(int ma_mon_hoc)
        {
            return await _monHocService.SelectOne(ma_mon_hoc);
        }






        private async Task NotifSVStatusThiToAdmin(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian)
        {
            // 0: bắt đầu thi, 1: kết thúc thi
            await _mainHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian);
        }
        private async Task NotifyCongGioSVToAdmin(int ma_chi_tiet_ca_thi)
        {
            await _mainHub.Clients.Group("admin").SendAsync("CongGioSV", ma_chi_tiet_ca_thi);
        }
    }
}