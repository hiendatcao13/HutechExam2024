using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class MonHocRepository(IMapper mapper) : IMonHocRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng MonHoc

        public MonHocDto GetProperty(IDataReader dataReader, int start = 0)
        {
            MonHoc monHoc = new()
            {
                MaMonHoc = dataReader.GetInt32(0 + start),
                MaSoMonHoc = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                TenMonHoc = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(2 + start)
            };
            return _mapper.Map<MonHocDto>(monHoc);
        }

        public async Task<MonHocDto> SelectOne(int ma_mon_hoc)
        {
            using DatabaseReader sql = new("MonHoc_SelectOne");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);

            using var dataReader = await sql.ExecuteReaderAsync();
            MonHocDto monHoc = new();

            if (await dataReader!.ReadAsync())
            {
                monHoc = GetProperty(dataReader);
            }

            return monHoc;
        }

        public async Task<List<MonHocDto>> GetAll()
        {
            using DatabaseReader sql = new("MonHoc_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<MonHocDto> result = [];

            while (await dataReader!.ReadAsync())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<Paged<MonHocDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("MonHoc_GetAll_Paged");

            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<MonHocDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                result.Add(GetProperty(dataReader));
            }

            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader != null && dataReader.NextResult())
            {
                while (dataReader.Read())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<MonHocDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<int> Insert(string ma_so_mon_hoc, string ten_mon_hoc)
        {
            using DatabaseReader sql = new("MonHoc_Insert");

            sql.SqlParams("@MaSoMonHoc", SqlDbType.NVarChar, ma_so_mon_hoc);
            sql.SqlParams("@TenMonHoc", SqlDbType.NVarChar, ten_mon_hoc);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_mon_hoc, string ma_so_mon_hoc, string ten_mon_hoc)
        {
            using DatabaseReader sql = new("MonHoc_Update");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@MaSoMonHoc", SqlDbType.NVarChar, ma_so_mon_hoc);
            sql.SqlParams("@TenMonHoc", SqlDbType.NVarChar, ten_mon_hoc);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_mon_hoc)
        {
            using DatabaseReader sql = new("MonHoc_Remove");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_mon_hoc)
        {
            using DatabaseReader sql = new("MonHoc_ForceRemove");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
