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
        public async Task<int> Insert(string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc)
        {
            return (int)(await _dotThiRepository.Insert(ten_dot_thi, thoi_gian_bat_dau, thoi_gian_ket_thuc, nam_hoc) ?? -1);
        }
        public async Task<int> Update(int ma_dot_thi, string ten_dot_thi, DateTime thoi_gian_bat_dau, DateTime thoi_gian_ket_thuc, int nam_hoc)
        {
            return await _dotThiRepository.Update(ma_dot_thi, ten_dot_thi, thoi_gian_bat_dau, thoi_gian_ket_thuc, nam_hoc);
        }
        public async Task<int> Remove(int ma_dot_thi)
        {
            return await _dotThiRepository.Remove(ma_dot_thi);
        }
    }
}
