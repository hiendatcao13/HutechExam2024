using Hutech.Exam.Server.DAL.DataReader;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class KhoaRepository : IKhoaRepository
    {
        public async Task<IDataReader> SelectOne(int ma_khoa)
        {
            DatabaseReader sql = new("khoa_SelectOne");
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(string ten_khoa, DateTime ngay_thanh_lap)
        {
            DatabaseReader sql = new("khoa_Insert");
            sql.SqlParams("@ten_khoa", SqlDbType.NVarChar, ten_khoa);
            sql.SqlParams("@ngay_thanh_lap", SqlDbType.DateTime, ngay_thanh_lap);
            return await sql.ExecuteScalarAsync();
        }

        public async Task<int> Update(int ma_khoa, string ten_khoa, DateTime ngay_thanh_lap)
        {
            DatabaseReader sql = new("khoa_Update");
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            sql.SqlParams("@ten_khoa", SqlDbType.NVarChar, ten_khoa);
            sql.SqlParams("@ngay_thanh_lap", SqlDbType.DateTime, ngay_thanh_lap);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> Remove(int ma_khoa)
        {
            DatabaseReader sql = new("khoa_Remove");
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> ForceRemove(int ma_khoa)
        {
            DatabaseReader sql = new("khoa_ForceRemove");
            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new("khoa_GetAll");
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> GetAll_Paged(int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("khoa_GetAll_Paged");
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }
    }
}
