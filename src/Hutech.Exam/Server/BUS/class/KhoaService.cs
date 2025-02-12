using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class KhoaService
    {
        private readonly IKhoaRepository _khoaRepository;
        private readonly IMapper _mapper;

        public KhoaService(IKhoaRepository khoaRepository, IMapper mapper)
        {
            _khoaRepository = khoaRepository;
            _mapper = mapper;
        }
        private KhoaDto getProperty(IDataReader dataReader)
        {
            Khoa khoa = new()
            {
                MaKhoa = dataReader.GetInt32(0),
                TenKhoa = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                NgayThanhLap = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2)
            };
            return _mapper.Map<KhoaDto>(khoa);
        }
        public async Task<List<KhoaDto>> GetAll()
        {
            List<KhoaDto> results = new List<KhoaDto>();
            using (IDataReader dataReader = await _khoaRepository.GetAll())
            {
                while (dataReader.Read())                {
                    results.Add(getProperty(dataReader));
                }
            }
            return results;
        }
    }
}
