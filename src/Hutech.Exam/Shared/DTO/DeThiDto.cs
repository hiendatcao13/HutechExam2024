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

        public long MaDeThi { get; set; }

        public int MaMonHoc { get; set; }

        public string TenDeThi { get; set; } = null!;

        public string? KyHieuDe { get; set; }

        public Guid Guid {  get; set; }

        public DateTime NgayTao { get; set; }

        //custom
        public bool DaChon { get; set; } = false;

        public DeThiDto(long maDeThi, int maMonHoc, string tenDeThi, string? kyHieuDe, Guid guid, DateTime ngayTao, bool daChon)
        {
            MaDeThi = maDeThi;
            MaMonHoc = maMonHoc;
            TenDeThi = tenDeThi;
            KyHieuDe = kyHieuDe;
            Guid = guid;
            NgayTao = ngayTao;
            DaChon = daChon;
        }

        public DeThiDto(DeThiDto other)
        {
            MaDeThi = other.MaDeThi;
            MaMonHoc = other.MaMonHoc;
            TenDeThi = other.TenDeThi;
            KyHieuDe = other.KyHieuDe;
            Guid = other.Guid;
            NgayTao = other.NgayTao;
            DaChon = other.DaChon;
        }

        public override string ToString()
        {
            return TenDeThi;
        }
    }
}
