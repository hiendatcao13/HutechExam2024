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


        private const string WARNING_ALREADY_DETHIHOANVI = "Đề thi này đã tạo ra các đề hoán vị. Việc thao tác thêm sẽ không ảnh hưởng đến các bộ đề hoán vị, sửa hoặc xóa nội dung câu hỏi, mã nhóm gốc vẫn được thực hiện.";
        private const string NOT_SELECT_OBJECT = "Vui lòng chọn đối tượng cần thao tác";

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

        private async Task Start()
        {
            monHocs = await MonHocs_GetAllAPI();
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
                    if(parent != null)
                    {
                        var nhomCauHoi = Mapper.Map<CustomNhomCauHoi>(item);
                        parent.NhomCauHoiCons.Add(nhomCauHoi);
                    }
                }
            }
            return result;
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
            if(selectedNhomCauHoi == null || selectedDeThi == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
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
            if (selectedNhomCauHoi == null || selectedDeThi == null || selectedMonHoc != null)
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
            cauHois = await CauHois_SelectBy_MaNhomAPI(nhomCauHoi.MaNhom);
        }
        private async Task<DialogResult?> OpenNhomCauHoiDialog(string tittle, bool isEdit, NhomCauHoiDto? nhomCauHoiCha, NhomCauHoiDto? nhomCauHoi, DeThiDto deThi)
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

    }
}
