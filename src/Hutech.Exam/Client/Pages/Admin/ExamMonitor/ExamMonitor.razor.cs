﻿using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using MudBlazor;
using Hutech.Exam.Client.Components.Dialogs;
using Microsoft.JSInterop;
using Hutech.Exam.Client.DAL;
using Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog;
using System.Net.Http.Json;
using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Pages.Result;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor : IAsyncDisposable
    {
        [Parameter][SupplyParameterFromQuery] public string? ma_ca_thi { get; set; }

        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [Inject] private IJSRuntime Js { get; set; } = default!;

        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private CaThiDto? caThi;
        private List<ChiTietCaThiDto>? chiTietCaThis = [];
        private HubConnection? hubConnection;

        private const string ERROR_PAGE = "Cách hoạt động trang trang web không hợp lệ. Vui lòng quay lại";
        private const string SUCCESS_NOPBAI = "Nộp bài của thí sinh thành công";
        private const string ERROR_NOPBAI = "Nộp bài của thí sinh thất bại";
        private const string ALERT_ADDSV = "Thêm thí sinh được dùng cho việc khẩn cấp. Hãy đảm bảo MSSV thí sinh đã tồn tại trong hệ thống";
        private const string WAITING_DOWNLOADEXCEL = "Đang tải xuống file excel. Hãy chờ trong giây lát";
        private const string ERROR_MOCKTEST = "Không thể xem bài của thí sinh khi thí sinh chưa nộp bài";


        private const string FAILED_RESETLOGIN = "Không thể reset đăng nhập cho thí sinh khi thí sinh không đăng nhập vào hệ thống thi";
        private const string FAILED_CONGGIO = "Không thể cộng giờ cho thí sinh khi thí sinh chưa thi";
        private const string FAILED_NOPBAI = "Không thể bắt thí sinh nộp bài khi thí sinh chưa thi hoặc thí sinh không đăng nhập";

        private const string UPDATE_CA_THI = "Ca thi này đã được cập nhật theo yêu cầu của quản trị viên";
        private const string DELETE_CA_THI = "Ca thi này đã được xóa theo yêu cầu của quản trị viên. Vui lòng rời khỏi trang ...";

        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                caThi = await SessionStorage.GetItemAsync<CaThiDto>("CaThi");
                bool isConvert = int.TryParse(ma_ca_thi, out int maCaThi);
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
            else
                Snackbar.Add(FAILED_RESETLOGIN, Severity.Error);
        }
        private async Task HandleResetLogin(SinhVienDto sinhVien)
        {
            await ResetLoginAPI(sinhVien.MaSinhVien);
        }
        private async Task OnClickCongGioThem(ChiTietCaThiDto chiTietCaThi)
        {
            var result = await OpenCongGioThemDialog(chiTietCaThi);
            if (result != null && !result.Canceled && result.Data != null && chiTietCaThis != null)
            {
                var newChiTietCaThi = (ChiTietCaThiDto)result.Data;
                int index = chiTietCaThis.FindIndex(p => p.MaChiTietCaThi == newChiTietCaThi.MaChiTietCaThi);
                chiTietCaThis[index] = newChiTietCaThi;
            }    
        }

        private async Task<DialogResult?> OpenCongGioThemDialog(ChiTietCaThiDto chiTietCaThi)
        {
            if (chiTietCaThi != null && chiTietCaThi.DaThi == false && chiTietCaThi.MaSinhVienNavigation != null && chiTietCaThi.MaSinhVienNavigation.IsLoggedIn == false)
            {
                Snackbar.Add(FAILED_CONGGIO, Severity.Error);
                return null;
            }
            var parameters = new DialogParameters<CongGioDialog>
            {
                { x => x.chiTietCaThi, chiTietCaThi },
                { x => x.caThi, caThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<CongGioDialog>("Cộng giờ thi", parameters, options);
            return await dialog.Result;
        }
        private async Task OnClickNopBai(ChiTietCaThiDto chiTietCaThi)
        {
            SinhVienDto? sinhVien = chiTietCaThi.MaSinhVienNavigation;
            if ((chiTietCaThi != null && chiTietCaThi.DaThi == false) || sinhVien == null || sinhVien.IsLoggedIn == false)
            {
                Snackbar.Add(FAILED_NOPBAI, Severity.Error);
                return;
            }
            var parameters = new DialogParameters<Simple_Dialog>
                {
                    { x => x.ContentText, $"Nộp bài của thí sinh. Vui lòng chắc chắn thao tác này chỉ thực hiện khi thí sinh đang trong quá trình thi và đăng nhập. Nếu muốn tính điểm, chọn 'Check Điểm'" },
                    { x => x.ButtonText, "Nộp bài" },
                    { x => x.Color, Color.Warning },
                    { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleNopBai(chiTietCaThi))   }
                };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Nộp bài làm thí sinh", parameters, options);

        }
        private async Task HandleNopBai(ChiTietCaThiDto chiTietCaThi)
        {
            SinhVienDto? sinhVien = chiTietCaThi.MaSinhVienNavigation;
            bool result = await NopBaiAPI(sinhVien?.MaSinhVien ?? -1);
            if (result)
                Snackbar.Add(SUCCESS_NOPBAI, Severity.Success);
            else
                Snackbar.Add(ERROR_NOPBAI, Severity.Error);
        }
        private async Task OnClickXemCTBaiThi(ChiTietCaThiDto chiTietCaThi)
        {
            if (chiTietCaThi.Diem == -1 || !chiTietCaThi.DaHoanThanh)
            {
                Snackbar.Add(ERROR_MOCKTEST, Severity.Error);
                return;
            }

            await Js.InvokeVoidAsync("openInNewTab", $"/monitor/mocktest?ma_chi_tiet_ca_thi={chiTietCaThi.MaChiTietCaThi}");
        }

        private async Task Refresh()
        {
            searchString = string.Empty;
            (chiTietCaThis, totalRecords, totalPages) = await ChiTietCaThis_SelectBy_MaCaThi_PagedAPI(caThi?.MaCaThi ?? -1, currentPage, rowsPerPage);
        }
        private async Task OnClickDownloadExcel()
        {
            if (chiTietCaThis != null)
            {
                Snackbar.Add(WAITING_DOWNLOADEXCEL, Severity.Info);
                var excelData = await GetExcelFileAPI(chiTietCaThis);
                if (excelData != null)
                {
                    var base64 = Convert.ToBase64String(excelData);
                    var fileName = $"Bảng điểm ca thi {caThi?.MaCaThi}.xlsx";

                    // Tạo link tải xuống
                    await Js.InvokeVoidAsync("downloadFile", fileName, base64);
                }
            }
        }

        private async Task Start()
        {
            IntializeThoiGianBieu();
            (chiTietCaThis, totalRecords, totalPages) = await ChiTietCaThis_SelectBy_MaCaThi_PagedAPI(caThi?.MaCaThi ?? -1, currentPage, rowsPerPage);
            CreateFakeData();


            await CreateHubConnection();
        }


        private void CreateFakeData()
        {
            if (chiTietCaThis != null && chiTietCaThis.Count != 0)
            {
                int count_fake = totalRecords - chiTietCaThis.Count;
                bool isFake = totalRecords > chiTietCaThis.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        chiTietCaThis.Add(new ChiTietCaThiDto());
                }
            }
        }
        private void PadEmptyRows(List<ChiTietCaThiDto> newChiTietCaThi)
        {
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage * rowsPerPage;
            if (chiTietCaThis != null && chiTietCaThis.Count != 0)
            {
                for (int i = 0; i < newChiTietCaThi.Count; i++)
                {
                    chiTietCaThis[startRow++] = newChiTietCaThi[i];
                }
            }
            StateHasChanged();

        }


        public async ValueTask DisposeAsync()
        {
            await AdminHub.DisconnectionAsync();
        }
    }
}
