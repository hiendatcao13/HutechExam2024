using AutoMapper;
using Hutech.Exam.Server.DAL.Repositories;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;
using System.Data;

namespace Hutech.Exam.Server.BUS
{
    public class NhomCauHoiHoanViService(INhomCauHoiHoanViRepository nhomCauHoiHoanViRepository, IMapper mapper)
    {
        private readonly INhomCauHoiHoanViRepository _nhomCauHoiHoanViRepository = nhomCauHoiHoanViRepository;
        private readonly IMapper _mapper = mapper;

        public static readonly int COLUMN_LENGTH = 3; // số lượng cột trong bảng NhomCauHoiHoanVi
        public NhomCauHoiHoanViDto GetProperty(IDataReader dataReader, int start = 0)
        {
            NhomCauHoiHoanVi nhomCauHoiHoanVi = new()
            {
                MaDeHv = dataReader.GetInt64(0 + start),
                MaNhom = dataReader.GetInt32(1 + start),
                ThuTu = dataReader.GetInt32(2 + start)
            };
            return _mapper.Map<NhomCauHoiHoanViDto>(nhomCauHoiHoanVi);
        }
        //public TblNhomCauHoiHoanVi SelectOne(long ma_de_hoan_vi, int ma_nhom)
        //{
        //    TblNhomCauHoiHoanVi nhomCauHoiHoanVi = new TblNhomCauHoiHoanVi();
        //    TblDeThiHoanVi deThiHoanVi = _deThiHoanViService.SelectOne(ma_de_hoan_vi);
        //    using(IDataReader dataReader = _nhomCauHoiHoanViRepository.SelectOne(ma_de_hoan_vi, ma_nhom))
        //    {
        //        if (dataReader.Read())
        //        {
        //            nhomCauHoiHoanVi = getProperty(dataReader, deThiHoanVi);
        //        }
        //    }
        //    return nhomCauHoiHoanVi;
        //}
        //public List<TblNhomCauHoiHoanVi> SelectBy_MaDeHV(long ma_de_hoan_vi)
        //{
        //    List<TblNhomCauHoiHoanVi> result = new List<TblNhomCauHoiHoanVi>();
        //    TblDeThiHoanVi deThiHoanVi = _deThiHoanViService.SelectOne(ma_de_hoan_vi);
        //    using (IDataReader dataReader = _nhomCauHoiHoanViRepository.SelectBy_MaDeHV(ma_de_hoan_vi))
        //    {
        //        while (dataReader.Read())
        //        {
        //            TblNhomCauHoiHoanVi nhomCauHoiHoanVi = getProperty(dataReader, deThiHoanVi);
        //            result.Add(nhomCauHoiHoanVi);
        //        }
        //    }
        //    return result;
        //}
    }
}
