using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class AudioListenedRepository : IAudioListenedRepository
    {
        public async Task<IDataReader> SelectOne(int ma_chi_tiet_ca_thi, string fileName)
        {
            DatabaseReader sql = new DatabaseReader("tbl_AudioListened_SelectOne");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@FileName", SqlDbType.NVarChar, fileName);
            return await sql.ExecuteReader();
        }
        public async Task<bool> Save(int ma_chi_tiet_ca_thi, string fileName)
        {
            DatabaseReader sql = new DatabaseReader("tbl_AudioListened_Save");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@FileName", SqlDbType.NVarChar, fileName);
            return await sql.ExcuteNonQuery() != 0;
        }
    }
}
