using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request;
using Hutech.Exam.Shared.DTO.Request.ChiTietCaThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietCaThiService(IChiTietCaThiRepository chiTietCaThiRepository, SinhVienService sinhVienService)
    {
        #region Private Fields
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository = chiTietCaThiRepository;
        #endregion

        #region Public Methods
        private readonly SinhVienService _sinhVienService = sinhVienService;

        public async Task<ChiTietCaThiDto> SelectOne(int chi_tiet_ca_thi)
        {
            return await _chiTietCaThiRepository.SelectOne(chi_tiet_ca_thi);
        }

        public async Task<Paged<ChiTietCaThiDto>> SelectBy_MaCaThi_Paged(int ma_ca_thi, int pageNumber, int pageSize)
        {
            return await _chiTietCaThiRepository.SelectBy_ma_ca_thi_Paged(ma_ca_thi, pageNumber, pageSize);
        }

        public async Task<Paged<ChiTietCaThiDto>> SelectBy_MaCaThi_Search_Paged(int ma_ca_thi, string keyword, int pageNumber, int pageSize)
        {
            return await _chiTietCaThiRepository.SelectBy_ma_ca_thi_Search_Paged(ma_ca_thi, keyword, pageNumber, pageSize);
        }

        public async Task<ChiTietCaThiDto> SelectBy_MaSinhVienThi(long ma_sinh_vien)
        {
            var result = await _chiTietCaThiRepository.SelectBy_MaSinhVienThi(ma_sinh_vien);

            if(result.MaChiTietCaThi == 0)
            {
                result.MaSinhVienNavigation = await _sinhVienService.SelectOne(ma_sinh_vien);
            }

            return result;
        }
        public async Task<bool> UpdateBatDau(int ma_chi_tiet_ca_thi, DateTime thoi_gian_bat_dau)
        {
            return await _chiTietCaThiRepository.UpdateBatDau(ma_chi_tiet_ca_thi, thoi_gian_bat_dau);
        }

        public async Task<bool> UpdateKetThuc(int id, ChiTietCaThiUpdateKTThiRequest chiTietCaThi)
        {
            return await _chiTietCaThiRepository.UpdateKetThuc(id, chiTietCaThi.ThoiGianKetThuc, chiTietCaThi.Diem, chiTietCaThi.SoCauDung, chiTietCaThi.TongSoCau);
        }

        public async Task<bool> CongGio(int id, int gioCongThem)
        {
            return await _chiTietCaThiRepository.CongGio(id, gioCongThem);
        }

        public async Task<int> Insert(ChiTietCaThiCreateRequest chiTietCaThi)
        {
            return await _chiTietCaThiRepository.Insert(chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, -1);
        }

        public async Task Insert_Batch(List<ChiTietCaThiCreateBatchRequest> chiTietCaThis)
        {
            await _chiTietCaThiRepository.Insert_Batch(chiTietCaThis);
        }


        public async Task<bool> Remove(int ma_chi_tiet_ca_thi)
        {
            return await _chiTietCaThiRepository.Remove(ma_chi_tiet_ca_thi);
        }

        public async Task<bool> ForceRemove(int ma_chi_tiet_ca_thi)
        {
            return await _chiTietCaThiRepository.ForceRemove(ma_chi_tiet_ca_thi);
        }

        public async Task<bool> Update(int id, ChiTietCaThiUpdateRequest chiTietCaThi)
        {
            return await _chiTietCaThiRepository.Update(id, chiTietCaThi.MaCaThi, chiTietCaThi.MaSinhVien, chiTietCaThi.MaDeThi, -1);
        }
        #endregion

    }
}
