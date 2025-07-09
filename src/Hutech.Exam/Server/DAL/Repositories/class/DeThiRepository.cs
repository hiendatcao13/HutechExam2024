using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DeThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class DeThiRepository(IMapper mapper) : IDeThiRepository
    {

        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 11; // số lượng cột trong bảng DeThi

        public DeThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            DeThi deThi = new()
            {
                MaDeThi = dataReader.GetInt64(0 + start),
                MaMonHoc = dataReader.GetInt32(1 + start),
                TenDeThi = dataReader.GetString(2 + start),
                Guid = dataReader.GetGuid(3 + start),
                KyHieuDe = dataReader.GetString(4 + start),
                NgayTao = dataReader.GetDateTime(5 + start),
            };
            return _mapper.Map<DeThiDto>(deThi);
        }

        public async Task<int> Insert(int ma_mon_hoc, string ten_de_thi, Guid guid, DateTime ngay_tao, string ky_hieu_de)
        {
            using DatabaseReader sql = new("DeThi_Insert");

            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@TenDeThi", SqlDbType.NVarChar, ten_de_thi);
            sql.SqlParams("@Guid", SqlDbType.UniqueIdentifier, guid);
            sql.SqlParams("@KyHieuDe", SqlDbType.VarChar, ky_hieu_de);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, ngay_tao);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(long ma_de_thi, int ma_mon_hoc, string ten_de_thi, Guid guid, string ky_hieu_de)
        {
            using DatabaseReader sql = new("DeThi_Update");

            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, ma_de_thi);
            sql.SqlParams("@MaMonHoc", SqlDbType.Int, ma_mon_hoc);
            sql.SqlParams("@TenDeThi", SqlDbType.NVarChar, ten_de_thi);
            sql.SqlParams("@Guid", SqlDbType.UniqueIdentifier, guid);
            sql.SqlParams("@KyHieuDe", SqlDbType.VarChar, ky_hieu_de);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task Save_Batch(List<DeThiDto> deThis)
        {
            var dt = DeThiHelper.ToDataTable(deThis);
            using DatabaseReader sql = new("DeThi_Save_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);

            await sql.ExecuteNonQueryAsync();
        }

        public async Task<bool> Delete(long ma_de_thi)
        {
            using DatabaseReader sql = new("DeThi_Delete");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceDelete(long ma_de_thi)
        {
            DatabaseReader sql = new("DeThi_ForceDelete");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }


        public async Task<DeThiDto> SelectOne(long ma_de_thi)
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
