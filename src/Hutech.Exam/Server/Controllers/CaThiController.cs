using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Audit;
using Hutech.Exam.Shared.DTO.Request.CaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Spire.Xls;
using Syncfusion.ExcelExport;
using System.Data.SqlClient;
using static MudBlazor.Icons.Custom;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/cathis")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class CaThiController(CaThiService caThiService, BcryptService bcryptService, IHubContext<AdminHub> adminHub, IHubContext<SinhVienHub> sinhVienHub) : Controller
    {
        #region Private Fields
        private readonly CaThiService _caThiService = caThiService;
        private readonly BcryptService _bcryptService = bcryptService;

        private readonly IHubContext<AdminHub> _adminHub = adminHub;
        private readonly IHubContext<SinhVienHub> _sinhVienHub = sinhVienHub;

        private const string NotFoundMessage = "Không tìm thấy ca thi";
        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _caThiService.SelectOne(id);

            if (result.MaCaThi == 0)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: "Không tìm thấy ca thi"));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(result, "Lấy ca thi thành công"));
        }

        [HttpGet("{id:int}/is-active")]
        [Authorize] //----------------Đánh dấu thí sinh có thể truy cập
        public async Task<IActionResult> IsActiveCaThi([FromRoute] int id)
        {
            CaThiDto caThi = await _caThiService.SelectOne(id);
            return Ok(APIResponse<bool>.SuccessResponse(data: caThi.IsActivated, message: "Lấy thông tin ca thi thành công"));
        }

        [HttpGet("filter-by-dotthi-lopao-lanthi")]
        public async Task<IActionResult> SelectBy_MaDotThi_MaLopAo_LanThi([FromQuery] int maDotThi, [FromQuery] int maLopAo, [FromQuery] int lanThi)
        {
            var result = await _caThiService.SelectBy_MaDotThi_MaLop_LanThi(maDotThi, maLopAo, lanThi);
            return Ok(APIResponse<List<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));

        }

        [HttpGet("filter-by-chitietdotthi-paged")]
        public async Task<IActionResult> SelectBy_ma_chi_tiet_dot_thi_Paged([FromQuery] int maChiTietDotThi, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi_Paged(maChiTietDotThi, pageNumber, pageSize);
            return Ok(APIResponse<Paged<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        [HttpGet("filter-by-chitietdotthi-search-paged")]
        public async Task<IActionResult> SelectBy_ma_chi_tiet_dot_thi_Search_Paged([FromQuery] int maChiTietDotThi, [FromQuery] string keyword, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _caThiService.SelectBy_ma_chi_tiet_dot_thi_Search_Paged(maChiTietDotThi, keyword, pageNumber, pageSize);
            return Ok(APIResponse<Paged<CaThiDto>>.SuccessResponse(data: result, message: "Lấy danh sách ca thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Insert([FromBody] CaThiCreateRequest caThi)
        {
            var id = await _caThiService.Insert(caThi);
            await NotifyChangeCaThiToAdmin(id, 0);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), "Thêm ca thi thành công"));
        }

        [HttpPost("{id:int}/verify")]
        public async Task<IActionResult> VerifyPassword([FromRoute] int id, [FromBody] string password)
        {
            var examSession = await _caThiService.SelectOne(id);
            var hashPassword = examSession.MatMa;
            if (string.IsNullOrWhiteSpace(hashPassword) || _bcryptService.VerifyPassword(password, hashPassword) == true)
            {
                return Ok(APIResponse<bool>.SuccessResponse(data: true, message: "Xác thực thành công"));
            }
            else
            {
                return BadRequest(APIResponse<bool>.ErrorResponse(message: "Xác không thành công. Vui lòng liên hệ quản trị viên"));
            }
        }

        [HttpPost("{id:int}/export-excel")]
        public async Task<IActionResult> GenerateExcelFile([FromRoute] int id, [FromBody] CaThiExportFileRequest request)
        {
            var result = await ConvertToByteFile(request);
            return Ok(APIResponse<byte[]>.SuccessResponse(data: result, message: "Xử lí file chi tiết ca thi thành công"));
        }

        [HttpPost("{id:int}/export-pdf")]
        public async Task<IActionResult> ExportToPdf([FromRoute] int id, [FromBody] CaThiExportFileRequest request)
        {
            var result = await ConvertExcelBytesToPdf(request);
            return Ok(APIResponse<byte[]>.SuccessResponse(data: result, message: "Xử lí file chi tiết ca thi thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CaThiUpdateRequest caThi)
        {
            var result = await _caThiService.Update(id, caThi);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật ca thi thành công"));
        }

        #endregion

        #region Patch Methods

        [HttpPatch("{id:int}/active")]
        public async Task<IActionResult> KichHoatCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.Activate(id, true, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Kích hoạt ca thi thành công"));

        }

        [HttpPatch("{id:int}/deactive")]
        public async Task<IActionResult> HuyKichHoatCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.HuyKichHoat(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Hủy kích hoạt ca thi thành công"));
        }

        [HttpPatch("{id:int}/pause")]
        public async Task<IActionResult> DungCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.Activate(id, false, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Dừng ca thi thành công"));
        }

        [HttpPatch("{id:int}/finish")]
        public async Task<IActionResult> KetThucCaThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.Ketthuc(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeStatusCaThiToSV(id);
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Kết thúc ca thi thành công"));
        }

        [HttpPatch("{id:int}/update-dethi")]
        [Authorize(Roles = "KhaoThi,Admin")]
        public async Task<IActionResult> UpdateDeThi([FromRoute] int id, [FromBody] CaThiUpdateDeThiRequest request)
        {
            var result = await _caThiService.UpdateDeThi(id, request);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật ca thi thành công"));
        }

        [HttpPatch("{id:int}/update-audit")]
        public async Task<IActionResult> UpdateAudit([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.UpdateLichSuHoatDong(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Cập nhật lịch sử hoạt động ca thi thành công"));
        }

        [HttpPatch("{id:int}/duyet-de")]
        [Authorize(Roles = "CNTT,Admin")]
        public async Task<IActionResult> DuyetDeThi([FromRoute] int id, [FromBody] string lichSuHoatDong)
        {
            var result = await _caThiService.DuyetDe(id, lichSuHoatDong);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 1);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(data: await _caThiService.SelectOne(id), message: "Duyệt đề thi thành công"));
        }

        [HttpPatch("{id:int}/all-reset-login")]
        public async Task<IActionResult> ResetAllLogin([FromRoute] int id)
        {
            var result = await _caThiService.UpdateAllResetLogin(id);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Reset đăng nhập toàn bộ thí sinh thành công"));
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id}")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _caThiService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 2);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Xóa ca thi thành công"));
        }

        [HttpDelete("{id}/force")]
        [Authorize(Roles = "DaoTao,Admin")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _caThiService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<CaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            await NotifyChangeCaThiToAdmin(id, 2);
            return Ok(APIResponse<CaThiDto>.SuccessResponse(message: "Xóa ca thi thành công"));
        }


        #endregion

        #region Private Methods
        private async Task NotifyChangeStatusCaThiToSV(int ma_ca_thi)
        {
            await _sinhVienHub.Clients.Group(ma_ca_thi + "").SendAsync("ChangeStatusCaThi");
        }

        // các admin khác cũng nhận được sự thay đổi của TT ca thi
        private async Task NotifyChangeCaThiToAdmin(int ma_ca_thi, int function)
        {
            // 0: Insert, 1: Update, 2:Delete
            string message = (function == 0) ? "InsertCaThi" : (function == 1) ? "UpdateCaThi" : "DeleteCaThi";
            await _adminHub.Clients.Group("admin").SendAsync(message, ma_ca_thi);
        }

        private async Task<byte[]> ConvertToByteFile(CaThiExportFileRequest request)
        {
            // Cấp phép cho EPPlus
            ExcelPackage.License.SetNonCommercialPersonal("Pino Dat");

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("DeThi");
                //Đường dẫn đến ảnh logo(có thể là đường dẫn tương đối trong wwwroot)
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Images\\Logo.png");

                // Kiểm tra ảnh tồn tại
                if (System.IO.File.Exists(imagePath))
                {
                    using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imagePath); // ImageSharp
                    using var ms = new MemoryStream();
                    image.SaveAsPng(ms);
                    ms.Position = 0;

                    var picture = ws.Drawings.AddPicture("LogoHutech", ms);
                    picture.SetPosition(1, 1, 2, 0); // Dòng 1, cột B
                    picture.SetSize(110); // Kích thước hợp lý
                }


                // Font toàn trang
                ws.Cells.Style.Font.Name = "Times New Roman";
                ws.Cells.Style.Font.Size = 15;

                // Chèn tiêu đề trái (Logo / Viện)
                ws.Cells["C5:D5"].Merge = true;
                ws.Cells["C5"].Value = "VIỆN HỢP TÁC";
                ws.Cells["C5"].Style.Font.Bold = true;
                ws.Cells["C5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells["B6:E6"].Merge = true;
                ws.Cells["B6"].Value = "VÀ PHÁT TRIỂN ĐÀO TẠO";
                ws.Cells["B6"].Style.Font.Bold = true;
                ws.Cells["B6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Chèn tiêu đề phải
                ws.Cells["F2:K2"].Merge = true;
                ws.Cells["F2"].Value = "ĐỀ THI KẾT THÚC HỌC PHẦN";
                ws.Cells["F2"].Style.Font.Bold = true;
                ws.Cells["F2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells["F3:K3"].Merge = true;
                ws.Cells["F3"].Value = $"NĂM HỌC {request.DotThi.NamHoc} - {request.DotThi.NamHoc + 1}";
                ws.Cells["F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Thông tin học phần
                ws.Cells["F4:G4"].Merge = true;
                ws.Cells["F4"].Value = " Ca thi";
                ws.Cells["F4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["F5:G5"].Merge = true;
                ws.Cells["F5"].Value = " Tên học phần";
                ws.Cells["F5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["F6:G6"].Merge = true;
                ws.Cells["F6"].Value = " Mã học phần";
                ws.Cells["F6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["F7:G7"].Merge = true;
                ws.Cells["F7"].Value = " Ngày thi";
                ws.Cells["F7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["F8:G8"].Merge = true;
                ws.Cells["F8"].Value = " Thời gian làm bài";
                ws.Cells["F8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H4:K4"].Merge = true;
                ws.Cells["H4"].Value = $": {request.CaThi.TenCaThi}";
                ws.Cells["H4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H5:K5"].Merge = true;
                ws.Cells["H5"].Value = $": {request.MonHoc.TenMonHoc}";
                ws.Cells["H5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H6:K6"].Merge = true;
                ws.Cells["H6"].Value = $": {request.MonHoc.MaSoMonHoc}";
                ws.Cells["H6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H7:K7"].Merge = true;
                ws.Cells["H7"].Value = $": {request.CaThi.ThoiGianBatDau.ToString("dd/MM/yyyy")}";
                ws.Cells["H7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H8:K8"].Merge = true;
                ws.Cells["H8"].Value = $": {request.CaThi.ThoiGianThi}";
                ws.Cells["H8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;


                // Dòng tiêu đề bảng dữ liệu
                string[] headers = { "STT", "MaSV", "HoLotSV", "TenSV", "Ngày sinh", "Mã lớp", "Điện thoại", "Email", "Mã đề", "Điểm" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[11, i + 2].Value = headers[i];
                    ws.Cells[11, i + 2].Style.Font.Bold = true;
                    ws.Cells[11, i + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    ws.Cells[11, i + 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                // Dữ liệu sinh viên
                int rowIndex = 12;
                int stt = 1;
                foreach (var item in request.ChiTietCaThis)
                {
                    var sv = item.MaSinhVienNavigation;
                    if (sv != null)
                    {
                        ws.Cells[rowIndex, 2].Value = stt++; // STT
                        ws.Cells[rowIndex, 3].Value = sv.MaSoSinhVien;
                        ws.Cells[rowIndex, 4].Value = sv.HoVaTenLot;
                        ws.Cells[rowIndex, 5].Value = sv.TenSinhVien;
                        ws.Cells[rowIndex, 6].Value = sv.NgaySinh?.ToString("dd/MM/yyyy");
                        ws.Cells[rowIndex, 7].Value = (sv.GioiTinh == 1) ? "Nam" : "Nữ";
                        ws.Cells[rowIndex, 8].Value = sv.DienThoai;
                        ws.Cells[rowIndex, 9].Value = sv.Email;
                        ws.Cells[rowIndex, 10].Value = item.KyHieuDe;
                        ws.Cells[rowIndex, 11].Value = (item.Diem == -1) ? "-" : item.Diem;
                        rowIndex++;
                    }

                    ws.Cells[12, 2, rowIndex, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                // Căn chỉnh và kẻ bảng
                ws.Cells[12, 2, rowIndex - 1, 11].AutoFitColumns();
                ws.Cells[2, 2, 8, 5].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                ws.Cells[2, 6, 8, 11].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                ws.Cells[12, 2, rowIndex - 1, 11].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                var range = ws.Cells[12, 2, rowIndex - 1, 11];

                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                // Tự động điều chỉnh cột
                ws.Cells.AutoFitColumns();


                // Trả về dữ liệu Excel dưới dạng mảng byte
                return await Task.FromResult(package.GetAsByteArray());
            }
        }

        private async Task<byte[]> ConvertExcelBytesToPdf(CaThiExportFileRequest request)
        {
            var excelBytes = await ConvertToByteFile(request);
            // Bước 1: Lưu byte[] thành file Excel tạm
            var tempExcelPath = Path.Combine(Path.GetTempPath(), $"temp_{Guid.NewGuid()}.xlsx");
            System.IO.File.WriteAllBytes(tempExcelPath, excelBytes);

            // Bước 2: Load bằng Spire.XLS
            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
            workbook.LoadFromFile(tempExcelPath);
            Spire.Xls.Worksheet sheet = workbook.Worksheets[0];

            // FREESTYLE PDF STRUCTURE — CHÈN VÀO ĐÂY
            sheet.PageSetup.FitToPagesWide = 0;
            sheet.PageSetup.FitToPagesTall = 0;
            sheet.PageSetup.Zoom = 100;

            sheet.PageSetup.PaperSize = PaperSizeType.PaperA3;

            sheet.PageSetup.TopMargin = 0;
            sheet.PageSetup.BottomMargin = 0;
            sheet.PageSetup.LeftMargin = 0;
            sheet.PageSetup.RightMargin = 0;

            sheet.PageSetup.CenterHorizontally = false;
            sheet.PageSetup.CenterVertically = false;
            sheet.PageSetup.Orientation = PageOrientationType.Landscape;


            // Bước 3: Lưu thành file PDF tạm
            var tempPdfPath = Path.ChangeExtension(tempExcelPath, ".pdf");
            workbook.SaveToFile(tempPdfPath, FileFormat.PDF);

            // Bước 4: Đọc lại file PDF trả về byte[]
            var pdfBytes = System.IO.File.ReadAllBytes(tempPdfPath);

            // Dọn dẹp file tạm
            System.IO.File.Delete(tempExcelPath);
            System.IO.File.Delete(tempPdfPath);

            return pdfBytes;
        }
        #endregion

    }
}
