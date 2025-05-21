using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class AudioListenedRepository : IAudioListenedRepository
    {
        public async Task<IDataReader> SelectOne(int ma_chi_tiet_ca_thi, string fileName)
        {
            DatabaseReader sql = new("AudioListened_SelectOne");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@FileName", SqlDbType.NVarChar, fileName);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Save(int ma_chi_tiet_ca_thi, int ma_nhom)
        {
            DatabaseReader sql = new("AudioListened_Save");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return await sql.ExecuteScalarAsync();
        }
    }
}
