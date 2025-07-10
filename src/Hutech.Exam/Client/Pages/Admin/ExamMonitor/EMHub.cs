using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor
{
    public partial class ExamMonitor
    {
        private async Task CreateHubConnectionAsync()
        {
            hubConnection = await AdminHub.GetConnectionAsync();
            if (examSessionDetails != null)
            {
                hubConnection.On<long, bool, DateTime>("SV_Authentication", (ma_sinh_vien, isLogin, thoi_gian) =>
                {
                    if (examSessionDetails.Exists(p => p.MaSinhVien == ma_sinh_vien))
                    {
                        CallLoadUpdateSVAuthentication(ma_sinh_vien, isLogin, thoi_gian);
                        StateHasChanged();
                    }
                });

                hubConnection.On<int, bool, DateTime, double>("ChangeCTCaThi_SVThi", (ma_chi_tiet_ca_thi, isBDThi, thoi_gian, diem) =>
                {
                    if (examSessionDetails.Exists(p => p.MaChiTietCaThi == ma_chi_tiet_ca_thi))
                    {
                        CallLoadUpdateCTCaThi(ma_chi_tiet_ca_thi, isBDThi, thoi_gian, diem);
                        StateHasChanged();
                    }
                });

                hubConnection.On<int>("UpdateCaThi", async (ma_ca_thi) =>
                {
                    if(examSession != null && examSession.MaCaThi == ma_ca_thi)
                    {
                        await CallLoadUpdateCaThiAsync(ma_ca_thi);
                        StateHasChanged();
                    }
                });
                hubConnection.On<int>("DeleteCaThi", async (ma_ca_thi) =>
                {
                    if(examSession != null && examSession.MaCaThi == ma_ca_thi)
                    {
                        await CallLoadDeleteCaThiAsync();
                    }
                });

                //1 số thành phần khác không thuộc ở trang này
            }
        }
        private void CallLoadUpdateCTCaThi(int ma_chi_tiet_ca_thi, bool isBDThi, DateTime thoi_gian, double diem)
        {
            ChiTietCaThiDto? existingCTCaThi = examSessionDetails?.FirstOrDefault(p => p.MaChiTietCaThi == ma_chi_tiet_ca_thi);
            if(existingCTCaThi != null)
            {
                if (isBDThi)
                {
                    existingCTCaThi.DaThi = true;
                    existingCTCaThi.ThoiGianBatDau = thoi_gian;
                }
                else
                {
                    existingCTCaThi.DaHoanThanh = true;
                    existingCTCaThi.ThoiGianKetThuc = thoi_gian;
                    existingCTCaThi.Diem = diem;
                }
            }
        }
        private void CallLoadUpdateSVAuthentication(long ma_sinh_vien, bool isLogin, DateTime thoi_gian)
        {
            SinhVienDto? exsistingSV = examSessionDetails?.FirstOrDefault(p => p.MaSinhVien == ma_sinh_vien)?.MaSinhVienNavigation;
            if(exsistingSV != null)
            {
                if (isLogin)
                {
                    exsistingSV.IsLoggedIn = true;
                    exsistingSV.LastLoggedIn = thoi_gian;
                }
                else
                {
                    exsistingSV.IsLoggedIn = false;
                    exsistingSV.LastLoggedOut = thoi_gian;
                }
            }
        }

        private async Task CallLoadUpdateCaThiAsync(int ma_ca_thi)
        {
            Snackbar.Add(UPDATE_CA_THI, MudBlazor.Severity.Info);
            examSession = await ExamSession_SelectOneAPI(ma_ca_thi);
        }

        private async Task CallLoadDeleteCaThiAsync()
        {
            // xóa ca thi hiện tại, yêu cầu back lại trang web
            Snackbar.Add(DELETE_CA_THI, MudBlazor.Severity.Warning);
            examSession = null;
            await SessionStorage.RemoveItemAsync("CaThi");
            // Viết trang main để hướng dẫn người dùng
            Nav.NavigateTo("/admin/control");
        }

    }
}
