using Hutech.Exam.Client.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using MudBlazor;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Client.Pages.Admin.ExamReview
{
    public partial class ExamReview
    {
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;


        List<MonHocDto>? monHocs = [];
        MonHocDto? selectedMonHoc;

        List<DeThiDto>? deThis = [];
        DeThiDto? selectedDeThi;

        //thống kê report
        List<CustomThongKeCauHoi> customThongKeCauHois = [];

        // thống kê điểm
        List<(double Diem, int SoLuongSV)> customThongKeDiems = [];
        int tong_sv_thi, tong_sv_duoibang1, tong_sv_duoi5 = 0;
        double diem_trung_binh = 0;

        protected async override Task OnInitializedAsync()
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
            await FetchMonHocs();
        }


        private async Task FetchMonHocs()
        {
            (monHocs, totalPages_Mon, totalRecords_Mon) = await MonHocs_GetAll_PagedAPI(currentPage_Mon, rowsPerPage_Mon);
            CreateFakeData_MonHoc();

            selectedMonHoc = null;
            selectedDeThi = null;
        }

        private async Task FetchDeThi()
        {
            if (selectedMonHoc != null)
            {
                (deThis, totalPages_DeThi, totalRecords_DeThi) = await DeThis_SelectBy_MaMonHoc_PagedAPI(selectedMonHoc.MaMonHoc, currentPage_DeThi, rowsPerPage_DeThi);
                CreateFakeData_DeThi();
            }

            selectedDeThi = null;
        }

        private async Task ThongKeDiem(int maDeThi)
        {
            customThongKeDiems = await ThongKeDiem_SelectBy_DeThiAPI(maDeThi);

            List<double> diems = customThongKeDiems.Select(_ => _.Diem).ToList();
            diem_trung_binh = diems.Average();
            tong_sv_duoibang1 = diems.Count(d => d <= 1);
            tong_sv_duoi5 = diems.Count(d => d < 5);
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
    }
}
