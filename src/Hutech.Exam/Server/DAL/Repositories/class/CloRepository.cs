using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CloRepository(IMapper mapper) : ICloRepository
    {
        private IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 7; // số lượng cột trong bảng Clo

        public CloDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Clo clo = new()
            {
                MaClo = dataReader.GetInt32(0 + start),
                MaMonHoc = dataReader.GetInt32(1 + start),
                MaSoClo = dataReader.GetString(2 + start),
                TieuDe = dataReader.GetString(3 + start),
                NoiDung = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                TieuChi = dataReader.GetInt32(5 + start),
                SoCau = dataReader.GetInt32(6 + start)
            };
            return _mapper.Map<CloDto>(clo);
        }

        public async Task<CloDto> SelectOne(int ma_clo)
        {
            using DatabaseReader sql = new("Clo_SelectOne");

            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);

            using var dataReader = await sql.ExecuteReaderAsync();
            CloDto clo = new();
            if (dataReader != null && dataReader.Read())
            {
                clo = GetProperty(dataReader);
            }
            return clo;
        }

        public async Task<int> Insert(int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau)
        {
            using DatabaseReader sql = new("Clo_Insert");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@MaSoCLO", SqlDbType.VarChar, ma_so_clo);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@NoiDung", SqlDbType.NVarChar, noi_dung);
            sql.SqlParams("@TieuChi", SqlDbType.Int, tieu_chi);
            sql.SqlParams("@SoCau", SqlDbType.Int, so_cau);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_clo, int ma_mon_hoc, string ma_so_clo, string tieu_de, string noi_dung, int tieu_chi, int so_cau)
        {
            using DatabaseReader sql = new("Clo_Update");

            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@MaSoCLO", SqlDbType.VarChar, ma_so_clo);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@NoiDung", SqlDbType.NVarChar, noi_dung);
            sql.SqlParams("@TieuChi", SqlDbType.Int, tieu_chi);
            sql.SqlParams("@SoCau", SqlDbType.Int, so_cau);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_clo)
        {
            using DatabaseReader sql = new("Clo_Remove");

            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_clo)
        {
            using DatabaseReader sql = new("Clo_ForceRemove");

            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<List<CloDto>> SelectBy_MaMonHoc(int ma_mon_hoc)
        {
            using DatabaseReader sql = new("Clo_SelectBy_MaMonHoc");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<CloDto> result = [];
            while (dataReader != null && dataReader.Read())
            {
                CloDto clo = GetProperty(dataReader);
                result.Add(clo);
            }

            return result;
        }
    }
}
