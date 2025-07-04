using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class LopRepository(IMapper mapper) : ILopRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng Lop

        public LopDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Lop lop = new()
            {
                MaLop = dataReader.GetInt32(0 + start),
                TenLop = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayBatDau = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start),
                MaKhoa = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt32(3 + start)
            };
            return _mapper.Map<LopDto>(lop);
        }

        public async Task<LopDto> SelectOne(int ma_lop)
        {
            using DatabaseReader sql = new("Lop_SelectOne");

            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);

            using var dataReader = await sql.ExecuteReaderAsync();
            LopDto lop = new();

            if (await dataReader!.ReadAsync())
            {
                lop = GetProperty(dataReader);
            }

            return lop;
        }
        public async Task<int> Insert(string ten_lop, DateTime ngay_bat_dau, int ma_khoa)
        {
            using DatabaseReader sql = new("Lop_Insert");

            sql.SqlParams("@TenLop", SqlDbType.NVarChar, ten_lop);
            sql.SqlParams("@NgayBatDau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@MaKhoa", SqlDbType.Int, ma_khoa);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_lop, string ten_lop, DateTime ngay_bat_dau, int ma_khoa)
        {
            using DatabaseReader sql = new("Lop_Insert");

            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@TenLop", SqlDbType.NVarChar, ten_lop);
            sql.SqlParams("@NgayBatDau", SqlDbType.DateTime, ngay_bat_dau);
            sql.SqlParams("@MaKhoa", SqlDbType.Int, ma_khoa);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_lop)
        {
            using DatabaseReader sql = new("Lop_Remove");

            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_lop)
        {
            using DatabaseReader sql = new("Lop_ForceRemove");

            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<LopDto> SelectBy_ten_lop(string ten_lop)
        {
            using DatabaseReader sql = new("Lop_SelectBy_TenLop");

            sql.SqlParams("@TenLop", SqlDbType.NVarChar, ten_lop);

            using var dataReader = await sql.ExecuteReaderAsync();
            LopDto lop = new();

            if (await dataReader!.ReadAsync())
            {
                lop = GetProperty(dataReader);
            }

            return lop;
        }

        public async Task<Paged<LopDto>> SelectBy_ma_khoa_Paged(int ma_khoa, int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("Lop_SelectBy_MaKhoa_Paged");

            sql.SqlParams("@MaKhoa", SqlDbType.Int, ma_khoa);
            sql.SqlParams("@pageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@pageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<LopDto> result = [];
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

            return new Paged<LopDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

    }
}
