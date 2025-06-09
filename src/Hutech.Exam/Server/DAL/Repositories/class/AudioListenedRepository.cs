using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class AudioListenedRepository(IMapper mapper) : IAudioListenedRepository
    {
        private readonly IMapper _mapper = mapper;
        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng AudioListened

        public AudioListenedDto GetProperty(IDataReader dataReader, int start = 0)
        {
            AudioListened audioListened = new()
            {
                ListenId = dataReader.GetInt64(0 + start),
                MaChiTietCaThi = dataReader.GetInt32(1 + start),
                MaNhom = dataReader.GetInt32(2 + start),
                FileName = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                ListenedCount = dataReader.GetInt32(4 + start)
            };
            return _mapper.Map<AudioListenedDto>(audioListened);
        }

        public async Task<int> SelectOne(int ma_chi_tiet_ca_thi, string fileName)
        {
            int listenedCount = 0;

            using DatabaseReader sql = new("AudioListened_SelectOne");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@FileName", SqlDbType.NVarChar, fileName);

            using var dataReader =  await sql.ExecuteReaderAsync();
            if (dataReader != null && dataReader.Read())
            {
                listenedCount = dataReader.GetInt32(0);
            }

            return listenedCount;
        }

        public async Task<int> Save(int ma_chi_tiet_ca_thi, int ma_nhom)
        {
            using DatabaseReader sql = new("AudioListened_Save");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }
    }
}
