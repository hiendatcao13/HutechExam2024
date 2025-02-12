using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICaThiRepository
    {
        public Task<IDataReader> SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi);
        public Task<IDataReader> SelectOne(int ma_ca_thi);
        public Task<IDataReader> ca_thi_GetAll();
        public Task ca_thi_Activate(int ma_ca_thi, bool IsActivated);
        public Task ca_thi_Ketthuc(int ma_ca_thi);
        public Task Remove(int ma_ca_thi);
        public Task Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi);
    }
}
