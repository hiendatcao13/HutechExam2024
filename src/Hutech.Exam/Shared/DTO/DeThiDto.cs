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

        public DateTime NgayTao { get; set; }

        public int NguoiTao { get; set; }

        public string? GhiChu { get; set; }

        public bool? LuuTam { get; set; }

        public bool DaDuyet { get; set; }

        public int? TongSoDeHoanVi { get; set; }

        public bool BoChuongPhan { get; set; }

        public virtual ICollection<DeThiHoanViDto> DeThiHoanVis { get; set; } = new List<DeThiHoanViDto>();

        public virtual ICollection<NhomCauHoiDto> NhomCauHois { get; set; } = new List<NhomCauHoiDto>();

        public override string ToString()
        {
            return TenDeThi;
        }

        public DeThiDto(int maDeThi, int maMonHoc, string tenDeThi, DateTime ngayTao, int nguoiTao, string? ghiChu, bool? luuTam, bool daDuyet, int? tongSoDeHoanVi, bool boChuongPhan, ICollection<DeThiHoanViDto> deThiHoanVis, ICollection<NhomCauHoiDto> nhomCauHois)
        {
            MaDeThi = maDeThi;
            MaMonHoc = maMonHoc;
            TenDeThi = tenDeThi;
            NgayTao = ngayTao;
            NguoiTao = nguoiTao;
            GhiChu = ghiChu;
            LuuTam = luuTam;
            DaDuyet = daDuyet;
            TongSoDeHoanVi = tongSoDeHoanVi;
            BoChuongPhan = boChuongPhan;
            DeThiHoanVis = deThiHoanVis;
            NhomCauHois = nhomCauHois;
        }

        public DeThiDto(DeThiDto other)
        {
            MaDeThi = other.MaDeThi;
            MaMonHoc = other.MaMonHoc;
            TenDeThi = other.TenDeThi;
            NgayTao = other.NgayTao;
            NguoiTao = other.NguoiTao;
            GhiChu = other.GhiChu;
            LuuTam = other.LuuTam;
            DaDuyet = other.DaDuyet;
            TongSoDeHoanVi = other.TongSoDeHoanVi;
            BoChuongPhan = other.BoChuongPhan;
            DeThiHoanVis = other.DeThiHoanVis;
            NhomCauHois = other.NhomCauHois;
        }
    }
}
