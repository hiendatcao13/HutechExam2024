using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauTraLoiRepository(IMapper mapper) : ICauTraLoiRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 6; // số lượng cột trong bảng CauTraLoi

        public CauTraLoiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CauTraLoi cauTraLoi = new()
            {
                MaCauTraLoi = dataReader.GetInt32(0 + start),
                MaCauHoi = dataReader.GetInt32(1 + start),
                ThuTu = dataReader.GetInt32(2 + start),
                NoiDung = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                LaDapAn = dataReader.GetBoolean(4 + start),
                HoanVi = dataReader.GetBoolean(5 + start)
            };
            return _mapper.Map<CauTraLoiDto>(cauTraLoi);
        }

        public async Task<CauTraLoiDto> SelectOne(int ma_cau_tra_loi)
        {
            CauTraLoiDto result = new();

            using DatabaseReader sql = new("CauHoi_SelectOne");
            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);

            using var dataReader = await sql.ExecuteReaderAsync();
            if (dataReader != null && dataReader.Read())
            {
                result = GetProperty(dataReader);
            }

            return result;
        }

        public async Task<int> Insert(int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi)
        {
            using DatabaseReader sql = new("CauTraLoi_Insert");

            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@LaDapAn", SqlDbType.Bit, la_dap_an);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_cau_tra_loi, int ma_cau_hoi, int thu_tu, string noi_dung, bool la_dap_an, bool hoan_vi)
        {
            using DatabaseReader sql = new("CauTraLoi_Update");

            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@LaDapAn", SqlDbType.Bit, la_dap_an);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_cau_tra_loi)
        {
            using DatabaseReader sql = new("CauTraLoi_Delete");

            sql.SqlParams("@MaCauTraLoi", SqlDbType.Int, ma_cau_tra_loi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<List<CauTraLoiDto>> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            List<CauTraLoiDto> result = [];

            using DatabaseReader sql = new("CauTraLoi_SelectBy_MaCauHoi");

            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);

            using var dataReader =  await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                CauTraLoiDto cauTraLoi = GetProperty(dataReader);
                result.Add(cauTraLoi);
            }

            return result;
        }
    }
}
