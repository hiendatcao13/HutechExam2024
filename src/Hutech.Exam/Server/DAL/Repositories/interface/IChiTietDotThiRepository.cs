using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietDotThiResposity
    {
        public Task<IDataReader> SelectBy_MaDotThi(int ma_dot_thi);
        public Task<IDataReader> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao);
        public Task<IDataReader> SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, int lan_thi);
        public Task<IDataReader> SelectOne(int ma_chi_tiet_dot_thi);
        public Task<IDataReader> GetAll();
        public Task<object?> Insert(string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi);
        public Task<int> Update(int ma_chi_tiet_dot_thi, string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi);
        public Task<int> Remove(int ma_chi_tiet_dot_thi);
    }
}
