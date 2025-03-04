using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietBaiThiService
    {
        private readonly IChiTietBaiThiRepository _chiTietBaiThiRepository;
        private readonly IMapper _mapper;
        public ChiTietBaiThiService(IChiTietBaiThiRepository chiTietBaiThiRepository, IMapper mapper)
        {
            _chiTietBaiThiRepository = chiTietBaiThiRepository;
            _mapper = mapper;
        }
        private ChiTietBaiThiDto getProperty(IDataReader dataReader)
        {
            ChiTietBaiThi chiTietBaiThi = new()
            {
                MaChiTietBaiThi = dataReader.GetInt64(0),
                MaChiTietCaThi = dataReader.GetInt32(1),
                MaDeHv = dataReader.GetInt64(2),
                MaNhom = dataReader.GetInt32(3),
                MaCauHoi = dataReader.GetInt32(4),
                CauTraLoi = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5),
                NgayTao = dataReader.GetDateTime(6),
                NgayCapNhat = dataReader.IsDBNull(7) ? null : dataReader.GetDateTime(7),
                KetQua = dataReader.IsDBNull(8) ? null : dataReader.GetBoolean(8),
                ThuTu = dataReader.GetInt32(9)
            };
            return _mapper.Map<ChiTietBaiThiDto>(chiTietBaiThi);
        }
        public async Task<long> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, DateTime NgayTao, int ThuTu)
        {
            return (long)(await _chiTietBaiThiRepository.Insert(ma_chi_tiet_ca_thi, MaDeHV, MaNhom, MaCauHoi, NgayTao, ThuTu) ?? -1);
        }
        public async Task<int> Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            return await _chiTietBaiThiRepository.Update(MaChiTietBaiThi, CauTraLoi, NgayCapNhat, KetQua);
        }
        public async Task<List<ChiTietBaiThiDto>> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            List<ChiTietBaiThiDto> result = new();
            using (IDataReader dataReader = await _chiTietBaiThiRepository.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi))
            {
                while (dataReader.Read())
                {
                    ChiTietBaiThiDto chiTietBaiThi = getProperty(dataReader);
                    result.Add(chiTietBaiThi);
                }
            }
            return result;

        }
        public async Task insertChiTietBaiThis_SelectByChiTietDeThiHV(List<CustomDeThi>? customDeThis, int ma_chi_tiet_ca_thi, long ma_de_hoan_vi)
        {
            int stt = 0;
            if (customDeThis == null)
                return;
            foreach (var item in customDeThis)
            {
                await this.Insert(ma_chi_tiet_ca_thi, ma_de_hoan_vi, item.MaNhom, item.MaCauHoi, DateTime.Now, ++stt);
            }
        }
        public async Task updateChiTietBaiThis(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            foreach (var item in chiTietBaiThis)
            {
                if (item.CauTraLoi != null && item.KetQua != null)
                {
                    var chiTiet = await this.SelectOne_v2(item.MaChiTietCaThi, item.MaDeHv, item.MaNhom, item.MaCauHoi);
                    long ma_chi_tiet_bai_thi = chiTiet.MaChiTietBaiThi;
                    await this.Update(ma_chi_tiet_bai_thi, (int)item.CauTraLoi, DateTime.Now, (bool)item.KetQua);
                }
            }
        }
        public async Task insertChiTietBaiThis(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            foreach (var item in chiTietBaiThis)
            {
                await this.Insert(item.MaChiTietCaThi, item.MaDeHv, item.MaNhom, item.MaCauHoi, DateTime.Now, item.ThuTu);
            }
        }
        public async Task<int> Delete(long ma_chi_tiet_bai_thi)
        {
            return await _chiTietBaiThiRepository.Delete(ma_chi_tiet_bai_thi);
        }
        public async Task<ChiTietBaiThiDto> SelectOne_v2(int ma_chi_tiet_ca_thi, long ma_de_hv, int ma_nhom, int ma_cau_hoi)
        {
            ChiTietBaiThiDto chiTietBaiThi = new();
            using (IDataReader dataReader = await _chiTietBaiThiRepository.SelectOne_v2(ma_chi_tiet_ca_thi, ma_de_hv, ma_nhom, ma_cau_hoi))
            {
                if (dataReader.Read())
                {
                    chiTietBaiThi = getProperty(dataReader);
                }
            }
            return chiTietBaiThi;
        }
    }
}
