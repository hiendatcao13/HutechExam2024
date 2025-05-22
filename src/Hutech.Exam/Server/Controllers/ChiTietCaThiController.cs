using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChiTietCaThiController(ChiTietCaThiService chiTietCaThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly ChiTietCaThiService _chiTietCaThiService = chiTietCaThiService;
        private readonly IHubContext<AdminHub> _mainHub = mainHub;

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert([FromBody] ChiTietCaThiDto chiTietCaThi)
        {
            if (chiTietCaThi.MaCaThi == null || chiTietCaThi.MaSinhVien == null || chiTietCaThi.MaDeThi == null)
                return BadRequest("Thông tin bị thiếu xót. Vui lòng kiểm tra");
            return Ok(await _chiTietCaThiService.Insert((int)chiTietCaThi.MaCaThi, (long)chiTietCaThi.MaSinhVien, (long)chiTietCaThi.MaDeThi, 0));
        }

        [HttpPost("ThemSVKhanCap")]
        public async Task<ActionResult<SinhVienDto>> ThemSVKhanCap([FromBody] SVKhanCapRequest request)
        {
            var ma_chi_tiet_ca_thi = await _chiTietCaThiService.ThemSVKhanCap(request.MaSoSinhVien, request.MaCaThi, request.MaDeThi);
            if(ma_chi_tiet_ca_thi == -1)
                return NotFound("Không tìm thấy sinh viên này trong hệ thống");
            else
                return Ok(await _chiTietCaThiService.SelectOne(ma_chi_tiet_ca_thi));
        }


        [HttpGet("SelectBy_MSSVThi")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectBy_MSSVThi([FromQuery] long ma_sinh_vien)
        {
            return Ok(await _chiTietCaThiService.SelectBy_MaSinhVienThi(ma_sinh_vien));
        }


        [HttpPut("UpdateBatDauThi")]
        public async Task<ActionResult> UpdateBatDauThi([FromBody] ChiTietCaThiRequest chiTietCaThi)
        {
            chiTietCaThi.ThoiGianBatDau = DateTime.Now;
            await _chiTietCaThiService.UpdateBatDau(chiTietCaThi.MaChiTietCaThi, chiTietCaThi.ThoiGianBatDau ?? DateTime.Now);
            await NotifSVStatusThiToAdmin(chiTietCaThi.MaChiTietCaThi, true, chiTietCaThi.ThoiGianBatDau ?? DateTime.Now);
            return Ok();
        }


        [HttpPut("UpdateKetThucThi")]
        public async Task<ActionResult> UpdateKetThucThi([FromBody] ChiTietCaThiRequest chiTietCaThi)
        {
            chiTietCaThi.ThoiGianKetThuc = DateTime.Now;
            await _chiTietCaThiService.UpdateKetThuc(chiTietCaThi.MaChiTietCaThi, chiTietCaThi.ThoiGianKetThuc ?? DateTime.Now, chiTietCaThi.Diem, chiTietCaThi.SoCauDung ?? 0, chiTietCaThi.TongSoCau ?? 0);
            await NotifSVStatusThiToAdmin(chiTietCaThi.MaChiTietCaThi, false, chiTietCaThi.ThoiGianBatDau ?? DateTime.Now);
            return Ok();
        }


        [HttpGet("SelectBy_MaCaThi_Paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiPageResult>> SelectBy_MaCaThi_Paged([FromQuery] int ma_ca_thi, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(await _chiTietCaThiService.SelectBy_MaCaThi_Paged(ma_ca_thi, pageNumber, pageSize));
        }


        [HttpGet("SelectBy_MaCaThi_Search_Paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiPageResult>> SelectBy_MaCaThi_Search_Paged([FromQuery] int ma_ca_thi, [FromQuery] string keyword, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(await _chiTietCaThiService.SelectBy_MaCaThi_Search_Paged(ma_ca_thi, keyword, pageNumber, pageSize));
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


        [HttpGet("SelectBy_MaCaThi_MSSV")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<string>>> SelectBy_MaCaThi_MSSV([FromQuery] int ma_ca_thi)
        {
            return await _chiTietCaThiService.SelectBy_ma_ca_thi_MSSV(ma_ca_thi);
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