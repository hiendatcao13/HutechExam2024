using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class SinhVienRepository(IMapper mapper) : ISinhVienRepository
    {
        #region Private Fields
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 15; // số lượng cột trong bảng SinhVien

        #endregion

        public SinhVienDto GetProperty(IDataReader dataReader, int start = 0)
        {
            SinhVien sv = new()
            {
                MaSinhVien = dataReader.GetInt64(0 + start),
                HoVaTenLot = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                TenSinhVien = dataReader.IsDBNull(2 + start) ? null : dataReader.GetString(2 + start),
                GioiTinh = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt16(3 + start),
                NgaySinh = dataReader.IsDBNull(4 + start) ? null : dataReader.GetDateTime(4 + start),
                MaLop = dataReader.IsDBNull(5 + start) ? null : dataReader.GetInt32(5 + start),
                DiaChi = dataReader.IsDBNull(6 + start) ? null : dataReader.GetString(6 + start),
                Email = dataReader.IsDBNull(7 + start) ? null : dataReader.GetString(7 + start),
                DienThoai = dataReader.IsDBNull(8 + start) ? null : dataReader.GetString(8 + start),
                MaSoSinhVien = dataReader.IsDBNull(9 + start) ? null : dataReader.GetString(9 + start),
                StudentId = dataReader.IsDBNull(10 + start) ? null : dataReader.GetGuid(10 + start),
                IsLoggedIn = dataReader.IsDBNull(11 + start) ? null : dataReader.GetBoolean(11 + start),
                LastLoggedIn = dataReader.IsDBNull(12 + start) ? null : dataReader.GetDateTime(12 + start),
                LastLoggedOut = dataReader.IsDBNull(13 + start) ? null : dataReader.GetDateTime(13 + start),
                Photo = null // chưa biết cách xử lí (image === byte)
            };
            return _mapper.Map<SinhVienDto>(sv);
        }

        public async Task<long> Insert(string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh, DateTime? ngay_sinh, int? ma_lop,
            string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien, Guid? student_id)
        {
            using DatabaseReader sql = new("SinhVien_Insert");

            sql.SqlParams("@HoVaTenLot", SqlDbType.NVarChar, ho_va_ten_lot ?? (object)DBNull.Value);
            sql.SqlParams("@TenSinhVien", SqlDbType.NVarChar, ten_sinh_vien ?? (object)DBNull.Value);
            sql.SqlParams("@GioiTinh", SqlDbType.SmallInt, gioi_tinh ?? (object)DBNull.Value);
            sql.SqlParams("@NgaySinh", SqlDbType.DateTime, ngay_sinh ?? (object)DBNull.Value);
            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop ?? (object)DBNull.Value);
            sql.SqlParams("@DiaChi", SqlDbType.NVarChar, dia_chi ?? (object)DBNull.Value);
            sql.SqlParams("@Email", SqlDbType.NVarChar, email ?? (object)DBNull.Value);
            sql.SqlParams("@DienThoai", SqlDbType.NVarChar, dien_thoai ?? (object)DBNull.Value);
            sql.SqlParams("@MaSoSinhVien", SqlDbType.NVarChar, ma_so_sinh_vien ?? (object)DBNull.Value);
            sql.SqlParams("@Guid", SqlDbType.UniqueIdentifier, student_id ?? (object)DBNull.Value);

            return Convert.ToInt64(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task Insert_Batch(List<SinhVienDto> sinhVienDtos)
        {
            var dt = SinhVienHelper.ToDataTable(sinhVienDtos);
            using DatabaseReader sql = new("SinhVien_Insert_Batch");
            sql.SqlParams("@DanhSachSinhVien", SqlDbType.Structured, dt);

            await sql.ExecuteNonQueryAsync();
        }
        public async Task<bool> Update(long ma_sinh_vien, string? ho_va_ten_lot, string? ten_sinh_vien, int? gioi_tinh,
            DateTime? ngay_sinh, int? ma_lop, string? dia_chi, string? email, string? dien_thoai, string? ma_so_sinh_vien)
        {
            using DatabaseReader sql = new("SinhVien_Update");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@HoVaTenLot", SqlDbType.NVarChar, NormalizeString(ho_va_ten_lot) ?? (object)DBNull.Value);
            sql.SqlParams("@TenSinhVien", SqlDbType.NVarChar, NormalizeString(ten_sinh_vien) ?? (object)DBNull.Value);
            sql.SqlParams("@GioiTinh", SqlDbType.SmallInt, gioi_tinh ?? (object)DBNull.Value);
            sql.SqlParams("@NgaySinh", SqlDbType.DateTime, ngay_sinh ?? (object)DBNull.Value);
            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop ?? (object)DBNull.Value);
            sql.SqlParams("@DiaChi", SqlDbType.NVarChar, NormalizeString(dia_chi) ?? (object)DBNull.Value);
            sql.SqlParams("@Email", SqlDbType.NVarChar, NormalizeString(email) ?? (object)DBNull.Value);
            sql.SqlParams("@DienThoai", SqlDbType.NVarChar, NormalizeString(dien_thoai) ?? (object)DBNull.Value);
            sql.SqlParams("@MaSoSinhVien", SqlDbType.NVarChar, ma_so_sinh_vien ?? (object)DBNull.Value);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> Remove(long ma_sinh_vien)
        {
            using DatabaseReader sql = new("SinhVien_Remove");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(long ma_sinh_vien)
        {
            using DatabaseReader sql = new("SinhVien_ForceRemove");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        // lấy thông tin của 1 SV từ maSV
        public async Task<SinhVienDto> SelectOne(long ma_sinh_vien)
        {
            using DatabaseReader sql = new("SinhVien_SelectOne");
            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);
            using var dataReader = await sql.ExecuteReaderAsync();
            SinhVienDto sv = new();

            if (await dataReader!.ReadAsync())
            {
                sv = GetProperty(dataReader);
            }

            return sv;
        }

        // lấy mã SV từ mã số SV
        public async Task<SinhVienDto> SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien)
        {
            using DatabaseReader sql = new("SinhVien_SelectBy_MaSoSinhVien");

            sql.SqlParams("@MaSoSinhVien", SqlDbType.NVarChar, ma_so_sinh_vien);

            using var dataReader = await sql.ExecuteReaderAsync();
            SinhVienDto sv = new();

            if (await dataReader!.ReadAsync())
            {
                sv = GetProperty(dataReader);
            }

            return sv;
        }

        public async Task<List<SinhVienDto>> GetAll()
        {
            using DatabaseReader sql = new("SinhVien_GetAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<SinhVienDto> result = [];
            while (await dataReader!.ReadAsync())
            {
                SinhVienDto sv = GetProperty(dataReader);
                result.Add(sv);
            }

            return result;
        }

        public async Task<bool> Login(long ma_sinh_vien, DateTime last_log_in)
        {
            using DatabaseReader sql = new("SinhVien_Login");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@ThoiGianDangNhap", SqlDbType.DateTime, last_log_in);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Logout(long ma_sinh_vien, DateTime last_log_out)
        {
            using DatabaseReader sql = new("SinhVien_Logout");

            sql.SqlParams("@MaSinhVien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@ThoiGianDangXuat", SqlDbType.DateTime, last_log_out);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<Paged<SinhVienDto>> SelectBy_ma_lop_Search_Paged(int ma_lop, string keyword, int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("SinhVien_SelectBy_MaLop_Search_Paged");

            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@Keyword", SqlDbType.NVarChar, keyword);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<SinhVienDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                SinhVienDto sv = GetProperty(dataReader);
                result.Add(sv);
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
            return new Paged<SinhVienDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<Paged<SinhVienDto>> SelectBy_ma_lop_Paged(int ma_lop, int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("SinhVien_SelectBy_MaLop_Paged");

            sql.SqlParams("@MaLop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<SinhVienDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (await dataReader!.ReadAsync())
            {
                SinhVienDto sv = GetProperty(dataReader);
                result.Add(sv);
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

            return new Paged<SinhVienDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }

        public async Task<long> LoginCount()
        {
            using DatabaseReader sql = new("SinhVien_LoginCount");

            using var dataReader = await sql.ExecuteReaderAsync();

            if(await dataReader!.ReadAsync())
            {
                return dataReader.GetInt64(0);
            }

            return 0;
        }






        private string? NormalizeString(string? input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }
    }
}
