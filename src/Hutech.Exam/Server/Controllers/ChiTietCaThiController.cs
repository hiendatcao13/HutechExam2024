using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Spire.Xls;
using Syncfusion.PdfExport;
using System.Data.SqlClient;
using System.Drawing;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/chitietcathis")]
    [ApiController]
    [Authorize]
    public class ChiTietCaThiController(ChiTietCaThiService chiTietCaThiService, IHubContext<AdminHub> adminHub) : Controller
    {
        #region Private Fields

        private readonly ChiTietCaThiService _chiTietCaThiService = chiTietCaThiService;
        private readonly IHubContext<AdminHub> _adminHub = adminHub;

        private const string NotFoundMessage = "Không tìm thấy chi tiết ca thi";

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectOne([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.SelectOne(id);
            if (result.MaChiTietCaThi == 0)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết ca thi"));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: result, message: "Lấy chi tiết ca thi thành công"));
        }

        [HttpGet("filter-by-sinhvien")]
        [Authorize(Roles = "SinhVien")]
        public async Task<IActionResult> SelectBy_MSSVThi([FromQuery] long maSinhVien)
        {
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaSinhVienThi(maSinhVien), message: "Lấy chi tiết ca thi thành công"));
        }

        [HttpGet("filter-by-cathi-paged")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectBy_MaCaThi_Paged([FromQuery] int maCaThi, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(APIResponse<Paged<ChiTietCaThiDto>>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaCaThi_Paged(maCaThi, pageNumber, pageSize), message: "Lấy chi tiết ca thi thành công"));
        }


        [HttpGet("filter-by-cathi-search-paged")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> SelectBy_MaCaThi_Search_Paged([FromQuery] int maCaThi, [FromQuery] string keyword, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(APIResponse<Paged<ChiTietCaThiDto>>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaCaThi_Search_Paged(maCaThi, keyword, pageNumber, pageSize), message: "Lấy chi tiết ca thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> Insert([FromBody] ChiTietCaThiCreateRequest chiTietCaThi)
        {
            var id = await _chiTietCaThiService.Insert(chiTietCaThi);
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Thêm chi tiết ca thi thành công"));
        }

        [HttpPost("batch")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> InsertBatch([FromBody] List<ChiTietCaThiCreateBatchRequest> chiTietCaThis)
        {
            await _chiTietCaThiService.Insert_Batch(chiTietCaThis);
            return Ok(APIResponse<List<ChiTietCaThiDto>>.SuccessResponse(message: "Thêm danh sách chi tiết ca thi thành công"));
        }

        [HttpPost("export-excel")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> GenerateExcelFile([FromBody] List<ChiTietCaThiDto> chiTietCaThis)
        {
                var result = await ConvertToByteFile(chiTietCaThis);
                return Ok(APIResponse<byte[]>.SuccessResponse(data: result, message: "Xử lí file chi tiết ca thi thành công"));
        }

        [HttpPost("export-pdf")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> ExportToPdf([FromBody] List<ChiTietCaThiDto> chiTietCaThis)
        {
            var result = await ConvertExcelBytesToPdf(chiTietCaThis);
            return Ok(APIResponse<byte[]>.SuccessResponse(data: result, message: "Xử lí file chi tiết ca thi thành công"));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChiTietCaThiUpdateRequest chiTietCaThi)
        {
            var result = await _chiTietCaThiService.Update(id, chiTietCaThi);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Cập nhật chi tiết ca thi thành công"));
        }

        #endregion

        #region Patch Methods

        [HttpPatch("{id:int}/cong-gio")]
        [Authorize(Roles = "QuanTri")]
        public async Task<ActionResult<ChiTietCaThiDto>> CongGioSinhVien([FromRoute] int id, [FromQuery] int gioCongThem)
        {
            var result = await _chiTietCaThiService.CongGio(id, gioCongThem);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Cộng giờ cho thí sinh thành công"));

        }

        [HttpPatch("{id:int}/bat-dau-thi")]//------------------API cho thí sinh----------------------
        public async Task<ActionResult> UpdateBatDauThi([FromRoute] int id)
        {
            await _chiTietCaThiService.UpdateBatDau(id, DateTime.Now);
            await NotifSVStatusThiToAdmin(id, true, DateTime.Now);
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(message: "Cập nhật trạng thái bắt đầu thi cho thí sinh thành công"));
        }

        #endregion

        #region Delete Methods

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.Remove(id);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(message: "Xóa chi tiết ca thi thành công"));
        }

        [HttpDelete("{id:int}/force")]
        [Authorize(Roles = "QuanTri")]
        public async Task<IActionResult> ForceDelete([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.ForceRemove(id);
            if (!result)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: NotFoundMessage));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(message: "Xóa chi tiết ca thi thành công"));
        }


        #endregion

        #region Private Methods

        private async Task NotifSVStatusThiToAdmin(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian)
        {
            // 0: bắt đầu thi, 1: kết thúc thi
            await _adminHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian, -1);
        }

        private async Task<byte[]> ConvertToByteFile(List<ChiTietCaThiDto> chiTietCaThis)
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
                    using var image = Image.Load<Rgba32>(imagePath); // ImageSharp
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
                ws.Cells["F3"].Value = "NĂM HỌC 2024 - 2025";
                ws.Cells["F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Thông tin học phần
                ws.Cells["F4:G4"].Merge = true;
                ws.Cells["F4"].Value = " Ngành";
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
                ws.Cells["H4"].Value = ": Công nghệ phần mềm";
                ws.Cells["H4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H5:K5"].Merge = true;
                ws.Cells["H5"].Value = ": Lập trình hướng đối tượng";
                ws.Cells["H5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H6:K6"].Merge = true;
                ws.Cells["H6"].Value = ": CMP12";
                ws.Cells["H6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H7:K7"].Merge = true;
                ws.Cells["H7"].Value = ": 07/07/2025";
                ws.Cells["H7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                ws.Cells["H8:K8"].Merge = true;
                ws.Cells["H8"].Value = ": 90 phút";
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
                foreach (var item in chiTietCaThis)
                {
                    var sv = item.MaSinhVienNavigation;
                    if (sv != null)
                    {
                        ws.Cells[rowIndex, 2].Value = stt++; // STT
                        ws.Cells[rowIndex, 3].Value = sv.MaSoSinhVien;
                        ws.Cells[rowIndex, 4].Value = sv.HoVaTenLot;
                        ws.Cells[rowIndex, 5].Value = sv.TenSinhVien;
                        ws.Cells[rowIndex, 6].Value = sv.NgaySinh?.ToString("dd/MM/yyyy");
                        ws.Cells[rowIndex, 7].Value = sv.MaLop;
                        ws.Cells[rowIndex, 8].Value = sv.DienThoai;
                        ws.Cells[rowIndex, 9].Value = sv.Email;
                        ws.Cells[rowIndex, 10].Value = item.KyHieuDe;
                        ws.Cells[rowIndex, 11].Value = item.Diem;
                        rowIndex++;
                    }
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

        public async Task<byte[]> ConvertExcelBytesToPdf(List<ChiTietCaThiDto> chiTietCaThis)
        {
            var excelBytes = await ConvertToByteFile(chiTietCaThis);
            // Bước 1: Lưu byte[] thành file Excel tạm
            var tempExcelPath = Path.Combine(Path.GetTempPath(), $"temp_{Guid.NewGuid()}.xlsx");
            System.IO.File.WriteAllBytes(tempExcelPath, excelBytes);

            // Bước 2: Load bằng Spire.XLS
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(tempExcelPath);
            Worksheet sheet = workbook.Worksheets[0];

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