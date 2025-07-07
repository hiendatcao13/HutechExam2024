using System.Data.SqlClient;
using System.Drawing;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.API.Response;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Syncfusion.PdfExport;
using System.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

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
            // Cấp phép cho EPPlus
            ExcelPackage.License.SetNonCommercialPersonal("Pino Dat");

            using (var package = new ExcelPackage())
            {
                // Tạo worksheet
                var ws = package.Workbook.Worksheets.Add("Data");

                //// Chèn Logo HUTECH
                //// Load ảnh bằng ImageSharp
                //var imagePath = "./images/exam/ava.jpg";
                //using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imagePath);

                //// Chuyển ảnh thành stream
                //using var ms = new MemoryStream();
                //image.SaveAsPng(ms); // có thể dùng SaveAsJpeg nếu là jpg
                //ms.Position = 0;

                //// Chèn vào Excel
                //var picture = ws.Drawings.AddPicture("LogoHutech", ms);
                //picture.SetPosition(0, 0, 0, 0); // Dòng 1, cột A

                // Merge Tiêu đề Viện Hợp Tác
                ws.Cells["C1:H1"].Merge = true;
                ws.Cells["C1"].Value = "VIỆN HỢP TÁC VÀ PHÁT TRIỂN ĐÀO TẠO";
                ws.Cells["C1"].Style.Font.Bold = true;
                ws.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells["C2:H2"].Merge = true;
                ws.Cells["C2"].Value = "(Đề thi có 9 trang)";
                ws.Cells["C2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Tiêu đề ĐỀ THI
                ws.Cells["D4:H4"].Merge = true;
                ws.Cells["D4"].Value = "ĐỀ THI KẾT THÚC HỌC PHẦN";
                ws.Cells["D4"].Style.Font.Bold = true;
                ws.Cells["D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells["D5:H5"].Merge = true;
                ws.Cells["D5"].Value = "HỌC KỲ II NĂM HỌC 2024 - 2025";
                ws.Cells["D5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Thông tin chi tiết
                ws.Cells["A7"].Value = "Ngành/Lớp:";
                ws.Cells["A8"].Value = "Tên học phần:";
                ws.Cells["A9"].Value = "Mã học phần:";
                ws.Cells["A10"].Value = "Ngày thi:";
                ws.Cells["A11"].Value = "Thời gian làm bài:";
                ws.Cells["A12"].Value = "Mã đề:";

                ws.Cells["D7"].Value = "24TXTHB1 + 24TXTHC1";
                ws.Cells["D8"].Value = "Lập trình Hướng đối tượng";
                ws.Cells["D9"].Value = "ECMP167… Số TC: 3";
                ws.Cells["D11"].Value = "60 phút";

                // Checkbox sử dụng tài liệu
                ws.Cells["A13"].Value = "SỬ DỤNG TÀI LIỆU:";
                ws.Cells["C13"].Value = "CÓ □";
                ws.Cells["E13"].Value = "KHÔNG ☑";

                // Định dạng các ô
                ws.Cells["A1:H15"].Style.Font.Name = "Times New Roman";
                ws.Cells["A1:H15"].Style.Font.Size = 11;

                // Thêm dữ liệu
                ws.Cells[17, 1].Value = "STT";
                ws.Cells[17, 2].Value = "MSSV";
                ws.Cells[17, 3].Value = "HoVaTenLot";
                ws.Cells[17, 4].Value = "TenSinhVien";
                ws.Cells[17, 5].Value = "Diem";

                if (chiTietCaThis != null)
                {
                    int rowIndex = 18; // Bắt đầu từ hàng thứ 18 (dòng dữ liệu)
                    foreach (var item in chiTietCaThis)
                    {
                        SinhVienDto? sv = item.MaSinhVienNavigation;
                        if (sv != null)
                        {
                            ws.Cells[rowIndex, 1].Value = rowIndex - 1; // Số thứ tự
                            ws.Cells[rowIndex, 2].Value = sv.MaSoSinhVien;
                            ws.Cells[rowIndex, 3].Value = sv.HoVaTenLot;
                            ws.Cells[rowIndex, 4].Value = sv.TenSinhVien;
                            ws.Cells[rowIndex, 5].Value = item.Diem;
                            rowIndex++;
                        }
                    }
                }

                // Tự động điều chỉnh cột
                ws.Cells.AutoFitColumns();

                // Trả về dữ liệu Excel dưới dạng mảng byte
                var result = await Task.FromResult(package.GetAsByteArray());
                return Ok(APIResponse<byte[]>.SuccessResponse(data: result, message: "Xử lí file chi tiết ca thi thành công"));
            }
        }

        [HttpPost("export-pdf")]
        [Authorize(Roles = "QuanTri")]
        public Task<IActionResult> ExportToPdf([FromBody] List<ChiTietCaThiDto> data)
        {
            using var document = new PdfDocument();
            var page = document.Pages.Add();

            // Đọc font hỗ trợ Unicode
            var fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            using var fontStream = new FileStream(fontPath, FileMode.Open, FileAccess.Read);
            var unicodeFont = new PdfTrueTypeFont(fontStream, 12);
            var titleFont = new PdfTrueTypeFont(fontStream, 16, PdfFontStyle.Bold); // font to và đậm hơn

            // Vẽ tiêu đề lên đầu trang
            string title = "BẢNG ĐIỂM THÍ SINH";
            // Tạo brush màu đen
            PdfBrush blackBrush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            page.Graphics.DrawString(title, titleFont, blackBrush, new System.Drawing.PointF(180, 20));

            // Tạo PdfGrid và định nghĩa số cột
            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Columns.Add(4);

            // Thêm header
            pdfGrid.Headers.Add(1);
            var headerRow = pdfGrid.Headers[0];
            headerRow.Cells[0].Value = "MSSV";
            headerRow.Cells[1].Value = "Họ lót";
            headerRow.Cells[2].Value = "Tên";
            headerRow.Cells[3].Value = "Điểm";

            // Thêm từng hàng dữ liệu
            foreach (var item in data)
            {
                var sv = item.MaSinhVienNavigation;
                var row = pdfGrid.Rows.Add();
                row.Cells[0].Value = sv?.MaSoSinhVien ?? "";
                row.Cells[1].Value = sv?.HoVaTenLot ?? "";
                row.Cells[2].Value = sv?.TenSinhVien ?? "";
                row.Cells[3].Value = item.Diem.ToString();
            }

            // Áp dụng font cho bảng
            pdfGrid.Style.Font = unicodeFont;
            pdfGrid.Headers[0].Style.Font = unicodeFont;

            // Padding cho nội dung bảng
            pdfGrid.Style.CellPadding = new PdfPaddings(5, 4, 5, 4);
            headerRow.Style.BackgroundBrush = new PdfSolidBrush(new PdfColor(173, 216, 230));

            // Vẽ bảng cách tiêu đề khoảng 50px
            pdfGrid.Draw(page, new System.Drawing.PointF(0, 60));

            using var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            var bytes = stream.ToArray();

            return Task.FromResult<IActionResult>(Ok(APIResponse<byte[]>.SuccessResponse(data: bytes, message: "Xử lí file PDF chi tiết ca thi thành công")));
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
            await _adminHub.Clients.Group("admin").SendAsync("ChangeCTCaThi_SVThi", ma_chi_tiet_ca_thi, isBDThi, thoi_gian);
        }

        #endregion

    }
}