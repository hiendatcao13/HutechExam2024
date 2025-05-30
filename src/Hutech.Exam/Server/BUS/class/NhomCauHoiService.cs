using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.NhomCauHoi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class NhomCauHoiService(INhomCauHoiRepository nhomCauHoiRepository, IMapper mapper)
    {
        private readonly INhomCauHoiRepository _nhomCauHoiRepository = nhomCauHoiRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 10; // số lượng cột trong bảng NhomCauHoi

        public NhomCauHoiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            NhomCauHoi nhomCauHoi = new()
            {
                MaNhom = dataReader.GetInt32(0 + start),
                MaDeThi = dataReader.GetInt32(1 + start),
                TenNhom = dataReader.GetString(2 + start),
                KieuNoiDung = dataReader.GetInt32(3 + start),
                NoiDung = dataReader.IsDBNull(4 + start) ? null : dataReader.GetString(4 + start),
                SoCauHoi = dataReader.GetInt32(5 + start),
                HoanVi = dataReader.GetBoolean(6 + start),
                ThuTu = dataReader.GetInt32(7 + start),
                MaNhomCha = dataReader.GetInt32(8 + start),
                SoCauLay = dataReader.GetInt32(9 + start),
                LaCauHoiNhom = dataReader.IsDBNull(10 + start) ? null : dataReader.GetBoolean(10 + start)
            };
            return _mapper.Map<NhomCauHoiDto>(nhomCauHoi);
        }
        public async Task<int> Insert(NhomCauHoiCreateRequest nhomCauHoi)
        {
            return Convert.ToInt32(await _nhomCauHoiRepository.Insert(nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha, nhomCauHoi.SoCauLay, nhomCauHoi.LaCauHoiNhom) ?? -1);
        }
        public async Task<bool> Update(int id, NhomCauHoiUpdateRequest nhomCauHoi)
        {
            return await _nhomCauHoiRepository.Update(id, nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha) != 0;
        }
        public async Task<bool> Remove(int ma_nhom)
        {
            return await _nhomCauHoiRepository.Remove(ma_nhom) != 0;
        }
        public async Task<List<NhomCauHoiDto>> SelectAllBy_MaDeThi(int ma_de_thi)
        {
            List<NhomCauHoiDto> list = [];
            using(IDataReader dataReader = await _nhomCauHoiRepository.SelectAllBy_MaDeThi(ma_de_thi))
            {
                while (dataReader.Read())
                {
                    NhomCauHoiDto nhomCauHoi = GetProperty(dataReader);
                    list.Add(nhomCauHoi);
                }
            }
            return list;
        }
        public async Task<NhomCauHoiDto> SelectOne(int ma_nhom)
        {
            NhomCauHoiDto nhomCauHoi = new();
            using(IDataReader dataReader = await _nhomCauHoiRepository.SelectOne(ma_nhom))
            {
                if (dataReader.Read())
                {
                    nhomCauHoi = GetProperty(dataReader);
                }
            }
            return nhomCauHoi;
        }
    }
}
