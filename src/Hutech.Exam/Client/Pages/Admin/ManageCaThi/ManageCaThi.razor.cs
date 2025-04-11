using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Http.Connections;

namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private AdminDataService MyData { get; set; } = default!;

        private List<CaThiDto>? caThis;
        private List<DotThiDto>? dotThis; // combobox
        private List<MonHocDto>? monHocs; // combobox
        private List<LopAoDto>? lopAos; // combobox
        private HubConnection? hubConnection;


        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                Nav.NavigateTo("/admin", true);
            }
            await Start();
            await base.OnInitializedAsync();
        }

        private bool Filter(CaThiDto element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.TenCaThi != null && RemoveDiacritics(element.TenCaThi).Contains(RemoveDiacritics(searchString), StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.MaCaThi.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        // loại bỏ dấu
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        private async Task GetItemsInSessionStorage()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataMC>("storedDataMC");
            if (storedData != null)
            {
                selectedDotThi = storedData.DotThi;
                selectedMonHoc = storedData.MonHoc;
                selectedLopAo = storedData.LopAo;
                selectedLanThi = storedData.LanThi;
            }
            await FetchAllCaThi();
        }
        private async Task Start()
        {
            dotThis = await GetAllDotThiAPI();
            monHocs = await GetAllMonHocAPI();

            await GetItemsInSessionStorage();

            await CreateHubConnection();
        }
        private async Task OnClickShowDialog(CaThiDto caThi)
        {
            selectedCaThi = caThi;
            string[] content_texts = [caThi.ThoiGianBatDau.ToString() ?? "", caThi.TenCaThi ?? "", caThi.ThoiGianThi.ToString() ?? "", caThi.ActivatedDate.ToString() ?? ""];
            var parameters = new DialogParameters<TinhTrangCaThiDialog>
            {
                { x => x.caThi, caThi },
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraExtraLarge, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<TinhTrangCaThiDialog>("THÔNG TIN CA THI", parameters, options);
        }

        private async Task OnClickChiTietCaThi(CaThiDto caThi)
        {
            //MyData.CaThi = caThi;
            await SessionStorage.SetItemAsync("CaThi", caThi);

            Nav.NavigateTo($"admin/monitor?ma_ca_thi={caThi.MaCaThi}");

        }
        private async Task OnClickXoaCaThi(int ma_ca_thi)
        {
            await Http.DeleteAsync($"api/Admin/DeleteCaThi?ma_ca_thi={ma_ca_thi}");
            if (IsConnectHub())
                await SendMessage();
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

            hubConnection.On("ChangeStatusCaThi", async () =>
            {
                await CallLoadData();
            });

            await hubConnection.StartAsync();

            // tham gia vào group admin
            await hubConnection.InvokeAsync("JoinGroupAdmin");
        }

        private async Task CallLoadData()
        {
            await FetchAllCaThi();
        }
        private bool IsConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task SendMessage()
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessage");
        }
    }
}
