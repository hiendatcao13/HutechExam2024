using AutoMapper;
using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class NhomCauHoiHoanViService(INhomCauHoiHoanViRepository nhomCauHoiHoanViRepository)
    {
        private readonly INhomCauHoiHoanViRepository _nhomCauHoiHoanViRepository = nhomCauHoiHoanViRepository;
        

        public async Task<NhomCauHoiHoanViDto> SelectOne(long ma_de_hoan_vi, int ma_nhom)
        {
            return await _nhomCauHoiHoanViRepository.SelectOne(ma_de_hoan_vi, ma_nhom);
        }


        public async Task<List<NhomCauHoiHoanViDto>> SelectBy_MaDeHV(long ma_de_hoan_vi)
        {
            return await _nhomCauHoiHoanViRepository.SelectBy_MaDeHV(ma_de_hoan_vi);
        }
    }
}
