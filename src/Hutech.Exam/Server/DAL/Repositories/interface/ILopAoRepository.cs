using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{

    public interface ILopAoRepository
    {
        LopAoDto GetProperty(IDataReader dataReader, int start = 0);

        Task<LopAoDto> SelectOne(int ma_lop_ao);

        Task<List<LopAoDto>> SelectBy_ma_mon_hoc(int ma_mon_hoc);

        Task<List<LopAoDto>> GetAll();

        Task<int> Insert(string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc);

        Task<bool> Update(int ma_lop_ao, string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc);

        Task<bool> Remove(int ma_lop_ao);

        Task<bool> ForceRemove(int ma_lop_ao);
    }
}
