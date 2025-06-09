using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Request.CauHoi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class CauHoiService(ICauHoiRepository cauHoiRepository)
    {
        private readonly ICauHoiRepository _cauHoiRepository = cauHoiRepository;


        public async Task<int> Insert(CauHoiCreateRequest cauHoi)
        {
            return await _cauHoiRepository.Insert(cauHoi.MaClo, cauHoi.MaNhom, cauHoi.TieuDe, cauHoi.KieuNoiDung, cauHoi.NoiDung, cauHoi.ThuTu, cauHoi.GhiChu, cauHoi.HoanVi);
        }

        public async Task<bool> Update(int id, CauHoiUpdateRequest cauHoi)
        {
            return await _cauHoiRepository.Update(id, cauHoi.MaNhom, cauHoi.MaClo, cauHoi.TieuDe, cauHoi.KieuNoiDung, cauHoi.NoiDung, cauHoi.ThuTu, cauHoi.GhiChu, cauHoi.HoanVi);
        }

        public async Task<bool> Remove(int ma_cau_hoi)
        {
            return await _cauHoiRepository.Remove(ma_cau_hoi);
        }

        public async Task<bool> ForceRemove(int ma_cau_hoi)
        {
            return await _cauHoiRepository.ForceRemove(ma_cau_hoi);
        }

        public async Task<CauHoiDto> SelectOne(int ma_cau_hoi)
        {
            return await _cauHoiRepository.SelectOne(ma_cau_hoi);
        }

        public async Task<List<CauHoiDto>> SelectBy_MaNhom(int ma_nhom)
        {
            return await _cauHoiRepository.SelectBy_MaNhom(ma_nhom);
        }
    }
}
