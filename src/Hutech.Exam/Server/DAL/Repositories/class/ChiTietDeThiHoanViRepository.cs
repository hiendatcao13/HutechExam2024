using AutoMapper;
using Hutech.Exam.Client.Pages.Result;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietDeThiHoanViRepository(IMapper mapper) : IChiTietDeThiHoanViRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 6; // số lượng cột trong bảng ChiTietDeThiHoanVi

        public ChiTietDeThiHoanViDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietDeThiHoanVi chiTietDeThiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                MaCauHoi = dataReader.GetInt32(2 + start),
                ThuTu = dataReader.GetInt32(3 + start),
                HoanViTraLoi = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                DapAn = dataReader.IsDBNull(5 + start) ? null : dataReader.GetInt32(5 + start)
            };
            return _mapper.Map<ChiTietDeThiHoanViDto>(chiTietDeThiHoanVi);
        }

        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV(long maDeHV)
        {
            List<ChiTietDeThiHoanViDto> result = [];

            using DatabaseReader sql = new("ChiTietDeThiHoanVi_SelectBy_MaDeHV");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, maDeHV);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }
        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            List<ChiTietDeThiHoanViDto> result = [];

            using DatabaseReader sql = new("ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }
    }
}
