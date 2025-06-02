using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.ChiTietDotThi;
using Hutech.Exam.Shared.Models;
using System.Collections.Generic;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDotThiService(IChiTietDotThiResposity chiTietDotThiRepository, LopAoService lopAoService, MonHocService monHocService, IMapper mapper)
    {
        private readonly IChiTietDotThiResposity _chiTietDotThiResposity = chiTietDotThiRepository;

        private readonly LopAoService _lopAoService = lopAoService;
        private readonly MonHocService _monHocService = monHocService;

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
                    chiTietDotThi.MaLopAoNavigation = _lopAoService.GetProperty(dataReader, COLUMN_LENGTH);
                    chiTietDotThi.MaLopAoNavigation.MaMonHocNavigation = _monHocService.GetProperty(dataReader, COLUMN_LENGTH + LopAoService.COLUMN_LENGTH);
                    list.Add(chiTietDotThi);
                }
            }
            return list;
        }

        public async Task<ChiTietDotThiPage> SelectBy_MaDotThi_Paged(int ma_dot_thi, int pageNumber, int pageSize)
        {
            List<ChiTietDotThiDto> result = [];
            int tong_so_ban_ghi = 0, tong_so_trang = 0;
            using (IDataReader dataReader = await _chiTietDotThiResposity.SelectBy_MaDotThi(ma_dot_thi))
            {
                while (dataReader.Read())
                {
                    ChiTietDotThiDto chiTietDotThi = GetProperty(dataReader);
                    chiTietDotThi.MaLopAoNavigation = _lopAoService.GetProperty(dataReader, COLUMN_LENGTH);
                    chiTietDotThi.MaLopAoNavigation.MaMonHocNavigation = _monHocService.GetProperty(dataReader, COLUMN_LENGTH + LopAoService.COLUMN_LENGTH);
                    result.Add(chiTietDotThi);
                }

                //chuyển sang bảng thứ 2 đọc tổng số lượng bản ghi và tổng số lượng trang
                if (dataReader.NextResult())
                {
                    while (dataReader.Read())
                    {
                        tong_so_ban_ghi = dataReader.GetInt32(0);
                        tong_so_trang = dataReader.GetInt32(1);
                    }
                }
            }
            return new ChiTietDotThiPage { Data = result, TotalPages = tong_so_trang, TotalRecords = tong_so_ban_ghi};
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
                    chiTietDotThi.MaLopAoNavigation = _lopAoService.GetProperty(dataReader, COLUMN_LENGTH);
                    chiTietDotThi.MaLopAoNavigation.MaMonHocNavigation = _monHocService.GetProperty(dataReader, COLUMN_LENGTH + LopAoService.COLUMN_LENGTH);
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
        public async Task<int> Insert(ChiTietDotThiCreateRequest chiTietDotThi)
        {
            return Convert.ToInt32(await _chiTietDotThiResposity.Insert(chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi) ?? -1);
        }
        public async Task<bool> Update(int id, ChiTietDotThiUpdateRequest chiTietDotThi)
        {
            return await _chiTietDotThiResposity.Update(id, chiTietDotThi.TenChiTietDotThi, chiTietDotThi.MaLopAo, chiTietDotThi.MaDotThi, chiTietDotThi.LanThi) != 0;
        }
        public async Task<bool> Remove(int ma_chi_tiet_dot_thi)
        {
            return await _chiTietDotThiResposity.Remove(ma_chi_tiet_dot_thi) != 0;
        }
    }
}
