using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface INhomCauHoiHoanViRepository
    {
        public Task<IDataReader> SelectOne(long ma_de_hoan_vi, int ma_nhom);
        public Task<IDataReader> SelectBy_MaDeHV(long ma_de_hoan_vi);
    }
}
