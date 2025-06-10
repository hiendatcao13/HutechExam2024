using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;
using Hutech.Exam.Shared.DTO.Request.Khoa;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class KhoaService(IKhoaRepository khoaRepository)
    {
        private readonly IKhoaRepository _khoaRepository = khoaRepository;

        public async Task<KhoaDto> SelectOne(int ma_khoa)
        {
            return await _khoaRepository.SelectOne(ma_khoa);
        }

        public async Task<int> Insert(KhoaCreateRequest khoa)
        {
            return await _khoaRepository.Insert(khoa.TenKhoa, khoa.NgayThanhLap);
        }

        public async Task<bool> Update(int id, KhoaUpdateRequest khoa)
        {
            return await _khoaRepository.Update(id, khoa.TenKhoa, khoa.NgayThanhLap);
        }

        public async Task<bool> Remove(int ma_khoa)
        {
            return await _khoaRepository.Remove(ma_khoa);
        }

        public async Task<bool> ForceRemove(int ma_khoa)
        {
            return await _khoaRepository.ForceRemove(ma_khoa);
        }

        public async Task<List<KhoaDto>> GetAll()
        {
            return await _khoaRepository.GetAll();
        }

        public async Task<Paged<KhoaDto>> GetAll_Paged(int pageNumber, int pageSize)
        {
            return await _khoaRepository.GetAll_Paged(pageNumber, pageSize);
        }
    }
}
