using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.DAL
{
    public class ApplicationDataService
    {
        public HubConnection? HubConnection { get; private set; }
        //ma_sinh_vien có kiểu dữ liệu là BigInt, identity(1,1)
        public SinhVienDto SinhVien { get; set; } = new();
        //sv thi ca nào
        public ChiTietCaThiDto ChiTietCaThi { get; set; } = new();
        public List<CustomChiTietBaiThi> ChiTietBaiThis { get; set; } = [];
        public List<CustomDeThi> CustomDeThis { get; set; } = [];
        public List<int> ListDapAnKhoanh { get; set; } = [];
        public int BonusTime { get; set; }
    }
}
