using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ILopRepository
    {
        public IDataReader SelectBy_ten_lop(string ten_lop);
        public Object Insert(string? ten_lop, DateTime? ngay_bat_dau, int? ma_khoa);
    }
}
