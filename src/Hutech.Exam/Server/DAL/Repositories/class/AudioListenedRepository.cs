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
                MaNghe = dataReader.GetInt64(0 + start),
                MaChiTietCaThi = dataReader.GetInt32(1 + start),
                TenFile = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(3 + start),
                SoLanNghe = dataReader.GetInt32(3 + start)
            };
            return _mapper.Map<AudioListenedDto>(audioListened);
        }

        public async Task<int> SelectOneAsync(int examSessionDetailId, string fileName)
        {
            int listenedCount = 0;

            using DatabaseReader sql = new("Audio_SelectOne");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, examSessionDetailId);
            sql.SqlParams("@TenFile", SqlDbType.NVarChar, fileName);

            using var dataReader =  await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                listenedCount = dataReader.GetInt32(0);
            }

            return listenedCount;
        }

        public async Task<int> UpdateAsync(int examSessionDetailId, Guid groupQuestionId, string fileName)
        {
            using DatabaseReader sql = new("Audio_Update");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, examSessionDetailId);
            sql.SqlParams("@MaNhom", SqlDbType.UniqueIdentifier, groupQuestionId);
            sql.SqlParams("@TenFile", SqlDbType.Int, fileName);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }
    }
}
