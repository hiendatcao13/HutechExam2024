using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IMonHocRepository
    {
        public Task<IDataReader> SelectOne(int ma_mon_hoc);
    }
}
