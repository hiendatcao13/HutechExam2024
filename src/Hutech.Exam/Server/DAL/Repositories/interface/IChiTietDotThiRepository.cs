using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IChiTietDotThiResposity
    {
        public Task<IDataReader> SelectBy_MaDotThi(int ma_dot_thi);
        public Task<IDataReader> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao);
        public Task<IDataReader> SelectOne(int ma_chi_tiet_dot_thi);
    }
}
