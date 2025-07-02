SET IDENTITY_INSERT [dbo].[Audio] ON 

INSERT [dbo].[Audio] ([MaNghe], [MaChiTietCaThi], [MaNhom], [TenFile], [SoLanNghe]) VALUES (10020, 3, 14, NULL, 2)
INSERT [dbo].[Audio] ([MaNghe], [MaChiTietCaThi], [MaNhom], [TenFile], [SoLanNghe]) VALUES (10027, 1, 14, NULL, 3)
SET IDENTITY_INSERT [dbo].[Audio] OFF
GO
SET IDENTITY_INSERT [dbo].[CaThi] ON 

INSERT [dbo].[CaThi] ([MaCaThi], [TenCaThi], [MaChiTietDotThi], [ThoiGianBatDau], [MaDeThi], [KichHoat], [ThoiGianKichHoat], [ThoiGianThi], [KetThuc], [ThoiDiemKetThuc], [MatMa], [DaDuyet], [NgayDuyet], [LyDoDuyet]) VALUES (1, N'Thứ 5, 19/12/2024 - 10:10:00 PM ', 1, CAST(N'2025-06-26T09:15:00.000' AS DateTime), 2, 1, CAST(N'2025-06-28T21:36:14.503' AS DateTime), 90, 0, NULL, N'$2a$10$F6xGpY/uiqxnkwqsiI2Hxet39M9ywBtDt252JbsPmPVvDxdh0iY6C', 1, CAST(N'2024-12-10' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[CaThi] OFF
GO

SET IDENTITY_INSERT [dbo].[ChiTietCaThi] ON 

INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (1, 1, 1, 7, CAST(N'2025-06-26T09:19:18.970' AS DateTime), NULL, 1, 1, 0, 41, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (2, 1, 2, 6, NULL, NULL, 0, 1, 8, 40, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (3, 1, 3, 5, CAST(N'2025-06-25T17:11:21.023' AS DateTime), NULL, 1, 1, 10, 40, 4, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (18, 1, 4, 8, NULL, NULL, 0, 1, 7, 0, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (27, 1, 6, 8, NULL, NULL, 0, 1, 6.5, -1, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (28, 1, 11, 7, NULL, NULL, 0, 0, -1, 0, 0, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ChiTietCaThi] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietDotThi] ON 

INSERT [dbo].[ChiTietDotThi] ([MaChiTietDotThi], [TenChiTietDotThi], [MaLopAo], [MaDotThi], [LanThi]) VALUES (1, N'', 1, 1, 1)
INSERT [dbo].[ChiTietDotThi] ([MaChiTietDotThi], [TenChiTietDotThi], [MaLopAo], [MaDotThi], [LanThi]) VALUES (2, N'Hello', 5, 3, 2)
SET IDENTITY_INSERT [dbo].[ChiTietDotThi] OFF
GO

INSERT [dbo].[DeThi] ([MaDeThi], [MaMonHoc], [TenDeThi], [Guid], [NgayTao], [NguoiTao], [GhiChu], [LuuTam], [DaDuyet], [TongSoDeHoanVi], [BoChuongPhan]) VALUES (1, 1, N'ĐỀ THI-TIẾNG_ANH-2024_04_12', NULL, CAST(N'2024-11-04T00:00:00.000' AS DateTime), -1, NULL, 0, 1, 4, 1)
INSERT [dbo].[DeThi] ([MaDeThi], [MaMonHoc], [TenDeThi], [Guid], [NgayTao], [NguoiTao], [GhiChu], [LuuTam], [DaDuyet], [TongSoDeHoanVi], [BoChuongPhan]) VALUES (2, 1, N'ĐỀ THI-TỔNG-HỢP-MÔN-2024_06_19', NULL, CAST(N'2024-06-19T00:00:00.000' AS DateTime), -1, NULL, 1, 0, 4, 0)
SET IDENTITY_INSERT [dbo].[DeThi] OFF
GO
SET IDENTITY_INSERT [dbo].[DeThiHoanVi] ON 

INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (1, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (2, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (3, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (4, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (5, 2, N'TTNN001', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (6, 2, N'TTNN002', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000001')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (7, 2, N'TTNN003', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000002')
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (8, 2, N'TTNN004', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000003')
SET IDENTITY_INSERT [dbo].[DeThiHoanVi] OFF
GO
SET IDENTITY_INSERT [dbo].[DotThi] ON 

INSERT [dbo].[DotThi] ([MaDotThi], [TenDotThi], [ThoiGianBatDau], [ThoiGianKetThuc], [NamHoc]) VALUES (1, N'THI ĐỒ ÁN CƠ SỞ', CAST(N'2024-04-11T00:00:00.000' AS DateTime), CAST(N'2024-04-13T00:00:00.000' AS DateTime), 2024)
INSERT [dbo].[DotThi] ([MaDotThi], [TenDotThi], [ThoiGianBatDau], [ThoiGianKetThuc], [NamHoc]) VALUES (3, N'Hello', CAST(N'2025-06-03T00:00:00.000' AS DateTime), CAST(N'2026-03-02T00:00:00.000' AS DateTime), 2025)
SET IDENTITY_INSERT [dbo].[DotThi] OFF
GO
SET IDENTITY_INSERT [dbo].[Khoa] ON 

INSERT [dbo].[Khoa] ([MaKhoa], [TenKhoa], [NgayThanhLap]) VALUES (1, N'CNTT', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Khoa] OFF
GO
SET IDENTITY_INSERT [dbo].[Lop] ON 

INSERT [dbo].[Lop] ([MaLop], [TenLop], [NgayBatDau], [MaKhoa]) VALUES (1, N'21DTHD2', CAST(N'2024-01-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Lop] ([MaLop], [TenLop], [NgayBatDau], [MaKhoa]) VALUES (2, N'21DTHD3', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Lop] OFF
GO
SET IDENTITY_INSERT [dbo].[LopAo] ON 

INSERT [dbo].[LopAo] ([MaLopAo], [TenLopAo], [NgayBatDau], [MaMonHoc]) VALUES (1, N'E1-04.04', CAST(N'2024-04-11T22:12:49.787' AS DateTime), 1)
INSERT [dbo].[LopAo] ([MaLopAo], [TenLopAo], [NgayBatDau], [MaMonHoc]) VALUES (2, N'E2-05.06', CAST(N'2024-04-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[LopAo] ([MaLopAo], [TenLopAo], [NgayBatDau], [MaMonHoc]) VALUES (4, N'E1.08-08 2', CAST(N'2025-04-10T00:00:00.000' AS DateTime), 4)
INSERT [dbo].[LopAo] ([MaLopAo], [TenLopAo], [NgayBatDau], [MaMonHoc]) VALUES (5, N'E12.09-09', CAST(N'2025-06-03T00:00:00.000' AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[LopAo] OFF
GO
SET IDENTITY_INSERT [dbo].[MonHoc] ON 

INSERT [dbo].[MonHoc] ([MaMonHoc], [MaSoMonHoc], [TenMonHoc]) VALUES (1, N'ENC106', N'Tổng Hợp Ngôn Ngữ')
INSERT [dbo].[MonHoc] ([MaMonHoc], [MaSoMonHoc], [TenMonHoc]) VALUES (2, N'SUM165', N'Tổng Hợp')
INSERT [dbo].[MonHoc] ([MaMonHoc], [MaSoMonHoc], [TenMonHoc]) VALUES (3, N'SUKA203', N'Ielts')
INSERT [dbo].[MonHoc] ([MaMonHoc], [MaSoMonHoc], [TenMonHoc]) VALUES (4, N'PINODAT', N'chiêm tinh 2')
SET IDENTITY_INSERT [dbo].[MonHoc] OFF
GO

SET IDENTITY_INSERT [dbo].[VaiTro] ON 

INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (2, N'KhaoThi', N'Phòng Khảo Thí')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (3, N'DaoTao', N'Phòng Đào Tạo')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (4, N'CNTT', N'Trung tâm CNTT')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (5, N'GiamThi', N'Giám thị')
SET IDENTITY_INSERT [dbo].[VaiTro] OFF
GO
SET IDENTITY_INSERT [dbo].[SinhVien] ON 

INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (1, N'Cao Hiển ', N'Đạt', 1, CAST(N'2003-03-18T00:00:00.000' AS DateTime), NULL, N'18/6B B?c Lân, Bà Ði?m, Hóc Môn, TP.HCM', N'hiendatcao13@gmail.com', N'0342429410', N'2180608276', N'd07c9341-52de-4e49-a3fc-b2fd3c83fe39', 0, CAST(N'2025-06-27T09:31:27.750' AS DateTime), CAST(N'2025-06-27T09:31:33.450' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (2, N'Đỗ Thùy', N'Dung', 0, CAST(N'2003-11-11T00:00:00.000' AS DateTime), 1, NULL, N'dothuydung14@gmail.com', NULL, N'2180607359', N'21c952e2-bb45-4cbc-be49-3d4089e1ec1d', 0, CAST(N'2025-05-14T18:05:39.950' AS DateTime), CAST(N'2025-06-25T09:17:29.223' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (3, N'Đặng Duy', N'Linh', 1, CAST(N'2003-07-15T00:00:00.000' AS DateTime), 1, NULL, N'dangduylinh15@gmail.com', NULL, N'2180608877', N'29cff326-4ba2-426c-847b-d2270d4e5720', 1, CAST(N'2025-06-25T17:10:42.953' AS DateTime), CAST(N'2025-03-26T22:39:11.950' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (4, N'Vương Khả', N'Thạch', 1, CAST(N'2003-09-08T00:00:00.000' AS DateTime), 1, NULL, N'vuongkhathach16@gmail.com', NULL, N'2180608012', N'47c11564-5180-4b24-8577-f610cf0ba4bb', 0, CAST(N'2024-10-30T09:52:22.377' AS DateTime), CAST(N'2024-10-30T09:52:36.297' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (5, N'Hello', N'Dat', 1, NULL, NULL, NULL, NULL, NULL, N'1234567', N'65011e14-e53c-47d6-9453-862eb8f49966', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (6, N'Pino', N'Đat', 1, NULL, NULL, NULL, NULL, NULL, N'123456', N'27891f56-d285-4759-822c-a52e84bb4035', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (7, N'Unknown', N'Me', 1, CAST(N'2003-03-19T00:00:00.000' AS DateTime), 1, N'18/6B', N'datcao@gmail.com', N'0342429410', N'2180601111', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (11, N'Trần Văn', N'Mười', 1, NULL, 1, NULL, NULL, NULL, N'2180607777', N'0fefcaac-3745-4f47-9b5a-ed20385acfee', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (12, N'Nguyễn Văn', N'Cẩm', 0, NULL, 1, NULL, NULL, NULL, N'2180601234', N'af4f1b86-b218-4a00-85db-a0dc99fa3989', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (13, N'Nguyễn Thị', N'Hòa', 0, NULL, 1, NULL, NULL, NULL, N'2180609999', N'8f7bd75e-7eee-4f18-a5d7-4145cd7d8da3', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SinhVien] OFF
GO
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianDangNhap], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'47b854c1-1a8d-487a-881b-13c4442de60c', N'khaothi', N'khaothi@examsuite.vn', N'Phòng Khảo Thí', N'$2a$12$DRDHpP8efDp4alPZhISj7.5W9FqxgSjNOx1Ywjt/vICAKQa6l3RIm', 2, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 0, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianDangNhap], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'f4a86e92-737d-4214-b6b2-520a76e71fc2', N'daotao', N'daotao@examsuite.vn', N'Phòng Đào tạo', N'$2a$12$my1zS/WaMFBVhGZP.OJeeeJf0foghg3TpmLB4uWld9hD2i4vbx50O', 3, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 1, 0, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianDangNhap], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'21bf922b-cc11-448c-a9c6-c98acab7c085', N'admin', N'admin@examsuite.vn', N'Administrator', N'$2a$12$G2VuZtMA/rQ72BbCq5wQYOgub7w2MYWafokCwF7E5t15J2q6D9WvG', 1, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 0, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianDangNhap], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'3ac347e7-0d07-451d-bbf4-d56370a8804a', N'ttcntt', N'ttcntt@examsuite.vn', N'TT CNTT', N'$2a$12$6S/V54uiykBISR/rt8k0eOUbkHb6/wz60/.esof6tGDA5YsV1knZG', 4, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 1, NULL, NULL, NULL, NULL, NULL, 1)
GO