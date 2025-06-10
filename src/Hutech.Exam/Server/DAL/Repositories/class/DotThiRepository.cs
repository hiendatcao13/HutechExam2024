using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DotThiRepository(IMapper mapper) : IDotThiRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 5; // số lượng cột trong bảng DotThi

        public DotThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            DotThi dotThi = new()
            {
                MaDotThi = dataReader.GetInt32(0 + start),
                TenDotThi = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                ThoiGianBatDau = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start),
                ThoiGianKetThuc = dataReader.IsDBNull(3 + start) ? null : dataReader.GetDateTime(3 + start),
                NamHoc = dataReader.IsDBNull(4 + start) ? null : dataReader.GetInt32(4 + start)
            };
            return _mapper.Map<DotThiDto>(dotThi);
        }

        public async Task<List<DotThiDto>> GetAll()
        {
            using DatabaseReader sql = new("dot_thi_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<DotThiDto> result = [];

            while (dataReader != null && dataReader.Read())
            {
                DotThiDto dotThi = GetProperty(dataReader);
                result.Add(dotThi);
            }

            return result;
        }

        public async Task<Paged<DotThiDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("dot_thi_GetAll_Paged");

            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<DotThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (dataReader != null && dataReader.Read())
            {
                DotThiDto dotThi = GetProperty(dataReader);
                result.Add(dotThi);
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

            return new Paged<DotThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<DotThiDto> SelectOne(int ma_dot_thi)
        {
            using DatabaseReader sql = new("dot_thi_SelectOne");

            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            DotThiDto dotThi = new();

            if (dataReader != null && dataReader.Read())
            {
                dotThi = GetProperty(dataReader);
            }

            return dotThi;
        }

        public async Task<int> Insert(string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc)
        {
            DatabaseReader sql = new("dot_thi_Insert");
            sql.SqlParams("@ten_dot_thi", SqlDbType.NVarChar, ten_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@thoi_gian_ket_thuc", SqlDbType.DateTime, thoi_gian_ket_thuc);
            sql.SqlParams("@NamHoc", SqlDbType.Int, nam_hoc);
            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_dot_thi, string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc)
        {
            using DatabaseReader sql = new("dot_thi_Update");

            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ten_dot_thi", SqlDbType.NVarChar, ten_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@thoi_gian_ket_thuc", SqlDbType.DateTime, thoi_gian_ket_thuc);
            sql.SqlParams("@NamHoc", SqlDbType.Int, nam_hoc);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_dot_thi)
        {
            using DatabaseReader sql = new("dot_thi_Remove");

            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_dot_thi)
        {
            using DatabaseReader sql = new("dot_thi_ForceRemove");

            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
