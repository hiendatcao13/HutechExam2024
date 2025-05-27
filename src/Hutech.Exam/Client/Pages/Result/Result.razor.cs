using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using AutoMapper;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private ApplicationDataService MyData { get; set; } = default!;
        [Inject] private StudentHubService StudentHub { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IMapper Mapper { get; set; } = default!;

        // biến binding với UI
        private Canvas2DContext? Context { get; set; }

        private BECanvasComponent? CanvasReference { get; set; }

        private bool IsLoading {get; set;}

        private SinhVienDto SinhVien { get; set; } = default!;

        private CaThiDto CaThi { get; set; } = default!;

        private ChiTietCaThiDto ChiTietCaThi { get; set; } = default!;

        // biến cục bộ

        private List<bool> ketQuaDapAn = [];
        private double diem = 0;
        private int so_cau_dung;
        private HubConnection? hubConnection;
        private bool isShow = false; // chỉ nhận kết quả cho lần đầu, tránh nhận thêm thông điệp từ server do gửi lỗi

        private const string LOGOUT_MESSAGE = "Bạn có chắc chắn muốn đăng xuất?";
        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private async Task CheckPage()
        {
            if (MyData.ChiTietCaThi.MaChiTietCaThi == 0 || MyData.SinhVien.MaSinhVien == 0)
            {
                Snackbar.Add(ERROR_PAGE, Severity.Error);
                Nav.NavigateTo("/info");
                return;
            }
            if (MyData.ChiTietCaThi != null && MyData.ChiTietCaThi.MaCaThiNavigation != null)
            {
                ChiTietCaThi = MyData.ChiTietCaThi;
                CaThi = MyData.ChiTietCaThi.MaCaThiNavigation;
                SinhVien = MyData.SinhVien;
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

        private async Task OnClickDangXuatAsync()
        {
            var parameters = new DialogParameters<Simple_Dialog>
        {
            { x => x.ContentText, LOGOUT_MESSAGE },
            { x => x.ButtonText, "Đăng xuất" },
            { x => x.Color, Color.Error },
            { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDangXuat())   }
        };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Thoát tài khoản", parameters, options);
        }
        private async Task HandleDangXuat()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien.MaSinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }

        }
        private async Task Start()
        {
            IsLoading = true;
            await CreateHubConnection();
        }
        private async Task CreateHubConnection()
        {
            hubConnection = await StudentHub.GetConnectionAsync(SinhVien?.MaSinhVien ?? -1);

            hubConnection.On("ResetLogin", async () =>
            {
                await HandleDangXuat();
            });

            hubConnection.On<List<bool>, int, double>("DeliverDapAn", async (dapAns, so_cau_dung, diem) =>
            {
                if(!isShow) // chỉ nhận cho lần đầu
                {
                    ketQuaDapAn = dapAns;
                    this.so_cau_dung = so_cau_dung;
                    this.diem = diem;
                    IsLoading = false;

                    // hiển thị điểm
                    await HandleDiem();
                    StateHasChanged();

                    isShow = true;  
                }    
            });

            //rời group ca thi
            await hubConnection.InvokeAsync("LeaveGroupMaCaThi", CaThi?.MaCaThi ?? -1);
        }
        private async Task HandleDiem()
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
    }
}
