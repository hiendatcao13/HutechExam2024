using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Server.DAL.Helper;
using Hutech.Exam.Shared.DTO;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class ChiTietBaiThiRepository : IChiTietBaiThiRepository
    {
        public async Task<object?> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, DateTime NgayTao, int ThuTu)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_Insert");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, MaDeHV);
            sql.SqlParams("@MaNhom", SqlDbType.Int, MaNhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, MaCauHoi);
            sql.SqlParams("@MaCLO", SqlDbType.Int, MaClo);
            sql.SqlParams("@NgayTao", SqlDbType.DateTime, NgayTao);
            sql.SqlParams("@ThuTu", SqlDbType.Int, ThuTu);
            return await sql.ExecuteScalarAsync();
        }
        public async Task Insert_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            var dt = ChiTietBaiThiHelper.ToDataTable(chiTietBaiThis);
            DatabaseReader sql = new("chi_tiet_bai_thi_Insert_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);
            await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_Update");
            sql.SqlParams("@MaChiTietBaiThi", SqlDbType.BigInt, MaChiTietBaiThi);
            sql.SqlParams("@CauTraLoi", SqlDbType.Int, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Update_v2(int MaChiTietCaThi, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_Update_v2");
            sql.SqlParams("@MaChiTietCaThi", SqlDbType.Int, MaChiTietCaThi);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, MaCauHoi);
            sql.SqlParams("@MaCLO", SqlDbType.Int, MaClo);
            sql.SqlParams("@CauTraLoi", SqlDbType.Int, CauTraLoi);
            sql.SqlParams("@NgayCapNhat", SqlDbType.DateTime, NgayCapNhat);
            sql.SqlParams("@KetQua", SqlDbType.Bit, KetQua);
            return await sql.ExecuteNonQueryAsync();
        }
        // bản nâng cấp vừa insert vừa update
        public async Task<int> Save(int MaChiTietCaThi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayTao, DateTime NgayCapNhat, bool KetQua, int ThuTu)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_Save");
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
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Save_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            var dt = ChiTietBaiThiHelper.ToDataTable(chiTietBaiThis);
            DatabaseReader sql = new("chi_tiet_bai_thi_Save_Batch");
            sql.SqlParams("@Data", SqlDbType.Structured, dt);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<IDataReader> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<int> Delete(long ma_chi_tiet_bai_thi)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_Delete");
            sql.SqlParams("@MaChiTietBaiThi", SqlDbType.BigInt, ma_chi_tiet_bai_thi);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<IDataReader> SelectOne_v2(int ma_chi_tiet_ca_thi, long ma_de_hv, int ma_nhom, int ma_cau_hoi)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_SelectOne_v2");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hv);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<string> DaThi(int ma_chi_tiet_ca_thi)
        {
            DatabaseReader sql = new("chi_tiet_bai_thi_DaThi");
            sql.SqlParams("@ma_chi_tiet_ca_thi", SqlDbType.Int, ma_chi_tiet_ca_thi);
            var result = await sql.ExecuteScalarAsync();
            return result?.ToString() ?? string.Empty;
        }
    }
}
