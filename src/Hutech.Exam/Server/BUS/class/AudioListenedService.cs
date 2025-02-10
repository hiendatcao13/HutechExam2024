using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class AudioListenedService
    {
        private readonly IAudioListenedRepository _audioListenedRepository;
        private readonly IMapper _mapper;
        public AudioListenedService(IAudioListenedRepository audioListenedRepository, IMapper mapper)
        {
            _audioListenedRepository = audioListenedRepository;
            _mapper = mapper;
        }
        private AudioListenedDto getProperty(IDataReader dataReader)
        {
            TblAudioListened audioListened = new()
            {
                ListenId = dataReader.GetInt64(0),
                MaChiTietCaThi = dataReader.GetInt32(1),
                FileName = dataReader.GetString(2),
                ListenedCount = dataReader.GetInt32(3)
            };
            return _mapper.Map<AudioListenedDto>(audioListened);
        }
        public int SelectOne(int ma_chi_tiet_ca_thi, string fileName)
        {
            int listenedCount = 0;
            using (IDataReader dataReader = _audioListenedRepository.SelectOne(ma_chi_tiet_ca_thi, fileName))
            {
                if (dataReader.Read())
                {
                    listenedCount = dataReader.GetInt32(0);
                }
            }
            return listenedCount;
        }
        public void Save(int ma_chi_tiet_ca_thi, string fileName)
        {
            try
            {
                _audioListenedRepository.Save(ma_chi_tiet_ca_thi, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
