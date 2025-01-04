using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiRepository
    {
        public IDataReader SelectOne(int ma_de_thi);

        public IDataReader SelectBy_ma_de_hv(long ma_de_hv);
    }
}
