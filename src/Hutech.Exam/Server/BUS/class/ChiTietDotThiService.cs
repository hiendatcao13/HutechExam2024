using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Collections.Generic;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDotThiService
    {
        private readonly IChiTietDotThiResposity _chiTietDotThiResposity;
        private readonly IMapper _mapper;
        public ChiTietDotThiService(IChiTietDotThiResposity chiTietDotThiRepository, IMapper mapper)
        {
            _chiTietDotThiResposity = chiTietDotThiRepository;
            _mapper = mapper;
        }
        private ChiTietDotThiDto getProperty(IDataReader dataReader)
        {
            ChiTietDotThi chiTietDotThi = new()
            {
                MaChiTietDotThi = dataReader.GetInt32(0),
                TenChiTietDotThi = dataReader.GetString(1),
                MaLopAo = dataReader.GetInt32(2),
                MaDotThi = dataReader.GetInt32(3),
                LanThi = dataReader.GetString(4)
            };
            return _mapper.Map<ChiTietDotThiDto>(chiTietDotThi);
        }
        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi(int ma_dot_thi)
        {
            List<ChiTietDotThiDto> list = new();
            using(IDataReader dataReader = await _chiTietDotThiResposity.SelectBy_MaDotThi(ma_dot_thi))
            {
                while(dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = getProperty(dataReader);
                    list.Add(chiTietDotThi);
                }
            }
            return list;
        }
        public async Task<ChiTietDotThiDto> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            ChiTietDotThiDto chiTietDotThi = new();
            using (IDataReader dataReader = await _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = getProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
        public async Task<ChiTietDotThiDto> SelectOne(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto chiTietDotThi = new();
            using (IDataReader dataReader = await _chiTietDotThiResposity.SelectOne(ma_chi_tiet_dot_thi))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = getProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
        public async Task<List<ChiTietDotThiDto>> GetAll()
        {
            List<ChiTietDotThiDto> result = new();
            using (IDataReader dataReader = await _chiTietDotThiResposity.GetAll())
            {
                while (dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = getProperty(dataReader);
                    result.Add(chiTietDotThi);
                }
            }
            return result;
        }
        public async Task<object?> Insert(string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, string lan_thi)
        {
            return (int)(await _chiTietDotThiResposity.Insert(ten_chi_tiet_dot_thi, ma_lop_ao, ma_dot_thi, lan_thi) ?? -1);
        }
        public async Task<int> Update(int ma_chi_tiet_dot_thi, string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, string lan_thi)
        {
            return await _chiTietDotThiResposity.Update(ma_chi_tiet_dot_thi, ten_chi_tiet_dot_thi, ma_lop_ao, ma_dot_thi, lan_thi);
        }
        public async Task<int> Remove(int ma_chi_tiet_dot_thi)
        {
            return await _chiTietDotThiResposity.Remove(ma_chi_tiet_dot_thi);
        }
    }
}
