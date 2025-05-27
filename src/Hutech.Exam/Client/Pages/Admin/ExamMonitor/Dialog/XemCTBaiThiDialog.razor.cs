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
        
        private const string ERROR_GET_CTBT = "Lấy chi tiết bài thi của thí sinh thất bại";
        private const string ERROR_GET_CTCT = "Lấy chi tiết ca thi thất bại";
        private const string ERROR_FETCH_DETHI = "Không thể lấy đề thi. Vui lòng kiểm tra SV đã có đề thi chưa hoặc hệ thống lỗi";
        private const string ERROR_GET_DAPAN = "Lấy đề không thành công";

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
                ChiTietCaThi = await ChiTietCaThi_SelectOneAPI(maChiTietCaThi);
                SinhVien = ChiTietCaThi.MaSinhVienNavigation ?? new();
                CaThi = ChiTietCaThi.MaCaThiNavigation ?? new();
            }

            //lấy nội dung đề
            CustomDeThis = await GetDeThiAPI(ChiTietCaThi.MaDeThi);

            // lấy bài thi của thí sinh
            chiTietBaiThis = await ChiTietBaiThis_SelectBy_ma_chi_tiet_ca_thiAPI(ChiTietCaThi.MaChiTietCaThi);

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


        private async Task<ChiTietCaThiDto> ChiTietCaThi_SelectOneAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await Http.GetAsync($"api/chitietcathis/{ma_chi_tiet_ca_thi}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ChiTietCaThiDto>() ?? new();
            }
            else
            {
                Snackbar.Add(ERROR_GET_CTCT, Severity.Error);
                return new();
            }
        }

        private async Task<List<ChiTietBaiThiDto>> ChiTietBaiThis_SelectBy_ma_chi_tiet_ca_thiAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await Http.GetAsync($"api/chitietbaithis/filter-chitietcathi?maChiTietCaThi={ma_chi_tiet_ca_thi}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ChiTietBaiThiDto>>() ?? new();
            }
            else
            {
                Snackbar.Add(ERROR_GET_CTBT, Severity.Error);
                return new();
            }
        }

        private async Task<List<CustomDeThi>?> GetDeThiAPI(long? ma_de_hoan_vi)
        {
            var response = await Http.GetAsync($"api/dethihoanvis/{ma_de_hoan_vi}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CustomDeThi>?>();
            }
            else
            {
                Snackbar.Add(ERROR_FETCH_DETHI, Severity.Error);
                return null;
            }
        }

        private async Task<Dictionary<int, int>?> GetDapAnAPI(long maDeHV)
        {
            var response = await Http.GetAsync($"api/dethihoanvis/{maDeHV}/dap-an");
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Dictionary<int, int>>();
            }
            else
            {
                Snackbar.Add(ERROR_GET_DAPAN, Severity.Error);
                return null;
            }
        }
    }
}
