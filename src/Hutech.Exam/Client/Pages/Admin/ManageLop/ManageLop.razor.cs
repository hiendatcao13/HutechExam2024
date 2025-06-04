using Hutech.Exam.Client.API;
using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.ManageLop.Dialog;
using Hutech.Exam.Client.Components.Dialogs;

namespace Hutech.Exam.Client.Pages.Admin.ManageLop
{
    public partial class ManageLop
    {

        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        
        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private List<KhoaDto>? Khoas { get; set; } = [];

        private List<LopDto>? Lops { get; set; } = [];

        private List<SinhVienDto>? SinhViens { get; set; } = [];

        private const string NO_SELECT_KHOA = "Vui lòng chọn khoa trước";

        private const string NO_SELECT_LOP = "Vui lòng chọn lớp trước";

        private const string NO_SELECT_SINHVIEN = "Vui lòng chọn sinh viên trước";

        private const string DELETE_KHOA_MESSAGE = "Bạn có chắc chắn muốn xóa khoa này. Các lớp và toàn bộ sinh viên thuộc khoa này sẽ bị xóa?";
        private const string DELETE_LOP_MESSAGE = "Bạn có chắc chắn muốn xóa lớp này. Tất cả sinh viên thuộc lớp này sẽ bị xóa?";
        private const string DELETE_SINHVIEN_MESSAGE = "Bạn có chắc chắn muốn xóa sinh viên này?";

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
            await GetItemsInSessionStorage();
            await base.OnInitializedAsync();
        }

        private async Task Start()
        {
            // lấy danh sách khoa
            (Khoas, totalPages_Khoa, totalRecords_Khoa) = await Khoas_GetAll_PagedAPI(currentPage_Khoa, rowsPerPage_Khoa);
            CreateFakeData_Khoa();
        }


        private async Task Filter()
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                (SinhViens, totalRecords_SinhVien, totalPages_SinhVien) = await SinhViens_SelectBy_MaLop_Search_PagedAPI(selectedLop?.MaLop ?? -1, searchString, currentPage_SinhVien, rowsPerPage_SinhVien);
                CreateFakeData_SinhVien();
            }
            else
            {
                (SinhViens, totalRecords_SinhVien, totalPages_SinhVien) = await SinhViens_SelectBy_MaLop_PagedAPI(selectedLop?.MaLop ?? -1, currentPage_SinhVien, rowsPerPage_SinhVien);
                CreateFakeData_SinhVien();
            }
        }


        private async Task GetItemsInSessionStorage()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataML>("storedDataML");

            if (storedData != null)
            {
                selectedKhoa = storedData.Khoa;
                selectedLop = storedData.Lop;
            }
            if(selectedKhoa != null)
            {
                await FetchLops();
                await FetchSinhViens();
            }    
        }


        private async Task FetchLops()
        {
            if (selectedKhoa != null)
            {
                (Lops, totalPages_Lop, totalRecords_Lop) = await Lops_SelectBy_ma_khoa_PagedAPI(selectedKhoa.MaKhoa, currentPage_Lop, rowsPerPage_Lop);
                CreateFakeData_Lop();
            }
        }

        private async Task FetchSinhViens()
        {
            if (selectedLop != null)
            {
                (SinhViens, totalPages_SinhVien, totalRecords_SinhVien) = await SinhViens_SelectBy_MaLop_PagedAPI(selectedLop.MaLop, currentPage_SinhVien, rowsPerPage_SinhVien);
                CreateFakeData_SinhVien();
            }
        }

        private async Task OnClickThemKhoa()
        {
            var result = await OpenKhoaDialog(false, null);

            if(result != null && !result.Canceled && result.Data is KhoaDto newKhoa)
            {
                Khoas?.Insert(0, newKhoa);
            }
        }

        private async Task OnClickSuaKhoa()
        {
            if(selectedKhoa == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }
            var result = await OpenKhoaDialog(true, selectedKhoa);

            if (result != null && !result.Canceled && result.Data is KhoaDto newKhoa && Khoas != null)
            {
                int index = Khoas.FindIndex(k => k.MaKhoa == newKhoa.MaKhoa);
                if(index != -1)
                {
                    Khoas[index] = newKhoa;
                }    
            }
        }

        private async Task OnClickXoaKhoa()
        {
            if(selectedKhoa == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, DELETE_KHOA_MESSAGE },
                { x => x.ButtonText, "Xóa" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDeleteKhoa())   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Simple_Dialog>("XÓA KHOA", parameters, options);
                
        }

        private async Task HandleDeleteKhoa()
        {
            if(selectedKhoa != null)
            {
                var result = await Khoa_Delete(selectedKhoa.MaKhoa);

                if (result)
                {
                    Khoas?.Remove(selectedKhoa);
                    selectedKhoa = null;
                }
            }    
        }

        private async Task OnClickThemLop()
        {
            if (selectedKhoa == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }
            var result = await OpenLopDialog(false, selectedKhoa, null);

            if (result != null && !result.Canceled && result.Data is LopDto newLop && Lops != null)
            {
                Lops?.Insert(0, newLop);
            }
        }

        private async Task OnClickSuaLop()
        {
            if (selectedKhoa == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }
            if (selectedLop == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            var result = await OpenLopDialog(true, selectedKhoa, selectedLop);

            if (result != null && !result.Canceled && result.Data is LopDto newLop && Lops != null)
            {
                int index = Lops.FindIndex(l => l.MaLop == newLop.MaLop);
                if(index != -1)
                {
                    Lops[index] = newLop;
                }    
            }
        }

        private async Task OnClickXoaLop()
        {
            if(selectedLop == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            if (selectedKhoa == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, DELETE_LOP_MESSAGE },
                { x => x.ButtonText, "Xóa" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDeleteLop())   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Simple_Dialog>("XÓA LỚP", parameters, options);


              
        }

        private async Task HandleDeleteLop()
        {
            if(selectedLop != null)
            {
                var result = await Lop_Delete(selectedLop.MaLop);

                if (result)
                {
                    Lops?.Remove(selectedLop);
                    selectedLop = null;
                }
            }    
        }

        private async Task OnClickThemSinhVien()
        {
            if(selectedLop == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            var result = await OpenSinhVienDialog(false, selectedLop, null);

            if (result != null && !result.Canceled && result.Data is SinhVienDto newSinhVien && SinhViens != null)
            {
                SinhViens.Insert(0, newSinhVien);
            }
        }

        private async Task OnClickSuaSinhVien()
        {
            if (selectedLop == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }
            if (selectedSinhVien == null)
            {
                Snackbar.Add(NO_SELECT_SINHVIEN, Severity.Warning);
                return;
            }    

            var result = await OpenSinhVienDialog(true, selectedLop, selectedSinhVien);

            if (result != null && !result.Canceled && result.Data is SinhVienDto newSinhVien && SinhViens != null)
            {
                int index = SinhViens.FindIndex(sv => sv.MaSinhVien == newSinhVien.MaSinhVien);
                if(index != -1)
                {
                    SinhViens[index] = newSinhVien;
                }    
            }
        }

        private async Task OnClickXoaSinhVien()
        {
            if(selectedSinhVien == null)
            {
                Snackbar.Add(NO_SELECT_SINHVIEN, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, DELETE_SINHVIEN_MESSAGE },
                { x => x.ButtonText, "Xóa" },
                { x => x.Color, Color.Error },
                { x => x.onHandleSubmit, EventCallback.Factory.Create(this, async () => await HandleDeleteSinhVien())   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Simple_Dialog>("XÓA SINH VIÊN", parameters, options);
        }

        private async Task HandleDeleteSinhVien()
        {
            if(selectedSinhVien != null)
            {
                var result = await SinhVien_Delete(selectedSinhVien.MaSinhVien);
                if (result)
                {
                    SinhViens?.Remove(selectedSinhVien);
                    selectedSinhVien = null;
                }
            }    
        }

        private async Task<DialogResult?> OpenKhoaDialog(bool isEdit, KhoaDto? khoa)
        {
            var parameters = new DialogParameters<KhoaDialog>
            {
                { x => x.Khoa, khoa },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<KhoaDialog>((isEdit) ? "SỬA KHOA" : "THÊM KHOA", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenLopDialog(bool isEdit, KhoaDto? khoa, LopDto? lop)
        {
            var parameters = new DialogParameters<LopDialog>
            {
                { x => x.Khoa, khoa },
                { x => x.Lop, lop },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<LopDialog>((isEdit) ? "SỬA LỚP" : "THÊM LỚP", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenSinhVienDialog(bool isEdit, LopDto? lop, SinhVienDto? sinhVien)
        {
            var parameters = new DialogParameters<SinhVienDialog>
            {
                { x => x.SinhVien, sinhVien },
                { x => x.Lop, lop },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<SinhVienDialog>((isEdit) ? "SỬA SINH VIÊN" : "THÊM SINH VIÊN", parameters, options);
            return await dialog.Result;
        }

        private async Task SaveData()
        {
            var selectedData = new StoredDataML
            {
                Khoa = selectedKhoa,
                Lop = selectedLop
            };
            await SessionStorage.SetItemAsync("storedDataML", selectedData);
        }



        private void PadEmptyRows(List<KhoaDto>? newKhoas)
        {
            if (newKhoas == null || newKhoas.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Khoa * rowsPerPage_Khoa;
            if (Khoas != null && Khoas.Count != 0)
            {
                for (int i = 0; i < newKhoas.Count; i++)
                {

                    Khoas[startRow++] = newKhoas[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<LopDto>? newLops)
        {
            if (newLops == null || newLops.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Lop * rowsPerPage_Lop;
            if (Lops != null && Lops.Count != 0)
            {
                for (int i = 0; i < newLops.Count; i++)
                {

                    Lops[startRow++] = newLops[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<SinhVienDto>? newSinhViens)
        {
            if (newSinhViens == null || newSinhViens.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_SinhVien * rowsPerPage_SinhVien;
            if (SinhViens != null && SinhViens.Count != 0)
            {
                for (int i = 0; i < newSinhViens.Count; i++)
                {

                    SinhViens[startRow++] = newSinhViens[i];
                }
            }
            StateHasChanged();
        }

        private void CreateFakeData_Khoa()
        {
            if (Khoas != null && Khoas.Count != 0)
            {
                int count_fake = totalRecords_Khoa - Khoas.Count;
                bool isFake = totalRecords_Khoa > Khoas.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        Khoas.Add(new KhoaDto());
                }
            }
        }

        private void CreateFakeData_Lop()
        {
            if (Lops != null && Lops.Count != 0)
            {
                int count_fake = totalRecords_Lop - Lops.Count;
                bool isFake = totalRecords_Lop > Lops.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        Lops.Add(new LopDto());
                }
            }
        }

        private void CreateFakeData_SinhVien()
        {
            if (SinhViens != null && SinhViens.Count != 0)
            {
                int count_fake = totalRecords_SinhVien - SinhViens.Count;
                bool isFake = totalRecords_SinhVien > SinhViens.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        SinhViens.Add(new SinhVienDto());
                }
            }
        }
    }
}
