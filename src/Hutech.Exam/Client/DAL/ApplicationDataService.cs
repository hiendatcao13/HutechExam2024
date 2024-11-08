﻿using Hutech.Exam.Shared.Models;
using System.Numerics;

namespace Hutech.Exam.Client.DAL
{
    public class ApplicationDataService
    {
        //ma_sinh_vien có kiểu dữ liệu là BigInt, identity(1,1)
        public SinhVien? sinhVien { get; set; }
        //sv thi ca nào
        public ChiTietCaThi? chiTietCaThi{ get; set; } 
        public List<ChiTietBaiThi>? chiTietBaiThis { get; set;}
        public List<CustomDeThi>? customDeThis { get; set; }
        public List<int>? listDapAnKhoanh { get; set; }
        public int bonusTime { get; set; }

    }
}
