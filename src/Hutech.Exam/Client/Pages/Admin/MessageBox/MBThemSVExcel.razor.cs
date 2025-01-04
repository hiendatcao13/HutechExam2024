using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using OfficeOpenXml;
using System.Globalization;
using System.Text;

namespace Hutech.Exam.Client.Pages.Admin.MessageBox
{
    public partial class MBThemSVExcel
    {
        [Parameter]
        public List<Khoa>? listKhoa { get; set; }
        [Parameter]
        public EventCallback onClickLuu { get; set; }
        [Parameter]
        public EventCallback onClickThoat { get; set; }
        public int ma_khoa_selected = -1;
        public List<SinhVien>? sinhViens { get; set; }
        public bool isReadyToSave = false;
        private ErrorMessage? errorMessages { get; set; }
        [Parameter]
        public List<SinhVien>? sinhVienGocs { get; set; } // ds sinh viên đã tồn tại trong ca thi
        [Inject]
        IJSRuntime? js { get; set; }
        private bool isCheckNull = true;
        private bool isCheckMSSV = false;
        private bool isCheckName = false;
        private bool isCheckSex = false;
        private bool isCheckExist = false;
        private async Task handleFileUpload(InputFileChangeEventArgs e)
        {
            errorMessages?.errorDoubles.Clear();
            errorMessages?.errorExists.Clear();
            isReadyToSave = false;
            StateHasChanged(); // cập nhật giao diện nhanh
            sinhViens?.Clear();
            errorMessages = new ErrorMessage();

            var file = e.File;
            if (file is null || !(file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") ||
                !(file.Name.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) || file.Name.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)))
            {
                js?.InvokeVoidAsync("alert", "Định dạng file không phải Excel hoặc file rỗng");
                return;
            }
            sinhViens = new List<SinhVien>();

            using var memoryStream = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: 5*1024*1024).CopyToAsync(memoryStream); // 3 MB
            memoryStream.Position = 0;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(memoryStream);
            var worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên
            var rowCount = worksheet.Dimension.End.Row;       // Số dòng

            for (int row = 2; row <= rowCount; row++) // Bỏ qua dòng tiêu đề
            {
                SinhVien sinhVien = new SinhVien();
                sinhVien.MaSinhVien = row; // lưu tạm thời mã sinh viên thành dòng bị lỗi
                sinhVien.MaSoSinhVien = worksheet.Cells[row, 1].Text?.Trim();
                sinhVien.HoVaTenLot = worksheet.Cells[row, 2].Text?.Trim();
                sinhVien.TenSinhVien = worksheet.Cells[row, 3].Text?.Trim();
                sinhVien.DiaChi = worksheet.Cells[row, 4].Text?.Trim().ToUpper(); // lớp tạm thời lưu tại địa chỉ, viết hoa toàn bộ
                var gioiTinhText = worksheet.Cells[row, 5].Text?.Trim();
                sinhVien.GioiTinh = (!gioiTinhText.IsNullOrEmpty() && (gioiTinhText == "1" || gioiTinhText == "0")) ? short.Parse(gioiTinhText) : null ;

                //Kiểm tra trùng lặp
                SinhVien? temp = sinhViens.Where(p => p.MaSoSinhVien == sinhVien.MaSoSinhVien).FirstOrDefault();
                if(temp != null  && errorMessages != null)
                {
                    errorMessages.errorDoubles.Add((int)temp.MaSinhVien);
                    errorMessages.errorDoubles.Add((int)sinhVien.MaSinhVien);
                }

                //Kiểm tra có tồn tại trong ca thi chưa
                SinhVien? temp2 = sinhVienGocs?.Where(p => p.MaSoSinhVien == sinhVien.MaSoSinhVien).FirstOrDefault();
                if (!isCheckExist && errorMessages != null)
                {
                    if (temp != null)
                        errorMessages.errorExists.Add((int)temp.MaSinhVien);
                }
                else
                {
                    continue; // bỏ qua, không thêm vào
                }
                sinhViens.Add(sinhVien);
            }
            isValidData();
        }
        private void isValidData()
        {
            errorMessages?.errorNulls.Clear();
            errorMessages?.errorMSSVs.Clear();
            errorMessages?.errorNames.Clear();
            errorMessages?.errorSexs.Clear();

            if(sinhViens != null && errorMessages != null)
            {
                foreach(var item in sinhViens)
                {
                    // mặc định mã số sinh viên không được rỗng
                    if (!isCheckNull && item.MaSoSinhVien.IsNullOrEmpty())
                        errorMessages.errorMSSVs.Add((int)item.MaSinhVien);
                    if (isCheckNull && (item.MaSoSinhVien.IsNullOrEmpty() || item.HoVaTenLot.IsNullOrEmpty() || item.TenSinhVien.IsNullOrEmpty() || item.DiaChi.IsNullOrEmpty() || item.GioiTinh.ToString().IsNullOrEmpty()))
                        errorMessages.errorNulls.Add((int)item.MaSinhVien);
                    if (isCheckMSSV && item.MaSoSinhVien != null && item.MaSoSinhVien.Any(char.IsLetter))
                        errorMessages.errorMSSVs.Add((int)item.MaSinhVien);
                    if(isCheckName && ((item.HoVaTenLot != null && item.HoVaTenLot.Any(char.IsDigit)) || (item.TenSinhVien != null && item.TenSinhVien.Any(char.IsDigit))))
                        errorMessages.errorNames.Add((int)item.MaSinhVien);
                    if (isCheckSex && (item.GioiTinh != 1 && item.GioiTinh != 0))
                        errorMessages.errorSexs.Add((int)item.MaSinhVien);
                }
                int count = errorMessages.errorNulls.Count + errorMessages.errorMSSVs.Count + errorMessages.errorNames.Count + errorMessages.errorSexs.Count + errorMessages.errorDoubles.Count + errorMessages.errorExists.Count;
                if(count > 0)
                {
                    // xuất các lỗi cho người dùng
                    js?.InvokeVoidAsync("alert", errorMessages.printErrorNull() + errorMessages.printErrorMSSV() + errorMessages.printErrorName() + errorMessages.printErrorSex() + errorMessages.printErrorDouble() + errorMessages.printErrorExist());
                    isReadyToSave = false; // kiểm tra theo chế độ của người dùng
                }
                else
                {
                    isReadyToSave = true;
                    js?.InvokeVoidAsync("alert", "Sẵn sàng để lưu");
                }
                StateHasChanged();
            }
        }
        private void onClickCheckNull()
        {
            isCheckNull = !isCheckNull;
            isReadyToSave = false;
            StateHasChanged();
        }
        private void onClickCheckMSSV()
        {
            isCheckMSSV = !isCheckMSSV;
            isValidData();
            StateHasChanged();
        }
        private void onClickCheckName()
        {
            isCheckName = !isCheckName;
            isValidData();
            StateHasChanged();
        }
        private void onClickCheckSex()
        {
            isCheckSex = !isCheckSex;
            isValidData();
            StateHasChanged();
        }
        private void onClickCheckExist()
        {
            isCheckSex = !isCheckSex;
            isValidData();
            StateHasChanged();
        }
    }
    public class ErrorMessage
    {
        public List<int> errorNulls { get; set; }
        public List<int> errorMSSVs { get; set; }
        public List<int> errorNames { get; set; }
        public List<int> errorSexs { get; set; }
        public List<int> errorDoubles { get; set; } // khi mã số sinh viên bị trùng lập
        public List<int> errorExists { get; set; } // khi mã số sinh viên đã tồn tại trong ca thi
        public ErrorMessage()
        {
            errorNulls = new List<int>();
            errorMSSVs = new List<int>();
            errorNames = new List<int>();
            errorSexs = new List<int>();
            errorDoubles = new List<int>();
            errorExists = new List<int>();
        }
        public string printErrorNull()
        {
            if (errorNulls.Count > 0)
            {
                string message = "Kiểm tra phần tử Null dòng: ";
                foreach (var item in errorNulls)
                    message += "[" + item.ToString() + "], ";
                return message + "   ";
            }
            return "";
        }
        public string printErrorMSSV()
        {
            if(errorMSSVs.Count > 0)
            {
                string message = "Kiểm tra phần tử MSSV dòng: ";
                foreach (var item in errorMSSVs)
                    message += "[" + item.ToString() + "], ";
                return message + "   ";
            }
            return "";
        }
        public string printErrorName()
        {
            if(errorNames.Count > 0)
            {
                string message = "Kiểm tra phần tử Họ và Tên dòng: ";
                foreach (var item in errorNames)
                    message += "[" + item.ToString() + "], ";
                return message + "   ";
            }
            return "";
        }
        public string printErrorSex()
        {
            if(errorSexs.Count > 0)
            {
                string message = "Kiểm tra phần tử Giới tính dòng: ";
                foreach (var item in errorSexs)
                    message += "[" + item.ToString() + "], ";
                return message + "   ";
            }
            return "";
        }
        public string printErrorDouble()
        {
            if (errorDoubles.Count > 0)
            {
                string message = "Mã sinh viên bị trùng lặp dòng: ";
                foreach (var item in errorDoubles)
                    message += "[" + item.ToString() + "], ";
                return message + "   ";
            }
            return "";
        }
        public string printErrorExist()
        {
            if (errorExists.Count > 0)
            {
                string message = "Mã sinh viên đã tồn tại trong ca thi dòng: ";
                foreach (var item in errorExists)
                    message += "[" + item.ToString() + "], ";
                return message + "   ";
            }
            return "";
        }
    }
}
