using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Transactions;

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
        private readonly IHubContext<MainHub> _mainHub;
        public CaThiController(CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, ChiTietCaThiService chiTietCaThiService, 
            ChiTietBaiThiService chiTietBaiThiService, IHubContext<MainHub> mainHub)
        {
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _chiTietCaThiService = chiTietCaThiService;
            _chiTietBaiThiService = chiTietBaiThiService;
            _mainHub = mainHub;
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
            await NotifyChangeStatusCaThiToSV(ma_ca_thi);
            await NotifyChangeStatusCaThiToAdmin();
            return Ok();
        }
        [HttpPut("HuyKichHoatCaThi")]
        public async Task<ActionResult> HuyKichHoatCaThi([FromBody] int ma_ca_thi)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    List<ChiTietCaThiDto> chiTietCaThis = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
                    foreach (var item in chiTietCaThis)
                    {
                        if (!item.DaHoanThanh)
                        {
                            item.DaHoanThanh = true;
                            item.ThoiGianKetThuc = DateTime.Now;
                            await _chiTietCaThiService.UpdateKetThuc(item);
                        }

                        List<ChiTietBaiThiDto> chiTietBaiThis = await _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(item.MaChiTietCaThi);
                        await RemoveListCTBT(chiTietBaiThis);
                    }
                    await DungCaThi(ma_ca_thi);
                    await NotifyChangeStatusCaThiToSV(ma_ca_thi);
                    await NotifyChangeStatusCaThiToAdmin();

                    // Commit tất cả thay đổi nếu không có lỗi
                    transaction.Complete();
                    return Ok();
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, transaction sẽ rollback tự động khi Dispose()
                    return BadRequest("Có lỗi xảy ra khi hủy kích hoạt ca thi." + ex.Message );
                }
            }
        }
        [HttpPut("DungCaThi")]
        public async Task<ActionResult> DungCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.ca_thi_Activate(ma_ca_thi, false);
            await NotifyChangeStatusCaThiToSV(ma_ca_thi);
            await NotifyChangeStatusCaThiToAdmin();
            return Ok();
        }
        [HttpPut("KetThucCaThi")]
        public async Task<ActionResult<bool>> KetThucCaThi([FromBody] int ma_ca_thi)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    List<ChiTietCaThiDto> chiTietCaThis = await _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
                    foreach (var item in chiTietCaThis)
                    {
                        if (!item.DaHoanThanh)
                        {
                            // Nếu có thí sinh chưa hoàn thành bài, rollback và return false
                            return Ok(false);
                        }
                    }

                    await _caThiService.ca_thi_Ketthuc(ma_ca_thi);
                    await NotifyChangeStatusCaThiToSV(ma_ca_thi);
                    await NotifyChangeStatusCaThiToAdmin();

                    // Xác nhận tất cả thao tác hợp lệ
                    transaction.Complete();
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, transaction sẽ rollback tự động
                    return BadRequest("Lỗi khi hủy kích hoạt ca thi." + ex.Message );
                }
            }
        }




        private async Task<ChiTietDotThiDto> GetCTDT_SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, string lan_thi)
        {
            return await _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo_LanThi(ma_dot_thi, ma_lop_ao, lan_thi);
        }
        private async Task RemoveListCTBT(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            foreach (var item in chiTietBaiThis)
                await _chiTietBaiThiService.Delete(item.MaChiTietBaiThi);
        }
        private async Task NotifyChangeStatusCaThiToSV(int ma_lop)
        {
            await _mainHub.Clients.Group(ma_lop + "").SendAsync("ChangeStatusCaThi", ma_lop);
        }
        // các admin khác cũng nhận được sự thay đổi của TT ca thi
        private async Task NotifyChangeStatusCaThiToAdmin()
        {
            await _mainHub.Clients.Group("admin").SendAsync("ChangeStatusCaThi");
        }
    }
}
