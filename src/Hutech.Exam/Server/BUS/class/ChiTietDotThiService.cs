﻿using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Collections.Generic;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDotThiService(IChiTietDotThiResposity chiTietDotThiRepository, IMapper mapper)
    {
        private readonly IChiTietDotThiResposity _chiTietDotThiResposity = chiTietDotThiRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 5; // số lượng cột trong bảng ChiTietDotThi

        public ChiTietDotThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietDotThi chiTietDotThi = new()
            {
                MaChiTietDotThi = dataReader.GetInt32(0 + start),
                TenChiTietDotThi = dataReader.GetString(1 + start),
                MaLopAo = dataReader.GetInt32(2 + start),
                MaDotThi = dataReader.GetInt32(3 + start),
                LanThi = dataReader.GetInt32(4 + start)
            };
            return _mapper.Map<ChiTietDotThiDto>(chiTietDotThi);
        }
        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi(int ma_dot_thi)
        {
            List<ChiTietDotThiDto> list = [];
            using(IDataReader dataReader = await _chiTietDotThiResposity.SelectBy_MaDotThi(ma_dot_thi))
            {
                while(dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = GetProperty(dataReader);
                    list.Add(chiTietDotThi);
                }
            }
            return list;
        }
        public async Task<List<ChiTietDotThiDto>> SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            List<ChiTietDotThiDto> list = [];
            using (IDataReader dataReader = await _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao))
            {

                while (dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = GetProperty(dataReader);
                    list.Add(chiTietDotThi);
                }
            }
            return list;
        }
        public async Task<ChiTietDotThiDto> SelectBy_MaDotThi_MaLopAo_LanThi(int ma_dot_thi, int ma_lop_ao, int lan_thi)
        {
            ChiTietDotThiDto chiTietDotThi = new();
            using (IDataReader dataReader = await _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo_LanThi(ma_dot_thi, ma_lop_ao, lan_thi))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = GetProperty(dataReader);
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
                    chiTietDotThi = GetProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
        public async Task<List<ChiTietDotThiDto>> GetAll()
        {
            List<ChiTietDotThiDto> result = [];
            using (IDataReader dataReader = await _chiTietDotThiResposity.GetAll())
            {
                while (dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = GetProperty(dataReader);
                    result.Add(chiTietDotThi);
                }
            }
            return result;
        }
        public async Task<int> Insert(string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi)
        {
            return Convert.ToInt32(await _chiTietDotThiResposity.Insert(ten_chi_tiet_dot_thi, ma_lop_ao, ma_dot_thi, lan_thi) ?? -1);
        }
        public async Task<int> Update(int ma_chi_tiet_dot_thi, string ten_chi_tiet_dot_thi, int ma_lop_ao, int ma_dot_thi, int lan_thi)
        {
            return await _chiTietDotThiResposity.Update(ma_chi_tiet_dot_thi, ten_chi_tiet_dot_thi, ma_lop_ao, ma_dot_thi, lan_thi);
        }
        public async Task<int> Remove(int ma_chi_tiet_dot_thi)
        {
            return await _chiTietDotThiResposity.Remove(ma_chi_tiet_dot_thi);
        }
    }
}
