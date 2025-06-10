using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class KhoaRepository(IMapper mapper) : IKhoaRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng Khoa

        public KhoaDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Khoa khoa = new()
            {
                MaKhoa = dataReader.GetInt32(0 + start),
                TenKhoa = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayThanhLap = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start)
            };
            return _mapper.Map<KhoaDto>(khoa);
        }

        public async Task<KhoaDto> SelectOne(int ma_khoa)
        {
            using DatabaseReader sql = new("khoa_SelectOne");

            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);

            using var dataReader = await sql.ExecuteReaderAsync();
            KhoaDto khoa = new();

            if (dataReader != null && dataReader.Read())
            {
                khoa = GetProperty(dataReader);
            }

            return khoa;
        }
        public async Task<int> Insert(string ten_khoa, DateTime ngay_thanh_lap)
        {
            using DatabaseReader sql = new("khoa_Insert");

            sql.SqlParams("@ten_khoa", SqlDbType.NVarChar, ten_khoa);
            sql.SqlParams("@ngay_thanh_lap", SqlDbType.DateTime, ngay_thanh_lap);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_khoa, string ten_khoa, DateTime ngay_thanh_lap)
        {
            using DatabaseReader sql = new("khoa_Update");

            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);
            sql.SqlParams("@ten_khoa", SqlDbType.NVarChar, ten_khoa);
            sql.SqlParams("@ngay_thanh_lap", SqlDbType.DateTime, ngay_thanh_lap);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_khoa)
        {
            using DatabaseReader sql = new("khoa_Remove");

            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_khoa)
        {
            using DatabaseReader sql = new("khoa_ForceRemove");

            sql.SqlParams("@ma_khoa", SqlDbType.Int, ma_khoa);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<List<KhoaDto>> GetAll()
        {
            using DatabaseReader sql = new("khoa_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<KhoaDto> results = [];

            while (dataReader != null && dataReader.Read())
            {
                results.Add(GetProperty(dataReader));
            }

            return results;
        }

        public async Task<Paged<KhoaDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("khoa_GetAll_Paged");

            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<KhoaDto> results = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (dataReader != null && dataReader.Read())
            {
                results.Add(GetProperty(dataReader));
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

            return new Paged<KhoaDto> { Data = results, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }
    }
}
