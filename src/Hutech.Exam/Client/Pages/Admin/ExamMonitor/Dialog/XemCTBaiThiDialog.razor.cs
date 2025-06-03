using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamMonitor.Dialog
{
    partial class XemCTBaiThiDialog
    {
        [Parameter][SupplyParameterFromQuery] public string? ma_chi_tiet_ca_thi { get; set; }

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] public HttpClient Http { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        [Inject] private IJSRuntime Js { get; set; } = default!;

        private List<CustomDeThi>? CustomDeThis { get; set; }

        private ChiTietCaThiDto ChiTietCaThi { get; set; } = default!;

        private CaThiDto CaThi { get; set; } = default!;

        private SinhVienDto SinhVien { get; set; } = default!;

        private List<ChiTietBaiThiDto> chiTietBaiThis = default!;

        private Dictionary<int, int> DsDapAn { get; set; } = []; // ds đáp án của đề thi

        // Item1: số thứ tự câu hỏi, Item2: mã câu trả lời, Item3: kết quả của câu
        private Dictionary<int, (int, int?, bool?)> DSKhoanhDapAn { get; set; } = []; // lưu vết các câu hỏi đã chọn hay chưa chọn của sinh viên


        private bool _shouldRender = false;

        private const string ERROR_PAGE = "Cách hoạt động trang không bình thường. Vui lòng quay lại";


        protected override async Task OnInitializedAsync()
        {

            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                bool isConvert = int.TryParse(ma_chi_tiet_ca_thi, out int maChiTietCaThi);

                if (!isConvert)
                {
                    await Js.InvokeVoidAsync("alert", ERROR_PAGE);
                    return; // không cho tiếp cận trang
                }

                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                // lấy thông tin cho thí sinh
                ChiTietCaThi = await ChiTietCaThi_SelectOneAPI(maChiTietCaThi) ?? new();
                SinhVien = ChiTietCaThi.MaSinhVienNavigation ?? new();
                CaThi = ChiTietCaThi.MaCaThiNavigation ?? new();
            }

            //lấy nội dung đề
            CustomDeThis = await GetDeThiAPI(ChiTietCaThi.MaDeThi);

            // lấy bài thi của thí sinh
            chiTietBaiThis = await ChiTietBaiThis_SelectBy_ma_chi_tiet_ca_thiAPI(ChiTietCaThi.MaChiTietCaThi) ?? new();

            // xử lí dữ liệu đưa ra màn hình
            HandleDsKhoanh(chiTietBaiThis);

            //hiện đáp án
            await OnClickHienDapAn();


            await base.OnInitializedAsync();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($" rendered. FirstRender: {firstRender}");
        }

        protected override void OnParametersSet() => _shouldRender = true;

        protected override bool ShouldRender()
        {
            if (_shouldRender)
            {
                _shouldRender = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void HandleDsKhoanh(List<ChiTietBaiThiDto> dsChiTietBaiThi)
        {
            if (CustomDeThis != null && CustomDeThis.Count != 0)
            {
                int stt = 0;
                foreach (var noidung in CustomDeThis)
                {
                    DSKhoanhDapAn.Add(noidung.MaCauHoi, (++stt, null, null)); // khởi tạo tất cả các câu hỏi với giá trị null)
                }
            }

            // xử lí câu đáp án đã khoanh
            foreach (var item in dsChiTietBaiThi)
            {
                DSKhoanhDapAn[item.MaCauHoi] = (DSKhoanhDapAn[item.MaCauHoi].Item1,item.CauTraLoi, item.KetQua);
            }
        }


        private async Task OnClickHienDapAn()
        {
            DsDapAn = await GetDapAnAPI(ChiTietCaThi.MaDeThi ?? -1) ?? [];
        }


        private async Task<ChiTietCaThiDto?> ChiTietCaThi_SelectOneAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await SenderAPI.GetAsync<ChiTietCaThiDto>($"api/chitietcathis/{ma_chi_tiet_ca_thi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<List<ChiTietBaiThiDto>?> ChiTietBaiThis_SelectBy_ma_chi_tiet_ca_thiAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await SenderAPI.GetAsync<List<ChiTietBaiThiDto>>($"api/chitietbaithis/filter-by-chitietcathi?maChiTietCaThi={ma_chi_tiet_ca_thi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<List<CustomDeThi>?> GetDeThiAPI(long? ma_de_hoan_vi)
        {
            var response = await SenderAPI.GetAsync<List<CustomDeThi>>($"api/dethihoanvis/{ma_de_hoan_vi}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<Dictionary<int, int>?> GetDapAnAPI(long maDeHV)
        {
            var response = await SenderAPI.GetAsync<Dictionary<int, int>>($"api/dethihoanvis/{maDeHV}/dap-an");
            return (response.Success) ? response.Data : null; 
        }
    }
}
