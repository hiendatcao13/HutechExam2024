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
        public List<ChiTietDotThiDto> SelectBy_MaDotThi(int ma_dot_thi)
        {
            List<ChiTietDotThiDto> list = new();
            using(IDataReader dataReader = _chiTietDotThiResposity.SelectBy_MaDotThi(ma_dot_thi))
            {
                while(dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = getProperty(dataReader);
                    list.Add(chiTietDotThi);
                }
            }
            return list;
        }
        public ChiTietDotThiDto SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            ChiTietDotThiDto chiTietDotThi = new();
            using (IDataReader dataReader = _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = getProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
        public ChiTietDotThiDto SelectOne(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThiDto chiTietDotThi = new();
            using (IDataReader dataReader = _chiTietDotThiResposity.SelectOne(ma_chi_tiet_dot_thi))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = getProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
    }
}
