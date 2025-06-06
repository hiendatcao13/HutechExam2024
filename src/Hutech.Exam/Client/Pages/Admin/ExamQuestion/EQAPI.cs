using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;
using System.Net.Http.Json;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public partial class ExamQuestion
    {
        private async Task<NhomCauHoiDto?> NhomCauHoi_SelectOneAPI(int ma_nhom)
        {
            var response = await SenderAPI.GetAsync<NhomCauHoiDto>($"api/nhomcauhois/{ma_nhom}");
            return (response.Success) ? response.Data : null;
        }
        

        private async Task<(List<MonHocDto>?, int, int)> MonHocs_GetAll_PagedAPI(int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<MonHocDto>>($"api/monhocs?pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<List<CloDto>?> Clos_SelectBy_MaMonHocAPI(int ma_mon_hoc)
        {
            var response = await SenderAPI.GetAsync<List<CloDto>>($"api/clos/filter-by-monhoc?maMonHoc={ma_mon_hoc}");
            return (response.Success) ? response.Data : null;
        }

        private async Task<(List<DeThiDto>?, int, int)> DeThis_SelectBy_MaMonHoc_PagedAPI(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<DeThiDto>>($"api/dethis/filter-by-monhoc?maMonHoc={ma_mon_hoc}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<List<NhomCauHoiDto>?> NhomCauHois_SelectAllBy_MaDeThiAPI(int ma_de_thi)
        {
            var response = await SenderAPI.GetAsync<List<NhomCauHoiDto>>($"api/nhomcauhois/filter-by-dethi?maDeThi={ma_de_thi}");
            return (response.Success) ? response.Data : null;
        }

        public async Task<List<CauHoiDto>?> CauHois_SelectBy_MaNhomAPI(int ma_nhom)
        {
            var response = await SenderAPI.GetAsync<List<CauHoiDto>>($"api/cauhois/filter-by-nhomcauhoi?maNhomCauHoi={ma_nhom}");
            return (response.Success) ? response.Data : null;
        }


        public async Task<bool> MonHoc_DeleteAPI(int ma_mon_hoc)
        {
            var response = await SenderAPI.DeleteAsync<MonHocDto>($"api/monhocs/{ma_mon_hoc}");
            return response.Success;
        }

        public async Task<bool> MonHoc_ForceDeleteAPI(int ma_mon_hoc)
        {
            var response = await SenderAPI.DeleteAsync<MonHocDto>($"api/monhocs/{ma_mon_hoc}/force");
            return response.Success;
        }

        public async Task<bool> DeThi_DeleteAPI(int ma_de_thi)
        {
            var response = await SenderAPI.DeleteAsync<DeThiDto>($"api/dethis/{ma_de_thi}");
            return response.Success;
        }

        public async Task<bool> DeThi_ForceDeleteAPI(int ma_de_thi)
        {
            var response = await SenderAPI.DeleteAsync<DeThiDto>($"api/dethis/{ma_de_thi}/force");
            return response.Success;
        }

        public async Task<bool> NhomCauHoi_DeleteAPI(int ma_nhom_cau_hoi)
        {
            var response = await SenderAPI.DeleteAsync<NhomCauHoiDto>($"api/nhomcauhois/{ma_nhom_cau_hoi}");
            return response.Success;
        }

        public async Task<bool> NhomCauHoi_ForceDeleteAPI(int ma_nhom_cau_hoi)
        {
            var response = await SenderAPI.DeleteAsync<NhomCauHoiDto>($"api/nhomcauhois/{ma_nhom_cau_hoi}/force");
            return response.Success;
        }

        public async Task<bool> CauHoi_DeleteAPI(int ma_cau_hoi)
        {
            var response = await SenderAPI.DeleteAsync<CauHoiDto>($"api/cauhois/{ma_cau_hoi}");
            return response.Success;
        }

        public async Task<bool> CauHoi_ForceDeleteAPI(int ma_cau_hoi)
        {
            var response = await SenderAPI.DeleteAsync<CauHoiDto>($"api/cauhois/{ma_cau_hoi}/force");
            return response.Success;
        }

        public async Task<bool> CauTraLoi_DeleteAPI(int ma_cau_tra_loi)
        {
            var response = await SenderAPI.DeleteAsync<CauTraLoiDto>($"api/cautralois/{ma_cau_tra_loi}");
            return response.Success;
        }

        public async Task<bool> CauTraLoi_ForceDeleteAPI(int ma_cau_tra_loi)
        {
            var response = await SenderAPI.DeleteAsync<CauTraLoiDto>($"api/cautralois/{ma_cau_tra_loi}/force");
            return response.Success;
        }

        public async Task<bool> Clo_DeleteAPI(int maClo)
        {
            var response = await SenderAPI.DeleteAsync<CloDto>($"api/clos/{maClo}");
            return response.Success;
        }

        public async Task<bool> Clo_ForceDeleteAPI(int maClo)
        {
            var response = await SenderAPI.DeleteAsync<CloDto>($"api/clos/{maClo}/force");
            return response.Success;
        }
    }
}
