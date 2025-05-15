using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaThiController(CaThiService caThiService, IHubContext<AdminHub> mainHub) : Controller
    {
        private readonly CaThiService _caThiService = caThiService;
        private readonly IHubContext<AdminHub> _mainHub = mainHub;

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
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_MaDotThi_MaLopAo_LanThi([FromQuery] int ma_dot_thi, [FromQuery] int ma_lop_ao, [FromQuery] int lan_thi)
        {
            return Ok(await _caThiService.SelectBy_MaDotThi_MaLop_LanThi(ma_dot_thi, ma_lop_ao, lan_thi));

        }
        [HttpGet("SelectBy_ma_chi_tiet_dot_thi")]
        public async Task<ActionResult<List<CaThiDto>>> SelectBy_ma_chi_tiet_dot_thi([FromQuery] int ma_chi_tiet_dot_thi)
        {
            return Ok(await _caThiService.SelectBy_ma_chi_tiet_dot_thi(ma_chi_tiet_dot_thi));
        }
        [HttpPut("KichHoatCaThi")]
        public async Task<ActionResult> KichHoatCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.Activate(ma_ca_thi, true);
            await NotifyChangeCaThiToAdmin(ma_ca_thi, 1);
            return Ok();
        }
        [HttpPut("HuyKichHoatCaThi")]
        public async Task<ActionResult> HuyKichHoatCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.HuyKichHoat(ma_ca_thi);
            await NotifyChangeStatusCaThiToSV(ma_ca_thi);
            await NotifyChangeCaThiToAdmin(ma_ca_thi, 1);
            return Ok();
        }
        [HttpPut("DungCaThi")]
        public async Task<ActionResult> DungCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.Activate(ma_ca_thi, false);
            await NotifyChangeStatusCaThiToSV(ma_ca_thi);
            await NotifyChangeCaThiToAdmin(ma_ca_thi, 1);
            return Ok();
        }
        [HttpPut("KetThucCaThi")]
        public async Task<ActionResult> KetThucCaThi([FromBody] int ma_ca_thi)
        {
            await _caThiService.Ketthuc(ma_ca_thi);
            await NotifyChangeStatusCaThiToSV(ma_ca_thi);
            await NotifyChangeCaThiToAdmin(ma_ca_thi, 1);
            return Ok();
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> Insert([FromBody] CustomCaThi caThi)
        {
            var id = await _caThiService.Insert(caThi.TenCaThi ?? "", caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi);
            await NotifyChangeCaThiToAdmin(id, 0);
            return Ok(id);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] CustomCaThi caThi)
        {
            if (caThi.TenCaThi != null)
                await _caThiService.Update(caThi.MaCaThi, caThi.TenCaThi, caThi.MaChiTietDotThi, caThi.ThoiGianBatDau, caThi.MaDeThi, caThi.ThoiGianThi);
            await NotifyChangeCaThiToAdmin(caThi.MaCaThi, 1);
            return Ok();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int ma_ca_thi)
        {
            await _caThiService.Remove(ma_ca_thi);
            await NotifyChangeCaThiToAdmin(ma_ca_thi, 2);
            return Ok();
        }
        [HttpPost("GenerateExcelFile")]
        public async Task<ActionResult<byte[]>> GenerateExcelFile([FromBody] List<ChiTietCaThiDto> chiTietCaThis)
        {
            // Cấp phép cho EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Tạo worksheet
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Thêm dữ liệu
                worksheet.Cells[1, 1].Value = "ISTT";
                worksheet.Cells[1, 2].Value = "MSSV";
                worksheet.Cells[1, 3].Value = "HoVaTenLot";
                worksheet.Cells[1, 4].Value = "TenSinhVien";
                worksheet.Cells[1, 5].Value = "Diem";

                if (chiTietCaThis != null)
                {
                    int rowIndex = 2; // Bắt đầu từ hàng thứ 2 (dòng dữ liệu)
                    foreach (var item in chiTietCaThis)
                    {
                        SinhVienDto? sv = item.MaSinhVienNavigation;
                        if (sv != null)
                        {
                            worksheet.Cells[rowIndex, 1].Value = rowIndex - 1; // Số thứ tự
                            worksheet.Cells[rowIndex, 2].Value = sv.MaSoSinhVien;
                            worksheet.Cells[rowIndex, 3].Value = sv.HoVaTenLot;
                            worksheet.Cells[rowIndex, 4].Value = sv.TenSinhVien;
                            worksheet.Cells[rowIndex, 5].Value = item.Diem;
                            rowIndex++;
                        }
                    }
                }

                // Tự động điều chỉnh cột
                worksheet.Cells.AutoFitColumns();

                // Trả về dữ liệu Excel dưới dạng mảng byte
                return await Task.FromResult(package.GetAsByteArray());
            }
        }



        private async Task NotifyChangeStatusCaThiToSV(int ma_ca_thi)
        {
            await _mainHub.Clients.Group(ma_ca_thi + "").SendAsync("ChangeStatusCaThi");
        }
        // các admin khác cũng nhận được sự thay đổi của TT ca thi
        private async Task NotifyChangeCaThiToAdmin(int ma_ca_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCaThi" : (function == 1) ? "UpdateCaThi" : "DeleteCaThi";
            await _mainHub.Clients.Group("admin").SendAsync(message, ma_ca_thi);
        }
    }
}
