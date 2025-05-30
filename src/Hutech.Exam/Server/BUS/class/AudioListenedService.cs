using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class AudioListenedService(IAudioListenedRepository audioListenedRepository, IMapper mapper)
    {
        private readonly IAudioListenedRepository _audioListenedRepository = audioListenedRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 4; // số lượng cột trong bảng AudioListened

        public AudioListenedDto GetProperty(IDataReader dataReader, int start = 0)
        {
            AudioListened audioListened = new()
            {
                ListenId = dataReader.GetInt64(0 + start),
                MaChiTietCaThi = dataReader.GetInt32(1 + start),
                MaNhom = dataReader.GetInt32(2 + start),
                FileName = dataReader.IsDBNull(3 + start) ? null : dataReader.GetString(3 + start),
                ListenedCount = dataReader.GetInt32(4 + start)
            };
            return _mapper.Map<AudioListenedDto>(audioListened);
        }
        public async Task<int> SelectOne(int ma_chi_tiet_ca_thi, string fileName)
        {
            int listenedCount = 0;
            using (IDataReader dataReader = await _audioListenedRepository.SelectOne(ma_chi_tiet_ca_thi, fileName))
            {
                if (dataReader.Read())
                {
                    listenedCount = dataReader.GetInt32(0);
                }
            }
            return listenedCount;
        }
        public async Task<int> Save(AudioListenedDto audio)
        {
            return Convert.ToInt32(await _audioListenedRepository.Save(audio.MaChiTietCaThi, audio.MaNhom));
        }
    }
}
