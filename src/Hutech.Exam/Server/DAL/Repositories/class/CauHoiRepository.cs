using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauHoiRepository : ICauHoiRepository
    {
        public async Task<IDataReader> SelectOne(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauHoi_SelectOne");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectDapAn(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauHoi_SelectDapAn");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaNhom(int ma_nhom)
        {
            DatabaseReader sql = new("CauHoi_SelectBy_MaNhom");
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<object?> Insert(int ma_clo, int ma_nhom, string tieu_de, int kieu_noi_dung, string noi_dung, int thu_tu, string ghi_chu, bool hoan_vi)
        {
            DatabaseReader sql = new("CauHoi_Insert");
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@KieuNoiDung", SqlDbType.Int, kieu_noi_dung);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            return await sql.ExecuteScalarAsync();
        }

        public async Task<int> Update(int ma_cau_hoi, int ma_nhom, int ma_clo, string tieu_de, int kieu_noi_dung, string noi_dung, int thu_tu, string ghi_chu, bool hoan_vi)
        {
            DatabaseReader sql = new("CauHoi_Update");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@KieuNoiDung", SqlDbType.Int, kieu_noi_dung);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> Remove(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauHoi_Delete");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> ForceRemove(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauHoi_ForceDelete");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
