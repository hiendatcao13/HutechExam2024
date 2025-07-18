USE [master]
GO
/****** Object:  Database [HutechExam2025]    Script Date: 7/1/2025 3:29:35 PM ******/
CREATE DATABASE [HutechExam2025]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HutechExam2025', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL\MSSQL\DATA\HutechExam2025.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HutechExam2025_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL\MSSQL\DATA\HutechExam2025_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  UserDefinedTableType [dbo].[ChiTietBaiThiType]    Script Date: 7/1/2025 3:29:35 PM ******/
CREATE TYPE [dbo].[ChiTietBaiThiType] AS TABLE(
	[ma_chi_tiet_ca_thi] [int] NULL,
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
/****** Object:  UserDefinedTableType [dbo].[DeThiType]    Script Date: 7/1/2025 3:29:35 PM ******/
CREATE TYPE [dbo].[DeThiType] AS TABLE(
	[MaNhom] [int] NULL,
	[ThuTuNhom] [int] NULL,
	[MaCauHoi] [int] NULL,
	[ThuTuCauHoi] [int] NULL,
	[HoanViTraLoi] [nvarchar](4) NULL,
	[DapAn] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SinhVienCaThiType]    Script Date: 7/1/2025 3:29:35 PM ******/
CREATE TYPE [dbo].[SinhVienCaThiType] AS TABLE(
	[MaSoSinhVien] [nvarchar](50) NULL,
	[MaCaThi] [int] NULL,
	[MaDeThi] [bigint] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SinhVienType]    Script Date: 7/1/2025 3:29:35 PM ******/
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
	[StudentId] [uniqueidentifier] NULL
)
GO
/****** Object:  Table [dbo].[AudioListened]    Script Date: 7/1/2025 3:29:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AudioListened](
	[ListenID] [bigint] IDENTITY(1,1) NOT NULL,
	[MaChiTietCaThi] [int] NOT NULL,
	[MaNhom] [int] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[ListenedCount] [int] NOT NULL,
 CONSTRAINT [PK_AudioListened] PRIMARY KEY CLUSTERED 
(
	[ListenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ca_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ca_thi](
	[ma_ca_thi] [int] IDENTITY(1,1) NOT NULL,
	[ten_ca_thi] [nvarchar](50) NULL,
	[ma_chi_tiet_dot_thi] [int] NOT NULL,
	[thoi_gian_bat_dau] [datetime] NOT NULL,
	[MaDeThi] [int] NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[ActivatedDate] [datetime] NULL,
	[ThoiGianThi] [int] NOT NULL,
	[KetThuc] [bit] NOT NULL,
	[ThoiDiemKetThuc] [datetime] NULL,
	[MatMa] [nvarchar](128) NULL,
	[Approved] [bit] NOT NULL,
	[ApprovedDate] [date] NULL,
	[ApprovedComments] [nvarchar](500) NULL,
 CONSTRAINT [PK_ca_thi] PRIMARY KEY CLUSTERED 
(
	[ma_ca_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CauHoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauHoi](
	[MaCauHoi] [int] IDENTITY(1,1) NOT NULL,
	[MaNhom] [int] NOT NULL,
	[MaCLO] [int] NOT NULL,
	[TieuDe] [nvarchar](250) NULL,
	[KieuNoiDung] [int] NOT NULL,
	[NoiDung] [nvarchar](max) NULL,
	[ThuTu] [int] NOT NULL,
	[Guid] [uniqueidentifier] NULL,
	[GhiChu] [nvarchar](100) NULL,
	[HoanVi] [bit] NOT NULL,
 CONSTRAINT [PK_CauHoi] PRIMARY KEY CLUSTERED 
(
	[MaCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CauTraLoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauTraLoi](
	[MaCauTraLoi] [int] IDENTITY(1,1) NOT NULL,
	[MaCauHoi] [int] NOT NULL,
	[ThuTu] [int] NOT NULL,
	[NoiDung] [nvarchar](max) NULL,
	[LaDapAn] [bit] NOT NULL,
	[HoanVi] [bit] NOT NULL,
 CONSTRAINT [PK_CauTraLoi] PRIMARY KEY CLUSTERED 
(
	[MaCauTraLoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chi_tiet_bai_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chi_tiet_bai_thi](
	[MaChiTietBaiThi] [bigint] IDENTITY(1,1) NOT NULL,
	[ma_chi_tiet_ca_thi] [int] NOT NULL,
	[MaDeHV] [bigint] NOT NULL,
	[MaNhom] [int] NOT NULL,
	[MaCLO] [int] NOT NULL,
	[MaCauHoi] [int] NOT NULL,
	[CauTraLoi] [int] NULL,
	[NgayTao] [datetime] NOT NULL,
	[NgayCapNhat] [datetime] NULL,
	[KetQua] [bit] NULL,
	[ThuTu] [int] NOT NULL,
 CONSTRAINT [PK_chi_tiet_bai_thi] PRIMARY KEY CLUSTERED 
(
	[MaChiTietBaiThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chi_tiet_ca_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chi_tiet_ca_thi](
	[ma_chi_tiet_ca_thi] [int] IDENTITY(1,1) NOT NULL,
	[ma_ca_thi] [int] NULL,
	[ma_sinh_vien] [bigint] NULL,
	[ma_de_thi] [bigint] NULL,
	[thoi_gian_bat_dau] [datetime] NULL,
	[thoi_gian_ket_thuc] [datetime] NULL,
	[da_thi] [bit] NOT NULL,
	[da_hoan_thanh] [bit] NOT NULL,
	[diem] [float] NOT NULL,
	[tong_so_cau] [int] NULL,
	[so_cau_dung] [int] NULL,
	[gio_cong_them] [int] NOT NULL,
	[thoi_diem_cong] [datetime] NULL,
	[ly_do_cong] [nvarchar](max) NULL,
 CONSTRAINT [PK_chi_tiet_ca_thi] PRIMARY KEY CLUSTERED 
(
	[ma_chi_tiet_ca_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chi_tiet_dot_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chi_tiet_dot_thi](
	[ma_chi_tiet_dot_thi] [int] IDENTITY(1,1) NOT NULL,
	[ten_chi_tiet_dot_thi] [nvarchar](200) NOT NULL,
	[ma_lop_ao] [int] NOT NULL,
	[ma_dot_thi] [int] NOT NULL,
	[lan_thi] [int] NOT NULL,
 CONSTRAINT [PK_chi_tiet_dot_thi] PRIMARY KEY CLUSTERED 
(
	[ma_chi_tiet_dot_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDeThi](
	[MaNhom] [int] NOT NULL,
	[MaCauHoi] [int] NOT NULL,
	[ThuTu] [int] NOT NULL,
 CONSTRAINT [PK_tbl_ChiTietDeThi] PRIMARY KEY CLUSTERED 
(
	[MaNhom] ASC,
	[MaCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDeThiHoanVi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDeThiHoanVi](
	[MaDeHV] [bigint] NOT NULL,
	[MaNhom] [int] NOT NULL,
	[MaCauHoi] [int] NOT NULL,
	[ThuTu] [int] NOT NULL,
	[HoanViTraLoi] [nvarchar](4) NULL,
	[DapAn] [int] NULL,
 CONSTRAINT [PK_ChiTietDeThiHoanVi] PRIMARY KEY CLUSTERED 
(
	[MaDeHV] ASC,
	[MaNhom] ASC,
	[MaCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLO]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLO](
	[MaCLO] [int] IDENTITY(1,1) NOT NULL,
	[MaMonHoc] [int] NOT NULL,
	[MaSoCLO] [varchar](50) NOT NULL,
	[TieuDe] [nvarchar](max) NOT NULL,
	[NoiDung] [nvarchar](max) NULL,
	[TieuChi(%)] [int] NOT NULL,
	[SoCau] [int] NOT NULL,
 CONSTRAINT [PK_CLO] PRIMARY KEY CLUSTERED 
(
	[MaCLO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeThi](
	[MaDeThi] [int] IDENTITY(1,1) NOT NULL,
	[MaMonHoc] [int] NOT NULL,
	[TenDeThi] [nvarchar](250) NOT NULL,
	[Guid] [uniqueidentifier] NULL,
	[NgayTao] [datetime] NOT NULL,
	[NguoiTao] [int] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[LuuTam] [bit] NOT NULL,
	[DaDuyet] [bit] NOT NULL,
	[TongSoDeHoanVi] [int] NULL,
	[BoChuongPhan] [bit] NOT NULL,
 CONSTRAINT [PK_DeThi] PRIMARY KEY CLUSTERED 
(
	[MaDeThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeThiHoanVi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeThiHoanVi](
	[MaDeHV] [bigint] IDENTITY(1,1) NOT NULL,
	[MaDeThi] [int] NOT NULL,
	[KyHieuDe] [nvarchar](50) NULL,
	[NgayTao] [datetime] NOT NULL,
	[Guid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DeThiHoanVi] PRIMARY KEY CLUSTERED 
(
	[MaDeHV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dot_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dot_thi](
	[ma_dot_thi] [int] IDENTITY(1,1) NOT NULL,
	[ten_dot_thi] [nvarchar](150) NULL,
	[thoi_gian_bat_dau] [datetime] NULL,
	[thoi_gian_ket_thuc] [datetime] NULL,
	[NamHoc] [int] NULL,
 CONSTRAINT [PK_dot_thi] PRIMARY KEY CLUSTERED 
(
	[ma_dot_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[khoa]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[khoa](
	[ma_khoa] [int] IDENTITY(1,1) NOT NULL,
	[ten_khoa] [nvarchar](30) NULL,
	[ngay_thanh_lap] [datetime] NULL,
 CONSTRAINT [PK_khoa] PRIMARY KEY CLUSTERED 
(
	[ma_khoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoiDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoiDeThi](
	[MaLoi] [int] IDENTITY(1,1) NOT NULL,
	[MaDe] [int] NOT NULL,
	[MaNhom] [int] NULL,
	[MaCauHoi] [int] NULL,
	[MaCauTraLoi] [int] NULL,
	[NoiDung] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LoiDeThi] PRIMARY KEY CLUSTERED 
(
	[MaLoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lop]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lop](
	[ma_lop] [int] IDENTITY(1,1) NOT NULL,
	[ten_lop] [nvarchar](50) NULL,
	[ngay_bat_dau] [datetime] NULL,
	[ma_khoa] [int] NULL,
 CONSTRAINT [PK_lop] PRIMARY KEY CLUSTERED 
(
	[ma_lop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lop_ao]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lop_ao](
	[ma_lop_ao] [int] IDENTITY(1,1) NOT NULL,
	[ten_lop_ao] [nvarchar](200) NULL,
	[ngay_bat_dau] [datetime] NULL,
	[ma_mon_hoc] [int] NULL,
 CONSTRAINT [PK_lop_ao] PRIMARY KEY CLUSTERED 
(
	[ma_lop_ao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mon_hoc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mon_hoc](
	[ma_mon_hoc] [int] IDENTITY(1,1) NOT NULL,
	[ma_so_mon_hoc] [nvarchar](50) NULL,
	[ten_mon_hoc] [nvarchar](200) NULL,
 CONSTRAINT [PK_mon_hoc] PRIMARY KEY CLUSTERED 
(
	[ma_mon_hoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhomCauHoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomCauHoi](
	[MaNhom] [int] IDENTITY(1,1) NOT NULL,
	[MaDeThi] [int] NOT NULL,
	[TenNhom] [nvarchar](250) NOT NULL,
	[KieuNoiDung] [int] NOT NULL,
	[NoiDung] [nvarchar](max) NULL,
	[SoCauHoi] [int] NOT NULL,
	[HoanVi] [bit] NOT NULL,
	[ThuTu] [int] NOT NULL,
	[MaNhomCha] [int] NOT NULL,
	[SoCauLay] [int] NOT NULL,
	[LaCauHoiNhom] [bit] NULL,
 CONSTRAINT [PK_NhomCauHoi] PRIMARY KEY CLUSTERED 
(
	[MaNhom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhomCauHoiHoanVi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomCauHoiHoanVi](
	[MaDeHV] [bigint] NOT NULL,
	[MaNhom] [int] NOT NULL,
	[ThuTu] [int] NOT NULL,
 CONSTRAINT [PK_NhomHoanVi] PRIMARY KEY CLUSTERED 
(
	[MaDeHV] ASC,
	[MaNhom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[MaRole] [int] IDENTITY(1,1) NOT NULL,
	[TenRole] [nvarchar](250) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[MaRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sinh_vien]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sinh_vien](
	[ma_sinh_vien] [bigint] IDENTITY(1,1) NOT NULL,
	[ho_va_ten_lot] [nvarchar](300) NULL,
	[ten_sinh_vien] [nvarchar](50) NULL,
	[gioi_tinh] [smallint] NULL,
	[ngay_sinh] [datetime] NULL,
	[ma_lop] [int] NULL,
	[dia_chi] [nvarchar](max) NULL,
	[email] [nvarchar](200) NULL,
	[dien_thoai] [nvarchar](50) NULL,
	[ma_so_sinh_vien] [nvarchar](50) NULL,
	[student_id] [uniqueidentifier] NULL,
	[is_logged_in] [bit] NULL,
	[last_logged_in] [datetime] NULL,
	[last_logged_out] [datetime] NULL,
	[Photo] [image] NULL,
 CONSTRAINT [PK_sinh_vien] PRIMARY KEY CLUSTERED 
(
	[ma_sinh_vien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_MSSV] UNIQUE NONCLUSTERED 
(
	[ma_so_sinh_vien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien_DuPhong]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien_DuPhong](
	[ma_sinh_vien_du_phong] [bigint] IDENTITY(1,1) NOT NULL,
	[ma_sinh_vien] [bigint] NOT NULL,
	[ma_lop_ao] [int] NOT NULL,
	[ma_ca_thi] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SinhVien_DuPhong] PRIMARY KEY CLUSTERED 
(
	[ma_sinh_vien_du_phong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[MaThongBao] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[NgayTao] [datetime] NOT NULL,
	[NoiDung] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ThongBao] PRIMARY KEY CLUSTERED 
(
	[MaThongBao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[MaRole] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[LastActivityDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[LastLockoutDate] [datetime] NULL,
	[FailedPwdAttemptCount] [int] NULL,
	[FailedPwdAttemptWindowStart] [datetime] NULL,
	[FailedPwdAnswerCount] [int] NULL,
	[FailedPwdAnswerWindowStart] [datetime] NULL,
	[PasswordSalt] [nvarchar](255) NULL,
	[Comment] [nvarchar](max) NULL,
	[IsBuildInUser] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [idex_MaChiTietCaThi]    Script Date: 7/1/2025 3:29:36 PM ******/
CREATE NONCLUSTERED INDEX [idex_MaChiTietCaThi] ON [dbo].[AudioListened]
(
	[MaChiTietCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChiTietBaiThi_MaChiTietCaThi]    Script Date: 7/1/2025 3:29:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChiTietBaiThi_MaChiTietCaThi] ON [dbo].[chi_tiet_bai_thi]
(
	[ma_chi_tiet_ca_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IDX_chi_tiet_ca_thi_ma_ca_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
CREATE NONCLUSTERED INDEX [IDX_chi_tiet_ca_thi_ma_ca_thi] ON [dbo].[chi_tiet_ca_thi]
(
	[ma_ca_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AudioListened] ADD  CONSTRAINT [DF_AudioListened_ListenedCount]  DEFAULT ((0)) FOR [ListenedCount]
GO
ALTER TABLE [dbo].[ca_thi] ADD  CONSTRAINT [DF_ca_thi_IsActivated]  DEFAULT ((0)) FOR [IsActivated]
GO
ALTER TABLE [dbo].[ca_thi] ADD  CONSTRAINT [DF_ca_thi_ThoiGianThi]  DEFAULT ((0)) FOR [ThoiGianThi]
GO
ALTER TABLE [dbo].[ca_thi] ADD  CONSTRAINT [DF_ca_thi_KetThuc]  DEFAULT ((0)) FOR [KetThuc]
GO
ALTER TABLE [dbo].[ca_thi] ADD  CONSTRAINT [DF_ca_thi_Approved]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[CauHoi] ADD  CONSTRAINT [DF_CauHoi_KieuNoiDung]  DEFAULT ((-1)) FOR [KieuNoiDung]
GO
ALTER TABLE [dbo].[CauHoi] ADD  CONSTRAINT [DF_CauHoi_HoanVi]  DEFAULT ((1)) FOR [HoanVi]
GO
ALTER TABLE [dbo].[CauTraLoi] ADD  CONSTRAINT [DF_CauTraLoi_MaCauHoi]  DEFAULT ((-1)) FOR [MaCauHoi]
GO
ALTER TABLE [dbo].[CauTraLoi] ADD  CONSTRAINT [DF_Table_1_ViTri]  DEFAULT ((1)) FOR [ThuTu]
GO
ALTER TABLE [dbo].[CauTraLoi] ADD  CONSTRAINT [DF_CauTraLoi_LaDapAn]  DEFAULT ((0)) FOR [LaDapAn]
GO
ALTER TABLE [dbo].[CauTraLoi] ADD  CONSTRAINT [DF_CauTraLoi_HoanVi]  DEFAULT ((0)) FOR [HoanVi]
GO
ALTER TABLE [dbo].[chi_tiet_bai_thi] ADD  CONSTRAINT [DF_chi_tiet_bai_thi_NgayTao]  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[chi_tiet_bai_thi] ADD  CONSTRAINT [DF_chi_tiet_bai_thi_ThuTu]  DEFAULT ((0)) FOR [ThuTu]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] ADD  CONSTRAINT [DF_chi_tiet_ca_thi_da_thi]  DEFAULT ((0)) FOR [da_thi]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] ADD  CONSTRAINT [DF_chi_tiet_ca_thi_da_hoan_thanh]  DEFAULT ((0)) FOR [da_hoan_thanh]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] ADD  CONSTRAINT [DF_chi_tiet_ca_thi_diem]  DEFAULT ((-1)) FOR [diem]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] ADD  CONSTRAINT [DF_chi_tiet_ca_thi_tong_so_cau]  DEFAULT ((0)) FOR [tong_so_cau]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] ADD  CONSTRAINT [DF_chi_tiet_ca_thi_so_cau_dung]  DEFAULT ((0)) FOR [so_cau_dung]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] ADD  CONSTRAINT [DF_chi_tiet_ca_thi_gio_cong_them_1]  DEFAULT ((0)) FOR [gio_cong_them]
GO
ALTER TABLE [dbo].[chi_tiet_dot_thi] ADD  CONSTRAINT [DF_ChiTietDotThi_LanThi]  DEFAULT ((1)) FOR [lan_thi]
GO
ALTER TABLE [dbo].[ChiTietDeThiHoanVi] ADD  CONSTRAINT [DF_ChiTietDeThiHoanVi_ThuTu]  DEFAULT ((1)) FOR [ThuTu]
GO
ALTER TABLE [dbo].[DeThi] ADD  CONSTRAINT [DF_DeThi_MaMonHoc]  DEFAULT ((-1)) FOR [MaMonHoc]
GO
ALTER TABLE [dbo].[DeThi] ADD  CONSTRAINT [DF_DeThi_NgayTao]  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[DeThi] ADD  CONSTRAINT [DF_DeThi_NguoiTao]  DEFAULT ((-1)) FOR [NguoiTao]
GO
ALTER TABLE [dbo].[DeThi] ADD  CONSTRAINT [DF_DeThi_LuuTam]  DEFAULT ((1)) FOR [LuuTam]
GO
ALTER TABLE [dbo].[DeThi] ADD  CONSTRAINT [DF_DeThi_DaDuyet]  DEFAULT ((0)) FOR [DaDuyet]
GO
ALTER TABLE [dbo].[DeThi] ADD  CONSTRAINT [DF_DeThi_BoChuongPhan_1]  DEFAULT ((0)) FOR [BoChuongPhan]
GO
ALTER TABLE [dbo].[DeThiHoanVi] ADD  CONSTRAINT [DF_DeThiHoanVi_MaDeThi]  DEFAULT ((-1)) FOR [MaDeThi]
GO
ALTER TABLE [dbo].[DeThiHoanVi] ADD  CONSTRAINT [DF_DeThiHoanVi_NgayTao]  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_NhomCauHoi_MaDeThi]  DEFAULT ((-1)) FOR [MaDeThi]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_KieuNoiDung]  DEFAULT ((-1)) FOR [KieuNoiDung]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_NhomCauHoi_SoCauHoi]  DEFAULT ((0)) FOR [SoCauHoi]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_NhomCauHoi_HoanVi]  DEFAULT ((0)) FOR [HoanVi]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_NhomCauHoi_MaNhomCha]  DEFAULT ((-1)) FOR [MaNhomCha]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_NhomCauHoi_SoCauLay_1]  DEFAULT ((-1)) FOR [SoCauLay]
GO
ALTER TABLE [dbo].[NhomCauHoi] ADD  CONSTRAINT [DF_NhomCauHoi_LaCauHoiNhom_1]  DEFAULT ((0)) FOR [LaCauHoiNhom]
GO
ALTER TABLE [dbo].[NhomCauHoiHoanVi] ADD  CONSTRAINT [DF_NhomHoanVi_ThuTu]  DEFAULT ((1)) FOR [ThuTu]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_IsLockedOut]  DEFAULT ((0)) FOR [IsLockedOut]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_IsBuildInUser]  DEFAULT ((0)) FOR [IsBuildInUser]
GO
ALTER TABLE [dbo].[ca_thi]  WITH CHECK ADD  CONSTRAINT [FK_ca_thi_chi_tiet_dot_thi] FOREIGN KEY([ma_chi_tiet_dot_thi])
REFERENCES [dbo].[chi_tiet_dot_thi] ([ma_chi_tiet_dot_thi])
GO
ALTER TABLE [dbo].[ca_thi] CHECK CONSTRAINT [FK_ca_thi_chi_tiet_dot_thi]
GO
ALTER TABLE [dbo].[CauHoi]  WITH CHECK ADD  CONSTRAINT [FK_CauHoi_CLO] FOREIGN KEY([MaCLO])
REFERENCES [dbo].[CLO] ([MaCLO])
GO
ALTER TABLE [dbo].[CauHoi] CHECK CONSTRAINT [FK_CauHoi_CLO]
GO
ALTER TABLE [dbo].[CauHoi]  WITH CHECK ADD  CONSTRAINT [FK_CauHoi_NhomCauHoi] FOREIGN KEY([MaNhom])
REFERENCES [dbo].[NhomCauHoi] ([MaNhom])
GO
ALTER TABLE [dbo].[CauHoi] CHECK CONSTRAINT [FK_CauHoi_NhomCauHoi]
GO
ALTER TABLE [dbo].[CauTraLoi]  WITH CHECK ADD  CONSTRAINT [FK_CauTraLoi_CauHoi] FOREIGN KEY([MaCauHoi])
REFERENCES [dbo].[CauHoi] ([MaCauHoi])
GO
ALTER TABLE [dbo].[CauTraLoi] CHECK CONSTRAINT [FK_CauTraLoi_CauHoi]
GO
ALTER TABLE [dbo].[chi_tiet_bai_thi]  WITH CHECK ADD  CONSTRAINT [FK_chi_tiet_bai_thi_chi_tiet_ca_thi] FOREIGN KEY([ma_chi_tiet_ca_thi])
REFERENCES [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi])
GO
ALTER TABLE [dbo].[chi_tiet_bai_thi] CHECK CONSTRAINT [FK_chi_tiet_bai_thi_chi_tiet_ca_thi]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi]  WITH CHECK ADD  CONSTRAINT [FK_chi_tiet_ca_thi_ca_thi] FOREIGN KEY([ma_ca_thi])
REFERENCES [dbo].[ca_thi] ([ma_ca_thi])
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] CHECK CONSTRAINT [FK_chi_tiet_ca_thi_ca_thi]
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi]  WITH CHECK ADD  CONSTRAINT [FK_chi_tiet_ca_thi_sinh_vien] FOREIGN KEY([ma_sinh_vien])
REFERENCES [dbo].[sinh_vien] ([ma_sinh_vien])
GO
ALTER TABLE [dbo].[chi_tiet_ca_thi] CHECK CONSTRAINT [FK_chi_tiet_ca_thi_sinh_vien]
GO
ALTER TABLE [dbo].[chi_tiet_dot_thi]  WITH CHECK ADD  CONSTRAINT [FK_chi_tiet_dot_thi_dot_thi1] FOREIGN KEY([ma_dot_thi])
REFERENCES [dbo].[dot_thi] ([ma_dot_thi])
GO
ALTER TABLE [dbo].[chi_tiet_dot_thi] CHECK CONSTRAINT [FK_chi_tiet_dot_thi_dot_thi1]
GO
ALTER TABLE [dbo].[chi_tiet_dot_thi]  WITH CHECK ADD  CONSTRAINT [FK_chi_tiet_dot_thi_lop_ao] FOREIGN KEY([ma_lop_ao])
REFERENCES [dbo].[lop_ao] ([ma_lop_ao])
GO
ALTER TABLE [dbo].[chi_tiet_dot_thi] CHECK CONSTRAINT [FK_chi_tiet_dot_thi_lop_ao]
GO
ALTER TABLE [dbo].[ChiTietDeThiHoanVi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDeThiHoanVi_CauHoi] FOREIGN KEY([MaCauHoi])
REFERENCES [dbo].[CauHoi] ([MaCauHoi])
GO
ALTER TABLE [dbo].[ChiTietDeThiHoanVi] CHECK CONSTRAINT [FK_ChiTietDeThiHoanVi_CauHoi]
GO
ALTER TABLE [dbo].[ChiTietDeThiHoanVi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDeThiHoanVi_NhomCauHoiHoanVi] FOREIGN KEY([MaDeHV], [MaNhom])
REFERENCES [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom])
GO
ALTER TABLE [dbo].[ChiTietDeThiHoanVi] CHECK CONSTRAINT [FK_ChiTietDeThiHoanVi_NhomCauHoiHoanVi]
GO
ALTER TABLE [dbo].[CLO]  WITH CHECK ADD  CONSTRAINT [FK_CLO_mon_hoc] FOREIGN KEY([MaMonHoc])
REFERENCES [dbo].[mon_hoc] ([ma_mon_hoc])
GO
ALTER TABLE [dbo].[CLO] CHECK CONSTRAINT [FK_CLO_mon_hoc]
GO
ALTER TABLE [dbo].[DeThiHoanVi]  WITH CHECK ADD  CONSTRAINT [FK_DeThiHoanVi_DeThi1] FOREIGN KEY([MaDeThi])
REFERENCES [dbo].[DeThi] ([MaDeThi])
GO
ALTER TABLE [dbo].[DeThiHoanVi] CHECK CONSTRAINT [FK_DeThiHoanVi_DeThi1]
GO
ALTER TABLE [dbo].[lop]  WITH CHECK ADD  CONSTRAINT [FK_lop_khoa] FOREIGN KEY([ma_khoa])
REFERENCES [dbo].[khoa] ([ma_khoa])
GO
ALTER TABLE [dbo].[lop] CHECK CONSTRAINT [FK_lop_khoa]
GO
ALTER TABLE [dbo].[lop_ao]  WITH CHECK ADD  CONSTRAINT [FK_lop_ao_mon_hoc] FOREIGN KEY([ma_mon_hoc])
REFERENCES [dbo].[mon_hoc] ([ma_mon_hoc])
GO
ALTER TABLE [dbo].[lop_ao] CHECK CONSTRAINT [FK_lop_ao_mon_hoc]
GO
ALTER TABLE [dbo].[NhomCauHoi]  WITH CHECK ADD  CONSTRAINT [FK_NhomCauHoi_DeThi] FOREIGN KEY([MaDeThi])
REFERENCES [dbo].[DeThi] ([MaDeThi])
GO
ALTER TABLE [dbo].[NhomCauHoi] CHECK CONSTRAINT [FK_NhomCauHoi_DeThi]
GO
ALTER TABLE [dbo].[NhomCauHoiHoanVi]  WITH CHECK ADD  CONSTRAINT [FK_NhomCauHoiHoanVi_DeThiHoanVi] FOREIGN KEY([MaDeHV])
REFERENCES [dbo].[DeThiHoanVi] ([MaDeHV])
GO
ALTER TABLE [dbo].[NhomCauHoiHoanVi] CHECK CONSTRAINT [FK_NhomCauHoiHoanVi_DeThiHoanVi]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([MaRole])
REFERENCES [dbo].[Role] ([MaRole])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  StoredProcedure [dbo].[AudioListened_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AudioListened_Delete]
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [dbo].[AudioListened]
WHERE
	[MaChiTietCaThi] = @MaChiTietCaThi


GO
/****** Object:  StoredProcedure [dbo].[AudioListened_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AudioListened_Insert]
	@MaChiTietCaThi [int],
	@FileName [nvarchar](max),
	@ListenedCount [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO 	[dbo].[AudioListened] 
(
				[MaChiTietCaThi],
				[FileName],
				[ListenedCount]
) 

VALUES 
(
				@MaChiTietCaThi,
				@FileName,
				@ListenedCount
				
)
SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[AudioListened_Save]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AudioListened_Save]
    @MaChiTietCaThi [int],
    @MaNhom [int],
    @FileName [nvarchar](max) = NULL
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentCount INT;

    -- Lấy số lần nghe hiện tại nếu có
    SELECT @CurrentCount = ListenedCount
    FROM [dbo].[AudioListened]
    WHERE [MaChiTietCaThi] = @MaChiTietCaThi
      AND [MaNhom] = @MaNhom;

    -- Nếu không có bản ghi, insert mới
    IF @CurrentCount IS NULL
    BEGIN
        INSERT INTO [dbo].[AudioListened] (
            [MaChiTietCaThi],
            [MaNhom],
            [FileName],
            [ListenedCount]
        )
        VALUES (
            @MaChiTietCaThi,
            @MaNhom,
            @FileName,
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
            UPDATE [dbo].[AudioListened]
            SET [ListenedCount] = [ListenedCount] + 1
            WHERE [MaChiTietCaThi] = @MaChiTietCaThi
              AND [MaNhom] = @MaNhom;

            SELECT @CurrentCount + 1; -- Trả về số lần nghe mới
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[AudioListened_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AudioListened_SelectOne]
	@MaChiTietCaThi [int],
	@FileName [nvarchar](max)
WITH EXECUTE AS CALLER
AS
SELECT	[ListenedCount]
		
FROM
		[dbo].[AudioListened]
		
WHERE
		[MaChiTietCaThi] = @MaChiTietCaThi
	AND	[FileName] = @FileName



GO
/****** Object:  StoredProcedure [dbo].[AudioListened_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AudioListened_Update]
	@MaChiTietCaThi [int],
	@FileName [nvarchar](max),
	@ListenedCount [int]
WITH EXECUTE AS CALLER
AS
UPDATE 		[dbo].[AudioListened] 

SET
			[ListenedCount] = @ListenedCount
			
WHERE
			[MaChiTietCaThi] = @MaChiTietCaThi
		AND	[FileName] = @FileName



GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Activate]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Activate]
	@ma_ca_thi [int],
	@IsActivated [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [ca_thi] 
SET
	[IsActivated]	= @IsActivated,
	[ActivatedDate] = getDate()
WHERE
	[ma_ca_thi] = @ma_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_CanInsertStudent]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_CanInsertStudent]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	count(*)
					
	FROM	[ca_thi] CT 

	WHERE	CT.ma_ca_thi	= @MaCaThi
		AND	CT.KetThuc		= 0


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_ForceRemove]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Bảng biến để lưu danh sách chi tiết ca thi cần xóa
		DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT);

		-- Lấy các bản ghi liên quan
		INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
		SELECT ma_chi_tiet_ca_thi
		FROM chi_tiet_ca_thi
		WHERE ma_ca_thi = @ma_ca_thi;

		-- Xóa chi tiết bài thi trước
		DELETE FROM chi_tiet_bai_thi
		WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi);

		-- Xóa Audio
		DELETE FROM AudioListened
		WHERE MaChiTietCaThi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi);

		-- Xóa chi tiết ca thi
		DELETE FROM chi_tiet_ca_thi
		WHERE ma_ca_thi = @ma_ca_thi;

		-- Xóa ca thi
		DELETE FROM ca_thi
		WHERE ma_ca_thi = @ma_ca_thi;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT	*

FROM	[ca_thi] CT 

LEFT JOIN [chi_tiet_dot_thi] CTDT 
	ON CT.ma_chi_tiet_dot_thi = CTDT.ma_chi_tiet_dot_thi

LEFT JOIN 
	DeThi DT ON CT.MaDeThi = DT.MaDeThi
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_GetAll1]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_GetAll1]
WITH EXECUTE AS CALLER
AS
SELECT	[ma_ca_thi], [ten_ca_thi], [thoi_gian_bat_dau], [ThoiGianThi], [TenDeThi]

FROM		[ca_thi] CT 

LEFT JOIN	[chi_tiet_dot_thi] CTDT 
	ON		CT.ma_chi_tiet_dot_thi = CTDT.ma_chi_tiet_dot_thi

LEFT JOIN	[DeThi] DT 
	ON		CT.MaDeThi = DT.MaDeThi



GO
/****** Object:  StoredProcedure [dbo].[ca_thi_GetCount]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_GetCount]
WITH EXECUTE AS CALLER
AS
SELECT	count(*)
					
	FROM	[ca_thi] CT 

	LEFT JOIN [chi_tiet_dot_thi] CTDT 
		ON CT.ma_chi_tiet_dot_thi = CTDT.ma_chi_tiet_dot_thi

	LEFT JOIN 
		DeThi DT ON CT.MaDeThi = DT.MaDeThi


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_HuyKichHoat]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_HuyKichHoat]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
UPDATE [ca_thi] 
SET
	[IsActivated]	= 0,
	[ActivatedDate] = getDate()
WHERE
	[ma_ca_thi] = @ma_ca_thi

-- Xóa toàn bộ bài làm của sinh viên trong ca thi
    DELETE CTBT
    FROM chi_tiet_bai_thi CTBT
    JOIN chi_tiet_ca_thi CTCT 
        ON CTBT.ma_chi_tiet_ca_thi = CTCT.ma_chi_tiet_ca_thi
    WHERE CTCT.ma_ca_thi = @ma_ca_thi;
-- Xóa những ghi nhận phần nghe trong ca thi
	DELETE Audio
	FROM AudioListened Audio
	JOIN chi_tiet_ca_thi CTCT
		ON Audio.MaChiTietCaThi = CTCT.ma_chi_tiet_ca_thi
	WHERE CTCT.ma_ca_thi = @ma_ca_thi
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Insert]
	@ten_ca_thi [nvarchar](50),
	@ma_chi_tiet_dot_thi [int],
	@thoi_gian_bat_dau [datetime],
	@MaDeThi [int],
	@ThoiGianThi [int],
	@MatMa [nvarchar](128)
WITH EXECUTE AS CALLER
AS
IF EXISTS (
	SELECT	* 
	FROM	[ca_thi] 
	WHERE	[ten_ca_thi] = @ten_ca_thi
		and [ma_chi_tiet_dot_thi] = @ma_chi_tiet_dot_thi
)
	set @ten_ca_thi = @ten_ca_thi + '_01'

INSERT INTO [ca_thi] 
(
	[ten_ca_thi],
	[ma_chi_tiet_dot_thi],
	[thoi_gian_bat_dau],
	[MaDeThi],
	--[IsActivated],
	[ThoiGianThi],
	[MatMa]
) 
VALUES 
(
	@ten_ca_thi,
	@ma_chi_tiet_dot_thi,
	@thoi_gian_bat_dau,
	@MaDeThi,
	--@IsActivated,
	@ThoiGianThi,
	@MatMa
)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_IsExists]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_IsExists]
	@MaCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	count(*)
					
	FROM	[ca_thi] CT 

	WHERE	CT.ma_ca_thi = @MaCaThi


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Ketthuc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Ketthuc]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
IF NOT EXISTS (
	SELECT	* FROM [ca_thi] 
	WHERE	[ma_ca_thi] = @ma_ca_thi
		AND [KetThuc]	= 1)
BEGIN

	UPDATE [ca_thi] 
	SET
		[KetThuc]	= 1,
		[ThoiDiemKetThuc] = getDate()
	WHERE
		[ma_ca_thi] = @ma_ca_thi
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Ketthuc1]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Ketthuc1]
	@ma_ca_thi [int],
	@is_ket_thuc [bit]
WITH EXECUTE AS CALLER
AS
IF NOT EXISTS (
	SELECT	* FROM [ca_thi] 
	WHERE	[ma_ca_thi] = @ma_ca_thi
		AND [KetThuc]	= @is_ket_thuc)
BEGIN
	UPDATE [ca_thi] 
	SET
		[KetThuc] = @is_ket_thuc,
		[ThoiDiemKetThuc] = getDate()
	WHERE
		[ma_ca_thi] = @ma_ca_thi
END


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Remove]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
    DELETE FROM [ca_thi]
	WHERE ma_ca_thi = @ma_ca_thi
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi]
	@ma_chi_tiet_dot_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT *, (select COUNT(ma_chi_tiet_ca_thi) from chi_tiet_ca_thi ctct where ctct.ma_ca_thi = CT.ma_ca_thi) as 'SoLuongSinhVien'
FROM ca_thi CT
LEFT JOIN [chi_tiet_dot_thi] CTDT ON CT.ma_chi_tiet_dot_thi=CTDT.ma_chi_tiet_dot_thi 
LEFT JOIN DeThi DT ON CT.MaDeThi = DT.MaDeThi
WHERE CT.ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi_Paged]
	@ma_chi_tiet_dot_thi [int],
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
    FROM [ca_thi] CT
    WHERE CT.ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi;

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *, (SELECT COUNT(*) FROM chi_tiet_ca_thi  WHERE ma_ca_thi = CT.ma_ca_thi) AS TongSV
	FROM [ca_thi] CT
    WHERE CT.ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), thoi_gian_bat_dau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi_Search_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi_Search_Paged]
	@ma_chi_tiet_dot_thi [int],
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
    FROM [ca_thi] CT
    WHERE CT.ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi
	AND (
        CT.ten_ca_thi LIKE '%' + @Keyword + '%'
        OR CT.ma_ca_thi LIKE '%' + @Keyword + '%'
    );

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *, (SELECT COUNT(*) FROM chi_tiet_ca_thi  WHERE ma_ca_thi = CT.ma_ca_thi) AS TongSV
    FROM [ca_thi] CT
    WHERE CT.ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi
	AND (
        CT.ten_ca_thi LIKE '%' + @Keyword + '%'
        OR CT.ma_ca_thi LIKE '%' + @Keyword + '%'
    )
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), thoi_gian_bat_dau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_MaDotThi_MaLop]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectBy_MaDotThi_MaLop]
	@ma_dot_thi [int] = NULL,
	@ma_lop [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ct.* 

FROM	[dbo].[ca_thi] ct 

JOIN	[dbo].[chi_tiet_dot_thi] ctdt 
	ON	ct.[ma_chi_tiet_dot_thi]	= ctdt.[ma_chi_tiet_dot_thi] 

JOIN	[dbo].[dot_thi] dt 
	ON	dt.[ma_dot_thi]	= ctdt.ma_dot_thi

JOIN	[dbo].[lop_ao] la 
	ON	la.[ma_lop_ao]	= ctdt.ma_lop_ao

WHERE	dt.ma_dot_thi	= @ma_dot_thi 
	AND la.[ma_lop_ao]	= @ma_lop
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_MaDotThi_MaLop_LanThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectBy_MaDotThi_MaLop_LanThi]
	@ma_dot_thi [int] = NULL,
	@ma_lop [int] = NULL,
	@lan_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ct.* 

FROM	[dbo].[ca_thi] ct 

JOIN	[dbo].[chi_tiet_dot_thi] ctdt 
	ON	ct.[ma_chi_tiet_dot_thi]	= ctdt.[ma_chi_tiet_dot_thi] 

JOIN	[dbo].[dot_thi] dt 
	ON	dt.[ma_dot_thi]	= ctdt.ma_dot_thi

JOIN	[dbo].[lop_ao] la 
	ON	la.[ma_lop_ao]	= ctdt.ma_lop_ao

WHERE	dt.ma_dot_thi	= @ma_dot_thi 
	AND la.[ma_lop_ao]	= @ma_lop
	AND ctdt.lan_thi	= @lan_thi
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectByMonHoc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectByMonHoc]
	@ma_mon_hoc [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
	FROM	ca_thi ct

	LEFT JOIN [chi_tiet_dot_thi] ctdt
	ON	ct.ma_chi_tiet_dot_thi = ctdt.ma_chi_tiet_dot_thi 

	LEFT JOIN [lop_ao] la
	ON	la.ma_lop_ao = ctdt.ma_lop_ao

	WHERE la.ma_mon_hoc = @ma_mon_hoc


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectByMonThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectByMonThi]
	@ma_mon_hoc [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
	FROM	ca_thi ct

	LEFT JOIN [chi_tiet_dot_thi] ctdt
	ON	ct.ma_chi_tiet_dot_thi = ctdt.ma_chi_tiet_dot_thi 

	LEFT JOIN [lop_ao] la
	ON	la.ma_lop_ao = ctdt.ma_lop_ao

	WHERE la.ma_mon_hoc = @ma_mon_hoc


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectCongGioMax]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectCongGioMax]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT	'GIO_CONG_THEM_MAX' = MAX([gio_cong_them]) 
FROM	[chi_tiet_ca_thi] ctct
WHERE	[ma_ca_thi] = @ma_ca_thi
 

GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectKetThuc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectKetThuc]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
IF EXISTS (	
		SELECT	*
		FROM	[ca_thi]
		WHERE	[ma_ca_thi] = @ma_ca_thi			
			AND [KetThuc]	= 1
	)
SELECT 1
ELSE
SELECT 0




GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectOne]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM
	[ca_thi]
WHERE
	[ma_ca_thi] = @ma_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectPage]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectPage]
	@PageNumber [int],
	@PageSize [int]
WITH EXECUTE AS CALLER
AS
DECLARE @PageLowerBound int
DECLARE @PageUpperBound int


SET @PageLowerBound = (@PageSize * @PageNumber) - @PageSize
SET @PageUpperBound = @PageLowerBound + @PageSize + 1


CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ma_ca_thi int
)

BEGIN

	INSERT INTO #PageIndex ( 
		ma_ca_thi
	)

	SELECT	[ma_ca_thi]
					
	FROM	[ca_thi] CT 

	LEFT JOIN [chi_tiet_dot_thi] CTDT 
		ON CT.ma_chi_tiet_dot_thi = CTDT.ma_chi_tiet_dot_thi

	LEFT JOIN 
		DeThi DT ON CT.MaDeThi = DT.MaDeThi

	ORDER BY [IsActivated], [thoi_gian_bat_dau] desc,  [ten_ca_thi]
END


SELECT	*

FROM	[ca_thi] CT 

LEFT JOIN [chi_tiet_dot_thi] CTDT 
	ON CT.ma_chi_tiet_dot_thi = CTDT.ma_chi_tiet_dot_thi

LEFT JOIN 
	DeThi DT ON CT.MaDeThi = DT.MaDeThi

JOIN	#PageIndex t2
ON			
		CT.[ma_ca_thi] = t2.[ma_ca_thi]
		
WHERE
		t2.IndexID > @PageLowerBound 
		AND t2.IndexID < @PageUpperBound
		
ORDER BY t2.IndexID

DROP TABLE #PageIndex


GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectPaged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectPaged]
	@ma_dot_thi [int],
	@ma_mon_hoc [int],
	@ma_lop_ao [int],
	@lan_thi [nvarchar](200)
WITH EXECUTE AS CALLER
AS
SELECT		CT.[ma_ca_thi], 
			CT.[ten_ca_thi], 
			CT.[thoi_gian_bat_dau], 
			CT.[ThoiGianThi], 
			DT.[TenDeThi]

FROM		[dbo].[ca_thi] CT

LEFT JOIN	[dbo].[chi_tiet_dot_thi] CTDT 
	ON		CT.[ma_chi_tiet_dot_thi] = CTDT.[ma_chi_tiet_dot_thi]

LEFT JOIN	[dbo].[lop_ao] la
	ON		la.[ma_lop_ao] = CTDT.[ma_lop_ao]

LEFT JOIN	[dbo].[DeThi] DT 
	ON		CT.[MaDeThi] = DT.[MaDeThi]

WHERE	(	CTDT.[ma_dot_thi]	= @ma_dot_thi)
	AND	(	la.[ma_mon_hoc]		= @ma_mon_hoc
		OR	@ma_mon_hoc = -1 OR	@ma_mon_hoc IS NULL)
	AND	(	CTDT.[ma_lop_ao]	= @ma_lop_ao
		OR	@ma_lop_ao = -1 OR	@ma_lop_ao IS NULL)
	AND	(	CTDT.[lan_thi]	= @lan_thi
		OR	@lan_thi = N'Tt c' OR	@lan_thi IS NULL)
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectResult]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectResult]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT	'MaDeThi'		= ctct.ma_de_thi,
		'DiemThi'		= ctct.diem,
		'DaThi'			= ctct.da_thi,
		'DaHoanThanh'	= ctct.da_hoan_thanh,
		'TenCaThi'		= ct.ten_ca_thi,
		'NgayThi'		= ct.thoi_gian_bat_dau,
		'ThoiLuongThi'	= ct.ThoiGianThi,
		'MSSV'			= sv.ma_so_sinh_vien,
		'Ho'			= sv.ho_va_ten_lot,
		'Ten'			= sv.ten_sinh_vien,
		'GioiTinh'		= sv.gioi_tinh,
		'SoCauDung'		= ctct.so_cau_dung,
		'TongSoCau'		= ctct.tong_so_cau
		--,'NgaySinh'		= sv.ngay_sinh

FROM	[chi_tiet_ca_thi] ctct

LEFT OUTER JOIN [ca_thi] ct
	ON	ctct.[ma_ca_thi] = ct.[ma_ca_thi] 

LEFT OUTER JOIN [sinh_vien] sv
	ON	ctct.[ma_sinh_vien] = sv.[ma_sinh_vien] 

WHERE	ctct.[ma_ca_thi] = @ma_ca_thi

ORDER BY	sv.[ten_sinh_vien], sv.[ho_va_ten_lot]
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectRunning]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_SelectRunning]
WITH EXECUTE AS CALLER
AS
SELECT	*

FROM	[ca_thi] ct

LEFT JOIN	[chi_tiet_dot_thi] ctdt 
	ON	ct.[ma_chi_tiet_dot_thi] = ctdt.[ma_chi_tiet_dot_thi]

LEFT JOIN	[DeThi] dt 
	ON	ct.MaDeThi			= dt.MaDeThi

WHERE	ct.[IsActivated]	= 1
	AND	ct.[KetThuc]		= 0
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Update]
	@ma_ca_thi [int],
	@ten_ca_thi [nvarchar](50),
	@ma_chi_tiet_dot_thi [int],
	@thoi_gian_bat_dau [datetime],
	@MaDeThi [int],
	@ThoiGianThi [int],
	@MatMa [nvarchar](128)
WITH EXECUTE AS CALLER
AS
UPDATE [ca_thi] 
SET
	[ten_ca_thi] = @ten_ca_thi,
	[ma_chi_tiet_dot_thi] = @ma_chi_tiet_dot_thi,
	[thoi_gian_bat_dau] = @thoi_gian_bat_dau,
	[MaDeThi] = @MaDeThi,
	--[IsActivated ]=@IsActivated,
	[ThoiGianThi]=@ThoiGianThi,
	[MatMa] = @MatMa
WHERE
	[ma_ca_thi] = @ma_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_UpdateApproved]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_UpdateApproved]
	@ma_ca_thi [int],
	@Approved [bit],
	@ApprovedDate [datetime],
	@ApprovedComments [nvarchar](500)
WITH EXECUTE AS CALLER
AS
UPDATE [ca_thi] 
SET
	Approved = @Approved,
	ApprovedDate = @ApprovedDate,
	ApprovedComments = @ApprovedComments
WHERE
	[ma_ca_thi] = @ma_ca_thi

GO
/****** Object:  StoredProcedure [dbo].[ca_thi_UpdateDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_UpdateDeThi]
	@MaCaThi [int],
	@MaDeThi [int],
	@IsOrderMSSV [bit],
	@DsDeThiHoanVi NVARCHAR(max)
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION

        -- Cập nhật đề gốc cho ca thi
        UPDATE ca_thi
        SET MaDeThi = @MaDeThi
        WHERE ma_ca_thi = @MaCaThi;

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
					CASE WHEN @IsOrderMSSV = 1 THEN SV.ma_so_sinh_vien END,
					CASE WHEN @IsOrderMSSV = 0 THEN SV.ten_sinh_vien END) AS rn,
                C.ma_chi_tiet_ca_thi
            FROM chi_tiet_ca_thi C
            JOIN sinh_vien SV ON SV.ma_sinh_vien = C.ma_sinh_vien
            WHERE C.ma_ca_thi = @MaCaThi
        )

        -- Gán mã đề hoán vị theo thứ tự MSSV
        UPDATE C
        SET ma_de_thi = D.MaDeHoanVi
        FROM chi_tiet_ca_thi C
        INNER JOIN SV_CTE S ON C.ma_chi_tiet_ca_thi = S.ma_chi_tiet_ca_thi
        INNER JOIN DeThiHV_CTE D ON S.rn = D.rn;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[CauHoi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_Delete]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
DECLARE @MaNhom BIGINT, @ThuTu INT

    SELECT @MaNhom = MaNhom, @ThuTu = ThuTu
    FROM [CauHoi]
    WHERE MaCauHoi = @MaCauHoi

DELETE FROM CauHoi
WHERE MaCauHoi = @MaCauHoi

--cập nhật thứ tự câu hỏi
    UPDATE [CauHoi]
    SET [ThuTu] = [ThuTu] - 1
    WHERE [MaNhom] = @MaNhom AND [ThuTu] > @ThuTu
GO
/****** Object:  StoredProcedure [dbo].[CauHoi_ForceDelete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_ForceDelete]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION

			DELETE FROM [CauTraLoi]
			WHERE MaCauHoi = @MaCauHoi

			DELETE FROM [ChiTietDeThiHoanVi]
			WHERE MaCauHoi = @MaCauHoi

			DECLARE @MaNhom BIGINT, @ThuTu INT
				SELECT @MaNhom = MaNhom, @ThuTu = ThuTu
				FROM [CauHoi]
				WHERE MaCauHoi = @MaCauHoi

			DELETE FROM [CauHoi]
			WHERE MaCauHoi = @MaCauHoi

			--cập nhật thứ tự câu hỏi
			UPDATE [CauHoi]
			SET [ThuTu] = [ThuTu] - 1
			WHERE [MaNhom] = @MaNhom AND [ThuTu] > @ThuTu
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[CauHoi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_Insert]
	@MaCLO [int],
	@MaNhom [int],
	@TieuDe [nvarchar](250),
	@KieuNoiDung [int],
	@NoiDung [nvarchar](max),
	@ThuTu [int],
	@GhiChu [nvarchar](100),
	@HoanVi [bit]
WITH EXECUTE AS CALLER
AS
INSERT INTO [CauHoi] (
	[MaCLO],
	[MaNhom],
	[TieuDe],
	[KieuNoiDung],
	[NoiDung],
	[ThuTu],
	[GhiChu],
	[HoanVi]
) VALUES (
	@MaCLO,
	@MaNhom,
	@TieuDe,
	@KieuNoiDung,
	@NoiDung,
	@ThuTu,
	@GhiChu,
	@HoanVi
)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[CauHoi]



GO
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectBy_MaNhom]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_SelectBy_MaNhom]
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM	[CauHoi] ch
LEFT JOIN [CLO] clo ON ch.MaCLO = clo.MaCLO
LEFT JOIN [CauTraLoi] ctl ON ch.MaCauHoi = ctl.MaCauHoi
WHERE
	[MaNhom] = @MaNhom
GO
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectDapAn]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_SelectDapAn]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
SELECT TOP 1 MaCauTraLoi
FROM	[CauTraLoi]
WHERE	[MaCauHoi]  = @MaCauHoi
	AND	[LaDapAn]	= 1
ORDER BY ThuTu



GO
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_SelectOne]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[CauHoi] ch
LEFT JOIN [CLO] clo ON ch.MaCLO = clo.MaCLO
LEFT JOIN [CauTraLoi] ctl ON ch.MaCauHoi = ctl.MaCauHoi
WHERE
	ch.[MaCauHoi] = @MaCauHoi
GO
/****** Object:  StoredProcedure [dbo].[CauHoi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_Update]
	@MaCauHoi [int],
	@MaNhom [int],
	@MaCLO [int],
	@TieuDe [nvarchar](250),
	@KieuNoiDung [int],
	@NoiDung [nvarchar](max),
	@ThuTu [int],
	@GhiChu [nvarchar](100),
	@HoanVi [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [CauHoi] 
SET
	[MaCLO] = @MaCLO,
	[MaNhom] = @MaNhom,
	[TieuDe] = @TieuDe,
	[KieuNoiDung] = @KieuNoiDung,
	[NoiDung] = @NoiDung,
	[ThuTu] = @ThuTu,
	[GhiChu] = @GhiChu,
	[HoanVi] = @HoanVi
WHERE
	[MaCauHoi] = @MaCauHoi
GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_CountBy_MaCauHoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_CountBy_MaCauHoi]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
SELECT	count(*)
FROM	CauTraLoi
WHERE	MaCauHoi = @MaCauHoi

GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_Delete]
	@MaCauTraLoi [int]
WITH EXECUTE AS CALLER
AS
DECLARE @MaCauHoi INT, @ThuTu INT

SELECT @MaCauHoi = MaCauHoi, @ThuTu = ThuTu
FROM [CauTraLoi]
WHERE MaCauTraLoi = @MaCauTraLoi

DELETE FROM [CauTraLoi]
WHERE
	[MaCauTraLoi] = @MaCauTraLoi

-- cập nhật thứ tự của câu trả lời
UPDATE [CauTraLoi]
SET ThuTu = ThuTu - 1
WHERE MaCauHoi = @MaCauHoi AND ThuTu > @ThuTu
GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_Insert]
	@MaCauHoi [int],
	@ThuTu [int],
	@NoiDung [nvarchar](max),
	@LaDapAn [bit],
	@HoanVi [bit]
WITH EXECUTE AS CALLER
AS
INSERT INTO [CauTraLoi] (
	[MaCauHoi],
	[ThuTu],
	[NoiDung],
	[LaDapAn],
	[HoanVi]
) 
VALUES (
	@MaCauHoi,
	@ThuTu,
	@NoiDung,
	@LaDapAn,
	@HoanVi
)

SELECT @@IDENTITY



GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[CauTraLoi]



GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_SelectBy_MaCauHoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_SelectBy_MaCauHoi]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	CauTraLoi
WHERE	MaCauHoi = @MaCauHoi

GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_SelectOne]
	@MaCauTraLoi [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[CauTraLoi]
WHERE
	[MaCauTraLoi] = @MaCauTraLoi



GO
/****** Object:  StoredProcedure [dbo].[CauTraLoi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauTraLoi_Update]
	@MaCauTraLoi [int],
	@MaCauHoi [int],
	@ThuTu [int],
	@NoiDung [nvarchar](max),
	@LaDapAn [bit],
	@HoanVi [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [CauTraLoi] 
SET
	[MaCauHoi] = @MaCauHoi,
	[ThuTu] = @ThuTu,
	[NoiDung] = @NoiDung,
	[LaDapAn] = @LaDapAn,
	[HoanVi]	= @HoanVi
WHERE
	[MaCauTraLoi] = @MaCauTraLoi
 


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_DaThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_DaThi]
	@ma_chi_tiet_ca_thi [int]
WITH EXECUTE AS CALLER
AS
	SELECT STRING_AGG(ctbt.CauTraLoi, ';;;')
	FROM [chi_tiet_bai_thi] ctbt
	WHERE ctbt.ma_chi_tiet_ca_thi = @ma_chi_tiet_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Delete]
	@MaChiTietBaiThi [bigint]
WITH EXECUTE AS CALLER
AS
DELETE FROM [chi_tiet_bai_thi]
WHERE
	[MaChiTietBaiThi] = @MaChiTietBaiThi

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Insert]
	@ma_chi_tiet_ca_thi [int],
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@MaCLO [int],
	@NgayTao [datetime],
	@ThuTu [int]
WITH EXECUTE AS CALLER
AS
-- kiem tra ton tai
if not exists (select * from [chi_tiet_bai_thi]
	where	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
		and [MaDeHV]	= @MaDeHV
		and [MaNhom]	= @MaNhom
		and [MaCauHoi]	= @MaCauHoi
		and [MaCLO]		= @MaCLO)
begin
	INSERT INTO [chi_tiet_bai_thi] 
	(
		[ma_chi_tiet_ca_thi],
		[MaDeHV],
		[MaNhom],
		[MaCauHoi],
		[MaCLO],
		[NgayTao],
		[ThuTu]
	) 
	VALUES 
	(
		@ma_chi_tiet_ca_thi,
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Insert_Batch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Insert_Batch]
	@Data ChiTietBaiThiType READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRANSACTION;

		INSERT INTO chi_tiet_bai_thi (
			ma_chi_tiet_ca_thi,
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
			source.ma_chi_tiet_ca_thi,
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Save]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Save]
	@ma_chi_tiet_ca_thi [int],
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
if not exists (select * from [chi_tiet_bai_thi]
	where	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
		and [MaDeHV]	= @MaDeHV
		and [MaNhom]	= @MaNhom
		and [MaCauHoi]	= @MaCauHoi
		and [MaCLO]		= @MaCLO)
begin
	INSERT INTO [chi_tiet_bai_thi] 
	(
		[ma_chi_tiet_ca_thi],
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
		@ma_chi_tiet_ca_thi,
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
	UPDATE [chi_tiet_bai_thi] 
SET
	[CauTraLoi]		= @CauTraLoi,
	[NgayCapNhat]	= @NgayCapNhat,
	[KetQua]		= @KetQua

where	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
		and [MaDeHV]	= @MaDeHV
		and [MaNhom]	= @MaNhom
		and [MaCauHoi]	= @MaCauHoi
end
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Save_Batch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Save_Batch]
    @Data ChiTietBaiThiType READONLY
AS
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    MERGE chi_tiet_bai_thi AS target
    USING @Data AS source
    ON target.ma_chi_tiet_ca_thi = source.ma_chi_tiet_ca_thi
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
            ma_chi_tiet_ca_thi, MaDeHV, MaNhom, MaCauHoi, MaCLO,
            CauTraLoi, NgayTao, KetQua, ThuTu
        )
        VALUES (
            source.ma_chi_tiet_ca_thi, source.MaDeHV, source.MaNhom,
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[chi_tiet_bai_thi]


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi]
	@ma_chi_tiet_ca_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	bt.* 
FROM	chi_tiet_bai_thi bt

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

WHERE	 bt.ma_chi_tiet_ca_thi = @ma_chi_tiet_ca_thi
			
order by bt.ThuTu--	nhv.ThuTu, nhv2.ThuTu, cthv.ThuTu
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi_Count]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi_Count]
	@ma_chi_tiet_ca_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT		count(*)

	FROM		chi_tiet_bai_thi

	WHERE		ma_chi_tiet_ca_thi = @ma_chi_tiet_ca_thi


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi_Paged]
	@ma_chi_tiet_ca_thi [int] = NULL,
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

	FROM		chi_tiet_bai_thi

	WHERE		ma_chi_tiet_ca_thi = @ma_chi_tiet_ca_thi

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
			[dbo].chi_tiet_bai_thi t

	JOIN	#PageIndex t2
	ON		t.MaChiTietBaiThi = t2.MaChiTietBaiThi

	WHERE
			t2.IndexID > @PageLowerBound
		AND t2.IndexID < @PageUpperBound
			
	ORDER BY t2.IndexID

	DROP TABLE #PageIndex

			
			
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_SelectOne]
	@MaChiTietBaiThi [bigint]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[chi_tiet_bai_thi]
WHERE
	[MaChiTietBaiThi] = @MaChiTietBaiThi


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectOne_v2]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_SelectOne_v2]
	@ma_chi_tiet_ca_thi [int],
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@MaCLO [int]
WITH EXECUTE AS CALLER
AS
SELECT 	bt.*,
		dt.HoanViTraLoi,
		dt.DapAn
		
FROM	[chi_tiet_bai_thi] bt

LEFT OUTER JOIN [ChiTietDeThiHoanVi] dt
	ON dt.MaDeHV = bt.MaDeHV 

WHERE
	bt.ma_chi_tiet_ca_thi	= @ma_chi_tiet_ca_thi
AND	bt.MaDeHV				= @MaDeHV
AND bt.MaNhom				= @MaNhom
AND bt.MaCauHoi				= @MaCauHoi
AND bt.MaCLO				= @MaCLO


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Update]
	@MaChiTietBaiThi [bigint],
	@CauTraLoi [int],
	@NgayCapNhat [datetime],
	@KetQua [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [chi_tiet_bai_thi] 
SET
	[CauTraLoi]		= @CauTraLoi,
	[NgayCapNhat]	= @NgayCapNhat,
	[KetQua]		= @KetQua
	--[ThuTu]			= @ThuTu
WHERE
	[MaChiTietBaiThi] = @MaChiTietBaiThi
 

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Update_v2]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Update_v2]
	@MaChiTietCaThi [bigint],
	@MaCauHoi [int],
	@MaCLO [int],
	@CauTraLoi [int],
	@NgayCapNhat [datetime],
	@KetQua [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [chi_tiet_bai_thi] 
SET
	[MaCLO]			= @MaCLO,
	[CauTraLoi]		= @CauTraLoi,
	[NgayCapNhat]	= @NgayCapNhat,
	[KetQua]		= @KetQua
	--[ThuTu]			= @ThuTu
WHERE
	[ma_chi_tiet_ca_thi] = @MaChiTietCaThi
AND
	[MaCauHoi] = @MaCauHoi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_CongGio]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_CongGio]
	@ma_chi_tiet_ca_thi [int],
	@gio_cong_them [int],
	@thoi_diem_cong [datetime],
	@ly_do_cong [nvarchar](max)
WITH EXECUTE AS CALLER
AS
UPDATE [chi_tiet_ca_thi] 
SET
	[gio_cong_them] = @gio_cong_them,
	[thoi_diem_cong] = @thoi_diem_cong,
	[ly_do_cong] = @ly_do_cong
WHERE
	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
 


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_ForceRemove]
	@ma_chi_tiet_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
	BEGIN TRANSACTION

	DELETE FROM [chi_tiet_bai_thi]
	WHERE ma_chi_tiet_ca_thi = @ma_chi_tiet_ca_thi

	DELETE FROM [chi_tiet_ca_thi]
	WHERE [ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi

	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[chi_tiet_ca_thi] CTCT JOIN [sinh_vien] SV ON CTCT.ma_sinh_vien = SV.ma_sinh_vien
	JOIN [ca_thi] CT ON CTCT.ma_ca_thi = CT.ma_ca_thi


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_Insert]
	@ma_ca_thi [int],
	@ma_sinh_vien [bigint],
	@ma_de_thi [bigint],
	@tong_so_cau [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [chi_tiet_ca_thi] 
(
	[ma_ca_thi],
	[ma_sinh_vien],
	[ma_de_thi],
	[tong_so_cau]
) 
VALUES 
(
	@ma_ca_thi,
	@ma_sinh_vien,
	@ma_de_thi,
	@tong_so_cau
)

SELECT @@IDENTITY




GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Insert_Batch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_Insert_Batch]
	@DanhSachSinhVienCaThi SinhVienCaThiType READONLY
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION;
    SET NOCOUNT ON;

    INSERT INTO chi_tiet_ca_thi(ma_ca_thi, ma_sinh_vien, ma_de_thi)
    SELECT s.MaCaThi, sv.ma_sinh_vien, s.MaDeThi
    FROM sinh_vien sv
    INNER JOIN @DanhSachSinhVienCaThi s ON sv.ma_so_sinh_vien = s.MaSoSinhVien
    WHERE NOT EXISTS (
        SELECT 1 FROM chi_tiet_ca_thi ctct
        WHERE ctct.ma_ca_thi = s.MaCaThi AND ctct.ma_sinh_vien = sv.ma_sinh_vien
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_Remove]
	@ma_chi_tiet_ca_thi [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [chi_tiet_ca_thi]
WHERE
	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi



GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Save_Batch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_Save_Batch]
WITH EXECUTE AS CALLER
AS
BEGIN
SELECT *
FROM [sinh_vien]
END
/*	@DanhSachSinhVien SinhVienType READONLY,
	@MaCaThi [INT]
AS
BEGIN
BEGIN TRANSACTION;
	SET NOCOUNT ON;

    -- Chèn sinh viên mới nếu chưa có (giả sử MaSoSinhVien là duy nhất)
    INSERT INTO sinh_vien (ma_so_sinh_vien, ho_va_ten_lot, ten_sinh_vien, gioi_tinh)
    SELECT s.MaSoSinhVien, s.HoVaTenLot, s.TenSinhVien, s.GioiTinh
    FROM @DanhSachSinhVien s
    WHERE NOT EXISTS (
        SELECT 1 FROM sinh_vien sv WHERE sv.ma_so_sinh_vien = s.MaSoSinhVien
    );

    -- Chèn vào bảng chi tiết ca thi (ví dụ ChiTietCaThi)
    INSERT INTO chi_tiet_ca_thi(ma_ca_thi, ma_sinh_vien)
    SELECT @MaCaThi, sv.ma_sinh_vien
    FROM sinh_vien sv
    INNER JOIN @DanhSachSinhVien s ON sv.ma_so_sinh_vien = s.MaSoSinhVien
    WHERE NOT EXISTS (
        SELECT 1 FROM chi_tiet_ca_thi ctct
        WHERE ctct.ma_ca_thi = @MaCaThi AND ctct.ma_sinh_vien = sv.ma_sinh_vien
    );
COMMIT TRANSACTION;
END*/
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Select_GioCongThem]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_Select_GioCongThem]
	@ma_chi_tiet_ca_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT	[gio_cong_them]
FROM	[chi_tiet_ca_thi]
WHERE	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi




GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi]
	@ma_ca_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	CTCT.*, SV.*, DTHV.KyHieuDe 
FROM	[chi_tiet_ca_thi] CTCT 

LEFT JOIN	[sinh_vien] SV 
	ON	CTCT.ma_sinh_vien = SV.ma_sinh_vien
LEFT JOIN	[DeThiHoanVi] DTHV
	ON CTCT.ma_de_thi = DTHV.MaDeHV

WHERE CTCT.ma_ca_thi = @ma_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Count]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Count]
	@ma_ca_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	count(*)

	FROM	[chi_tiet_ca_thi]

	WHERE	[ma_ca_thi] = @ma_ca_thi


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_CountForSearch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_CountForSearch]
	@ma_ca_thi [int] = NULL,
	@Keyword [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SELECT	count(CTCT.[ma_chi_tiet_ca_thi])

	FROM	[chi_tiet_ca_thi] CTCT

	JOIN	[sinh_vien] sv
		ON	CTCT.ma_sinh_vien = sv.ma_sinh_vien

	WHERE	[ma_ca_thi] = @ma_ca_thi
		AND (
			sv.[ma_so_sinh_vien] LIKE '%' + @Keyword + '%'
		OR sv.[ten_sinh_vien] LIKE '%' + @Keyword + '%'
		OR sv.[ho_va_ten_lot] LIKE '%' + @Keyword + '%')

	

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Page]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Page]
	@ma_ca_thi [int] = NULL,
	@PageIndex INT = 0,
	@PageSize INT = 0
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        * 
    FROM 
        [chi_tiet_ca_thi] CTCT 
    JOIN 
        [sinh_vien] SV ON CTCT.ma_sinh_vien = SV.ma_sinh_vien
    WHERE 
        CTCT.ma_ca_thi = @ma_ca_thi
    ORDER BY 
        CTCT.ma_sinh_vien -- cần ORDER BY để OFFSET-FETCH hoạt động
    OFFSET @PageIndex * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Paged]
    @ma_ca_thi INT,
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
    FROM [chi_tiet_ca_thi] CTCT
    WHERE CTCT.ma_ca_thi = @ma_ca_thi;

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT 
        CTCT.*, 
        SV.*, 
		DTHV.KyHieuDe,
        --CT.*, bỏ qua ca thi vì lặp quá nhiều
        ROW_NUMBER() OVER (ORDER BY SV.ma_so_sinh_vien) AS RowNum
    FROM [chi_tiet_ca_thi] CTCT
    LEFT JOIN [sinh_vien] SV ON CTCT.ma_sinh_vien = SV.ma_sinh_vien
	LEFT JOIN [DeThiHoanVi] DTHV ON CTCT.ma_de_thi = DTHV.MaDeHV
    WHERE CTCT.ma_ca_thi = @ma_ca_thi
    ORDER BY SV.ma_so_sinh_vien -- sắp xếp theo mã số sinh viên
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Search_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Search_Paged]
    @ma_ca_thi INT,
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
    FROM [chi_tiet_ca_thi] CTCT
    JOIN [sinh_vien] SV ON CTCT.ma_sinh_vien = SV.ma_sinh_vien
    WHERE CTCT.ma_ca_thi = @ma_ca_thi
    AND (
        SV.ma_so_sinh_vien LIKE '%' + @Keyword + '%'
        OR SV.ten_sinh_vien LIKE '%' + @Keyword + '%'
        OR SV.ho_va_ten_lot LIKE '%' + @Keyword + '%'
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
		DTHV.KyHieuDe,
		-- CT.*, bỏ ca thi vì lặp nhiều
        ROW_NUMBER() OVER (ORDER BY SV.ma_so_sinh_vien) AS RowNum
    FROM [chi_tiet_ca_thi] CTCT
    LEFT JOIN [sinh_vien] SV ON CTCT.ma_sinh_vien = SV.ma_sinh_vien
	LEFT JOIN [DeThiHoanVi] DTHV ON CTCT.ma_de_thi = DTHV.MaDeHV
    WHERE CTCT.ma_ca_thi = @ma_ca_thi
    AND (
        SV.ma_so_sinh_vien LIKE '%' + @Keyword + '%'
        OR SV.ten_sinh_vien LIKE '%' + @Keyword + '%'
        OR SV.ho_va_ten_lot LIKE '%' + @Keyword + '%'
    )
    ORDER BY SV.ma_so_sinh_vien
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi1]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi1]
	@ma_ca_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ma_so_sinh_vien, 
		ho_va_ten_lot, 
		ten_sinh_vien 
FROM	chi_tiet_ca_thi CTCT 
JOIN	[sinh_vien] SV 
	ON	CTCT.ma_sinh_vien = SV.ma_sinh_vien
JOIN	[ca_thi] CT 
	ON	CTCT.ma_ca_thi = CT.ma_ca_thi
WHERE	CTCT.ma_ca_thi = @ma_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_de_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_de_thi]
	@ma_de_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ctct.*,
		--dethi.MaDeThi,
		ct.ten_ca_thi,
		ct.ThoiGianThi,
		'ThoiDiemKichHoat' = ct.ActivatedDate,
		mon.ten_mon_hoc

FROM
	[chi_tiet_ca_thi] ctct

LEFT OUTER JOIN [ca_thi] ct
	ON	ct.ma_ca_thi = ctct.ma_ca_thi 
	
LEFT OUTER JOIN	[DeThi] dethi 
	ON	dethi.MaDeThi			= CT.MaDeThi

LEFT OUTER JOIN	[mon_hoc] mon 
	ON	dethi.MaMonHoc			= mon.ma_mon_hoc
	
WHERE ctct.ma_de_thi = @ma_de_thi




GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_sinh_vien]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_ma_sinh_vien]
	@ma_sinh_vien [bigint] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ctct.*,
		--dethi.MaDeThi,
		ct.ten_ca_thi,
		ct.ThoiGianThi,
		'ThoiDiemKichHoat' = ct.ActivatedDate,
		mon.ten_mon_hoc

FROM
	[chi_tiet_ca_thi] ctct

LEFT OUTER JOIN [ca_thi] ct
	ON	ct.ma_ca_thi = ctct.ma_ca_thi 
	
LEFT OUTER JOIN	[DeThi] dethi 
	ON	dethi.MaDeThi			= CT.MaDeThi

LEFT OUTER JOIN	[mon_hoc] mon 
	ON	dethi.MaMonHoc			= mon.ma_mon_hoc
	
WHERE ctct.ma_sinh_vien = @ma_sinh_vien

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_MaCaThi_MaSinhVien]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_MaCaThi_MaSinhVien]
	@ma_ca_thi [int] = NULL,
	@ma_sinh_vien [bigint] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ctct.*,
		--dethi.MaDeThi,
		ct.ten_ca_thi,
		ct.ThoiGianThi,
		'ThoiDiemKichHoat' = ct.ActivatedDate,
		mon.ten_mon_hoc

FROM
	[chi_tiet_ca_thi] ctct

LEFT OUTER JOIN [ca_thi] ct
	ON	ct.ma_ca_thi = ctct.ma_ca_thi 
	
LEFT OUTER JOIN	[DeThi] dethi 
	ON	dethi.MaDeThi			= CT.MaDeThi

LEFT OUTER JOIN	[mon_hoc] mon 
	ON	dethi.MaMonHoc			= mon.ma_mon_hoc
	
WHERE	ctct.ma_ca_thi		= @ma_ca_thi 
	AND ctct.ma_sinh_vien	= @ma_sinh_vien
			
	
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_MaSinhVienThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectBy_MaSinhVienThi]
	@ma_sinh_vien [bigint] = NULL
WITH EXECUTE AS CALLER
AS
SELECT TOP 1 CTCT.*, CT.*, CTDT.*, LA.*, MH.*, SV.*, DTHV.KyHieuDe

FROM	[chi_tiet_ca_thi] CTCT 

LEFT JOIN	[ca_thi] CT 
	ON	CTCT.ma_ca_thi			= CT.ma_ca_thi

LEFT JOIN	[sinh_vien] SV
	ON	CTCT.ma_sinh_vien			= SV.ma_sinh_vien

LEFT JOIN	[chi_tiet_dot_thi] CTDT
	ON	CT.ma_chi_tiet_dot_thi	= CTDT.ma_chi_tiet_dot_thi

LEFT JOIN	[lop_ao] LA
	ON	CTDT.ma_lop_ao			= LA.ma_lop_ao

LEFT JOIN	[mon_hoc] MH 
	ON	LA.ma_mon_hoc			= MH.ma_mon_hoc

LEFT JOIN	[DeThiHoanVi] DTHV
	ON	DTHV.MaDeHV				= CTCT.ma_de_thi

WHERE	CTCT.ma_sinh_vien		= @ma_sinh_vien 
	AND CT.KetThuc				= 0 -- ca thi chua ket thuc	
	AND CT.IsActivated			= 1 -- da kich hoat
	AND	CTCT.da_hoan_thanh		= 0 -- chua thi xong
	AND (CTCT.thoi_gian_bat_dau IS NULL
		OR DATEADD(MINUTE, CT.ThoiGianThi, CTCT.thoi_gian_bat_dau) >= GETDATE()) -- Chưa hết giờ
ORDER BY
    ABS(DATEDIFF(SECOND, GETDATE(), CTCT.thoi_gian_bat_dau)) -- lấy thời gian gần nhất với hiện tại
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_SelectOne]
	@ma_chi_tiet_ca_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT	ctct.*, sv.*

FROM
	[chi_tiet_ca_thi] ctct

LEFT OUTER JOIN [sinh_vien] sv
	ON	sv.ma_sinh_vien = ctct.ma_sinh_vien 


WHERE
	ctct.[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_Update]
	@ma_chi_tiet_ca_thi [int],
	@ma_ca_thi [int],
	@ma_sinh_vien [bigint],
	@ma_de_thi [bigint],
	@tong_so_cau [int]
WITH EXECUTE AS CALLER
AS
UPDATE [chi_tiet_ca_thi] SET
	[ma_ca_thi] = @ma_ca_thi,
	[ma_sinh_vien] = @ma_sinh_vien,
	[ma_de_thi] = @ma_de_thi,
	[tong_so_cau] = @tong_so_cau 
WHERE
	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
 


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_UpdateBatDau]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_UpdateBatDau]
	@ma_chi_tiet_ca_thi [int],
	@thoi_gian_bat_dau [datetime]
WITH EXECUTE AS CALLER
AS
IF EXISTS (SELECT * FROM [chi_tiet_ca_thi]
	WHERE	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
		and [thoi_gian_bat_dau] is NULL)
BEGIN

UPDATE [chi_tiet_ca_thi] 
SET
	thoi_gian_bat_dau = @thoi_gian_bat_dau,
	da_thi = 1 --da thi
WHERE
	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
 
END

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_UpdateKetThuc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_UpdateKetThuc]
	@ma_chi_tiet_ca_thi [int],
	@thoi_gian_ket_thuc [datetime],
	@diem [float],
	@so_cau_dung [int],
	@tong_so_cau [int]
WITH EXECUTE AS CALLER
AS
UPDATE [chi_tiet_ca_thi] 
SET
	thoi_gian_ket_thuc = @thoi_gian_ket_thuc,
	diem = @diem,
	so_cau_dung = @so_cau_dung,
	tong_so_cau = @tong_so_cau,
	da_hoan_thanh = 1 --da hoan thanh
WHERE
	[ma_chi_tiet_ca_thi] = @ma_chi_tiet_ca_thi
 


GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_ForceRemove]
	@ma_chi_tiet_dot_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION

        -- Bảng tạm lưu ID liên quan
        DECLARE @CaThi TABLE (ma_ca_thi INT)
        DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT)

        -- Lấy dữ liệu vào bảng tạm
        INSERT INTO @CaThi (ma_ca_thi)
        SELECT ma_ca_thi FROM ca_thi WHERE ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi

        INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
        SELECT ma_chi_tiet_ca_thi FROM chi_tiet_ca_thi WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi)

        -- Xoá theo thứ tự phụ thuộc
        DELETE FROM chi_tiet_bai_thi
        WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

		DELETE FROM AudioListened
        WHERE MaChiTietCaThi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

        DELETE FROM chi_tiet_ca_thi
        WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi)

        DELETE FROM ca_thi
        WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi)

        DELETE FROM chi_tiet_dot_thi
        WHERE ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi

        COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[chi_tiet_dot_thi] CTDT LEFT JOIN [dot_thi] DT ON CTDT.ma_dot_thi=DT.ma_dot_thi
	LEFT JOIN [lop_ao] LA ON CTDT.ma_lop_ao=LA.ma_lop_ao

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_Insert]
	@ten_chi_tiet_dot_thi [nvarchar](200),
	@ma_lop_ao [int],
	@ma_dot_thi [int],
	@lan_thi [nvarchar](200)
WITH EXECUTE AS CALLER
AS
INSERT INTO [chi_tiet_dot_thi] (
	[ten_chi_tiet_dot_thi],
	[ma_lop_ao],
	[ma_dot_thi],
	[lan_thi]
) VALUES (
	@ten_chi_tiet_dot_thi,
	@ma_lop_ao,
	@ma_dot_thi,
	@lan_thi
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_Remove]
	@ma_chi_tiet_dot_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [chi_tiet_dot_thi]
	WHERE ma_chi_tiet_dot_thi = @ma_chi_tiet_dot_thi
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_ma_dot_thi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_ma_dot_thi]
	@ma_dot_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT CTDT.*, LA.*, MH.*
FROM chi_tiet_dot_thi CTDT 
	JOIN [lop_ao] LA ON CTDT.ma_lop_ao=LA.ma_lop_ao
	JOIN [mon_hoc] MH ON MH.ma_mon_hoc = LA.ma_mon_hoc
WHERE CTDT.ma_dot_thi = @ma_dot_thi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_ma_dot_thi_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_ma_dot_thi_Paged]
	@ma_dot_thi [int],
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
    FROM [chi_tiet_dot_thi] CTDT
	LEFT JOIN [lop_ao] LA ON LA.ma_lop_ao = CTDT.ma_lop_ao
	LEFT JOIN [mon_hoc] MH ON MH.ma_mon_hoc = LA.ma_mon_hoc
    WHERE CTDT.ma_dot_thi = @ma_dot_thi

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [chi_tiet_dot_thi] CTDT
	LEFT JOIN [lop_ao] LA ON LA.ma_lop_ao = CTDT.ma_lop_ao
	LEFT JOIN [mon_hoc] MH ON MH.ma_mon_hoc = LA.ma_mon_hoc
	WHERE CTDT.ma_dot_thi = @ma_dot_thi
    ORDER BY CTDT.lan_thi DESC -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_ma_lop_ao]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_ma_lop_ao]
	@ma_lop_ao [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM chi_tiet_dot_thi
WHERE ma_lop_ao = @ma_lop_ao
			
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: Friday, December 25, 2009
-- Created By:   LXMANH
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaCTDT_MaDotThi_MaLopAo]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_MaCTDT_MaDotThi_MaLopAo]
	@ma_chi_tiet_dot_thi [int] = NULL,
	@ma_dot_thi [int] = NULL,
	@ma_lop_ao [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM chi_tiet_dot_thi CTDT LEFT JOIN [dot_thi] DT ON CTDT.ma_dot_thi=DT.ma_dot_thi
	LEFT JOIN [lop_ao] LA ON CTDT.ma_lop_ao=LA.ma_lop_ao
WHERE CTDT.ma_dot_thi = @ma_dot_thi AND CTDT.ma_lop_ao = @ma_lop_ao AND CTDT.ma_chi_tiet_dot_thi != @ma_chi_tiet_dot_thi

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaCTDT_MaLopAo_LanThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_MaCTDT_MaLopAo_LanThi]
	@ma_chi_tiet_dot_thi [int] = NULL,
	@ma_lop_ao [int] = NULL,
	@lan_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM chi_tiet_dot_thi CTDT LEFT JOIN [dot_thi] DT ON CTDT.ma_dot_thi=DT.ma_dot_thi
	LEFT JOIN [lop_ao] LA ON CTDT.ma_lop_ao=LA.ma_lop_ao
WHERE CTDT.lan_thi = @lan_thi AND CTDT.ma_lop_ao = @ma_lop_ao AND CTDT.ma_chi_tiet_dot_thi != @ma_chi_tiet_dot_thi

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo]
	@ma_dot_thi [int] = NULL,
	@ma_lop_ao [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM chi_tiet_dot_thi CTDT LEFT JOIN [dot_thi] DT ON CTDT.ma_dot_thi=DT.ma_dot_thi
	LEFT JOIN [lop_ao] LA ON CTDT.ma_lop_ao=LA.ma_lop_ao
WHERE CTDT.ma_dot_thi = @ma_dot_thi AND CTDT.ma_lop_ao = @ma_lop_ao

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo_LanThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo_LanThi]
	@ma_dot_thi [int] = NULL,
	@ma_lop_ao [int] = NULL,
	@lan_thi [nvarchar] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM chi_tiet_dot_thi CTDT 
WHERE CTDT.ma_dot_thi = @ma_dot_thi AND CTDT.lan_thi = @lan_thi AND CTDT.ma_lop_ao = @ma_lop_ao
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaLopAo_LanThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectBy_MaLopAo_LanThi]
	@ma_lop_ao [int] = NULL,
	@lan_thi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM chi_tiet_dot_thi CTDT LEFT JOIN [dot_thi] DT ON CTDT.ma_dot_thi=DT.ma_dot_thi
	LEFT JOIN [lop_ao] LA ON CTDT.ma_lop_ao=LA.ma_lop_ao
WHERE CTDT.lan_thi = @lan_thi AND CTDT.ma_lop_ao = @ma_lop_ao

GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_SelectOne]
	@ma_chi_tiet_dot_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT ctdt.*, la.*, mh.*
FROM
	[chi_tiet_dot_thi] ctdt
JOIN [lop_ao] la ON la.ma_lop_ao = ctdt.ma_lop_ao
JOIN [mon_hoc] mh ON mh.ma_mon_hoc = la.ma_mon_hoc
WHERE
	[ma_chi_tiet_dot_thi] = @ma_chi_tiet_dot_thi
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_Update]
	@ma_chi_tiet_dot_thi [int],
	@ten_chi_tiet_dot_thi [nvarchar](200),
	@ma_lop_ao [int],
	@ma_dot_thi [int],
	@lan_thi [nvarchar](200)
WITH EXECUTE AS CALLER
AS
UPDATE [chi_tiet_dot_thi] SET
	[ten_chi_tiet_dot_thi] = @ten_chi_tiet_dot_thi,
	[ma_lop_ao] = @ma_lop_ao,
	[ma_dot_thi] = @ma_dot_thi,
	[lan_thi] = @lan_thi
WHERE
	[ma_chi_tiet_dot_thi] = @ma_chi_tiet_dot_thi

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_Insert]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@ThuTu [int],
	@HoanViTraLoi [nvarchar](4),
	@DapAn [int]
WITH EXECUTE AS CALLER
AS
-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [ChiTietDeThiHoanVi] (
	[MaDeHV],
	[MaNhom],
	[MaCauHoi],
	[ThuTu],
	[HoanViTraLoi],
	[DapAn]
) 
VALUES 
(
	@MaDeHV,
	@MaNhom,
	@MaCauHoi,
	@ThuTu,
	@HoanViTraLoi,
	@DapAn
)
SELECT @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Insert_Batch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_Insert_Batch]
	@MaDeThi [int],
	@KyHieuDe [nvarchar](50) = NULL,
	@TongSLDe [int],
	@DanhSachThongTinDeThi DeThiType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION;
	DECLARE @MaDeHV [bigint]

	--Thêm đề thi hoán vị
	INSERT INTO DeThiHoanVi(MaDeThi, KyHieuDe, NgayTao, Guid)
	SELECT @MaDeThi, @KyHieuDe, GETDATE(), NEWID()

	-- Lấy ID vừa insert
	SET @MaDeHV = SCOPE_IDENTITY();

	--Thêm nhóm câu hỏi hoán vị
	INSERT INTO NhomCauHoiHoanVi(MaDeHV, MaNhom, ThuTu)
	SELECT @MaDeHV, ds.MaNhom, ds.ThuTuNhom
	FROM @DanhSachThongTinDeThi ds

    -- Thêm chi tiết đề thi hoán vị
    INSERT INTO ChiTietDeThiHoanVi(MaDeHV, MaNhom, MaCauHoi, ThuTu, HoanViTraLoi, DapAn)
	SELECT @MaDeHV, ds.MaNhom, ds.MaCauHoi, ds.ThuTuCauHoi, ds.HoanViTraLoi, ds.DapAn
	FROM @DanhSachThongTinDeThi ds

	--Cập nhật tổng số đề thi hoán vi
	UPDATE DeThi
	SET TongSoDeHoanVi = ISNULL(TongSoDeHoanVi, 0) + @TongSLDe
	WHERE MaDeThi = @MaDeThi

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_Remove]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [ChiTietDeThiHoanVi]
WHERE
	[MaDeHV] = @MaDeHV
	AND [MaNhom] = @MaNhom
	AND [MaCauHoi] = @MaCauHoi


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT	*
FROM	[ChiTietDeThiHoanVi]



GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_DapAn]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_DapAn]
	@DapAn [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDeThiHoanVi
WHERE DapAn = @DapAn

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaCauHoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaCauHoi]
	@MaCauHoi [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDeThiHoanVi
WHERE MaCauHoi = @MaCauHoi
			
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV]
	@MaDeHV [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ct.* 

FROM	ChiTietDeThiHoanVi ct

LEFT OUTER JOIN NhomCauHoi nch
	ON nch.MaNhom = ct.MaNhom

WHERE	ct.MaDeHV = @MaDeHV 

ORDER BY ct.ThuTu, nch.ThuTu

			
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaCauHoi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaCauHoi]
	@MaDeHV [bigint],
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
SELECT * FROM ChiTietDeThiHoanVi
WHERE MaDeHV = @MaDeHV and MaCauHoi = @MaCauHoi		
			
		


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom]
	@MaDeHV [bigint],
	@MaNhom [int]
WITH EXECUTE AS CALLER
AS
DECLARE @MANHOMCHA INT
DECLARE @TONGCAUHOI INT

SELECT	@MANHOMCHA	= n.MaNhomCha
FROM	ChiTietDeThiHoanVi ct
LEFT OUTER JOIN NhomCauHoi n
	ON	n.MaNhom	= ct.MaNhom		
WHERE	ct.MaNhom	= @MaNhom 
	AND ct.MaDeHV	= @MaDeHV

IF (@MANHOMCHA = -1 OR @MANHOMCHA IS NULL)
	
BEGIN

	SELECT @TONGCAUHOI = SUM(SOCAUHOI) 
	FROM
	(
		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
		(
			SELECT	MaNhom 
			FROM	NHOMCAUHOIHOANVI			
			WHERE	MaDeHV = @MaDeHV 
				AND	ThuTu <
				(	
					SELECT	ThuTu 
					FROM	NHOMCAUHOIHOANVI
					WHERE	MaDeHV = @MaDeHV
						AND	MaNhom = @MaNhom
				)
				AND MaNhom IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOI
					WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
				)
		)
		GROUP BY ct.MaNhom

		UNION ALL

		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
		(
			SELECT	MaNhom 
			FROM	NHOMCAUHOI
			WHERE	MaNhomCha IN
			(	SELECT	MaNhom 
				FROM	NHOMCAUHOIHOANVI			
				WHERE	MaDeHV = @MaDeHV 
					AND	ThuTu <
					(	
						SELECT	ThuTu 
						FROM	NHOMCAUHOIHOANVI
						WHERE	MaDeHV = @MaDeHV
							AND	MaNhom = @MaNhom
					)
					AND MaNhom IN
					(	SELECT	MaNhom 
						FROM	NHOMCAUHOI
						WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
					)
			)
		)
		GROUP BY ct.MaNhom

	) AS TEMP

	IF @TONGCAUHOI IS NULL
		SET @TONGCAUHOI = 0


	SELECT	CTDHV.*,
			CH.*,
			'ThuTuSapXep' = @TONGCAUHOI + CTDHV.ThuTu,
			@TONGCAUHOI

	FROM	ChiTietDeThiHoanVi CTDHV

	JOIN	CauHoi CH 
		ON	CTDHV.MaCauHoi	= CH.MaCauHoi

	WHERE	CTDHV.MaNhom = @MaNhom 
		AND CTDHV.MaDeHV = @MaDeHV

	ORDER BY CTDHV.ThuTu			
END

ELSE -- khong phai nhom cap 1

BEGIN

	SELECT	@TONGCAUHOI = SUM(SOCAUHOI) 
	FROM
	(
		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
			(
			SELECT	MaNhom 
			FROM	NHOMCAUHOIHOANVI			
			WHERE	MaDeHV = @MaDeHV 
				AND	ThuTu <=
				(	
					SELECT	DISTINCT NHV.ThuTu 
					FROM	NHOMCAUHOIHOANVI NHV
					LEFT OUTER JOIN NHOMCAUHOI N
						ON	N.MaNhom = NHV.MaNhom
					WHERE	NHV.MaDeHV = @MaDeHV
						AND	n.MaNhom IN
							(SELECT MaNhomCha FROM NHOMCAUHOI WHERE MaNhom = @MaNhom)
				)
				AND MaNhom IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOI
					WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
				)
			)
		GROUP BY ct.MaNhom

		UNION ALL

		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
		(
			SELECT	MaNhom 
			FROM	NHOMCAUHOI
			WHERE	MaNhomCha IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOIHOANVI			
					WHERE	MaDeHV = @MaDeHV 
						AND	ThuTu <=
						(	
							SELECT	DISTINCT NHV.ThuTu 
							FROM	NHOMCAUHOIHOANVI NHV
							LEFT OUTER JOIN NHOMCAUHOI N
								ON	N.MaNhom = NHV.MaNhom
							WHERE	NHV.MaDeHV = @MaDeHV
								AND	N.MaNhom IN
									(SELECT MaNhomCha FROM NHOMCAUHOI WHERE MaNhom = @MaNhom)
						)
						AND MaNhom IN
						(	SELECT	MaNhom 
							FROM	NHOMCAUHOI
							WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
						)
				)
				AND MaNhom IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOIHOANVI			
					WHERE	MaDeHV = @MaDeHV 
						AND	ThuTu <
						(	
							SELECT	ThuTu 
							FROM	NHOMCAUHOIHOANVI
							WHERE	MaDeHV	= @MaDeHV
								AND	MaNhom	= @MaNhom
						)
--						AND MaNhom IN
--						(	SELECT	MaNhom 
--							FROM	NHOMCAUHOI
--							WHERE	MaNhomCha in (SELECT MaNhomCha FROM NHOMCAUHOI WHERE MaNhom = @MaNhom)
--						)
				)
		)
		GROUP BY ct.MaNhom

	) AS TEMP

	IF @TONGCAUHOI IS NULL
		SET @TONGCAUHOI = 0

	SELECT	CTDHV.*,
			CH.*,
			'ThuTuSapXep' = @TONGCAUHOI + CTDHV.ThuTu,
			@TONGCAUHOI

	FROM	ChiTietDeThiHoanVi CTDHV

	JOIN	CauHoi CH 
		ON	CTDHV.MaCauHoi	= CH.MaCauHoi

	WHERE	CTDHV.MaNhom = @MaNhom 
		AND CTDHV.MaDeHV = @MaDeHV

	ORDER BY CTDHV.ThuTu			
END		
			
		


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
DECLARE @MANHOMCHA INT
DECLARE @TONGCAUHOI INT

SELECT	@MANHOMCHA	= n.MaNhomCha
FROM	ChiTietDeThiHoanVi ct
LEFT OUTER JOIN NhomCauHoi n
	ON	n.MaNhom	= ct.MaNhom		
WHERE	ct.MaNhom	= @MaNhom 
	AND ct.MaDeHV	= @MaDeHV

IF (@MANHOMCHA = -1 OR @MANHOMCHA IS NULL)
	
BEGIN

	SELECT @TONGCAUHOI = SUM(SOCAUHOI) 
	FROM
	(
		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
		(
			SELECT	MaNhom 
			FROM	NHOMCAUHOIHOANVI			
			WHERE	MaDeHV = @MaDeHV 
				AND	ThuTu <
				(	
					SELECT	ThuTu 
					FROM	NHOMCAUHOIHOANVI
					WHERE	MaDeHV = @MaDeHV
						AND	MaNhom = @MaNhom
				)
				AND MaNhom IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOI
					WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
				)
		)
		GROUP BY ct.MaNhom

		UNION ALL

		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
		(
			SELECT	MaNhom 
			FROM	NHOMCAUHOI
			WHERE	MaNhomCha IN
			(	SELECT	MaNhom 
				FROM	NHOMCAUHOIHOANVI			
				WHERE	MaDeHV = @MaDeHV 
					AND	ThuTu <
					(	
						SELECT	ThuTu 
						FROM	NHOMCAUHOIHOANVI
						WHERE	MaDeHV = @MaDeHV
							AND	MaNhom = @MaNhom
					)
					AND MaNhom IN
					(	SELECT	MaNhom 
						FROM	NHOMCAUHOI
						WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
					)
			)
		)
		GROUP BY ct.MaNhom

	) AS TEMP

	IF @TONGCAUHOI IS NULL
		SET @TONGCAUHOI = 0


	SELECT	CTDHV.*,
			CH.*,
			'ThuTuSapXep' = @TONGCAUHOI + CTDHV.ThuTu,
			@TONGCAUHOI,
			'MaTraLoiDaLuu' = bt.CauTraLoi

	FROM	ChiTietDeThiHoanVi CTDHV

	JOIN	CauHoi CH 
		ON	CTDHV.MaCauHoi	= CH.MaCauHoi

	LEFT OUTER JOIN	
			[chi_tiet_bai_thi] bt
		ON	(bt.MaCauHoi	= CTDHV.MaCauHoi
			AND	bt.MaNhom	= CTDHV.MaNhom
			AND bt.MaDeHV	= CTDHV.MaDeHV
			AND bt.Ma_Chi_Tiet_Ca_Thi = @MaChiTietCaThi)

	WHERE	CTDHV.MaNhom = @MaNhom 
		AND CTDHV.MaDeHV = @MaDeHV

	ORDER BY CTDHV.ThuTu			
END

ELSE -- khong phai nhom cap 1

BEGIN

	SELECT	@TONGCAUHOI = SUM(SOCAUHOI) 
	FROM
	(
		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
			(
			SELECT	MaNhom 
			FROM	NHOMCAUHOIHOANVI			
			WHERE	MaDeHV = @MaDeHV 
				AND	ThuTu <=
				(	
					SELECT	DISTINCT NHV.ThuTu 
					FROM	NHOMCAUHOIHOANVI NHV
					LEFT OUTER JOIN NHOMCAUHOI N
						ON	N.MaNhom = NHV.MaNhom
					WHERE	NHV.MaDeHV = @MaDeHV
						AND	n.MaNhom IN
							(SELECT MaNhomCha FROM NHOMCAUHOI WHERE MaNhom = @MaNhom)
				)
				AND MaNhom IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOI
					WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
				)
			)
		GROUP BY ct.MaNhom

		UNION ALL

		SELECT	COUNT(ct.MACAUHOI) AS SOCAUHOI

		FROM	CHITIETDETHI ct

		WHERE	ct.MaNhom IN
		(
			SELECT	MaNhom 
			FROM	NHOMCAUHOI
			WHERE	MaNhomCha IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOIHOANVI			
					WHERE	MaDeHV = @MaDeHV 
						AND	ThuTu <=
						(	
							SELECT	DISTINCT NHV.ThuTu 
							FROM	NHOMCAUHOIHOANVI NHV
							LEFT OUTER JOIN NHOMCAUHOI N
								ON	N.MaNhom = NHV.MaNhom
							WHERE	NHV.MaDeHV = @MaDeHV
								AND	N.MaNhom IN
									(SELECT MaNhomCha FROM NHOMCAUHOI WHERE MaNhom = @MaNhom)
						)
						AND MaNhom IN
						(	SELECT	MaNhom 
							FROM	NHOMCAUHOI
							WHERE	MaNhomCha = -1 OR MaNhomCha IS NULL
						)
				)
				AND MaNhom IN
				(	SELECT	MaNhom 
					FROM	NHOMCAUHOIHOANVI			
					WHERE	MaDeHV = @MaDeHV 
						AND	ThuTu <
						(	
							SELECT	ThuTu 
							FROM	NHOMCAUHOIHOANVI
							WHERE	MaDeHV	= @MaDeHV
								AND	MaNhom	= @MaNhom
						)
--						AND MaNhom IN
--						(	SELECT	MaNhom 
--							FROM	NHOMCAUHOI
--							WHERE	MaNhomCha in (SELECT MaNhomCha FROM NHOMCAUHOI WHERE MaNhom = @MaNhom)
--						)
				)
		)
		GROUP BY ct.MaNhom

	) AS TEMP

	IF @TONGCAUHOI IS NULL
		SET @TONGCAUHOI = 0

	SELECT	CTDHV.*,
			CH.*,
			'ThuTuSapXep' = @TONGCAUHOI + CTDHV.ThuTu,
			@TONGCAUHOI,
			'MaTraLoiDaLuu' = bt.CauTraLoi

	FROM	ChiTietDeThiHoanVi CTDHV

	JOIN	CauHoi CH 
		ON	CTDHV.MaCauHoi	= CH.MaCauHoi

	LEFT OUTER JOIN	
			[chi_tiet_bai_thi] bt
		ON	(bt.MaCauHoi = CH.MaCauHoi
		AND	bt.MaNhom = @MaNhom
		AND bt.MaDeHV = @MaDeHV
		AND bt.Ma_Chi_Tiet_Ca_Thi = @MaChiTietCaThi)

	WHERE	CTDHV.MaNhom = @MaNhom 
		AND CTDHV.MaDeHV = @MaDeHV

	ORDER BY CTDHV.ThuTu			
END		
			
		


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v2]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v2]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
CREATE TABLE #DeThiIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	MaCauHoi int
)

BEGIN

INSERT INTO #DeThiIndex ( 
	MaCauHoi
)

SELECT	
		MaCauHoi
		
FROM	ChiTietDeThiHoanVi
		
WHERE	MaDeHV = @MaDeHV

ORDER BY [MaNhom], [ThuTu]

END

--select * from #DeThiIndex
	SELECT	CTDHV.*,
			CH.*,
			'ThuTuSapXep' = p.IndexID,
			--@TONGCAUHOI,
			'MaTraLoiDaLuu' = bt.CauTraLoi

	FROM	ChiTietDeThiHoanVi CTDHV

	JOIN	CauHoi CH 
		ON	CTDHV.MaCauHoi	= CH.MaCauHoi

	LEFT OUTER JOIN	
			[chi_tiet_bai_thi] bt
		ON	(bt.MaCauHoi = CH.MaCauHoi
		AND	bt.MaNhom = @MaNhom
		AND bt.MaDeHV = @MaDeHV
		AND bt.Ma_Chi_Tiet_Ca_Thi = @MaChiTietCaThi)

	JOIN	#DeThiIndex p 
		ON  CTDHV.MaCauHoi = p.MaCauHoi
		

	WHERE	CTDHV.MaDeHV = @MaDeHV
		AND	CTDHV.MaNhom = @MaNhom 
		

	ORDER BY p.IndexID	
			
		
DROP TABLE #DeThiIndex

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v3]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v3]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaChiTietCaThi [int]
WITH EXECUTE AS CALLER
AS
SELECT	CTDHV.*,
		CH.*,
		'ThuTuSapXep' = bt.ThuTu,
		--@TONGCAUHOI,
		'MaTraLoiDaLuu' = bt.CauTraLoi

FROM	ChiTietDeThiHoanVi CTDHV

JOIN	CauHoi CH 
	ON	CTDHV.MaCauHoi	= CH.MaCauHoi

LEFT OUTER JOIN	
		[chi_tiet_bai_thi] bt
	ON	(bt.MaCauHoi = CH.MaCauHoi
	AND	bt.MaNhom = @MaNhom
	AND bt.MaDeHV = @MaDeHV
	AND bt.Ma_Chi_Tiet_Ca_Thi = @MaChiTietCaThi)
	

WHERE	CTDHV.MaDeHV = @MaDeHV
	AND	CTDHV.MaNhom = @MaNhom 
	

ORDER BY bt.ThuTu	
			

GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaNhom]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectBy_MaNhom]
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

SELECT MaNhom FROM ChiTietDeThiHoanVi
WHERE MaNhom = @MaNhom

END

SELECT	n.* ,
		p.STT

FROM	ChiTietDeThiHoanVi n

INNER JOIN #PageIndex p
ON	p.MaNhom = n.MaNhom

WHERE	n.MaNhom = @MaNhom


DROP TABLE #PageIndex

			
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectNhom1By_MaDeHV]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectNhom1By_MaDeHV]
	@MaDeHV [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	ct.* 

FROM	ChiTietDeThiHoanVi ct

LEFT OUTER JOIN NhomCauHoiHoanVi nhv
	ON	nhv.MaDeHV = ct.MaDeHV
	AND nhv.MaNhom = ct.MaNhom

LEFT OUTER JOIN NhomCauHoi nch
	ON nch.MaNhom = ct.MaNhom

WHERE	ct.MaDeHV = @MaDeHV 
	AND	(nch.MaNhomCha = -1 OR nch.MaNhomCha IS NULL)

ORDER BY ct.ThuTu, nch.ThuTu

			
			
		
			
			
		
------------------------------------------------------------------------------------------------------------------------
-- Date Created: 24/12/2009
-- Created By:   Le Xuan Manh
------------------------------------------------------------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON


GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_SelectOne]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[ChiTietDeThiHoanVi]
WHERE
	[MaDeHV] = @MaDeHV
	AND [MaNhom] = @MaNhom
	AND [MaCauHoi] = @MaCauHoi



GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_Update]
	@MaDeHV [bigint],
	@MaNhom [int],
	@MaCauHoi [int],
	@ThuTu [int],
	@HoanViTraLoi [nvarchar](4),
	@DapAn [int]
WITH EXECUTE AS CALLER
AS
UPDATE [ChiTietDeThiHoanVi] SET
	[ThuTu] = @ThuTu,
	[HoanViTraLoi] = @HoanViTraLoi,
	[DapAn] = @DapAn
WHERE
	[MaDeHV] = @MaDeHV
	AND [MaNhom] = @MaNhom
	AND [MaCauHoi] = @MaCauHoi
 


GO
/****** Object:  StoredProcedure [dbo].[Clo_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_ForceRemove]
	@MaCLO [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION
			--Tạo bảng CauHoi
			DECLARE @CauHoi TABLE (ma_cau_hoi INT)

			-- Lấy dữ liệu vào bảng
			INSERT INTO @CauHoi (ma_cau_hoi)
			SELECT MaCauHoi FROM CauHoi WHERE MaCLO = @MaCLO

			-- Thực hiện xóa lần lượt
			DELETE FROM CauTraLoi
			WHERE MaCauHoi IN (SELECT ma_cau_hoi FROM @CauHoi)

			DELETE FROM ChiTietDeThiHoanVi
			WHERE MaCauHoi IN (SELECT ma_cau_hoi FROM @CauHoi)

			DELETE FROM CauHoi
			WHERE MaCLO = @MaCLO

			DELETE FROM CLO
			WHERE MaCLO = @MaCLO
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Clo_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_Insert]
	@MaMonHoc [int],
	@MaSoCLO [varchar](50),
	@TieuDe [nvarchar](max),
	@NoiDung [nvarchar](max),
	@TieuChi [int],
	@SoCau [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [CLO] (
	[MaMonHoc],
	[MaSoCLO],
	[TieuDe],
	[NoiDung],
	[TieuChi(%)],
	[SoCau]
) 
VALUES (
	@MaMonHoc,
	@MaSoCLO,
	@TieuDe,
	@NoiDung,
	@TieuChi,
	@SoCau
)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[Clo_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_Remove]
	@MaCLO [int]
WITH EXECUTE AS CALLER
AS

DELETE FROM [CLO]
WHERE
	[MaCLO] = @MaCLO
GO
/****** Object:  StoredProcedure [dbo].[Clo_SelectBy_MaMonHoc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_SelectBy_MaMonHoc]
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
SELECT * 
FROM [CLO]
WHERE MaMonHoc = @MaMonHoc
GO
/****** Object:  StoredProcedure [dbo].[Clo_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_SelectOne]
	@MaClo [int]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM
	[CLO]
WHERE
	[MaCLO] = @MaClo
GO
/****** Object:  StoredProcedure [dbo].[Clo_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_Update]
	@MaCLO [int],
	@MaMonHoc [int],
	@MaSoCLO [varchar](50),
	@TieuDe [nvarchar](max),
	@NoiDung [nvarchar](max),
	@TieuChi [int],
	@SoCau [int]
WITH EXECUTE AS CALLER
AS
UPDATE [CLO]
SET
	[MaMonHoc] = @MaMonHoc,
	[MaSoCLO] = @MaSoCLO,
	[TieuDe] = @TieuDe,
	[NoiDung] = @NoiDung,
	[TieuChi(%)] = @TieuChi,
	[SoCau] = @SoCau
WHERE
	[MaCLO] = @MaCLO
GO
/****** Object:  StoredProcedure [dbo].[Custom_GetDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_LayMaThongTinDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_LayMaThongTinDeThiTheoNhom]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeCauHoi_SelectBy_DeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
        chi_tiet_bai_thi CTBT
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
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeCauHoi_SelectBy_Nhom]    Script Date: 7/1/2025 3:29:36 PM ******/
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
        chi_tiet_bai_thi CTBT
    JOIN 
        CauHoi CH ON CH.MaCauHoi = CTBT.MaCauHoi
    WHERE 
        CTBT.MaNhom = @MaNhom
    GROUP BY 
        CH.MaCauHoi, CH.TieuDe, CH.MaCLO
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeDiem_SelectBy_DeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Custom_ThongKeDiem_SelectBy_DeThi]
    @MaDeThi BIGINT
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT CTCT.diem AS Diem, COUNT(*) AS SoLuong
	FROM [chi_tiet_ca_thi] CTCT
	JOIN [DeThiHoanVi] DTHV ON CTCT.ma_de_thi = DTHV.MaDeHV
	WHERE DTHV.MaDeThi = @MaDeThi AND CTCT.diem <> -1
	GROUP BY CTCT.diem
END
GO
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeNhom_SelectBy_DeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
        chi_tiet_bai_thi CTBT
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
/****** Object:  StoredProcedure [dbo].[CustomThongKeCapBacSV_SelectBy_DeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
    CT.thoi_gian_bat_dau AS NgayThi,
    COUNT(DISTINCT CTCT.ma_chi_tiet_ca_thi) AS TongSV
  FROM DeThi DT
  JOIN DeThiHoanVi DTHV ON DT.MaDeThi = DTHV.MaDeThi
  JOIN chi_tiet_ca_thi CTCT ON CTCT.ma_de_thi = DTHV.MaDeHV AND CTCT.da_hoan_thanh = 1
  JOIN ca_thi CT ON CT.MaDeThi = DT.MaDeThi
  WHERE DT.MaDeThi = @MaDeThi
  GROUP BY DT.GhiChu, DT.TenDeThi, CT.thoi_gian_bat_dau, DT.Guid

  -- 2. CTE điểm và phân loại top/bottom 27%
  ;WITH Scores AS (
    SELECT
      CTCT.ma_chi_tiet_ca_thi,
      CTCT.ma_sinh_vien,
      CTCT.diem,
      PERCENT_RANK() OVER (ORDER BY CTCT.diem) AS pr_rank
    FROM chi_tiet_ca_thi CTCT
    JOIN DeThiHoanVi DTHV ON CTCT.ma_de_thi = DTHV.MaDeHV
    WHERE DTHV.MaDeThi = @MaDeThi
      AND CTCT.da_hoan_thanh = 1
  ),
  -- Top và bottom dựa trên tiêu chí 27%
  TopBottom AS (
    SELECT
      ma_chi_tiet_ca_thi,
      ma_sinh_vien,
      diem,
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
      COUNT(DISTINCT CTBT.ma_chi_tiet_ca_thi) AS SoSVTrong27Percent,
      SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS SoSVDungTrong27Percent
    FROM TopBottom TB
    JOIN chi_tiet_bai_thi CTBT
      ON TB.ma_chi_tiet_ca_thi = CTBT.ma_chi_tiet_ca_thi
	JOIN CauHoi CH
	  ON CH.MaCauHoi = CTBT.MaCauHoi
    WHERE TB.category IS NOT NULL
    GROUP BY TB.category, CH.MaCauHoi, CH.Guid
  ),
  AllStudentsAnswers AS (
    SELECT
      CTBT.MaCauHoi,
	  SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS TongSoSVDung,
	  COUNT(DISTINCT CTCT.ma_sinh_vien) AS TongSoSVTraLoi
    FROM chi_tiet_bai_thi CTBT
    JOIN chi_tiet_ca_thi CTCT ON CTBT.ma_chi_tiet_ca_thi = CTCT.ma_chi_tiet_ca_thi
    JOIN DeThiHoanVi DTHV ON CTCT.ma_de_thi = DTHV.MaDeHV
    WHERE CTCT.da_hoan_thanh = 1
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
/****** Object:  StoredProcedure [dbo].[DeThi_AutoUpdate_Stats]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_AutoUpdate_Stats]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
DECLARE @TONGSOHV bigint

SET @TONGSOHV = (SELECT COUNT(*)
	FROM DeThiHoanVi
	WHERE	MaDeThi = @MaDeThi)

UPDATE [DeThi] 
SET
	[TongSoDeHoanVi] = @TONGSOHV
WHERE
	[MaDeThi] = @MaDeThi
 



GO
/****** Object:  StoredProcedure [dbo].[DeThi_DecreamentTongSoDeHoanVi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_DecreamentTongSoDeHoanVi]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
IF NOT EXISTS ( SELECT * FROM [DeThi] WHERE [MaDeThi] = @MaDeThi
	AND [TongSoDeHoanVi] <= 0)
BEGIN
	UPDATE [DeThi] 
	SET
		[TongSoDeHoanVi] = [TongSoDeHoanVi] - 1
	WHERE
		[MaDeThi] = @MaDeThi
 END



GO
/****** Object:  StoredProcedure [dbo].[DeThi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_Delete]
	@MaDeThi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM DeThi
	WHERE MaDeThi = @MaDeThi
END
GO
/****** Object:  StoredProcedure [dbo].[DeThi_DeleteAll]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_ForceDelete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_ForceDelete]
	@MaDeThi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Khai báo bảng tạm
		DECLARE @MaDeHV TABLE (MaDeHV INT);
		DECLARE @MaNhom TABLE (MaNhom INT);
		DECLARE @MaCauHoi TABLE (MaCauHoi INT);

		-- Lấy danh sách cần xóa vào bảng tạm
		INSERT INTO @MaDeHV (MaDeHV)
		SELECT MaDeHV FROM DeThiHoanVi WHERE MaDeThi = @MaDeThi;

		INSERT INTO @MaNhom (MaNhom)
		SELECT MaNhom FROM NhomCauHoi WHERE MaDeThi = @MaDeThi;

		INSERT INTO @MaCauHoi (MaCauHoi)
		SELECT MaCauHoi
		FROM ChiTietDeThi
		WHERE MaNhom IN (SELECT MaNhom FROM @MaNhom);


		-- Xóa theo thứ tự phụ thuộc
		DELETE FROM ChiTietDeThiHoanVi
		WHERE MaDeHV IN (SELECT MaDeHV FROM @MaDeHV);

		DELETE FROM NhomCauHoiHoanVi
		WHERE MaDeHV IN (SELECT MaDeHV FROM @MaDeHV);

		DELETE FROM DeThiHoanVi
		WHERE MaDeThi = @MaDeThi;

		DELETE FROM CauTraLoi
		WHERE MaCauHoi IN (SELECT MaCauHoi FROM @MaCauHoi);

		DELETE FROM CauHoi
		WHERE MaCauHoi IN (SELECT MaCauHoi FROM @MaCauHoi);

		DELETE FROM NhomCauHoi
		WHERE MaDeThi = @MaDeThi;

		DELETE FROM DeThi
		WHERE MaDeThi = @MaDeThi;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeThi_IncreamentTongSoDeHoanVi]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_IncreamentTongSoDeHoanVi]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
UPDATE [DeThi] 
SET
	[TongSoDeHoanVi] = [TongSoDeHoanVi] + 1
WHERE
	[MaDeThi] = @MaDeThi
 



GO
/****** Object:  StoredProcedure [dbo].[DeThi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_Insert]
	@MaMonHoc [int],
	@TenDeThi [nvarchar](250),
	@NgayTao [datetime],
	@NguoiTao [int],
	@GhiChu [nvarchar](max),
	@BoChuongPhan [bit]
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
		[NgayTao],
		[NguoiTao],
		[GhiChu],
		[BoChuongPhan],
		[DaDuyet],
		[LuuTam]
	) 
	VALUES 
	(
		@MaMonHoc,
		@TenDeThi,
		@NgayTao,
		@NguoiTao,
		@GhiChu,
		@BoChuongPhan,
		0,
		1
	)

	SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[DeThi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectBy_ma_de_hv]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectByMonHoc]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectByMonHoc_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_SelectOne]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[DeThi]
WHERE
	[MaDeThi] = @MaDeThi



GO
/****** Object:  StoredProcedure [dbo].[DeThi_SelectTongSoDeHV]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_SelectTongSoDeHV]
	@MaDeThi [int]
WITH EXECUTE AS CALLER
AS
SELECT 	TongSoDeHoanVi
FROM	[DeThi]
WHERE	[MaDeThi] = @MaDeThi



GO
/****** Object:  StoredProcedure [dbo].[DeThi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_Update]
	@MaDeThi [int],
	@MaMonHoc [int],
	@TenDeThi [nvarchar](250),
	@NgayTao [datetime],
	@NguoiTao [int],
	@GhiChu [nvarchar](max),
	@BoChuongPhan [bit],
	@DaDuyet [bit],
	@LuuTam [bit]
WITH EXECUTE AS CALLER
AS
UPDATE [DeThi] 
SET
	[MaMonHoc] = @MaMonHoc,
	[TenDeThi] = @TenDeThi,
	[NgayTao] = @NgayTao,
	[NguoiTao] = @NguoiTao,
	[GhiChu] = @GhiChu,
	[BoChuongPhan] = @BoChuongPhan,
	[LuuTam] = @LuuTam,
	[DaDuyet] = @DaDuyet
WHERE
	[MaDeThi] = @MaDeThi
GO
/****** Object:  StoredProcedure [dbo].[DeThi_Update_Stats]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_Update_Stats]
	@MaDeThi [int],
	@TongSoDeHoanVi [bigint]
WITH EXECUTE AS CALLER
AS
UPDATE [DeThi] 
SET
	[TongSoDeHoanVi] = @TongSoDeHoanVi
WHERE
	[MaDeThi] = @MaDeThi
 


GO
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_DapAn]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_ForceDelete]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectBy_MaDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_ForceRemove]
    @ma_dot_thi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION

        -- Bảng tạm lưu ID liên quan
        DECLARE @ChiTietDotThi TABLE (ma_chi_tiet_dot_thi INT)
        DECLARE @CaThi TABLE (ma_ca_thi INT)
        DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT)

        -- Lấy dữ liệu vào bảng tạm
        INSERT INTO @ChiTietDotThi (ma_chi_tiet_dot_thi)
        SELECT ma_chi_tiet_dot_thi FROM chi_tiet_dot_thi WHERE ma_dot_thi = @ma_dot_thi

        INSERT INTO @CaThi (ma_ca_thi)
        SELECT ma_ca_thi FROM ca_thi WHERE ma_chi_tiet_dot_thi IN (SELECT ma_chi_tiet_dot_thi FROM @ChiTietDotThi)

        INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
        SELECT ma_chi_tiet_ca_thi FROM chi_tiet_ca_thi WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi)

        -- Xoá theo thứ tự phụ thuộc
        DELETE FROM chi_tiet_bai_thi
        WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

		DELETE FROM AudioListened
        WHERE MaChiTietCaThi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

        DELETE FROM chi_tiet_ca_thi
        WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi)

        DELETE FROM ca_thi
        WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi)

        DELETE FROM chi_tiet_dot_thi
        WHERE ma_chi_tiet_dot_thi IN (SELECT ma_chi_tiet_dot_thi FROM @ChiTietDotThi)

        DELETE FROM dot_thi
        WHERE ma_dot_thi = @ma_dot_thi

        COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[dot_thi_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[dot_thi]

GO
/****** Object:  StoredProcedure [dbo].[dot_thi_GetAll_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_GetAll_Paged]
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
    FROM [dot_thi] DT

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [dot_thi] DT
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), thoi_gian_bat_dau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[dot_thi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_Insert]
	@ten_dot_thi [nvarchar](150),
	@thoi_gian_bat_dau [datetime],
	@thoi_gian_ket_thuc [datetime],
	@NamHoc [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [dot_thi] (
	[ten_dot_thi],
	[thoi_gian_bat_dau],
	[thoi_gian_ket_thuc],
	[NamHoc]
) VALUES (
	@ten_dot_thi,
	@thoi_gian_bat_dau,
	@thoi_gian_ket_thuc,
	@NamHoc 
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[dot_thi_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_Remove]
    @ma_dot_thi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [dot_thi]
	WHERE ma_dot_thi = @ma_dot_thi
END
GO
/****** Object:  StoredProcedure [dbo].[dot_thi_SelectByMaNamHoc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_SelectByMaNamHoc]
	@NamHoc [int]
WITH EXECUTE AS CALLER
AS
SELECT	*
FROM
	[dot_thi]
WHERE
	[NamHoc] = @NamHoc 
	 

GO
/****** Object:  StoredProcedure [dbo].[dot_thi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_SelectOne]
	@ma_dot_thi [int]
WITH EXECUTE AS CALLER
AS
SELECT	*
FROM
	[dot_thi]
WHERE
	[ma_dot_thi] = @ma_dot_thi

GO
/****** Object:  StoredProcedure [dbo].[dot_thi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_Update]
	@ma_dot_thi [int],
	@ten_dot_thi [nvarchar](150),
	@thoi_gian_bat_dau [datetime],
	@thoi_gian_ket_thuc [datetime],
	@NamHoc [int]
WITH EXECUTE AS CALLER
AS
UPDATE [dot_thi] SET
	[ten_dot_thi] = @ten_dot_thi,
	[thoi_gian_bat_dau] = @thoi_gian_bat_dau,
	[thoi_gian_ket_thuc] = @thoi_gian_ket_thuc,
	[NamHoc] = @NamHoc 
WHERE
	[ma_dot_thi] = @ma_dot_thi

GO
/****** Object:  StoredProcedure [dbo].[khoa_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_ForceRemove]
	@ma_khoa INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION

		-- Tạo bảng tạm để lưu các ID trung gian
		DECLARE @LopCuaKhoa TABLE (ma_lop INT)
		DECLARE @SinhVienTrongLop TABLE (ma_sinh_vien INT)
		DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT)

		-- Lấy dữ liệu vào bảng tạm
		INSERT INTO @LopCuaKhoa (ma_lop)
		SELECT ma_lop FROM lop WHERE ma_khoa = @ma_khoa

		INSERT INTO @SinhVienTrongLop (ma_sinh_vien)
		SELECT ma_sinh_vien FROM sinh_vien WHERE ma_lop IN (SELECT ma_lop FROM @LopCuaKhoa)

		INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
		SELECT ma_chi_tiet_ca_thi FROM chi_tiet_ca_thi WHERE ma_sinh_vien IN (SELECT ma_sinh_vien FROM @SinhVienTrongLop)

		-- Thực hiện xóa theo thứ tự phụ thuộc
		DELETE FROM chi_tiet_bai_thi
		WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

		DELETE FROM chi_tiet_ca_thi
		WHERE ma_sinh_vien IN (SELECT ma_sinh_vien FROM @SinhVienTrongLop)

		DELETE FROM sinh_vien
		WHERE ma_sinh_vien IN (SELECT ma_sinh_vien FROM @SinhVienTrongLop)

		DELETE FROM lop
		WHERE ma_lop IN (SELECT ma_lop FROM @LopCuaKhoa)

		DELETE FROM khoa
		WHERE ma_khoa = @ma_khoa

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[khoa_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[khoa]

GO
/****** Object:  StoredProcedure [dbo].[khoa_GetAll_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_GetAll_Paged]
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
    FROM [khoa] Khoa

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [Khoa] Khoa
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), ngay_thanh_lap)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[khoa_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_Insert]
	@ten_khoa [nvarchar](30),
	@ngay_thanh_lap [datetime]
WITH EXECUTE AS CALLER
AS
INSERT INTO [khoa] (
	[ten_khoa],
	[ngay_thanh_lap]
) VALUES (
	@ten_khoa,
	@ngay_thanh_lap
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[khoa_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_Remove]
	@ma_khoa INT
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [khoa]
	WHERE ma_khoa = @ma_khoa
END
GO
/****** Object:  StoredProcedure [dbo].[khoa_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_SelectOne]
	@ma_khoa [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[ma_khoa],
	[ten_khoa],
	[ngay_thanh_lap]
FROM
	[khoa]
WHERE
	[ma_khoa] = @ma_khoa

GO
/****** Object:  StoredProcedure [dbo].[khoa_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_Update]
	@ma_khoa [int],
	@ten_khoa [nvarchar](30),
	@ngay_thanh_lap [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [khoa] SET
	[ten_khoa] = @ten_khoa,
	[ngay_thanh_lap] = @ngay_thanh_lap
WHERE
	[ma_khoa] = @ma_khoa

GO
/****** Object:  StoredProcedure [dbo].[lop_ao_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_ForceRemove]
	@ma_lop_ao INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Bảng biến lưu các khóa liên quan
		DECLARE @ChiTietDotThi TABLE (ma_chi_tiet_dot_thi INT);
		DECLARE @CaThi TABLE (ma_ca_thi INT);
		DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT);

		-- Lấy danh sách cần xóa theo quan hệ
		INSERT INTO @ChiTietDotThi (ma_chi_tiet_dot_thi)
		SELECT ma_chi_tiet_dot_thi
		FROM chi_tiet_dot_thi
		WHERE ma_lop_ao = @ma_lop_ao;

		INSERT INTO @CaThi (ma_ca_thi)
		SELECT ma_ca_thi
		FROM ca_thi
		WHERE ma_chi_tiet_dot_thi IN (SELECT ma_chi_tiet_dot_thi FROM @ChiTietDotThi);

		INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
		SELECT ma_chi_tiet_ca_thi
		FROM chi_tiet_ca_thi
		WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi);

		-- Xóa theo thứ tự phụ thuộc
		DELETE FROM chi_tiet_bai_thi
		WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi);

		DELETE FROM chi_tiet_ca_thi
		WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi);

		DELETE FROM ca_thi
		WHERE ma_chi_tiet_dot_thi IN (SELECT ma_chi_tiet_dot_thi FROM @ChiTietDotThi);

		DELETE FROM chi_tiet_dot_thi
		WHERE ma_lop_ao = @ma_lop_ao;

		-- Cuối cùng xóa lớp ảo
		DELETE FROM lop_ao
		WHERE ma_lop_ao = @ma_lop_ao;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[lop_ao_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[lop_ao] LA LEFT JOIN [mon_hoc] MH ON LA.ma_mon_hoc=MH.ma_mon_hoc


GO
/****** Object:  StoredProcedure [dbo].[lop_ao_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_Insert]
	@ten_lop_ao [nvarchar](200),
	@ngay_bat_dau [datetime],
	@ma_mon_hoc [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [lop_ao] (
	[ten_lop_ao],
	[ngay_bat_dau],
	[ma_mon_hoc]
) VALUES (
	@ten_lop_ao,
	@ngay_bat_dau,
	@ma_mon_hoc
)

SELECT @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[lop_ao_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_Remove]
	@ma_lop_ao [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [lop_ao]
WHERE
	[ma_lop_ao] = @ma_lop_ao

GO
/****** Object:  StoredProcedure [dbo].[lop_ao_SelectBy_ma_mon_hoc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_SelectBy_ma_mon_hoc]
	@ma_mon_hoc [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM lop_ao
WHERE ma_mon_hoc = @ma_mon_hoc

GO
/****** Object:  StoredProcedure [dbo].[lop_ao_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_SelectOne]
	@ma_lop_ao [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[ma_lop_ao],
	[ten_lop_ao],
	[ngay_bat_dau],
	[ma_mon_hoc]
FROM
	[lop_ao]
WHERE
	[ma_lop_ao] = @ma_lop_ao


GO
/****** Object:  StoredProcedure [dbo].[lop_ao_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_Update]
	@ma_lop_ao [int],
	@ten_lop_ao [nvarchar](200),
	@ngay_bat_dau [datetime],
	@ma_mon_hoc [int]
WITH EXECUTE AS CALLER
AS
UPDATE [lop_ao] SET
	[ten_lop_ao] = @ten_lop_ao,
	[ngay_bat_dau] = @ngay_bat_dau,
	[ma_mon_hoc] = @ma_mon_hoc
WHERE
	[ma_lop_ao] = @ma_lop_ao
 

GO
/****** Object:  StoredProcedure [dbo].[lop_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ForceRemove]
	@ma_lop [int]
WITH EXECUTE AS CALLER
AS
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
	BEGIN TRANSACTION

	-- Tạo bảng tạm để lưu các ID trung gian
	DECLARE @SinhVienTrongLop TABLE (ma_sinh_vien INT)
	DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT)

	-- Lấy dữ liệu vào bảng tạm
	INSERT INTO @SinhVienTrongLop (ma_sinh_vien)
	SELECT ma_sinh_vien FROM sinh_vien WHERE ma_lop = @ma_lop

	INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
	SELECT ma_chi_tiet_ca_thi FROM chi_tiet_ca_thi WHERE ma_sinh_vien IN (SELECT ma_sinh_vien FROM @SinhVienTrongLop)

	-- Thực hiện xóa theo thứ tự phụ thuộc
	DELETE FROM chi_tiet_bai_thi
	WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

	DELETE FROM chi_tiet_ca_thi
	WHERE ma_sinh_vien IN (SELECT ma_sinh_vien FROM @SinhVienTrongLop)

	DELETE FROM sinh_vien
	WHERE ma_sinh_vien IN (SELECT ma_sinh_vien FROM @SinhVienTrongLop)

	DELETE FROM lop
	WHERE ma_lop = @ma_lop

	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[lop_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[lop] L LEFT JOIN [khoa] K ON L.ma_khoa = K.ma_khoa

GO
/****** Object:  StoredProcedure [dbo].[lop_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_Insert]
	@ten_lop [nvarchar](50),
	@ngay_bat_dau [datetime],
	@ma_khoa [int]
WITH EXECUTE AS CALLER
AS
INSERT INTO [lop] (
	[ten_lop],
	[ngay_bat_dau],
	[ma_khoa]
) VALUES (
	@ten_lop,
	@ngay_bat_dau,
	@ma_khoa
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[lop_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_Remove]
	@ma_lop [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [lop]
	WHERE ma_lop = @ma_lop
END
GO
/****** Object:  StoredProcedure [dbo].[lop_SelectBy_ma_khoa]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_SelectBy_ma_khoa]
	@ma_khoa [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM lop
WHERE ma_khoa = @ma_khoa

GO
/****** Object:  StoredProcedure [dbo].[lop_SelectBy_ma_khoa_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_SelectBy_ma_khoa_Paged]
	@ma_khoa [int],
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
    FROM [lop] Lop
	INNER JOIN [khoa] ON lop.ma_khoa = khoa.ma_khoa
	WHERE khoa.ma_khoa = @ma_khoa

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT lop.*
    FROM [lop] Lop
	INNER JOIN [khoa] ON lop.ma_khoa = khoa.ma_khoa
	WHERE khoa.ma_khoa = @ma_khoa
    ORDER BY ABS(DATEDIFF(SECOND, GETDATE(), lop.ngay_bat_dau)) -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[lop_SelectBy_ten_lop]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_SelectBy_ten_lop]
	@ten_lop [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM lop
WHERE ten_lop = @ten_lop

GO
/****** Object:  StoredProcedure [dbo].[lop_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_SelectOne]
	@ma_lop [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[ma_lop],
	[ten_lop],
	[ngay_bat_dau],
	[ma_khoa]
FROM
	[lop]
WHERE
	[ma_lop] = @ma_lop

GO
/****** Object:  StoredProcedure [dbo].[lop_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_Update]
	@ma_lop [int],
	@ten_lop [nvarchar](50),
	@ngay_bat_dau [datetime],
	@ma_khoa [int]
WITH EXECUTE AS CALLER
AS
UPDATE [lop] SET
	[ten_lop] = @ten_lop,
	[ngay_bat_dau] = @ngay_bat_dau,
	[ma_khoa] = @ma_khoa
WHERE
	[ma_lop] = @ma_lop

GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_ForceRemove]
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Giai đoạn 1: Xóa dữ liệu liên quan đến đề thi

		DECLARE @DeThi TABLE (MaDeThi INT);
		DECLARE @DeThiHoanVi TABLE (MaDeHV INT);
		DECLARE @NhomCauHoi TABLE (MaNhom INT);
		DECLARE @CauHoi TABLE (MaCauHoi INT);

		-- Lấy danh sách cần xóa
		INSERT INTO @DeThi (MaDeThi)
		SELECT MaDeThi FROM DeThi WHERE MaMonHoc = @MaMonHoc;

		INSERT INTO @DeThiHoanVi (MaDeHV)
		SELECT MaDeHV FROM DeThiHoanVi WHERE MaDeThi IN (SELECT MaDeThi FROM @DeThi);

		INSERT INTO @NhomCauHoi (MaNhom)
		SELECT MaNhom FROM NhomCauHoi WHERE MaDeThi IN (SELECT MaDeThi FROM @DeThi);

		INSERT INTO @CauHoi (MaCauHoi)
		SELECT MaCauHoi
		FROM ChiTietDeThi
		WHERE MaNhom IN (SELECT MaNhom FROM @NhomCauHoi);


		-- Xóa dữ liệu
		DELETE FROM ChiTietDeThiHoanVi
		WHERE MaDeHV IN (SELECT MaDeHV FROM @DeThiHoanVi);

		DELETE FROM NhomCauHoiHoanVi
		WHERE MaDeHV IN (SELECT MaDeHV FROM @DeThiHoanVi);

		DELETE FROM DeThiHoanVi
		WHERE MaDeThi IN (SELECT MaDeThi FROM @DeThi);

		DELETE FROM CauTraLoi
		WHERE MaCauHoi IN (SELECT MaCauHoi FROM @CauHoi);

		DELETE FROM CauHoi
		WHERE MaCauHoi IN (SELECT MaCauHoi FROM @CauHoi);

		DELETE FROM NhomCauHoi
		WHERE MaDeThi IN (SELECT MaDeThi FROM @DeThi);

		DELETE FROM DeThi
		WHERE MaDeThi IN (SELECT MaDeThi FROM @DeThi);

		DELETE FROM CLO
		WHERE MaMonHoc = @MaMonHoc;


		-- Giai đoạn 2: Xóa dữ liệu liên quan đến lớp ảo và thi

		DECLARE @LopAo TABLE (ma_lop_ao INT);
		DECLARE @ChiTietDotThi TABLE (ma_chi_tiet_dot_thi INT);
		DECLARE @CaThi TABLE (ma_ca_thi INT);
		DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT);

		-- Lấy danh sách cần xóa
		INSERT INTO @LopAo (ma_lop_ao)
		SELECT ma_lop_ao FROM lop_ao WHERE ma_mon_hoc = @MaMonHoc;

		INSERT INTO @ChiTietDotThi (ma_chi_tiet_dot_thi)
		SELECT ma_chi_tiet_dot_thi
		FROM chi_tiet_dot_thi
		WHERE ma_lop_ao IN (SELECT ma_lop_ao FROM @LopAo);

		INSERT INTO @CaThi (ma_ca_thi)
		SELECT ma_ca_thi
		FROM ca_thi
		WHERE ma_chi_tiet_dot_thi IN (SELECT ma_chi_tiet_dot_thi FROM @ChiTietDotThi);

		INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
		SELECT ma_chi_tiet_ca_thi
		FROM chi_tiet_ca_thi
		WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi);


		-- Xóa dữ liệu
		DELETE FROM chi_tiet_bai_thi
		WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi);

		DELETE FROM chi_tiet_ca_thi
		WHERE ma_ca_thi IN (SELECT ma_ca_thi FROM @CaThi);

		DELETE FROM ca_thi
		WHERE ma_chi_tiet_dot_thi IN (SELECT ma_chi_tiet_dot_thi FROM @ChiTietDotThi);

		DELETE FROM chi_tiet_dot_thi
		WHERE ma_lop_ao IN (SELECT ma_lop_ao FROM @LopAo);

		DELETE FROM lop_ao
		WHERE ma_mon_hoc = @MaMonHoc;

		-- Cuối cùng xóa môn học
		DELETE FROM mon_hoc
		WHERE ma_mon_hoc = @MaMonHoc;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[mon_hoc]

GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_GetAll_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_GetAll_Paged]
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
    FROM [mon_hoc]

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [mon_hoc] MH
    ORDER BY MH.ma_mon_hoc DESC -- gần ngày tạo nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_Insert]
	@ma_so_mon_hoc [nvarchar](50),
	@ten_mon_hoc [nvarchar](200)
WITH EXECUTE AS CALLER
AS
INSERT INTO [mon_hoc] (
	[ma_so_mon_hoc],
	[ten_mon_hoc]
) VALUES (
	@ma_so_mon_hoc,
	@ten_mon_hoc
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_Remove]
	@ma_mon_hoc [int]
WITH EXECUTE AS CALLER
AS
DELETE FROM [mon_hoc]
WHERE
	[ma_mon_hoc] = @ma_mon_hoc

GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_SelectBy_MaSoMonHoc]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_SelectBy_MaSoMonHoc]
	@ma_so_mon_hoc [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SELECT
	[ma_mon_hoc],
	[ma_so_mon_hoc],
	[ten_mon_hoc]
FROM
	[mon_hoc]
WHERE
	lower([ma_so_mon_hoc]) = lower(@ma_so_mon_hoc)

GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_SelectOne]
	@ma_mon_hoc [int]
WITH EXECUTE AS CALLER
AS
SELECT
	[ma_mon_hoc],
	[ma_so_mon_hoc],
	[ten_mon_hoc]
FROM
	[mon_hoc]
WHERE
	[ma_mon_hoc] = @ma_mon_hoc

GO
/****** Object:  StoredProcedure [dbo].[mon_hoc_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_Update]
	@ma_mon_hoc [int],
	@ma_so_mon_hoc [nvarchar](50),
	@ten_mon_hoc [nvarchar](200)
WITH EXECUTE AS CALLER
AS
UPDATE [mon_hoc] SET
	[ma_so_mon_hoc] = @ma_so_mon_hoc,
	[ten_mon_hoc] = @ten_mon_hoc
WHERE
	[ma_mon_hoc] = @ma_mon_hoc

GO
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_ForceDelete]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_IsCauHoiNhom]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectAllBy_MaDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectBy_MaDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectBy_MaNhomCha]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectHoanViBy_MaDeThi]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_CountBy_MaNhomCha]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaDeHV]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhom]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhomCha]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectPageBy_MaNhomCha]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_ForceRemove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_ForceRemove]
	@ma_sinh_vien [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
	BEGIN TRANSACTION

	-- Tạo bảng tạm để lưu các ID trung gian
	DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT)

	-- Lấy dữ liệu vào bảng tạm
	INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
	SELECT ma_chi_tiet_ca_thi FROM chi_tiet_ca_thi WHERE ma_sinh_vien = @ma_sinh_vien

	-- Thực hiện xóa theo thứ tự phụ thuộc
	DELETE FROM chi_tiet_bai_thi
	WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

	DELETE FROM AudioListened
	WHERE MaChiTietCaThi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

	DELETE FROM chi_tiet_ca_thi
	WHERE ma_sinh_vien = @ma_sinh_vien

	DELETE FROM sinh_vien
	WHERE ma_sinh_vien = @ma_sinh_vien

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_GetAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_GetAll]
WITH EXECUTE AS CALLER
AS
SELECT
	*
FROM
	[sinh_vien] SV LEFT JOIN [lop] L ON SV.ma_lop = L.ma_lop
LEFT JOIN [khoa] K ON L.ma_khoa = K.ma_khoa


GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_Insert]
	@ho_va_ten_lot [nvarchar](300) = NULL,
	@ten_sinh_vien [nvarchar](50),
	@gioi_tinh [smallint] = NULL,
	@ngay_sinh [datetime] = NULL,
	@ma_lop [int] = NULL,
	@dia_chi [nvarchar](max) = NULL,
	@email [nvarchar](200) = NULL,
	@dien_thoai [nvarchar](50) = NULL,
	@ma_so_sinh_vien [nvarchar](50),
	@student_id [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
-- THIS STORED PROCEDURE NEEDS TO BE MANUALLY COMPLETED
-- MULITPLE PRIMARY KEY MEMBERS OR NON-GUID/INT PRIMARY KEY

INSERT INTO [sinh_vien] (
	[ho_va_ten_lot],
	[ten_sinh_vien],
	[gioi_tinh],
	[ngay_sinh],
	[ma_lop],
	[dia_chi],
	[email],
	[dien_thoai],
	[ma_so_sinh_vien],
	[student_id]
) VALUES (
	@ho_va_ten_lot,
	@ten_sinh_vien,
	@gioi_tinh,
	@ngay_sinh,
	@ma_lop,
	@dia_chi,
	@email,
	@dien_thoai,
	@ma_so_sinh_vien,
	@student_id
)

select @@identity
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_Insert_Batch]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_Insert_Batch]
	@DanhSachSinhVien SinhVienType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION;
	SET NOCOUNT ON;

    -- Chèn sinh viên mới nếu chưa có
    INSERT INTO sinh_vien (ma_so_sinh_vien, ho_va_ten_lot, ten_sinh_vien, gioi_tinh, ngay_sinh, ma_lop, dia_chi, email, dien_thoai, student_id)
    SELECT s.MaSoSinhVien, s.HoVaTenLot, s.TenSinhVien, s.GioiTinh, s.NgaySinh, s.MaLop, s.DiaChi, s.Email, s.DienThoai, s.StudentId
    FROM @DanhSachSinhVien s
    WHERE NOT EXISTS (
        SELECT 1 FROM sinh_vien sv WHERE sv.ma_so_sinh_vien = s.MaSoSinhVien
    );
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_Login]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_Login]
	@ma_sinh_vien [bigint],
	@last_logged_in [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [sinh_vien] 
SET
	[is_logged_in] = 1,
	[last_logged_in] = @last_logged_in
WHERE
	[ma_sinh_vien] = @ma_sinh_vien
 

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_Logout]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_Logout]
	@ma_sinh_vien [bigint],
	@last_logged_out [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [sinh_vien] 
SET
	[is_logged_in] = 0,
	[last_logged_out] = @last_logged_out
WHERE
	[ma_sinh_vien] = @ma_sinh_vien
 

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_Remove]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_Remove]
	@ma_sinh_vien [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
	DELETE FROM [sinh_vien]
	WHERE ma_sinh_vien = @ma_sinh_vien
END
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_khoa]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_khoa]
	@ma_khoa [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM sinh_vien SV JOIN LOP L ON SV.ma_lop=L.ma_lop
JOIN khoa K ON L.ma_khoa=K.ma_khoa
WHERE K.ma_khoa = @ma_khoa

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_khoa_ma_lop]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_khoa_ma_lop]
	@ma_khoa [int] = NULL,
	@ma_lop [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM sinh_vien SV JOIN LOP L ON SV.ma_lop=L.ma_lop
JOIN khoa K ON L.ma_khoa=K.ma_khoa
WHERE K.ma_khoa = @ma_khoa AND L.ma_lop = @ma_lop

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_lop]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_lop]
	@ma_lop [int] = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM sinh_vien SV JOIN lop L ON SV.ma_lop = L.ma_lop
LEFT JOIN khoa K ON K.ma_khoa = L.ma_khoa
WHERE L.ma_lop = @ma_lop

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_lop_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_lop_Paged]
	@ma_lop [int],
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
    FROM [sinh_vien] SV
	INNER JOIN [lop] LP ON SV.ma_lop = LP.ma_lop
	WHERE LP.ma_lop = @ma_lop

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [sinh_vien] SV
	INNER JOIN [lop] LP ON SV.ma_lop = LP.ma_lop
	WHERE LP.ma_lop = @ma_lop
    ORDER BY SV.ma_so_sinh_vien -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_lop_Search_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_lop_Search_Paged]
	@ma_lop [int],
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
    FROM [sinh_vien] SV
	INNER JOIN [lop] LP ON SV.ma_lop = LP.ma_lop
	WHERE LP.ma_lop = @ma_lop
	AND(
		SV.ma_so_sinh_vien LIKE '%' + @Keyword + '%'
		OR SV.ho_va_ten_lot LIKE '%' + @Keyword + '%'
		OR SV.ten_sinh_vien LIKE '%' + @Keyword + '%'
	)

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [sinh_vien] SV
	INNER JOIN [lop] LP ON SV.ma_lop = LP.ma_lop
	WHERE LP.ma_lop = @ma_lop
	AND(
		SV.ma_so_sinh_vien LIKE '%' + @Keyword + '%'
		OR SV.ho_va_ten_lot LIKE '%' + @Keyword + '%'
		OR SV.ten_sinh_vien LIKE '%' + @Keyword + '%'
	)
    ORDER BY SV.ma_so_sinh_vien -- gần thời gian hiện tại nhất
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_so_sinh_vien]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_so_sinh_vien]
	@ma_so_sinh_vien [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
SELECT * 
FROM sinh_vien 
WHERE ma_so_sinh_vien = @ma_so_sinh_vien

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_so_sinh_vien1]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ma_so_sinh_vien1]
	@ma_so_sinh_vien [nvarchar](50) = NULL
WITH EXECUTE AS CALLER
AS
SELECT * FROM sinh_vien
WHERE ma_so_sinh_vien = @ma_so_sinh_vien

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_student_id]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_student_id]
	@student_id [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	[sinh_vien]
WHERE	[student_id] = @student_id

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ten]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectBy_ten]
	@ten [nvarchar](1) = NULL
WITH EXECUTE AS CALLER
AS
SELECT	* 
FROM	sinh_vien
WHERE	(ho_va_ten_lot + ' ' + ten_sinh_vien) like @ten

GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_SelectOne]
	@ma_sinh_vien [bigint]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM
	[sinh_vien]
WHERE
	[ma_sinh_vien] = @ma_sinh_vien


GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_Update]
	@ma_sinh_vien [bigint],
	@ho_va_ten_lot [nvarchar](300),
	@ten_sinh_vien [nvarchar](50),
	@gioi_tinh [smallint],
	@ngay_sinh [datetime],
	@ma_lop [int],
	@dia_chi [nvarchar](max),
	@email [nvarchar](200),
	@dien_thoai [nvarchar](50),
	@ma_so_sinh_vien [nvarchar](50)
WITH EXECUTE AS CALLER
AS
UPDATE [sinh_vien] SET
	[ho_va_ten_lot] = @ho_va_ten_lot,
	[ten_sinh_vien] = @ten_sinh_vien,
	[gioi_tinh] = @gioi_tinh,
	[ngay_sinh] = @ngay_sinh,
	[ma_lop] = @ma_lop,
	[dia_chi] = @dia_chi,
	[email] = @email,
	[dien_thoai] = @dien_thoai,
	[ma_so_sinh_vien] = @ma_so_sinh_vien
WHERE
	[ma_sinh_vien] = @ma_sinh_vien
 

GO
/****** Object:  StoredProcedure [dbo].[User_CheckName]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_CheckName]
	@LoginName NVARCHAR(50),
    @Email NVARCHAR(50)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM [User]
        WHERE 
            (@LoginName IS NOT NULL AND LoginName = @LoginName)
            OR
            (@Email IS NOT NULL AND Email = @Email)
    )
        RETURN 1; -- Có trùng

    RETURN 0; -- Không trùng
END
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Delete]
	@UserId [uniqueidentifier]
WITH EXECUTE AS CALLER
AS

UPDATE [User] 
SET
	[IsDeleted] = 1
WHERE
	[UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[User_GetAll_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_GetAll_Paged]
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
    FROM [User] U
	JOIN [Role] R ON U.MaRole = R.MaRole

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [User] U
	JOIN [Role] R ON U.MaRole = R.MaRole
    ORDER BY DateCreated -- thời gian tạo
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[User_GetAll_Search_Paged]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_GetAll_Search_Paged]
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
    FROM [User] U
	JOIN [Role] R ON U.MaRole = R.MaRole
	WHERE U.Name LIKE '%' + @Keyword + '%' OR U.Email LIKE '%' + @Keyword + '%' 

    -- Tính tổng số trang
    SET @TotalPages = @TotalRecords / @PageSize;
    SET @Remainder = @TotalRecords % @PageSize;
    IF @Remainder > 0
        SET @TotalPages = @TotalPages + 1;

    -- Truy vấn dữ liệu phân trang
    SELECT *
    FROM [User] U
	JOIN [Role] R ON U.MaRole = R.MaRole
	WHERE U.Name LIKE '%' + @Keyword + '%' OR U.Email LIKE '%' + @Keyword + '%'
    ORDER BY DateCreated -- thời gian tạo
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Trả về thông tin tổng số bản ghi và số trang
    SELECT 
        @TotalRecords AS TotalRecords, 
        @TotalPages AS TotalPages;
END
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Insert]
	@MaRole [int],
	@LoginName [nvarchar](100),
	@Email [nvarchar](100),
	@Name [nvarchar](255),
	@Password [nvarchar](128),
	@DateCreated [datetime],
	@Comment [nvarchar](255)
WITH EXECUTE AS CALLER
AS
DECLARE @UserId uniqueidentifier = NEWID();
INSERT INTO [User] (
	[UserId],
	[MaRole],
	[LoginName],
	[Email],
	[Name],
	[Password],
	[DateCreated],
	[Comment],
	[IsBuildInUser]
) 
VALUES 
(
	@UserId,
	@MaRole,
	@LoginName,
	@Email,
	@Name,
	@Password,
	@DateCreated,
	@Comment,
	0
)
SELECT @UserId
GO
/****** Object:  StoredProcedure [dbo].[User_Login]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Login]
	@LoginName [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT [UserId], [Name], [Password], r.TenRole
	FROM [User] u, [Role] r
	WHERE [LoginName] = @LoginName AND u.[MaRole] = r.[MaRole]

	UPDATE [User]
	SET [LastLoginDate] = GETDATE()
	WHERE [LoginName] = @LoginName OR [Email] = @LoginName
END
GO
/****** Object:  StoredProcedure [dbo].[User_LoginFail]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_LoginFail]
	@UserId [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
IF EXISTS(
	SELECT * FROM [dbo].[User]
	WHERE [UserId] = @UserId AND [FailedPwdAttemptCount] = 10
	)
	-- Nhap sai tren 10 lan bi xoa tai khoan tam thoi
	BEGIN
		UPDATE [User]
		SET [IsLockedOut] = 1, [LastLockoutDate] = GETDATE()
		WHERE [UserId] = @UserId
	END
ELSE
	BEGIN
	UPDATE [User]
	SET [FailedPwdAnswerWindowStart] = CURRENT_TIMESTAMP,
		[FailedPwdAttemptCount] = [FailedPwdAttemptCount] + 1
	WHERE [UserId] = @UserId
	END
END
GO
/****** Object:  StoredProcedure [dbo].[User_LoginSuccess]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_LoginSuccess]
	@UserId [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	UPDATE [User]
	SET [LastLoginDate] = CURRENT_TIMESTAMP, [FailedPwdAttemptCount] = 0
	WHERE [UserId] = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[User_SelectAll]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_SelectAll]
WITH EXECUTE AS CALLER
AS
SELECT	u.*

FROM	[User] u

ORDER BY u.[IsBuildInUser] DESC, u.[LoginName], u.[Name]




GO
/****** Object:  StoredProcedure [dbo].[User_SelectByLoginName]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_SelectByLoginName]
	@LoginName [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SELECT	u.*

FROM	[User] u

WHERE	u.[LoginName] = @LoginName




GO
/****** Object:  StoredProcedure [dbo].[User_SelectOne]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_SelectOne]
	@UserId [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
SELECT 	*
FROM	[User]
WHERE	[UserId] = @UserId





GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Update]
	@UserId [uniqueidentifier],
	@MaRole [int],
	@LoginName [nvarchar](100),
	@Email [nvarchar](100),
	@Name [nvarchar](255),
	@IsDeleted [bit],
	@IsLockedOut [bit],
	@Comment [nvarchar](255)
WITH EXECUTE AS CALLER
AS
UPDATE [User] 
SET
	[LoginName] = CASE WHEN @LoginName IS NULL THEN [LoginName] ELSE @LoginName END,
	[MaRole] = CASE WHEN @MaRole IS NULL THEN [MaRole] ELSE @MaRole END,
	[Email] = CASE WHEN @Email IS NULL THEN [Email] ELSE @Email END,
	[Name] = CASE WHEN @Name IS NULL THEN [Name] ELSE @Name END,
	[IsDeleted] = CASE WHEN @IsDeleted IS NULL THEN [IsDeleted] ELSE @IsDeleted END,
	[IsLockedOut] = CASE WHEN @IsLockedOut IS NULL THEN [IsLockedOut] ELSE @IsLockedOut END,
	[Comment] = CASE WHEN @Comment IS NULL THEN [Comment] ELSE @Comment END
WHERE
	[UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[User_UpdateLastActivity]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_UpdateLastActivity]
	@UserId [uniqueidentifier],
	@LastActiviyDate [datetime]
WITH EXECUTE AS CALLER
AS
UPDATE [User] 
SET
	[LastActivityDate] = CASE WHEN @LastActiviyDate IS NULL THEN [LastActivityDate] ELSE @LastActiviyDate END
WHERE
	[UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[User_UpdatePassword]    Script Date: 7/1/2025 3:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_UpdatePassword]
	@UserId [uniqueidentifier],
	@Password [nvarchar](128),
	@LastPasswordChangedDate DATETIME
WITH EXECUTE AS CALLER
AS
UPDATE [User] 
SET
	[Password] = @Password,
	[LastPasswordChangedDate] = @LastPasswordChangedDate
WHERE
	[UserId] = @UserId
 
GO
USE [master]
GO
ALTER DATABASE [HutechExam2025] SET  READ_WRITE 
GO
