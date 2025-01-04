using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class SinhVienRepository : ISinhVienRepository
    {
        public Object Insert(string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh, DateTime? ngay_sinh, int? ma_lop,
            string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien, Guid? student_id)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Insert");
            sql.SqlParams("@ho_va_ten_lot", SqlDbType.NVarChar, ho_va_ten_lot ?? (object)DBNull.Value);
            sql.SqlParams("@ten_sinh_vien", SqlDbType.NVarChar, ten_sinh_vien ?? (object)DBNull.Value);
            sql.SqlParams("@gioi_tinh", SqlDbType.SmallInt, gioi_tinh ?? (object)DBNull.Value);
            sql.SqlParams("@ngay_sinh", SqlDbType.DateTime, ngay_sinh ?? (object)DBNull.Value);
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop ?? (object)DBNull.Value);
            sql.SqlParams("@dia_chi", SqlDbType.Text, dia_chi ?? (object)DBNull.Value);
            sql.SqlParams("@email", SqlDbType.NVarChar, email ?? (object)DBNull.Value);
            sql.SqlParams("@dien_thoai", SqlDbType.NVarChar, dien_thoai ?? (object)DBNull.Value);
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.NVarChar, ma_so_sinh_vien ?? (object)DBNull.Value);
            sql.SqlParams("@student_id", SqlDbType.UniqueIdentifier, student_id ?? (object)DBNull.Value);
            return sql.ExecuteScalar();
        }
        public bool Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh, 
            DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Update");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@ho_va_ten_lot", SqlDbType.NVarChar, ho_va_ten_lot ?? (object)DBNull.Value);
            sql.SqlParams("@ten_sinh_vien", SqlDbType.NVarChar, ten_sinh_vien ?? (object)DBNull.Value);
            sql.SqlParams("@gioi_tinh", SqlDbType.SmallInt, gioi_tinh ?? (object)DBNull.Value);
            sql.SqlParams("@ngay_sinh", SqlDbType.DateTime, ngay_sinh ?? (object)DBNull.Value);
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop ?? (object)DBNull.Value);
            sql.SqlParams("@dia_chi", SqlDbType.Text, dia_chi ?? (object)DBNull.Value);
            sql.SqlParams("@email", SqlDbType.NVarChar, email ?? (object)DBNull.Value);
            sql.SqlParams("@dien_thoai", SqlDbType.NVarChar, dien_thoai ?? (object)DBNull.Value);
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.NVarChar, ma_so_sinh_vien ?? (object)DBNull.Value);
            int result = sql.ExcuteNonQuery();
            return result != 0;
        }
        public bool Remove(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Remove");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            int result = sql.ExcuteNonQuery();
            return result != 0;
        }
        // lấy thông tin của 1 SV từ maSV
        public IDataReader SelectOne(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_SelectOne");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return sql.ExcuteReader();
        }
        // lấy mã SV từ mã số SV
        public IDataReader SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_SelectBy_ma_so_sinh_vien");
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.NVarChar, ma_so_sinh_vien);
            return sql.ExcuteReader();
        }
        public IDataReader GetAll()
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_GetAll");
            return sql.ExcuteReader();
        }
        public bool Login(long ma_sinh_vien, DateTime last_log_in)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Login");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@last_logged_in", SqlDbType.DateTime, last_log_in);
            return sql.ExcuteNonQuery() != 0;
        }
        public bool Logout(long ma_sinh_vien, DateTime last_log_out)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Logout");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@last_logged_out", SqlDbType.DateTime, last_log_out);
            return sql.ExcuteNonQuery() != 0;
        }
    }
}
