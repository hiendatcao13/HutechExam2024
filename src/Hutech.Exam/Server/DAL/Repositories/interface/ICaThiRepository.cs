﻿using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface ICaThiRepository
    {
        public Task<IDataReader> SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi);

        public Task<IDataReader> SelectOne(int ma_ca_thi);

        public Task<IDataReader> GetAll();

        public Task<int> Activate(int ma_ca_thi, bool IsActivated);

        public Task<int> HuyKichHoat(int ma_ca_thi);

        public Task<int> Ketthuc(int ma_ca_thi);

        public Task<object?> Insert(string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi);

        public Task<int> Remove(int ma_ca_thi);

        public Task<int> Update(int ma_ca_thi, string ten_ca_thi, int ma_chi_tiet_dot_thi, DateTime thoi_gian_bat_dau, int ma_de_thi, int thoi_gian_thi);

        public Task<IDataReader> SelectBy_MaDotThi_MaLop_LanThi(int ma_dot_thi, int ma_lop, int lan_thi);
    }
}
