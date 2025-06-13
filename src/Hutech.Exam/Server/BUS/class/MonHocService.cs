using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.MonHoc;
using Hutech.Exam.Shared.Models;
using System.Data;
using System.Data.Common;

namespace Hutech.Exam.Server.BUS
{
    public class MonHocService(IMonHocRepository monHocRepository)
    {
        #region Private Fields
        private readonly IMonHocRepository _monHocRepository = monHocRepository;
        #endregion

        #region Public Methods
        public async Task<MonHocDto> SelectOne(int ma_mon_hoc)
        {
            return await _monHocRepository.SelectOne(ma_mon_hoc);
        }

        public async Task<List<MonHocDto>> GetAll()
        {
            return await _monHocRepository.GetAll();
        }

        public async Task<Paged<MonHocDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            return await _monHocRepository.GetAll_Paged(pageNumber, pageSize);
        }

        public async Task<int> Insert(MonHocCreateRequest monHoc)
        {
            return await _monHocRepository.Insert(monHoc.MaSoMonHoc, monHoc.TenMonHoc);
        }

        public async Task<bool> Update(int id, MonHocUpdateRequest monHoc)
        {
            return await _monHocRepository.Update(id, monHoc.MaSoMonHoc, monHoc.TenMonHoc);
        }
        public async Task<bool> Remove(int ma_mon_hoc)
        {
            return await _monHocRepository.Remove(ma_mon_hoc);
        }

        public async Task<bool> ForceRemove(int ma_mon_hoc)
        {
            return await _monHocRepository.ForceRemove(ma_mon_hoc);
        }
        #endregion

    }
}
