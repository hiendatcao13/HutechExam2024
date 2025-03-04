using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories 
{

    public interface ILopAoRepository
    {
        public Task<IDataReader> SelectOne(int ma_lop_ao);
        public Task<IDataReader> SelectBy_ma_mon_hoc(int ma_mon_hoc);
        public Task<IDataReader> GetAll();
        public Task<object?> Insert(string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc);
        public Task<int> Update(int ma_lop_ao, string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc);
        public Task<int> Remove(int ma_lop_ao);
    }
}
