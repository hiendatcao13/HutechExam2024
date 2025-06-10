using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DeThiHoanViRepository(IMapper mapper) : IDeThiHoanViRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 5; // số lượng cột trong bảng DeThiHoanVi

        public DeThiHoanViDto GetProperty(IDataReader dataReader, int start = 0)
        {
            DeThiHoanVi deThiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0 + start),
                MaDeThi = dataReader.GetInt32(1 + start),
                KyHieuDe = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(2 + start),
                NgayTao = dataReader.GetDateTime(3 + start),
                Guid = dataReader.IsDBNull(4 + start) ? null : dataReader.GetGuid(4 + start)
            };
            return _mapper.Map<DeThiHoanViDto>(deThiHoanVi);
        }

        public async Task<DeThiHoanViDto> SelectOne(long ma_de_hoan_vi)
        {
            using DatabaseReader sql = new("DeThiHoanVi_SelectOne");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);

            using var dataReader = await sql.ExecuteReaderAsync();
            DeThiHoanViDto result = new();
            if (dataReader != null && dataReader.Read())
            {
                result = GetProperty(dataReader);
            }
            return result;
        }

        public async Task<List<DeThiHoanViDto>> SelectBy_MaDeThi(int ma_de_thi)
        {
            using DatabaseReader sql = new("DeThiHoanVi_SelectBy_MaDeThi");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<DeThiHoanViDto> deThiHoanVis = new();

            while (dataReader != null && dataReader.Read())
            {
                DeThiHoanViDto deThiHoanVi = GetProperty(dataReader);
                deThiHoanVi.MaDeThiNavigation = new();
                deThiHoanVis.Add(GetProperty(dataReader));
            }

            return deThiHoanVis;
        }

        public async Task<Dictionary<int, int>> DapAn(long ma_de_hoan_vi)
        {
            using DatabaseReader sql = new("DeThiHoanVi_DapAn");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);

            using var dataReader = await sql.ExecuteReaderAsync();
            Dictionary<int, int> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result[dataReader.GetInt32(0)] = dataReader.GetInt32(1);
            }

            return result;
        }
    }
}
