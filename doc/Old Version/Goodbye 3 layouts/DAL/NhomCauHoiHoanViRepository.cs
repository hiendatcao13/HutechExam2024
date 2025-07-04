using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class NhomCauHoiHoanViRepository(IMapper mapper) : INhomCauHoiHoanViRepository
    {
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng NhomCauHoiHoanVi


        //TODO: bị nhầm lẫn với nhóm câu hỏi hoán vị và nhóm câu hỏi
        public NhomCauHoiHoanViDto GetProperty(IDataReader dataReader, int start = 0)
        {
            NhomCauHoiHoanVi nhomCauHoiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                ThuTu = dataReader.GetInt32(2 + start)
            };
            return _mapper.Map<NhomCauHoiHoanViDto>(nhomCauHoiHoanVi);
        }

        public async Task<NhomCauHoiHoanViDto> SelectOne (long ma_de_hoan_vi, int ma_nhom)
        {
            using DatabaseReader sql = new("NhomCauHoiHoanVi_SelectOne");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);
            sql.SqlParams("@MaNhom", SqlDbType.Int, ma_nhom);

            using var dataReader = await sql.ExecuteReaderAsync();
            NhomCauHoiHoanViDto nhomCauHoi = new();
            if (dataReader != null && dataReader.Read())
            {
                nhomCauHoi = GetProperty(dataReader);
            }

            return nhomCauHoi;
        }

        public async Task<List<NhomCauHoiHoanViDto>> SelectBy_MaDeHV(long ma_de_hoan_vi)
        {
            using DatabaseReader sql = new("NhomCauHoiHoanVi_SelectBy_MaDeHV");

            sql.SqlParams("@MaDeHV", SqlDbType.BigInt, ma_de_hoan_vi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<NhomCauHoiHoanViDto> result = [];
            while(dataReader != null && dataReader.Read())
            {
                result.Add(GetProperty(dataReader));
            }

            return result;
        }
    }
}
