using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICauTraLoiRepository
    {
        public Task<IDataReader> SelectOne(int ma_cau_tra_loi);
        public Task<IDataReader> SelectBy_MaCauHoi(int ma_cau_hoi);
    }
}
