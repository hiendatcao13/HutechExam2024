using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Khoa;
using Hutech.Exam.Shared.DTO.Request.Lop;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class LopService(ILopRepository lopRepository)
    {
        #region Private Fields
        private readonly ILopRepository _lopRepository = lopRepository;
        #endregion

        #region Public Methods
        public async Task<LopDto> SelectBy_ten_lop(string ten_lop)
        {
            return await _lopRepository.SelectBy_ten_lop(ten_lop);
        }

        public async Task<Paged<LopDto>> SelectBy_ma_khoa_Paged(int ma_khoa, int pageNumber, int pageSize)
        {
            return await _lopRepository.SelectBy_ma_khoa_Paged(ma_khoa, pageNumber, pageSize);
        }

        public async Task<LopDto> SelectOne(int ma_lop)
        {
            return await _lopRepository.SelectOne(ma_lop);
        }

        public async Task<int> Insert(LopCreateRequest lop)
        {
            return await _lopRepository.Insert(lop.TenLop, lop.NgayBatDau, lop.MaKhoa);
        }

        public async Task<bool> Update(int id, LopUpdateRequest lop)
        {
            return await _lopRepository.Update(id, lop.TenLop, lop.NgayBatDau, lop.MaKhoa);
        }

        public async Task<bool> Remove(int ma_lop)
        {
            return await _lopRepository.Remove(ma_lop);
        }

        public async Task<bool> ForceRemove(int ma_lop)
        {
            return await _lopRepository.ForceRemove(ma_lop);
        }
        #endregion

    }
}
