using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class AudioListenedDto
    {
        public AudioListenedDto()
        {
        }

        public long ListenId { get; set; }

        public int MaNhom { get; set; }

        public int MaChiTietCaThi { get; set; }

        public string? FileName { get; set; }

        public int ListenedCount { get; set; }

        public AudioListenedDto(long listenId, int maNhom, int maChiTietCaThi, string? fileName, int listenedCount)
        {
            ListenId = listenId;
            MaNhom = maNhom;
            MaChiTietCaThi = maChiTietCaThi;
            FileName = fileName;
            ListenedCount = listenedCount;
        }

        public AudioListenedDto(AudioListenedDto other)
        {
            ListenId = other.ListenId;
            MaNhom = other.MaNhom;
            MaChiTietCaThi = other.MaChiTietCaThi;
            FileName = other.FileName;
            ListenedCount = other.ListenedCount;
        }
    }
}
