using AutoMapper;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietDotThiRepository(ILopAoRepository lopAoRepository, IMonHocRepository monHocRepository, IMapper mapper) : IChiTietDotThiRepository
    {
        private readonly ILopAoRepository _lopAoRepository = lopAoRepository;
        private readonly IMonHocRepository _monHocRepository = monHocRepository;

        private readonly IMapper _mapper = mapper;
        public static readonly int COLUMN_LENGTH = 5; // số lượng cột trong bảng ChiTietDotThi

        public ChiTietDotThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietDotThi chiTietDotThi = new()
            {
                MaChiTietDotThi = dataReader.GetInt32(0 + start),
                TenChiTietDotThi = dataReader.GetString(1 + start),
                MaLopAo = dataReader.GetInt32(2 + start),
                MaDotThi = dataReader.GetInt32(3 + start),
                LanThi = dataReader.GetInt32(4 + start)
            };
            return _mapper.Map<ChiTietDotThiDto>(chiTietDotThi);
        }

        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi(int ma_dot_thi)
        {
            List<ChiTietDotThiDto> result = [];

            using DatabaseReader sql = new("ChiTietDotThi_SelectBy_MaDotThi");

            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                ChiTietDotThiDto chiTietDotThi = GetProperty(dataReader);
                chiTietDotThi.MaLopAoNavigation = _lopAoRepository.GetProperty(dataReader, COLUMN_LENGTH);
                chiTietDotThi.MaLopAoNavigation.MaMonHocNavigation = _monHocRepository.GetProperty(dataReader, COLUMN_LENGTH + LopAoRepository.COLUMN_LENGTH);
                result.Add(chiTietDotThi);
            }

            return result;
        }

        public async Task<Paged<ChiTietDotThiDto>> SelectBy_MaDotThi_Paged(int ma_dot_thi, int pageNumber, int pageSize)
        {
            List<ChiTietDotThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("ChiTietDotThi_SelectBy_MaDotThi_Paged");

            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                ChiTietDotThiDto chiTietDotThi = GetProperty(dataReader);
                chiTietDotThi.MaLopAoNavigation = _lopAoRepository.GetProperty(dataReader, COLUMN_LENGTH);
                chiTietDotThi.MaLopAoNavigation.MaMonHocNavigation = _monHocRepository.GetProperty(dataReader, COLUMN_LENGTH + LopAoRepository.COLUMN_LENGTH);
                result.Add(chiTietDotThi);
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

            return new Paged<ChiTietDotThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            List<ChiTietDotThiDto> result = [];

            using DatabaseReader sql = new("ChiTietDotThi_SelectBy_MaDotThi_MaLopAo");

            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@MaLopAo", SqlDbType.Int, ma_lop_ao);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<ChiTietDotThiDto> SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            ChiTietDotThiDto result = new();

            using DatabaseReader sql = new("ChiTietDotThi_SelectBy_MaDotThi_MaLopAo_LanThi");

            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@MaLopAo", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@LanThi", SqlDbType.NVarChar, lan_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                result = GetProperty(dataReader);
            }

            return result;
        }

        public async Task<ChiTietDotThiDto> SelectOne(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto result = new();

            using DatabaseReader sql = new("ChiTietDotThi_SelectOne");
            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                result = GetProperty(dataReader);
                result.MaLopAoNavigation = _lopAoRepository.GetProperty(dataReader, COLUMN_LENGTH);
                result.MaLopAoNavigation.MaMonHocNavigation = _monHocRepository.GetProperty(dataReader, COLUMN_LENGTH + LopAoRepository.COLUMN_LENGTH);
            }

            return result;
        }
        public async Task<List<ChiTietDotThiDto>> GetAll()
        {
            List<ChiTietDotThiDto> result = [];

            using DatabaseReader sql = new("ChiTietDotThi_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }
        public async Task<int> Insert(string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi)
        {
            using DatabaseReader sql = new("ChiTietDotThi_Insert");

            sql.SqlParams("@TenChiTietDotThi", SqlDbType.NVarChar, ten_chi_tiet_dot_thi);
            sql.SqlParams("@MaLopAo", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@LanThi", SqlDbType.NVarChar, lan_thi);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }
        public async Task<bool> Update(int ma_chi_tiet_dot_thi, string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi)
        {
            using DatabaseReader sql = new("ChiTietDotThi_Update");

            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);
            sql.SqlParams("@TenChiTietDotThi", SqlDbType.NVarChar, ten_chi_tiet_dot_thi);
            sql.SqlParams("@MaLopAo", SqlDbType.Int, ma_lop_ao);
            sql.SqlParams("@MaDotThi", SqlDbType.Int, ma_dot_thi);
            sql.SqlParams("@LanThi", SqlDbType.NVarChar, lan_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> Remove(int ma_chi_tiet_dot_thi)
        {
            using DatabaseReader sql = new("ChiTietDotThi_Remove");

            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_chi_tiet_dot_thi)
        {
            using DatabaseReader sql = new("ChiTietDotThi_ForceRemove");

            sql.SqlParams("@MaChiTietDotThi", SqlDbType.Int, ma_chi_tiet_dot_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
