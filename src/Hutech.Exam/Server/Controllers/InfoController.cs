using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.DependencyResolver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoController : ControllerBase
    {
        private readonly SinhVienService _sinhVienService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly DotThiService _dotThiService;
        public InfoController(SinhVienService sinhVienService, ChiTietCaThiService chiTietCaThiService,
            CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService, MonHocService monHocService, DotThiService dotThiService)
        {
            _sinhVienService = sinhVienService;
            _chiTietCaThiService = chiTietCaThiService;
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _dotThiService = dotThiService;
        }
        [HttpGet("GetThongTinChiTietCaThi")]
        public async Task<ActionResult<List<ChiTietCaThiDto>>> GetThongTinChiTietCaThi([FromQuery] long ma_sinh_vien)
        {
            List<ChiTietCaThiDto> result = await _chiTietCaThiService.SelectBy_MaSinhVienThi(ma_sinh_vien, DateTime.Now);
            foreach (var item in result)
            {
                item.MaCaThiNavigation = (item.MaCaThi != null) ? await getThongTinCaThi((int)item.MaCaThi) : null;
                item.MaSinhVienNavigation = await getThongTinSV(ma_sinh_vien);
            }
            //TH thí sinh không có ca thi
            if(result.Count == 0)
            {
                ChiTietCaThiDto newChiTietCaThi = new ChiTietCaThiDto();
                newChiTietCaThi.MaSinhVienNavigation = await getThongTinSV(ma_sinh_vien);
                result.Add(newChiTietCaThi);
            }
            return result;
        }
        private async Task<SinhVienDto?> getThongTinSV(long ma_sinh_vien)
        {
            return await _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private async Task<CaThiDto> getThongTinCaThi(int ma_ca_thi)
        {
            CaThiDto caThi = await _caThiService.SelectOne(ma_ca_thi);
            caThi.MaChiTietDotThiNavigation = await getThongTinChiTietDotThi(caThi.MaChiTietDotThi);
            return caThi;
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
        [HttpPost("UpdateBatDauThi")]
        public async Task<ActionResult> UpdateBatDauThi([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            await _chiTietCaThiService.UpdateBatDau(chiTietCaThi);
            return Ok();
        }
    }
}
