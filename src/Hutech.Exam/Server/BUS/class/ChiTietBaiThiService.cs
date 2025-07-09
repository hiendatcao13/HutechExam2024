using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi;
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



        public void UpdateDungSai_SelectByListCTBT_DapAn(Dictionary<Guid, ChiTietBaiThiRequest> chiTietBaiThis, Dictionary<Guid, Guid> dapAns)
        {

            foreach (var (cauSo, dapAn) in dapAns)
            {

                if (chiTietBaiThis.TryGetValue(cauSo, out var chiTiet))
                {
                    chiTiet.KetQua = (dapAn == chiTiet.CauTraLoi);
                }
            }
        }


        public (List<bool?>, int, double) GetDungSai_SelectBy_DapAnKhoanh(Dictionary<Guid, Guid?> dapAnKhoanhs, Dictionary<Guid, Guid> dapAns)
        {
            var ketQuaList = new List<bool?>(dapAns.Count);
            int soCauDung = 0;

            foreach (var (cauSo, dapAn) in dapAns)
            {
                bool? ketQua = null;

                if (dapAnKhoanhs.TryGetValue(cauSo, out var chiTiet))
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
