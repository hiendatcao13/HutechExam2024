using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class DeThiDto
    {
        public DeThiDto()
        {
        }

        public int MaDeThi { get; set; }

        public int MaMonHoc { get; set; }

        public string TenDeThi { get; set; } = null!;

        public Guid? Guid {  get; set; }

        public DateTime NgayTao { get; set; }

        public override string ToString()
        {
            return TenDeThi;
        }
    }
}
