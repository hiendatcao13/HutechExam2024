using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public ChiTietCaThiController(ChiTietCaThiService chiTietCaThiService, CaThiService caThiService,
            SinhVienService sinhVienService, ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService,
            MonHocService monHocService, DotThiService dotThiService)
        {
            _chiTietCaThiService = chiTietCaThiService;
            _caThiService = caThiService;
            _sinhVienService = sinhVienService;
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _dotThiService = dotThiService;
        }

        [HttpGet("SelectBy_MSSVThi")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectBy_MSSVThi([FromQuery] long ma_sinh_vien)
        {
            List<ChiTietCaThiDto> result = await _chiTietCaThiService.SelectBy_MaSinhVienThi(ma_sinh_vien, DateTime.Now);
            foreach (var item in result)
            {
                item.MaCaThiNavigation = (item.MaCaThi != null) ? await getCaThi((int)item.MaCaThi) : null;
                item.MaSinhVienNavigation = await getSinhVien(ma_sinh_vien);
            }
            // Chỉ lấy ca thi gần sát với thời gian hiện tại - tránh lấy nhiều ca thi về
            ChiTietCaThiDto? chi_tiet_ca_thi_gan_nhat = result.OrderBy(p => Math.Abs((p.MaCaThiNavigation.ThoiGianBatDau - DateTime.Now).TotalMinutes)).FirstOrDefault();
            //TH thí sinh không có ca thi
            if (chi_tiet_ca_thi_gan_nhat == null)
            {
                return new ChiTietCaThiDto { MaSinhVienNavigation = await getSinhVien(ma_sinh_vien)};
            }
            return chi_tiet_ca_thi_gan_nhat;
        }
        [HttpPut("UpdateBatDauThi")]
        public async Task<ActionResult> UpdateBatDauThi([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            await _chiTietCaThiService.UpdateBatDau(chiTietCaThi);
            return Ok();
        }
        [HttpPut("UpdateKetThuc")]
        public async Task<ActionResult> UpdateKetThuc([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            await _chiTietCaThiService.UpdateKetThuc(chiTietCaThi);
            return Ok();
        }





        private async Task<SinhVienDto?> getSinhVien(long ma_sinh_vien)
        {
            return await _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private async Task<CaThiDto> getCaThi(int ma_ca_thi)
        {
            CaThiDto caThi = await _caThiService.SelectOne(ma_ca_thi);
            caThi.MaChiTietDotThiNavigation = await getChiTietDotThi(caThi.MaChiTietDotThi);
            return caThi;
        }
        private async Task<ChiTietDotThiDto> getChiTietDotThi(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto chiTietDotThi = await _chiTietDotThiService.SelectOne(ma_chi_tiet_dot_thi);
            chiTietDotThi.MaDotThiNavigation = await getDotThi(chiTietDotThi.MaDotThi);
            chiTietDotThi.MaLopAoNavigation = await getLopAo(chiTietDotThi.MaLopAo);
            return chiTietDotThi;
        }
        private async Task<DotThiDto> getDotThi(int ma_dot_thi)
        {
            return await _dotThiService.SelectOne(ma_dot_thi);
        }
        private async Task<LopAoDto> getLopAo(int ma_lop_ao)
        {
            LopAoDto lopAo = await _lopAoService.SelectOne(ma_lop_ao);
            lopAo.MaMonHocNavigation = await getMonHoc(ma_lop_ao);
            return lopAo;
        }
        private async Task<MonHocDto> getMonHoc(int ma_mon_hoc)
        {
            return await _monHocService.SelectOne(ma_mon_hoc);
        }
    }
}