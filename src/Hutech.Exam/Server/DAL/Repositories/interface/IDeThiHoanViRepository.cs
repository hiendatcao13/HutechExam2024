using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiHoanViRepository
    {
        DeThiHoanViDto GetProperty(IDataReader dataReader, int start = 0);  

        Task<DeThiHoanViDto> SelectOne(long ma_de_hoan_vi);

        Task<List<DeThiHoanViDto>> SelectBy_MaDeThi(int ma_de_thi);

        Task<Dictionary<int, int>> DapAn(long ma_de_hoan_vi);
    }

}
