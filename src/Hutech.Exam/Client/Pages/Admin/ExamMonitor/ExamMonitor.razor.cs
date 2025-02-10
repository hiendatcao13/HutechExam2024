using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Client.Pages.Admin.DAL;
using Hutech.Exam.Client.Pages.Admin.MessageBox;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Ninject.Activation;
using OfficeOpenXml;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        [Inject]
        HttpClient? httpClient { get; set; }
        [Inject]
        AdminDataService? myData { get; set; }
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        NavigationManager? navManager { get; set; }
        [Inject]
        IJSRuntime? js { get; set; }
        [Inject]
        Blazored.SessionStorage.ISessionStorageService? sessionStorage { get; set; }
        private SinhVienDto? sinhVien { get; set; }
        private List<ChiTietCaThiDto>? chiTietCaThis { get; set; }
        private List<ChiTietCaThiDto>? displayChiTietCaThis { get; set; }
        private bool isShowMessageBox { get; set; } // messageBox cộng giờ this
        private ChiTietCaThiDto? displayChiTietCaThi { get; set; } // hiển thị tên 1 đối tượng sinh viên cho MB cộng giờ
        private string? MB_ly_do_cong { get; set; }
        private int? MB_thoi_gian_cong { get; set; }
        private MBCongGio? MBCongGio { get; set; }
        private int ma_ca_thi { get; set; }
        private bool isShowMBThemSV { get; set; }
        private MBThemSV? MBThemSV { get; set; }
        private SinhVienDto? sinhVienMBThemSV { get; set; }
        private HubConnection? hubConnection { get; set; }
        private string? input_MSSV { get; set; }
        private bool isShowMBExcel { get; set; }
        private MBThemSVExcel? MBThemSVExcel { get; set; }
        private List<KhoaDto>? listKhoa { get; set; }
        private bool isShowMBSuaSV { get; set; }
        private List<long>? listMaDes { get; set; }
        private MBSuaSV? MBSuaSV { get; set; }
        private ChiTietCaThiDto? displayCTCTMBSuaSV { get; set; }

        protected async override Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                if (myData != null && myData.caThi == null && js != null && sessionStorage != null)
                {
                    myData.caThi = await sessionStorage.GetItemAsync<CaThiDto>("ca_thi");
                    if(myData.caThi == null)
                    {
                        await js.InvokeVoidAsync("alert", "Cách hoạt động trang web không bình thường. Vui lòng quay lại");
                        navManager?.NavigateTo("/control");
                        return;
                    }
                    ma_ca_thi = myData.caThi.MaCaThi;
                }
            }
            else
            {
                navManager?.NavigateTo("/admin", true);
                return;
            }
            await Start();
            await base.OnInitializedAsync();
        }

        private async Task getThongTinChiTietCaThi(int ma_ca_thi)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.GetAsync($"api/Admin/GetThongTinCTCaThiTheoMaCaThi?ma_ca_thi={ma_ca_thi}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                chiTietCaThis = JsonSerializer.Deserialize<List<ChiTietCaThiDto>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                displayChiTietCaThis = chiTietCaThis?.ToList();
            }
            StateHasChanged();
        }
        private async Task getThongTinCaThi(int ma_ca_thi)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.GetAsync($"api/Admin/GetThongTinCaThi?ma_ca_thi={ma_ca_thi}");
            if (response != null && response.IsSuccessStatusCode && myData != null)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                myData.caThi = JsonSerializer.Deserialize<CaThiDto>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            StateHasChanged();
        }
        private async Task onClickDangXuat()
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn đăng xuất?");
            if (result && authenticationStateProvider != null)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                navManager?.NavigateTo("/admin", true);
            }
        }
        private async void onClickResetLogin(SinhVienDto sinhVien)
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", $"Thí sinh đăng nhập lần cuối vào lúc {sinhVien.LastLoggedIn}. Hãy cân nhắc thời gian trên và chắc chắn rằng sinh viên không gian lận");
            if (result && httpClient != null)
            {
                await httpClient.PostAsync($"api/Admin/UpdateLogoutSinhVien?ma_sinh_vien={sinhVien.MaSinhVien}", null);
                if (isConnectHub())
                {
                    await sendMessage(ma_ca_thi);
                    await sendMessageResetLogin(sinhVien.MaSinhVien);
                }
            }
        }

        private async Task congGioSinhVien(ChiTietCaThiDto chiTietCaThi)
        {
            var jsonString = JsonSerializer.Serialize(chiTietCaThi);
            if (httpClient != null)
                await httpClient.PostAsync($"api/Admin/CongGioSinhVien?chiTietCaThi={chiTietCaThi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
        }
        private async Task onClickRemoveCTCT(ChiTietCaThiDto chiTietCaThi)
        {
            await getAllDeThi();
            string? ten_sv = chiTietCaThi.MaSinhVienNavigation?.HoVaTenLot + chiTietCaThi.MaSinhVienNavigation?.TenSinhVien;
            bool result = await js.InvokeAsync<bool>("confirm", $"Bạn muốn xóa sinh viên {ten_sv}?");
            if (result)
            {
                if (httpClient != null)
                    await httpClient.DeleteAsync($"api/ExamMonitor/RemoveCTCT?ma_chi_tiet_ca_thi={chiTietCaThi.MaChiTietCaThi}");
                if (isConnectHub())
                    await sendMessage(ma_ca_thi);
            }
        }

        private async void refresh()
        {
            displayChiTietCaThis = chiTietCaThis;
            input_MSSV = "";
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
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

                if(chiTietCaThis != null)
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
        private async Task onClickDownloadExcel()
        {
            var excelData = await GenerateExcelAsync();
            var base64 = Convert.ToBase64String(excelData);
            var fileName = $"Bảng điểm ca thi {ma_ca_thi}.xlsx";

            // Tạo link tải xuống
            js?.InvokeVoidAsync("downloadFile", fileName, base64);
        }

        private async Task Start()
        {
            sinhVien = new();
            isShowMessageBox = isShowMBExcel = false;
            chiTietCaThis = new();
            displayChiTietCaThis = new();
            displayCTCTMBSuaSV = new();
            MB_ly_do_cong = "";
            MB_thoi_gian_cong = 0;
            displayChiTietCaThi = new();
            if (myData != null && myData.caThi != null)
                ma_ca_thi = myData.caThi.MaCaThi;
            else
                await getThongTinCaThi(ma_ca_thi);
            await createHubConnection();
        }
        private async Task createHubConnection()
        {
            if (navManager != null && chiTietCaThis != null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri("/ChiTietCaThiHub"))
                    .Build();

                hubConnection.On<int>("ReceiveMessageMCT", (ma_ca_thi_message) =>
                {
                    if (ma_ca_thi_message == ma_ca_thi)
                    {
                        callLoadData();
                        StateHasChanged();
                    }
                });
                hubConnection.On<long>("ReceiveMessageMSV", (ma_sinh_vien) =>
                {
                    if (chiTietCaThis.Exists(p => p.MaSinhVien == ma_sinh_vien))
                    {
                        callLoadData();
                        StateHasChanged();
                    }
                });
                await hubConnection.StartAsync();
                await getThongTinChiTietCaThi(ma_ca_thi);
            }
        }

        private void callLoadData()
        {
            Task.Run(async () =>
            {
                await getThongTinChiTietCaThi(ma_ca_thi);
                StateHasChanged();
            });
        }
        private bool isConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task sendMessage(int ma_ca_thi)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageMCT", ma_ca_thi);
        }
        private async Task sendMessageResetLogin(long ma_sinh_vien)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageResetLogin", ma_sinh_vien);
        }
        public void Dispose()
        {
            hubConnection?.DisposeAsync();
        }
    }
}
