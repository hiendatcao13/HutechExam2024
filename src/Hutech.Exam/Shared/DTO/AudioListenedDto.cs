using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class AudioListenedDto
    {
        public long ListenId { get; set; }

        public int MaChiTietCaThi { get; set; }

        public string FileName { get; set; } = null!;

        public int ListenedCount { get; set; }
        public override string ToString()
        {
            return FileName;
        }
    }
}
