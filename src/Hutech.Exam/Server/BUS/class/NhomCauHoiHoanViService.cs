﻿using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class NhomCauHoiHoanViService
    {
        private readonly INhomCauHoiHoanViRepository _nhomCauHoiHoanViRepository;
        private readonly DeThiHoanViService _deThiHoanViService;
        public NhomCauHoiHoanViService(INhomCauHoiHoanViRepository nhomCauHoiHoanViRepository, DeThiHoanViService deThiHoanViService)
        {
            _nhomCauHoiHoanViRepository = nhomCauHoiHoanViRepository;
            _deThiHoanViService = deThiHoanViService;
        }
        private TblNhomCauHoiHoanVi getProperty(IDataReader dataReader, TblDeThiHoanVi deThiHoanVi)
        {
            TblNhomCauHoiHoanVi nhomCauHoiHoanVi = new TblNhomCauHoiHoanVi();
            nhomCauHoiHoanVi.MaDeHv = dataReader.GetInt64(0);
            nhomCauHoiHoanVi.MaNhom = dataReader.GetInt32(1);
            nhomCauHoiHoanVi.ThuTu = dataReader.GetInt32(2);
            // có trường đặc biệt MaDeNavigation - là đối tượng Mã đề hoán vị
            nhomCauHoiHoanVi.MaDeHvNavigation = deThiHoanVi;
            return nhomCauHoiHoanVi;
        }
        public TblNhomCauHoiHoanVi SelectOne(long ma_de_hoan_vi, int ma_nhom)
        {
            TblNhomCauHoiHoanVi nhomCauHoiHoanVi = new TblNhomCauHoiHoanVi();
            TblDeThiHoanVi deThiHoanVi = _deThiHoanViService.SelectOne(ma_de_hoan_vi);
            using(IDataReader dataReader = _nhomCauHoiHoanViRepository.SelectOne(ma_de_hoan_vi, ma_nhom))
            {
                if (dataReader.Read())
                {
                    nhomCauHoiHoanVi = getProperty(dataReader, deThiHoanVi);
                }
            }
            return nhomCauHoiHoanVi;
        }
        public List<TblNhomCauHoiHoanVi> SelectBy_MaDeHV(long ma_de_hoan_vi)
        {
            List<TblNhomCauHoiHoanVi> result = new List<TblNhomCauHoiHoanVi>();
            TblDeThiHoanVi deThiHoanVi = _deThiHoanViService.SelectOne(ma_de_hoan_vi);
            using (IDataReader dataReader = _nhomCauHoiHoanViRepository.SelectBy_MaDeHV(ma_de_hoan_vi))
            {
                while (dataReader.Read())
                {
                    TblNhomCauHoiHoanVi nhomCauHoiHoanVi = getProperty(dataReader, deThiHoanVi);
                    result.Add(nhomCauHoiHoanVi);
                }
            }
            return result;
        }
    }
}
