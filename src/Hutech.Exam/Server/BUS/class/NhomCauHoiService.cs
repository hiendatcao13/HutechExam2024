using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
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
        public async Task<int> Insert(int ma_de_thi, string ten_nhom, int kieu_noi_dung, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha, int so_cau_lay, bool la_cau_hoi_nhom)
        {
            return Convert.ToInt32(await _nhomCauHoiRepository.Insert(ma_de_thi, ten_nhom, kieu_noi_dung, noi_dung, so_cau_hoi, hoan_vi, thu_tu, ma_nhom_cha, so_cau_lay, la_cau_hoi_nhom) ?? -1);
        }
        public async Task<int> Update(int ma_nhom, int ma_de_thi, string ten_nhom, int kieu_noi_dung, string noi_dung, int so_cau_hoi, bool hoan_vi, int thu_tu, int ma_nhom_cha)
        {
            return await _nhomCauHoiRepository.Update(ma_nhom, ma_de_thi, ten_nhom, kieu_noi_dung, noi_dung, so_cau_hoi, hoan_vi, thu_tu, ma_nhom_cha);
        }
        public async Task<int> Remove(int ma_nhom)
        {
            return await _nhomCauHoiRepository.Remove(ma_nhom);
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
