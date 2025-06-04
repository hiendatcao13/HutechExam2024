using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ILopRepository
    {
        public Task<IDataReader> SelectOne(int ma_lop);
        public Task<object?> Insert(string ten_lop, DateTime ngay_bat_dau, int ma_khoa);

        public Task<int> Update(int ma_lop, string ten_lop, DateTime ngay_bat_dau, int ma_khoa);

        public Task<int> Remove(int ma_lop);

        public Task<int> ForceRemove(int ma_lop);

        public Task<IDataReader> SelectBy_ten_lop(string ten_lop);

        public Task<IDataReader> SelectBy_ma_khoa_Paged(int ma_khoa, int pageNumber, int pageSize);

        public Task<object?> Insert(string? ten_lop, DateTime? ngay_bat_dau, int? ma_khoa);
    }
}
