using AutoMapper;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CauHoiRepository(ICloRepository cloRepository, ICauTraLoiRepository cauTraLoiRepository, IMapper mapper) : ICauHoiRepository
    {
        private readonly ICloRepository _cloRepository = cloRepository;
        private readonly ICauTraLoiRepository _cauTraLoiRepository = cauTraLoiRepository;

        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 10; // số lượng cột trong bảng CauHoi

        public CauHoiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            CauHoiDto cauHoi = new()
            {
                MaCauHoi = dataReader.GetInt32(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                MaClo = dataReader.GetInt32(2 + start),
                TieuDe = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                KieuNoiDung = dataReader.GetInt32(4 + start),
                NoiDung = dataReader.IsDBNull(5 + start) ? null : dataReader.GetString(5 + start),
                ThuTu = dataReader.GetInt32(6 + start),
                Guild = dataReader.IsDBNull(7 + start) ? null : dataReader.GetGuid(7 + start),
                GhiChu = dataReader.IsDBNull(8 + start) ? null : dataReader.GetString(8 + start),
                HoanVi = dataReader.IsDBNull(9 + start) ? null : dataReader.GetBoolean(9 + start)
            };
            return cauHoi;
        }

        public async Task<int> Insert(int ma_clo, int ma_nhom, string tieu_de, int kieu_noi_dung, string noi_dung, int thu_tu, string ghi_chu, bool hoan_vi)
        {
            using DatabaseReader sql = new("CauHoi_Insert");

            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@KieuNoiDung", SqlDbType.Int, kieu_noi_dung);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);

            return Convert.ToInt32(await sql.ExecuteScalarAsync() ?? -1);
        }

        public async Task<bool> Update(int ma_cau_hoi, int ma_nhom, int ma_clo, string tieu_de, int kieu_noi_dung, string noi_dung, int thu_tu, string ghi_chu, bool hoan_vi)
        {
            using DatabaseReader sql = new("CauHoi_Update");
            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);
            sql.SqlParams("@MaClo", SqlDbType.Int, ma_clo);
            sql.SqlParams("@TieuDe", SqlDbType.NVarChar, tieu_de);
            sql.SqlParams("@KieuNoiDung", SqlDbType.Int, kieu_noi_dung);
            sql.SqlParams("@NoiDung", SqlDbType.NText, noi_dung);
            sql.SqlParams("@ThuTu", SqlDbType.Int, thu_tu);
            sql.SqlParams("@GhiChu", SqlDbType.NVarChar, ghi_chu);
            sql.SqlParams("@HoanVi", SqlDbType.Bit, hoan_vi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> Remove(int ma_cau_hoi)
        {
            using DatabaseReader sql = new("CauHoi_Delete");

            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ForceRemove(int ma_cau_hoi)
        {
            using DatabaseReader sql = new("CauHoi_ForceDelete");

            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);

            return await sql.ExecuteNonQueryAsync() > 0;
        }

        public async Task<CauHoiDto> SelectOne(int ma_cau_hoi)
        {
            CauHoiDto cauHoi = new();
            List<CauTraLoiDto> cauTraLois = [];
            bool isFirst = true;

            using DatabaseReader sql = new("CauHoi_SelectOne");

            sql.SqlParams("@MaCauHoi", SqlDbType.Int, ma_cau_hoi);

            using var dataReader = await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                if (isFirst)
                {
                    cauHoi = GetProperty(dataReader);
                    cauHoi.MaCloNavigation = _cloRepository.GetProperty(dataReader, COLUMN_LENGTH);
                    isFirst = false;
                }
                cauTraLois.Add(_cauTraLoiRepository.GetProperty(dataReader, COLUMN_LENGTH + CloService.COLUMN_LENGTH));
            }
            cauHoi.CauTraLois = cauTraLois;

            return cauHoi;

        }

        public async Task<List<CauHoiDto>> SelectBy_MaNhom(int ma_nhom)
        {
            List<CauHoiDto> result = [];
            Dictionary<int, CauHoiDto> cauHoiMap = []; // Để tra cứu nhanh theo MaCauHoi

            using DatabaseReader sql = new("CauHoi_SelectBy_MaNhom");

            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            using var dataReader =  await sql.ExecuteReaderAsync();
            while (dataReader != null && dataReader.Read())
            {
                int maCauHoi = dataReader.GetInt32(0);

                if (!cauHoiMap.TryGetValue(maCauHoi, out var cauHoi))
                {
                    cauHoi = GetProperty(dataReader);
                    cauHoi.MaCloNavigation = _cloRepository.GetProperty(dataReader, COLUMN_LENGTH);
                    cauHoiMap[maCauHoi] = cauHoi;
                    result.Add(cauHoi);
                }

                var cauTraLoi = _cauTraLoiRepository.GetProperty(dataReader, COLUMN_LENGTH + CloService.COLUMN_LENGTH);
                cauHoi.CauTraLois.Add(cauTraLoi);
            }

            return result;
        }

    }
}
