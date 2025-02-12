using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface INhomCauHoiRepository
    {
        public Task<IDataReader> SelectBy_MaDeThi(int ma_de_thi);
        public Task<IDataReader> SelectOne(int ma_nhom);
    }
}
