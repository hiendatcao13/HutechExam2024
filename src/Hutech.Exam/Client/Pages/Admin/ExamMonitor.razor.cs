﻿using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Client.Pages.Admin.DAL;
using Hutech.Exam.Client.Pages.Admin.MessageBox;
using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Hutech.Exam.Client.Pages.Admin
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
        private SinhVien? sinhVien { get; set; }
        private List<ChiTietCaThi>? chiTietCaThis { get; set; }
        private List<ChiTietCaThi>? displayChiTietCaThis { get; set; }
        private bool isShowMessageBox { get; set; } // messageBox cộng giờ this
        private ChiTietCaThi? displayChiTietCaThi { get; set; } // hiển thị tên 1 đối tượng sinh viên cho MB cộng giờ
        private string? MB_ly_do_cong { get; set; }
        private int? MB_thoi_gian_cong { get; set; }
        private MBCongGio? MBCongGio { get; set; }
        private int ma_ca_thi { get; set; }
        private bool isShowMBThemSV { get; set; }
        private bool isExsistSVMB { get; set; } // biến check sinh viên tồn tại trong database
        private MBThemSV? MBThemSV { get; set; }
        private SinhVien? sinhVienMBThemSV { get; set; }
        private HubConnection? hubConnection { get; set; }
        private string? input_MSSV { get; set; }

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
                    myData.caThi = await sessionStorage.GetItemAsync<CaThi>("ca_thi");
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
                chiTietCaThis = JsonSerializer.Deserialize<List<ChiTietCaThi>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                myData.caThi = JsonSerializer.Deserialize<CaThi>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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

        private async Task congGioSinhVien(ChiTietCaThi chiTietCaThi)
        {
            var jsonString = JsonSerializer.Serialize(chiTietCaThi);
            if (httpClient != null)
                await httpClient.PostAsync($"api/Admin/CongGioSinhVien?chiTietCaThi={chiTietCaThi}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
        }

        private async Task onClickCongGioThem(ChiTietCaThi chiTietCaThi)
        {
            bool result = (js != null) && await js.InvokeAsync<bool>("confirm", "Cộng giờ thêm được dùng trong trường hợp thí sinh bị treo máy hoặc nguyên nhân thích đáng khác");
            if (result && js != null)
            {
                isShowMessageBox = true;
                displayChiTietCaThi = chiTietCaThi;
            }
        }

        private async Task onClickMBLuu()
        {
            if(displayChiTietCaThi != null && MBCongGio != null && MBCongGio.thoiGianCongThem != null)
            {
                displayChiTietCaThi.ThoiDiemCong = DateTime.Now;
                displayChiTietCaThi.LyDoCong = MBCongGio.lyDoCong;
                displayChiTietCaThi.GioCongThem = (int)MBCongGio.thoiGianCongThem;
                await congGioSinhVien(displayChiTietCaThi);
            }
            onClickMBThoat();
        }

        private void onClickMBThoat()
        {
            isShowMessageBox = false;
            StateHasChanged();
        }
        
        private async void onClickResetLogin(SinhVien sinhVien)
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
        private void onClickThemSV()
        {
            js?.InvokeVoidAsync("alert", "Chức năng thêm sinh viên vào ca thi chỉ dành cho trường hợp khẩn cấp");
            if (MBThemSV != null)
                MBThemSV.is_existMSSV = null;
            isShowMBThemSV = true;
            StateHasChanged();
        }
        private void onClickThoatMBThemSV()
        {
            isShowMBThemSV = false;
            StateHasChanged();
        }
        private async Task onClickCheckSV()
        {
            if(chiTietCaThis != null && MBThemSV != null)
            {
                if(MBThemSV.MSSV == null)
                {
                    js?.InvokeVoidAsync("alert", "Vui lòng nhập đầy đủ thông tin");
                    return;
                }
                SinhVien? sinhVien = chiTietCaThis.FirstOrDefault(p => p.MaSinhVienNavigation?.MaSoSinhVien == MBThemSV.MSSV)?.MaSinhVienNavigation;
                if (sinhVien != null)
                {
                    js?.InvokeVoidAsync("alert", "Sinh viên này đã nằm trong ca thi. Vui lòng kiểm tra");
                    return;
                }
            }
            HttpResponseMessage? response = null;
            if (httpClient != null && MBThemSV != null)
                response = await httpClient.GetAsync($"api/Admin/GetThongTinSinhVien?ma_so_sinh_vien={MBThemSV.MSSV}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                sinhVienMBThemSV = JsonSerializer.Deserialize<SinhVien>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            if (sinhVienMBThemSV != null && sinhVienMBThemSV.TenSinhVien != null && MBThemSV != null)
            {
                MBThemSV.is_existMSSV = true;
                MBThemSV.hoTenLot = sinhVienMBThemSV.HoVaTenLot;
                MBThemSV.tenSinhVien = sinhVienMBThemSV.TenSinhVien;
            }
            else if(MBThemSV != null)
                MBThemSV.is_existMSSV = false;
            StateHasChanged();
        }
        private async Task onClickLuuMBThemSV()
        {
            long? ma_de_hoan_vi = chooseRandomDeThiHV();
            if(ma_de_hoan_vi == null)
            {
                js?.InvokeVoidAsync("alert","Thêm sinh viên cho trường hợp khẩn cấp thất bại. Hãy chắc chắn là các đề đã được tạo và ca thi này phải tồn tại ít nhất 1 sinh viên");
                return;
            }
            if (httpClient != null)
                await httpClient.PostAsync($"api/Admin/InsertCTCT?ma_ca_thi={ma_ca_thi}&ma_sinh_vien={sinhVienMBThemSV?.MaSinhVien}&ma_de_hoan_vi={ma_de_hoan_vi}", null);
            if (isConnectHub())
                await sendMessage(ma_ca_thi);
            onClickThoatMBThemSV();
        }
        private long? chooseRandomDeThiHV()
        {
            // lưu ý là đây là thêm sinh viên trường hợp khẩn cấp, tức là database đã đầy đủ, mọi sinh viên đã có đủ đề
            if(chiTietCaThis != null)
            {
                int count = chiTietCaThis.Count();
                Random rd = new Random();
                int index = rd.Next(0, count - 1);
                return chiTietCaThis[index].MaDeThi;
            }
            return null;
        }
        private void onChangeInputMSSV(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "" && chiTietCaThis != null)
                    displayChiTietCaThis = chiTietCaThis.ToList();
                else if(displayChiTietCaThis != null && chiTietCaThis != null)
                {
                    displayChiTietCaThis.Clear();
                    string temp = "" + e.Value.ToString();
                    var item = chiTietCaThis.Where(p => 
                        p.MaSinhVienNavigation != null && 
                        p.MaSinhVienNavigation.MaSoSinhVien != null && 
                        p.MaSinhVienNavigation.MaSoSinhVien.Contains(temp)
                    ).ToList();
                    displayChiTietCaThis.AddRange(item);
                }
            }
            StateHasChanged();
        }
        private void refresh()
        {
            displayChiTietCaThis = chiTietCaThis;
            input_MSSV = "";
        }
        
        private async Task Start()
        {
            sinhVien = new SinhVien();
            isShowMessageBox = false;
            chiTietCaThis = new List<ChiTietCaThi>();
            displayChiTietCaThis = new List<ChiTietCaThi>();
            MB_ly_do_cong = "";
            MB_thoi_gian_cong = 0;
            displayChiTietCaThi = new ChiTietCaThi();
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