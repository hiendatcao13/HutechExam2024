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
using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.DTO.Request.Custom;

namespace Hutech.Exam.Client.Pages.Result
{
    public partial class Result
    {
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private ApplicationDataService MyData { get; set; } = default!;

        [Inject] private StudentHubService StudentHub { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;


        // biến binding với UI
        private Canvas2DContext? Context { get; set; }

        private BECanvasComponent? CanvasReference { get; set; }

        private bool IsLoading {get; set;}

        private SinhVienDto Student { get; set; } = default!;

        private CaThiDto ExamSession { get; set; } = default!;

        private ChiTietCaThiDto ExamSessionDetail { get; set; } = default!;

        // biến cục bộ

        private List<bool?> selectedAnswerResults = [];
        private double score = 0;
        private int totalRightAnswer;
        private HubConnection? hubConnection;
        private bool isShow = false; // chỉ nhận kết quả cho lần đầu, tránh nhận thêm thông điệp từ server do gửi lỗi

        private const string LOGOUT_MESSAGE = "Bạn có chắc chắn muốn đăng xuất?";
        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        
        private async Task CheckPageAsync()
        {
            if (MyData.ChiTietCaThi.MaChiTietCaThi == 0 || MyData.SinhVien.MaSinhVien == 0)
            {
                Snackbar.Add(ERROR_PAGE, Severity.Error);
                Nav.NavigateTo("/info");
                return;
            }
            if (MyData.ChiTietCaThi != null && MyData.ChiTietCaThi.MaCaThiNavigation != null)
            {
                ExamSessionDetail = MyData.ChiTietCaThi;
                ExamSession = MyData.ChiTietCaThi.MaCaThiNavigation;
                Student = MyData.SinhVien;
                await StartAsync();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            try
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
                await CheckPageAsync();
                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                Snackbar.Add("Hệ thống server đang gặp sự cố. Vui lòng liên hệ người giám sát", Severity.Error);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (score != 0)
            {
                Context = await CanvasReference.CreateCanvas2DAsync();
                await Context.SetFontAsync("35px Arial");
                if (score - (int)score != 0)
                {
                    await Context.FillTextAsync(score.ToString(), 5, 35);
                }
                string text = score + ".0";
                await Context.FillTextAsync(text.ToString(), 5, 35);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task OnClickLogoutAsync()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, LOGOUT_MESSAGE },
                { x => x.ButtonText, "Đăng xuất" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleLogoutAsync())   }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Thoát tài khoản", parameters, options);
        }

        private async Task HandleLogoutAsync()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien.MaSinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }

        }

        private async Task StartAsync()
        {
            IsLoading = true;
            await CreateHubConnectionAsync();
        }

        private async Task CreateHubConnectionAsync()
        {
            hubConnection = await StudentHub.GetConnectionAsync(Student?.MaSinhVien ?? -1);

            hubConnection.On("ResetLogin", async () =>
            {
                await HandleLogoutAsync();
            });

            hubConnection.On("RequestRecoverySubmit", async () =>
            {
                var storedData = await GetDataAsync();
                if(storedData != null)
                {
                    await StudentHub.RecoverySubmit(storedData);
                }    
            });

            hubConnection.On<List<bool?>, int, double>("DeliverDapAn", async (dapAns, so_cau_dung, diem) =>
            {
                if(!isShow) // chỉ nhận cho lần đầu
                {
                    selectedAnswerResults = dapAns;
                    this.totalRightAnswer = so_cau_dung;
                    this.score = diem;
                    IsLoading = false;

                    // hiển thị điểm
                    await HandleScoreAsync();
                    StateHasChanged();

                    isShow = true;  
                }    
            });

            //rời group ca thi
            await hubConnection.InvokeAsync("LeaveGroupMaCaThi", ExamSession?.MaCaThi ?? -1);
        }

        private async Task HandleScoreAsync()
        {
            Context = await CanvasReference.CreateCanvas2DAsync();
            await Context.SetFontAsync("35px Arial");
            if (score - (int)score != 0)
            {
                await Context.FillTextAsync(score.ToString(), 5, 35);
            }
            string text = score + ".0";
            await Context.FillTextAsync(text.ToString(), 5, 35);
        }

        private async Task<SubmitRequest?> GetDataAsync()
        {
            return await SessionStorage.GetItemAsync<SubmitRequest?>("SubmitRequest");
        }
    }
}
