using Hutech.Exam.Server.BUS;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaThiController : Controller
    {
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly ChiTietBaiThiService _chiTietBaiThiService;
        public CaThiController(CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, ChiTietCaThiService chiTietCaThiService, ChiTietBaiThiService chiTietBaiThiService)
        {
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _chiTietCaThiService = chiTietCaThiService;
            _chiTietBaiThiService = chiTietBaiThiService;
        }
        [HttpGet("IsActiveCaThi")]
        [Authorize]
        public async Task<ActionResult<bool>> IsActiveCaThi([FromQuery] int ma_ca_thi)
        {
            CaThiDto caThi = await _caThiService.SelectOne(ma_ca_thi);
            return (caThi.IsActivated);
        }
        [HttpGet("SelectOne")]
        public async Task<ActionResult<CaThiDto>> SelectOne([FromQuery] int ma_ca_thi)
        {
            return Ok(await _caThiService.SelectOne(ma_ca_thi));
        }
        [HttpGet("SelectBy_MaDotThi_MaLopAo_LanThi")]
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_MaDotThi_MaLopAo_LanThi([FromQuery] int ma_dot_thi, [FromQuery] int ma_lop_ao, [FromQuery] string lan_thi)
        {
            ChiTietDotThiDto chiTietDotThiDto = await GetCTDT_SelectBy_MaDotThi_MaLopAo_LanThi(ma_dot_thi, ma_lop_ao, lan_thi);
            return Ok(await _caThiService.SelectBy_ma_chi_tiet_dot_thi(chiTietDotThiDto.MaChiTietDotThi));

        }
        [HttpPut("KichHoatCaThi")]
        public async Task<ActionResult> KichHoatCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.ca_thi_Activate(ma_ca_thi, true);
            return Ok();
        }
        [HttpPut("HuyKichHoatCaThi")]
        public async Task<ActionResult> HuyKichHoatCaThi([FromBody] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> chiTietCaThis = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach (var item in chiTietCaThis)
            {
                if (!item.DaHoanThanh) // cập nhật những thí sinh đang thi, chưa thi thành đã hoàn thành
                {
                    item.DaHoanThanh = true;
                    item.ThoiGianKetThuc = DateTime.Now;
                    await _chiTietCaThiService.UpdateKetThuc(item);
                }
                List<ChiTietBaiThiDto> chiTietBaiThis = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(item.MaChiTietCaThi);
                await removeListCTBT(chiTietBaiThis);
            }
            await DungCaThi(ma_ca_thi);
            return Ok();
        }
        [HttpPut("DungCaThi")]
        public async Task<ActionResult> DungCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.ca_thi_Activate(ma_ca_thi, false);
            return Ok();
        }
        [HttpPut("KetThucCaThi")]
        public async Task<ActionResult<bool>> KetThucCaThi([FromBody] int ma_ca_thi)
        {
            List<ChiTietCaThiDto> chiTietCaThis = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach (var item in chiTietCaThis)
            {
                if (!item.DaHoanThanh) // Yêu cầu kết thúc ca thi chỉ thực hiện khi các thí sinh đã hoàn thành bài thi
                    return Ok(false);
            }
            await _caThiService.ca_thi_Ketthuc(ma_ca_thi);
            await DungCaThi(ma_ca_thi);
            return Ok(true);
        }




        private async Task<ChiTietDotThiDto> GetCTDT_SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, string lan_thi)
        {
            return await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo_LanThi(ma_dot_thi, ma_lop_ao, lan_thi);
        }
        private async Task removeListCTBT(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            foreach (var item in chiTietBaiThis)
                await _chiTietBaiThiService.Delete(item.MaChiTietBaiThi);
        }
    }
}
