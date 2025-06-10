using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IMonHocRepository
    {
        MonHocDto GetProperty(IDataReader dataReader, int start = 0);   

        Task<MonHocDto> SelectOne(int ma_mon_hoc);

        Task<List<MonHocDto>> GetAll();

        Task<Paged<MonHocDto>> GetAll_Paged(int pageNumber, int pageSize);

        Task<int> Insert(string ma_so_mon_hoc, string ten_mon_hoc);

        Task<bool> Update(int ma_mon_hoc, string ma_so_mon_hoc, string ten_mon_hoc);

        Task<bool> Remove(int ma_mon_hoc);

        Task<bool> ForceRemove(int ma_mon_hoc);
    }
}
