using Hutech.Exam.Shared.DTO;
namespace Hutech.Exam.Client.Pages.Info
{
    public partial class Info
    {
        private async Task<ChiTietCaThiDto?> GetChiTietCaThiAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.GetAsync<ChiTietCaThiDto>($"api/chitietcathis/filter-by-sinhvien?maSinhVien={ma_sinh_vien}");

            if(response.Success)
            {
                return response.Data;
            }    
            return null;
        }
        private async Task<bool> UpdateLogoutAPI(long ma_sinh_vien)
        {
            var response = await SenderAPI.PostAsync<SinhVienDto>($"api/sinhviens/{ma_sinh_vien}/logout", default);
            return response.Success;

        }
        private async Task<bool> UpdateBatDauThiAPI(int ma_chi_tiet_ca_thi)
        {
            var response = await SenderAPI.PatchAsync<ChiTietCaThiDto>($"api/chitietcathis/{ma_chi_tiet_ca_thi}/bat-dau-thi", null);
            return response.Success;
        }
    }
}
