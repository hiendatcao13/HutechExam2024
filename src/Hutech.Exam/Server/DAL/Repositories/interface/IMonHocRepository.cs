using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IMonHocRepository
    {
        public Task<IDataReader> SelectOne(int ma_mon_hoc);
        public Task<IDataReader> GetAll();
        public Task<object?> Insert(string ma_so_mon_hoc, string ten_mon_hoc);
        public Task<int> Update(int ma_mon_hoc, string ma_so_mon_hoc, string ten_mon_hoc);
        public Task<int> Remove(int ma_mon_hoc);
    }
}
