using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class NhomCauHoiHoanViRepository : INhomCauHoiHoanViRepository
    {
        public async Task<IDataReader> SelectOne (long ma_de_hoan_vi, int ma_nhom)
        {
            DatabaseReader sql = new DatabaseReader("tbl_NhomCauHoiHoanVi_SelectOne");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return await sql.ExecuteReader();
        }
        public async Task<IDataReader> SelectBy_MaDeHV(long ma_de_hoan_vi)
        {
            DatabaseReader sql = new DatabaseReader("tbl_NhomCauHoiHoanVi_SelectBy_MaDeHV");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            return await sql.ExecuteReader();
        }
    }
}
