using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http.Connections;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private ApplicationDataService MyData { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        private Canvas2DContext? Context { get; set; }
        protected BECanvasComponent? CanvasReference { get; set; }

        private SinhVienDto? sinhVien = new();
        private CaThiDto? caThi = new();
        private ChiTietCaThiDto? chiTietCaThi = new();
        private List<CustomDeThi>? customDeThis;
        private List<bool?>? ketQuaDapAn = [];
        private double diem;
        private int so_cau_dung;
        private HubConnection? hubConnection;

        private const string LOGOUT_MESSAGE = "Bạn có chắc chắn muốn đăng xuất?";
        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private async Task CheckPage()
        {
            if (MyData.ChiTietCaThi.MaChiTietCaThi == 0 || MyData.SinhVien.MaSinhVien == 0)
            {
                Snackbar.Add(ERROR_PAGE, Severity.Error);
                await Task.Delay(1000);
                Nav.NavigateTo("/info");
                return;
            }
            if (MyData != null && MyData.ChiTietCaThi != null)
            {
                KhoiTaoBanDau();
                chiTietCaThi = MyData.ChiTietCaThi;
                caThi = MyData.ChiTietCaThi.MaCaThiNavigation;
                sinhVien = MyData.SinhVien;
                await Start();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (AuthenticationStateProvider != null) ? (CustomAuthenticationStateProvider)AuthenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                Nav.NavigateTo("/");
            }
            await CheckPage();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (diem != 0)
            {
                Context = await CanvasReference.CreateCanvas2DAsync();
                await Context.SetFontAsync("35px Arial");
                if (diem - (int)diem != 0)
                {
                    await Context.FillTextAsync(diem.ToString(), 5, 35);
                }
                string text = diem + ".0";
                await Context.FillTextAsync(text.ToString(), 5, 35);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task HandleUpdateKetThuc()
        {
            if (chiTietCaThi != null && customDeThis != null)
            {
                chiTietCaThi.ThoiGianKetThuc = DateTime.Now;
                chiTietCaThi.Diem = diem;
                chiTietCaThi.SoCauDung = so_cau_dung;
                chiTietCaThi.TongSoCau = MyData.ListDapAnKhoanh.Count;
                await UpdateKetThucAPI(chiTietCaThi);
            }
        }
        private void TinhDiemSo()
        {
            diem = so_cau_dung = 0;
            double diem_tung_cau = 0;
            if (customDeThis != null)
                diem_tung_cau = (10.0 / customDeThis.Count);
            if (ketQuaDapAn != null)
            {
                foreach (var item in ketQuaDapAn)
                {
                    if (item == true)
                    {
                        diem += diem_tung_cau;
                        so_cau_dung++;
                    }
                }
            }
            diem = QuyDoiDiem(diem);
        }
        private double QuyDoiDiem(double diem)
        {
            double so_phay = diem % 1;
            if (so_phay > 0 && so_phay <= 0.25)
                return Math.Floor(diem) + 0.3;
            if (so_phay > 0.25 && so_phay <= 0.5)
                return Math.Floor(diem) + 0.5;
            if (so_phay > 0.5 && so_phay <= 0.75)
                return Math.Floor(diem) + 0.8;
            if (so_phay > 0.75)
                return Math.Ceiling(diem);
            return Math.Floor(diem);
        }
        private async Task OnClickDangXuatAsync()
        {
            var parameters = new DialogParameters<Simple_Dialog>
        {
            { x => x.ContentText, LOGOUT_MESSAGE },
            { x => x.ButtonText, "Logout" },
            { x => x.Color, Color.Error },
            { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDangXuat())   }
        };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Đăng xuất", parameters, options);
        }
        private async Task HandleDangXuat()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }

        }
        private void KhoiTaoBanDau()
        {
            customDeThis = MyData.CustomDeThis;
        }
        private async Task GetListDungSai()
        {
            var chiTietBaiThis = MyData.ChiTietBaiThis.OrderBy(p => p.ThuTu).ToList();
            var listDapAnKhoanh = new List<int?>();
            int tong_so_cau = MyData.CustomDeThis.Count;
            for(int i = 1; i <= tong_so_cau; i++)
            {
                int? cau_tra_loi = chiTietBaiThis?.FirstOrDefault(p => p.ThuTu == i)?.CauTraLoi;
                listDapAnKhoanh.Add(cau_tra_loi);
            }
            ketQuaDapAn = await GetListDungSaiAPI(listDapAnKhoanh, MyData.ChiTietCaThi.MaDeThi ?? -1);
        }
        private async Task Start()
        {
            await CreateHubConnection();
            await GetListDungSai();
            TinhDiemSo();
            await HandleUpdateKetThuc();
        }
        private async Task CreateHubConnection()
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

            hubConnection.On<long>("ResetLogin", async (ma_sinh_vien) =>
            {
                if (sinhVien != null && sinhVien.MaSinhVien == ma_sinh_vien)
                    await HandleDangXuat();
            });
            await hubConnection.StartAsync();

            //tham gia vào group lớp
            await hubConnection.InvokeAsync("JoinGroupLop", sinhVien?.MaLop ?? -1);
        }
    }
}
