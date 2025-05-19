using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietBaiThiService(IChiTietBaiThiRepository chiTietBaiThiRepository, IMapper mapper)
    {
        private readonly IChiTietBaiThiRepository _chiTietBaiThiRepository = chiTietBaiThiRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 11; // số lượng cột trong bảng ChiTietBaiThi

        public ChiTietBaiThiDto GetProperty(IDataReader dataReader, int start = 0)
        {
            ChiTietBaiThi chiTietBaiThi = new()
            {
                MaChiTietBaiThi = dataReader.GetInt64(0 + start),
                MaChiTietCaThi = dataReader.GetInt32(1 + start),
                MaDeHv = dataReader.GetInt64(2 + start),
                MaNhom = dataReader.GetInt32(3 + start),
                MaClo = dataReader.GetInt32(4 + start),
                MaCauHoi = dataReader.GetInt32(5 + start),
                CauTraLoi = dataReader.IsDBNull(6 + start) ? null : dataReader.GetInt32(6 + start),
                NgayTao = dataReader.GetDateTime(7 + start),
                NgayCapNhat = dataReader.IsDBNull(8 + start) ? null : dataReader.GetDateTime(8 + start),
                KetQua = dataReader.IsDBNull(9 + start) ? null : dataReader.GetBoolean(9 + start),
                ThuTu = dataReader.GetInt32(10 + start)
            };
            return _mapper.Map<ChiTietBaiThiDto>(chiTietBaiThi);
        }
        public async Task<long> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, DateTime NgayTao, int ThuTu)
        {
            return (long)(await _chiTietBaiThiRepository.Insert(ma_chi_tiet_ca_thi, MaDeHV, MaNhom, MaCauHoi, MaClo, NgayTao, ThuTu) ?? -1);
        }
        public async Task Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            await _chiTietBaiThiRepository.Update(MaChiTietBaiThi, CauTraLoi, NgayCapNhat, KetQua);
        }
        public async Task Update_v2(int MaChiTietCaThi, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            await _chiTietBaiThiRepository.Update_v2(MaChiTietCaThi, MaCauHoi, MaClo, CauTraLoi, NgayCapNhat, KetQua);
        }
        public async Task Save(int MaChiTietCaThi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayTao, DateTime NgayCapNhat, bool KetQua, int ThuTu)
        {
            await _chiTietBaiThiRepository.Save(MaChiTietCaThi, MaDeHV, MaNhom, MaCauHoi, MaClo, CauTraLoi, NgayTao, NgayCapNhat, KetQua, ThuTu);
        }
        public async Task Save_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            await _chiTietBaiThiRepository.Save_Batch(chiTietBaiThis);
        }
        public async Task Insert_Batch(List<ChiTietBaiThiDto> chiTietBaiThis)
        {
            await _chiTietBaiThiRepository.Insert_Batch(chiTietBaiThis);
        }
        public async Task<List<ChiTietBaiThiDto>> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            List<ChiTietBaiThiDto> result = [];
            using (IDataReader dataReader = await _chiTietBaiThiRepository.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi))
            {
                while (dataReader.Read())
                {
                    ChiTietBaiThiDto chiTietBaiThi = GetProperty(dataReader);
                    result.Add(chiTietBaiThi);
                }
            }
            return result;

        }
        public async Task<List<int>> DaThi(int ma_chi_tiet_ca_thi)
        {
            string result = await _chiTietBaiThiRepository.DaThi(ma_chi_tiet_ca_thi);
            return result.Split(";;;").Select(int.Parse).ToList();
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
                    chiTietBaiThi = GetProperty(dataReader);
                }
            }
            return chiTietBaiThi;
        }



        public Dictionary<int, int> GetDapAnKhoanh_SelectByListCTBT(List<ChiTietBaiThiRequest?> listChiTietBaiThi)
        {
            Dictionary<int, int> result = [];
            foreach (var chiTietBaiThi in listChiTietBaiThi)
            {
                if (chiTietBaiThi != null && chiTietBaiThi.CauTraLoi != null)
                {
                    result[chiTietBaiThi.MaCauHoi] = (int)chiTietBaiThi.CauTraLoi;
                }
            }
            return result;
        }
        public List<bool> GetDungSai_SelectByListCTBT_DapAn(Dictionary<int, ChiTietBaiThiRequest> chiTietBaiThis, Dictionary<int, int> dapAns)
        {
            List<bool> result = [];
            foreach(var item in dapAns)
            {
                if (chiTietBaiThis.ContainsKey(item.Key))
                {
                    if (item.Value == chiTietBaiThis[item.Key].CauTraLoi)
                    {
                        chiTietBaiThis[item.Key].KetQua = true;
                        result.Add(true);
                    }
                    else
                    {
                        chiTietBaiThis[item.Key].KetQua = false;
                        result.Add(false);
                    }
                }
            }
            return result;
        }
    }
}
