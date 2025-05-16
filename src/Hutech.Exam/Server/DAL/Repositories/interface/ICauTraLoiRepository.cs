using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICauTraLoiRepository
    {
        public Task<IDataReader> SelectOne(int ma_cau_tra_loi);
        public Task<IDataReader> SelectBy_MaCauHoi(int ma_cau_hoi);
        public Task<object?> Insert(int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi);
        public Task<int> Update(int ma_cau_tra_loi, int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi);
        public Task<int> Remove(int ma_cau_tra_loi);
        public Task<IDataReader> SelectBy_MaDeHV_DapAn(long ma_de_hv);
    }
}
