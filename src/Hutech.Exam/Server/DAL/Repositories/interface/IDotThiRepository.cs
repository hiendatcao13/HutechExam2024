using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDotThiRepository
    {
        DotThiDto GetProperty(IDataReader dataReader, int start = 0);

        public Task<List<DotThiDto>> GetAll();

        public Task<Paged<DotThiDto>> GetAll_Paged(int pageNumber, int pageSize);

        public Task<DotThiDto> SelectOne(int ma_dot_thi);

        public Task<int> Insert(string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc);

        public Task<bool> Update(int ma_dot_thi, string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc);

        public Task<bool> Remove(int ma_dot_thi);

        public Task<bool> ForceRemove(int ma_dot_thi);
    }
}
