using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class KhoaService(IKhoaRepository khoaRepository, IMapper mapper)
    {
        private readonly IKhoaRepository _khoaRepository = khoaRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng Khoa

        public KhoaDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Khoa khoa = new()
            {
                MaKhoa = dataReader.GetInt32(0 + start),
                TenKhoa = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayThanhLap = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start)
            };
            return _mapper.Map<KhoaDto>(khoa);
        }
        public async Task<List<KhoaDto>> GetAll()
        {
            List<KhoaDto> results = [];
            using (IDataReader dataReader = await _khoaRepository.GetAll())
            {
                while (dataReader.Read())                {
                    results.Add(GetProperty(dataReader));
                }
            }
            return results;
        }
    }
}
