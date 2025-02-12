using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDotThiRepository
    {
        public Task<IDataReader> GetAll();
        public Task<IDataReader> SelectOne(int ma_dot_thi);
    }
}
