using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDeThiHoanViService(IChiTietDeThiHoanViRepository chiTietDeThiHoanViRepository, IMapper mapper)
    {
        private readonly IChiTietDeThiHoanViRepository _chiTietDeThiHoanViRepository = chiTietDeThiHoanViRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 6; // số lượng cột trong bảng ChiTietDeThiHoanVi

        public ChiTietDeThiHoanViDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietDeThiHoanVi chiTietDeThiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                MaCauHoi = dataReader.GetInt32(2 + start),
                ThuTu = dataReader.GetInt32(3 + start),
                HoanViTraLoi = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                DapAn = dataReader.IsDBNull(5 + start) ? null : dataReader.GetInt32(5 + start)
            };
            return _mapper.Map<ChiTietDeThiHoanViDto>(chiTietDeThiHoanVi);
        }
        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV(long maDeHV)
        {
            List<ChiTietDeThiHoanViDto> result = [];
            using(IDataReader dataReader = await _chiTietDeThiHoanViRepository.SelectBy_MaDeHV(maDeHV))
            {
                while (dataReader.Read())
                {
                    ChiTietDeThiHoanViDto chiTietDeThiHoanVi = GetProperty(dataReader);
                    result.Add(chiTietDeThiHoanVi);
                }
            }
            return result;
        }
        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            List<ChiTietDeThiHoanViDto> list = [];
            using (IDataReader dataReader = await _chiTietDeThiHoanViRepository.SelectBy_MaDeHV_MaNhom(ma_de_hoan_vi, ma_nhom))
            {
                while (dataReader.Read())
                {
                    ChiTietDeThiHoanViDto chiTietDeThiHoanVi = GetProperty(dataReader);
                    list.Add(chiTietDeThiHoanVi);
                }
            }
            return list;
        }
    }
}
