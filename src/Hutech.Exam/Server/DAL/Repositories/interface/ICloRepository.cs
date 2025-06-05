using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICloRepository
    {
        public Task<IDataReader> SelectOne(int ma_clo);

        public Task<object?> Insert(int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau);

        public Task<int> Update(int ma_clo, int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau);

        public Task<int> Remove(int ma_clo);

        public Task<int> ForceRemove(int ma_clo);

        public Task<IDataReader> SelectBy_MaMonHoc(int ma_mon_hoc);
    }
}
