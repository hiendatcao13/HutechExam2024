using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class KhoaService
    {
        private readonly IKhoaRepository _khoaRepository;

        public KhoaService(IKhoaRepository khoaRepository)
        {
            _khoaRepository = khoaRepository;
        }
        private Khoa getProperty(IDataReader dataReader)
        {
            Khoa khoa = new Khoa();
            khoa.MaKhoa = dataReader.GetInt32(0);
            khoa.TenKhoa = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            khoa.NgayThanhLap = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2);
            return khoa;
        }
        public List<Khoa> GetAll()
        {
            List<Khoa> results = new List<Khoa>();
            using (IDataReader dataReader = _khoaRepository.GetAll())
            {
                while (dataReader.Read())                {
                    results.Add(getProperty(dataReader));
                }
            }
            return results;
        }
    }
}
