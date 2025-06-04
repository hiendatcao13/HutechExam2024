using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDotThiRepository
    {
        public Task<IDataReader> GetAll();

        public Task<IDataReader> GetAll_Paged(int pageNumber, int pageSize);

        public Task<IDataReader> SelectOne(int ma_dot_thi);

        public Task<object?> Insert(string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc);

        public Task<int> Update(int ma_dot_thi, string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc);

        public Task<int> Remove(int ma_dot_thi);

        public Task<int> ForceRemove(int ma_dot_thi);
    }
}
