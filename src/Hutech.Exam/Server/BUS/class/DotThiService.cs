using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.DotThi;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DotThiService(IDotThiRepository dotThiRepository)
    {
        private readonly IDotThiRepository _dotThiRepository = dotThiRepository;


        public async Task<List<DotThiDto>> GetAll()
        {
            return await _dotThiRepository.GetAll();
        }

        public async Task<Paged<DotThiDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            return await _dotThiRepository.GetAll_Paged(pageNumber, pageSize);
        }

        public async Task<DotThiDto> SelectOne(int ma_dot_thi)
        {
            return await _dotThiRepository.SelectOne(ma_dot_thi);
        }

        public async Task<int> Insert(DotThiCreateRequest dotThi)
        {
            return await _dotThiRepository.Insert(dotThi.TenDotThi, dotThi.ThoiGianBatDau, dotThi.ThoiGianKetThuc, dotThi.NamHoc);
        }

        public async Task<bool> Update(int id, DotThiUpdateRequest dotThi)
        {
            return await _dotThiRepository.Update(id, dotThi.TenDotThi, dotThi.ThoiGianBatDau, dotThi.ThoiGianKetThuc, dotThi.NamHoc);
        }

        public async Task<bool> Remove(int ma_dot_thi)
        {
            return await _dotThiRepository.Remove(ma_dot_thi);
        }

        public async Task<bool> ForceRemove(int ma_dot_thi)
        {
            return await _dotThiRepository.ForceRemove(ma_dot_thi);
        }
    }
}
