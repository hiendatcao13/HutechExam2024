using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Audit;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CaThiRepository(IMapper mapper) : ICaThiRepository
    {
        private readonly IMapper _mapper = mapper;
        public static readonly int COLUMN_LENGTH = 13; // số lượng cột trong bảng CaThi

        public CaThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CaThi caThi = new()
            {
                MaCaThi = dataReader.GetInt32(0 + start),
                TenCaThi = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                MaChiTietDotThi = dataReader.GetInt32(2 + start),
                ThoiGianBatDau = dataReader.GetDateTime(3 + start),
                DaGanDe = dataReader.GetBoolean(4 + start),
                KichHoat = dataReader.GetBoolean(5 + start),
                ThoiGianKichHoat = dataReader.IsDBNull(6 + start) ? null : dataReader.GetDateTime(6 + start),
                ThoiGianThi = dataReader.GetInt32(7 + start),
                KetThuc = dataReader.GetBoolean(8 + start),
                ThoiDiemKetThuc = dataReader.IsDBNull(9 + start) ? null : dataReader.GetDateTime(9 + start),
                MatMa = dataReader.IsDBNull(10 + start) ? null : dataReader.GetString(10 + start),
                DaDuyet = dataReader.GetBoolean(11 + start),
                LichSuHoatDong = dataReader.IsDBNull(12 + start) ? null : dataReader.GetString(12 + start),
            };
            return _mapper.Map<CaThiDto>(caThi);
        }

        public async Task<List<CaThiDto>> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi)
        {
            List<CaThiDto> result = [];

            using DatabaseReader sql = new("CaThi_SelectBy_MaDotThi_MaLop_LanThi");
            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@LanThi", SqlDbType.Int, lan_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            List<CaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("CaThi_SelectBy_MaChiTietDotThi_Paged");
            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                CaThiDto caThi = GetProperty(dataReader);
                // thêm trường số lượng SV
                caThi.TongSV = dataReader.GetInt32(COLUMN_LENGTH);
                result.Add(caThi);
            }

            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader!.NextResult())
            {
                while (await dataReader.ReadAsync())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<CaThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            List<CaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("CaThi_SelectBy_MaChiTietDotThi_Search_Paged");
            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                CaThiDto caThi = GetProperty(dataReader);
                // thêm trường số lượng SV
                caThi.TongSV = dataReader.GetInt32(COLUMN_LENGTH);
                result.Add(caThi);
            }

            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader!.NextResult())
            {
                while (await dataReader.ReadAsync())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<CaThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<CaThiDto> SelectOne(int ma_ca_thi)
        {
            CaThiDto result = new();

            using DatabaseReader sql = new("CaThi_SelectOne");
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                result = GetProperty(dataReader);
                result.TongSV = dataReader.GetInt32(COLUMN_LENGTH);
            }

            return result;
        }

        public async Task<List<CaThiDto>> GetAll()
        {
            List<CaThiDto> result = [];

            using DatabaseReader sql = new("CaThi_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                CaThiDto caThi = GetProperty(dataReader);
                result.Add(caThi);
            }

            return result;
        }

        public async Task<bool> KichHoat(int ma_ca_thi, bool kich_hoat, string actionHistory)
        {
            using DatabaseReader sql = new("CaThi_KichHoat");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@KichHoat", SqlDbType.Bit, kich_hoat);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, actionHistory);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> HuyKichHoat(int ma_ca_thi, string actionHistory)
        {
            using DatabaseReader sql = new("CaThi_HuyKichHoat");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, actionHistory);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> Ketthuc(int ma_ca_thi, string actionHistory)
        {
            using DatabaseReader sql = new("CaThi_Ketthuc");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, actionHistory);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<int> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int thoi_gian_thi, string mat_ma)
        {
            using DatabaseReader sql = new("CaThi_Insert");

            sql.SqlParams("@TenCaThi", SqlDbType.NVarChar, ten_ca_thi);
            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@ThoiGianBatDau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@ThoiGianThi", SqlDbType.Int, thoi_gian_thi);
            sql.SqlParams("@MatMa", SqlDbType.NVarChar, mat_ma);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }
        public async Task<bool> Remove(int ma_ca_thi)
        {
            using DatabaseReader sql = new("CaThi_Remove");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_ca_thi)
        {
            using DatabaseReader sql = new("CaThi_ForceRemove");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int thoi_gian_thi, string mat_ma)
        {
            using DatabaseReader sql = new("CaThi_Update");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@TenCaThi", SqlDbType.NVarChar, ten_ca_thi);
            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@ThoiGianBatDau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@ThoiGianThi", SqlDbType.Int, thoi_gian_thi);
            sql.SqlParams("@MatMa", SqlDbType.NVarChar, mat_ma);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAllResetLogin(int maCaThi)
        {
            using DatabaseReader sql = new("CaThi_UpdateAllResetLogin");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, maCaThi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateDeThi(int ma_ca_thi, bool isOrderMSSV,string lichSuHoatDong, List<long> dsDeThis)
        {
            using DatabaseReader sql = new("CaThi_UpdateDeThi");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@XepMSSV", SqlDbType.Bit, isOrderMSSV);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);
            sql.SqlParams("@DsDeThi", SqlDbType.NVarChar, string.Join(",", dsDeThis));

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateLichSuHoatDong(int ma_ca_thi, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("CaThi_UpdateLichSuHoatDong");
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);
            return await sql.ExecuteNonQueryAsync() > 0;

        }

        public async Task<bool> DuyetDe(int ma_ca_thi, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("CaThi_DuyetDe");
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);
            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
