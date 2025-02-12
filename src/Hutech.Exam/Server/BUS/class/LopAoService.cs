using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.BUS
{
    public class LopAoService
    {
        private readonly ILopAoRepository _lopAoRepository;
        private readonly IMapper _mapper;
        public LopAoService(ILopAoRepository lopAoRepository, IMapper mapper)
        {
            _lopAoRepository = lopAoRepository;
            _mapper = mapper;
        }
        private LopAoDto getProperty(IDataReader dataReader)
        {
            LopAo lopAo = new()
            {
                MaLopAo = dataReader.GetInt32(0),
                TenLopAo = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                NgayBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2),
                MaMonHoc = dataReader.IsDBNull(3) ? null : dataReader.GetInt32(3)
            };
            return _mapper.Map<LopAoDto>(lopAo);
        }
        public async Task<LopAoDto> SelectOne(int ma_lop_ao)
        {
            LopAoDto lopAo = new();
            using(IDataReader dataReader = await _lopAoRepository.SelectOne(ma_lop_ao))
            {
                if (dataReader.Read())
                {
                    lopAo = getProperty(dataReader);
                }
            }
            return lopAo;
        }
        public async Task<List<LopAoDto>> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            List<LopAoDto> list = new();
            using(IDataReader dataReader = await _lopAoRepository.SelectBy_ma_mon_hoc(ma_mon_hoc))
            {
                while (dataReader.Read())
                {
                    LopAoDto lopAo = getProperty(dataReader);
                    list.Add(lopAo);
                }
            }
            return list;
        }
        public async Task<List<LopAoDto>> SelectBy_ListChiTietDotThi(List<ChiTietDotThi> list)
        {
            List<LopAoDto> result = new();
            foreach(var chiTietDotThi in list)
            {
                LopAoDto lopAo = await this.SelectOne(chiTietDotThi.MaLopAo);
                // tránh bị trùng lặp
                if (!result.Contains(lopAo))
                {
                    result.Add(lopAo);
                }
            }
            return result;
        }
    }
}
