using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class NhomCauHoiRepository : INhomCauHoiRepository
    {
        public async Task<IDataReader> SelectAllBy_MaDeThi(int ma_de_thi)
        {
            DatabaseReader sql = new("NhomCauHoi_SelectAllBy_MaDeThi");
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectOne(int ma_nhom)
        {
            DatabaseReader sql = new("NhomCauHoi_SelectOne");
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
        public async Task<int> Update(int ma_nhom, int ma_de_thi, string ten_nhom, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha)
        {
            DatabaseReader sql = new("NhomCauHoi_Update");
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@TenNhom", SqlDbType.NVarChar, ten_nhom);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@SoCauHoi", SqlDbType.Int, so_cau_hoi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            sql.SqlParams("@MaNhomCha", SqlDbType.Int, ma_nhom_cha);
            return await sql.ExecuteNonQueryAsync();
        }
        public async Task<int> Remove(int ma_nhom)
        {
            DatabaseReader sql = new("NhomCauHoi_Delete");
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
