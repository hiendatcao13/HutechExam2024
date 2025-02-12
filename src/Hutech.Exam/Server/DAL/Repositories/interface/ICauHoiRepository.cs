using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICauHoiRepository
    {
        public Task<IDataReader> SelectOne(int ma_cau_hoi);
        public Task<IDataReader> SelectDapAn(int ma_cau_hoi);
    }
}
