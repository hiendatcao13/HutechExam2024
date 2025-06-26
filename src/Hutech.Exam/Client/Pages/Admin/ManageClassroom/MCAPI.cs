using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;

namespace Hutech.Exam.Client.Pages.Admin.ManageClassroom
{
    public partial class ManageClassroom
    {
        private async Task<(List<KhoaDto>?, int, int)> Departments_GetAll_PagedAPI(int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<KhoaDto>>($"api/khoas?pageNumber={pageNumber + 1}&pageSize={pageSize}");

            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<(List<LopDto>?, int, int)> Classroom_SelectBy_DepartmentId_PagedAPI(int maKhoa, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<LopDto>>($"api/lops/filter-by-khoa?maKhoa={maKhoa}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<(List<SinhVienDto>?, int, int)> Students_SelectBy_ClassroomId_PagedAPI(int maLop, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<SinhVienDto>>($"api/sinhviens/filter-by-lop-paged?maLop={maLop}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }

        private async Task<(List<SinhVienDto>?, int, int)> Students_SelectBy_ClassromId_Search_PagedAPI(int maLop, string keyword, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<SinhVienDto>>($"api/sinhviens/filter-by-lop-search-paged?maLop={maLop}&keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) : (null, 0, 0);
        }


        private async Task<bool> Department_DeleteAPI(int maKhoa)
        {
            var response = await SenderAPI.DeleteAsync<KhoaDto>($"api/khoas/{maKhoa}");
            return response.Success;
        }

        private async Task<bool> Department_ForceDeleteAPI(int maKhoa)
        {
            var response = await SenderAPI.DeleteAsync<KhoaDto>($"api/khoas/{maKhoa}/force");
            return response.Success;
        }

        private async Task<bool> Classroom_DeleteAPI(int maLop)
        {
            var response = await SenderAPI.DeleteAsync<LopDto>($"api/lops/{maLop}");
            return response.Success;
        }

        private async Task<bool> Classroom_ForceDeleteAPI(int maLop)
        {
            var response = await SenderAPI.DeleteAsync<LopDto>($"api/lops/{maLop}/force");
            return response.Success;
        }

        private async Task<bool> Student_DeleteAPI(long maSinhVien)
        {
            var response = await SenderAPI.DeleteAsync<SinhVienDto>($"api/sinhviens/{maSinhVien}");
            return response.Success;
        }

        private async Task<bool> Student_ForceDeleteAPI(long maSinhVien)
        {
            var response = await SenderAPI.DeleteAsync<SinhVienDto>($"api/sinhviens/{maSinhVien}/force");
            return response.Success;
        }
    }
}
