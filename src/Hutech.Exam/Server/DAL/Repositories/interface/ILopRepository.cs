using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ILopRepository
    {
        LopDto GetProperty(IDataReader dataReader, int start = 0);

        Task<LopDto> SelectOne(int ma_lop);

        Task<int> Insert(string ten_lop, DateTime ngay_bat_dau, int ma_khoa);

        Task<bool> Update(int ma_lop, string ten_lop, DateTime ngay_bat_dau, int ma_khoa);

        Task<bool> Remove(int ma_lop);

        Task<bool> ForceRemove(int ma_lop);

        Task<LopDto> SelectBy_ten_lop(string ten_lop);

        Task<Paged<LopDto>> SelectBy_ma_khoa_Paged(int ma_khoa, int pageNumber, int pageSize);

    }
}
