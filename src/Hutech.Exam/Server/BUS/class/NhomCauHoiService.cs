using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class NhomCauHoiService
    {
        private readonly INhomCauHoiRepository _nhomCauHoiRepository;
        private readonly IMapper _mapper;
        public NhomCauHoiService(INhomCauHoiRepository nhomCauHoiRepository, IMapper mapper)
        {
            _nhomCauHoiRepository = nhomCauHoiRepository;
            _mapper = mapper;   
        }
        private NhomCauHoiDto getProperty(IDataReader dataReader)
        {
            NhomCauHoi nhomCauHoi = new()
            {
                MaNhom = dataReader.GetInt32(0),
                MaDeThi = dataReader.GetInt32(1),
                TenNhom = dataReader.GetString(2),
                NoiDung = dataReader.IsDBNull(3) ? null : dataReader.GetString(3),
                SoCauHoi = dataReader.GetInt32(4),
                HoanVi = dataReader.GetBoolean(5),
                ThuTu = dataReader.GetInt32(6),
                MaNhomCha = dataReader.GetInt32(7),
                SoCauLay = dataReader.GetInt32(8),
                LaCauHoiNhom = dataReader.IsDBNull(9) ? null : dataReader.GetBoolean(9)
            };
            return _mapper.Map<NhomCauHoiDto>(nhomCauHoi);
        }
        public async Task<List<NhomCauHoiDto>> SelectBy_MaDeThi(int ma_de_thi)
        {
            List<NhomCauHoiDto> list = new();
            using(IDataReader dataReader = await _nhomCauHoiRepository.SelectBy_MaDeThi(ma_de_thi))
            {
                while (dataReader.Read())
                {
                    NhomCauHoiDto nhomCauHoi = getProperty(dataReader);
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
                    nhomCauHoi = getProperty(dataReader);
                }
            }
            return nhomCauHoi;
        }
    }
}
