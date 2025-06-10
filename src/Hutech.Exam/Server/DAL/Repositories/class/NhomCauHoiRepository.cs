using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class NhomCauHoiRepository(IMapper mapper) : INhomCauHoiRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 10; // số lượng cột trong bảng NhomCauHoi

        public NhomCauHoiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            NhomCauHoi nhomCauHoi = new()
            {
                MaNhom = dataReader.GetInt32(0 + start),
                MaDeThi = dataReader.GetInt32(1 + start),
                TenNhom = dataReader.GetString(2 + start),
                KieuNoiDung = dataReader.GetInt32(3 + start),
                NoiDung = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                SoCauHoi = dataReader.GetInt32(5 + start),
                HoanVi = dataReader.GetBoolean(6 + start),
                ThuTu = dataReader.GetInt32(7 + start),
                MaNhomCha = dataReader.GetInt32(8 + start),
                SoCauLay = dataReader.GetInt32(9 + start),
                LaCauHoiNhom = dataReader.IsDBNull(10 + start) ? null : dataReader.GetBoolean(10 + start)
            };
            return _mapper.Map<NhomCauHoiDto>(nhomCauHoi);
        }

        public async Task<List<NhomCauHoiDto>> SelectAllBy_MaDeThi(int ma_de_thi)
        {
            using DatabaseReader sql = new("NhomCauHoi_SelectAllBy_MaDeThi");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<NhomCauHoiDto> list = [];

            while (dataReader != null && dataReader.Read())
            {
                NhomCauHoiDto nhomCauHoi = GetProperty(dataReader);
                list.Add(nhomCauHoi);
            }

            return list;
        }

        public async Task<NhomCauHoiDto> SelectOne(int ma_nhom)
        {
            using DatabaseReader sql = new("NhomCauHoi_SelectOne");

            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            using var dataReader = await sql.ExecuteReaderAsync();
            NhomCauHoiDto nhomCauHoi = new();

            if (dataReader != null && dataReader.Read())
            {
                nhomCauHoi = GetProperty(dataReader);
            }

            return nhomCauHoi;
        }
        public async Task<int> Insert(int ma_de_thi, string ten_nhom, int kieu_noi_dung, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha, int so_cau_lay, bool la_cau_hoi_nhom)
        {
            using DatabaseReader sql = new("NhomCauHoi_Insert");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@TenNhom", SqlDbType.NVarChar, ten_nhom);
            sql.SqlParams("@KieuNoiDung", SqlDbType.Int, kieu_noi_dung);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@SoCauHoi", SqlDbType.Int, so_cau_hoi);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@MaNhomCha", SqlDbType.Int, ma_nhom_cha);
            sql.SqlParams("@SoCauLay", SqlDbType.Int, so_cau_lay);
            sql.SqlParams("@LaCauHoiNhom", SqlDbType.Bit, la_cau_hoi_nhom);

            return Convert.ToInt32(await sql.ExecuteScalarAsync());
        }

        public async Task<bool> Update(int ma_nhom, int ma_de_thi, string ten_nhom, int kieu_noi_dung, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha)
        {
            using DatabaseReader sql = new("NhomCauHoi_Update");

            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaDeThi", SqlDbType.Int, ma_de_thi);
            sql.SqlParams("@TenNhom", SqlDbType.NVarChar, ten_nhom);
            sql.SqlParams("@KieuNoiDung", SqlDbType.Int, kieu_noi_dung);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@SoCauHoi", SqlDbType.Int, so_cau_hoi);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);
            sql.SqlParams("@MaNhomCha", SqlDbType.Int, ma_nhom_cha);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_nhom)
        {
            using DatabaseReader sql = new("NhomCauHoi_Delete");

            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_nhom)
        {
            using DatabaseReader sql = new("NhomCauHoi_ForceDelete");

            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
