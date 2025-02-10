using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDeThiHoanViService
    {
        private readonly IChiTietDeThiHoanViRepository _chiTietDeThiHoanViRepository;
        private readonly IMapper _mapper;
        public ChiTietDeThiHoanViService(IChiTietDeThiHoanViRepository chiTietDeThiHoanViRepository, IMapper mapper)
        {
            _chiTietDeThiHoanViRepository = chiTietDeThiHoanViRepository;
            _mapper = mapper;
        }
        private ChiTietDeThiHoanViDto getProperty(IDataReader dataReader)
        {
            TblChiTietDeThiHoanVi chiTietDeThiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0),
                MaNhom = dataReader.GetInt32(1),
                MaCauHoi = dataReader.GetInt32(2),
                ThuTu = dataReader.GetInt32(3),
                HoanViTraLoi = dataReader.IsDBNull(4) ? null : dataReader.GetString(4),
                DapAn = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5)
            };
            return _mapper.Map<ChiTietDeThiHoanViDto>(chiTietDeThiHoanVi);
        }
        public List<ChiTietDeThiHoanViDto> SelectBy_MaDeHV(long maDeHV)
        {
            List<ChiTietDeThiHoanViDto> result = new();
            using(IDataReader dataReader = _chiTietDeThiHoanViRepository.SelectBy_MaDeHV(maDeHV))
            {
                while (dataReader.Read())
                {
                    ChiTietDeThiHoanViDto chiTietDeThiHoanVi = getProperty(dataReader);
                    result.Add(chiTietDeThiHoanVi);
                }
            }
            return result;
        }
        public List<ChiTietDeThiHoanViDto> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            List<ChiTietDeThiHoanViDto> list = new();
            using (IDataReader dataReader = _chiTietDeThiHoanViRepository.SelectBy_MaDeHV_MaNhom(ma_de_hoan_vi, ma_nhom))
            {
                while (dataReader.Read())
                {
                    ChiTietDeThiHoanViDto chiTietDeThiHoanVi = getProperty(dataReader);
                    list.Add(chiTietDeThiHoanVi);
                }
            }
            return list;
        }
    }
}
