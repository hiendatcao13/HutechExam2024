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
                MaDeHv = dataReader.GetInt64(2 + start),
                MaNhom = dataReader.GetInt32(3 + start),
                MaClo = dataReader.GetInt32(4 + start),
                MaCauHoi = dataReader.GetInt32(5 + start),
                CauTraLoi = dataReader.IsDBNull(6 + start) ? null : dataReader.GetInt32(6 + start),
                NgayTao = dataReader.GetDateTime(7 + start),
                NgayCapNhat = dataReader.IsDBNull(8 + start) ? null : dataReader.GetDateTime(8 + start),
                KetQua = dataReader.IsDBNull(9 + start) ? null : dataReader.GetBoolean(9 + start),
                ThuTu = dataReader.GetInt32(10 + start)
            };
            return _mapper.Map<ChiTietBaiThiDto>(chiTietBaiThi);
        }

        public async Task<int> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, DateTime NgayTao, int ThuTu)
        {
            using DatabaseReader sql = new("chi_tiet_bai_thi_Insert");

            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, MaDeHV);
            sql.SqlParams("@MaNhom", SqlDbType.Int, MaNhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, MaCauHoi);
            sql.SqlParams("@MaCLO", SqlDbType.Int, MaClo);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, NgayTao);
            sql.SqlParams("@ThuTu", SqlDbType.Int, ThuTu);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }

        public async Task Insert_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            var dt = ChiTietBaiThiHelper.ToDataTable(chiTietBaiThis);

            using DatabaseReader sql = new("chi_tiet_bai_thi_Insert_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);

            await sql.ExecuteNonQueryAsync();
        }

        public async Task<bool> Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            using DatabaseReader sql = new("chi_tiet_bai_thi_Update");

            sql.SqlParams("@MaChiTietBaiThi", SqlDbType.BigInt, MaChiTietBaiThi);
            sql.SqlParams("@CauTraLoi", SqlDbType.Int, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Update_v2(int MaChiTietCaThi, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            using DatabaseReader sql = new("chi_tiet_bai_thi_Update_v2");

            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, MaChiTietCaThi);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, MaCauHoi);
            sql.SqlParams("@MaCLO", SqlDbType.Int, MaClo);
            sql.SqlParams("@CauTraLoi", SqlDbType.Int, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        // bản nâng cấp vừa insert vừa update
        public async Task<bool> Save(int MaChiTietCaThi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayTao, DateTime NgayCapNhat, bool KetQua, int ThuTu)
        {
            using DatabaseReader sql = new("chi_tiet_bai_thi_Save");

            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, MaChiTietCaThi);
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, MaDeHV);
            sql.SqlParams("@MaNhom", SqlDbType.Int, MaNhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, MaCauHoi);
            sql.SqlParams("@MaCLO", SqlDbType.Int, MaClo);
            sql.SqlParams("@CauTraLoi", SqlDbType.Int, CauTraLoi);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, NgayTao);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);
            sql.SqlParams("@ThuTu", SqlDbType.Int, ThuTu);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task Save_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            var dt = ChiTietBaiThiHelper.ToDataTable(chiTietBaiThis);

            using DatabaseReader sql = new("chi_tiet_bai_thi_Save_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);
            await sql.ExecuteNonQueryAsync();
        }

        public async Task<List<ChiTietBaiThiDto>> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            List<ChiTietBaiThiDto> result = [];

            using DatabaseReader sql = new("chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi");

            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);

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
            using DatabaseReader sql = new("chi_tiet_bai_thi_Delete");

            sql.SqlParams("@MaChiTietBaiThi", SqlDbType.BigInt, ma_chi_tiet_bai_thi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<ChiTietBaiThiDto> SelectOne_v2(int ma_chi_tiet_ca_thi, long ma_de_hv, int ma_nhom, int ma_cau_hoi)
        {
            ChiTietBaiThiDto result = new();

            using DatabaseReader sql = new("chi_tiet_bai_thi_SelectOne_v2");

            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hv);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (dataReader != null && dataReader.Read())
            {
                result = GetProperty(dataReader);
            }

            return result;
        }

    }
}
