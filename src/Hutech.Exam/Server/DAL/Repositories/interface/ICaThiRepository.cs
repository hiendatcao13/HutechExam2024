using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICaThiRepository
    {
        public IDataReader SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi);
        public IDataReader SelectOne(int ma_ca_thi);
        public IDataReader ca_thi_GetAll();
        public void ca_thi_Activate(int ma_ca_thi, bool IsActivated);
        public void ca_thi_Ketthuc(int ma_ca_thi);
        public void Remove(int ma_ca_thi);
        public void Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi);
    }
}
