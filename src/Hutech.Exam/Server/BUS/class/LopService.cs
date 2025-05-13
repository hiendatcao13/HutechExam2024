using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class LopService(ILopRepository lopRepository, IMapper mapper)
    {
        private readonly ILopRepository _lopRepository = lopRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng Lop

        public LopDto GetProperty(IDataReader dataReader, int start = 0)
        {
            Lop lop = new()
            {
                MaLop = dataReader.GetInt32(0 + start),
                TenLop = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayBatDau = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start),
                MaKhoa = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt32(3 + start)
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
                    lop = GetProperty(dataReader);
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
