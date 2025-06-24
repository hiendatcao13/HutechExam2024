using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DeThiRepository(IMapper mapper) : IDeThiRepository
    {

        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 10; // số lượng cột trong bảng DeThi

        public DeThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            DeThi deThi = new()
            {
                MaDeThi = dataReader.GetInt32(0 + start),
                MaMonHoc = dataReader.GetInt32(1 + start),
                TenDeThi = dataReader.GetString(2 + start),
                NgayTao = dataReader.GetDateTime(3 + start),
                NguoiTao = dataReader.GetInt32(4 + start),
                GhiChu = dataReader.IsDBNull(5 + start) ? null : dataReader.GetString(5 + start),
                LuuTam = dataReader.GetBoolean(6 + start),
                DaDuyet = dataReader.GetBoolean(7 + start),
                TongSoDeHoanVi = dataReader.IsDBNull(8 + start) ? null : dataReader.GetInt32(8 + start),
                BoChuongPhan = dataReader.GetBoolean(9 + start)
            };
            return _mapper.Map<DeThiDto>(deThi);
        }

        public async Task<int> Insert(int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan)
        {
            using DatabaseReader sql = new("DeThi_Insert");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@TenDeThi", SqlDbType.NVarChar, ten_de_thi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, ngay_tao);
            sql.SqlParams("@NguoiTao", SqlDbType.Int, nguoi_tao);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@BoChuongPhan", SqlDbType.Bit, bo_chuong_phan);
            sql.SqlParams("@BoChuongPhan", SqlDbType.Bit, bo_chuong_phan);
            sql.SqlParams("@BoChuongPhan", SqlDbType.Bit, bo_chuong_phan);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_de_thi, int ma_mon_hoc, string ten_de_thi, DateTime ngay_tao, int nguoi_tao, string ghi_chu, bool bo_chuong_phan, bool da_duyet, bool luu_tam)
        {
            using DatabaseReader sql = new("DeThi_Update");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@TenDeThi", SqlDbType.NVarChar, ten_de_thi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, ngay_tao);
            sql.SqlParams("@NguoiTao", SqlDbType.Int, nguoi_tao);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@BoChuongPhan", SqlDbType.Bit, bo_chuong_phan);
            sql.SqlParams("@DaDuyet", SqlDbType.Bit, da_duyet);
            sql.SqlParams("@LuuTam", SqlDbType.Bit, luu_tam);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Delete(int ma_de_thi)
        {
            using DatabaseReader sql = new("DeThi_Delete");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceDelete(int ma_de_thi)
        {
            DatabaseReader sql = new("DeThi_ForceDelete");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }


        public async Task<DeThiDto> SelectOne(int ma_de_thi)
        {
            using DatabaseReader sql = new("DeThi_SelectOne");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            DeThiDto deThi = new();

            if (dataReader != null && dataReader.Read())
            {
                deThi = GetProperty(dataReader);
            }

            return deThi;
        }

        public async Task<DeThiDto> SelectBy_ma_de_hv(long ma_de_hv)
        {
            using DatabaseReader sql = new("DeThi_SelectBy_ma_de_hv");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hv);

            using var dataReader = await sql.ExecuteReaderAsync();
            DeThiDto deThi = new();

            if (dataReader != null && dataReader.Read())
            {
                deThi = GetProperty(dataReader);
            }

            return deThi;
        }

        public async Task<List<DeThiDto>> GetAll()
        {
            using DatabaseReader sql = new("DeThi_SelectAll");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<DeThiDto> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<List<DeThiDto>> SelectByMonHoc(int ma_mon_hoc)
        {
            using DatabaseReader sql = new("DeThi_SelectByMonHoc");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<DeThiDto> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }

        public async Task<Paged<DeThiDto>> SelectByMonHoc_Paged(int ma_mon_hoc, int pageNumber, int pageSize)
        {
            using DatabaseReader sql = new("DeThi_SelectByMonHoc_Paged");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@PageNumber", SqlDbType.Int, pageNumber);
            sql.SqlParams("@PageSize", SqlDbType.Int, pageSize);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<DeThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;

            while (dataReader != null && dataReader.Read())
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

            return new Paged<DeThiDto> { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi };
        }
    }
}
