using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class NhomCauHoiHoanViRepository : INhomCauHoiHoanViRepository
    {
        public async Task<IDataReader> SelectOne (long ma_de_hoan_vi, int ma_nhom)
        {
            DatabaseReader sql = new("NhomCauHoiHoanVi_SelectOne");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<object?> Insert(int ma_de_thi, string ten_nhom, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha, int so_cau_lay, bool la_cau_hoi_nhom)
        {
            DatabaseReader sql = new("NhomCauHoi_Insert");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@TenNhom", SqlDbType.NVarChar, ten_nhom);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@SoCauHoi", SqlDbType.Int, so_cau_hoi);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@MaNhomCha", SqlDbType.Int, ma_nhom_cha);
            sql.SqlParams("@SoCauLay", SqlDbType.Int, so_cau_lay);
            sql.SqlParams("@LaCauHoiNhom", SqlDbType.Bit, la_cau_hoi_nhom);
            return await sql.ExecuteScalarAsync();
        }
        private async Task<int> Update(int ma_nhom, int ma_de_thi, string ten_nhom, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha)
        {
            DatabaseReader sql = new("NhomCauHoi_Update");
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@TenNhom", SqlDbType.NVarChar, ten_nhom);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@SoCauHoi", SqlDbType.Int, so_cau_hoi);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@MaNhomCha", SqlDbType.Int, ma_nhom_cha);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<IDataReader> SelectBy_MaDeHV(long ma_de_hoan_vi)
        {
            DatabaseReader sql = new("NhomCauHoiHoanVi_SelectBy_MaDeHV");
            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            return await sql.ExecuteReaderAsync();
        }
    }
}
