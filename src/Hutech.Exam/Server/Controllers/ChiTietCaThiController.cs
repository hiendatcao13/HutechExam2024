using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietcathis")]
    [ApiController]
    [Authorize]
    public class ChiTietCaThiController(ChiTietCaThiService chiTietCaThiService, HashIdHelper hashIdHelper, IHubContext<AdminHub> adminHub) : Controller
    {
        private readonly ChiTietCaThiService _chiTietCaThiService = chiTietCaThiService;
        private readonly HashIdHelper _hashIdHelper = hashIdHelper;

        private readonly IHubContext<AdminHub> _adminHub = adminHub;

        //////////////////CRUD///////////////////////////

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectOne([FromRoute] int id)
        {
            return Ok(await _chiTietCaThiService.SelectOne(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> Insert([FromBody] ChiTietCaThiCreateRequest chiTietCaThi)
        {
            var ma_chi_tiet_ca_thi = await _chiTietCaThiService.Insert(chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, -1);
            return Ok(await _chiTietCaThiService.SelectOne(ma_chi_tiet_ca_thi));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> Update([FromRoute] int id, [FromBody] ChiTietCaThiUpdateRequest chiTietCaThi)
        {
            var ma_chi_tiet_ca_thi = await _chiTietCaThiService.Update(id, chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, -1);
            return Ok(await _chiTietCaThiService.SelectOne(ma_chi_tiet_ca_thi));
        }

        [HttpPut("{id}/cong-gio")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CongGioSinhVien([FromRoute] int id, [FromBody] ChiTietCaThiUpdateCongGioRequest chiTietCaThi)
        {
            await _chiTietCaThiService.CongGio(id, chiTietCaThi.GioCongThem, chiTietCaThi.ThoiDiemCong, chiTietCaThi.LyDoCong);
            // báo cho tất cả admin
            await NotifyCongGioSVToAdmin(id);
            return Ok(true);
        }

        [HttpPut("{id}/bat-dau-thi")]
        public async Task<ActionResult> UpdateBatDauThi([FromRoute] int id)
        {
            await _chiTietCaThiService.UpdateBatDau(id, DateTime.Now);
            await NotifSVStatusThiToAdmin(id, true, DateTime.Now);
            return Ok();
        }


        [HttpPut("{id}/ket-thuc-thi")]
        public async Task<ActionResult> UpdateKetThucThi([FromRoute] int id, [FromBody] ChiTietCaThiUpdateKTThiRequest chiTietCaThi)
        {
            await _chiTietCaThiService.UpdateKetThuc(id, chiTietCaThi.ThoiGianKetThuc, chiTietCaThi.Diem, chiTietCaThi.SoCauDung, chiTietCaThi.TongSoCau);
            await NotifSVStatusThiToAdmin(id, false, chiTietCaThi.ThoiGianKetThuc);
            return Ok();
        }

        //////////////////FILTER///////////////////////////

        [HttpGet("filter-by-sinhvien")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectBy_MSSVThi([FromQuery] string maSinhVien)
        {
            long id = _hashIdHelper.DecodeLongId(maSinhVien);
            return Ok(await _chiTietCaThiService.SelectBy_MaSinhVienThi(id));
        }

        [HttpGet("filter-by-cathi-paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiPageResult>> SelectBy_MaCaThi_Paged([FromQuery] int maCaThi, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(await _chiTietCaThiService.SelectBy_MaCaThi_Paged(maCaThi, pageNumber, pageSize));
        }


        [HttpGet("filter-by-cathi-search-paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiPageResult>> SelectBy_MaCaThi_Search_Paged([FromQuery] int maCaThi, [FromQuery] string keywork, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(await _chiTietCaThiService.SelectBy_MaCaThi_Search_Paged(maCaThi, keywork, pageNumber, pageSize));
        }

        //////////////////OTHERS///////////////////////////

        [HttpPost("export-excel")]
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

        //////////////////PRIVATE///////////////////////////

        private async Task NotifSVStatusThiToAdmin(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian)
        {
            // 0: bắt đầu thi, 1: kết thúc thi
            await _adminHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian);
        }
        private async Task NotifyCongGioSVToAdmin(int ma_chi_tiet_ca_thi)
        {
            await _adminHub.Clients.Group("admin").SendAsync("CongGioSV", ma_chi_tiet_ca_thi);
        }
    }
}