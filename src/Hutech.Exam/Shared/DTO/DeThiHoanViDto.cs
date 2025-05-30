using Hutech.Exam.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Shared.DTO
{
    public class DeThiHoanViDto
    {
        public DeThiHoanViDto()
        {
        }

        public long MaDeHv { get; set; }

        public int MaDeThi { get; set; }

        public string? KyHieuDe { get; set; }

        public DateTime NgayTao { get; set; }

        public Guid? Guid { get; set; }

        public virtual DeThiDto MaDeThiNavigation { get; set; } = null!;

        public virtual ICollection<NhomCauHoiHoanViDto> NhomCauHoiHoanVis { get; set; } = new List<NhomCauHoiHoanViDto>();

        public DeThiHoanViDto(long maDeHv, int maDeThi, string? kyHieuDe, DateTime ngayTao, Guid? guid, DeThiDto maDeThiNavigation, ICollection<NhomCauHoiHoanViDto> nhomCauHoiHoanVis)
        {
            MaDeHv = maDeHv;
            MaDeThi = maDeThi;
            KyHieuDe = kyHieuDe;
            NgayTao = ngayTao;
            Guid = guid;
            MaDeThiNavigation = maDeThiNavigation;
            NhomCauHoiHoanVis = nhomCauHoiHoanVis;
        }

        public DeThiHoanViDto(DeThiHoanViDto other)
        {
            MaDeHv = other.MaDeHv;
            MaDeThi = other.MaDeThi;
            KyHieuDe = other.KyHieuDe;
            NgayTao = other.NgayTao;
            Guid = other.Guid;
            MaDeThiNavigation = other.MaDeThiNavigation;
            NhomCauHoiHoanVis = other.NhomCauHoiHoanVis;
        }
    }
}
