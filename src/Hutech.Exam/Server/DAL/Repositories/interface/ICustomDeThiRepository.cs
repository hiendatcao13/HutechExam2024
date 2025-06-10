using Hutech.Exam.Shared.DTO.Custom;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICustomDeThiRepository
    {
        CustomDeThi GetProperty(IDataReader dataReader, int start = 0);

        public Task<List<CustomDeThi>> GetDeThi(long ma_de_hoan_vi);

    }
}
