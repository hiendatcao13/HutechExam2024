using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.CauTraLoi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CauTraLoiService(ICauTraLoiRepository cauTraLoiRepository)
    {
        #region Private Fields
        private readonly ICauTraLoiRepository _cauTraLoiRepository = cauTraLoiRepository;
        #endregion

        #region Public Methods
        public async Task<CauTraLoiDto> SelectOne(int ma_cau_tra_loi)
        {
            return await _cauTraLoiRepository.SelectOne(ma_cau_tra_loi);
        }

        public async Task<int> Insert(CauTraLoiCreateRequest cauTraLoi)
        {
            return await _cauTraLoiRepository.Insert(cauTraLoi.MaCauHoi, cauTraLoi.ThuTu, cauTraLoi.NoiDung, cauTraLoi.LaDapAn, cauTraLoi.HoanVi);
        }

        public async Task<bool> Update(int id, CauTraLoiUpdateRequest cauTraLoi)
        {
            return await _cauTraLoiRepository.Update(id, cauTraLoi.MaCauHoi, cauTraLoi.ThuTu, cauTraLoi.NoiDung, cauTraLoi.LaDapAn, cauTraLoi.HoanVi);
        }

        public async Task<bool> Remove(int ma_cau_tra_loi)
        {
            return await _cauTraLoiRepository.Remove(ma_cau_tra_loi);
        }

        public async Task<List<CauTraLoiDto>> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            return await _cauTraLoiRepository.SelectBy_MaCauHoi(ma_cau_hoi);
        }
        #endregion
    }
}
