using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class DeThiService
    {
        private readonly IDeThiRepository _deThiRepository;
        public DeThiService(IDeThiRepository deThiRepository)
        {
            _deThiRepository = deThiRepository;
        }
        private TblDeThi getProperty(IDataReader dataReader)
        {
            TblDeThi deThi = new TblDeThi();
            deThi.MaDeThi = dataReader.GetInt32(0);
            deThi.MaMonHoc = dataReader.GetInt32(1);
            deThi.TenDeThi = dataReader.GetString(2);
            deThi.NgayTao = dataReader.GetDateTime(3);
            deThi.NguoiTao = dataReader.GetInt32(4);
            deThi.GhiChu = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
            deThi.LuuTam = dataReader.GetBoolean(6);
            deThi.DaDuyet = dataReader.GetBoolean(7);
            deThi.TongSoDeHoanVi = dataReader.IsDBNull(8) ? null : dataReader.GetInt32(8);
            deThi.BoChuongPhan = dataReader.GetBoolean(9);
            return deThi;
        }
        public TblDeThi SelectOne(int ma_de_thi)
        {
            TblDeThi deThi = new TblDeThi();
            using(IDataReader dataReader = _deThiRepository.SelectOne(ma_de_thi))
            {
                if (dataReader.Read())
                {
                    deThi = getProperty(dataReader);
                }
            }
            return deThi;
        }
        public TblDeThi SelectBy_ma_de_hv(long ma_de_hv)
        {
            TblDeThi deThi = new TblDeThi();
            using (IDataReader dataReader = _deThiRepository.SelectBy_ma_de_hv(ma_de_hv))
            {
                if (dataReader.Read())
                {
                    deThi = getProperty(dataReader);
                }
            }
            return deThi;
        }
    }
}
