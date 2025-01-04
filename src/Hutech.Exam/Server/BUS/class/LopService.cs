using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class LopService
    {
        private readonly ILopRepository _lopRepository;
        public LopService(ILopRepository lopRepository)
        {
            _lopRepository = lopRepository;
        }

        private Lop getProperty(IDataReader dataReader)
        {
            Lop lop = new Lop();
            lop.MaLop = dataReader.GetInt32(0);
            lop.TenLop = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            lop.NgayBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2);
            lop.MaKhoa = dataReader.IsDBNull(3) ? null : dataReader.GetInt32(3);
            return lop;
        }
        public Lop SelectBy_ten_lop(string ten_lop)
        {
            Lop lop = new Lop();
            using(IDataReader dataReader = _lopRepository.SelectBy_ten_lop(ten_lop))
            {
                if (dataReader.Read())
                {
                    lop = getProperty(dataReader);
                }
            }
            return lop;
        }
        public int Insert(string? ten_lop, DateTime? ngay_bat_dau, int? ma_khoa)
        {
            Object ma_lop = _lopRepository.Insert(ten_lop, ngay_bat_dau, ma_khoa);
            try
            {
                return Convert.ToInt32(ma_lop);
            }
            catch (Exception ex) { throw new Exception("Can not insert Lop " + ex.Message); }
        }
    }
}
