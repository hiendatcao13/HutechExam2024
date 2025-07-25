USE [master]
GO
/****** Object:  Database [HutechExam2025]    Script Date: 7/3/2025 6:11:16 PM ******/
CREATE DATABASE [HutechExam2025]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HutechExam2025', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\HutechExam2025.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HutechExam2025_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\HutechExam2025_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HutechExam2025] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HutechExam2025].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HutechExam2025] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HutechExam2025] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HutechExam2025] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HutechExam2025] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HutechExam2025] SET ARITHABORT OFF 
GO
ALTER DATABASE [HutechExam2025] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HutechExam2025] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HutechExam2025] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HutechExam2025] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HutechExam2025] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HutechExam2025] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HutechExam2025] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HutechExam2025] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HutechExam2025] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HutechExam2025] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HutechExam2025] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HutechExam2025] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HutechExam2025] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HutechExam2025] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HutechExam2025] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HutechExam2025] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HutechExam2025] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HutechExam2025] SET RECOVERY FULL 
GO
ALTER DATABASE [HutechExam2025] SET  MULTI_USER 
GO
ALTER DATABASE [HutechExam2025] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HutechExam2025] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HutechExam2025] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HutechExam2025] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HutechExam2025] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HutechExam2025] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HutechExam2025', N'ON'
GO
ALTER DATABASE [HutechExam2025] SET QUERY_STORE = ON
GO
ALTER DATABASE [HutechExam2025] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HutechExam2025]
GO
/****** Object:  UserDefinedTableType [dbo].[ChiTietBaiThiType]    Script Date: 7/3/2025 6:11:17 PM ******/
CREATE TYPE [dbo].[ChiTietBaiThiType] AS TABLE(
	[MaChiTietCaThi] [int] NULL,
	[MaDeHV] [bigint] NULL,
	[MaNhom] [int] NULL,
	[MaCauHoi] [int] NULL,
	[MaCLO] [int] NULL,
	[CauTraLoi] [int] NULL,
	[NgayTao] [datetime] NULL,
	[NgayCapNhat] [datetime] NULL,
	[KetQua] [bit] NULL,
	[ThuTu] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SinhVienCaThiType]    Script Date: 7/3/2025 6:11:17 PM ******/
CREATE TYPE [dbo].[SinhVienCaThiType] AS TABLE(
	[MaSoSinhVien] [nvarchar](50) NULL,
	[MaCaThi] [int] NULL,
	[MaDeThi] [bigint] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SinhVienType]    Script Date: 7/3/2025 6:11:17 PM ******/
CREATE TYPE [dbo].[SinhVienType] AS TABLE(
	[HoVaTenLot] [nvarchar](300) NULL,
	[TenSinhVien] [nvarchar](50) NULL,
	[GioiTinh] [smallint] NULL,
	[NgaySinh] [datetime] NULL,
	[MaLop] [int] NULL,
	[DiaChi] [nvarchar](max) NULL,
	[Email] [nvarchar](200) NULL,
	[DienThoai] [nvarchar](50) NULL,
	[MaSoSinhVien] [nvarchar](50) NULL,
	[Guid] [uniqueidentifier] NULL
)
GO
/****** Object:  Table [dbo].[Audio]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audio](
	[MaNghe] [bigint] IDENTITY(1,1) NOT NULL,
	[MaChiTietCaThi] [int] NOT NULL,
	[MaNhom] [int] NULL,
	[TenFile] [nvarchar](max) NULL,
	[SoLanNghe] [int] NOT NULL,
 CONSTRAINT [PK_Audio] PRIMARY KEY CLUSTERED 
(
	[MaNghe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaThi](
	[MaCaThi] [int] IDENTITY(1,1) NOT NULL,
	[TenCaThi] [nvarchar](50) NULL,
	[MaChiTietDotThi] [int] NOT NULL,
	[ThoiGianBatDau] [datetime] NOT NULL,
	[DaGanDe] [bit] NOT NULL,
	[KichHoat] [bit] NOT NULL,
	[ThoiGianKichHoat] [datetime] NULL,
	[ThoiGianThi] [int] NOT NULL,
	[KetThuc] [bit] NOT NULL,
	[ThoiDiemKetThuc] [datetime] NULL,
	[MatMa] [nvarchar](128) NULL,
	[DaDuyet] [bit] NOT NULL,
	[LichSuHoatDong] [nvarchar](max) NULL,
 CONSTRAINT [PK_CaThi] PRIMARY KEY CLUSTERED 
(
	[MaCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietBaiThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietBaiThi](
	[MaChiTietBaiThi] [bigint] IDENTITY(1,1) NOT NULL,
	[MaChiTietCaThi] [int] NOT NULL,
	[MaDeHV] [bigint] NOT NULL,
	[MaNhom] [int] NOT NULL,
	[MaCLO] [int] NOT NULL,
	[MaCauHoi] [int] NOT NULL,
	[CauTraLoi] [int] NULL,
	[NgayTao] [datetime] NOT NULL,
	[NgayCapNhat] [datetime] NULL,
	[KetQua] [bit] NULL,
	[ThuTu] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietBaiThi] PRIMARY KEY CLUSTERED 
(
	[MaChiTietBaiThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietCaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietCaThi](
	[MaChiTietCaThi] [int] IDENTITY(1,1) NOT NULL,
	[MaCaThi] [int] NULL,
	[MaSinhVien] [bigint] NULL,
	[MaDeThi] [bigint] NULL,
	[ThoiGianBatDau] [datetime] NULL,
	[ThoiGianKetThuc] [datetime] NULL,
	[DaThi] [bit] NOT NULL,
	[DaHoanThanh] [bit] NOT NULL,
	[Diem] [float] NOT NULL,
	[TongSoCau] [int] NULL,
	[SoCauDung] [int] NULL,
	[GioCongThem] [int] NOT NULL,
	[ThoiDiemCong] [datetime] NULL,
	[LyDoCong] [nvarchar](250) NULL,
 CONSTRAINT [PK_ChiTietCaThi] PRIMARY KEY CLUSTERED 
(
	[MaChiTietCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDotThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDotThi](
	[MaChiTietDotThi] [int] IDENTITY(1,1) NOT NULL,
	[TenChiTietDotThi] [nvarchar](200) NOT NULL,
	[MaLopAo] [int] NOT NULL,
	[MaDotThi] [int] NOT NULL,
	[LanThi] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietDotThi] PRIMARY KEY CLUSTERED 
(
	[MaChiTietDotThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeThi](
	[MaDeThi] [int] IDENTITY(1,1) NOT NULL,
	[MaMonHoc] [int] NOT NULL,
	[TenDeThi] [nvarchar](50) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[KyHieuDe] [varchar](50) NOT NULL,
	[NgayTao] [datetime] NOT NULL,
 CONSTRAINT [PK_DeThi] PRIMARY KEY CLUSTERED 
(
	[MaDeThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DotThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DotThi](
	[MaDotThi] [int] IDENTITY(1,1) NOT NULL,
	[TenDotThi] [nvarchar](150) NULL,
	[ThoiGianBatDau] [datetime] NULL,
	[ThoiGianKetThuc] [datetime] NULL,
	[NamHoc] [int] NULL,
 CONSTRAINT [PK_DotThi] PRIMARY KEY CLUSTERED 
(
	[MaDotThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[MaKhoa] [int] IDENTITY(1,1) NOT NULL,
	[TenKhoa] [nvarchar](30) NULL,
	[NgayThanhLap] [datetime] NULL,
 CONSTRAINT [PK_Khoa] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[MaLop] [int] IDENTITY(1,1) NOT NULL,
	[TenLop] [nvarchar](50) NULL,
	[NgayBatDau] [datetime] NULL,
	[MaKhoa] [int] NULL,
 CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopAo]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopAo](
	[MaLopAo] [int] IDENTITY(1,1) NOT NULL,
	[TenLopAo] [nvarchar](200) NULL,
	[NgayBatDau] [datetime] NULL,
	[MaMonHoc] [int] NULL,
 CONSTRAINT [PK_LopAo] PRIMARY KEY CLUSTERED 
(
	[MaLopAo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonHoc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[MaMonHoc] [int] IDENTITY(1,1) NOT NULL,
	[MaSoMonHoc] [nvarchar](50) NULL,
	[TenMonHoc] [nvarchar](200) NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MaMonHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[MaNguoiDung] [uniqueidentifier] NOT NULL,
	[TenDangNhap] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Ten] [nvarchar](255) NOT NULL,
	[MatKhau] [nvarchar](128) NOT NULL,
	[MaVaiTro] [int] NOT NULL,
	[NgayTao] [datetime] NOT NULL,
	[DaXoa] [bit] NOT NULL,
	[DaKhoa] [bit] NOT NULL,
	[ThoiGianHoatDong] [datetime] NULL,
	[ThoiGianDangNhap] [datetime] NULL,
	[ThoiGianDoiMatKhau] [datetime] NULL,
	[ThoiGianKhoa] [datetime] NULL,
	[SoLanDangNhapSai] [int] NULL,
	[ThoiGianDangNhapSai] [datetime] NULL,
	[GhiChu] [nvarchar](max) NULL,
	[LaNguoiDungHeThong] [bit] NOT NULL,
 CONSTRAINT [PK_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSinhVien] [bigint] IDENTITY(1,1) NOT NULL,
	[HoVaTenLot] [nvarchar](300) NULL,
	[TenSinhVien] [nvarchar](50) NULL,
	[GioiTinh] [smallint] NULL,
	[NgaySinh] [datetime] NULL,
	[MaLop] [int] NULL,
	[DiaChi] [nvarchar](max) NULL,
	[Email] [nvarchar](200) NULL,
	[DienThoai] [nvarchar](50) NULL,
	[MaSoSinhVien] [nvarchar](50) NULL,
	[Guid] [uniqueidentifier] NULL,
	[DaDangNhap] [bit] NULL,
	[ThoiGianDangNhap] [datetime] NULL,
	[ThoiGianDangXuat] [datetime] NULL,
	[HinhAnh] [image] NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaiTro]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaiTro](
	[MaVaiTro] [int] IDENTITY(1,1) NOT NULL,
	[TenVaiTro] [nvarchar](250) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_VaiTro] PRIMARY KEY CLUSTERED 
(
	[MaVaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CaThi] ON 

INSERT [dbo].[CaThi] ([MaCaThi], [TenCaThi], [MaChiTietDotThi], [ThoiGianBatDau], [DaGanDe], [KichHoat], [ThoiGianKichHoat], [ThoiGianThi], [KetThuc], [ThoiDiemKetThuc], [MatMa], [DaDuyet], [LichSuHoatDong]) VALUES (1, N'Thứ 5, 19/12/2024 - 10:10:00 PM ', 1, CAST(N'2025-07-03T16:40:00.000' AS DateTime), 0, 1, CAST(N'2025-07-03T16:27:03.533' AS DateTime), 90, 0, NULL, N'$2a$10$ahohXLWhgl8k0/ZdxfX3T.z4tIMXLVs5k4S3i4gT2Z28F2eP60Nk2', 1, NULL)
SET IDENTITY_INSERT [dbo].[CaThi] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietCaThi] ON 

INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (1, 1, 1, NULL, NULL, NULL, 0, 0, 0, 41, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (2, 1, 2, NULL, NULL, NULL, 0, 1, 8, 40, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (3, 1, 3, NULL, NULL, NULL, 1, 1, 10, 40, 4, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (18, 1, 4, NULL, NULL, NULL, 0, 1, 7, 0, 0, 0, NULL, NULL)
INSERT [dbo].[ChiTietCaThi] ([MaChiTietCaThi], [MaCaThi], [MaSinhVien], [MaDeThi], [ThoiGianBatDau], [ThoiGianKetThuc], [DaThi], [DaHoanThanh], [Diem], [TongSoCau], [SoCauDung], [GioCongThem], [ThoiDiemCong], [LyDoCong]) VALUES (28, 1, 11, NULL, NULL, NULL, 0, 0, -1, 0, 0, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ChiTietCaThi] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietDotThi] ON 

INSERT [dbo].[ChiTietDotThi] ([MaChiTietDotThi], [TenChiTietDotThi], [MaLopAo], [MaDotThi], [LanThi]) VALUES (1, N'ĐỢT THI TIẾNG ANH LẦN 1', 1, 1, 1)
INSERT [dbo].[ChiTietDotThi] ([MaChiTietDotThi], [TenChiTietDotThi], [MaLopAo], [MaDotThi], [LanThi]) VALUES (2, N'Hello', 5, 3, 2)
SET IDENTITY_INSERT [dbo].[ChiTietDotThi] OFF
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
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianHoatDong], [ThoiGianDangNhap], [ThoiGianDoiMatKhau], [ThoiGianKhoa], [SoLanDangNhapSai], [ThoiGianDangNhapSai], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'47b854c1-1a8d-487a-881b-13c4442de60c', N'khaothi', N'khaothi@examsuite.vn', N'Phòng Khảo Thí', N'$2a$12$DRDHpP8efDp4alPZhISj7.5W9FqxgSjNOx1Ywjt/vICAKQa6l3RIm', 2, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianHoatDong], [ThoiGianDangNhap], [ThoiGianDoiMatKhau], [ThoiGianKhoa], [SoLanDangNhapSai], [ThoiGianDangNhapSai], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'f4a86e92-737d-4214-b6b2-520a76e71fc2', N'daotao', N'daotao@examsuite.vn', N'Phòng Đào tạo', N'$2a$12$my1zS/WaMFBVhGZP.OJeeeJf0foghg3TpmLB4uWld9hD2i4vbx50O', 3, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianHoatDong], [ThoiGianDangNhap], [ThoiGianDoiMatKhau], [ThoiGianKhoa], [SoLanDangNhapSai], [ThoiGianDangNhapSai], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'21bf922b-cc11-448c-a9c6-c98acab7c085', N'admin', N'admin@examsuite.vn', N'Administrator', N'$2a$12$G2VuZtMA/rQ72BbCq5wQYOgub7w2MYWafokCwF7E5t15J2q6D9WvG', 1, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 0, NULL, CAST(N'2025-07-03T16:24:51.933' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TenDangNhap], [Email], [Ten], [MatKhau], [MaVaiTro], [NgayTao], [DaXoa], [DaKhoa], [ThoiGianHoatDong], [ThoiGianDangNhap], [ThoiGianDoiMatKhau], [ThoiGianKhoa], [SoLanDangNhapSai], [ThoiGianDangNhapSai], [GhiChu], [LaNguoiDungHeThong]) VALUES (N'3ac347e7-0d07-451d-bbf4-d56370a8804a', N'ttcntt', N'ttcntt@examsuite.vn', N'TT CNTT', N'$2a$12$6S/V54uiykBISR/rt8k0eOUbkHb6/wz60/.esof6tGDA5YsV1knZG', 4, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[SinhVien] ON 

INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (1, N'Cao Hiển ', N'Đạt', 1, CAST(N'2003-03-18T00:00:00.000' AS DateTime), 1, N'18/6B Hóc Môn', N'hiendatcao13@gmail.com', N'0342429410', N'2180608276', N'd07c9341-52de-4e49-a3fc-b2fd3c83fe39', 0, CAST(N'2025-07-03T16:35:25.190' AS DateTime), CAST(N'2025-07-03T16:35:33.877' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (2, N'Đỗ Thùy', N'Dung', 0, CAST(N'2003-11-11T00:00:00.000' AS DateTime), 1, NULL, N'dothuydung14@gmail.com', NULL, N'2180607359', N'21c952e2-bb45-4cbc-be49-3d4089e1ec1d', 0, CAST(N'2025-05-14T18:05:39.950' AS DateTime), CAST(N'2025-06-25T09:17:29.223' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (3, N'Đặng Duy', N'Linh', 1, CAST(N'2003-07-15T00:00:00.000' AS DateTime), 1, NULL, N'dangduylinh15@gmail.com', NULL, N'2180608877', N'29cff326-4ba2-426c-847b-d2270d4e5720', 1, CAST(N'2025-06-25T17:10:42.953' AS DateTime), CAST(N'2025-03-26T22:39:11.950' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (4, N'Vương Khả', N'Thạch', 1, CAST(N'2003-09-08T00:00:00.000' AS DateTime), 1, NULL, N'vuongkhathach16@gmail.com', NULL, N'2180608012', N'47c11564-5180-4b24-8577-f610cf0ba4bb', 0, CAST(N'2024-10-30T09:52:22.377' AS DateTime), CAST(N'2024-10-30T09:52:36.297' AS DateTime), NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (5, N'Hello', N'Dat', 1, NULL, NULL, NULL, NULL, NULL, N'1234567', N'65011e14-e53c-47d6-9453-862eb8f49966', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (6, N'Pino', N'Đat', 1, NULL, NULL, NULL, NULL, NULL, N'123456', N'27891f56-d285-4759-822c-a52e84bb4035', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (7, N'Unknown', N'Me', 1, CAST(N'2003-03-18T00:00:00.000' AS DateTime), 1, N'18/6B', N'datcao@gmail.com', N'0342429410', N'2180601111', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (11, N'Trần Văn', N'Mười', 1, NULL, 1, NULL, NULL, NULL, N'2180607777', N'0fefcaac-3745-4f47-9b5a-ed20385acfee', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (12, N'Nguyễn Văn', N'Cẩm', 0, NULL, 1, NULL, NULL, NULL, N'2180601234', N'af4f1b86-b218-4a00-85db-a0dc99fa3989', NULL, NULL, NULL, NULL)
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoVaTenLot], [TenSinhVien], [GioiTinh], [NgaySinh], [MaLop], [DiaChi], [Email], [DienThoai], [MaSoSinhVien], [Guid], [DaDangNhap], [ThoiGianDangNhap], [ThoiGianDangXuat], [HinhAnh]) VALUES (13, N'Nguyễn Thị', N'Hòa', 0, NULL, 1, NULL, NULL, NULL, N'2180609999', N'8f7bd75e-7eee-4f18-a5d7-4145cd7d8da3', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SinhVien] OFF
GO
SET IDENTITY_INSERT [dbo].[VaiTro] ON 

INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (2, N'KhaoThi', N'Phòng Khảo Thí')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (3, N'DaoTao', N'Phòng Đào Tạo')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (4, N'CNTT', N'Trung tâm CNTT')
INSERT [dbo].[VaiTro] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (5, N'GiamThi', N'Giám thị')
SET IDENTITY_INSERT [dbo].[VaiTro] OFF
GO
/****** Object:  Index [IX_Audio_MaChiTietCaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_Audio_MaChiTietCaThi] ON [dbo].[Audio]
(
	[MaChiTietCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChiTietBaiThi_MaChiTietCaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChiTietBaiThi_MaChiTietCaThi] ON [dbo].[ChiTietBaiThi]
(
	[MaChiTietCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IDX_ChiTietCaThi_MaCaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
CREATE NONCLUSTERED INDEX [IDX_ChiTietCaThi_MaCaThi] ON [dbo].[ChiTietCaThi]
(
	[MaCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_MSSV]    Script Date: 7/3/2025 6:11:17 PM ******/
ALTER TABLE [dbo].[SinhVien] ADD  CONSTRAINT [UQ_MSSV] UNIQUE NONCLUSTERED 
(
	[MaSoSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audio] ADD  CONSTRAINT [DF_Audio_SoLanNghe]  DEFAULT ((0)) FOR [SoLanNghe]
GO
ALTER TABLE [dbo].[CaThi] ADD  CONSTRAINT [DF_CaThi_KichHoat]  DEFAULT ((0)) FOR [KichHoat]
GO
ALTER TABLE [dbo].[CaThi] ADD  CONSTRAINT [DF_CaThi_ThoiGianThi]  DEFAULT ((0)) FOR [ThoiGianThi]
GO
ALTER TABLE [dbo].[CaThi] ADD  CONSTRAINT [DF_CaThi_KetThuc]  DEFAULT ((0)) FOR [KetThuc]
GO
ALTER TABLE [dbo].[CaThi] ADD  CONSTRAINT [DF_CaThi_DaDuyet]  DEFAULT ((0)) FOR [DaDuyet]
GO
ALTER TABLE [dbo].[ChiTietBaiThi] ADD  CONSTRAINT [DF_ChiTietBaiThi_NgayTao]  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[ChiTietBaiThi] ADD  CONSTRAINT [DF_ChiTietBaiThi_ThuTu]  DEFAULT ((0)) FOR [ThuTu]
GO
ALTER TABLE [dbo].[ChiTietCaThi] ADD  CONSTRAINT [DF_ChiTietCaThi_DaThi]  DEFAULT ((0)) FOR [DaThi]
GO
ALTER TABLE [dbo].[ChiTietCaThi] ADD  CONSTRAINT [DF_ChiTietCaThi_DaHoanThanh]  DEFAULT ((0)) FOR [DaHoanThanh]
GO
ALTER TABLE [dbo].[ChiTietCaThi] ADD  CONSTRAINT [DF_ChiTietCaThi_Diem]  DEFAULT ((-1)) FOR [Diem]
GO
ALTER TABLE [dbo].[ChiTietCaThi] ADD  CONSTRAINT [DF_ChiTietCaThi_TongSoCau]  DEFAULT ((0)) FOR [TongSoCau]
GO
ALTER TABLE [dbo].[ChiTietCaThi] ADD  CONSTRAINT [DF_ChiTietCaThi_SoCauDung]  DEFAULT ((0)) FOR [SoCauDung]
GO
ALTER TABLE [dbo].[ChiTietCaThi] ADD  CONSTRAINT [DF_ChiTietCaThi_GioCongThem_1]  DEFAULT ((0)) FOR [GioCongThem]
GO
ALTER TABLE [dbo].[ChiTietDotThi] ADD  CONSTRAINT [DF_ChiTietDotThi_LanThi]  DEFAULT ((1)) FOR [LanThi]
GO
ALTER TABLE [dbo].[NguoiDung] ADD  CONSTRAINT [DF_NguoiDungs_DaXoa]  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[NguoiDung] ADD  CONSTRAINT [DF_NguoiDungs_DaKhoa]  DEFAULT ((0)) FOR [DaKhoa]
GO
ALTER TABLE [dbo].[NguoiDung] ADD  CONSTRAINT [DF_NguoiDungs_LaNguoiDungHeThong]  DEFAULT ((0)) FOR [LaNguoiDungHeThong]
GO
ALTER TABLE [dbo].[CaThi]  WITH CHECK ADD  CONSTRAINT [FK_CaThi_ChiTietDotThi] FOREIGN KEY([MaChiTietDotThi])
REFERENCES [dbo].[ChiTietDotThi] ([MaChiTietDotThi])
GO
ALTER TABLE [dbo].[CaThi] CHECK CONSTRAINT [FK_CaThi_ChiTietDotThi]
GO
ALTER TABLE [dbo].[ChiTietBaiThi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietBaiThi_ChiTietCaThi] FOREIGN KEY([MaChiTietCaThi])
REFERENCES [dbo].[ChiTietCaThi] ([MaChiTietCaThi])
GO
ALTER TABLE [dbo].[ChiTietBaiThi] CHECK CONSTRAINT [FK_ChiTietBaiThi_ChiTietCaThi]
GO
ALTER TABLE [dbo].[ChiTietCaThi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietCaThi_CaThi] FOREIGN KEY([MaCaThi])
REFERENCES [dbo].[CaThi] ([MaCaThi])
GO
ALTER TABLE [dbo].[ChiTietCaThi] CHECK CONSTRAINT [FK_ChiTietCaThi_CaThi]
GO
ALTER TABLE [dbo].[ChiTietCaThi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietCaThi_SinhVien] FOREIGN KEY([MaSinhVien])
REFERENCES [dbo].[SinhVien] ([MaSinhVien])
GO
ALTER TABLE [dbo].[ChiTietCaThi] CHECK CONSTRAINT [FK_ChiTietCaThi_SinhVien]
GO
ALTER TABLE [dbo].[ChiTietDotThi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDotThi_DotThi1] FOREIGN KEY([MaDotThi])
REFERENCES [dbo].[DotThi] ([MaDotThi])
GO
ALTER TABLE [dbo].[ChiTietDotThi] CHECK CONSTRAINT [FK_ChiTietDotThi_DotThi1]
GO
ALTER TABLE [dbo].[ChiTietDotThi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDotThi_LopAo] FOREIGN KEY([MaLopAo])
REFERENCES [dbo].[LopAo] ([MaLopAo])
GO
ALTER TABLE [dbo].[ChiTietDotThi] CHECK CONSTRAINT [FK_ChiTietDotThi_LopAo]
GO
ALTER TABLE [dbo].[DeThi]  WITH CHECK ADD  CONSTRAINT [FK_DeThi_MonHoc] FOREIGN KEY([MaMonHoc])
REFERENCES [dbo].[MonHoc] ([MaMonHoc])
GO
ALTER TABLE [dbo].[DeThi] CHECK CONSTRAINT [FK_DeThi_MonHoc]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_Khoa]
GO
ALTER TABLE [dbo].[LopAo]  WITH CHECK ADD  CONSTRAINT [FK_LopAo_MonHoc] FOREIGN KEY([MaMonHoc])
REFERENCES [dbo].[MonHoc] ([MaMonHoc])
GO
ALTER TABLE [dbo].[LopAo] CHECK CONSTRAINT [FK_LopAo_MonHoc]
GO
ALTER TABLE [dbo].[NguoiDung]  WITH CHECK ADD  CONSTRAINT [FK_NguoiDung_VaiTro] FOREIGN KEY([MaVaiTro])
REFERENCES [dbo].[VaiTro] ([MaVaiTro])
GO
ALTER TABLE [dbo].[NguoiDung] CHECK CONSTRAINT [FK_NguoiDung_VaiTro]
GO
/****** Object:  StoredProcedure [dbo].[Audio_Delete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Audio_Delete]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [dbo].[Audio]
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi


GO
/****** Object:  StoredProcedure [dbo].[Audio_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Audio_Insert]
	@MaChiTietCaThi [int],
	@TenFile [nvarchar](max),
	@SoLanNghe [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO 	[dbo].[Audio] 
(
				[MaChiTietCaThi],
				[TenFile],
				[SoLanNghe]
) 

VALUES 
(
				@MaChiTietCaThi,
				@TenFile,
				@SoLanNghe
				
)
SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[Audio_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Audio_SelectOne]
	@MaChiTietCaThi [int],
	@TenFile [nvarchar](max)
WITH EXECUTE AS CALLER
AS
SELECT	[SoLanNghe]
		
FROM
		[dbo].[Audio]
		
WHERE
		[MaChiTietCaThi] = @MaChiTietCaThi
	AND	[TenFile] = @TenFile



GO
/****** Object:  StoredProcedure [dbo].[Audio_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Audio_Update]
	@MaChiTietCaThi [int],
	@TenFile [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentCount INT;

    -- Lấy số lần nghe hiện tại nếu có
    SELECT @CurrentCount = SoLanNghe
    FROM [dbo].[Audio]
    WHERE [MaChiTietCaThi] = @MaChiTietCaThi
      AND [TenFile] = @TenFile;

    -- Nếu không có bản ghi, insert mới
    IF @CurrentCount IS NULL
    BEGIN
        INSERT INTO [dbo].[Audio] (
            [MaChiTietCaThi],
            [TenFile],
            [SoLanNghe]
        )
        VALUES (
            @MaChiTietCaThi,
            @TenFile,
            1
        );

        SELECT 1; -- Lần đầu nghe
    END
    ELSE
    BEGIN
        -- Nếu đã nghe >= 3 lần thì không cho nghe tiếp
        IF @CurrentCount >= 3
        BEGIN
            SELECT -1; -- Không cho phép nghe thêm
        END
        ELSE
        BEGIN
            -- Tăng số lần nghe lên 1
            UPDATE [dbo].[Audio]
            SET [SoLanNghe] = [SoLanNghe] + 1
            WHERE [MaChiTietCaThi] = @MaChiTietCaThi
              AND [TenFile] = @TenFile;

            SELECT @CurrentCount + 1; -- Trả về số lần nghe mới
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_Activate]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_Activate]
	@MaCaThi [int],
	@KichHoat [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [CaThi] 
SET
	[KichHoat]	= @KichHoat,
	[ThoiGianKichHoat] = getDate()
WHERE
	[MaCaThi] = @MaCaThi
GO
/****** Object:  StoredProcedure [dbo].[CaThi_CanInsertStudent]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_CanInsertStudent]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	count(*)
					
	FROM	[CaThi] CT 

	WHERE	CT.MaCaThi	= @MaCaThi
		AND	CT.KetThuc		= 0


GO
/****** Object:  StoredProcedure [dbo].[CaThi_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_ForceRemove]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Bảng biến để lưu danh sách chi tiết ca thi cần xóa
		DECLARE @ChiTietCaThi TABLE (MaChiTietCaThi INT);

		-- Lấy các bản ghi liên quan
		INSERT INTO @ChiTietCaThi (MaChiTietCaThi)
		SELECT MaChiTietCaThi
		FROM ChiTietCaThi
		WHERE MaCaThi = @MaCaThi;

		-- Xóa chi tiết bài thi trước
		DELETE FROM ChiTietBaiThi
		WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi);

		-- Xóa Audio
		DELETE FROM Audio
		WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi);

		-- Xóa chi tiết ca thi
		DELETE FROM ChiTietCaThi
		WHERE MaCaThi = @MaCaThi;

		-- Xóa ca thi
		DELETE FROM CaThi
		WHERE MaCaThi = @MaCaThi;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_HuyKichHoat]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_HuyKichHoat]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
UPDATE [CaThi] 
SET
	[KichHoat]	= 0,
	[ThoiGianKichHoat] = getDate()
WHERE
	[MaCaThi] = @MaCaThi

-- Xóa toàn bộ bài làm của sinh viên trong ca thi
    DELETE CTBT
    FROM ChiTietBaiThi CTBT
    JOIN ChiTietCaThi CTCT 
        ON CTBT.MaChiTietCaThi = CTCT.MaChiTietCaThi
    WHERE CTCT.MaCaThi = @MaCaThi;
-- Xóa những ghi nhận phần nghe trong ca thi
	DELETE Audio
	FROM Audio Audio
	JOIN ChiTietCaThi CTCT
		ON Audio.MaChiTietCaThi = CTCT.MaChiTietCaThi
	WHERE CTCT.MaCaThi = @MaCaThi
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_Insert]
	@TenCaThi [nvarchar](50),
	@MaChiTietDotThi [int],
	@ThoiGianBatDau [datetime],
	@ThoiGianThi [int],
	@MatMa [nvarchar](128)
WITH EXECUTE AS CALLER
AS
IF EXISTS (
	SELECT	* 
	FROM	[CaThi] 
	WHERE	[TenCaThi] = @TenCaThi
		and [MaChiTietDotThi] = @MaChiTietDotThi
)
	set @TenCaThi = @TenCaThi + '_01'

INSERT INTO [CaThi] 
(
	[TenCaThi],
	[MaChiTietDotThi],
	[ThoiGianBatDau],
	[DaGanDe],
	--[KichHoat],
	[ThoiGianThi],
	[MatMa]
) 
VALUES 
(
	@TenCaThi,
	@MaChiTietDotThi,
	@ThoiGianBatDau,
	0,
	--@KichHoat,
	@ThoiGianThi,
	@MatMa
)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[CaThi_IsExists]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_IsExists]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	count(*)
					
	FROM	[CaThi] CT 

	WHERE	CT.MaCaThi = @MaCaThi


GO
/****** Object:  StoredProcedure [dbo].[CaThi_Ketthuc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_Ketthuc]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
IF NOT EXISTS (
	SELECT	* FROM [CaThi] 
	WHERE	[MaCaThi] = @MaCaThi
		AND [KetThuc]	= 1)
BEGIN

	UPDATE [CaThi] 
	SET
		[KetThuc]	= 1,
		[ThoiDiemKetThuc] = getDate()
	WHERE
		[MaCaThi] = @MaCaThi
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_Ketthuc1]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_Ketthuc1]
	@MaCaThi [int],
	@is_ket_thuc [bit]
WITH EXECUTE AS CALLER
AS
IF NOT EXISTS (
	SELECT	* FROM [CaThi] 
	WHERE	[MaCaThi] = @MaCaThi
		AND [KetThuc]	= @is_ket_thuc)
BEGIN
	UPDATE [CaThi] 
	SET
		[KetThuc] = @is_ket_thuc,
		[ThoiDiemKetThuc] = getDate()
	WHERE
		[MaCaThi] = @MaCaThi
END


GO
/****** Object:  StoredProcedure [dbo].[CaThi_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_Remove]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
    DELETE FROM [CaThi]
	WHERE MaCaThi = @MaCaThi
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectBy_MaChiTietDotThi_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectBy_MaChiTietDotThi_Paged]
	@MaChiTietDotThi [int],
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [CaThi] CT
    WHERE CT.MaChiTietDotThi = @MaChiTietDotThi;

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *, (SELECT COUNT(*) FROM ChiTietCaThi  WHERE MaCaThi = CT.MaCaThi) AS TongSV
	FROM [CaThi] CT
    WHERE CT.MaChiTietDotThi = @MaChiTietDotThi
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), ThoiGianBatDau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectBy_MaChiTietDotThi_Search_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectBy_MaChiTietDotThi_Search_Paged]
	@MaChiTietDotThi [int],
	@Keyword NVARCHAR(100),
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [CaThi] CT
    WHERE CT.MaChiTietDotThi = @MaChiTietDotThi
	AND (
        CT.TenCaThi LIKE '%' + @Keyword + '%'
        OR CT.MaCaThi LIKE '%' + @Keyword + '%'
    );

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *, (SELECT COUNT(*) FROM ChiTietCaThi  WHERE MaCaThi = CT.MaCaThi) AS TongSV
    FROM [CaThi] CT
    WHERE CT.MaChiTietDotThi = @MaChiTietDotThi
	AND (
        CT.TenCaThi LIKE '%' + @Keyword + '%'
        OR CT.MaCaThi LIKE '%' + @Keyword + '%'
    )
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), ThoiGianBatDau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectBy_MaDotThi_MaLop]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectBy_MaDotThi_MaLop]
	@MaDotThi [int] = NULL,
	@MaLop [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ct.* 

FROM	[dbo].[CaThi] ct 

JOIN	[dbo].[ChiTietDotThi] ctdt 
	ON	ct.[MaChiTietDotThi]	= ctdt.[MaChiTietDotThi] 

JOIN	[dbo].[DotThi] dt 
	ON	dt.[MaDotThi]	= ctdt.MaDotThi

JOIN	[dbo].[LopAo] la 
	ON	la.[MaLopAo]	= ctdt.MaLopAo

WHERE	dt.MaDotThi	= @MaDotThi 
	AND la.[MaLopAo]	= @MaLop
GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectBy_MaDotThi_MaLop_LanThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectBy_MaDotThi_MaLop_LanThi]
	@MaDotThi [int] = NULL,
	@MaLop [int] = NULL,
	@LanThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ct.* 

FROM	[dbo].[CaThi] ct 

JOIN	[dbo].[ChiTietDotThi] ctdt 
	ON	ct.[MaChiTietDotThi]	= ctdt.[MaChiTietDotThi] 

JOIN	[dbo].[DotThi] dt 
	ON	dt.[MaDotThi]	= ctdt.MaDotThi

JOIN	[dbo].[LopAo] la 
	ON	la.[MaLopAo]	= ctdt.MaLopAo

WHERE	dt.MaDotThi	= @MaDotThi 
	AND la.[MaLopAo]	= @MaLop
	AND ctdt.LanThi	= @LanThi
GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectByMonHoc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectByMonHoc]
	@MaMonHoc [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
	FROM	CaThi ct

	LEFT JOIN [ChiTietDotThi] ctdt
	ON	ct.MaChiTietDotThi = ctdt.MaChiTietDotThi 

	LEFT JOIN [LopAo] la
	ON	la.MaLopAo = ctdt.MaLopAo

	WHERE la.MaMonHoc = @MaMonHoc


GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectByMonThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectByMonThi]
	@MaMonHoc [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
	FROM	CaThi ct

	LEFT JOIN [ChiTietDotThi] ctdt
	ON	ct.MaChiTietDotThi = ctdt.MaChiTietDotThi 

	LEFT JOIN [LopAo] la
	ON	la.MaLopAo = ctdt.MaLopAo

	WHERE la.MaMonHoc = @MaMonHoc


GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectCongGioMax]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectCongGioMax]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	'GioCongThem_MAX' = MAX([GioCongThem]) 
FROM	[ChiTietCaThi] ctct
WHERE	[MaCaThi] = @MaCaThi
 

GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectKetThuc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectKetThuc]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
IF EXISTS (	
		SELECT	*
		FROM	[CaThi]
		WHERE	[MaCaThi] = @MaCaThi			
			AND [KetThuc]	= 1
	)
SELECT 1
ELSE
SELECT 0




GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectOne]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM
	[CaThi]
WHERE
	[MaCaThi] = @MaCaThi
GO
/****** Object:  StoredProcedure [dbo].[CaThi_SelectResult]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_SelectResult]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	'MaDeThi'		= ctct.MaDeThi,
		'DiemThi'		= ctct.Diem,
		'DaThi'			= ctct.DaThi,
		'DaHoanThanh'	= ctct.DaHoanThanh,
		'TenCaThi'		= ct.TenCaThi,
		'NgayThi'		= ct.ThoiGianBatDau,
		'ThoiLuongThi'	= ct.ThoiGianThi,
		'MSSV'			= sv.MaSoSinhVien,
		'Ho'			= sv.HoVaTenLot,
		'Ten'			= sv.TenSinhVien,
		'GioiTinh'		= sv.GioiTinh,
		'SoCauDung'		= ctct.SoCauDung,
		'TongSoCau'		= ctct.TongSoCau
		--,'NgaySinh'		= sv.NgaySinh

FROM	[ChiTietCaThi] ctct

LEFT OUTER JOIN [CaThi] ct
	ON	ctct.[MaCaThi] = ct.[MaCaThi] 

LEFT OUTER JOIN [SinhVien] sv
	ON	ctct.[MaSinhVien] = sv.[MaSinhVien] 

WHERE	ctct.[MaCaThi] = @MaCaThi

ORDER BY	sv.[TenSinhVien], sv.[HoVaTenLot]
GO
/****** Object:  StoredProcedure [dbo].[CaThi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_Update]
	@MaCaThi [int],
	@TenCaThi [nvarchar](50),
	@MaChiTietDotThi [int],
	@ThoiGianBatDau [datetime],
	@ThoiGianThi [int],
	@MatMa [nvarchar](128)
WITH EXECUTE AS CALLER
AS
BEGIN
	UPDATE [CaThi] 
	SET
		[TenCaThi] = @TenCaThi,
		[MaChiTietDotThi] = @MaChiTietDotThi,
		[ThoiGianBatDau] = @ThoiGianBatDau,
		--[KichHoat ]=@KichHoat,
		[ThoiGianThi]=@ThoiGianThi
	WHERE
	[MaCaThi] = @MaCaThi

	-- Nếu có MatMa (khác null, khác rỗng, khác toàn khoảng trắng)
	IF (@MatMa IS NOT NULL AND LTRIM(RTRIM(@MatMa)) <> '')
	BEGIN
		UPDATE [CaThi]
		SET [MatMa] = @MatMa
		WHERE [MaCaThi] = @MaCaThi;
	END
END
GO
/****** Object:  StoredProcedure [dbo].[CaThi_UpdateDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaThi_UpdateDeThi]
	@MaCaThi [int],
	@IsOrderMSSV [bit],
	@DsDeThiHoanVi NVARCHAR(max)
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION

        -- Cập nhật đề gốc cho ca thi
        UPDATE CaThi
        SET DaGanDe = 1
        WHERE MaCaThi = @MaCaThi;

        -- Tạo danh sách đề hoán vị có thứ tự index
        ;WITH DeThiHV_CTE AS (
            SELECT 
                ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS rn,
                value AS MaDeHoanVi
            FROM STRING_SPLIT(@DsDeThiHoanVi, ',')
        ),
        SV_CTE AS (
            SELECT 
                ROW_NUMBER() OVER (
				ORDER BY 
					CASE WHEN @IsOrderMSSV = 1 THEN SV.MaSoSinhVien END,
					CASE WHEN @IsOrderMSSV = 0 THEN SV.TenSinhVien END) AS rn,
                C.MaChiTietCaThi
            FROM ChiTietCaThi C
            JOIN SinhVien SV ON SV.MaSinhVien = C.MaSinhVien
            WHERE C.MaCaThi = @MaCaThi
        )

        -- Gán mã đề hoán vị theo thứ tự MSSV
        UPDATE C
        SET MaDeThi = D.MaDeHoanVi
        FROM ChiTietCaThi C
        INNER JOIN SV_CTE S ON C.MaChiTietCaThi = S.MaChiTietCaThi
        INNER JOIN DeThiHV_CTE D ON S.rn = D.rn;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_DaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_DaThi]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
	SELECT STRING_AGG(ctbt.CauTraLoi, ';;;')
	FROM [ChiTietBaiThi] ctbt
	WHERE ctbt.MaChiTietCaThi = @MaChiTietCaThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Delete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Delete]
	@MaChiTietBaiThi [bigint]
WITH EXECUTE AS CALLER
AS
DELETE FROM [ChiTietBaiThi]
WHERE
	[MaChiTietBaiThi] = @MaChiTietBaiThi

GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Insert]
	@MaChiTietCaThi [int],
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@MaCLO [int],
	@NgayTao [datetime],
	@ThuTu [int]
WITH EXECUTE AS CALLER
AS
-- kiem tra ton tai
if not exists (select * from [ChiTietBaiThi]
	where	[MaChiTietCaThi] = @MaChiTietCaThi
		and [MaDeHV]	= @MaDeHV
		and [MaNhom]	= @MaNhom
		and [MaCauHoi]	= @MaCauHoi
		and [MaCLO]		= @MaCLO)
begin
	INSERT INTO [ChiTietBaiThi] 
	(
		[MaChiTietCaThi],
		[MaDeHV],
		[MaNhom],
		[MaCauHoi],
		[MaCLO],
		[NgayTao],
		[ThuTu]
	) 
	VALUES 
	(
		@MaChiTietCaThi,
		@MaDeHV,
		@MaNhom,
		@MaCauHoi,
		@MaCLO,
		@NgayTao,
		@ThuTu
	)
end
select @@identity
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Insert_Batch]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Insert_Batch]
	@Data ChiTietBaiThiType READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRANSACTION;

		INSERT INTO ChiTietBaiThi (
			MaChiTietCaThi,
			MaDeHV,
			MaNhom,
			MaCauHoi,
			MaCLO,
			CauTraLoi,
			NgayTao,
			NgayCapNhat,
			KetQua,
			ThuTu
		)
		SELECT
			source.MaChiTietCaThi,
			source.MaDeHV,
			source.MaNhom,
			source.MaCauHoi,
			source.MaCLO,
			source.CauTraLoi,
			source.NgayTao, 
			source.NgayCapNhat,
			source.KetQua,
			source.ThuTu
		FROM @Data source;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Save]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Save]
	@MaChiTietCaThi [int],
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@MaCLO [int],
	@CauTraLoi [int],
	@NgayTao [datetime],
	@NgayCapNhat [datetime],
	@KetQua [bit],
	@ThuTu [int]
WITH EXECUTE AS CALLER
AS
-- kiem tra ton tai
if not exists (select * from [ChiTietBaiThi]
	where	[MaChiTietCaThi] = @MaChiTietCaThi
		and [MaDeHV]	= @MaDeHV
		and [MaNhom]	= @MaNhom
		and [MaCauHoi]	= @MaCauHoi
		and [MaCLO]		= @MaCLO)
begin
	INSERT INTO [ChiTietBaiThi] 
	(
		[MaChiTietCaThi],
		[MaDeHV],
		[MaNhom],
		[MaCauHoi],
		[MaCLO],
		[CauTraLoi],
		[NgayTao],
		[KetQua],
		[ThuTu]
	) 
	VALUES 
	(
		@MaChiTietCaThi,
		@MaDeHV,
		@MaNhom,
		@MaCauHoi,
		@MaCLO,
		@CauTraLoi,
		@NgayTao,
		@KetQua,
		@ThuTu
	)
end
-- neu ton tai thi cap nhat
else
begin
	UPDATE [ChiTietBaiThi] 
SET
	[CauTraLoi]		= @CauTraLoi,
	[NgayCapNhat]	= @NgayCapNhat,
	[KetQua]		= @KetQua

where	[MaChiTietCaThi] = @MaChiTietCaThi
		and [MaDeHV]	= @MaDeHV
		and [MaNhom]	= @MaNhom
		and [MaCauHoi]	= @MaCauHoi
end
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Save_Batch]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Save_Batch]
    @Data ChiTietBaiThiType READONLY
AS
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    MERGE ChiTietBaiThi AS target
    USING @Data AS source
    ON target.MaChiTietCaThi = source.MaChiTietCaThi
       AND target.MaDeHV = source.MaDeHV
       AND target.MaNhom = source.MaNhom
       AND target.MaCauHoi = source.MaCauHoi
       AND target.MaCLO = source.MaCLO
    WHEN MATCHED THEN
        UPDATE SET
            target.CauTraLoi = source.CauTraLoi,
            target.NgayCapNhat = GetDate(),
            target.KetQua = source.KetQua
    WHEN NOT MATCHED THEN
        INSERT (
            MaChiTietCaThi, MaDeHV, MaNhom, MaCauHoi, MaCLO,
            CauTraLoi, NgayTao, KetQua, ThuTu
        )
        VALUES (
            source.MaChiTietCaThi, source.MaDeHV, source.MaNhom,
            source.MaCauHoi, source.MaCLO, source.CauTraLoi,
            GetDate(), source.KetQua, source.ThuTu
        );
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_SelectAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[ChiTietBaiThi]


GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_SelectBy_MaChiTietCaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_SelectBy_MaChiTietCaThi]
	@MaChiTietCaThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	bt.* 
FROM	ChiTietBaiThi bt

--left outer join NhomCauHoiHoanVi nhv
--	on nhv.MaDeHV = bt.MaDeHV
--	and nhv.MaNhom = bt.MaNhom
--	and nhv.MaNhom in(
--		select MaNhom from NhomCauHoi
--		where MaNhomCha = -1 or MaNhomCha is null)
	
--left outer join NhomCauHoiHoanVi nhv2
--	on nhv2.MaDeHV = bt.MaDeHV
--	and nhv2.MaNhom = bt.MaNhom
--	and nhv2.MaNhom not in(
--		select MaNhom from NhomCauHoi
--		where MaNhomCha = -1 or MaNhomCha is null)

--left outer join ChiTietDeThiHoanVi cthv
--	on cthv.MaDeHV = bt.MaDeHV
--	and cthv.MaNhom = bt.MaNhom
--	and cthv.MaCauHoi = bt.MaCauHoi

WHERE	 bt.MaChiTietCaThi = @MaChiTietCaThi
			
order by bt.ThuTu--	nhv.ThuTu, nhv2.ThuTu, cthv.ThuTu
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_SelectBy_MaChiTietCaThi_Count]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_SelectBy_MaChiTietCaThi_Count]
	@MaChiTietCaThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT		count(*)

	FROM		ChiTietBaiThi

	WHERE		MaChiTietCaThi = @MaChiTietCaThi


GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_SelectBy_MaChiTietCaThi_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_SelectBy_MaChiTietCaThi_Paged]
	@MaChiTietCaThi [int] = NULL,
	@PageSize [int] = 10,
	@PageNumber [int] = 1
WITH EXECUTE AS CALLER
AS
DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int

	SET @PageLowerBound = (@PageSize * @PageNumber) - @PageSize
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1

	create TABLE #PageIndex
	(
		IndexID int IDENTITY (1, 1) NOT NULL,
		MaChiTietBaiThi bigint
	)

	BEGIN

	INSERT INTO #PageIndex 
	(
		MaChiTietBaiThi
	)

	SELECT		MaChiTietBaiThi

	FROM		ChiTietBaiThi

	WHERE		MaChiTietCaThi = @MaChiTietCaThi

	ORDER BY	ThuTu

	END

	DECLARE @TotalRows int
	DECLARE @TotalPages int
	DECLARE @Remainder int

	SET @TotalRows = (SELECT Count(*) FROM #PageIndex)
	SET @TotalPages = @TotalRows / @PageSize
	SET @Remainder = @TotalRows % @PageSize
	IF	@Remainder > 0
	SET @TotalPages = @TotalPages + 1

	SELECT
			t.*,
			'STT' = t2.IndexID
	FROM
			[dbo].ChiTietBaiThi t

	JOIN	#PageIndex t2
	ON		t.MaChiTietBaiThi = t2.MaChiTietBaiThi

	WHERE
			t2.IndexID > @PageLowerBound
		AND t2.IndexID < @PageUpperBound
			
	ORDER BY t2.IndexID

	DROP TABLE #PageIndex

			
			
GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_SelectOne]
	@MaChiTietBaiThi [bigint]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[ChiTietBaiThi]
WHERE
	[MaChiTietBaiThi] = @MaChiTietBaiThi


GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_SelectOne_v2]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_SelectOne_v2]
	@MaChiTietCaThi [int],
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@MaCLO [int]
WITH EXECUTE AS CALLER
AS
SELECT 	bt.*,
		dt.HoanViTraLoi,
		dt.DapAn
		
FROM	[ChiTietBaiThi] bt

LEFT OUTER JOIN [ChiTietDeThiHoanVi] dt
	ON dt.MaDeHV = bt.MaDeHV 

WHERE
	bt.MaChiTietCaThi	= @MaChiTietCaThi
AND	bt.MaDeHV				= @MaDeHV
AND bt.MaNhom				= @MaNhom
AND bt.MaCauHoi				= @MaCauHoi
AND bt.MaCLO				= @MaCLO


GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Update]
	@MaChiTietBaiThi [bigint],
	@CauTraLoi [int],
	@NgayCapNhat [datetime],
	@KetQua [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietBaiThi] 
SET
	[CauTraLoi]		= @CauTraLoi,
	[NgayCapNhat]	= @NgayCapNhat,
	[KetQua]		= @KetQua
	--[ThuTu]			= @ThuTu
WHERE
	[MaChiTietBaiThi] = @MaChiTietBaiThi
 

GO
/****** Object:  StoredProcedure [dbo].[ChiTietBaiThi_Update_v2]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietBaiThi_Update_v2]
	@MaChiTietCaThi [bigint],
	@MaCauHoi [int],
	@MaCLO [int],
	@CauTraLoi [int],
	@NgayCapNhat [datetime],
	@KetQua [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietBaiThi] 
SET
	[MaCLO]			= @MaCLO,
	[CauTraLoi]		= @CauTraLoi,
	[NgayCapNhat]	= @NgayCapNhat,
	[KetQua]		= @KetQua
	--[ThuTu]			= @ThuTu
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi
AND
	[MaCauHoi] = @MaCauHoi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_CongGio]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_CongGio]
	@MaChiTietCaThi [int],
	@GioCongThem [int],
	@ThoiDiemCong [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietCaThi] 
SET
	[GioCongThem] = @GioCongThem,
	[ThoiDiemCong] = @ThoiDiemCong
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_ForceRemove]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
	BEGIN TRANSACTION

	DELETE FROM [ChiTietBaiThi]
	WHERE MaChiTietCaThi = @MaChiTietCaThi

	DELETE FROM [ChiTietCaThi]
	WHERE [MaChiTietCaThi] = @MaChiTietCaThi

	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[ChiTietCaThi] CTCT JOIN [SinhVien] SV ON CTCT.MaSinhVien = SV.MaSinhVien
	JOIN [CaThi] CT ON CTCT.MaCaThi = CT.MaCaThi


GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_Insert]
	@MaCaThi [int],
	@MaSinhVien [bigint],
	@MaDeThi [bigint],
	@TongSoCau [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [ChiTietCaThi] 
(
	[MaCaThi],
	[MaSinhVien],
	[MaDeThi],
	[TongSoCau]
) 
VALUES 
(
	@MaCaThi,
	@MaSinhVien,
	@MaDeThi,
	@TongSoCau
)

SELECT @@IDENTITY




GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_Insert_Batch]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_Insert_Batch]
	@DanhSachSinhVienCaThi SinhVienCaThiType READONLY
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION;
    SET NOCOUNT ON;

    INSERT INTO ChiTietCaThi(MaCaThi, MaSinhVien, MaDeThi)
    SELECT s.MaCaThi, sv.MaSinhVien, s.MaDeThi
    FROM SinhVien sv
    INNER JOIN @DanhSachSinhVienCaThi s ON sv.MaSoSinhVien = s.MaSoSinhVien
    WHERE NOT EXISTS (
        SELECT 1 FROM ChiTietCaThi ctct
        WHERE ctct.MaCaThi = s.MaCaThi AND ctct.MaSinhVien = sv.MaSinhVien
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_Remove]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [ChiTietCaThi]
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi



GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_Save_Batch]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_Save_Batch]
WITH EXECUTE AS CALLER
AS
BEGIN
SELECT *
FROM [SinhVien]
END
/*	@DanhSachSinhVien SinhVienType READONLY,
	@MaCaThi [INT]
AS
BEGIN
BEGIN TRANSACTION;
	SET NOCOUNT ON;

    -- Chèn sinh viên mới nếu chưa có (giả sử MaSoSinhVien là duy nhất)
    INSERT INTO SinhVien (MaSoSinhVien, HoVaTenLot, TenSinhVien, GioiTinh)
    SELECT s.MaSoSinhVien, s.HoVaTenLot, s.TenSinhVien, s.GioiTinh
    FROM @DanhSachSinhVien s
    WHERE NOT EXISTS (
        SELECT 1 FROM SinhVien sv WHERE sv.MaSoSinhVien = s.MaSoSinhVien
    );

    -- Chèn vào bảng chi tiết ca thi (ví dụ ChiTietCaThi)
    INSERT INTO ChiTietCaThi(MaCaThi, MaSinhVien)
    SELECT @MaCaThi, sv.MaSinhVien
    FROM SinhVien sv
    INNER JOIN @DanhSachSinhVien s ON sv.MaSoSinhVien = s.MaSoSinhVien
    WHERE NOT EXISTS (
        SELECT 1 FROM ChiTietCaThi ctct
        WHERE ctct.MaCaThi = @MaCaThi AND ctct.MaSinhVien = sv.MaSinhVien
    );
COMMIT TRANSACTION;
END*/
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_Select_GioCongThem]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_Select_GioCongThem]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	[GioCongThem]
FROM	[ChiTietCaThi]
WHERE	[MaChiTietCaThi] = @MaChiTietCaThi




GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi]
	@MaCaThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	CTCT.*, SV.*, DT.KyHieuDe 
FROM	[ChiTietCaThi] CTCT 

LEFT JOIN	[SinhVien] SV 
	ON	CTCT.MaSinhVien = SV.MaSinhVien
LEFT JOIN	[DeThi] DT
	ON CTCT.MaDeThi = DT.MaDeThi

WHERE CTCT.MaCaThi = @MaCaThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Count]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Count]
	@MaCaThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	count(*)

	FROM	[ChiTietCaThi]

	WHERE	[MaCaThi] = @MaCaThi


GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi_CountForSearch]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi_CountForSearch]
	@MaCaThi [int] = NULL,
	@Keyword [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SELECT	count(CTCT.[MaChiTietCaThi])

	FROM	[ChiTietCaThi] CTCT

	JOIN	[SinhVien] sv
		ON	CTCT.MaSinhVien = sv.MaSinhVien

	WHERE	[MaCaThi] = @MaCaThi
		AND (
			sv.[MaSoSinhVien] LIKE '%' + @Keyword + '%'
		OR sv.[TenSinhVien] LIKE '%' + @Keyword + '%'
		OR sv.[HoVaTenLot] LIKE '%' + @Keyword + '%')

	

GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Page]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Page]
	@MaCaThi [int] = NULL,
	@PageIndex INT = 0,
	@PageSize INT = 0
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        * 
    FROM 
        [ChiTietCaThi] CTCT 
    JOIN 
        [SinhVien] SV ON CTCT.MaSinhVien = SV.MaSinhVien
    WHERE 
        CTCT.MaCaThi = @MaCaThi
    ORDER BY 
        CTCT.MaSinhVien -- cần ORDER BY để OFFSET-FETCH hoạt động
    OFFSET @PageIndex * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Paged]
    @MaCaThi INT,
    @PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [ChiTietCaThi] CTCT
    WHERE CTCT.MaCaThi = @MaCaThi;

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT 
        CTCT.*, 
        SV.*, 
		DT.KyHieuDe,
        --CT.*, bỏ qua ca thi vì lặp quá nhiều
        ROW_NUMBER() OVER (ORDER BY SV.MaSoSinhVien) AS RowNum
    FROM [ChiTietCaThi] CTCT
    LEFT JOIN [SinhVien] SV ON CTCT.MaSinhVien = SV.MaSinhVien
	LEFT JOIN [DeThi] DT ON CTCT.MaDeThi = DT.MaDeThi
    WHERE CTCT.MaCaThi = @MaCaThi
    ORDER BY SV.MaSoSinhVien -- sắp xếp theo mã số sinh viên
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Search_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi_Search_Paged]
    @MaCaThi INT,
    @Keyword NVARCHAR(100),
    @PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán số lượng bản ghi và trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện tìm kiếm
    SELECT @TotalRecords = COUNT(*)
    FROM [ChiTietCaThi] CTCT
    JOIN [SinhVien] SV ON CTCT.MaSinhVien = SV.MaSinhVien
    WHERE CTCT.MaCaThi = @MaCaThi
    AND (
        SV.MaSoSinhVien LIKE '%' + @Keyword + '%'
        OR SV.TenSinhVien LIKE '%' + @Keyword + '%'
        OR SV.HoVaTenLot LIKE '%' + @Keyword + '%'
    );

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Lấy dữ liệu phân trang
    SELECT 
        CTCT.*, 
        SV.*, 
		DT.KyHieuDe,
		-- CT.*, bỏ ca thi vì lặp nhiều
        ROW_NUMBER() OVER (ORDER BY SV.MaSoSinhVien) AS RowNum
    FROM [ChiTietCaThi] CTCT
    LEFT JOIN [SinhVien] SV ON CTCT.MaSinhVien = SV.MaSinhVien
	LEFT JOIN [DeThi] DT ON CTCT.MaDeThi = DT.MaDeThi
    WHERE CTCT.MaCaThi = @MaCaThi
    AND (
        SV.MaSoSinhVien LIKE '%' + @Keyword + '%'
        OR SV.TenSinhVien LIKE '%' + @Keyword + '%'
        OR SV.HoVaTenLot LIKE '%' + @Keyword + '%'
    )
    ORDER BY SV.MaSoSinhVien
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaCaThi1]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaCaThi1]
	@MaCaThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	MaSoSinhVien, 
		HoVaTenLot, 
		TenSinhVien 
FROM	ChiTietCaThi CTCT 
JOIN	[SinhVien] SV 
	ON	CTCT.MaSinhVien = SV.MaSinhVien
JOIN	[CaThi] CT 
	ON	CTCT.MaCaThi = CT.MaCaThi
WHERE	CTCT.MaCaThi = @MaCaThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectBy_MaSinhVienThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectBy_MaSinhVienThi]
	@MaSinhVien [bigint] = NULL
WITH EXECUTE AS CALLER
AS
SELECT TOP 1 CTCT.*, CT.*, CTDT.*, LA.*, MH.*, SV.*, DT.KyHieuDe

FROM	[ChiTietCaThi] CTCT 

LEFT JOIN	[CaThi] CT 
	ON	CTCT.MaCaThi			= CT.MaCaThi

LEFT JOIN	[SinhVien] SV
	ON	CTCT.MaSinhVien			= SV.MaSinhVien

LEFT JOIN	[ChiTietDotThi] CTDT
	ON	CT.MaChiTietDotThi	= CTDT.MaChiTietDotThi

LEFT JOIN	[LopAo] LA
	ON	CTDT.MaLopAo			= LA.MaLopAo

LEFT JOIN	[MonHoc] MH 
	ON	LA.MaMonHoc			= MH.MaMonHoc

LEFT JOIN	[DeThi] DT
	ON	DT.MaDeThi			= CTCT.MaDeThi

WHERE	CTCT.MaSinhVien		= @MaSinhVien 
	AND CT.KetThuc				= 0 -- ca thi chua ket thuc	
	AND CT.KichHoat			= 1 -- da kich hoat
	AND	CTCT.DaHoanThanh		= 0 -- chua thi xong
	AND (CTCT.ThoiGianBatDau IS NULL
		OR DATEADD(MINUTE, CT.ThoiGianThi, CTCT.ThoiGianBatDau) >= GETDATE()) -- Chưa hết giờ
ORDER BY
    ABS(DATEDIFF(SECOND, GETDATE(), CTCT.ThoiGianBatDau)) -- lấy thời gian gần nhất với hiện tại
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_SelectOne]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	ctct.*, sv.*

FROM
	[ChiTietCaThi] ctct

LEFT OUTER JOIN [SinhVien] sv
	ON	sv.MaSinhVien = ctct.MaSinhVien 


WHERE
	ctct.[MaChiTietCaThi] = @MaChiTietCaThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_Update]
	@MaChiTietCaThi [int],
	@MaCaThi [int],
	@MaSinhVien [bigint],
	@MaDeThi [bigint],
	@TongSoCau [int]
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietCaThi] SET
	[MaCaThi] = @MaCaThi,
	[MaSinhVien] = @MaSinhVien,
	[MaDeThi] = @MaDeThi,
	[TongSoCau] = @TongSoCau 
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi
 


GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_UpdateBatDau]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_UpdateBatDau]
	@MaChiTietCaThi [int],
	@ThoiGianBatDau [datetime]
WITH EXECUTE AS CALLER
AS
IF EXISTS (SELECT * FROM [ChiTietCaThi]
	WHERE	[MaChiTietCaThi] = @MaChiTietCaThi
		and [ThoiGianBatDau] is NULL)
BEGIN

UPDATE [ChiTietCaThi] 
SET
	ThoiGianBatDau = @ThoiGianBatDau,
	DaThi = 1 --da thi
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi
 
END

GO
/****** Object:  StoredProcedure [dbo].[ChiTietCaThi_UpdateKetThuc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietCaThi_UpdateKetThuc]
	@MaChiTietCaThi [int],
	@ThoiGianKetThuc [datetime],
	@Diem [float],
	@SoCauDung [int],
	@TongSoCau [int]
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietCaThi] 
SET
	ThoiGianKetThuc = @ThoiGianKetThuc,
	Diem = @Diem,
	SoCauDung = @SoCauDung,
	TongSoCau = @TongSoCau,
	DaHoanThanh = 1 --da hoan thanh
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi
 


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_Insert]
	@TenChiTietDotThi [nvarchar](200),
	@MaLopAo [int],
	@MaDotThi [int],
	@LanThi [nvarchar](200)
WITH EXECUTE AS CALLER
AS
INSERT INTO [ChiTietDotThi] (
	[TenChiTietDotThi],
	[MaLopAo],
	[MaDotThi],
	[LanThi]
) VALUES (
	@TenChiTietDotThi,
	@MaLopAo,
	@MaDotThi,
	@LanThi
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_Remove]
	@MaChiTietDotThi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [ChiTietDotThi]
	WHERE MaChiTietDotThi = @MaChiTietDotThi
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaCTDT_MaDotThi_MaLopAo]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaCTDT_MaDotThi_MaLopAo]
	@MaChiTietDotThi [int] = NULL,
	@MaDotThi [int] = NULL,
	@MaLopAo [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDotThi CTDT LEFT JOIN [DotThi] DT ON CTDT.MaDotThi=DT.MaDotThi
	LEFT JOIN [LopAo] LA ON CTDT.MaLopAo=LA.MaLopAo
WHERE CTDT.MaDotThi = @MaDotThi AND CTDT.MaLopAo = @MaLopAo AND CTDT.MaChiTietDotThi != @MaChiTietDotThi

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaCTDT_MaLopAo_LanThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaCTDT_MaLopAo_LanThi]
	@MaChiTietDotThi [int] = NULL,
	@MaLopAo [int] = NULL,
	@LanThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDotThi CTDT LEFT JOIN [DotThi] DT ON CTDT.MaDotThi=DT.MaDotThi
	LEFT JOIN [LopAo] LA ON CTDT.MaLopAo=LA.MaLopAo
WHERE CTDT.LanThi = @LanThi AND CTDT.MaLopAo = @MaLopAo AND CTDT.MaChiTietDotThi != @MaChiTietDotThi

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaDotThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaDotThi]
	@MaDotThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT CTDT.*, LA.*, MH.*
FROM ChiTietDotThi CTDT 
	JOIN [LopAo] LA ON CTDT.MaLopAo=LA.MaLopAo
	JOIN [MonHoc] MH ON MH.MaMonHoc = LA.MaMonHoc
WHERE CTDT.MaDotThi = @MaDotThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaDotThi_MaLopAo]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaDotThi_MaLopAo]
	@MaDotThi [int] = NULL,
	@MaLopAo [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDotThi CTDT LEFT JOIN [DotThi] DT ON CTDT.MaDotThi=DT.MaDotThi
	LEFT JOIN [LopAo] LA ON CTDT.MaLopAo=LA.MaLopAo
WHERE CTDT.MaDotThi = @MaDotThi AND CTDT.MaLopAo = @MaLopAo

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaDotThi_MaLopAo_LanThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaDotThi_MaLopAo_LanThi]
	@MaDotThi [int] = NULL,
	@MaLopAo [int] = NULL,
	@LanThi [nvarchar] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDotThi CTDT 
WHERE CTDT.MaDotThi = @MaDotThi AND CTDT.LanThi = @LanThi AND CTDT.MaLopAo = @MaLopAo
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaDotThi_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaDotThi_Paged]
	@MaDotThi [int],
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [ChiTietDotThi] CTDT
	LEFT JOIN [LopAo] LA ON LA.MaLopAo = CTDT.MaLopAo
	LEFT JOIN [MonHoc] MH ON MH.MaMonHoc = LA.MaMonHoc
    WHERE CTDT.MaDotThi = @MaDotThi

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [ChiTietDotThi] CTDT
	LEFT JOIN [LopAo] LA ON LA.MaLopAo = CTDT.MaLopAo
	LEFT JOIN [MonHoc] MH ON MH.MaMonHoc = LA.MaMonHoc
	WHERE CTDT.MaDotThi = @MaDotThi
    ORDER BY CTDT.LanThi DESC -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaLopAo]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaLopAo]
	@MaLopAo [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDotThi
WHERE MaLopAo = @MaLopAo
			
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: Friday, December 25, 2009
-- Created By:   LXMANH
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectBy_MaLopAo_LanThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectBy_MaLopAo_LanThi]
	@MaLopAo [int] = NULL,
	@LanThi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDotThi CTDT LEFT JOIN [DotThi] DT ON CTDT.MaDotThi=DT.MaDotThi
	LEFT JOIN [LopAo] LA ON CTDT.MaLopAo=LA.MaLopAo
WHERE CTDT.LanThi = @LanThi AND CTDT.MaLopAo = @MaLopAo

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_SelectOne]
	@MaChiTietDotThi [int]
WITH EXECUTE AS CALLER
AS
SELECT ctdt.*, la.*, mh.*
FROM
	[ChiTietDotThi] ctdt
JOIN [LopAo] la ON la.MaLopAo = ctdt.MaLopAo
JOIN [MonHoc] mh ON mh.MaMonHoc = la.MaMonHoc
WHERE
	[MaChiTietDotThi] = @MaChiTietDotThi
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDotThi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDotThi_Update]
	@MaChiTietDotThi [int],
	@TenChiTietDotThi [nvarchar](200),
	@MaLopAo [int],
	@MaDotThi [int],
	@LanThi [nvarchar](200)
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietDotThi] SET
	[TenChiTietDotThi] = @TenChiTietDotThi,
	[MaLopAo] = @MaLopAo,
	[MaDotThi] = @MaDotThi,
	[LanThi] = @LanThi
WHERE
	[MaChiTietDotThi] = @MaChiTietDotThi

GO
/****** Object:  StoredProcedure [dbo].[Custom_GetDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_GetDeThi]
@MaDeThiHoanVi [bigint]
WITH EXECUTE AS CALLER
AS
SELECT 
    nch.MaNhom, 
    ch.MaCauHoi, 
	nch.MaNhomCha,
	clo.MaCLO,
    clo.MaSoCLO, 
	CASE
		WHEN dt.BoChuongPhan = 1 THEN NULL
		ELSE nch2.NoiDung
	END AS NoiDungNhomCha,
    CASE 
        WHEN dt.BoChuongPhan = 1 AND nch.LaCauHoiNhom = 0 THEN NULL 
        ELSE nch.NoiDung 
    END AS NoiDungNhom, 
    ch.NoiDung AS NoiDungCauHoi, 
    ch.KieuNoiDung AS KieuNoiDungCauHoi, 
	nch.KieuNoiDung AS KieuNoiDungCauHoiNhom,
	STRING_AGG(ctl.MaCauTraLoi, ';;;') AS MaDapAnGop,
	ctdthv.HoanViTraLoi,
    STRING_AGG(ctl.NoiDung, ';;;') AS NoiDungDapAnGop,
	ROW_NUMBER() OVER (ORDER BY MIN(ctdthv.ThuTu)) AS ThuTuCauHoi
FROM [ChiTietDeThiHoanVi] ctdthv
JOIN [NhomCauHoi] nch ON ctdthv.MaNhom = nch.MaNhom
JOIN [CauHoi] ch ON ctdthv.MaCauHoi = ch.MaCauHoi
JOIN [CLO] clo ON ch.MaCLO = clo.MaCLO
JOIN [CauTraLoi] ctl ON ch.MaCauHoi = ctl.MaCauHoi
LEFT JOIN [NhomCauHoi] nch2 ON nch.MaNhomCha = nch2.MaNhom
JOIN [DeThiHoanVi] dthv ON ctdthv.MaDeHV = dthv.MaDeHV
JOIN [DeThi] dt ON dthv.MaDeThi = dt.MaDeThi
WHERE ctdthv.MaDeHV = @MaDeThiHoanVi
GROUP BY 
    nch.MaNhom, 
    ch.MaCauHoi, 
	nch.MaNhomCha,
	clo.MaCLO,
	clo.MaSoCLO,
    ch.MaCLO, 
    nch2.NoiDung, 
    nch.NoiDung, 
    ch.NoiDung, 
    ch.KieuNoiDung, 
	nch.KieuNoiDung,
	dt.BoChuongPhan,
	nch.LaCauHoiNhom,
	ctdthv.HoanViTraLoi
GO
/****** Object:  StoredProcedure [dbo].[Custom_LayMaThongTinDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_LayMaThongTinDeThi]
    @MaDeThi BIGINT
WITH EXECUTE AS CALLER
AS
BEGIN
	-- Khi lấy dữ liệu nhớ các hàng không có MaCauHoi vì nó là chương
    -- Lấy dữ liệu câu hỏi có phân chia theo chương
    SELECT 
        nch.MaNhomCha AS Chuong,                         -- Mã chương
        nch.MaNhom AS NhomCauHoi,                         -- Mã nhóm câu hỏi
        nch.HoanVi AS HoanViNhom,                         -- Cờ hoán vị nhóm
        nch.ThuTu AS ThuTuNhom,                           -- Thứ tự nhóm
        ch.MaCauHoi,                                      -- Mã câu hỏi
        ch.HoanVi AS HoanViCauHoi,                        -- Cờ hoán vị câu hỏi
		MAX(ctl2.MaCauTraLoi) AS DapAn,						  -- Mã đáp án
        ch.ThuTu AS ThuTuCauHoi,                          -- Thứ tự câu hỏi trong nhóm
        STRING_AGG(CAST(ctl.ThuTu AS VARCHAR), '') AS CauTraLoiKhongHoanVi
    FROM [NhomCauHoi] nch
    LEFT JOIN [CauHoi] ch ON ch.MaNhom = nch.MaNhom
    LEFT JOIN [CauTraLoi] ctl ON ctl.MaCauHoi = ch.MaCauHoi AND ctl.HoanVi = 0
	LEFT JOIN [CauTraLoi] ctl2 ON ctl2.MaCauHoi = ch.MaCauHoi AND ctl2.LaDapAn = 1
    WHERE nch.MaDeThi = @MaDeThi
    GROUP BY nch.MaNhomCha, nch.MaNhom, nch.ThuTu, nch.HoanVi, ch.MaCauHoi, ch.HoanVi, ch.ThuTu
    ORDER BY nch.MaNhomCha, nch.ThuTu, ch.ThuTu;
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_LayMaThongTinDeThiTheoNhom]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_LayMaThongTinDeThiTheoNhom]
@MaDeThi [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
    -- Mã nhóm, mã câu hỏi, danh sách thứ tự câu trả lời không được hoán vị
    SELECT 
        nch.MaNhom, 
		nch.HoanVi,
		nch.ThuTu AS ThuTuNhom,
        ch.MaCauHoi,
		ch.HoanVi,
		ch.ThuTu AS ThuTuCauHoi,
        STRING_AGG(CAST(ctl.ThuTu AS VARCHAR), ',') AS CauTraLoiKhongHoanVi
    FROM [NhomCauHoi] nch
    LEFT JOIN [CauHoi] ch ON ch.MaNhom = nch.MaNhom
    LEFT JOIN [CauTraLoi] ctl ON ctl.MaCauHoi = ch.MaCauHoi AND ctl.HoanVi = 0
    WHERE nch.MaDeThi = @MaDeThi
        AND nch.LaCauHoiNhom = 1 -- chỉ lấy nhóm câu hỏi, không lấy chương
    GROUP BY nch.MaNhom, ch.MaCauHoi, nch.ThuTu, nch.HoanVi, ch.HoanVi, ch.ThuTu
    ORDER BY ThuTuNhom, ThuTuCauHoi
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeCauHoi_SelectBy_DeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_ThongKeCauHoi_SelectBy_DeThi]
    @MaDeThi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT 
		CH.MaCauHoi, 
		CH.MaNhom, 
		CL.MaSoCLO,
		SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS TongSLDung,  -- Trả lời đúng
		COUNT(*) AS TongSLTraLoi,  -- Tổng số lượt trả lời
        ROUND(
			CAST(SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS FLOAT) 
            / NULLIF(COUNT(*), 0), 2) AS PhanTram
    FROM 
        ChiTietBaiThi CTBT
    JOIN 
        CauHoi CH ON CH.MaCauHoi = CTBT.MaCauHoi
    JOIN 
        NhomCauHoi NCH ON NCH.MaNhom = CTBT.MaNhom
	JOIN
		CLO CL ON CH.MaCLO = CL.MaCLO
    WHERE 
        CTBT.MaDeHV IN (
            SELECT DTHV.MaDeHV
            FROM DeThiHoanVi DTHV
            WHERE DTHV.MaDeThi = @MaDeThi
        )
	GROUP BY 
        CH.MaCauHoi, CH.MaNhom, CH.TieuDe, CL.MaSoCLO
	ORDER BY 
		CH.MaCauHoi 
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeCauHoi_SelectBy_Nhom]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_ThongKeCauHoi_SelectBy_Nhom]
    @MaNhom INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT 
		CH.MaCauHoi, CH.TieuDe, CH.MaCLO,
        ROUND(
			CAST(SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS FLOAT) 
            / COUNT(*) , 2) AS PhanTram
    FROM 
        ChiTietBaiThi CTBT
    JOIN 
        CauHoi CH ON CH.MaCauHoi = CTBT.MaCauHoi
    WHERE 
        CTBT.MaNhom = @MaNhom
    GROUP BY 
        CH.MaCauHoi, CH.TieuDe, CH.MaCLO
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeDiem_SelectBy_DeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_ThongKeDiem_SelectBy_DeThi]
    @MaDeThi BIGINT
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT CTCT.Diem AS Diem, COUNT(*) AS SoLuong
	FROM [ChiTietCaThi] CTCT
	JOIN [DeThiHoanVi] DTHV ON CTCT.MaDeThi = DTHV.MaDeHV
	WHERE DTHV.MaDeThi = @MaDeThi AND CTCT.Diem <> -1
	GROUP BY CTCT.Diem
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeNhom_SelectBy_DeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_ThongKeNhom_SelectBy_DeThi]
    @MaDeThi BIGINT
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT 
        NCH.MaNhom, NCH.TenNhom, 
        ROUND(
        CAST(SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*),
        2
    ) AS PhanTram
    FROM 
        ChiTietBaiThi CTBT
    JOIN 
        NhomCauHoi NCH ON NCH.MaNhom = CTBT.MaNhom
    WHERE 
        CTBT.MaDeHV IN (
            SELECT DTHV.MaDeHV
            FROM DeThiHoanVi DTHV
            WHERE DTHV.MaDeThi = @MaDeThi
        )
    GROUP BY 
        NCH.MaNhom, NCH.TenNhom
END
GO
/****** Object:  StoredProcedure [dbo].[CustomThongKeCapBacSV_SelectBy_DeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CustomThongKeCapBacSV_SelectBy_DeThi]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;

  -- 1. Thống kê chung
  SELECT 
    DT.Guid,
    DT.TenDeThi,
    CT.ThoiGianBatDau AS NgayThi,
    COUNT(DISTINCT CTCT.MaChiTietCaThi) AS TongSV
  FROM DeThi DT
  JOIN DeThiHoanVi DTHV ON DT.MaDeThi = DTHV.MaDeThi
  JOIN ChiTietCaThi CTCT ON CTCT.MaDeThi = DTHV.MaDeHV AND CTCT.DaHoanThanh = 1
  JOIN CaThi CT ON CT.MaDeThi = DT.MaDeThi
  WHERE DT.MaDeThi = @MaDeThi
  GROUP BY DT.GhiChu, DT.TenDeThi, CT.ThoiGianBatDau, DT.Guid

  -- 2. CTE điểm và phân loại top/bottom 27%
  ;WITH Scores AS (
    SELECT
      CTCT.MaChiTietCaThi,
      CTCT.MaSinhVien,
      CTCT.Diem,
      PERCENT_RANK() OVER (ORDER BY CTCT.Diem) AS pr_rank
    FROM ChiTietCaThi CTCT
    JOIN DeThiHoanVi DTHV ON CTCT.MaDeThi = DTHV.MaDeHV
    WHERE DTHV.MaDeThi = @MaDeThi
      AND CTCT.DaHoanThanh = 1
  ),
  -- Top và bottom dựa trên tiêu chí 27%
  TopBottom AS (
    SELECT
      MaChiTietCaThi,
      MaSinhVien,
      Diem,
      CASE 
        WHEN pr_rank >= 0.73 THEN 'Top'
        WHEN pr_rank <= 0.27 THEN 'Bottom'
        ELSE NULL
      END AS category
    FROM Scores
  ),
  Questions AS (
    SELECT
      TB.category,
      CH.MaCauHoi,
	  CH.Guid,
      COUNT(DISTINCT CTBT.MaChiTietCaThi) AS SoSVTrong27Percent,
      SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS SoSVDungTrong27Percent
    FROM TopBottom TB
    JOIN ChiTietBaiThi CTBT
      ON TB.MaChiTietCaThi = CTBT.MaChiTietCaThi
	JOIN CauHoi CH
	  ON CH.MaCauHoi = CTBT.MaCauHoi
    WHERE TB.category IS NOT NULL
    GROUP BY TB.category, CH.MaCauHoi, CH.Guid
  ),
  AllStudentsAnswers AS (
    SELECT
      CTBT.MaCauHoi,
	  SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS TongSoSVDung,
	  COUNT(DISTINCT CTCT.MaSinhVien) AS TongSoSVTraLoi
    FROM ChiTietBaiThi CTBT
    JOIN ChiTietCaThi CTCT ON CTBT.MaChiTietCaThi = CTCT.MaChiTietCaThi
    JOIN DeThiHoanVi DTHV ON CTCT.MaDeThi = DTHV.MaDeHV
    WHERE CTCT.DaHoanThanh = 1
      AND DTHV.MaDeThi = @MaDeThi
    GROUP BY CTBT.MaCauHoi
  )

  -- 3. Kết hợp và hiển thị kết quả
  SELECT 
    Q.MaCauHoi,
	Q.Guid,
  
  -- Top group
  SUM(CASE WHEN Q.category = 'Top' THEN Q.SoSVDungTrong27Percent ELSE 0 END) AS SVTopDung,
  SUM(CASE WHEN Q.category = 'Top' THEN Q.SoSVTrong27Percent ELSE 0 END) AS SVTop,
  
  -- Bottom group
  SUM(CASE WHEN Q.category = 'Bottom' THEN Q.SoSVDungTrong27Percent ELSE 0 END) AS SVBottomDung,
  SUM(CASE WHEN Q.category = 'Bottom' THEN Q.SoSVTrong27Percent ELSE 0 END) AS SVBottom,

  -- Tổng toàn bộ
    ISNULL(ASCS.TongSoSVDung, 0) AS TongSoSVDung,
  ISNULL(ASCS.TongSoSVTraLoi, 0) AS TongSoSVTraLoi
  FROM Questions Q
  LEFT JOIN AllStudentsAnswers ASCS
    ON Q.MaCauHoi = ASCS.MaCauHoi
  GROUP BY Q.MaCauHoi, ASCS.TongSoSVTraLoi, ASCS.TongSoSVDung, Q.Guid
  ORDER BY Q.MaCauHoi;
END
GO
/****** Object:  StoredProcedure [dbo].[DeThi_DeleteAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_DeleteAll]
WITH EXECUTE AS CALLER
AS
DELETE FROM ChiTietDeThiHoanVi

DELETE FROM NhomCauHoiHoanVi

DELETE FROM DeThiHoanVi

DELETE FROM ChiTietDeThi

DELETE FROM NhomCauHoi

DELETE FROM	CauTraLoi

DELETE FROM	CauHoi

DELETE FROM [DeThi]
GO
/****** Object:  StoredProcedure [dbo].[DeThi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_Insert]
	@MaMonHoc [int],
	@TenDeThi [nvarchar](250),
	@KyHieuDe [varchar](50),
	@NgayTao [datetime]
WITH EXECUTE AS CALLER
AS
IF EXISTS (
		SELECT	* 
		FROM	[DeThi] 
		WHERE	[TenDeThi] = @TenDeThi
			and [MaMonHoc] = @MaMonHoc
	)
		set @TenDeThi = @TenDeThi + '_01'

	INSERT INTO [DeThi] 
	(
		[MaMonHoc],
		[TenDeThi],
		[KyHieuDe],
		[NgayTao]
	) 
	VALUES 
	(
		@MaMonHoc,
		@TenDeThi,
		@KyHieuDe,
		@NgayTao
	)

	SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[DeThi_SelectAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[DeThi]



GO
/****** Object:  StoredProcedure [dbo].[DeThi_SelectBy_ma_de_hv]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_SelectBy_ma_de_hv]
	@MaDeHV [bigint]
WITH EXECUTE AS CALLER
AS
SELECT b.*
FROM [DeThiHoanVi] a, [DeThi] b
WHERE a.MaDeHV = @MaDeHV AND a.MaDeThi = b.MaDeThi
GO
/****** Object:  StoredProcedure [dbo].[DeThi_SelectByMonHoc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_SelectByMonHoc]
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
SELECT	*
FROM	[DeThi]
WHERE	[MaMonHoc] = @MaMonHoc


GO
/****** Object:  StoredProcedure [dbo].[DeThi_SelectByMonHoc_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_SelectByMonHoc_Paged]
	@MaMonHoc [int],
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [DeThi] DT
	WHERE DT.MaMonHoc = @MaMonHoc

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [DeThi] DT
	WHERE DT.MaMonHoc = @MaMonHoc
    ORDER BY DT.NgayTao DESC -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_DapAn]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_DapAn]
	@MaDeHV [bigint]
WITH EXECUTE AS CALLER
AS
	SELECT ctl.MaCauHoi, ctl.MaCauTraLoi
	FROM [CauTraLoi] ctl
	JOIN [CauHoi] ch ON ch.MaCauHoi = ctl.MaCauHoi
	JOIN [ChiTietDeThiHoanVi] ctdthv ON ctdthv.MaCauHoi = ctl.MaCauHoi
	WHERE ctdthv.MaDeHV = @MaDeHV AND ctl.LaDapAn = 1
	ORDER BY ctdthv.ThuTu
GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_Delete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_Delete]
	@MaDeHV [bigint]
WITH EXECUTE AS CALLER
AS
DELETE FROM [DeThiHoanVi]
WHERE
	[MaDeHV] = @MaDeHV


GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_ForceDelete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_ForceDelete]
	@MaDeHV [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			DELETE ChiTietDeThiHoanVi
			WHERE MaDeHV = @MaDeHV

			DELETE NhomCauHoiHoanVi
			WHERE MaDeHV = @MaDeHV

			DELETE DeThiHoanVi
			WHERE MaDeHV = @MaDeHV
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_Insert]
	@MaDeThi [int],
	@KyHieuDe [nvarchar](50),
	@NgayTao [datetime],
	@Guid [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [DeThiHoanVi] (
	[MaDeThi],
	[KyHieuDe],
	[NgayTao],
	[Guid]
) VALUES (
	@MaDeThi,
	@KyHieuDe,
	@NgayTao,
	@Guid
)

SELECT @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[DeThiHoanVi]



GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectBy_MaDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_SelectBy_MaDeThi]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[DeThiHoanVi]
WHERE
	[MaDeThi] = @MaDeThi

GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_SelectOne]
	@MaDeHV [bigint]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[DeThiHoanVi]
WHERE
	[MaDeHV] = @MaDeHV



GO
/****** Object:  StoredProcedure [dbo].[DotThi_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_ForceRemove]
    @MaDotThi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION

        -- Bảng tạm lưu ID liên quan
        DECLARE @ChiTietDotThi TABLE (MaChiTietDotThi INT)
        DECLARE @CaThi TABLE (MaCaThi INT)
        DECLARE @ChiTietCaThi TABLE (MaChiTietCaThi INT)

        -- Lấy dữ liệu vào bảng tạm
        INSERT INTO @ChiTietDotThi (MaChiTietDotThi)
        SELECT MaChiTietDotThi FROM ChiTietDotThi WHERE MaDotThi = @MaDotThi

        INSERT INTO @CaThi (MaCaThi)
        SELECT MaCaThi FROM CaThi WHERE MaChiTietDotThi IN (SELECT MaChiTietDotThi FROM @ChiTietDotThi)

        INSERT INTO @ChiTietCaThi (MaChiTietCaThi)
        SELECT MaChiTietCaThi FROM ChiTietCaThi WHERE MaCaThi IN (SELECT MaCaThi FROM @CaThi)

        -- Xoá theo thứ tự phụ thuộc
        DELETE FROM ChiTietBaiThi
        WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi)

		DELETE FROM Audio
        WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi)

        DELETE FROM ChiTietCaThi
        WHERE MaCaThi IN (SELECT MaCaThi FROM @CaThi)

        DELETE FROM CaThi
        WHERE MaCaThi IN (SELECT MaCaThi FROM @CaThi)

        DELETE FROM ChiTietDotThi
        WHERE MaChiTietDotThi IN (SELECT MaChiTietDotThi FROM @ChiTietDotThi)

        DELETE FROM DotThi
        WHERE MaDotThi = @MaDotThi

        COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DotThi_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[DotThi]

GO
/****** Object:  StoredProcedure [dbo].[DotThi_GetAll_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_GetAll_Paged]
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [DotThi] DT

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [DotThi] DT
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), ThoiGianBatDau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[DotThi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_Insert]
	@TenDotThi [nvarchar](150),
	@ThoiGianBatDau [datetime],
	@ThoiGianKetThuc [datetime],
	@NamHoc [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [DotThi] (
	[TenDotThi],
	[ThoiGianBatDau],
	[ThoiGianKetThuc],
	[NamHoc]
) VALUES (
	@TenDotThi,
	@ThoiGianBatDau,
	@ThoiGianKetThuc,
	@NamHoc 
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[DotThi_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_Remove]
    @MaDotThi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [DotThi]
	WHERE MaDotThi = @MaDotThi
END
GO
/****** Object:  StoredProcedure [dbo].[DotThi_SelectByMaNamHoc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_SelectByMaNamHoc]
	@NamHoc [int]
WITH EXECUTE AS CALLER
AS
SELECT	*
FROM
	[DotThi]
WHERE
	[NamHoc] = @NamHoc 
	 

GO
/****** Object:  StoredProcedure [dbo].[DotThi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_SelectOne]
	@MaDotThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	*
FROM
	[DotThi]
WHERE
	[MaDotThi] = @MaDotThi

GO
/****** Object:  StoredProcedure [dbo].[DotThi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DotThi_Update]
	@MaDotThi [int],
	@TenDotThi [nvarchar](150),
	@ThoiGianBatDau [datetime],
	@ThoiGianKetThuc [datetime],
	@NamHoc [int]
WITH EXECUTE AS CALLER
AS
UPDATE [DotThi] SET
	[TenDotThi] = @TenDotThi,
	[ThoiGianBatDau] = @ThoiGianBatDau,
	[ThoiGianKetThuc] = @ThoiGianKetThuc,
	[NamHoc] = @NamHoc 
WHERE
	[MaDotThi] = @MaDotThi

GO
/****** Object:  StoredProcedure [dbo].[Khoa_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_ForceRemove]
	@MaKhoa INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION

		-- Tạo bảng tạm để lưu các ID trung gian
		DECLARE @LopCuaKhoa TABLE (MaLop INT)
		DECLARE @SinhVienTrongLop TABLE (MaSinhVien INT)
		DECLARE @ChiTietCaThi TABLE (MaChiTietCaThi INT)

		-- Lấy dữ liệu vào bảng tạm
		INSERT INTO @LopCuaKhoa (MaLop)
		SELECT MaLop FROM Lop WHERE MaKhoa = @MaKhoa

		INSERT INTO @SinhVienTrongLop (MaSinhVien)
		SELECT MaSinhVien FROM SinhVien WHERE MaLop IN (SELECT MaLop FROM @LopCuaKhoa)

		INSERT INTO @ChiTietCaThi (MaChiTietCaThi)
		SELECT MaChiTietCaThi FROM ChiTietCaThi WHERE MaSinhVien IN (SELECT MaSinhVien FROM @SinhVienTrongLop)

		-- Thực hiện xóa theo thứ tự phụ thuộc
		DELETE FROM ChiTietBaiThi
		WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi)

		DELETE FROM ChiTietCaThi
		WHERE MaSinhVien IN (SELECT MaSinhVien FROM @SinhVienTrongLop)

		DELETE FROM SinhVien
		WHERE MaSinhVien IN (SELECT MaSinhVien FROM @SinhVienTrongLop)

		DELETE FROM Lop
		WHERE MaLop IN (SELECT MaLop FROM @LopCuaKhoa)

		DELETE FROM Khoa
		WHERE MaKhoa = @MaKhoa

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[Khoa]

GO
/****** Object:  StoredProcedure [dbo].[Khoa_GetAll_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_GetAll_Paged]
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [Khoa] Khoa

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [Khoa] Khoa
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), NgayThanhLap)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_Insert]
	@TenKhoa [nvarchar](30),
	@NgayThanhLap [datetime]
WITH EXECUTE AS CALLER
AS
INSERT INTO [Khoa] (
	[TenKhoa],
	[NgayThanhLap]
) VALUES (
	@TenKhoa,
	@NgayThanhLap
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[Khoa_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_Remove]
	@MaKhoa INT
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [Khoa]
	WHERE MaKhoa = @MaKhoa
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_SelectOne]
	@MaKhoa [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[MaKhoa],
	[TenKhoa],
	[NgayThanhLap]
FROM
	[Khoa]
WHERE
	[MaKhoa] = @MaKhoa

GO
/****** Object:  StoredProcedure [dbo].[Khoa_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Khoa_Update]
	@MaKhoa [int],
	@TenKhoa [nvarchar](30),
	@NgayThanhLap [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [Khoa] SET
	[TenKhoa] = @TenKhoa,
	[NgayThanhLap] = @NgayThanhLap
WHERE
	[MaKhoa] = @MaKhoa

GO
/****** Object:  StoredProcedure [dbo].[Lop_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_ForceRemove]
	@MaLop [int]
WITH EXECUTE AS CALLER
AS
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
	BEGIN TRANSACTION

	-- Tạo bảng tạm để lưu các ID trung gian
	DECLARE @SinhVienTrongLop TABLE (MaSinhVien INT)
	DECLARE @ChiTietCaThi TABLE (MaChiTietCaThi INT)

	-- Lấy dữ liệu vào bảng tạm
	INSERT INTO @SinhVienTrongLop (MaSinhVien)
	SELECT MaSinhVien FROM SinhVien WHERE MaLop = @MaLop

	INSERT INTO @ChiTietCaThi (MaChiTietCaThi)
	SELECT MaChiTietCaThi FROM ChiTietCaThi WHERE MaSinhVien IN (SELECT MaSinhVien FROM @SinhVienTrongLop)

	-- Thực hiện xóa theo thứ tự phụ thuộc
	DELETE FROM ChiTietBaiThi
	WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi)

	DELETE FROM ChiTietCaThi
	WHERE MaSinhVien IN (SELECT MaSinhVien FROM @SinhVienTrongLop)

	DELETE FROM SinhVien
	WHERE MaSinhVien IN (SELECT MaSinhVien FROM @SinhVienTrongLop)

	DELETE FROM Lop
	WHERE MaLop = @MaLop

	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Lop_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[Lop] L LEFT JOIN [Khoa] K ON L.MaKhoa = K.MaKhoa

GO
/****** Object:  StoredProcedure [dbo].[Lop_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_Insert]
	@TenLop [nvarchar](50),
	@NgayBatDau [datetime],
	@MaKhoa [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [Lop] (
	[TenLop],
	[NgayBatDau],
	[MaKhoa]
) VALUES (
	@TenLop,
	@NgayBatDau,
	@MaKhoa
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[Lop_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_Remove]
	@MaLop [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [Lop]
	WHERE MaLop = @MaLop
END
GO
/****** Object:  StoredProcedure [dbo].[Lop_SelectBy_MaKhoa]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_SelectBy_MaKhoa]
	@MaKhoa [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM Lop
WHERE MaKhoa = @MaKhoa

GO
/****** Object:  StoredProcedure [dbo].[Lop_SelectBy_MaKhoa_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_SelectBy_MaKhoa_Paged]
	@MaKhoa [int],
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [Lop] Lop
	INNER JOIN [Khoa] ON Lop.MaKhoa = Khoa.MaKhoa
	WHERE Khoa.MaKhoa = @MaKhoa

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT Lop.*
    FROM [Lop] Lop
	INNER JOIN [Khoa] ON Lop.MaKhoa = Khoa.MaKhoa
	WHERE Khoa.MaKhoa = @MaKhoa
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), Lop.NgayBatDau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[Lop_SelectBy_TenLop]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_SelectBy_TenLop]
	@TenLop [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM Lop
WHERE TenLop = @TenLop

GO
/****** Object:  StoredProcedure [dbo].[Lop_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_SelectOne]
	@MaLop [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[MaLop],
	[TenLop],
	[NgayBatDau],
	[MaKhoa]
FROM
	[Lop]
WHERE
	[MaLop] = @MaLop

GO
/****** Object:  StoredProcedure [dbo].[Lop_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lop_Update]
	@MaLop [int],
	@TenLop [nvarchar](50),
	@NgayBatDau [datetime],
	@MaKhoa [int]
WITH EXECUTE AS CALLER
AS
UPDATE [Lop] SET
	[TenLop] = @TenLop,
	[NgayBatDau] = @NgayBatDau,
	[MaKhoa] = @MaKhoa
WHERE
	[MaLop] = @MaLop

GO
/****** Object:  StoredProcedure [dbo].[LopAo_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_ForceRemove]
	@MaLopAo INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Bảng biến lưu các khóa liên quan
		DECLARE @ChiTietDotThi TABLE (MaChiTietDotThi INT);
		DECLARE @CaThi TABLE (MaCaThi INT);
		DECLARE @ChiTietCaThi TABLE (MaChiTietCaThi INT);

		-- Lấy danh sách cần xóa theo quan hệ
		INSERT INTO @ChiTietDotThi (MaChiTietDotThi)
		SELECT MaChiTietDotThi
		FROM ChiTietDotThi
		WHERE MaLopAo = @MaLopAo;

		INSERT INTO @CaThi (MaCaThi)
		SELECT MaCaThi
		FROM CaThi
		WHERE MaChiTietDotThi IN (SELECT MaChiTietDotThi FROM @ChiTietDotThi);

		INSERT INTO @ChiTietCaThi (MaChiTietCaThi)
		SELECT MaChiTietCaThi
		FROM ChiTietCaThi
		WHERE MaCaThi IN (SELECT MaCaThi FROM @CaThi);

		-- Xóa theo thứ tự phụ thuộc
		DELETE FROM ChiTietBaiThi
		WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi);

		DELETE FROM ChiTietCaThi
		WHERE MaCaThi IN (SELECT MaCaThi FROM @CaThi);

		DELETE FROM CaThi
		WHERE MaChiTietDotThi IN (SELECT MaChiTietDotThi FROM @ChiTietDotThi);

		DELETE FROM ChiTietDotThi
		WHERE MaLopAo = @MaLopAo;

		-- Cuối cùng xóa lớp ảo
		DELETE FROM LopAo
		WHERE MaLopAo = @MaLopAo;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[LopAo_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[LopAo] LA LEFT JOIN [MonHoc] MH ON LA.MaMonHoc=MH.MaMonHoc


GO
/****** Object:  StoredProcedure [dbo].[LopAo_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_Insert]
	@TenLopAo [nvarchar](200),
	@NgayBatDau [datetime],
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [LopAo] (
	[TenLopAo],
	[NgayBatDau],
	[MaMonHoc]
) VALUES (
	@TenLopAo,
	@NgayBatDau,
	@MaMonHoc
)

SELECT @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[LopAo_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_Remove]
	@MaLopAo [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [LopAo]
WHERE
	[MaLopAo] = @MaLopAo

GO
/****** Object:  StoredProcedure [dbo].[LopAo_SelectBy_MaMonHoc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_SelectBy_MaMonHoc]
	@MaMonHoc [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM LopAo
WHERE MaMonHoc = @MaMonHoc

GO
/****** Object:  StoredProcedure [dbo].[LopAo_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_SelectOne]
	@MaLopAo [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[MaLopAo],
	[TenLopAo],
	[NgayBatDau],
	[MaMonHoc]
FROM
	[LopAo]
WHERE
	[MaLopAo] = @MaLopAo


GO
/****** Object:  StoredProcedure [dbo].[LopAo_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LopAo_Update]
	@MaLopAo [int],
	@TenLopAo [nvarchar](200),
	@NgayBatDau [datetime],
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
UPDATE [LopAo] SET
	[TenLopAo] = @TenLopAo,
	[NgayBatDau] = @NgayBatDau,
	[MaMonHoc] = @MaMonHoc
WHERE
	[MaLopAo] = @MaLopAo
 

GO
/****** Object:  StoredProcedure [dbo].[MonHoc_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[MonHoc]

GO
/****** Object:  StoredProcedure [dbo].[MonHoc_GetAll_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_GetAll_Paged]
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [MonHoc]

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [MonHoc] MH
    ORDER BY MH.MaMonHoc DESC -- gần ngày tạo nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[MonHoc_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_Insert]
	@MaSoMonHoc [nvarchar](50),
	@TenMonHoc [nvarchar](200)
WITH EXECUTE AS CALLER
AS
INSERT INTO [MonHoc] (
	[MaSoMonHoc],
	[TenMonHoc]
) VALUES (
	@MaSoMonHoc,
	@TenMonHoc
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[MonHoc_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_Remove]
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [MonHoc]
WHERE
	[MaMonHoc] = @MaMonHoc

GO
/****** Object:  StoredProcedure [dbo].[MonHoc_SelectBy_MaSoMonHoc]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_SelectBy_MaSoMonHoc]
	@MaSoMonHoc [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SELECT
	[MaMonHoc],
	[MaSoMonHoc],
	[TenMonHoc]
FROM
	[MonHoc]
WHERE
	lower([MaSoMonHoc]) = lower(@MaSoMonHoc)

GO
/****** Object:  StoredProcedure [dbo].[MonHoc_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_SelectOne]
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[MaMonHoc],
	[MaSoMonHoc],
	[TenMonHoc]
FROM
	[MonHoc]
WHERE
	[MaMonHoc] = @MaMonHoc

GO
/****** Object:  StoredProcedure [dbo].[MonHoc_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MonHoc_Update]
	@MaMonHoc [int],
	@MaSoMonHoc [nvarchar](50),
	@TenMonHoc [nvarchar](200)
WITH EXECUTE AS CALLER
AS
UPDATE [MonHoc] SET
	[MaSoMonHoc] = @MaSoMonHoc,
	[TenMonHoc] = @TenMonHoc
WHERE
	[MaMonHoc] = @MaMonHoc

GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_CheckTen]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_CheckTen]
	@TenDangNhap NVARCHAR(50),
    @Email NVARCHAR(50)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM [NguoiDung]
        WHERE 
            (@TenDangNhap IS NOT NULL AND TenDangNhap = @TenDangNhap)
            OR
            (@Email IS NOT NULL AND Email = @Email)
    )
        RETURN 1; -- Có trùng

    RETURN 0; -- Không trùng
END
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_Delete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_Delete]
	@MaNguoiDung [uniqueidentifier]
WITH EXECUTE AS CALLER
AS

UPDATE [NguoiDung] 
SET
	[DaXoa] = 1
WHERE
	[MaNguoiDung] = @MaNguoiDung
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_GetAll_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_GetAll_Paged]
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [NguoiDung] U
	JOIN [VaiTro] R ON U.MaVaiTro = R.MaVaiTro

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [NguoiDung] U
	JOIN [VaiTro] R ON U.MaVaiTro = R.MaVaiTro
    ORDER BY NgayTao -- thời gian tạo
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_GetAll_Search_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_GetAll_Search_Paged]
	@Keyword NVARCHAR(100),
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [NguoiDung] U
	JOIN [VaiTro] R ON U.MaVaiTro = R.MaVaiTro
	WHERE U.Ten LIKE '%' + @Keyword + '%' OR U.Email LIKE '%' + @Keyword + '%' 

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [NguoiDung] U
	JOIN [VaiTro] R ON U.MaVaiTro = R.MaVaiTro
	WHERE U.Ten LIKE '%' + @Keyword + '%' OR U.Email LIKE '%' + @Keyword + '%'
    ORDER BY NgayTao -- thời gian tạo
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_Insert]
	@MaVaiTro [int],
	@TenDangNhap [nvarchar](100),
	@Email [nvarchar](100),
	@Ten [nvarchar](255),
	@MatKhau [nvarchar](128),
	@NgayTao [datetime],
	@GhiChu [nvarchar](255)
WITH EXECUTE AS CALLER
AS
DECLARE @MaNguoiDung uniqueidentifier = NEWID();
INSERT INTO [NguoiDung] (
	[MaNguoiDung],
	[MaVaiTro],
	[TenDangNhap],
	[Email],
	[Ten],
	[MatKhau],
	[NgayTao],
	[GhiChu],
	[LaNguoiDungHeThong]
) 
VALUES 
(
	@MaNguoiDung,
	@MaVaiTro,
	@TenDangNhap,
	@Email,
	@Ten,
	@MatKhau,
	@NgayTao,
	@GhiChu,
	0
)
SELECT @MaNguoiDung
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_Login]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_Login]
	@TenDangNhap [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT [MaNguoiDung], [Ten], [MatKhau], r.TenVaiTro
	FROM [NguoiDung] u, [VaiTro] r
	WHERE [TenDangNhap] = @TenDangNhap AND u.[MaVaiTro] = r.[MaVaiTro]

	UPDATE [NguoiDung]
	SET [ThoiGianDangNhap] = GETDATE()
	WHERE [TenDangNhap] = @TenDangNhap OR [Email] = @TenDangNhap
END
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_LoginFail]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_LoginFail]
	@MaNguoiDung [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
IF EXISTS(
	SELECT * FROM [dbo].[NguoiDung]
	WHERE [MaNguoiDung] = @MaNguoiDung AND [SoLanDangNhapSai] = 10
	)
	-- Nhap sai tren 10 lan bi xoa tai Khoan tam thoi
	BEGIN
		UPDATE [NguoiDung]
		SET [DaKhoa] = 1, [ThoiGianKhoa] = GETDATE()
		WHERE [MaNguoiDung] = @MaNguoiDung
	END
ELSE
	BEGIN
	UPDATE [NguoiDung]
	SET [ThoiGianDangNhapSai] = CURRENT_TIMESTAMP,
		[SoLanDangNhapSai] = [SoLanDangNhapSai] + 1
	WHERE [MaNguoiDung] = @MaNguoiDung
	END
END
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_LoginSuccess]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_LoginSuccess]
	@MaNguoiDung [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	UPDATE [NguoiDung]
	SET [ThoiGianDangNhap] = CURRENT_TIMESTAMP, [SoLanDangNhapSai] = 0
	WHERE [MaNguoiDung] = @MaNguoiDung
END
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_SelectAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT	u.*

FROM	[NguoiDung] u

ORDER BY u.[LaNguoiDungHeThong] DESC, u.[TenDangNhap], u.[Ten]




GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_SelectByTenDangNhap]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_SelectByTenDangNhap]
	@TenDangNhap [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SELECT	u.*

FROM	[NguoiDung] u

WHERE	u.[TenDangNhap] = @TenDangNhap




GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_SelectOne]
	@MaNguoiDung [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[NguoiDung]
WHERE	[MaNguoiDung] = @MaNguoiDung





GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_Update]
	@MaNguoiDung [uniqueidentifier],
	@MaVaiTro [int],
	@TenDangNhap [nvarchar](100),
	@Email [nvarchar](100),
	@Ten [nvarchar](255),
	@DaXoa [bit],
	@DaKhoa [bit],
	@GhiChu [nvarchar](255)
WITH EXECUTE AS CALLER
AS
UPDATE [NguoiDung] 
SET
	[TenDangNhap] = CASE WHEN @TenDangNhap IS NULL THEN [TenDangNhap] ELSE @TenDangNhap END,
	[MaVaiTro] = CASE WHEN @MaVaiTro IS NULL THEN [MaVaiTro] ELSE @MaVaiTro END,
	[Email] = CASE WHEN @Email IS NULL THEN [Email] ELSE @Email END,
	[Ten] = CASE WHEN @Ten IS NULL THEN [Ten] ELSE @Ten END,
	[DaXoa] = CASE WHEN @DaXoa IS NULL THEN [DaXoa] ELSE @DaXoa END,
	[DaKhoa] = CASE WHEN @DaKhoa IS NULL THEN [DaKhoa] ELSE @DaKhoa END,
	[GhiChu] = CASE WHEN @GhiChu IS NULL THEN [GhiChu] ELSE @GhiChu END
WHERE
	[MaNguoiDung] = @MaNguoiDung
GO
/****** Object:  StoredProcedure [dbo].[NguoiDung_UpdateMatKhau]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NguoiDung_UpdateMatKhau]
	@MaNguoiDung [uniqueidentifier],
	@MatKhau [nvarchar](128),
	@ThoiGianDoiMatKhau DATETIME
WITH EXECUTE AS CALLER
AS
UPDATE [NguoiDung] 
SET
	[MatKhau] = @MatKhau,
	[ThoiGianDoiMatKhau] = @ThoiGianDoiMatKhau
WHERE
	[MaNguoiDung] = @MaNguoiDung
 
GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Delete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_Delete]
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS

DECLARE @MaDeThi BIGINT, @ThuTu INT

    SELECT @MaDeThi = MaDeThi, @ThuTu = ThuTu
    FROM [NhomCauHoi]
    WHERE MaNhom = @MaNhom

DELETE FROM NhomCauHoi
WHERE MaNhom = @MaNhom

--cập nhật thứ nhóm câu hỏi
    UPDATE [NhomCauHoi]
    SET [ThuTu] = [ThuTu] - 1
    WHERE [MaDeThi] = @MaDeThi AND [ThuTu] > @ThuTu
GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_ForceDelete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_ForceDelete]
	@ma_nhom INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Bảng biến lưu các ID liên quan
		DECLARE @CauHoi TABLE (ma_cau_hoi INT);

		-- Lấy danh sách câu hỏi thuộc nhóm
		INSERT INTO @CauHoi (ma_cau_hoi)
		SELECT MaCauHoi
		FROM ChiTietDeThi
		WHERE MaNhom = @ma_nhom;

		-- Xóa theo thứ tự phụ thuộc
		DELETE FROM CauTraLoi
		WHERE MaCauHoi IN (SELECT ma_cau_hoi FROM @CauHoi);

		DELETE FROM CauHoi
		WHERE MaCauHoi IN (SELECT ma_cau_hoi FROM @CauHoi);

		DELETE FROM ChiTietDeThi
		WHERE MaNhom = @ma_nhom;

		--Xóa nhóm câu hỏi trong nhóm câu hỏi hoán vị
		DELETE FROM [NhomCauHoiHoanVi]
		WHERE
		[MaNhom] = @ma_nhom

		DECLARE @MaDeThi BIGINT, @ThuTu INT

		SELECT @MaDeThi = MaDeThi, @ThuTu = ThuTu
		FROM [NhomCauHoi]
		WHERE MaNhom = @ma_nhom

		DELETE FROM NhomCauHoi
		WHERE MaNhom = @ma_nhom;

		--cập nhật thứ nhóm câu hỏi
		UPDATE [NhomCauHoi]
		SET [ThuTu] = [ThuTu] - 1
		WHERE [MaDeThi] = @MaDeThi AND [ThuTu] > @ThuTu

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_Insert]
	@MaDeThi [int],
	@TenNhom [nvarchar](250),
	@KieuNoiDung [int],
	@NoiDung [nvarchar](max),
	@SoCauHoi [int],
	@HoanVi [bit],
	@ThuTu [int],
	@MaNhomCha [int],
	@SoCauLay [int],
	@LaCauHoiNhom [bit]
WITH EXECUTE AS CALLER
AS
INSERT INTO [NhomCauHoi] (
	[MaDeThi],
	[TenNhom],
	[KieuNoiDung],
	[NoiDung],
	[SoCauHoi],
	[HoanVi],
	[ThuTu],
	[MaNhomCha],
	[SoCauLay],
	[LaCauHoiNhom]
) 
VALUES (
	@MaDeThi,
	@TenNhom,
	@KieuNoiDung,
	@NoiDung,
	@SoCauHoi,
	@HoanVi,
	@ThuTu,
	@MaNhomCha,
	@SoCauLay,
	@LaCauHoiNhom
)

-- 1. Cập nhật tất cả các nhóm có thứ tự >= @ThuTu tăng thêm 1
    UPDATE [NhomCauHoi]
    SET [ThuTu] = [ThuTu] + 1
    WHERE [MaDeThi] = @MaDeThi AND [ThuTu] >= @ThuTu

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_IsCauHoiNhom]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_IsCauHoiNhom]
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS
IF EXISTS (
	SELECT 	*
	FROM	[NhomCauHoi]
	WHERE	[MaNhom] = @MaNhom 
		AND [LaCauHoiNhom] = 1)
	RETURN 1
ELSE RETURN 0



GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[NhomCauHoi]



GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectAllBy_MaDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_SelectAllBy_MaDeThi]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	NhomCauHoi
WHERE	MaDeThi = @MaDeThi
ORDER BY	ThuTu

GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectBy_MaDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_SelectBy_MaDeThi]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	NhomCauHoi

WHERE	MaDeThi = @MaDeThi
	AND	(MaNhomCha = -1 OR MaNhomCha IS NULL)

ORDER BY ThuTu

GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectBy_MaNhomCha]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_SelectBy_MaNhomCha]
	@MaNhomCha [int]
WITH EXECUTE AS CALLER
AS
SELECT	* 

FROM	NhomCauHoi

WHERE	MaNhomCha = @MaNhomCha
	AND	(MaNhomCha <> -1 AND MaNhomCha IS NOT NULL)

ORDER BY ThuTu

GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectHoanViBy_MaDeThi]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_SelectHoanViBy_MaDeThi]
	@MaDeThi [int],
	@HoanVi [bit]
WITH EXECUTE AS CALLER
AS
SELECT	* 
	FROM	NhomCauHoi
	WHERE	MaDeThi	= @MaDeThi
		AND	HoanVi	= @HoanVi
	ORDER BY	ThuTu

GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_SelectOne]
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[NhomCauHoi]
WHERE
	[MaNhom] = @MaNhom



GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_Update]
	@MaNhom [int],
	@MaDeThi [int],
	@TenNhom [nvarchar](250),
	@KieuNoiDung [int],
	@NoiDung [nvarchar](max),
	@SoCauHoi [int],
	@HoanVi [bit],
	@ThuTu [int],
	@MaNhomCha [int]
WITH EXECUTE AS CALLER
AS
UPDATE [NhomCauHoi] 
SET
	[MaDeThi] = @MaDeThi,
	[TenNhom] = @TenNhom,
	[KieuNoiDung] = @KieuNoiDung,
	[NoiDung] = @NoiDung,
	[SoCauHoi] = @SoCauHoi,
	[HoanVi] = @HoanVi,
	[ThuTu] = @ThuTu,
	[MaNhomCha] = @MaNhomCha
WHERE
	[MaNhom] = @MaNhom
 
GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_CountBy_MaNhomCha]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_CountBy_MaNhomCha]
	@MaDeHV [int],
	@MaNhomCha [int]
WITH EXECUTE AS CALLER
AS
SELECT	COUNT(*)

FROM NhomCauHoiHoanVi nhv

LEFT OUTER JOIN NhomCauHoi n
on n.MaNhom = nhv.MaNhom

WHERE	nhv.MaDeHV = @MaDeHV
	AND	n.MaNhomCha = @MaNhomCha

		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Delete]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_Delete]
	@MaDeHV [bigint],
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [NhomCauHoiHoanVi]
WHERE
	[MaDeHV] = @MaDeHV
	AND [MaNhom] = @MaNhom


GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_Insert]
	@MaDeHV [bigint],
	@MaNhom [int],
	@ThuTu [int]
WITH EXECUTE AS CALLER
AS
-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [NhomCauHoiHoanVi] (
	[MaDeHV],
	[MaNhom],
	[ThuTu]
) VALUES (
	@MaDeHV,
	@MaNhom,
	@ThuTu
)
SELECT @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[NhomCauHoiHoanVi]



GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaDeHV]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_SelectBy_MaDeHV]
	@MaDeHV [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	nhv.* , 
		n.TenNhom, 
		n.NoiDung,
		n.LaCauHoiNhom

FROM NhomCauHoiHoanVi nhv

LEFT OUTER JOIN NhomCauHoi n
on n.MaNhom = nhv.MaNhom

WHERE MaDeHV = @MaDeHV
--and (MaNhomCha = -1 OR MaNhomCha IS NULL)

ORDER BY ThuTu	
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhom]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhom]
	@MaNhom [int] = NULL
WITH EXECUTE AS CALLER
AS
create TABLE #PageIndex
(
	STT int IDENTITY (1, 1) NOT NULL,
	MaNhom Int
--	Version  Int,
)

BEGIN

INSERT INTO #PageIndex (
	MaNhom
)

SELECT MaNhom FROM NhomCauHoiHoanVi
WHERE MaNhom = @MaNhom

END

SELECT	n.* ,
		p.STT

FROM	NhomCauHoiHoanVi n

INNER JOIN #PageIndex p
ON	p.MaNhom = n.MaNhom

WHERE	n.MaNhom = @MaNhom


DROP TABLE #PageIndex

GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhomCha]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhomCha]
	@MaDeHV [int],
	@MaNhomCha [int]
WITH EXECUTE AS CALLER
AS
SELECT	nhv.* , 
		n.TenNhom, 
		n.NoiDung,
		n.LaCauHoiNhom

FROM NhomCauHoiHoanVi nhv

LEFT OUTER JOIN NhomCauHoi n
on n.MaNhom = nhv.MaNhom

WHERE	nhv.MaDeHV = @MaDeHV
	AND	n.MaNhomCha = @MaNhomCha

ORDER BY nhv.ThuTu	
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_SelectOne]
	@MaDeHV [bigint],
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[NhomCauHoiHoanVi]
WHERE
	[MaDeHV] = @MaDeHV
	AND [MaNhom] = @MaNhom



GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectPageBy_MaNhomCha]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_SelectPageBy_MaNhomCha]
	@MaDeHV [int],
	@MaNhomCha [int],
	@PageNumber [int],
	@PageSize [int]
WITH EXECUTE AS CALLER
AS
--SELECT	nhv.* , 
--		n.TenNhom, 
--		n.NoiDung
--
--FROM NhomCauHoiHoanVi nhv
--
--LEFT OUTER JOIN NhomCauHoi n
--on n.MaNhom = nhv.MaNhom
--
--WHERE	nhv.MaDeHV = @MaDeHV
--	AND	n.MaNhomCha = @MaNhomCha
--
--ORDER BY nhv.ThuTu	
			
	DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int

	SET @PageLowerBound = (@PageSize * @PageNumber) - @PageSize
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1

	create TABLE #PageIndex
	(
		IndexID int IDENTITY (1, 1) NOT NULL,
		MaDeHV	bigint,
		MaNhom	int
	)

	BEGIN

	INSERT INTO #PageIndex 
	(
		MaDeHV,
		MaNhom
	)

	SELECT	nhv.MaDeHV, nhv.MaNhom

	FROM	NhomCauHoiHoanVi nhv

	LEFT OUTER JOIN NhomCauHoi n
		on	n.MaNhom = nhv.MaNhom

	WHERE	nhv.MaDeHV = @MaDeHV
		AND	n.MaNhomCha = @MaNhomCha

	ORDER BY nhv.ThuTu

	END

	DECLARE @TotalRecords int
	DECLARE @TotalPages int
	DECLARE @Remainder int

	SET @TotalRecords = (SELECT Count(*) FROM #PageIndex)
	SET @TotalPages = @TotalRecords / @PageSize
	SET @Remainder = @TotalRecords % @PageSize
	IF	@Remainder > 0
	SET @TotalPages = @TotalPages + 1

	SELECT	nhv.* , 
			n.TenNhom, 
			n.NoiDung,
		n.LaCauHoiNhom

	FROM	NhomCauHoiHoanVi nhv

	LEFT OUTER JOIN NhomCauHoi n
		on	n.MaNhom = nhv.MaNhom

	JOIN	#PageIndex t2
		ON	nhv.MaDeHV	= t2.MaDeHV
		AND nhv.MaNhom	= t2.MaNhom

	WHERE
			t2.IndexID > @PageLowerBound
		AND t2.IndexID < @PageUpperBound
			
	ORDER BY t2.IndexID

	DROP TABLE #PageIndex
GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoiHoanVi_Update]
	@MaDeHV [bigint],
	@MaNhom [int],
	@ThuTu [int]
WITH EXECUTE AS CALLER
AS
UPDATE [NhomCauHoiHoanVi] SET
	[ThuTu] = @ThuTu
WHERE
	[MaDeHV] = @MaDeHV
	AND [MaNhom] = @MaNhom
 

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_ForceRemove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_ForceRemove]
	@MaSinhVien [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
	BEGIN TRANSACTION

	-- Tạo bảng tạm để lưu các ID trung gian
	DECLARE @ChiTietCaThi TABLE (MaChiTietCaThi INT)

	-- Lấy dữ liệu vào bảng tạm
	INSERT INTO @ChiTietCaThi (MaChiTietCaThi)
	SELECT MaChiTietCaThi FROM ChiTietCaThi WHERE MaSinhVien = @MaSinhVien

	-- Thực hiện xóa theo thứ tự phụ thuộc
	DELETE FROM ChiTietBaiThi
	WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi)

	DELETE FROM Audio
	WHERE MaChiTietCaThi IN (SELECT MaChiTietCaThi FROM @ChiTietCaThi)

	DELETE FROM ChiTietCaThi
	WHERE MaSinhVien = @MaSinhVien

	DELETE FROM SinhVien
	WHERE MaSinhVien = @MaSinhVien

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_GetAll]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[SinhVien] SV LEFT JOIN [Lop] L ON SV.MaLop = L.MaLop
LEFT JOIN [Khoa] K ON L.MaKhoa = K.MaKhoa


GO
/****** Object:  StoredProcedure [dbo].[SinhVien_Insert]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_Insert]
	@HoVaTenLot [nvarchar](300) = NULL,
	@TenSinhVien [nvarchar](50),
	@GioiTinh [smallint] = NULL,
	@NgaySinh [datetime] = NULL,
	@MaLop [int] = NULL,
	@DiaChi [nvarchar](max) = NULL,
	@Email [nvarchar](200) = NULL,
	@DienThoai [nvarchar](50) = NULL,
	@MaSoSinhVien [nvarchar](50),
	@Guid [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [SinhVien] (
	[HoVaTenLot],
	[TenSinhVien],
	[GioiTinh],
	[NgaySinh],
	[MaLop],
	[DiaChi],
	[Email],
	[DienThoai],
	[MaSoSinhVien],
	[Guid]
) VALUES (
	@HoVaTenLot,
	@TenSinhVien,
	@GioiTinh,
	@NgaySinh,
	@MaLop,
	@DiaChi,
	@Email,
	@DienThoai,
	@MaSoSinhVien,
	@Guid
)

select @@identity
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_Insert_Batch]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_Insert_Batch]
	@DanhSachSinhVien SinhVienType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION;
	SET NOCOUNT ON;

    -- Chèn sinh viên mới nếu chưa có
    INSERT INTO SinhVien (MaSoSinhVien, HoVaTenLot, TenSinhVien, GioiTinh, NgaySinh, MaLop, DiaChi, Email, DienThoai, Guid)
    SELECT s.MaSoSinhVien, s.HoVaTenLot, s.TenSinhVien, s.GioiTinh, s.NgaySinh, s.MaLop, s.DiaChi, s.Email, s.DienThoai, s.Guid
    FROM @DanhSachSinhVien s
    WHERE NOT EXISTS (
        SELECT 1 FROM SinhVien sv WHERE sv.MaSoSinhVien = s.MaSoSinhVien
    );
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_Login]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_Login]
	@MaSinhVien [bigint],
	@ThoiGianDangNhap [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [SinhVien] 
SET
	[DaDangNhap] = 1,
	[ThoiGianDangNhap] = @ThoiGianDangNhap
WHERE
	[MaSinhVien] = @MaSinhVien
 

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_Logout]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_Logout]
	@MaSinhVien [bigint],
	@ThoiGianDangXuat [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [SinhVien] 
SET
	[DaDangNhap] = 0,
	[ThoiGianDangXuat] = @ThoiGianDangXuat
WHERE
	[MaSinhVien] = @MaSinhVien
 

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_Remove]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_Remove]
	@MaSinhVien [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [SinhVien]
	WHERE MaSinhVien = @MaSinhVien
END
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_Guid]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_Guid]
	@Guid [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	[SinhVien]
WHERE	[Guid] = @Guid

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaKhoa]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaKhoa]
	@MaKhoa [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM SinhVien SV JOIN Lop L ON SV.MaLop=L.MaLop
JOIN Khoa K ON L.MaKhoa=K.MaKhoa
WHERE K.MaKhoa = @MaKhoa

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaKhoa_MaLop]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaKhoa_MaLop]
	@MaKhoa [int] = NULL,
	@MaLop [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM SinhVien SV JOIN Lop L ON SV.MaLop=L.MaLop
JOIN Khoa K ON L.MaKhoa=K.MaKhoa
WHERE K.MaKhoa = @MaKhoa AND L.MaLop = @MaLop

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaLop]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaLop]
	@MaLop [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM SinhVien SV JOIN Lop L ON SV.MaLop = L.MaLop
LEFT JOIN Khoa K ON K.MaKhoa = L.MaKhoa
WHERE L.MaLop = @MaLop

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaLop_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaLop_Paged]
	@MaLop [int],
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [SinhVien] SV
	INNER JOIN [Lop] LP ON SV.MaLop = LP.MaLop
	WHERE LP.MaLop = @MaLop

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [SinhVien] SV
	INNER JOIN [Lop] LP ON SV.MaLop = LP.MaLop
	WHERE LP.MaLop = @MaLop
    ORDER BY SV.MaSoSinhVien -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaLop_Search_Paged]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaLop_Search_Paged]
	@MaLop [int],
	@Keyword NVARCHAR(100),
	@PageNumber INT = 1,
    @PageSize INT = 20
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính toán tổng số bản ghi và tổng số trang
    DECLARE @TotalRecords INT, @TotalPages INT, @Remainder INT;

    -- Lấy tổng số bản ghi thỏa mãn điều kiện phân trang
    SELECT @TotalRecords = COUNT(*)
    FROM [SinhVien] SV
	INNER JOIN [Lop] LP ON SV.MaLop = LP.MaLop
	WHERE LP.MaLop = @MaLop
	AND(
		SV.MaSoSinhVien LIKE '%' + @Keyword + '%'
		OR SV.HoVaTenLot LIKE '%' + @Keyword + '%'
		OR SV.TenSinhVien LIKE '%' + @Keyword + '%'
	)

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [SinhVien] SV
	INNER JOIN [Lop] LP ON SV.MaLop = LP.MaLop
	WHERE LP.MaLop = @MaLop
	AND(
		SV.MaSoSinhVien LIKE '%' + @Keyword + '%'
		OR SV.HoVaTenLot LIKE '%' + @Keyword + '%'
		OR SV.TenSinhVien LIKE '%' + @Keyword + '%'
	)
    ORDER BY SV.MaSoSinhVien -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaSoSinhVien]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaSoSinhVien]
	@MaSoSinhVien [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
SELECT * 
FROM SinhVien 
WHERE MaSoSinhVien = @MaSoSinhVien

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_MaSoSinhVien1]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_MaSoSinhVien1]
	@MaSoSinhVien [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM SinhVien
WHERE MaSoSinhVien = @MaSoSinhVien

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectBy_ten]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectBy_ten]
	@ten [nvarchar](1) = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	SinhVien
WHERE	(HoVaTenLot + ' ' + TenSinhVien) like @ten

GO
/****** Object:  StoredProcedure [dbo].[SinhVien_SelectOne]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_SelectOne]
	@MaSinhVien [bigint]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM
	[SinhVien]
WHERE
	[MaSinhVien] = @MaSinhVien


GO
/****** Object:  StoredProcedure [dbo].[SinhVien_Update]    Script Date: 7/3/2025 6:11:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SinhVien_Update]
	@MaSinhVien [bigint],
	@HoVaTenLot [nvarchar](300),
	@TenSinhVien [nvarchar](50),
	@GioiTinh [smallint],
	@NgaySinh [datetime],
	@MaLop [int],
	@DiaChi [nvarchar](max),
	@Email [nvarchar](200),
	@DienThoai [nvarchar](50),
	@MaSoSinhVien [nvarchar](50)
WITH EXECUTE AS CALLER
AS
UPDATE [SinhVien] SET
	[HoVaTenLot] = @HoVaTenLot,
	[TenSinhVien] = @TenSinhVien,
	[GioiTinh] = @GioiTinh,
	[NgaySinh] = @NgaySinh,
	[MaLop] = @MaLop,
	[DiaChi] = @DiaChi,
	[Email] = @Email,
	[DienThoai] = @DienThoai,
	[MaSoSinhVien] = @MaSoSinhVien
WHERE
	[MaSinhVien] = @MaSinhVien
 
GO
USE [master]
GO
ALTER DATABASE [HutechExam2025] SET  READ_WRITE 
GO
