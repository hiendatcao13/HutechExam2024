using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IAudioListenedRepository
    {
        public Task<IDataReader> SelectOne(int ma_chi_tiet_ca_thi, string filename);

        public Task<object?> Save(int ma_chi_tiet_ca_thi, int ma_nhom);
    }
}
