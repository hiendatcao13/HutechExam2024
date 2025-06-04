using Hutech.Exam.Server.DAL.DataReader;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class LopRepository : ILopRepository
    {
        public async Task<IDataReader> SelectOne(int ma_lop)
        {
            DatabaseReader sql = new("lop_SelectOne");
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(string ten_lop, DateTime ngay_bat_dau, int ma_khoa)
        {
            DatabaseReader sql = new("lop_Insert");
            sql.SqlParams("@ten_lop", SqlDbType.NVarChar, ten_lop);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            return await sql.ExecuteScalarAsync();
        }

        public async Task<int> Update(int ma_lop, string ten_lop, DateTime ngay_bat_dau, int ma_khoa)
        {
            DatabaseReader sql = new("lop_Insert");
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@ten_lop", SqlDbType.NVarChar, ten_lop);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> Remove(int ma_lop)
        {
            DatabaseReader sql = new("lop_Remove");
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> ForceRemove(int ma_lop)
        {
            DatabaseReader sql = new("lop_ForceRemove");
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> SelectBy_ten_lop(string ten_lop)
        {
            DatabaseReader sql = new("lop_SelectBy_ten_lop");
            sql.SqlParams("@ten_lop", SqlDbType.NVarChar, ten_lop);
            return await sql.ExecuteReaderAsync();

        }

        public async Task<IDataReader> SelectBy_ma_khoa_Paged(int ma_khoa, int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("lop_SelectBy_ma_khoa_Paged");
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            sql.SqlParams("@pageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@pageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<object?> Insert(string? ten_lop, DateTime? ngay_bat_dau, int? ma_khoa)
        {
            DatabaseReader sql = new("lop_Insert");
            sql.SqlParams("@ten_lop", SqlDbType.NVarChar, ten_lop ?? (object)DBNull.Value);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau ?? (object)DBNull.Value);
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa ?? (object)DBNull.Value);
            return await sql.ExecuteScalarAsync();
        }
    }
}
