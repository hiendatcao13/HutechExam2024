using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IDeThiRepository
    {
        public Task<object?> Insert(int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan);

        public Task<int> Update(int ma_de_thi, int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan);

        public Task<int> Delete(int ma_de_thi);

        public Task<int> ForceDelete(int ma_de_thi);

        public Task<IDataReader> SelectOne(int ma_de_thi);

        public Task<IDataReader> SelectBy_ma_de_hv(long ma_de_hv);

        public Task<IDataReader> GetAll();

        public Task<IDataReader> SelectByMonHoc(int ma_mon_hoc);

        public Task<IDataReader> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize);

    }
}
