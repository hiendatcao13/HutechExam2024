﻿using Hutech.Exam.Client.DAL;
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
using static MudBlazor.CategoryTypes;
namespace Hutech.Exam.Client.Pages;

public partial class Result
{
    [Inject]
    HttpClient? httpClient { get; set; }
    [Inject]
    ApplicationDataService? myData { get; set; }
    [Inject]
    AuthenticationStateProvider? authenticationStateProvider { get; set; }
    [Inject]
    NavigationManager? navManager { get; set; }
    [Inject]
    IJSRuntime? js { get; set; }
    private Canvas2DContext? _context { get; set; }
    protected BECanvasComponent? _canvasReference { get; set; }
    private SinhVienDto? sinhVien { get; set; }
    private CaThiDto? caThi { get; set; }
    private ChiTietCaThiDto? chiTietCaThi { get; set; }
    private List<CustomDeThi>? customDeThis { get; set; }
    private List<int>? listDapAnThucTe { get; set; }
    private List<bool?>? ketQuaDapAn { get; set; }
    private double diem { get; set; }
    private int so_cau_dung { get; set; }
    private HubConnection? hubConnection { get; set; }

    private const string LOGOUT_MESSAGE = "Bạn có chắc chắn muốn đăng xuất?";
    private async Task checkPage()
    {
        if ((myData == null || myData.ChiTietCaThi == null || myData.SinhVien == null) && js != null)
        {
            await js.InvokeVoidAsync("alert", "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại");
            navManager?.NavigateTo("/info");
            return;
        }
        if (myData != null && myData.ChiTietCaThi != null)
        {
            khoiTaoBanDau();
            chiTietCaThi = myData.ChiTietCaThi;
            caThi = myData.ChiTietCaThi.MaCaThiNavigation;
            sinhVien = myData.SinhVien;
            await Start();
        }
    }
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
            navManager?.NavigateTo("/");
        }
        await checkPage();
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(diem != 0)
        {
            _context = await _canvasReference.CreateCanvas2DAsync();
            await _context.SetFontAsync("35px Arial");
            if(diem - (int)diem != 0)
            {
                await _context.FillTextAsync(diem.ToString(), 5, 35);
            }
            string text = diem + ".0";
            await _context.FillTextAsync(text.ToString(), 5, 35);
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task HandleUpdateKetThuc()
    {
        if(chiTietCaThi != null && httpClient != null && myData != null && customDeThis != null)
        {
            chiTietCaThi.ThoiGianKetThuc = DateTime.Now;
            chiTietCaThi.Diem = diem;
            chiTietCaThi.SoCauDung = so_cau_dung;
            chiTietCaThi.TongSoCau = customDeThis.Count;
            var jsonString = JsonSerializer.Serialize(chiTietCaThi);
            await httpClient.PostAsync("api/Result/UpdateKetThuc", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            if (isConnectHub() && chiTietCaThi != null && chiTietCaThi.MaCaThi != null)
                await sendMessage((int)chiTietCaThi.MaCaThi);
        }
    }
    private void tinhDiemSo()
    {
        diem = so_cau_dung = 0;
        double diem_tung_cau = 0;
        if(customDeThis != null)
            diem_tung_cau = (10.0 / customDeThis.Count);
        if(ketQuaDapAn != null)
        {
            foreach(var item in ketQuaDapAn)
            {
                if (item == true)
                {
                    diem += diem_tung_cau;
                    so_cau_dung++;
                }
            }
        }
        diem = quyDoiDiem(diem);
    }
    private double quyDoiDiem(double diem)
    {
        double so_phay = diem % 1;
        if (so_phay > 0 && so_phay <= 0.25)
            return Math.Floor(diem) + 0.3;
        if (so_phay > 0.25 && so_phay <= 0.5)
            return Math.Floor(diem) + 0.5;
        if (so_phay > 0.5 && so_phay <= 0.75)
            return Math.Floor(diem) + 0.8;
        if(so_phay > 0.75)
            return Math.Ceiling(diem);
        return Math.Floor(diem);
    }
    private async Task onClickDangXuatAsync()
    {
        var parameters = new DialogParameters<Simple_Dialog>
        {
            { x => x.ContentText, LOGOUT_MESSAGE },
            { x => x.ButtonText, "Logout" },
            { x => x.Color, Color.Error },
            { x => x.onHandleSubmit, EventCallback.Factory.Create(this, UpdateLogout)   }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

        await Dialog.ShowAsync<Simple_Dialog>("Đăng xuất", parameters, options);
    }
    private async Task UpdateLogout()
    {
        if(httpClient != null && sinhVien != null && authenticationStateProvider != null)
        {
            await httpClient.GetAsync($"api/User/UpdateLogout?ma_sinh_vien={sinhVien.MaSinhVien}");
            // Cập nhật cho quản trị viên biết sinh viên đã đăng xuất
            if (isConnectHub() && sinhVien != null)
            {
                await sendMessage(sinhVien.MaSinhVien);
            }
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(null);
            navManager?.NavigateTo("/", true);
        }

    }
    private void khoiTaoBanDau()
    {
        sinhVien = new();
        caThi = new();
        chiTietCaThi = new();
        if(myData != null)
        {
            customDeThis = myData.CustomDeThis;
        }
        ketQuaDapAn = new List<bool?>();
    }
    private async Task getListDungSai()
    {
        HttpResponseMessage? response = null;
        if (httpClient != null && chiTietCaThi != null)
            response = await httpClient.GetAsync($"api/Result/GetListDungSai?ma_chi_tiet_ca_thi={chiTietCaThi.MaChiTietCaThi}&tong_so_cau={customDeThis?.Count}");
        if (response != null && response.IsSuccessStatusCode && myData != null)
        {
            var resultString = await response.Content.ReadAsStringAsync();
            ketQuaDapAn =  JsonSerializer.Deserialize<List<bool?>>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    private async Task Start()
    {
        await initialHubConnection();
        listDapAnThucTe = new List<int>();
        await getListDungSai();
        tinhDiemSo();
        await HandleUpdateKetThuc();
    }
    private async Task initialHubConnection()
    {
        if (navManager != null)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(navManager.ToAbsoluteUri("/ChiTietCaThiHub"))
                .Build();
            hubConnection.On<long>("ReceiveMessageResetLogin", (ma_so_sv) =>
            {
                if (sinhVien != null && ma_so_sv == sinhVien.MaSinhVien)
                    resetLogin();
            });
            await hubConnection.StartAsync();
        }
    }

    private bool isConnectHub() => hubConnection?.State == HubConnectionState.Connected;
    private async Task sendMessage(long ma_sinh_vien)
    {
        if (hubConnection != null)
            await hubConnection.SendAsync("SendMessageMSV", ma_sinh_vien);
    }
    private async Task sendMessage(int ma_ca_thi)
    {
        if (hubConnection != null)
            await hubConnection.SendAsync("SendMessageMCT", ma_ca_thi);
    }
    private void resetLogin()
    {
        Task.Run(async () =>
        {
            if (authenticationStateProvider != null)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                navManager?.NavigateTo("/", true);
            }
        });
    }
}