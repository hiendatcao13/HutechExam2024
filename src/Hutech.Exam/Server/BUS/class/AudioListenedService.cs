using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class AudioListenedService(IAudioListenedRepository audioListenedRepository)
    {
        private readonly IAudioListenedRepository _audioListenedRepository = audioListenedRepository;

        public async Task<int> SelectOne(int ma_chi_tiet_ca_thi, string fileName)
        {
            return await _audioListenedRepository.SelectOne(ma_chi_tiet_ca_thi, fileName);
        }

        public async Task<int> Save(AudioListenedDto audio)
        {
            return await _audioListenedRepository.Save(audio.MaChiTietCaThi, audio.MaNhom);
        }
    }
}
