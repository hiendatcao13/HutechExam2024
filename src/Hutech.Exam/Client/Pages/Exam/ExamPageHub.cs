﻿using Hutech.Exam.Client.Authentication;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage
    {
        private async Task CreateHubConnection()
        {
            _hubConnection = await StudentHub.GetConnectionAsync(MyData.SinhVien.MaSinhVien);

            // trường hợp thay đổi tình trạng ca thi
            _hubConnection.On("ChangeStatusCaThi", () =>
            {
                CallLoadThayDoiTinhTrangCaThi();
            });
            _hubConnection.On("ResetLogin", async () =>
            {
                await CallLoadDangXuat();
            });
            _hubConnection.On("SubmitExam", async () =>
            {
                Snackbar.Add(ADMIN_NOP_BAI, MudBlazor.Severity.Info);
                await KetThucThoiGianLamBai();
            });

            //tham gia vào group mã ca thi
            await _hubConnection.InvokeAsync("JoinGroupMaCaThi", CaThi?.MaCaThi ?? -1);
        }

        private async Task HandleDangXuat()
        {
            if (await UpdateLogoutAPI(MyData.SinhVien.MaSinhVien))
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(null);
                Nav?.NavigateTo("/", true);
            }
            Snackbar.Add("Có lỗi xảy ra khi đăng xuất tài khoản", MudBlazor.Severity.Error);
        }
        private void CallLoadThayDoiTinhTrangCaThi()
        {
            Snackbar.Add(DONG_BANG_CA_THI, MudBlazor.Severity.Warning);
            Nav.NavigateTo("/info");
        }
        private async Task CallLoadDangXuat()
        {
            Snackbar.Add(RESET_LOGIN, MudBlazor.Severity.Warning);
            await HandleDangXuat();
        }
    }
}
