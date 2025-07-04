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

        public int MaNhom { get; set; }

        public int MaChiTietCaThi { get; set; }

        public string? TenFile { get; set; }

        public int SoLangNghe { get; set; }

    }
}
