using AutoMapper;
using Hutech.Exam.Client.Pages.Result;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietCaThiRepository(ICaThiRepository caThiRepository, IChiTietDotThiRepository chiTietDotThiResposity, ILopAoRepository lopAoRepository, IMonHocRepository monHocRepository, ISinhVienRepository sinhVienRepository, IMapper mapper) : IChiTietCaThiRepository
    {
        private readonly ICaThiRepository _caThiRepository = caThiRepository;
        private readonly IChiTietDotThiRepository _chiTietDotThiRepository = chiTietDotThiResposity;
        private readonly ILopAoRepository _lopAoRepository = lopAoRepository;
        private readonly IMonHocRepository _monHocRepository = monHocRepository;
        private readonly ISinhVienRepository _sinhVienRepository = sinhVienRepository;

        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 12; // số lượng cột trong bảng ChiTietCaThi

        public ChiTietCaThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietCaThi chiTietCaThi = new()
            {
                MaChiTietCaThi = dataReader.GetInt32(0 + start),
                MaCaThi = dataReader.IsDBNull(1 + start) ? null : dataReader.GetInt32(1 + start),
                MaSinhVien = dataReader.IsDBNull(2 + start) ? null : dataReader.GetInt64(2 + start),
                MaDeThi = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt64(3 + start),
                ThoiGianBatDau = dataReader.IsDBNull(4 + start) ? null : dataReader.GetDateTime(4 + start),
                ThoiGianKetThuc = dataReader.IsDBNull(5 + start) ? null : dataReader.GetDateTime(5 + start),
                DaThi = dataReader.GetBoolean(6 + start),
                DaHoanThanh = dataReader.GetBoolean(7 + start),
                Diem = dataReader.GetDouble(8 + start),
                TongSoCau = dataReader.IsDBNull(9 + start) ? null : dataReader.GetInt32(9 + start),
                SoCauDung = dataReader.IsDBNull(10 + start) ? null : dataReader.GetInt32(10 + start),
                GioCongThem = dataReader.GetInt32(11 + start)
            };
            return _mapper.Map<ChiTietCaThiDto>(chiTietCaThi);
        }

        public async Task<ChiTietCaThiDto> SelectOne(int chi_tiet_ca_thi)
        {
            ChiTietCaThiDto result = new();
            using DatabaseReader sql = new("ChiTietCaThi_SelectOne");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, chi_tiet_ca_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                result = GetProperty(dataReader);
                result.MaSinhVienNavigation = _sinhVienRepository.GetProperty(dataReader, COLUMN_LENGTH);
            }

            return result;
        }

        public async Task<List<ChiTietCaThiDto>> SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            List<ChiTietCaThiDto> result = [];

            using DatabaseReader sql = new("ChiTietCaThi_SelectBy_MaSinhVien");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<List<ChiTietCaThiDto>> SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            List<ChiTietCaThiDto> result = [];

            using DatabaseReader sql = new("ChiTietCaThi_SelectBy_MaCaThi");
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (await dataReader!.ReadAsync())
            {
                ChiTietCaThiDto chiTietCaThi = GetProperty(dataReader);
                chiTietCaThi.MaSinhVienNavigation = _sinhVienRepository.GetProperty(dataReader, COLUMN_LENGTH);
                chiTietCaThi.KyHieuDe = dataReader.IsDBNull(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH) ? null : dataReader.GetString(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH);
                result.Add(chiTietCaThi);
            }

            return result;
        }
        public async Task<Paged<ChiTietCaThiDto>> SelectBy_ma_ca_thi_Search_Paged(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            List<ChiTietCaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("ChiTietCaThi_SelectBy_MaCaThi_Search_Paged");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                ChiTietCaThiDto chiTietCaThi = GetProperty(dataReader);
                chiTietCaThi.MaSinhVienNavigation = _sinhVienRepository.GetProperty(dataReader, COLUMN_LENGTH);
                chiTietCaThi.KyHieuDe = dataReader.IsDBNull(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH) ? null : dataReader.GetString(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH);
                chiTietCaThi.TenLop = dataReader.IsDBNull(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH + 1) ? null : dataReader.GetString(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH + 1);
                result.Add(chiTietCaThi);
            }

            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader!.NextResult())
            {
                while (dataReader.Read())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<ChiTietCaThiDto>() { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }
        public async Task<Paged<ChiTietCaThiDto>> SelectBy_ma_ca_thi_Paged(int ma_ca_thi, int pageNumber, int pageSize)
        {
            List<ChiTietCaThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            using DatabaseReader sql = new("ChiTietCaThi_SelectBy_MaCaThi_Paged");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                ChiTietCaThiDto chiTietCaThi = GetProperty(dataReader);
                chiTietCaThi.MaSinhVienNavigation = _sinhVienRepository.GetProperty(dataReader, COLUMN_LENGTH);
                chiTietCaThi.KyHieuDe = dataReader.IsDBNull(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH) ? null : dataReader.GetString(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH);
                chiTietCaThi.TenLop = dataReader.IsDBNull(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH + 1) ? null : dataReader.GetString(COLUMN_LENGTH + SinhVienRepository.COLUMN_LENGTH + 1);
                result.Add(chiTietCaThi);
            }

            //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
            if (dataReader!.NextResult())
            {
                while (dataReader.Read())
                {
                    tong_so_ban_ghi = dataReader.GetInt32(0);
                    tong_so_trang = dataReader.GetInt32(1);
                }
            }

            return new Paged<ChiTietCaThiDto>() { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }
        public async Task<ChiTietCaThiDto> SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien)
        {
            ChiTietCaThiDto result = new();

            using DatabaseReader sql = new("ChiTietCaThi_SelectBy_MaCaThi_MaSinhVien");
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                result = GetProperty(dataReader);
            }

            return result;
        }
        public async Task<ChiTietCaThiDto> SelectBy_MaSinhVienThi(long ma_sinh_vien)
        {
            int ca_thi_column = CaThiRepository.COLUMN_LENGTH, chi_dot_thi_column = ChiTietDotThiRepository.COLUMN_LENGTH, lop_ao_column = LopAoRepository.COLUMN_LENGTH, mon_hoc_column = MonHocRepository.COLUMN_LENGTH, sinh_vien_column = SinhVienRepository.COLUMN_LENGTH;
            ChiTietCaThiDto result = new();

            using DatabaseReader sql = new("ChiTietCaThi_SelectBy_MaSinhVienThi");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (await dataReader!.ReadAsync())
            {
                result = GetProperty(dataReader);
                result.MaCaThiNavigation = _caThiRepository.GetProperty(dataReader, COLUMN_LENGTH);
                result.MaCaThiNavigation.MaChiTietDotThiNavigation = _chiTietDotThiRepository.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column);
                result.MaCaThiNavigation.MaChiTietDotThiNavigation.MaLopAoNavigation = _lopAoRepository.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column);
                result.MaCaThiNavigation.MaChiTietDotThiNavigation.MaLopAoNavigation.MaMonHocNavigation = _monHocRepository.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column + lop_ao_column);
                result.MaSinhVienNavigation = _sinhVienRepository.GetProperty(dataReader, COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column + lop_ao_column + mon_hoc_column);
                result.KyHieuDe = dataReader.IsDBNull(COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column + lop_ao_column + mon_hoc_column + sinh_vien_column) ? null : dataReader.GetString(COLUMN_LENGTH + ca_thi_column + chi_dot_thi_column + lop_ao_column + mon_hoc_column + sinh_vien_column);
            }

            return result;
        }
        public async Task<bool> UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_bat_dau)
        {
            using DatabaseReader sql = new("ChiTietCaThi_UpdateBatDau");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@ThoiGianBatDau", SqlDbType.DateTime, thoi_gian_bat_dau ?? (Object)DBNull.Value);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> UpdateKetThuc(int ma_chi_tiet_ca_thi, DateTime? thoi_gian_ket_thuc, double diem, int? so_cau_dung, int? tong_so_cau)
        {
            using DatabaseReader sql = new("ChiTietCaThi_UpdateKetThuc");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@ThoiGianKetThuc", SqlDbType.DateTime, thoi_gian_ket_thuc ?? (Object)DBNull.Value);
            sql.SqlParams("@Diem", SqlDbType.Float, diem);
            sql.SqlParams("@SoCauDung", SqlDbType.Int, so_cau_dung ?? (Object)DBNull.Value);
            sql.SqlParams("@TongSoCau", SqlDbType.Int, tong_so_cau ?? (Object)DBNull.Value);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> CongGio(int ma_chi_tiet_ca_thi, int gio_cong_them)
        {
            using DatabaseReader sql = new("ChiTietCaThi_CongGio");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@GioCongThem", SqlDbType.Int, gio_cong_them);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<int> Insert(int ma_ca_thi, long ma_sinh_vien, long ma_de_thi, int tong_so_cau)
        {
            using DatabaseReader sql = new("ChiTietCaThi_Insert");

            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, ma_de_thi);
            sql.SqlParams("@TongSoCau", SqlDbType.Int, tong_so_cau);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }

        public async Task Insert_Batch(List<ChiTietCaThiCreateBatchRequest> chiTietCaThis)
        {
            var dt = SinhVienCaThiHelper.ToDataTable(chiTietCaThis);

            using DatabaseReader sql = new("ChiTietCaThi_Insert_Batch");
            sql.SqlParams("@DanhSachSinhVienCaThi", SqlDbType.Structured, dt);

            await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> ThemSVKhanCap(string ma_so_sinh_vien, int ma_ca_thi, long ma_de_thi)
        {
            using DatabaseReader sql = new("ChiTietCaThi_ThemSVKhanCap");

            sql.SqlParams("@MaSoSinhVien", SqlDbType.NVarChar, ma_so_sinh_vien);
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi);
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, ma_de_thi);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }

        public async Task<bool> Remove(int ma_chi_tiet_ca_thi)
        {
            using DatabaseReader sql = new("ChiTietCaThi_Remove");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_chi_tiet_ca_thi)
        {
            using DatabaseReader sql = new("ChiTietCaThi_ForceRemove");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update(int ma_chi_tiet_ca_thi, int? ma_ca_thi, long? ma_sinh_vien, long? ma_de_thi, int? tong_so_cau)
        {
            using DatabaseReader sql = new("ChiTietCaThi_Update");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaCaThi", SqlDbType.Int, ma_ca_thi ?? (Object)DBNull.Value);
            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien ?? (Object)DBNull.Value);
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, ma_de_thi ?? (Object)DBNull.Value);
            sql.SqlParams("@TongSoCau", SqlDbType.Int, tong_so_cau ?? (Object)DBNull.Value);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
