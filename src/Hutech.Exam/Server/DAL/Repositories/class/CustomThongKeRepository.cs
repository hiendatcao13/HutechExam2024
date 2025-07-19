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
                PhanTramDung = dataReader.GetDouble(5),
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

        public async Task<List<CustomThongKeDiem>> ThongKeDiem_SelectBy_DeThi(int MaDeThi)
        {
            using DatabaseReader sql = new("Custom_ThongKeDiem_SelectBy_DeThi");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, MaDeThi);

            using var dataReader = await sql.ExecuteReaderAsync();
            List<CustomThongKeDiem> result = [];

            while (dataReader != null && dataReader.Read())
            {
                result.Add(new CustomThongKeDiem { Diem = dataReader.GetDouble(0), SoLuong = dataReader.GetInt32(1)});
            }

            return result;
        }

        public async Task<CustomThongKeCapBacSV> ThongKeCapBacSV_SelectBy_DeThi(int MaDeThi)
        {
            using DatabaseReader sql = new("CustomThongKeCapBacSV_SelectBy_DeThi");

            sql.SqlParams("@MaDeThi", SqlDbType.Int, MaDeThi);

            using var dataReader = await sql.ExecuteReaderAsync();
            CustomThongKeCapBacSV result = new();

            if (dataReader != null && dataReader.Read())
            {
                CustomThongKeCapBacSV customCapBacSV = new()
                {
                    GuidMaDe = dataReader.IsDBNull(0) ? null : Guid.Parse(dataReader.GetString(0)),
                    TenDeThi = dataReader.GetString(1),
                    NgayThi = dataReader.GetDateTime(2),
                    TongSVThamGia = dataReader.GetInt32(3)
                };

                result = customCapBacSV;
            }


            if (dataReader != null && dataReader.NextResult())
            {
                while (dataReader.Read())
                {
                    ThongKeCapBacCauHoi thongKeCapBacCauHoi = new()
                    {
                        MaCauHoi = dataReader.GetInt32(0),
                        GuidCauHoi = dataReader.IsDBNull(1) ? null : dataReader.GetGuid(1),
                        TongSVTopDung = dataReader.GetInt32(2),
                        TongSVTop = dataReader.GetInt32(3),
                        TongSVBottomDung = dataReader.GetInt32(4),
                        TongSVBottom = dataReader.GetInt32(5),
                        TongSLDung = dataReader.GetInt32(6),
                        TongSLThamGia = dataReader.GetInt32(7)
                    };

                    result.ThongKeCapBacCauHois.Add(thongKeCapBacCauHoi);
                }    
            }

            return result;
        }

        public async Task<List<CustomThongKeDoPhanManh>> ThongKeDoPhanManh()
        {
            using DatabaseReader sql = new("Custom_ThongKeDoPhanManh");

            using var dataReader = await sql.ExecuteReaderAsync();
            List<CustomThongKeDoPhanManh> result = [];

            while(dataReader != null && dataReader.Read())
            {
                CustomThongKeDoPhanManh doPhanManh = new(){
                    SchemeName = dataReader.GetString(0),
                    TableName = dataReader.GetString(1),
                    IndexName = dataReader.GetString(2),
                    DoPhanManh = dataReader.GetDouble(3),
                    SoLuongTrang = dataReader.GetInt64(4)
                };

                result.Add(doPhanManh);
            }  
            
            return result;
        }

        public async Task<bool> RebuildOrReorganizeChiMuc()
        {
            using DatabaseReader sql = new("Custom_RebuildOrReorganizeIndexes");

            return await sql.ExecuteNonQueryAsync() > 0;
        }
    }
}
