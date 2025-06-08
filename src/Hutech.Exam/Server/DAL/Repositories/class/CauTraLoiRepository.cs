using Hutech.Exam.Server.DAL.DataReader;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauTraLoiRepository : ICauTraLoiRepository
    {
        public async Task<IDataReader> SelectOne(int ma_cau_tra_loi)
        {
            DatabaseReader sql = new("CauHoi_SelectOne");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            return await sql.ExecuteReaderAsync();
        }
        public async Task<IDataReader> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            DatabaseReader sql = new("CauTraLoi_SelectBy_MaCauHoi");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            return await sql.ExecuteReaderAsync();
        }

        public async Task<object?> Insert(int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi)
        {
            DatabaseReader sql = new("CauTraLoi_Insert");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@LaDapAn", SqlDbType.Bit, la_dap_an);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            return await sql.ExecuteScalarAsync();
        }

        public async Task<int> Update(int ma_cau_tra_loi, int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi)
        {
            DatabaseReader sql = new("CauTraLoi_Update");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@LaDapAn", SqlDbType.Bit, la_dap_an);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            return await sql.ExecuteNonQueryAsync();
        }

        public async Task<int> Remove(int ma_cau_tra_loi)
        {
            DatabaseReader sql = new("CauTraLoi_Delete");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            return await sql.ExecuteNonQueryAsync();
        }
    }
}
