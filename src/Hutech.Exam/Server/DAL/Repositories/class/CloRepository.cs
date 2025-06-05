using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CloRepository : ICloRepository
    {
        public async Task<IDataReader> SelectOne(int ma_clo)
        {
            DatabaseReader sql = new("Clo_SelectOne");
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<object?> Insert(int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau)
        {
            DatabaseReader sql = new("Clo_Insert");
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@MaSoCLO", SqlDbType.VarChar, ma_so_clo);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@NoiDung", SqlDbType.NVarChar, noi_dung);
            sql.SqlParams("@TieuChi", SqlDbType.Int, tieu_chi);
            sql.SqlParams("@SoCau", SqlDbType.Int, so_cau);
            return await sql.ExecuteScalarAsync();
        }
        public async Task<int> Update(int ma_clo, int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau)
        {
            DatabaseReader sql = new("Clo_Update");
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@MaSoCLO", SqlDbType.VarChar, ma_so_clo);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@NoiDung", SqlDbType.NVarChar, noi_dung);
            sql.SqlParams("@TieuChi", SqlDbType.Int, tieu_chi);
            sql.SqlParams("@SoCau", SqlDbType.Int, so_cau);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> Remove(int ma_clo)
        {
            DatabaseReader sql = new("Clo_Remove");
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> ForceRemove(int ma_clo)
        {
            DatabaseReader sql = new("Clo_ForceRemove");
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> SelectBy_MaMonHoc(int ma_mon_hoc)
        {
            DatabaseReader sql = new("Clo_SelectBy_MaMonHoc");
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            return await sql.ExecuteReaderAsync();
        }
    }
}
