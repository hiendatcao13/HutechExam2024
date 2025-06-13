using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class LopAoRepository(IMapper mapper) : ILopAoRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng LopAo

        public LopAoDto GetProperty(IDataReader dataReader, int start = 0)
        {
            LopAo lopAo = new()
            {
                MaLopAo = dataReader.GetInt32(0 + start),
                TenLopAo = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayBatDau = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start),
                MaMonHoc = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt32(3 + start)
            };
            return _mapper.Map<LopAoDto>(lopAo);
        }


        public async Task<LopAoDto> SelectOne(int ma_lop_ao)
        {
            using DatabaseReader sql = new("lop_ao_SelectOne");

            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);

            using var dataReader = await sql.ExecuteReaderAsync();
            LopAoDto lopAo = new();

            if (dataReader != null && dataReader.Read())
            {
                lopAo = GetProperty(dataReader);
            }

            return lopAo;
        }

        public async Task<List<LopAoDto>> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            using DatabaseReader sql = new("lop_ao_SelectBy_ma_mon_hoc");

            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<LopAoDto> result = [];

            while (dataReader != null && dataReader.Read())
            {
                LopAoDto lopAo = GetProperty(dataReader);
                result.Add(lopAo);
            }

            return result;
        }

        public async Task<List<LopAoDto>> GetAll()
        {
            using DatabaseReader sql = new("lop_ao_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<LopAoDto> result = [];

            while (dataReader != null && dataReader.Read())
            {
                LopAoDto lopAo = GetProperty(dataReader);
                result.Add(lopAo);
            }

            return result;
        }

        public async Task<int> Insert(string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc)
        {
            using DatabaseReader sql = new("lop_ao_Insert");

            sql.SqlParams("@ten_lop_ao", SqlDbType.NVarChar, ten_lop_ao);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_lop_ao, string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc)
        {
            using DatabaseReader sql = new("lop_ao_Update");

            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@ten_lop_ao", SqlDbType.NVarChar, ten_lop_ao);
            sql.SqlParams("@ngay_bat_dau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@ma_mon_hoc", SqlDbType.Int, ma_mon_hoc);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_lop_ao)
        {
            using DatabaseReader sql = new("lop_ao_Remove");

            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_lop_ao)
        {
            using DatabaseReader sql = new("lop_ao_ForceRemove");

            sql.SqlParams("@ma_lop_ao", SqlDbType.Int, ma_lop_ao);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

    }
}
