using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class ChiTietDeThiHoanViService(IChiTietDeThiHoanViRepository chiTietDeThiHoanViRepository)
    {
        private readonly IChiTietDeThiHoanViRepository _chiTietDeThiHoanViRepository = chiTietDeThiHoanViRepository;

        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV(long maDeHV)
        {
            return await _chiTietDeThiHoanViRepository.SelectBy_MaDeHV(maDeHV);
        }

        public async Task<List<ChiTietDeThiHoanViDto>> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            return await _chiTietDeThiHoanViRepository.SelectBy_MaDeHV_MaNhom(ma_de_hoan_vi, ma_nhom);
        }
    }
}
