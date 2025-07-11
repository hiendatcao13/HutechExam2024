using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietBaiThiRepository(IMapper mapper) : IChiTietBaiThiRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 11; // số lượng cột trong bảng ChiTietBaiThi

        public ChiTietBaiThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietBaiThi chiTietBaiThi = new()
            {
                MaChiTietBaiThi = dataReader.GetInt64(0 + start),
                MaChiTietCaThi = dataReader.GetInt32(1 + start),
                MaDeThi = dataReader.GetInt64(2 + start),
                MaNhom = dataReader.GetGuid(3 + start),
                MaCauHoi = dataReader.GetGuid(4 + start),
                CauTraLoi = dataReader.IsDBNull(5 + start) ? null : dataReader.GetGuid(5 + start),
                NgayTao = dataReader.GetDateTime(6 + start),
                NgayCapNhat = dataReader.IsDBNull(7 + start) ? null : dataReader.GetDateTime(7 + start),
                KetQua = dataReader.IsDBNull(8 + start) ? null : dataReader.GetBoolean(8 + start),
                ThuTu = dataReader.GetInt32(9 + start)
            };
            return _mapper.Map<ChiTietBaiThiDto>(chiTietBaiThi);
        }

        public async Task<int> Insert(int ma_chi_tiet_ca_thi, long MaDeThi, Guid MaNhom, Guid MaCauHoi, DateTime NgayTao, int ThuTu)
        {
            using DatabaseReader sql = new("ChiTietBaiThi_Insert");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, MaDeThi);
            sql.SqlParams("@MaNhom", SqlDbType.UniqueIdentifier, MaNhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.UniqueIdentifier, MaCauHoi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, NgayTao);
            sql.SqlParams("@ThuTu", SqlDbType.Int, ThuTu);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }

        public async Task Insert_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            var dt = ChiTietBaiThiHelper.ToDataTable(chiTietBaiThis);

            using DatabaseReader sql = new("ChiTietBaiThi_Insert_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);

            await sql.ExecuteNonQueryAsync();
        }

        public async Task<bool> Update(long MaChiTietBaiThi, Guid CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            using DatabaseReader sql = new("ChiTietBaiThi_Update");

            sql.SqlParams("@MaChiTietBaiThi", SqlDbType.BigInt, MaChiTietBaiThi);
            sql.SqlParams("@CauTraLoi", SqlDbType.UniqueIdentifier, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update_v2(int MaChiTietCaThi, Guid MaCauHoi, Guid CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            using DatabaseReader sql = new("ChiTietBaiThi_Update_v2");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, MaChiTietCaThi);
            sql.SqlParams("@MaCauHoi", SqlDbType.UniqueIdentifier, MaCauHoi);
            sql.SqlParams("@CauTraLoi", SqlDbType.UniqueIdentifier, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        // bản nâng cấp vừa insert vừa update
        public async Task<bool> Save(int MaChiTietCaThi, long MaDeThi, Guid MaNhom, Guid MaCauHoi, Guid CauTraLoi, DateTime NgayTao, DateTime NgayCapNhat, bool KetQua, int ThuTu)
        {
            using DatabaseReader sql = new("ChiTietBaiThi_Save");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, MaChiTietCaThi);
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, MaDeThi);
            sql.SqlParams("@MaNhom", SqlDbType.UniqueIdentifier, MaNhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.UniqueIdentifier, MaCauHoi);
            sql.SqlParams("@CauTraLoi", SqlDbType.UniqueIdentifier, CauTraLoi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, NgayTao);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);
            sql.SqlParams("@ThuTu", SqlDbType.Int, ThuTu);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task Save_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            var dt = ChiTietBaiThiHelper.ToDataTable(chiTietBaiThis);

            using DatabaseReader sql = new("ChiTietBaiThii_Save_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);
            await sql.ExecuteNonQueryAsync();
        }

        public async Task<List<ChiTietBaiThiDto>> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            List<ChiTietBaiThiDto> result = [];

            using DatabaseReader sql = new("ChiTietBaiThi_SelectBy_MaChiTietCaThi");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                ChiTietBaiThiDto chiTietBaiThi = GetProperty(dataReader);
                result.Add(chiTietBaiThi);
            }

            return result;
        }

        public async Task<bool> Delete(long ma_chi_tiet_bai_thi)
        {
            using DatabaseReader sql = new("ChiTietBaiThi_Delete");

            sql.SqlParams("@MaChiTietBaiThi", SqlDbType.BigInt, ma_chi_tiet_bai_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<ChiTietBaiThiDto> SelectOne_v2(int ma_chi_tiet_ca_thi, long maDeThi, Guid ma_nhom, Guid ma_cau_hoi)
        {
            ChiTietBaiThiDto result = new();

            using DatabaseReader sql = new("ChiTietBaiThi_SelectOne_v2");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeThi", SqlDbType.BigInt, maDeThi);
            sql.SqlParams("@MaNhom", SqlDbType.UniqueIdentifier, ma_nhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.UniqueIdentifier, ma_cau_hoi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (dataReader != null && dataReader.Read())
            {
                result = GetProperty(dataReader);
            }

            return result;
        }

    }
}
