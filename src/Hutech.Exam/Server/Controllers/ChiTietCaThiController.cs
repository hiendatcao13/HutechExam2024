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
using Syncfusion.PdfExport;

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

        #endregion

        #region Get Methods

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectOne([FromRoute] int id)
        {
            var result = await _chiTietCaThiService.SelectOne(id);
            if (result.MaChiTietCaThi == 0)
            {
                return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết ca thi"));
            }
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: result, message: "Lấy chi tiết ca thi thành công"));
        }

        [HttpGet("filter-by-sinhvien")]
        public async Task<ActionResult<ChiTietCaThiDto>> SelectBy_MSSVThi([FromQuery] int maSinhVien)
        {
            return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaSinhVienThi(maSinhVien), message: "Lấy chi tiết ca thi thành công"));
        }

        [HttpGet("filter-by-cathi-paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Paged<ChiTietCaThiDto>>> SelectBy_MaCaThi_Paged([FromQuery] int maCaThi, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(APIResponse<Paged<ChiTietCaThiDto>>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaCaThi_Paged(maCaThi, pageNumber, pageSize), message: "Lấy chi tiết ca thi thành công"));
        }


        [HttpGet("filter-by-cathi-search-paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Paged<ChiTietCaThiDto>>> SelectBy_MaCaThi_Search_Paged([FromQuery] int maCaThi, [FromQuery] string keyword, [FromQuery] int pageNumber, int pageSize)
        {
            // note: sẽ không có thông tin ca thi ở đây, vì là list, tối ưu lại, tránh lặp ca thi nhiều lần
            return Ok(APIResponse<Paged<ChiTietCaThiDto>>.SuccessResponse(data: await _chiTietCaThiService.SelectBy_MaCaThi_Search_Paged(maCaThi, keyword, pageNumber, pageSize), message: "Lấy chi tiết ca thi thành công"));
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> Insert([FromBody] ChiTietCaThiCreateRequest chiTietCaThi)
        {
            try
            {
                var id = await _chiTietCaThiService.Insert(chiTietCaThi);
                return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Thêm chi tiết ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Thêm chi tiết ca thi không thành công", errorDetails: ex.Message));
            }
        }

        [HttpPost("batch")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ChiTietCaThiDto>>> InsertBatch([FromBody] List<ChiTietCaThiCreateBatchRequest> chiTietCaThis)
        {
            try
            {
                await _chiTietCaThiService.Insert_Batch(chiTietCaThis);
                return Ok(APIResponse<List<ChiTietCaThiDto>>.SuccessResponse(message: "Thêm danh sách chi tiết ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Thêm danh sách chi tiết ca thi không thành công", errorDetails: ex.Message));
            }
        }

        [HttpPost("export-excel")]
        public async Task<ActionResult<byte[]>> GenerateExcelFile([FromBody] List<ChiTietCaThiDto> chiTietCaThis)
        {
            // Cấp phép cho EPPlus
            ExcelPackage.License.SetNonCommercialPersonal("Pino Dat");

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
                var result = await Task.FromResult(package.GetAsByteArray());
                return Ok(APIResponse<byte[]>.SuccessResponse(data: result, message: "Xử lí file chi tiết ca thi thành công"));
            }
        }

        [HttpPost("export-pdf")]
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
            page.Graphics.DrawString(title, titleFont, blackBrush, new PointF(180, 20));

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
            pdfGrid.Draw(page, new PointF(0, 60));

            using var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            var bytes = stream.ToArray();

            return Task.FromResult<IActionResult>(Ok(APIResponse<byte[]>.SuccessResponse(data: bytes, message: "Xử lí file PDF chi tiết ca thi thành công")));
        }

        #endregion

        #region Put Methods

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> Update([FromRoute] int id, [FromBody] ChiTietCaThiUpdateRequest chiTietCaThi)
        {
            try
            {
                var result = await _chiTietCaThiService.Update(id, chiTietCaThi);
                if (!result)
                {
                    return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết ca thi cần cập nhật"));
                }
                return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Cập nhật chi tiết ca thi thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Cập nhật chi tiết ca thi không thành công", errorDetails: ex.Message));
            }
        }

        #endregion

        #region Patch Methods

        [HttpPatch("{id:int}/cong-gio")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ChiTietCaThiDto>> CongGioSinhVien([FromRoute] int id, [FromBody] ChiTietCaThiUpdateCongGioRequest chiTietCaThi)
        {
            try
            {
                var result = await _chiTietCaThiService.CongGio(id, chiTietCaThi);
                if (!result)
                {
                    return NotFound(APIResponse<ChiTietCaThiDto>.NotFoundResponse(message: "Không tìm thấy chi tiết ca thi cần cộng giờ"));
                }
                return Ok(APIResponse<ChiTietCaThiDto>.SuccessResponse(data: await _chiTietCaThiService.SelectOne(id), message: "Cộng giờ cho thí sinh thành công"));
            }
            catch (SqlException sqlEx)
            {
                return SQLExceptionHelper<CaThiDto>.HandleSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<CaThiDto>.ErrorResponse(message: "Cộng giờ cho thí sinh không thành công", errorDetails: ex.Message));
            }

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