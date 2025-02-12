using Hutech.Exam.Server.DAL.DataReader;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class LopRepository : ILopRepository
    {
        public async Task<IDataReader> SelectBy_ten_lop(string ten_lop)
        {
            DatabaseReader sql = new DatabaseReader("lop_SelectBy_ten_lop");
            sql.SqlParams("@ten_lop", SqlDbType.NVarChar, ten_lop);
            return await sql.ExecuteReader();

        }
        public async Task<object?> Insert(string? ten_lop, DateTime? ngay_bat_dau, int? ma_khoa)
        {
            DatabaseReader sql = new DatabaseReader("lop_Insert");
            sql.SqlParams("@ten_lop", SqlDbType.NVarChar, ten_lop ?? (object)DBNull.Value);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau ?? (object)DBNull.Value);
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa ?? (object)DBNull.Value);
            return await sql.ExecuteScalar();
        }
    }
}
