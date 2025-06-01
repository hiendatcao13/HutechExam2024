using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using MudBlazor;
using System.Globalization;
using System.Text;
using Hutech.Exam.Client.Pages.Admin.ManageCaThi.Dialog;
using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    public partial class ManageCaThi
    {
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private ChiTietDotThiDto? chiTietDotThi;

        private List<CaThiDto>? caThis;

        private List<DotThiDto>? dotThis; // combobox

        private List<MonHocDto>? monHocs; // combobox

        private List<LopAoDto>? lopAos; // combobox

        private readonly List<int> lanThis = [1, 2, 3, 4, 5];

        private bool isFristRender = false; // biến để tạo fake data


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


        private async Task FetchAllCaThi()
        {
            if (selectedDotThi != null && selectedLopAo != null && selectedLanThi != 0 && selectedMonHoc != null)
            {
                chiTietDotThi = await ChiTietDotThis_SelectBy_MaDotThi_MaLopAo_LanThiAPI(selectedDotThi.MaDotThi, selectedLopAo.MaLopAo, selectedLanThi);
                if (chiTietDotThi != null)
                {
                    (caThis, totalPages, totalRecords) = await CaThis_SelectBy_MaChiTietDotThi_PagedAPI(chiTietDotThi.MaChiTietDotThi, currentPage, rowsPerPage);
                    CreateFakeData();

                    await SetItemsInSessionStorage();
                }
            }
            else
                caThis = [];
        }

        private async Task SetItemsInSessionStorage()
        {
            var selectedData = new StoredDataMC
            {
                DotThi = selectedDotThi,
                MonHoc = selectedMonHoc,
                LopAo = selectedLopAo,
                LanThi = selectedLanThi
            };
            await SessionStorage.SetItemAsync("storedDataMC", selectedData);
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
            dotThis = await DotThis_GetAllAPI();
            monHocs = await MonHocs_GetAllAPI();

            await GetItemsInSessionStorage();

            //await CreateHubConnection();
        }
        private async Task<DialogResult?> OpenTinhTrangCaThiDialog(CaThiDto caThi)
        {
            var parameters = new DialogParameters<TinhTrangCaThiDialog>
            {
                { x => x.CaThi, caThi },
            };

            var options = new DialogOptions { CloseButton = false, MaxWidth = MaxWidth.ExtraExtraLarge, BackgroundClass = "my-custom-class" };

            var dialog = await Dialog.ShowAsync<TinhTrangCaThiDialog>("THÔNG TIN CA THI", parameters, options);
            return await dialog.Result;
        }

        private async Task OnClickSuaCaThi(CaThiDto caThi)
        {
            var result = await OpenTinhTrangCaThiDialog(caThi);


            if (result != null && !result.Canceled && result.Data != null)
            {
                UpdateListCaThi((CaThiDto)result.Data);
            }
        }

        private async Task OnClickChiTietCaThi(CaThiDto caThi)
        {
            await SessionStorage.SetItemAsync("CaThi", caThi);

            Nav.NavigateTo($"admin/monitor?ma_ca_thi={caThi.MaCaThi}");

        }

        private void UpdateListCaThi(CaThiDto caThi)
        {
            if (caThi == null || caThis == null) return;

            var index = caThis.FindIndex(p => p.MaCaThi == caThi.MaCaThi);
            if (index != -1)
            {
                caThis[index] = caThi;
            }
        }

        private void CreateFakeData()
        {
            if (caThis != null && caThis.Count != 0)
            {
                int count_fake = totalRecords - caThis.Count;
                bool isFake = totalRecords > caThis.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        caThis.Add(new CaThiDto());
                }
            }
        }

        private void PadEmptyRows(List<CaThiDto>? newCaThi)
        {
            if (newCaThi == null || newCaThi.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage * rowsPerPage;
            if (caThis != null && caThis.Count != 0)
            {
                for (int i = 0; i < newCaThi.Count; i++)
                {

                    caThis[startRow++] = newCaThi[i];
                }
            }
            StateHasChanged();

        }

        private async Task OnClickXoaCaThi(int ma_ca_thi)
        {
            await Http.DeleteAsync($"api/Admin/DeleteCaThi?ma_ca_thi={ma_ca_thi}");
        }
    }
}
