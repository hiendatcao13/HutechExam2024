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
        #region Private Fields
        private readonly IAudioListenedRepository _audioListenedRepository = audioListenedRepository;
        #endregion

        #region Public Methods
        public async Task<int> Save(AudioListenedDto audio)
        {
            return await _audioListenedRepository.UpdateAsync(audio.MaChiTietCaThi, audio.TenFile ?? string.Empty);
        }
        #endregion


    }
}
