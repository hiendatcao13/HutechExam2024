using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICloRepository
    {
        CloDto GetProperty(IDataReader dataReader, int start = 0);

        Task<CloDto> SelectOne(int ma_clo);

        Task<int> Insert(int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau);

        Task<bool> Update(int ma_clo, int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau);

        Task<bool> Remove(int ma_clo);

        Task<bool> ForceRemove(int ma_clo);

        Task<List<CloDto>> SelectBy_MaMonHoc(int ma_mon_hoc);
    }
}
