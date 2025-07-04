using System.Data;
using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IAudioListenedRepository
    {
        AudioListenedDto GetProperty(IDataReader dataReader, int start = 0);

        Task<int> SelectOneAsync(int examSessionDetailId, string fileName);

        Task<int> UpdateAsync(int examSessionDetailId, string fileName);
    }
}
