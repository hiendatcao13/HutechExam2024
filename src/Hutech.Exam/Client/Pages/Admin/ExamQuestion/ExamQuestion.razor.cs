using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using AutoMapper;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.ExamQuestion.Dialog;
using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Pages.Admin.ManageLop;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;
using System.Runtime.CompilerServices;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public partial class ExamQuestion
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IMapper Mapper { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        List<MonHocDto>? monHocs = [];
        MonHocDto? selectedMonHoc;

        List<DeThiDto>? deThis = [];
        DeThiDto? selectedDeThi;

        List<NhomCauHoiDto>? nhomCauHois = []; // ds các nhóm câu hỏi gốc
        List<CustomNhomCauHoi>? customNhomCauHois = [];
        CustomNhomCauHoi? selectedNhomCauHoi;

        List<CauHoiDto>? cauHois = [];


        private const string WARNING_ALREADY_DETHIHOANVI = "Đề thi này đã tạo ra các đề hoán vị. Việc thao tác thêm sẽ không ảnh hưởng đến các bộ đề hoán vị, sửa hoặc xóa nội dung câu hỏi, mã nhóm gốc vẫn được thực hiện.";
        private const string NOT_SELECT_OBJECT = "Vui lòng chọn đối tượng cần thao tác";

        private const string DELETE_MONHOC_MESSAGE = "Bạn có chắc chắn muốn xóa môn học này không? Mối quan hệ phụ thuộc: CLO &rarr; DETHI &rarr; NHOMCAUHOI &rarr; CAUHOI &rarr; CAUTRALOI &rarr; DETHIHV &rarr; " +
            "NHOMCAUHOIHV &rarr; CHITIETDETHIHV, LOPAO &rarr; CHITIETDOTTHI &rarr; CATHI &rarr; CHITIETCATHI &rarr; CHITIETBAITHI";

        private const string DELETE_DETHI_MESSAGE = "Bạn có chắc chắn muốn xóa đề thi này không? Mối quan hệ phụ thuộc: NHOMCAUHOI &rarr; CAUHOI &rarr; CAUTRALOI &rarr; DETHIHV &rarr; NHOMCAUHOIHV &rarr; CHITIETDETHIHV";

        private const string DELETE_NHOMCAUHOI_MESSAGE = "Bạn có chắc chắn muốn xóa nhóm câu hỏi này không? Mối quan hệ phụ thuộc: CHITIETDETHIHV &rarr; NHOMCAUHOIHV, CAUHOI &rarr; CAUTRALOI";

        private const string DELETE_CAUHOI_MESSAGE = "Bạn có chắc chắn muốn xóa câu hỏi này không? Mối quan hệ phụ thuộc: CHITIETDETHIHV, CAUTRALOI";

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
            await FetchMonHocs();
        }

        private async Task GetItemsInSessionStorage()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataEQ>("storedDataEQ");

            if (storedData != null)
            {
                selectedMonHoc = storedData.MonHoc;
                selectedDeThi = storedData.DeThi;
            }
            await FetchAllData();
        }

        private async Task FetchAllData()
        {
            if(selectedMonHoc != null)
            {
                await FetchDeThi();
                await FetchNhomCauHoi();
                await FetchCauHoi();
            }
        }

        private async Task FetchMonHocs()
        {
            (monHocs, totalPages_Mon, totalRecords_Mon) = await MonHocs_GetAll_PagedAPI(currentPage_Mon, rowsPerPage_Mon);
            CreateFakeData_MonHoc();
        }

        private async Task FetchDeThi()
        {
            if (selectedMonHoc != null)
            {
                (deThis, totalPages_DeThi, totalRecords_DeThi) = await DeThis_SelectBy_MaMonHoc_PagedAPI(selectedMonHoc.MaMonHoc, currentPage_DeThi, rowsPerPage_DeThi);
                CreateFakeData_DeThi();
            }
        }

        private async Task FetchNhomCauHoi()
        {
            if (selectedDeThi != null)
            {
                nhomCauHois = await NhomCauHois_SelectAllBy_MaDeThiAPI(selectedDeThi.MaDeThi);
                cauHois?.Clear();

                // convert nhomCauHois to CustomNhomCauHoi
                if (nhomCauHois != null)
                    customNhomCauHois = HandleNhomCauHoi(nhomCauHois);
                if (selectedDeThi.TongSoDeHoanVi > 0)
                {
                    await OpenDialogAlreadyHasDeThiHoanVi();
                }
                return;
            }
        }

        private async Task FetchCauHoi()
        {
            if (selectedNhomCauHoi != null)
            {
                cauHois = await CauHois_SelectBy_MaNhomAPI(selectedNhomCauHoi.MaNhom);
                return;
            }
        }

        private List<CustomNhomCauHoi> HandleNhomCauHoi(List<NhomCauHoiDto> nhomCauHoiGoc)
        {
            List<CustomNhomCauHoi> result = new();
            // handle các câu hỏi cha trước
            foreach (var item in nhomCauHoiGoc)
            {
                var nhomCauHoi = Mapper.Map<CustomNhomCauHoi>(item);
                if (item.MaNhomCha == -1)
                    result.Add(nhomCauHoi);
            }
            // handle các câu hỏi nhóm con
            foreach (var item in nhomCauHoiGoc)
            {
                if (item.MaNhomCha != -1)
                {
                    var parent = result.FirstOrDefault(x => x.MaNhom == item.MaNhomCha);
                    if (parent != null)
                    {
                        var nhomCauHoi = Mapper.Map<CustomNhomCauHoi>(item);
                        parent.NhomCauHoiCons.Add(nhomCauHoi);
                    }
                }
            }
            return result;
        }

        private async Task OnClickThemMonThi()
        {
            var result = await OpenMonHocDialog(false);
            if (result != null && !result.Canceled && monHocs != null && result.Data != null)
            {
                var newMonHoc = (MonHocDto)result.Data;
                if (newMonHoc != null)
                {
                    monHocs.Insert(0, newMonHoc);
                    selectedMonHoc = newMonHoc;
                }
            }
        }

        private async Task OnClickSuaMonThi()
        {
            var result = await OpenMonHocDialog(true);
            if (result != null && !result.Canceled && monHocs != null && result.Data != null)
            {
                var newMonHoc = (MonHocDto)result.Data;
                if (newMonHoc != null && selectedMonHoc != null)
                {
                    int index = monHocs.FindIndex(m => m.MaMonHoc == newMonHoc.MaMonHoc);
                    if (index != -1)
                    {
                        monHocs[index] = newMonHoc;
                        selectedMonHoc = newMonHoc;
                    }
                }
            }
        }

        private async Task OnClickThemDeThi()
        {
            var result = await OpenDeThiDialog(false);
            if (result != null && !result.Canceled && deThis != null && result.Data != null)
            {
                var newDeThi = (DeThiDto)result.Data;
                if (deThis != null)
                {
                    deThis.Insert(0, newDeThi);
                    selectedDeThi = newDeThi;
                }
            }
        }

        private async Task OnClickSuaDeThi()
        {
            var result = await OpenDeThiDialog(true);
            if (result != null && !result.Canceled && monHocs != null && result.Data != null)
            {
                var newdeThi = (DeThiDto)result.Data;
                if (deThis != null && selectedDeThi != null)
                {
                    int index = deThis.FindIndex(m => m.MaDeThi == newdeThi.MaDeThi);
                    if (index != -1)
                    {
                        deThis[index] = newdeThi;
                        selectedDeThi = newdeThi;
                    }
                }
            }
        }

        private async Task OnClickXoaDeThi()
        {
            if (selectedDeThi == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_DETHI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteDeThi(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteDeThi(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA KHOA", parameters, options);
        }

        private async Task HandleDeleteDeThi(bool isForce)
        {
            if (selectedDeThi != null)
            {
                var result = (isForce) ? await DeThi_ForceDelete(selectedDeThi.MaDeThi) : await DeThi_Delete(selectedDeThi.MaDeThi);

                if (result)
                {
                    deThis?.Remove(selectedDeThi);
                    selectedDeThi = null;

                    customNhomCauHois?.Clear();
                    cauHois?.Clear();
                }
            }
        }

        private async Task OnClickXoaMonThi()
        {
            if (selectedMonHoc == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_MONHOC_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteMonHoc(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteMonHoc(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA KHOA", parameters, options);
        }

        private async Task HandleDeleteMonHoc(bool isForce)
        {
            if (selectedMonHoc != null)
            {
                var result = (isForce) ? await MonHoc_ForceDelete(selectedMonHoc.MaMonHoc) : await MonHoc_Delete(selectedMonHoc.MaMonHoc);

                if (result)
                {
                    monHocs?.Remove(selectedMonHoc);
                    selectedMonHoc = null;
                    deThis?.Clear();
                    customNhomCauHois?.Clear();
                    cauHois?.Clear();
                }
            }
        }

        private async Task<DialogResult?> OpenMonHocDialog(bool isEdit)
        {
            var parameters = new DialogParameters<MonHocDialog>
            {
                { x => x.IsEdit, isEdit},
                { x => x.MonHoc, selectedMonHoc }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<MonHocDialog>((isEdit) ? "SỬA MÔN HỌC" : "THÊM MÔN HỌC", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenDeThiDialog(bool isEdit)
        {
            var parameters = new DialogParameters<DeThiDialog>
            {
                { x => x.IsEdit, isEdit},
                { x => x.DeThi, selectedDeThi }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<DeThiDialog>((isEdit) ? "SỬA ĐỀ THI" : "THÊM ĐỀ THI", parameters, options);
            return await dialog.Result;
        }

        private async Task OpenDialogAlreadyHasDeThiHoanVi()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, WARNING_ALREADY_DETHIHOANVI },
                { x => x.ButtonText, "OK" },
                { x => x.Color, Color.Error },
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Thông báo", parameters, options);
        }

        private async Task OnClickThemNhomCauHoi()
        {
            if (selectedNhomCauHoi == null || selectedDeThi == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT + ".Vui lòng chọn để biết thêm vào vị trí nào", Severity.Warning);
                return;
            }
            NhomCauHoiDto nhomCauHoi = Mapper.Map<NhomCauHoiDto>(selectedNhomCauHoi);
            var result = await OpenNhomCauHoiDialog("Thêm Chương/Phần", false, nhomCauHoi, null, selectedDeThi);
            if (result != null && result.Data != null && !result.Canceled)
            {
                if (selectedDeThi != null)
                {
                    selectedNhomCauHoi = null;
                    nhomCauHois = await NhomCauHois_SelectAllBy_MaDeThiAPI(selectedDeThi.MaDeThi);

                    // convert nhomCauHois to CustomNhomCauHoi
                    if (nhomCauHois != null)
                        customNhomCauHois = HandleNhomCauHoi(nhomCauHois);
                }
            }
        }

        private async Task OnClickSuaNhomCauHoi()
        {
            if (selectedNhomCauHoi == null || selectedDeThi == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            NhomCauHoiDto nhomCauHoi = Mapper.Map<NhomCauHoiDto>(selectedNhomCauHoi);
            var result = await OpenNhomCauHoiDialog("Sửa Chương/Phần", true, null, nhomCauHoi, selectedDeThi);
            if (result != null && result.Data != null && !result.Canceled)
            {
                var id_nhomCauHoi = await NhomCauHoi_SelectOneAPI(Convert.ToInt32(result.Data));
                if (id_nhomCauHoi != null && selectedDeThi != null)
                {
                    selectedNhomCauHoi = null;
                    nhomCauHois = await NhomCauHois_SelectAllBy_MaDeThiAPI(selectedDeThi.MaDeThi);
                    // convert nhomCauHois to CustomNhomCauHoi
                    if (nhomCauHois != null)
                        customNhomCauHois = HandleNhomCauHoi(nhomCauHois);
                }
            }
        }

        private async Task OnClickXoaNhomCauHoi()
        {
            if (selectedNhomCauHoi == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_NHOMCAUHOI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteNhomCauHoi(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteNhomCauHoi(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA NHÓM CÂU HỎI", parameters, options);
        }

        private async Task HandleDeleteNhomCauHoi(bool isForce)
        {
            if (selectedNhomCauHoi != null)
            {
                var result = (isForce) ? await NhomCauHoi_ForceDelete(selectedNhomCauHoi.MaNhom) : await NhomCauHoi_Delete(selectedNhomCauHoi.MaNhom);

                if (result)
                {
                    await FetchNhomCauHoi();

                    cauHois?.Clear();
                }
            }
        }

        private async Task OnClickThemCauHoi()
        {
            if (selectedDeThi == null || selectedMonHoc == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            NhomCauHoiDto nhomCauHoi = Mapper.Map<NhomCauHoiDto>(selectedNhomCauHoi);
            await OpenCauHoiDialog("Thêm Câu Hỏi", false, null, selectedMonHoc, nhomCauHoi);
            cauHois = await CauHois_SelectBy_MaNhomAPI(nhomCauHoi.MaNhom);
        }

        private async Task OnClickSuaCauHoi(CauHoiDto cauHoi)
        {
            if (selectedNhomCauHoi == null || selectedDeThi == null || selectedMonHoc == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            NhomCauHoiDto nhomCauHoi = Mapper.Map<NhomCauHoiDto>(selectedNhomCauHoi);
            await OpenCauHoiDialog("Sửa Câu Hỏi", true, cauHoi, selectedMonHoc, nhomCauHoi);
            cauHois?.Clear();
            cauHois = await CauHois_SelectBy_MaNhomAPI(nhomCauHoi.MaNhom);
        }

        private async Task<DialogResult?> OpenNhomCauHoiDialog(string tittle, bool isEdit, NhomCauHoiDto? nhomCauHoiCha, NhomCauHoiDto? nhomCauHoi, DeThiDto? deThi)
        {
            var parameters = new DialogParameters<NhomCauHoiDialog>
            {
                { x => x.NhomCauHoi, nhomCauHoi },
                { x => x.NhomCauHoiCha, nhomCauHoiCha },
                { x => x.IsEdit, isEdit },
                { x => x.DeThi, deThi}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<NhomCauHoiDialog>(tittle, parameters, options);
            return await dialog.Result;
        }

        private async Task OpenCauHoiDialog(string tittle, bool isEdit, CauHoiDto? cauHoi, MonHocDto? monHoc, NhomCauHoiDto? nhomCauHoi)
        {
            var parameters = new DialogParameters<CauHoiDialog>
            {
                { x => x.CauHoi, cauHoi },
                { x => x.MonHoc, monHoc},
                { x => x.NhomCauHoi, nhomCauHoi },
                { x => x.IsEdit, isEdit },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<CauHoiDialog>(tittle, parameters, options);
        }




        private void PadEmptyRows(List<MonHocDto>? newMonHocs)
        {
            if (newMonHocs == null || newMonHocs.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Mon * rowsPerPage_Mon;
            if (monHocs != null && monHocs.Count != 0)
            {
                for (int i = 0; i < newMonHocs.Count; i++)
                {

                    monHocs[startRow++] = newMonHocs[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<DeThiDto>? newDeThis)
        {
            if (newDeThis == null || newDeThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_DeThi * rowsPerPage_DeThi;
            if (deThis != null && deThis.Count != 0)
            {
                for (int i = 0; i < newDeThis.Count; i++)
                {

                    deThis[startRow++] = newDeThis[i];
                }
            }
            StateHasChanged();
        }

        private void CreateFakeData_MonHoc()
        {
            if (monHocs != null && monHocs.Count != 0)
            {
                int count_fake = totalRecords_Mon - monHocs.Count;
                bool isFake = totalRecords_Mon > monHocs.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        monHocs.Add(new MonHocDto());
                }
            }
        }

        private void CreateFakeData_DeThi()
        {
            if (deThis != null && deThis.Count != 0)
            {
                int count_fake = totalRecords_DeThi - deThis.Count;
                bool isFake = totalRecords_DeThi > deThis.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        deThis.Add(new DeThiDto());
                }
            }
        }

        private async Task SaveData()
        {
            var selectedData = new StoredDataEQ
            {
                MonHoc = selectedMonHoc,
                DeThi = selectedDeThi,
                NhomCauHoi = Mapper.Map<NhomCauHoiDto>(selectedNhomCauHoi)
            };
            await SessionStorage.SetItemAsync("storedDataEQ", selectedData);
        }

    }
}
