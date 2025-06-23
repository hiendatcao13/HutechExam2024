using Hutech.Exam.Server.DAL.DataReader;
using Hutech.Exam.Shared.DTO.Custom;
using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public class CustomThongKeRepository : ICustomThongKeRepository
    {
        public CustomThongKeCauHoi GetPropertyCauHoi(IDataReader dataReader)
        {
            CustomThongKeCauHoi customThongKe = new()
            {
                MaCauHoi = dataReader.GetInt32(0),
                MaNhom = dataReader.GetInt32(1),
                MaSoCLO = dataReader.GetString(2),
                TongSLDung = dataReader.GetInt32(3),
                TongSLTraLoi = dataReader.GetInt32(4),
                PhanTram = dataReader.GetDouble(5),
            };

            return customThongKe;
        }

        public async Task<List<CustomThongKeCauHoi>> ThongKeCauHoi_SelectBy_DeThi(int MaDeThi)
        {
            using DatabaseReader sql = new("Custom_ThongKeCauHoi_SelectBy_DeThi");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, MaDeThi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<CustomThongKeCauHoi> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result.Add(GetPropertyCauHoi(dataReader));
            }

            return result;
        }

        public async Task<List<(double Diem, int SoLuongSV)>> ThongKeDiem_SelectBy_DeThi(int MaDeThi)
        {
            using DatabaseReader sql = new("Custom_ThongKeDiem_SelectBy_DeThi");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, MaDeThi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<(double Diem, int SoLuongSV)> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result.Add((dataReader.GetDouble(0), dataReader.GetInt32(1)));
            }

            return result;
        }
    }
}
