using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CaThiRepository(IMapper mapper) : ICaThiRepository
    {
        private readonly IMapper _mapper = mapper;
        public static readonly int COLUMN_LENGTH = 15; // số lượng cột trong bảng CaThi

        public CaThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CaThi caThi = new()
            {
                MaCaThi = dataReader.GetInt32(0 + start),
                TenCaThi = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                MaChiTietDotThi = dataReader.GetInt32(2 + start),
                ThoiGianBatDau = dataReader.GetDateTime(3 + start),
                MaDeThi = dataReader.GetInt32(4 + start),
                IsActivated = dataReader.GetBoolean(5 + start),
                ActivatedDate = dataReader.IsDBNull(6 + start) ? null : dataReader.GetDateTime(6 + start),
                ThoiGianThi = dataReader.GetInt32(7 + start),
                KetThuc = dataReader.GetBoolean(8 + start),
                ThoiDiemKetThuc = dataReader.IsDBNull(9 + start) ? null : dataReader.GetDateTime(9 + start),
                MatMa = dataReader.IsDBNull(10 + start) ? null : dataReader.GetString(10 + start),
                Approved = dataReader.GetBoolean(11 + start),
                ApprovedDate = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                ApprovedComments = dataReader.IsDBNull(13 + start) ? null : dataReader.GetString(13 + start),
                LichSuHoatDong = dataReader.IsDBNull(14 + start) ? string.Empty : dataReader.GetString(14 + start),
            };
            return _mapper.Map<CaThiDto>(caThi);
        }

        public async Task<List<CaThiDto>> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi)
        {
            List<CaThiDto> result = [];

            using DatabaseReader sql = new("ca_thi_SelectBy_MaDotThi_MaLop_LanThi");
            sql.SqlParams("@ma_dot_thi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@lan_thi", SqlDbType.Int, lan_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Paged(int ma_chi_tiet_dot_thi, int pageNumber, int pageSize)
        {
            List<CaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("ca_thi_SelectBy_ma_chi_tiet_dot_thi_Paged");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                CaThiDto caThi = GetProperty(dataReader);
                // thêm trường số lượng SV
                caThi.TongSV = dataReader.GetInt32(COLUMN_LENGTH);
                result.Add(caThi);
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

            return new Paged<CaThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<Paged<CaThiDto>> SelectBy_ma_chi_tiet_dot_thi_Search_Paged(int ma_chi_tiet_dot_thi, string keyword, int pageNumber, int pageSize)
        {
            List<CaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("ca_thi_SelectBy_ma_chi_tiet_dot_thi_Search_Paged");
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                CaThiDto caThi = GetProperty(dataReader);
                // thêm trường số lượng SV
                caThi.TongSV = dataReader.GetInt32(COLUMN_LENGTH);
                result.Add(caThi);
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

            return new Paged<CaThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<CaThiDto> SelectOne(int ma_ca_thi)
        {
            CaThiDto result = new();

            using DatabaseReader sql = new("ca_thi_SelectOne");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (dataReader != null && dataReader.Read())
            {
                result = GetProperty(dataReader);
            }

            return result;
        }

        public async Task<List<CaThiDto>> GetAll()
        {
            List<CaThiDto> result = [];

            using DatabaseReader sql = new("ca_thi_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                CaThiDto caThi = GetProperty(dataReader);
                result.Add(caThi);
            }

            return result;
        }

        public async Task<bool> Activate(int ma_ca_thi, bool IsActivated, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("ca_thi_Activate");

            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@IsActivated", SqlDbType.Bit, IsActivated);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> HuyKichHoat(int ma_ca_thi, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("ca_thi_HuyKichHoat");

            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> Ketthuc(int ma_ca_thi, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("ca_thi_Ketthuc");

            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<int> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi, string mat_ma)
        {
            using DatabaseReader sql = new("ca_thi_Insert");

            sql.SqlParams("@ten_ca_thi", SqlDbType.NVarChar, ten_ca_thi);
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@ThoiGianThi", SqlDbType.Int, thoi_gian_thi);
            sql.SqlParams("@MatMa", SqlDbType.NVarChar, mat_ma);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }
        public async Task<bool> Remove(int ma_ca_thi)
        {
            using DatabaseReader sql = new("ca_thi_Remove");

            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_ca_thi)
        {
            using DatabaseReader sql = new("ca_thi_ForceRemove");

            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi, string mat_ma)
        {
            using DatabaseReader sql = new("ca_thi_Update");

            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@ten_ca_thi", SqlDbType.NVarChar, ten_ca_thi);
            sql.SqlParams("@ma_chi_tiet_dot_thi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@thoi_gian_bat_dau", SqlDbType.DateTime, thoi_gian_bat_dau);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@ThoiGianThi", SqlDbType.Int, thoi_gian_thi);
            sql.SqlParams("@MatMa", SqlDbType.NVarChar, mat_ma);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateDeThi(int ma_ca_thi, int ma_de_thi, bool isOrderMSSV, string lichSuHoatDong, List<long> dsDeThiHVs)
        {
            using DatabaseReader sql = new("ca_thi_UpdateDeThi");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@IsOrderMSSV", SqlDbType.Bit, isOrderMSSV);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);
            sql.SqlParams("@DsDeThiHoanVi", SqlDbType.NVarChar, string.Join(",", dsDeThiHVs));

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DuyetDe(int ma_ca_thi, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("ca_thi_DuyetDe");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);
            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateLichSuHoatDong(int ma_ca_thi, string lichSuHoatDong)
        {
            using DatabaseReader sql = new("ca_thi_UpdateLichSuHoatDong");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@LichSuHoatDong", SqlDbType.NVarChar, lichSuHoatDong);
            return await sql.ExecuteNonQueryAsync() > 0;

        }

        public async Task<bool> UpdateAllResetLogin(int ma_ca_thi)
        {
            using DatabaseReader sql = new("ca_thi_UpdateAllResetLogin");
            sql.SqlParams("@ma_ca_thi", SqlDbType.Int, ma_ca_thi);
            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
