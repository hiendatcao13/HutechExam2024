using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;
using Microsoft.AspNetCore.SignalR.Client;
using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Client.Pages.Admin.ManageCaThi;
using Hutech.Exam.Client.API;
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

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private List<DotThiDto>? dotThis = [];
        private List<ChiTietDotThiDto>? chiTietDotThis = [];
        private List<CaThiDto>? caThis = [];
        private HubConnection? hubConnection;

        private const string NO_CHOOSE_OBJECT = "Vui lòng chọn 1 đối tượng để tiếp tục!";
        private const string WAITING_DELETE = "Việc xóa thực thể sẽ tốn thời gian tùy thuộc vào độ phức tạp của dữ liệu. Vui lòng chờ...";
        private const string DELETE_DOTTHI_MESSAGE = "Bạn có chắc chắn muốn xóa đợt thi này không? Mối quan hệ phụ thuộc: CHITIETDOTTHI &rarr; CATHI &rarr; CHITIETCATHI &rarr; CHITIETBAITHI";
        private const string DELETE_CATHI_MESSAGE = "Bạn có chắc chắn muốn xóa ca thi này không? Mối quan hệ phụ thuộc: CHITIETCATHI &rarr; CHITIETBAITHI";
        private const string DELETE_CTDOTTHI_MESSAGE = "Bạn có chắc chắn muốn xóa chi tiết đợt thi này không? Mối quan hệ phụ thuộc: CATHI &rarr; CHITIETCATHI &rarr; CHITIETBAITHI";

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
            (dotThis, totalPages_DT, totalRecords_DT) = await DotThis_GetAllAPI(currentPage_DT, rowsPerPage_DT);
            CreateFakeData_DT();
        }

        private async Task FetchCaThis()
        {
            if (selectedChiTietDotThi != null)
            {
                (caThis, totalPages_CT, totalRecords_CT) = await CaThis_SelectBy_MaChiTietDotThi_PagedAPI(selectedChiTietDotThi.MaChiTietDotThi, currentPage_CT, rowsPerPage_CT);
                CreateFakeData_CT();
            }
        }

        private async Task FetchCTDotThi()
        {
            if (selectedDotThi != null)
            {
                (chiTietDotThis, totalPages_CTDT, totalRecords_CTDT) = await ChiTietDotThis_SelectBy_MaDotThi_PagedAPI(selectedDotThi.MaDotThi, currentPage_CTDT, rowsPerPage_CTDT);
                CreateFakeData_CTDT();
            }
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
            if (selectedDotThi != null)
            {
                await FetchCTDotThi();
                await FetchCaThis();
            }
        }

        private async Task OnClickThemDotThi()
        {
            var result = await OpenDotThiDialog(false);

            if (result != null && !result.Canceled && result.Data != null)
            {
                var newDotThi = (DotThiDto)result.Data;
                dotThis?.Insert(0, newDotThi); // Thêm vào đầu danh sách
            }    
        }


        private async Task OnClickSuaDotThi()
        {
            var result = await OpenDotThiDialog(true);

            if(result != null && !result.Canceled && result.Data != null && dotThis != null)
            {
                var newDotThi = (DotThiDto)result.Data;

                int index = dotThis.FindIndex(dt => dt.MaDotThi == newDotThi.MaDotThi);
                if(index != -1)
                {
                    dotThis[index] = newDotThi; // Cập nhật đợt thi trong danh sách
                    selectedDotThi = dotThis[index];
                }    
            }    
        }

        private async Task<DialogResult?> OpenDotThiDialog(bool isEdit)
        {
            var parameters = new DialogParameters<DotThiDialog>
            {
                { x => x.DotThi, selectedDotThi ?? new() },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<DotThiDialog>((isEdit) ? "SỬA ĐỢT THI" : "THÊM ĐỢT THI", parameters, options);
            return await dialog.Result;
        }
        private async Task OnClickDeleteDotThi()
        {
            if (selectedDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_DOTTHI_MESSAGE },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteDotThi(true))   },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteDotThi(false))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA ĐỢT THI", parameters, options);
        }

        private async Task HandleDeleteDotThi(bool isForce)
        {
            bool result = (isForce) ? await ForceDeleteDotThiAPI(selectedDotThi?.MaDotThi ?? -1) : await DeleteDotThiAPI(selectedDotThi?.MaDotThi ?? -1);
            if (result && selectedDotThi != null)
            {
                Snackbar.Add(WAITING_DELETE, Severity.Warning);
                dotThis?.Remove(selectedDotThi);
                selectedDotThi = null;
                chiTietDotThis = [];
                caThis = [];
            }
        }

        private async Task OnClickThemCTDotThi()
        {
            var result = await OpenChiTietDotThiDialog(false);

            if(result != null && !result.Canceled && result.Data != null && chiTietDotThis != null)
            {
                var newChiTietDotThi = (ChiTietDotThiDto)result.Data;
                chiTietDotThis.Insert(0, newChiTietDotThi); // Thêm vào đầu danh sách
            }    
        }
        private async Task OnClickSuaCTDotThi()
        {
            var result = await OpenChiTietDotThiDialog(true);

            if (result != null && !result.Canceled && result.Data != null && chiTietDotThis != null)
            {
                var newChiTietDotThi = (ChiTietDotThiDto)result.Data;
                int index = chiTietDotThis.FindIndex(ct => ct.MaChiTietDotThi == newChiTietDotThi.MaChiTietDotThi);
                if(index != -1)
                {
                    chiTietDotThis[index] = newChiTietDotThi; // Cập nhật chi tiết đợt thi trong danh sách
                    selectedChiTietDotThi = chiTietDotThis[index];
                }
            }
        }

        private async Task<DialogResult?> OpenChiTietDotThiDialog(bool isEdit)
        {
            if (selectedDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return null;
            }
            var parameters = new DialogParameters<CTDotThiDialog>
            {
                { x => x.TenDotThi, selectedDotThi.TenDotThi ?? "Không có DL tên"},
                { x => x.MaDotThi , selectedDotThi.MaDotThi },
                { x => x.IsEdit, isEdit },
                { x => x.ChiTietDotThi, selectedChiTietDotThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<CTDotThiDialog>((isEdit) ? "SỬA CHI TIẾT ĐỢT THI": "THÊM CHI TIẾT ĐỢT THI", parameters, options);
            return await dialog.Result;
        }

        private async Task OnClickDeleteCTDotThi()
        {
            if (selectedChiTietDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }
            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_CTDOTTHI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteCTDotThi(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteCTDotThi(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CHI TIẾT ĐỢT THI", parameters, options);
        }
        private async Task HandleDeleteCTDotThi(bool isForce)
        {
            bool result = (isForce) ? await ForceDeleteCTDotThiAPI(selectedChiTietDotThi?.MaChiTietDotThi ?? -1) : await DeleteCTDotThiAPI(selectedChiTietDotThi?.MaChiTietDotThi ?? -1);
            if (result && selectedChiTietDotThi != null)
            {
                Snackbar.Add(WAITING_DELETE, Severity.Warning);
                chiTietDotThis?.Remove(selectedChiTietDotThi);
                selectedChiTietDotThi = null;
                caThis = [];
            }
        }
        private async Task OnClickThemCaThi()
        {
            var result = await OpenCaThiDialog(false);
            if(result != null && !result.Canceled && result.Data != null && caThis != null)
            {
                var newCaThi = (CaThiDto)result.Data;
                caThis.Insert(0, newCaThi); // Thêm vào cuối danh sách
            }    
        }
        private async Task OnClickSuaCaThi()
        {
            if (selectedCaThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }

            var result = await OpenCaThiDialog(true);
            if (result != null && !result.Canceled && result.Data != null && caThis != null)
            {
                var newCaThi = (CaThiDto)result.Data;
                int index = caThis.FindIndex(ct => ct.MaCaThi == newCaThi.MaCaThi);
                if (index != -1)
                {
                    caThis[index] = newCaThi; // Cập nhật ca thi trong danh sách
                    selectedCaThi = caThis[index];
                }
            }
        }

        private async Task<DialogResult?> OpenCaThiDialog(bool isEdit)
        {
            if (selectedChiTietDotThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return null;
            }
            var parameters = new DialogParameters<CaThiDialog>
            {
                { x => x.MaChiTietDotThi, selectedChiTietDotThi.MaChiTietDotThi },
                { x => x.TenDotThi, selectedDotThi?.TenDotThi ?? "Không có DL tên"},
                { x => x.TenLopAo , selectedChiTietDotThi.MaLopAoNavigation.TenLopAo },
                { x => x.TenMonThi, selectedChiTietDotThi.MaLopAoNavigation.MaMonHocNavigation?.TenMonHoc },
                { x => x.LanThi, selectedChiTietDotThi.LanThi },
                { x => x.IsEdit, isEdit },
                { x => x.CaThi, selectedCaThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<CaThiDialog>((isEdit) ? "SỬA CA THI" : "THÊM CA THI", parameters, options);
            return await dialog.Result;
        }

        private async Task OnClickDeleteCaThi()
        {
            if (selectedCaThi == null)
            {
                Snackbar.Add(NO_CHOOSE_OBJECT, Severity.Info);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_CATHI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteCaThi(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteCaThi(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CA THI", parameters, options);
        }

        private async Task HandleDeleteCaThi(bool isForce)
        {
            bool result = (isForce) ? await ForceDeleteCaThiAPI(selectedCaThi?.MaCaThi ?? -1) : await DeleteCaThiAPI(selectedCaThi?.MaCaThi ?? -1);
            if (result && selectedCaThi != null)
            {
                Snackbar.Add(WAITING_DELETE, Severity.Warning);
                caThis?.Remove(selectedCaThi);
                selectedCaThi = null;
            }
        }

        private async Task OnClickCapNhatDeThi(CaThiDto caThi)
        {
            var result = await OpenCapNhatDeThiDialog(caThi);
            if (result != null && result.Data != null && !result.Canceled && caThis != null)
            {
                var newCaThi = (CaThiDto)result.Data;
                int index = caThis.FindIndex(ct => ct.MaCaThi == newCaThi.MaCaThi);
                if(index != -1)
                {
                    caThis[index] = newCaThi;
                }    
            }
        }

        private async Task OnClickCapNhatSVCaThi()
        {

            var parameters = new DialogParameters<ThemSVCaThiExcelDialog>{ };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<ThemSVCaThiExcelDialog>("THÊM DANH SÁCH SINH VIÊN VÀO CA THI", parameters, options);
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
                LanThi = selectedChiTietDotThi?.LanThi ?? 0
            };
            await SessionStorage.SetItemAsync("storedDataEM", selectedData);
        }

        private void PadEmptyRows(List<DotThiDto>? newDotThis)
        {
            if (newDotThis == null || newDotThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_DT * rowsPerPage_DT;
            if (dotThis != null && dotThis.Count != 0)
            {
                for (int i = 0; i < newDotThis.Count; i++)
                {

                    dotThis[startRow++] = newDotThis[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<ChiTietDotThiDto>? newChiTietDotThis)
        {
            if (newChiTietDotThis == null || newChiTietDotThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_CTDT * rowsPerPage_CTDT;
            if (chiTietDotThis != null && chiTietDotThis.Count != 0)
            {
                for (int i = 0; i < newChiTietDotThis.Count; i++)
                {

                    chiTietDotThis[startRow++] = newChiTietDotThis[i];
                }
            }
            StateHasChanged();

        }

        private void PadEmptyRows(List<CaThiDto>? newCaThis)
        {
            if (newCaThis == null || newCaThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_CT * rowsPerPage_CT;
            if (caThis != null && caThis.Count != 0)
            {
                for (int i = 0; i < newCaThis.Count; i++)
                {

                    caThis[startRow++] = newCaThis[i];
                }
            }
            StateHasChanged();

        }

        private void CreateFakeData_DT()
        {
            if (dotThis != null && dotThis.Count != 0)
            {
                int count_fake = totalRecords_DT - dotThis.Count;
                bool isFake = totalRecords_DT > dotThis.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        dotThis.Add(new DotThiDto());
                }
            }
        }

        private void CreateFakeData_CTDT()
        {
            if (chiTietDotThis != null && chiTietDotThis.Count != 0)
            {
                int count_fake = totalRecords_CTDT - chiTietDotThis.Count;
                bool isFake = totalRecords_CTDT > chiTietDotThis.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        chiTietDotThis.Add(new ChiTietDotThiDto());
                }
            }
        }

        private void CreateFakeData_CT()
        {
            if (caThis != null && caThis.Count != 0)
            {
                int count_fake = totalRecords_CT - caThis.Count;
                bool isFake = totalRecords_CT > caThis.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        caThis.Add(new CaThiDto());
                }
            }
        }
    }
}
