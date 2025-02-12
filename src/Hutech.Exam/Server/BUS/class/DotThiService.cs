using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DotThiService
    {
        private readonly IDotThiRepository _dotThiRepository;
        private readonly IMapper _mapper;
        public DotThiService(IDotThiRepository dotThiRepository, IMapper mapper)
        {
            _dotThiRepository = dotThiRepository;
            _mapper = mapper;
        }
        private DotThiDto getProperty(IDataReader dataReader)
        {
            DotThi dotThi = new()
            {
                MaDotThi = dataReader.GetInt32(0),
                TenDotThi = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                ThoiGianBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2),
                ThoiGianKetThuc = dataReader.IsDBNull(3) ? null : dataReader.GetDateTime(3),
                NamHoc = dataReader.IsDBNull(4) ? null : dataReader.GetInt32(4)
            };
            return _mapper.Map<DotThiDto>(dotThi);
        }
        public async Task<List<DotThiDto>> GetAll()
        {
            List<DotThiDto> result = new();
            using (IDataReader dataReader = await _dotThiRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    DotThiDto dotThi = getProperty(dataReader);
                    result.Add(dotThi);
                }
            }
            return result;
        }
        public async Task<DotThiDto> SelectOne(int ma_dot_thi)
        {
            DotThiDto dotThi = new();
            using(IDataReader dataReader = await _dotThiRepository.SelectOne(ma_dot_thi))
            {
                if (dataReader.Read())
                {
                    dotThi = getProperty(dataReader);
                }
            }
            return dotThi;
        }
    }
}
