using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.NhomCauHoi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class NhomCauHoiService(INhomCauHoiRepository nhomCauHoiRepository)
    {
        #region Private Fields
        private readonly INhomCauHoiRepository _nhomCauHoiRepository = nhomCauHoiRepository;
        #endregion

        #region Public Methods
        public async Task<int> Insert(NhomCauHoiCreateRequest nhomCauHoi)
        {
            return await _nhomCauHoiRepository.Insert(nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "", nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi,
                nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha, nhomCauHoi.SoCauLay, nhomCauHoi.LaCauHoiNhom);
        }

        public async Task<bool> Update(int id, NhomCauHoiUpdateRequest nhomCauHoi)
        {
            return await _nhomCauHoiRepository.Update(id, nhomCauHoi.MaDeThi, nhomCauHoi.TenNhom, nhomCauHoi.KieuNoiDung, nhomCauHoi.NoiDung ?? "",
                nhomCauHoi.SoCauHoi, nhomCauHoi.HoanVi, nhomCauHoi.ThuTu, nhomCauHoi.MaNhomCha);
        }

        public async Task<bool> Remove(int ma_nhom)
        {
            return await _nhomCauHoiRepository.Remove(ma_nhom);
        }

        public async Task<bool> ForceRemove(int ma_nhom)
        {
            return await _nhomCauHoiRepository.ForceRemove(ma_nhom);
        }

        public async Task<List<NhomCauHoiDto>> SelectAllBy_MaDeThi(int ma_de_thi)
        {
            return await _nhomCauHoiRepository.SelectAllBy_MaDeThi(ma_de_thi);
        }

        public async Task<NhomCauHoiDto> SelectOne(int ma_nhom)
        {
            return await _nhomCauHoiRepository.SelectOne(ma_nhom);
        }
        #endregion

    }
}
