using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IKhoaRepository
    {
        public Task<IDataReader> SelectOne(int ma_khoa);

        public Task<object?> Insert(string ten_khoa, DateTime ngay_thanh_lap);

        public Task<int> Update(int ma_khoa, string ten_khoa, DateTime ngay_thanh_lap);

        public Task<int> Remove(int ma_khoa);

        public Task<int> ForceRemove(int ma_khoa);

        public Task<IDataReader> GetAll();

        public Task<IDataReader> GetAll_Paged(int pageNumber, int pageSize);
    }
}
