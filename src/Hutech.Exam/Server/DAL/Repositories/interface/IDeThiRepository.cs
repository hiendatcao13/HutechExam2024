using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiRepository
    {
        public Task<IDataReader> SelectOne(int ma_de_thi);

        public Task<IDataReader> SelectBy_ma_de_hv(long ma_de_hv);
        public Task<IDataReader> GetAll();
    }
}
