using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DeThiRepository : IDeThiRepository
    {

        public async Task<object?> Insert(int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan)
        {
            DatabaseReader sql = new("DeThi_Insert");
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@TenDeThi", SqlDbType.NVarChar, ten_de_thi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, ngay_tao);
            sql.SqlParams("@NguoiTao", SqlDbType.Int, nguoi_tao);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@BoChuongPhan", SqlDbType.Bit, bo_chuong_phan);
            return await sql.ExecuteScalarAsync();
        }

        public async Task<int> Update(int ma_de_thi, int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan)
        {
            DatabaseReader sql = new("DeThi_Update");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@TenDeThi", SqlDbType.NVarChar, ten_de_thi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, ngay_tao);
            sql.SqlParams("@NguoiTao", SqlDbType.Int, nguoi_tao);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@BoChuongPhan", SqlDbType.Bit, bo_chuong_phan);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> Delete(int ma_de_thi)
        {
            DatabaseReader sql = new("DeThi_Delete");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> ForceDelete(int ma_de_thi)
        {
            DatabaseReader sql = new("DeThi_ForceDelete");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return await sql.ExecuteNonQueryAsync();
        }


        public async Task<IDataReader> SelectOne(int ma_de_thi)
        {
            DatabaseReader sql = new("DeThi_SelectOne");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> SelectBy_ma_de_hv(long ma_de_hv)
        {
            DatabaseReader sql = new("DeThi_SelectBy_ma_de_hv");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hv);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> GetAll()
        {
            DatabaseReader sql = new("DeThi_SelectAll");
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> SelectByMonHoc(int ma_mon_hoc)
        {
            DatabaseReader sql = new("DeThi_SelectByMonHoc");
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<IDataReader> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            DatabaseReader sql = new("DeThi_SelectByMonHoc_Paged");
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);
            return await sql.ExecuteReaderAsync();
        }
    }
}
