using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietBaiThiService(IChiTietBaiThiRepository chiTietBaiThiRepository)
    {
        #region Private Fields
        private readonly IChiTietBaiThiRepository _chiTietBaiThiRepository = chiTietBaiThiRepository;
        #endregion

        #region Public Methods
        public async Task<long> Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, int MaClo, DateTime NgayTao, int ThuTu)
        {
            return await _chiTietBaiThiRepository.Insert(ma_chi_tiet_ca_thi, MaDeHV, MaNhom, MaCauHoi, MaClo, NgayTao, ThuTu);
        }

        public async Task<bool> Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            return await _chiTietBaiThiRepository.Update(MaChiTietBaiThi, CauTraLoi, NgayCapNhat, KetQua);
        }

        public async Task<bool> Update_v2(int MaChiTietCaThi, int MaCauHoi, int MaClo, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            return await _chiTietBaiThiRepository.Update_v2(MaChiTietCaThi, MaCauHoi, MaClo, CauTraLoi, NgayCapNhat, KetQua);
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
            return await _chiTietBaiThiRepository.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi);

        }

        public async Task<bool> Delete(long ma_chi_tiet_bai_thi)
        {
            return await _chiTietBaiThiRepository.Delete(ma_chi_tiet_bai_thi);
        }

        public async Task<ChiTietBaiThiDto> SelectOne_v2(int ma_chi_tiet_ca_thi, long ma_de_hv, int ma_nhom, int ma_cau_hoi)
        {
            return await _chiTietBaiThiRepository.SelectOne_v2(ma_chi_tiet_ca_thi, ma_de_hv, ma_nhom, ma_cau_hoi);
        }



        public Dictionary<int, int> GetDapAnKhoanh_SelectByListCTBT(List<ChiTietBaiThiRequest> listChiTietBaiThi)
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

        public void UpdateDungSai_SelectByListCTBT_DapAn(Dictionary<int, ChiTietBaiThiRequest> chiTietBaiThis, Dictionary<int, int> dapAns)
        {

            foreach (var (cauSo, dapAn) in dapAns)
            {

                if (chiTietBaiThis.TryGetValue(cauSo, out var chiTiet))
                {
                    chiTiet.KetQua = (dapAn == chiTiet.CauTraLoi);
                }
            }
        }


        public (List<bool?>, int, double) GetDungSai_SelectBy_DapAnKhoanh(Dictionary<int, int?> dapAnKhoanhs, Dictionary<int, int> dapAns)
        {
            var ketQuaList = new List<bool?>(dapAns.Count);
            int soCauDung = 0;

            foreach (var (cauSo, dapAn) in dapAns)
            {
                bool? ketQua = null;

                if (dapAnKhoanhs.TryGetValue(cauSo, out var chiTiet) && chiTiet != null)
                {
                    ketQua = (dapAn == chiTiet);
                }

                if (ketQua == true)
                {
                    soCauDung++;
                }

                ketQuaList.Add(ketQua);
            }

            double diem = TinhDem(ketQuaList.Count, soCauDung);
            return (ketQuaList, soCauDung, diem);
        }
        #endregion

        #region Private Methods
        private double TinhDem(int tong_so_cau, int so_cau_dung)
        {
            if (so_cau_dung == 0)
                return 0;
            double diem = ((double)so_cau_dung / tong_so_cau) * 10;
            return Math.Round(diem * 4, MidpointRounding.AwayFromZero) / 4.0;
        }
        #endregion
    }
}
