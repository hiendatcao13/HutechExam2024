using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Hutech.Exam.Client.Components.Dialogs;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Hutech.Exam.Client.Pages.Admin.ManageCaThi;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam
{
    partial class OrganizeExam
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        private List<DotThiDto>? dotThis = [];
        private List<ChiTietDotThiDto>? chiTietDotThis = [];
        private List<CaThiDto>? caThis = [];
        private HubConnection? hubConnection;

        private const string NO_CHOOSE_OBJECT = "Vui lòng chọn 1 đối tượng để tiếp tục!";
        private const string SUCCESS_DELETE_DOTTHI = "Xóa đợt thi thành công";
        private const string ERROR_DELETE_DOTTHI = "Xóa đợt thi thất bại";
        private const string WAITING_DELETE = "Việc xóa thực thể sẽ tốn thời gian tùy thuộc vào độ phức tạp của dữ liệu. Vui lòng chờ...";
        private const string DELETE_DOTTHI_MESSAGE = "Bạn có chắc chắn muốn xóa đợt thi này không?";
        private const string DELETE_CATHI_MESSAGE = "Bạn có chắc chắn muốn xóa ca thi này không?";
        private const string DELETE_CTDOTTHI_MESSAGE = "Bạn có chắc chắn muốn xóa chi tiết đợt thi này không?";
        private const string SUCCESS_DELETE_CATHI = "Xóa ca thi thành công";
        private const string ERROR_DELETE_CATHI = "Xóa ca thi thất bại";

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
            await CreateHubConnection();
            await GetItemsInSessionStorage();
            await base.OnInitializedAsync();
        }

        private async Task Start()
        {
            dotThis = await DotThis_GetAllAPI();
        }

        private async Task GetItemsInSessionStorage()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataOE>("storedDataOE");
            await Start();
            if (storedData != null)
            {
                selectedDotThi = storedData.DotThi;
                selectedChiTietDotThi = storedData.ChiTietDotThi;
            }
            await FetchAllData();
        }
        private async Task FetchAllData()
        {
            if (selectedDotThi != null && selectedDotThi != null)
            {
                chiTietDotThis = await ChiTietDotThis_SelectBy_MaDotThiAPI(selectedDotThi.MaDotThi);
                if (selectedChiTietDotThi != null)
                {
                    caThis = await CaThis_SelectBy_MaChiTietDotThiAPI(selectedChiTietDotThi.MaChiTietDotThi);
                }
            }
        }

        private async Task OnClickThemDotThi()
        {
            var parameters = new DialogParameters<DotThiDialog> { };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<DotThiDialog>("TẠO ĐỢT THI", parameters, options);
        }
        private async Task OnClickSuaDotThi()
        {
            var parameters = new DialogParameters<DotThiDialog>
            {
                { x => x.DotThi, selectedDotThi },
                { x => x.IsEdit, true }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<DotThiDialog>("SỬA ĐỢT THI", parameters, options);
        }
        private async Task OnClickDeleteDotThi()
        {
            if (selectedDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, DELETE_DOTTHI_MESSAGE },
                { x => x.ButtonText, "Xóa" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDeleteDotThi())   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Simple_Dialog>("Xóa đợt thi", parameters, options);
        }
        private async Task HandleDeleteDotThi()
        {
            Snackbar.Add(WAITING_DELETE, Severity.Warning);
            bool result = await DeleteDotThiAPI(selectedDotThi?.MaDotThi ?? -1);
            if (result)
            {
                Snackbar.Add(SUCCESS_DELETE_DOTTHI, Severity.Success);
                selectedDotThi = null;
                chiTietDotThis = [];
                caThis = [];
            }
            else
                Snackbar.Add(ERROR_DELETE_DOTTHI, Severity.Error);
        }
        private async Task OnClickThemCTDotThi()
        {
            if (selectedDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<CTDotThiDialog>
            {
                { x => x.TenDotThi, selectedDotThi.TenDotThi ?? "Không có DL tên"},
                { x => x.MaDotThi , selectedDotThi.MaDotThi },
                { x => x.IsEdit, false }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<CTDotThiDialog>("THÊM CHI TIẾT ĐỢT THI", parameters, options);
        }
        private async Task OnClickSuaCTDotThi()
        {
            if (selectedDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<CTDotThiDialog>
            {
                { x => x.TenDotThi, selectedDotThi.TenDotThi ?? "Không có DL tên"},
                { x => x.MaDotThi , selectedDotThi.MaDotThi },
                { x => x.IsEdit, true },
                { x => x.ChiTietDotThi, selectedChiTietDotThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<CTDotThiDialog>("SỬA CHI TIẾT ĐỢT THI", parameters, options);
        }
        private async Task OnClickDeleteCTDotThi()
        {
            if (selectedChiTietDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, DELETE_CTDOTTHI_MESSAGE },
                { x => x.ButtonText, "Xóa" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDeleteCTDotThi())   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Simple_Dialog>("Xóa chi tiết đợt thi", parameters, options);
        }
        private async Task HandleDeleteCTDotThi()
        {
            Snackbar.Add(WAITING_DELETE, Severity.Warning);
            bool result = await DeleteCTDotThiAPI(selectedChiTietDotThi?.MaChiTietDotThi ?? -1);
            if (result)
            {
                Snackbar.Add(SUCCESS_DELETE_DOTTHI, Severity.Success);
                selectedChiTietDotThi = null;
                caThis = [];
            }
            else
                Snackbar.Add(ERROR_DELETE_DOTTHI, Severity.Error);
        }
        private async Task OnClickThemCaThi()
        {
            if (selectedChiTietDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<CaThiDialog>
            {
                { x => x.MaChiTietDotThi, selectedChiTietDotThi.MaChiTietDotThi },
                { x => x.TenDotThi, selectedDotThi?.TenDotThi ?? "Không có DL tên"},
                { x => x.TenLopAo , selectedChiTietDotThi.MaLopAoNavigation.TenLopAo },
                { x => x.TenMonThi, selectedChiTietDotThi.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc },
                { x => x.LanThi, selectedChiTietDotThi.LanThi },
                { x => x.IsEdit, false }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<CaThiDialog>("THÊM CA THI", parameters, options);
        }
        private async Task OnClickSuaCaThi(CaThiDto caThi)
        {
            if (selectedChiTietDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<CaThiDialog>
            {
                { x => x.MaChiTietDotThi, selectedChiTietDotThi.MaChiTietDotThi },
                { x => x.TenDotThi, selectedDotThi?.TenDotThi ?? "Không có DL tên"},
                { x => x.TenLopAo , selectedChiTietDotThi.MaLopAoNavigation.TenLopAo },
                { x => x.TenMonThi, selectedChiTietDotThi.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc },
                { x => x.LanThi, selectedChiTietDotThi.LanThi },
                { x => x.IsEdit, true },
                { x => x.CaThi, caThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<CaThiDialog>("SỬA CA THI", parameters, options);
        }
        private async Task OnClickDeleteCaThi(CaThiDto caThi)
        {
            selectedCaThi = caThi;
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, DELETE_CATHI_MESSAGE },
                { x => x.ButtonText, "Xóa" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDeleteCaThi())   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Simple_Dialog>("XÓA CA THI", parameters, options);
        }
        private async Task HandleDeleteCaThi()
        {
            Snackbar.Add(WAITING_DELETE, Severity.Warning);
            bool result = await DeleteCaThiAPI(selectedCaThi?.MaCaThi ?? -1);
            if (result)
            {
                Snackbar.Add(SUCCESS_DELETE_CATHI, Severity.Success);
                selectedCaThi = null;
            }
            else
                Snackbar.Add(ERROR_DELETE_CATHI, Severity.Error);
        }
        private async Task OnClickCapNhatDeThi(CaThiDto caThi)
        {
            var result = await OpenCapNhatDeThiDialog(caThi);
            if (result != null && result.Data != null && !result.Canceled)
            {
                var caThiThayDoi = await CaThi_SelectOneAPI(Convert.ToInt32(result.Data));
                caThi.MaDeThi = caThiThayDoi?.MaDeThi ?? caThi.MaDeThi;
            }
        }
        private async Task OnClickCapNhatSVCaThi()
        {
            if(selectedCaThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<ThemSVExcelDialog>
            {
                { x => x.CaThi, selectedCaThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<ThemSVExcelDialog>("THÊM DANH SÁCH SINH VIÊN", parameters, options);
        }
        private async Task<DialogResult?> OpenCapNhatDeThiDialog(CaThiDto caThi)
        {
            if (selectedChiTietDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return DialogResult.Cancel();
            }
            var parameters = new DialogParameters<CapNhatDeThiDialog>
            {
                { x => x.MaChiTietDotThi, selectedChiTietDotThi.MaChiTietDotThi },
                { x => x.TenDotThi, selectedDotThi?.TenDotThi ?? "Không có DL tên"},
                { x => x.TenLopAo , selectedChiTietDotThi.MaLopAoNavigation.TenLopAo },
                { x => x.TenMonThi, selectedChiTietDotThi.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc },
                { x => x.LanThi, selectedChiTietDotThi.LanThi },
                { x => x.CaThi, caThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<CapNhatDeThiDialog>("UPDATE ĐỀ THI", parameters, options);
            return await dialog.Result;
        }



        private async Task OnClickShowChiTietCaThi(CaThiDto caThi)
        {
            await SaveData();
            // set Ca Thi cho trang EM, ko tốn API lấy lại
            await SessionStorage.SetItemAsync("CaThi", caThi);
            // set cho trang Manage Exam 
            await SetItemsInSessionStorage();
            Nav.NavigateTo($"admin/monitor?ma_ca_thi={caThi.MaCaThi}");
        }




        private async Task SaveData()
        {
            var selectedData = new StoredDataOE
            {
                DotThi = selectedDotThi,
                ChiTietDotThi = selectedChiTietDotThi
            };
            await SessionStorage.SetItemAsync("storedDataOE", selectedData);
        }
        private async Task SetItemsInSessionStorage()
        {
            var selectedData = new StoredDataMC
            {
                DotThi = selectedDotThi,
                MonHoc = selectedChiTietDotThi?.MaLopAoNavigation.MaMonHocNavigation,
                LopAo = selectedChiTietDotThi?.MaLopAoNavigation,
                LanThi = selectedChiTietDotThi?.LanThi
            };
            await SessionStorage.SetItemAsync("storedDataEM", selectedData);
        }
    }
}
