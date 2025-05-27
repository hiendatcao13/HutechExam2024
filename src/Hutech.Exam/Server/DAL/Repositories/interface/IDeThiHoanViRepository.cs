using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiHoanViRepository
    {
        public Task<IDataReader> SelectOne(long ma_de_hoan_vi);
        public Task<IDataReader> SelectBy_MaDeThi(int ma_de_thi);

        public Task<IDataReader> DapAn(long ma_de_hoan_vi);
    }

}
