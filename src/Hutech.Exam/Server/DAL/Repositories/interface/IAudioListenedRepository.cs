using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IAudioListenedRepository
    {
        AudioListenedDto GetProperty(IDataReader dataReader, int start = 0);

        Task<int> SelectOne(int ma_chi_tiet_ca_thi, string fileName);

        Task<int> Save(int ma_chi_tiet_ca_thi, int ma_nhom);
    }
}
