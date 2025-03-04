using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class LopService
    {
        private readonly ILopRepository _lopRepository;
        private readonly IMapper _mapper;
        public LopService(ILopRepository lopRepository, IMapper mapper)
        {
            _lopRepository = lopRepository;
            _mapper = mapper;
        }

        private LopDto getProperty(IDataReader dataReader)
        {
            Lop lop = new()
            {
                MaLop = dataReader.GetInt32(0),
                TenLop = dataReader.IsDBNull(1) ? null : dataReader.GetString(1),
                NgayBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2),
                MaKhoa = dataReader.IsDBNull(3) ? null : dataReader.GetInt32(3)
            };
            return _mapper.Map<LopDto>(lop);
        }
        public async Task<LopDto> SelectBy_ten_lop(string ten_lop)
        {
            LopDto lop = new();
            using(IDataReader dataReader = await _lopRepository.SelectBy_ten_lop(ten_lop))
            {
                if (dataReader.Read())
                {
                    lop = getProperty(dataReader);
                }
            }
            return lop;
        }
        public async Task<int> Insert(string? ten_lop, DateTime? ngay_bat_dau, int? ma_khoa)
        {
            return (int)(await _lopRepository.Insert(ten_lop, ngay_bat_dau, ma_khoa) ?? -1);
        }
    }
}
