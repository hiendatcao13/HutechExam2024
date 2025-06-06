using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICustomRepository
    {
        public Task<IDataReader> GetDeThi(long ma_de_hoan_vi);

        public Task<IDataReader> LayMaThongTinDeThi(long ma_de_thi);
    }
}
