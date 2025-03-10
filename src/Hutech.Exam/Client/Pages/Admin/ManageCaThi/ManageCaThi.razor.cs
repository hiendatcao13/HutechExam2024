using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Hutech.Exam.Client.Pages.Admin.DAL;
using System.Text.Json;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using MudBlazor;
using static MudBlazor.CategoryTypes;
using System.Globalization;
using System.Text;
using Hutech.Exam.Client.Components.Dialogs;

namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
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
        private string? input_maCaThi { get; set; }
        private DateTime? input_Date { get; set; }
        private List<CaThiDto>? caThis { get; set; }
        private List<CaThiDto>? displayCaThis { get; set; }
        private bool showMessageBox { get; set; }
        private CaThiDto? showCaThiMessageBox { get; set; }
        private UserDto? user { get; set; }
        private HubConnection? hubConnection { get; set; }
        private string GetActivationIcon(CaThiDto item) => item.KetThuc ? Icons.Material.Filled.PlayCircleFilled : item.IsActivated ? Icons.Material.Filled.CheckCircle : Icons.Material.Filled.Cancel;
        private Color GetActivationColor(CaThiDto item) => item.KetThuc ? Color.Default : item.IsActivated ? Color.Success : Color.Error;
        private string searchString = "";
        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (authenticationStateProvider != null) ? (CustomAuthenticationStateProvider)authenticationStateProvider : null;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token) && httpClient != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                navManager?.NavigateTo("/admin", true);
            }
            await Start();
            await base.OnInitializedAsync();
        }
        private async Task<bool> checkAdmin()
        {
            var authState = (authenticationState != null) ? await authenticationState : null;
            // lấy thông tin admin từ claim
            string loginName = "";
            if(authState != null && authState.User.Identity != null && authState.User.Identity.Name != null)
                loginName = authState.User.Identity.Name;
            await getThongTinUser(loginName);
            return user != null;
        }
        private async Task getThongTinUser(string loginName)
        {
            if (sessionStorage != null)
                user = await sessionStorage.GetItemAsync<UserDto>("user");
        }
        private async Task getAllCaThi()
        {
            HttpResponseMessage? response = null;
            if (httpClient != null)
                response = await httpClient.GetAsync("api/Admin/GetAllCaThi");
            if (response != null && response.IsSuccessStatusCode )
            {
                var resultString = await response.Content.ReadAsStringAsync();
                //ApiResponse<List<CaThi>>? temp = JsonSerializer.Deserialize<ApiResponse<List<CaThi>>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //displayCaThis = caThis = temp.result;
                displayCaThis = caThis = JsonSerializer.Deserialize<List<CaThiDto>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        private bool filter(CaThiDto element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.TenCaThi != null && removeDiacritics(element.TenCaThi).Contains(removeDiacritics(searchString), StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.MaCaThi.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        // loại bỏ dấu
        public static string removeDiacritics(string text)
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
        private async Task Start()
        {
            caThis = new();
            displayCaThis = new();
            showMessageBox = false;
            showCaThiMessageBox = new();
            user = new();
            await getAllCaThi();
            await createHubConnection();
            await checkAdmin();
        }
        private async Task onClickShowMessageBox(CaThiDto caThi)
        {
            //showMessageBox = true;
            showCaThiMessageBox = caThi;
            //StateHasChanged();
            string[] content_texts = [caThi.ThoiGianBatDau.ToString(), caThi.MaChiTietDotThiNavigation.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc ?? "", caThi.TenCaThi ?? "", caThi.ThoiGianThi.ToString(), 
                caThi.MaChiTietDotThiNavigation.MaDotThiNavigation.TenDotThi ?? "", caThi.ActivatedDate.ToString() ?? ""];
            var parameters = new DialogParameters<TinhTrangCaThiDialog>
            {
                { x => x.ContentText, content_texts },
                { x => x.trangThai, caThi.KetThuc ? 2 : caThi.IsActivated ? 0 : 1 },
                { x => x.onHandleKichHoat, EventCallback.Factory.Create(this, onClickKichHoatCaThi) },
                { x => x.onHandleHuyKichHoat, EventCallback.Factory.Create(this, onClickHuyKichHoatCaThi) },
                { x => x.onHandleDungCaThi, EventCallback.Factory.Create(this, onClickDungCaThi) },
                { x => x.onHandleKetThucCaThi, EventCallback.Factory.Create(this, onClickKetThucCaThi) }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraExtraLarge, BackgroundClass = "my-custom-class", Position = DialogPosition.TopCenter };

            await Dialog.ShowAsync<TinhTrangCaThiDialog>("THÔNG TIN CA THI", parameters, options);
            StateHasChanged();
        }

        private async Task OnClickChiTietCaThi(CaThiDto caThi)
        {
            if(myData != null && sessionStorage != null)
            {
                myData.CaThi = caThi;
                await sessionStorage.SetItemAsync("ca_thi", caThi);
                navManager?.NavigateTo("admin/monitor");
            }
        }
        private async Task UpdateTinhTrangCaThi(bool isActived)
        {
            HttpResponseMessage? response = null;
            if (httpClient != null && showCaThiMessageBox != null)
                response = await httpClient.GetAsync($"api/Admin/UpdateTinhTrangCaThi?ma_ca_thi={showCaThiMessageBox.MaCaThi}&isActived={isActived}");
        }
        private async Task HuyKichHoatCaThi()
        {
            HttpResponseMessage? response = null;
            if (httpClient != null && showCaThiMessageBox != null)
                response = await httpClient.GetAsync($"api/Admin/HuyKichHoatCaThi?ma_ca_thi={showCaThiMessageBox.MaCaThi}");
        }
        private async Task<bool> KetThucCaThi()
        {
            HttpResponseMessage? response = null;
            if (httpClient != null && showCaThiMessageBox != null)
                response = await httpClient.GetAsync($"api/Admin/KetThucCaThi?ma_ca_thi={showCaThiMessageBox.MaCaThi}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<bool>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return false;
        }
        private async Task onClickKichHoatCaThi()
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Bạn có muốn kích hoạt ca thi ?");
            if (result && showCaThiMessageBox != null)
            {
                await UpdateTinhTrangCaThi(true);
                await reLoadingComponent(showCaThiMessageBox.MaCaThi);
            }
            showMessageBox = false;
        }
        private async Task onClickHuyKichHoatCaThi()
        {
            // chỉ hủy kích hoạt ca thi khi mà không còn thí sinh nào đang thi

            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn hủy kích hoạt ca thi. Hệ thống sẽ hủy toàn bộ những ghi nhận đáp án mà tất cả sinh viên đã làm trong ca thi này. " +
                "Nếu bạn chỉ muốn dừng tạm thời ca thi, hãy chọn \"Dừng ca thi\".");
            if (result && showCaThiMessageBox != null)
            {
                js?.InvokeVoidAsync("alert", "Hệ thống sẽ mất ít thời gian để xóa toàn bộ dữ liệu");
                await HuyKichHoatCaThi();
                await reLoadingComponent(showCaThiMessageBox.MaCaThi);
            }
            showMessageBox = false;
        }
        private async Task onClickKetThucCaThi()
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn kết thúc ca thi. Việc này sẽ không thể kích hoạt lại ca thi này nữa");
            if (result && showCaThiMessageBox != null)
            {
                bool isKetThuc = await KetThucCaThi();
                if (!isKetThuc)
                {
                    js?.InvokeVoidAsync("alert", "Ca thi không thể kết thúc khi có thí sinh chưa hoàn thành bài thi. Vui lòng hủy kích hoạt ca thi trước");
                    return;
                }
                await reLoadingComponent(showCaThiMessageBox.MaCaThi);
            }
            showMessageBox = false;
        }
        private async Task onClickDungCaThi()
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn dừng ca thi. Việc này sẽ sẽ khiến toàn bộ sinh viên đang làm bài bị out ra tạm thời cho đến khi được kích hoạt trở lại");
            if (result && httpClient != null && showCaThiMessageBox != null)
            {
                await UpdateTinhTrangCaThi(false);
                await reLoadingComponent(showCaThiMessageBox.MaCaThi);
            }
            showMessageBox = false;
        }
        private void onClickThoatMessageBox()
        {
            showMessageBox = false;
            StateHasChanged();
        }
        private async Task onClickXoaCaThi(int ma_ca_thi)
        {
            if (httpClient != null)
                await httpClient.DeleteAsync($"api/Admin/DeleteCaThi?ma_ca_thi={ma_ca_thi}");
            if(isConnectHub())
                await sendMessage();
        }
        private async Task createHubConnection()
        {
            if (navManager != null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri("/ChiTietCaThiHub"))
                    .Build();

                hubConnection.On("ReceiveMessage", () =>
                {
                    callLoadData();
                    StateHasChanged();
                });
                await hubConnection.StartAsync();
                await getAllCaThi();
            }
        }

        private void callLoadData()
        {
            Task.Run(async () =>
            {
                await getAllCaThi();
                StateHasChanged();
            });
        }
        private async Task reLoadingComponent(int ma_ca_thi)
        {
            if (isConnectHub())
            {
                await sendMessage();
                await SendMessageStatusCaThi(ma_ca_thi);
            }
            StateHasChanged();
        }
        private bool isConnectHub() => hubConnection?.State == HubConnectionState.Connected;

        private async Task sendMessage()
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessage");
        }
        private async Task SendMessageStatusCaThi(int ma_ca_thi)
        {
            if (hubConnection != null)
                await hubConnection.SendAsync("SendMessageStatusCaThi", ma_ca_thi);
        }
    }
}
