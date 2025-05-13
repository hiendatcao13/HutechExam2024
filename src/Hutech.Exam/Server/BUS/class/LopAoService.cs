using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.BUS
{
    public class LopAoService(ILopAoRepository lopAoRepository, IMapper mapper)
    {
        private readonly ILopAoRepository _lopAoRepository = lopAoRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng LopAo

        public LopAoDto GetProperty(IDataReader dataReader, int start = 0)
        {
            LopAo lopAo = new()
            {
                MaLopAo = dataReader.GetInt32(0 + start),
                TenLopAo = dataReader.IsDBNull(1 + start) ? null : dataReader.GetString(1 + start),
                NgayBatDau = dataReader.IsDBNull(2 + start) ? null : dataReader.GetDateTime(2 + start),
                MaMonHoc = dataReader.IsDBNull(3 + start) ? null : dataReader.GetInt32(3 + start)
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
                    lopAo = GetProperty(dataReader);
                }
            }
            return lopAo;
        }
        public async Task<List<LopAoDto>> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            List<LopAoDto> list = [];
            using(IDataReader dataReader = await _lopAoRepository.SelectBy_ma_mon_hoc(ma_mon_hoc))
            {
                while (dataReader.Read())
                {
                    LopAoDto lopAo = GetProperty(dataReader);
                    list.Add(lopAo);
                }
            }
            return list;
        }
        public async Task<List<LopAoDto>> SelectBy_ListChiTietDotThi(List<ChiTietDotThi> list)
        {
            List<LopAoDto> result = [];
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
        public async Task<List<LopAoDto>> GetAll()
        {
            List<LopAoDto> result = [];
            using (IDataReader dataReader = await _lopAoRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    LopAoDto lopAo = GetProperty(dataReader);
                    result.Add(lopAo);
                }
            }
            return result;
        }
        public async Task<int> Insert(string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc)
        {
            return Convert.ToInt32(await _lopAoRepository.Insert(ten_lop_ao, ngay_bat_dau, ma_mon_hoc) ?? -1);
        }
        public async Task<int> Update(int ma_lop_ao, string ten_lop_ao, DateTime ngay_bat_dau, int ma_mon_hoc)
        {
            return await _lopAoRepository.Update(ma_lop_ao, ten_lop_ao, ngay_bat_dau, ma_mon_hoc);
        }
        public async Task<int> Remove(int ma_lop_ao)
        {
            return await _lopAoRepository.Remove(ma_lop_ao);
        }
    }
}
