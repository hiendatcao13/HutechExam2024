using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories 
{

    public interface ILopAoRepository
    {
        public Task<IDataReader> SelectOne(int ma_lop_ao);
        public Task<IDataReader> SelectBy_ma_mon_hoc(int ma_mon_hoc);
    }
}
