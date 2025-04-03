using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;
using OfficeOpenXml;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Http.Connections;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        [Parameter][SupplyParameterFromQuery] public string? ma_ca_thi { get; set; }
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        [Inject] private IJSRuntime Js { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        private CaThiDto? caThi;
        private List<ChiTietCaThiDto>? chiTietCaThis;
        private HubConnection? hubConnection;

        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private const string SUCCESS_RESETLOGIN = "Reset đăng nhập cho thí sinh thành công";
        private const string ERROR_RESETLOGIN = "Reset đăng nhập cho thí sinh thất bại";
        private const string ALERT_ADDSV = "Thêm thí sinh được dùng cho việc khẩn cấp. Hãy đảm bảo MSSV thí sinh đã tồn tại trong hệ thống";
        private const string MISSINGINFO_RESETLOGIN = "Tính năng reset login không thể hoạt động khi thiếu thông tin mã lớp";

        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                int maCaThi = -1;
                caThi = await SessionStorage.GetItemAsync<CaThiDto>("CaThi");
                bool isConvert = int.TryParse(ma_ca_thi, out maCaThi);
                if (ma_ca_thi == null || caThi == null && !isConvert || (isConvert && caThi != null && caThi.MaCaThi != maCaThi))
                {
                    Snackbar.Add(ERROR_PAGE, Severity.Error);
                    Nav.NavigateTo("/admin/control");
                    return;
                }
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                Nav.NavigateTo("/admin", true);
            }
            await Start();
            await base.OnInitializedAsync();
        }
        private async void OnClickResetLogin(SinhVienDto? sinhVien)
        {
            if (sinhVien != null && sinhVien.IsLoggedIn == true)
            {
                var parameters = new DialogParameters<Simple_Dialog>
                {
                    { x => x.ContentText, $"Thí sinh đăng nhập lần cuối vào lúc {sinhVien.LastLoggedIn}. Hãy cân nhắc thời gian trên và chắc chắn rằng sinh viên không gian lận" },
                    { x => x.ButtonText, "Reset" },
                    { x => x.Color, Color.Warning },
                    { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleResetLogin(sinhVien))   }
                };

                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

                await Dialog.ShowAsync<Simple_Dialog>("Đăng xuất cho thí sinh", parameters, options);

            }
        }
        private async Task HandleResetLogin(SinhVienDto sinhVien)
        {
            if(sinhVien.MaLop == null)
            {
                Snackbar.Add(MISSINGINFO_RESETLOGIN, Severity.Error);
                return;
            }
            await ResetLoginAPI(sinhVien);
        }

        private async Task Refresh()
        {
            chiTietCaThis = await GetThongTinChiTietCaThiAPI(caThi?.MaCaThi ?? -1);
        }
        public async Task<byte[]> GenerateExcelAsync()
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
        private async Task OnClickDownloadExcel()
        {
            var excelData = await GenerateExcelAsync();
            var base64 = Convert.ToBase64String(excelData);
            var fileName = $"Bảng điểm ca thi {caThi?.MaCaThi}.xlsx";

            // Tạo link tải xuống
            Js?.InvokeVoidAsync("downloadFile", fileName, base64);
        }

        private async Task Start()
        {
            chiTietCaThis = await GetThongTinChiTietCaThiAPI(caThi?.MaCaThi ?? -1);
            await CreateHubConnection();
        }
        private async Task CreateHubConnection()
        {
            if (chiTietCaThis != null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(Nav.ToAbsoluteUri("/MainHub"), options =>
                    {
                        options.Transports = HttpTransportType.WebSockets; // Ưu tiên WebSockets nếu có thể
                    })
                    .WithAutomaticReconnect() // Tự động kết nối lại nếu mất mạng
                    .Build();

                hubConnection.Closed += async (error) =>
                {
                    await Task.Delay(5000); // Chờ 5s trước khi thử kết nối lại
                    await CreateHubConnection(); // Thử kết nối lại
                };

                hubConnection.On<long>("SV_Authentication", async (ma_sinh_vien) =>
                {
                    if (chiTietCaThis.Exists(p => p.MaSinhVien == ma_sinh_vien))
                    {
                        await CallLoadData();
                    }
                });
                hubConnection.On<int>("SV_Status", async (ma_chi_tiet_ca_thi) =>
                {
                    if (chiTietCaThis.Exists(p => p.MaChiTietCaThi == ma_chi_tiet_ca_thi))
                    {
                        await CallLoadData();
                    }
                });

                await hubConnection.StartAsync();

                await hubConnection.InvokeAsync("JoinGroupAdmin");
            }
        }

        private async Task CallLoadData()
        {
            chiTietCaThis = await GetThongTinChiTietCaThiAPI(caThi?.MaCaThi ?? -1);
        }
        private bool IsConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task SendMessage(int ma_ca_thi)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMCT", ma_ca_thi);
        }
        public void Dispose()
        {
            hubConnection?.DisposeAsync();
        }
    }
}
