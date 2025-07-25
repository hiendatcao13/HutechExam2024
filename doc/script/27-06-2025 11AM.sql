USE [master]
GO
/****** Object:  Database [HutechExam2024]    Script Date: 6/27/2025 2:26:35 PM ******/
CREATE DATABASE [HutechExam2024]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HutechExam2024', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\HutechExam2024.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HutechExam2024_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\HutechExam2024_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HutechExam2024] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HutechExam2024].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HutechExam2024] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HutechExam2024] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HutechExam2024] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HutechExam2024] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HutechExam2024] SET ARITHABORT OFF 
GO
ALTER DATABASE [HutechExam2024] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HutechExam2024] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HutechExam2024] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HutechExam2024] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HutechExam2024] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HutechExam2024] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HutechExam2024] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HutechExam2024] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HutechExam2024] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HutechExam2024] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HutechExam2024] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HutechExam2024] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HutechExam2024] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HutechExam2024] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HutechExam2024] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HutechExam2024] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HutechExam2024] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HutechExam2024] SET RECOVERY FULL 
GO
ALTER DATABASE [HutechExam2024] SET  MULTI_USER 
GO
ALTER DATABASE [HutechExam2024] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HutechExam2024] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HutechExam2024] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HutechExam2024] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HutechExam2024] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HutechExam2024] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HutechExam2024', N'ON'
GO
ALTER DATABASE [HutechExam2024] SET QUERY_STORE = ON
GO
ALTER DATABASE [HutechExam2024] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HutechExam2024]
GO
/****** Object:  UserDefinedTableType [dbo].[ChiTietBaiThiType]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  UserDefinedTableType [dbo].[DeThiType]    Script Date: 6/27/2025 2:26:35 PM ******/
CREATE TYPE [dbo].[DeThiType] AS TABLE(
	[MaNhom] [int] NULL,
	[ThuTuNhom] [int] NULL,
	[MaCauHoi] [int] NULL,
	[ThuTuCauHoi] [int] NULL,
	[HoanViTraLoi] [nvarchar](4) NULL,
	[DapAn] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SinhVienCaThiType]    Script Date: 6/27/2025 2:26:35 PM ******/
CREATE TYPE [dbo].[SinhVienCaThiType] AS TABLE(
	[MaSoSinhVien] [nvarchar](50) NULL,
	[MaCaThi] [int] NULL,
	[MaDeThi] [bigint] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SinhVienType]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[chi_tiet_ca_thi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[DeThiHoanVi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  View [dbo].[v_index_diem_de_thi]    Script Date: 6/27/2025 2:26:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_index_diem_de_thi] AS
SELECT 
    DTHV.MaDeThi,
    CTCT.diem
FROM chi_tiet_ca_thi CTCT
JOIN DeThiHoanVi DTHV ON DTHV.MaDeHV = CTCT.ma_de_thi
WHERE 
    CTCT.da_hoan_thanh = 1 -- chỉ hiển thị những người đã hoàn thành thi
GO
/****** Object:  Table [dbo].[AudioListened]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[ca_thi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[CauHoi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
	[GhiChu] [nvarchar](100) NULL,
	[HoanVi] [bit] NOT NULL,
 CONSTRAINT [PK_CauHoi] PRIMARY KEY CLUSTERED 
(
	[MaCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CauTraLoi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[chi_tiet_bai_thi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[chi_tiet_dot_thi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[ChiTietDeThi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[ChiTietDeThiHoanVi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[CLO]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[DeThi]    Script Date: 6/27/2025 2:26:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeThi](
	[MaDeThi] [int] IDENTITY(1,1) NOT NULL,
	[MaMonHoc] [int] NOT NULL,
	[TenDeThi] [nvarchar](250) NOT NULL,
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
/****** Object:  Table [dbo].[dot_thi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[khoa]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[LoiDeThi]    Script Date: 6/27/2025 2:26:35 PM ******/
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
/****** Object:  Table [dbo].[lop]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[lop_ao]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[mon_hoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[NhomCauHoi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[NhomCauHoiHoanVi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[sinh_vien]    Script Date: 6/27/2025 2:26:36 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien_DuPhong]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[ThongBao]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 6/27/2025 2:26:36 PM ******/
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
SET IDENTITY_INSERT [dbo].[AudioListened] ON 
GO
INSERT [dbo].[AudioListened] ([ListenID], [MaChiTietCaThi], [MaNhom], [FileName], [ListenedCount]) VALUES (10020, 3, 14, NULL, 2)
GO
INSERT [dbo].[AudioListened] ([ListenID], [MaChiTietCaThi], [MaNhom], [FileName], [ListenedCount]) VALUES (10027, 1, 14, NULL, 3)
GO
SET IDENTITY_INSERT [dbo].[AudioListened] OFF
GO
SET IDENTITY_INSERT [dbo].[ca_thi] ON 
GO
INSERT [dbo].[ca_thi] ([ma_ca_thi], [ten_ca_thi], [ma_chi_tiet_dot_thi], [thoi_gian_bat_dau], [MaDeThi], [IsActivated], [ActivatedDate], [ThoiGianThi], [KetThuc], [ThoiDiemKetThuc], [MatMa], [Approved], [ApprovedDate], [ApprovedComments]) VALUES (1, N'Thứ 5, 19/12/2024 - 10:10:00 PM ', 1, CAST(N'2025-06-26T09:15:00.000' AS DateTime), 2, 1, CAST(N'2025-06-24T10:15:45.363' AS DateTime), 90, 0, NULL, N'$2y$12$CkTu07u/TXgRPVMSqVqSfOlPemSDSBil8NMtxKRXlYW4MQSrWrOey', 1, CAST(N'2024-12-10' AS Date), NULL)
GO
SET IDENTITY_INSERT [dbo].[ca_thi] OFF
GO
SET IDENTITY_INSERT [dbo].[CauHoi] ON 
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (1, 2, 1, N'Phần 1: phát âm', -1, N'Choose difference sound', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (2, 3, 1, N'Phần 1: phát âm', -1, N'Choose difference sound', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (3, 1, 1, N'Phần 2: ngữ pháp', -1, N'I _______ there once a long time ago and _______ back since.', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (4, 2, 1, N'Phần 2: ngữ pháp', -1, N'It is recommended that this machine ______ checked every year.', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (5, 1, 1, N'Phần 2: ngữ pháp', -1, N'_______ is writing brief entries about the daily activities of an individual or company.', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (6, 1, 1, N'Phần 2: ngữ pháp', -1, N'There are people who choose to abandon their heritage culture and assimilate _______ the new culture of the majority.', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (7, 2, 1, N'Phần 2: ngữ pháp', -1, N'You don’t get a lot of _______ from a news report on radio or TV.', 5, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (8, 1, 1, N'Phần 2: ngữ pháp', -1, N'Only after food has been dried or canned _______.', 6, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (9, 4, 1, N'Phần 2: ngữ pháp', -1, N'The areas _______ are destroyed suffer a lot from soil erosion.', 7, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (10, 1, 1, N'Phần 2: ngữ pháp', -1, N'<Equation Value="\frac{d}{dx}\left( \int_{0}^{x} f(u)\,du\right)=f(x)" >', 8, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (11, 4, 1, N'Phần 2: ngữ pháp', -1, N'We managed to finish the exercise on time and passed the exam. ________, it was very difficult.', 9, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (12, 2, 1, N'Phần 2: ngữ pháp', -1, N'He has the  ________ face and skin of an old traveller.', 10, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (13, 1, 1, N'Phần 2: ngữ pháp', -1, N'It is stated that we are now in the first stages of a battle for the _______ of life on the earth.', 11, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (14, 3, 1, N'Phần 2: ngữ pháp', -1, N'_______ Women’s Day is on _______ eighth of March.', 12, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (15, 1, 1, N'Phần 3: từ trái nghĩa', -1, N'One of the reasons why families break up is that parents are always critical of each other.', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (16, 2, 1, N'Phần 3: từ trái nghĩa', -1, N'Urbanization has resulted in massive problems besides the benefits.', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (17, 1, 1, N'Phần 4: từ đồng nghĩa', -1, N'Under the major''s able leadership, the soldiers found safety.', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (18, 2, 1, N'Phần 4: từ đồng nghĩa', -1, N'Adverts on Facebook seem to be more efficient than billboards or TV ads because of its enormous number of users.', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (19, 1, 1, N'Phần 5: câu hỏi nhóm', -1, N'', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (20, 1, 1, N'Phần 5: câu hỏi nhóm', -1, N'', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (21, 4, 1, N'Phần 5: câu hỏi nhóm', -1, N'', 5, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (22, 1, 1, N'Phần 5: câu hỏi nhóm', -1, N'', 6, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (23, 2, 1, N'Phần 5: câu hỏi nhóm', -1, N'', 7, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (24, 3, 1, N'Phần 6: nghe', -1, N'The Clavie is __________________.', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (25, 1, 1, N'Phần 6: nghe', -1, N'In the Up Helly Aa festival, they burn __________________.', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (26, 2, 1, N'Phần 6: nghe', -1, N'During a pancake race, you have to __________________.', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (27, 1, 1, N'Phần 6: nghe', -1, N'Nowadays, the people who win the cheese rolling competition are usually _______________.', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (28, 1, 1, N'Phần 6: nghe', -1, N'The fastest snail in the Snail Racing is __________________.', 5, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (29, 1, 1, N'Phần 6: nghe', -1, N'Black Pudding throwing is similar to __________________.', 6, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (30, 2, 1, N'Phần 6: nghe', -1, N'To win the best gurner competition, one man __________________.', 7, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (31, 1, 1, N'Phần 6: nghe', -1, N'The Burning of the Clocks festival is __________________.', 8, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (32, 7, 3, N'Thuộc nhóm chương 1', -1, N'Tìm từ khác nghĩa với các từ còn lại', 1, N'', 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (33, 7, 4, N'Thuộc nhóm chương 1', -1, N'Tìm từ khác nghĩa với các từ còn lại', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (34, 8, 4, N'Thuộc nhóm chương 2', -1, N'Cho lược đồ quan hệ R(U), với U = (A,B,D,C), F= {A→BCD, C→D}. Lược đồ quan hệ R(U)  ở dạng chuẩn nào?', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (35, 8, 1, N'Thuộc nhóm chương 2', -1, N'Trong thực tế cài đặt, một lược đồ cơ sở dữ liệu ít nhất phải đạt dạng chuẩn nào trong các dạng chuẩn sau:', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (36, 8, 2, N'Thuộc nhóm chương 2', -1, N'Cho lược đồ quan hệ R(U), với U = {A,B,D,C,E}, F = {A→BC, C→D, AC→E}. Lược đồ quan hệ R(U)  ở dạng chuẩn nào?', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (37, 8, 2, N'Thuộc nhóm chương 2', -1, N'Cho loại quan hệ SINHVIEN(MSSV, HoTen, Ngaysinh, NgayVaoDoan). Phát biểu: “Ngày vào Đoàn (NgayVaoDoan) > Ngày sinh (Ngaysinh)” thuộc loại ràng buộc toàn vẹn gì?', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (38, 9, 1, N'Thuộc nhóm chương 3', -1, N'Choose difference', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (39, 10, 1, N'Thuộc nhóm chương 4', -1, N'Cho lược đồ quan hệ SACH gồm các thuộc tính: Mã sách, tên tên sách, giá, mã nhà xuất bản, tên nhà xuất bản và tập PTH</p><p>F={Mã sách → tên sách, giá, mã nhà xuất bản; mã nhà xuất bản → tên nhà xuất bản}. Lược đồ quan hệ trên ở dạng chuẩn nào? ', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (40, 10, 1, N'Thuộc nhóm chương 4', -1, N'Cho lược đồ quan hệ HOADON(SOHD: Số hóa đơn, NGAY: Ngày lập hóa đơn, NOIDUNG: Nội dung). Ràng buộc: “Ngày lập hóa đơn phải nhỏ hơn hoặc bằng ngày hiện hành“. Hãy chỉ ra phát biểu mô tả đúng nhất?', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (41, 10, 1, N'Thuộc nhóm chương 4', -1, N'Cho lược đồ quan hệ Q(A,B,C,D,E,I) và tập phụ thuộc hàm F={ACD→EBI, CE→AD}. Q đạt dạng mấy?', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (42, 10, 1, N'Thuộc nhóm chương 4', -1, N'Dạng chuẩn đạt được của một lược đồ cơ sở dữ liệu là:', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (43, 12, 1, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 1, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (44, 12, 1, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 2, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (45, 12, 1, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 3, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (46, 12, 2, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 4, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (47, 12, 2, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 5, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (48, 12, 4, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 6, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (49, 12, 4, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 7, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (50, 12, 4, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 8, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (51, 12, 1, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 9, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (52, 12, 2, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 10, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (53, 12, 1, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 11, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (54, 12, 2, N'Thuộc nhóm chương 5.1', -1, N'<p></p>', 12, NULL, 0)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (55, 13, 3, N'Thuộc nhóm chương 6', -1, N'Nghiệm của phương trình <latex> 2^x = 3.</latex>', 1, N'', 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (56, 13, 4, N'Thuộc nhóm chương 6', -1, N'Cho a là số thực dương. Giá trị rút gọn của biểu thức <latex> P = a_4^3\sqrt{a}</latex> bằng', 1, N'', 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (57, 13, 1, N'Thuộc nhóm chương 6', -1, N'Bồn hoa của một trường X có dạng hình tròn bán kính bằng 8m. Người ta chia bồn hoa thành các phần như hình vẽ dưới đây và có ý định trồng hoa như sau: Phần diện tích bên trong hình vuông ABCD để trồng hoa. Phần diện tích kéo dài từ 4 cạnh của hình vuông đến đường tròn dùng để trồng cỏ. Ở bốn góc còn lại, mỗi góc trồng một cây cọ. Biết AB = 4m, giá trồng hoa là 200.000đ/<latex>m^2</latex>, giá trồng cỏ là 100.000đ/<latex>m^2</latex>, mỗi cây cọ giá 150.000đ. Hỏi cần bao nhiêu tiền để thực hiện việc trang trí bồn hoa đó.<br><br/><img src="./images/garden_radius.png">
', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (58, 13, 1, N'Thuộc nhóm chương 6', -1, N'Trong không gian Oxyz, đường thẳng Oz có phương trình là', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (59, 13, 1, N'Thuộc nhóm chương 6', -1, N'Tính thể tích V của khối lăng trụ có đáy là một lục giác đều cạnh a và chiều cao của khối lăng trụ 4a', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (60, 13, 1, N'Thuộc nhóm chương 6', -1, N'Đồ thị sau đây là đồ thị của hàm số nào?</p><br><br/><img src="./images/map_Oxy.png">', 5, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (61, 13, 2, N'Thuộc nhóm chương 6', -1, N'Một sóng cơ truyền dọc theo trục Ox với phương trình <latex> u=4\cos(20\pi t-2\pi x)</latex>(mm). Biên độ của sóng này là', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (62, 14, 2, N'Thuộc nhóm chương 7', -1, N'Mark is going to...', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (63, 14, 2, N'Thuộc nhóm chương 7', -1, N'Mark is also going to...', 3, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (64, 14, 2, N'Thuộc nhóm chương 7', -1, N'James is going to...', 4, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (65, 14, 1, N'Thuộc nhóm chương 7', -1, N'Sam is going to...', 5, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (66, 14, 1, N'Thuộc nhóm chương 7', -1, N'Jake is going to...', 6, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (67, 14, 1, N'Thuộc nhóm chương 7', -1, N'Carla is going to...', 7, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (68, 14, 2, N'Thuộc nhóm chương 7', -1, N'Tom is not going to...', 8, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (69, 14, 3, N'Thuộc nhóm chương 7', -1, N'Daniel and Tom are going to…', 9, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (70, 15, 4, N'Thuộc nhóm chương 8', -1, N'Hỗn hợp <latex>X</latex> gồm <latex>Al, Al_{2}O_{3}, Fe, FeO, Fe_{3}O_{4}</latex> và <latex> Fe_{2}O_{3}</latex> trong đó <latex>O</latex> chiếm 18,49% về khối lượng. Hòa tan hết 12,98 gam <latex>X</latex> cần vừa đủ 627,5 ml dung dịch <latex> HNO_{3} </latex> 1M thu được dung dịch Y và 0,448 lít hỗn hợp khí <latex>Z</latex> (đktc) gồm <latex>NO</latex> và<latex> N_{2}</latex> có tỷ lệ mol tương ứng là 1:1. Làm bay hơi dung dịch <latex>Y</latex> thu được m gam muối. Giá trị của m là:', 1, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (71, 15, 1, N'Thuộc nhóm chương 8', -1, N'Hỗn hợp <latex>X</latex> gồm <latex>Cu</latex> và <latex>Fe_{3}O_{4} </latex>. Hoà tan m gam hỗn hợp <latex>X</latex> bằng dung dịch <latex> H_{2}SO_{4}</latex> loãng dư thu được dung dịch <latex>Y</latex> và<latex>\frac{8}{45}</latex>m gam chất rắn không tan. Hoà tan m gam hỗn hợp <latex>X</latex> bằng dung dịch <latex>HNO_{3}</latex>dư thu được 0,05 mol<latex>NO_{2}</latex>(sản phẩm khử duy nhất). Giá trị của m là :', 2, NULL, 1)
GO
INSERT [dbo].[CauHoi] ([MaCauHoi], [MaNhom], [MaCLO], [TieuDe], [KieuNoiDung], [NoiDung], [ThuTu], [GhiChu], [HoanVi]) VALUES (74, 17, 2, N'Thuộc nhóm chương 10', -1, N'Cho mệnh đề chứa biến P(x): "x + 15 ≤ x2" với giá trị thực nào của x trong các giá trị sau P(x) là mệnh đề đúng', 1, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[CauHoi] OFF
GO
SET IDENTITY_INSERT [dbo].[CauTraLoi] ON 
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (1, 1, 1, N'sl<u><b>u</b></u>m', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (2, 1, 2, N'<u><b>u</b></u>rban', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (3, 1, 3, N'b<u><b>u</b></u>lb', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (4, 1, 4, N'cl<u><b>u</b></u>tter', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (5, 2, 1, N'de<u><b>t</b></u>ermine', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (6, 2, 2, N'cos<u><b>t</b></u>ume', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (7, 2, 3, N'cul<u><b>t</b></u>ure', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (8, 2, 4, N'cri<u><b>t</b></u>ical', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (9, 3, 1, N'went / wasn’t', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (10, 3, 2, N'was going / had not been', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (11, 3, 3, N'have gone / wasn’t                   ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (12, 3, 4, N'went / have not been', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (13, 4, 1, N'to be            ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (14, 4, 2, N'is', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (15, 4, 3, N'be', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (16, 4, 4, N'are', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (17, 5, 1, N'App', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (18, 5, 2, N'Mass media', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (19, 5, 3, N' Advent', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (20, 5, 4, N'Microblogging', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (21, 6, 1, N'on', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (22, 6, 2, N'in', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (23, 6, 3, N'with', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (24, 6, 4, N'to', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (25, 7, 1, N'inform', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (26, 7, 2, N'informative  ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (27, 7, 3, N'information', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (28, 7, 4, N'informatively', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (29, 8, 1, N'that it should be stored for future use', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (30, 8, 2, N'it should be stored for future use', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (31, 8, 3, N'should it be stored for future use', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (32, 8, 4, N'should it store for future', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (33, 9, 1, N'whose trees', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (34, 9, 2, N'trees of which', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (35, 9, 3, N'which trees', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (36, 9, 4, N'that trees', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (37, 10, 1, N'spirits', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (38, 10, 2, N'customs', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (39, 10, 3, N'national prides', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (40, 10, 4, N'behaviors', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (41, 11, 1, N'But', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (42, 11, 2, N'However', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (43, 11, 3, N'So', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (44, 11, 4, N'Therefore', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (45, 12, 1, N'thought-provoking', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (46, 12, 2, N'year-round', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (47, 12, 3, N'overloaded', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (48, 12, 4, N'weather-beaten', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (49, 13, 1, N'sustainability', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (50, 13, 2, N'conservation', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (51, 13, 3, N'responsibility', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (52, 13, 4, N'purification', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (53, 14, 1, N'&Oslash /the', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (54, 14, 2, N'&Oslash /an', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (55, 14, 3, N'The/ an', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (56, 14, 4, N'The/ &Oslash', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (57, 15, 1, N'intolerant', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (58, 15, 2, N'supportive', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (59, 15, 3, N'tired', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (60, 15, 4, N'unawar', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (61, 16, 1, N'major', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (62, 16, 2, N'serious', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (63, 16, 3, N'a few', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (64, 16, 4, N'minor', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (65, 17, 1, N'guidance', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (66, 17, 2, N'intensity', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (67, 17, 3, N'flagship', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (68, 17, 4, N'ability', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (69, 18, 1, N'disorganized', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (70, 18, 2, N'successful', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (71, 18, 3, N'connected', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (72, 18, 4, N'updated', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (73, 19, 1, N'carried out', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (74, 19, 2, N'carried over', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (75, 19, 3, N'carried off', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (76, 19, 4, N'carry back', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (77, 20, 1, N'industrialize', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (78, 20, 2, N'industrial', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (79, 20, 3, N'industrialisation', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (80, 20, 4, N'industry', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (81, 21, 1, N'Because', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (82, 21, 2, N'However', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (83, 21, 3, N'Therefore', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (84, 21, 4, N'Although', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (85, 22, 1, N'appearance', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (86, 22, 2, N'plenty', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (87, 22, 3, N'loss', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (88, 22, 4, N'lack', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (89, 23, 1, N'on', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (90, 23, 2, N'about', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (91, 23, 3, N'with', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (92, 23, 4, N'for', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (93, 24, 1, N'a whisky container', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (94, 24, 2, N'a wooden cross', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (95, 24, 3, N'a bonfire made of things people don''t need', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (96, 25, 1, N'a wooden man', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (97, 25, 2, N'a Viking boat', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (98, 25, 3, N'a line in the grass', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (99, 26, 1, N'eat as many pancakes as possible', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (100, 26, 2, N'run as fast as possible while tossing a pancake in a pan', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (101, 26, 3, N'run and jump over ropes without dropping the pancake', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (102, 27, 1, N'top athletes', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (103, 27, 2, N'people from the village', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (104, 27, 3, N'visitors from all over the world', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (105, 28, 1, N'cooked with garlic and butter', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (106, 28, 2, N'rescued from the barbecue', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (107, 28, 3, N'given a prize of extra lettuce', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (108, 29, 1, N'pancake tossing', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (109, 29, 2, N'Olympic sports like javelin and shotput', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (110, 29, 3, N'bowling', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (111, 30, 1, N'had all his teeth removed', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (112, 30, 2, N'grew a really long beard', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (113, 30, 3, N'had a lot of facial piercings', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (114, 31, 1, N'2 years old', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (115, 31, 2, N'20 years old', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (116, 31, 3, N'200 years old', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (117, 32, 1, N'Good bye', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (118, 32, 2, N'こんにちは', 1, 0)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (119, 32, 3, N'안녕하세요', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (120, 32, 4, N'你好', 0, 0)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (121, 33, 1, N'Apple', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (122, 33, 2, N'オレンジ', 1, 0)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (123, 33, 3, N'사과', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (124, 33, 4, N'Quả táo', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (125, 34, 1, N'Dạng chuẩn 1 (1NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (126, 34, 2, N'Dạng chuẩn 2 (2NF)', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (127, 34, 3, N'Dạng chuẩn 3 (3NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (128, 34, 4, N'Không ở dạng chuẩn nào', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (129, 35, 1, N'Dạng chuẩn 1 (1NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (130, 35, 2, N'Dạng chuẩn 2 (2NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (131, 35, 3, N'Dạng chuẩn 3 (3NF)', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (132, 35, 4, N'Dạng chuẩn Boyce-Codd (BCNF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (133, 36, 1, N'Dạng chuẩn 1 (1NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (134, 36, 2, N'Dạng chuẩn 2 (2NF)', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (135, 36, 3, N'Dạng chuẩn 3 (3NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (136, 36, 4, N'Không ở dạng chuẩn nào', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (137, 37, 1, N'Liên quan đến miền giá trị', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (138, 37, 2, N'Liên thuộc tính trên cùng loại quan hệ', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (139, 37, 3, N'Liên thuộc tính liên quan hệ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (140, 37, 4, N'Do thuộc tính tổng hợp', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (141, 38, 1, N'de<u><b>t</b></u>ermine', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (142, 38, 2, N'cos<u><b>t</b></u>ume', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (143, 38, 3, N'cul<u><b>t</b></u>ure', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (144, 38, 4, N'cri<u><b>t</b></u>ical', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (145, 39, 1, N'1NF', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (146, 39, 2, N'2NF', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (147, 39, 3, N'3NF', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (148, 39, 4, N'Không ở dạng chuẩn nào', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (149, 40, 1, N'<latex>\forall</latex> h <latex> \exists</latex> HOADON: h.NGAY<=Getdate()', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (150, 40, 2, N'<latex> \exists</latex> h <latex> \exists</latex> HOADON: h.NGAY<=Getdate()', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (151, 40, 3, N'<latex>\forall</latex> h <latex> \exists</latex> HOADON: h.NGAY>=Getdate()', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (152, 40, 4, N'<latex> \exists</latex> h <latex> \exists</latex> HOADON: h.NGAY>=Getdate() ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (153, 41, 1, N'Dạng chuẩn 1 (1NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (154, 41, 2, N'Dạng chuẩn 2 (2NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (155, 41, 3, N'Dạng chuẩn 3 (3NF)', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (156, 41, 4, N'Dạng chuẩn Boyce-Codd (BCNF)', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (157, 42, 1, N'MIN (dạng chuẩn Qi), với Qi là các lược đồ quan hệ của cơ sở dữ liệu', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (158, 42, 2, N'MAX (dạng chuẩn Qi), với Qi là các lược đồ quan hệ của cơ sở dữ liệu', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (159, 42, 3, N'Trung bình (dạng chuẩn Qi), với Qi là các lược đồ quan hệ của cơ sở dữ liệu', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (160, 42, 4, N'Tùy vào quyết định của người phân tích', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (161, 43, 1, N'most', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (162, 43, 2, N'almost ', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (163, 43, 3, N'the most', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (164, 43, 4, N'mostly', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (165, 44, 1, N'widely', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (166, 44, 2, N'hardly', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (167, 44, 3, N'legally', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (168, 44, 4, N'nationally ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (169, 45, 1, N'translated', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (170, 45, 2, N'transferred', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (171, 45, 3, N'transformed', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (172, 45, 4, N'transited', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (173, 46, 1, N'increase ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (174, 46, 2, N'upbringing', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (175, 46, 3, N'rising', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (176, 46, 4, N'grow', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (177, 47, 1, N'dropped in', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (178, 47, 2, N'dropped up ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (179, 47, 3, N'dropped out ', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (180, 47, 4, N'dropped by ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (181, 48, 1, N'hardly', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (182, 48, 2, N'truly', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (183, 48, 3, N'effortlessly ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (184, 48, 4, N'frequently ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (185, 49, 1, N'massly-produced', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (186, 49, 2, N'mass-produced ', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (187, 49, 3, N'massive-produced ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (188, 49, 4, N'mass-producing ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (189, 50, 1, N'Generally', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (190, 50, 2, N'Frankly', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (191, 50, 3, N'Fortunately', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (192, 50, 4, N'Unfortunately', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (193, 51, 1, N'few', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (194, 51, 2, N'a few ', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (195, 51, 3, N'a little', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (196, 51, 4, N'little', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (197, 52, 1, N'was allowing ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (198, 52, 2, N'has allowed ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (199, 52, 3, N'allowed', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (200, 52, 4, N'had allowed ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (201, 53, 1, N'for', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (202, 53, 2, N'since', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (203, 53, 3, N'during', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (204, 53, 4, N'of ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (205, 54, 1, N'devil', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (206, 54, 2, N'deviation', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (207, 54, 3, N'deviant', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (208, 54, 4, N'device', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (209, 55, 1, N'<latex> x  = \log_{2}{3}.</latex>', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (210, 55, 2, N'<latex> x = \log_{3}{2}.</latex>        ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (211, 55, 3, N'<latex>x = 2^3.</latex>        ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (212, 55, 4, N'<latex>x = 3^2.</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (213, 56, 1, N'<latex> a_{6}^{5}.</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (214, 56, 2, N'<latex> a_{6}^{11}.</latex>', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (215, 56, 3, N'<latex>a_{3}^{10}.</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (216, 56, 4, N'<latex>a_{3}^{7}.</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (217, 57, 1, N'14.865.000 đồng', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (218, 57, 2, N'12.218.000 đồng', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (219, 57, 3, N'14.465.000 đồng', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (220, 57, 4, N'13.265.000 đồng', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (221, 58, 1, N'<latex> \begin{cases}x=t\\y= 0\\z = 0\end{cases}</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (222, 58, 2, N'<latex> \begin{cases}x=0\\y=t\\z = 0\end{cases} </latex>        ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (223, 58, 3, N'<latex> \begin{cases}x=0\\y= t\\z = t\end{cases} </latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (224, 58, 4, N'<latex> \begin{cases}x=0\\y=0\\z=1+t\end{cases} </latex>', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (225, 59, 1, N'V = <latex>12a^{3}\sqrt{3}</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (226, 59, 2, N'V = <latex> 6a^{3}\sqrt{3} </latex>', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (227, 59, 3, N'V = <latex>2a^{3}\sqrt{3}</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (228, 59, 4, N'V = <latex>24a^{3}\sqrt{3}</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (229, 60, 1, N'y = <latex> -x^{4}+2x^{2}</latex>', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (230, 60, 2, N'y = <latex> x^{4}-2x^{2}</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (231, 60, 3, N'y = <latex> x^{4}-2x^{2}+1</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (232, 60, 4, N'y = <latex> -x^{4}+2x^{2}+1</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (233, 61, 1, N'<latex> 20\pi mm</latex> ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (234, 61, 2, N'<latex> 4mm</latex>', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (235, 61, 3, N'<latex>8mm</latex> ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (236, 61, 4, N'<latex> 2\pi mm</latex>', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (237, 62, 1, N'Visit his family', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (238, 62, 2, N'Travel', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (239, 62, 3, N'Buy a new car', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (240, 62, 4, N'Not going anywhere', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (241, 63, 1, N'Ride a bike', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (242, 63, 2, N'Buy a new bike for his daughter', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (243, 63, 3, N'Buy a new furniture', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (244, 63, 4, N'Buy a new car', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (245, 64, 1, N'Spend one month with his grandparents', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (246, 64, 2, N'Go fishing', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (247, 64, 3, N'Read a book', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (248, 64, 4, N'Go to buy some food', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (249, 65, 1, N'Call James', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (250, 65, 2, N'Visit her family for a few days', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (251, 65, 3, N'Read a book', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (252, 65, 4, N'Go fishing', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (253, 66, 1, N'Help his dad', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (254, 66, 2, N'Go to the cinema', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (255, 66, 3, N'Cook piza', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (256, 66, 4, N'Do some housework', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (257, 67, 1, N'Make popcorn', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (258, 67, 2, N'Visit a friend', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (259, 67, 3, N'Go to the beach', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (260, 67, 4, N'Do some homework', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (261, 68, 1, N'Have a shower', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (262, 68, 2, N'Eat dinner with his family', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (263, 68, 3, N'Drive to Tom’s place', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (264, 68, 4, N'Do some housework', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (265, 69, 1, N'Travel together', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (266, 69, 2, N'Go to the pub', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (267, 69, 3, N'Watch a match', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (268, 69, 4, N'Have a date', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (269, 70, 1, N'44,688', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (270, 70, 2, N'46,888', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (271, 70, 3, N'48,686', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (272, 70, 4, N'48,666', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (273, 71, 1, N'8,4 ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (274, 71, 2, N'3,6 ', 1, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (275, 71, 3, N'4,8 ', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (276, 71, 4, N'2,3', 0, 1)
GO
INSERT [dbo].[CauTraLoi] ([MaCauTraLoi], [MaCauHoi], [ThuTu], [NoiDung], [LaDapAn], [HoanVi]) VALUES (281, 74, 1, N'5', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[CauTraLoi] OFF
GO
SET IDENTITY_INSERT [dbo].[chi_tiet_bai_thi] ON 
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (137, 1, 7, 10, 1, 41, 154, CAST(N'2025-06-25T09:57:03.437' AS DateTime), NULL, 0, 2)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (138, 1, 7, 10, 1, 40, 151, CAST(N'2025-06-25T09:57:08.820' AS DateTime), NULL, 0, 3)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (139, 1, 7, 10, 1, 42, 158, CAST(N'2025-06-25T09:57:09.713' AS DateTime), NULL, 0, 4)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (140, 1, 7, 10, 1, 39, 147, CAST(N'2025-06-25T09:57:10.970' AS DateTime), NULL, 0, 5)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (141, 1, 7, 7, 4, 33, 123, CAST(N'2025-06-25T09:57:11.843' AS DateTime), NULL, 0, 6)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (142, 1, 7, 7, 3, 32, 117, CAST(N'2025-06-25T09:57:13.083' AS DateTime), NULL, 0, 7)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (143, 1, 7, 15, 1, 71, 274, CAST(N'2025-06-25T09:57:14.707' AS DateTime), NULL, 1, 8)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (144, 1, 7, 15, 4, 70, 271, CAST(N'2025-06-25T09:57:16.367' AS DateTime), NULL, 0, 9)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (145, 1, 7, 13, 1, 60, 230, CAST(N'2025-06-25T09:57:17.710' AS DateTime), NULL, 0, 10)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (146, 1, 7, 13, 2, 61, 233, CAST(N'2025-06-25T09:57:18.980' AS DateTime), NULL, 0, 11)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (147, 1, 7, 13, 4, 56, 216, CAST(N'2025-06-25T09:57:20.267' AS DateTime), NULL, 0, 12)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (148, 1, 7, 13, 1, 58, 221, CAST(N'2025-06-25T09:57:21.717' AS DateTime), NULL, 0, 13)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (149, 1, 7, 13, 1, 57, 218, CAST(N'2025-06-25T09:57:22.797' AS DateTime), NULL, 0, 14)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (150, 1, 7, 13, 3, 55, 210, CAST(N'2025-06-25T09:57:24.157' AS DateTime), NULL, 0, 15)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (151, 1, 7, 13, 1, 59, 228, CAST(N'2025-06-25T09:57:25.090' AS DateTime), NULL, 0, 16)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (152, 1, 7, 12, 1, 43, 161, CAST(N'2025-06-25T09:57:28.267' AS DateTime), NULL, 0, 17)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (153, 1, 7, 12, 1, 44, 167, CAST(N'2025-06-25T09:57:29.027' AS DateTime), NULL, 0, 18)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (154, 1, 7, 12, 1, 45, 171, CAST(N'2025-06-25T09:57:30.933' AS DateTime), NULL, 1, 19)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (155, 1, 7, 12, 2, 46, 174, CAST(N'2025-06-25T09:57:32.077' AS DateTime), NULL, 1, 20)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (156, 1, 7, 12, 2, 47, 177, CAST(N'2025-06-25T09:57:33.710' AS DateTime), NULL, 0, 21)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (157, 1, 7, 12, 4, 48, 182, CAST(N'2025-06-25T09:57:34.470' AS DateTime), NULL, 0, 22)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (158, 1, 7, 12, 4, 49, 185, CAST(N'2025-06-25T09:57:36.220' AS DateTime), NULL, 0, 23)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (159, 1, 7, 12, 4, 50, 191, CAST(N'2025-06-25T09:57:37.247' AS DateTime), NULL, 0, 24)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (160, 1, 7, 12, 1, 51, 193, CAST(N'2025-06-25T09:57:39.317' AS DateTime), NULL, 0, 25)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (161, 1, 7, 12, 2, 52, 200, CAST(N'2025-06-25T09:57:40.713' AS DateTime), NULL, 0, 26)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (162, 1, 7, 12, 1, 53, 201, CAST(N'2025-06-25T09:57:42.247' AS DateTime), NULL, 1, 27)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (163, 1, 7, 12, 2, 54, 206, CAST(N'2025-06-25T09:57:43.180' AS DateTime), NULL, 0, 28)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (164, 1, 7, 14, 1, 66, 253, CAST(N'2025-06-25T09:57:44.700' AS DateTime), NULL, 0, 29)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (165, 1, 7, 14, 3, 69, 267, CAST(N'2025-06-25T09:57:45.700' AS DateTime), NULL, 0, 30)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (166, 1, 7, 14, 2, 68, 262, CAST(N'2025-06-25T09:57:47.070' AS DateTime), NULL, 1, 31)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (167, 1, 7, 14, 1, 67, 258, CAST(N'2025-06-25T09:57:48.047' AS DateTime), NULL, 0, 32)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (168, 1, 7, 14, 2, 62, 237, CAST(N'2025-06-25T09:57:49.687' AS DateTime), NULL, 0, 33)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (169, 1, 7, 14, 2, 63, 242, CAST(N'2025-06-25T09:57:50.693' AS DateTime), NULL, 0, 34)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (170, 1, 7, 14, 2, 64, 245, CAST(N'2025-06-25T09:57:52.087' AS DateTime), NULL, 0, 35)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (171, 1, 7, 14, 1, 65, 251, CAST(N'2025-06-25T09:57:53.037' AS DateTime), NULL, 1, 36)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (172, 1, 7, 8, 4, 34, 125, CAST(N'2025-06-25T09:57:54.680' AS DateTime), NULL, 0, 37)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (173, 1, 7, 8, 1, 35, 130, CAST(N'2025-06-25T09:57:55.310' AS DateTime), NULL, 0, 38)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (174, 1, 7, 8, 2, 37, 137, CAST(N'2025-06-25T09:57:57.173' AS DateTime), NULL, 0, 39)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (175, 1, 7, 8, 2, 36, 134, CAST(N'2025-06-25T09:57:58.100' AS DateTime), NULL, 1, 40)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (176, 1, 7, 9, 1, 38, 141, CAST(N'2025-06-25T10:08:41.950' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (177, 2, 7, 10, 1, 45, 132, CAST(N'2025-06-25T11:00:00.000' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (178, 3, 7, 10, 1, 48, 133, CAST(N'2025-06-25T11:00:05.000' AS DateTime), NULL, 1, 2)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (179, 18, 7, 10, 1, 46, 135, CAST(N'2025-06-25T11:00:10.000' AS DateTime), NULL, 0, 3)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (180, 27, 7, 10, 1, 42, 137, CAST(N'2025-06-25T11:00:15.000' AS DateTime), NULL, 1, 4)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (181, 28, 7, 10, 1, 44, 140, CAST(N'2025-06-25T11:00:20.000' AS DateTime), NULL, 0, 5)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (182, 2, 7, 10, 1, 49, 143, CAST(N'2025-06-25T11:00:25.000' AS DateTime), NULL, 1, 6)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (183, 3, 7, 10, 1, 47, 146, CAST(N'2025-06-25T11:00:30.000' AS DateTime), NULL, 0, 7)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (184, 18, 7, 10, 1, 52, 149, CAST(N'2025-06-25T11:00:35.000' AS DateTime), NULL, 1, 8)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (185, 27, 7, 10, 1, 55, 151, CAST(N'2025-06-25T11:00:40.000' AS DateTime), NULL, 0, 9)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (186, 28, 7, 10, 1, 58, 154, CAST(N'2025-06-25T11:00:45.000' AS DateTime), NULL, 1, 10)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (187, 2, 7, 10, 1, 60, 157, CAST(N'2025-06-25T11:00:50.000' AS DateTime), NULL, 0, 11)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (188, 3, 7, 10, 1, 62, 160, CAST(N'2025-06-25T11:00:55.000' AS DateTime), NULL, 1, 12)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (189, 18, 7, 10, 1, 63, 163, CAST(N'2025-06-25T11:01:00.000' AS DateTime), NULL, 0, 13)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (190, 27, 7, 10, 1, 65, 166, CAST(N'2025-06-25T11:01:05.000' AS DateTime), NULL, 1, 14)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (191, 28, 7, 10, 1, 67, 169, CAST(N'2025-06-25T11:01:10.000' AS DateTime), NULL, 0, 15)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (192, 2, 7, 10, 1, 70, 172, CAST(N'2025-06-25T11:01:15.000' AS DateTime), NULL, 1, 16)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (193, 3, 7, 10, 1, 72, 175, CAST(N'2025-06-25T11:01:20.000' AS DateTime), NULL, 0, 17)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (194, 18, 7, 10, 1, 74, 178, CAST(N'2025-06-25T11:01:25.000' AS DateTime), NULL, 1, 18)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (195, 27, 7, 10, 1, 76, 181, CAST(N'2025-06-25T11:01:30.000' AS DateTime), NULL, 0, 19)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (196, 28, 7, 10, 1, 78, 184, CAST(N'2025-06-25T11:01:35.000' AS DateTime), NULL, 1, 20)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (197, 2, 7, 10, 1, 35, 120, CAST(N'2025-06-25T11:01:40.000' AS DateTime), NULL, 0, 21)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (198, 3, 7, 10, 1, 36, 122, CAST(N'2025-06-25T11:01:45.000' AS DateTime), NULL, 1, 22)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (199, 18, 7, 10, 1, 37, 124, CAST(N'2025-06-25T11:01:50.000' AS DateTime), NULL, 0, 23)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (200, 27, 7, 10, 1, 38, 126, CAST(N'2025-06-25T11:01:55.000' AS DateTime), NULL, 1, 24)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (201, 28, 7, 10, 1, 39, 128, CAST(N'2025-06-25T11:02:00.000' AS DateTime), NULL, 0, 25)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (202, 2, 7, 10, 1, 40, 130, CAST(N'2025-06-25T11:02:05.000' AS DateTime), NULL, 1, 26)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (203, 3, 7, 10, 1, 41, 132, CAST(N'2025-06-25T11:02:10.000' AS DateTime), NULL, 0, 27)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (204, 18, 7, 10, 1, 42, 134, CAST(N'2025-06-25T11:02:15.000' AS DateTime), NULL, 1, 28)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (205, 27, 7, 10, 1, 43, 136, CAST(N'2025-06-25T11:02:20.000' AS DateTime), NULL, 0, 29)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (206, 28, 7, 10, 1, 44, 138, CAST(N'2025-06-25T11:02:25.000' AS DateTime), NULL, 1, 30)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (207, 2, 7, 10, 1, 45, 140, CAST(N'2025-06-25T11:02:30.000' AS DateTime), NULL, 0, 31)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (208, 3, 7, 10, 1, 46, 142, CAST(N'2025-06-25T11:02:35.000' AS DateTime), NULL, 1, 32)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (209, 18, 7, 10, 1, 47, 144, CAST(N'2025-06-25T11:02:40.000' AS DateTime), NULL, 0, 33)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (210, 27, 7, 10, 1, 48, 146, CAST(N'2025-06-25T11:02:45.000' AS DateTime), NULL, 1, 34)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (211, 28, 7, 10, 1, 49, 148, CAST(N'2025-06-25T11:02:50.000' AS DateTime), NULL, 0, 35)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (212, 2, 7, 10, 1, 50, 150, CAST(N'2025-06-25T11:02:55.000' AS DateTime), NULL, 1, 36)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (213, 3, 7, 10, 1, 51, 152, CAST(N'2025-06-25T11:03:00.000' AS DateTime), NULL, 0, 37)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (214, 18, 7, 10, 1, 52, 154, CAST(N'2025-06-25T11:03:05.000' AS DateTime), NULL, 1, 38)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (215, 27, 7, 10, 1, 53, 156, CAST(N'2025-06-25T11:03:10.000' AS DateTime), NULL, 0, 39)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (216, 28, 7, 10, 1, 54, 158, CAST(N'2025-06-25T11:03:15.000' AS DateTime), NULL, 1, 40)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (217, 2, 7, 10, 1, 41, 154, CAST(N'2025-06-25T11:05:00.000' AS DateTime), NULL, 0, 240)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (218, 3, 7, 10, 1, 40, 151, CAST(N'2025-06-25T11:05:01.000' AS DateTime), NULL, 1, 241)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (219, 18, 7, 10, 1, 42, 158, CAST(N'2025-06-25T11:05:02.000' AS DateTime), NULL, 0, 242)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (220, 27, 7, 10, 1, 39, 147, CAST(N'2025-06-25T11:05:03.000' AS DateTime), NULL, 1, 243)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (221, 28, 7, 7, 4, 33, 123, CAST(N'2025-06-25T11:05:04.000' AS DateTime), NULL, 0, 244)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (222, 2, 7, 7, 3, 32, 117, CAST(N'2025-06-25T11:05:05.000' AS DateTime), NULL, 1, 245)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (223, 3, 7, 15, 1, 71, 274, CAST(N'2025-06-25T11:05:06.000' AS DateTime), NULL, 0, 246)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (224, 18, 7, 15, 4, 70, 271, CAST(N'2025-06-25T11:05:07.000' AS DateTime), NULL, 1, 247)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (225, 27, 7, 13, 1, 60, 230, CAST(N'2025-06-25T11:05:08.000' AS DateTime), NULL, 0, 248)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (226, 28, 7, 13, 2, 61, 233, CAST(N'2025-06-25T11:05:09.000' AS DateTime), NULL, 1, 249)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (227, 2, 7, 13, 4, 56, 216, CAST(N'2025-06-25T11:05:10.000' AS DateTime), NULL, 0, 250)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (228, 3, 7, 13, 1, 58, 221, CAST(N'2025-06-25T11:05:11.000' AS DateTime), NULL, 1, 251)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (229, 18, 7, 13, 1, 57, 218, CAST(N'2025-06-25T11:05:12.000' AS DateTime), NULL, 0, 252)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (230, 27, 7, 13, 3, 55, 210, CAST(N'2025-06-25T11:05:13.000' AS DateTime), NULL, 1, 253)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (231, 28, 7, 13, 1, 59, 228, CAST(N'2025-06-25T11:05:14.000' AS DateTime), NULL, 0, 254)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (232, 28, 7, 12, 1, 43, 161, CAST(N'2025-06-25T11:05:15.000' AS DateTime), NULL, 1, 255)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (233, 3, 7, 12, 1, 44, 167, CAST(N'2025-06-25T11:05:16.000' AS DateTime), NULL, 0, 256)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (234, 18, 7, 12, 1, 45, 171, CAST(N'2025-06-25T11:05:17.000' AS DateTime), NULL, 1, 257)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (235, 27, 7, 12, 2, 46, 174, CAST(N'2025-06-25T11:05:18.000' AS DateTime), NULL, 0, 258)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (236, 28, 7, 12, 2, 47, 177, CAST(N'2025-06-25T11:05:19.000' AS DateTime), NULL, 1, 259)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (237, 2, 7, 12, 4, 48, 182, CAST(N'2025-06-25T11:05:20.000' AS DateTime), NULL, 0, 260)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (238, 3, 7, 12, 4, 49, 185, CAST(N'2025-06-25T11:05:21.000' AS DateTime), NULL, 1, 261)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (239, 18, 7, 12, 4, 50, 191, CAST(N'2025-06-25T11:05:22.000' AS DateTime), NULL, 0, 262)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (240, 27, 7, 12, 1, 51, 193, CAST(N'2025-06-25T11:05:23.000' AS DateTime), NULL, 1, 263)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (241, 28, 7, 12, 2, 52, 200, CAST(N'2025-06-25T11:05:24.000' AS DateTime), NULL, 0, 264)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (242, 2, 7, 12, 1, 53, 201, CAST(N'2025-06-25T11:05:25.000' AS DateTime), NULL, 1, 265)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (243, 3, 7, 12, 2, 54, 206, CAST(N'2025-06-25T11:05:26.000' AS DateTime), NULL, 0, 266)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (244, 18, 7, 14, 1, 66, 253, CAST(N'2025-06-25T11:05:27.000' AS DateTime), NULL, 1, 267)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (245, 27, 7, 14, 3, 69, 267, CAST(N'2025-06-25T11:05:28.000' AS DateTime), NULL, 0, 268)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (246, 28, 7, 14, 2, 68, 262, CAST(N'2025-06-25T11:05:29.000' AS DateTime), NULL, 1, 269)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (247, 2, 7, 14, 1, 67, 258, CAST(N'2025-06-25T11:05:30.000' AS DateTime), NULL, 0, 270)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (248, 27, 7, 14, 2, 62, 237, CAST(N'2025-06-25T11:05:31.000' AS DateTime), NULL, 1, 271)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (249, 18, 7, 14, 2, 63, 242, CAST(N'2025-06-25T11:05:32.000' AS DateTime), NULL, 0, 272)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (250, 27, 7, 14, 2, 64, 245, CAST(N'2025-06-25T11:05:33.000' AS DateTime), NULL, 1, 273)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (251, 28, 7, 14, 1, 65, 251, CAST(N'2025-06-25T11:05:34.000' AS DateTime), NULL, 0, 274)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (252, 2, 7, 8, 4, 34, 125, CAST(N'2025-06-25T11:05:35.000' AS DateTime), NULL, 1, 275)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (253, 3, 7, 8, 1, 35, 130, CAST(N'2025-06-25T11:05:36.000' AS DateTime), NULL, 0, 276)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (254, 18, 7, 8, 2, 37, 137, CAST(N'2025-06-25T11:05:37.000' AS DateTime), NULL, 1, 277)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (255, 27, 7, 8, 2, 36, 134, CAST(N'2025-06-25T11:05:38.000' AS DateTime), NULL, 0, 278)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (256, 28, 7, 9, 1, 38, 141, CAST(N'2025-06-25T11:05:39.000' AS DateTime), NULL, 1, 279)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (257, 2, 7, 9, 1, 38, 142, CAST(N'2025-06-25T11:05:40.000' AS DateTime), NULL, 0, 280)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (258, 3, 7, 10, 1, 39, 143, CAST(N'2025-06-25T11:05:41.000' AS DateTime), NULL, 1, 281)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (259, 18, 7, 10, 2, 40, 144, CAST(N'2025-06-25T11:05:42.000' AS DateTime), NULL, 0, 282)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (260, 27, 7, 11, 1, 41, 145, CAST(N'2025-06-25T11:05:43.000' AS DateTime), NULL, 1, 283)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (261, 28, 7, 11, 2, 42, 146, CAST(N'2025-06-25T11:05:44.000' AS DateTime), NULL, 0, 284)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (262, 2, 7, 12, 1, 43, 147, CAST(N'2025-06-25T11:05:45.000' AS DateTime), NULL, 1, 285)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (263, 3, 7, 12, 2, 44, 148, CAST(N'2025-06-25T11:05:46.000' AS DateTime), NULL, 0, 286)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (264, 18, 7, 13, 1, 45, 149, CAST(N'2025-06-25T11:05:47.000' AS DateTime), NULL, 1, 287)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (265, 27, 7, 13, 2, 46, 150, CAST(N'2025-06-25T11:05:48.000' AS DateTime), NULL, 0, 288)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (266, 28, 7, 14, 1, 47, 151, CAST(N'2025-06-25T11:05:49.000' AS DateTime), NULL, 1, 289)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (267, 2, 7, 14, 2, 48, 152, CAST(N'2025-06-25T11:05:50.000' AS DateTime), NULL, 0, 290)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (268, 3, 7, 15, 1, 49, 153, CAST(N'2025-06-25T11:05:51.000' AS DateTime), NULL, 1, 291)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (269, 18, 7, 15, 2, 50, 154, CAST(N'2025-06-25T11:05:52.000' AS DateTime), NULL, 0, 292)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (270, 27, 7, 16, 1, 51, 155, CAST(N'2025-06-25T11:05:53.000' AS DateTime), NULL, 1, 293)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (271, 28, 7, 16, 2, 52, 156, CAST(N'2025-06-25T11:05:54.000' AS DateTime), NULL, 0, 294)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (272, 28, 7, 17, 1, 53, 157, CAST(N'2025-06-25T11:05:55.000' AS DateTime), NULL, 1, 295)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (273, 3, 7, 17, 2, 54, 158, CAST(N'2025-06-25T11:05:56.000' AS DateTime), NULL, 0, 296)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (274, 18, 7, 18, 1, 55, 159, CAST(N'2025-06-25T11:05:57.000' AS DateTime), NULL, 1, 297)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (275, 27, 7, 18, 2, 56, 160, CAST(N'2025-06-25T11:05:58.000' AS DateTime), NULL, 0, 298)
GO
INSERT [dbo].[chi_tiet_bai_thi] ([MaChiTietBaiThi], [ma_chi_tiet_ca_thi], [MaDeHV], [MaNhom], [MaCLO], [MaCauHoi], [CauTraLoi], [NgayTao], [NgayCapNhat], [KetQua], [ThuTu]) VALUES (276, 28, 7, 19, 1, 57, 161, CAST(N'2025-06-25T11:05:59.000' AS DateTime), NULL, 1, 299)
GO
SET IDENTITY_INSERT [dbo].[chi_tiet_bai_thi] OFF
GO
SET IDENTITY_INSERT [dbo].[chi_tiet_ca_thi] ON 
GO
INSERT [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi], [ma_ca_thi], [ma_sinh_vien], [ma_de_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [da_thi], [da_hoan_thanh], [diem], [tong_so_cau], [so_cau_dung], [gio_cong_them], [thoi_diem_cong], [ly_do_cong]) VALUES (1, 1, 1, 7, CAST(N'2025-06-26T09:19:18.970' AS DateTime), NULL, 1, 1, 0, 41, 0, 0, NULL, NULL)
GO
INSERT [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi], [ma_ca_thi], [ma_sinh_vien], [ma_de_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [da_thi], [da_hoan_thanh], [diem], [tong_so_cau], [so_cau_dung], [gio_cong_them], [thoi_diem_cong], [ly_do_cong]) VALUES (2, 1, 2, 6, NULL, NULL, 0, 1, 8, 40, 0, 0, NULL, NULL)
GO
INSERT [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi], [ma_ca_thi], [ma_sinh_vien], [ma_de_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [da_thi], [da_hoan_thanh], [diem], [tong_so_cau], [so_cau_dung], [gio_cong_them], [thoi_diem_cong], [ly_do_cong]) VALUES (3, 1, 3, 5, CAST(N'2025-06-25T17:11:21.023' AS DateTime), NULL, 1, 1, 10, 40, 4, 0, NULL, NULL)
GO
INSERT [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi], [ma_ca_thi], [ma_sinh_vien], [ma_de_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [da_thi], [da_hoan_thanh], [diem], [tong_so_cau], [so_cau_dung], [gio_cong_them], [thoi_diem_cong], [ly_do_cong]) VALUES (18, 1, 4, 8, NULL, NULL, 0, 1, 7, 0, 0, 0, NULL, NULL)
GO
INSERT [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi], [ma_ca_thi], [ma_sinh_vien], [ma_de_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [da_thi], [da_hoan_thanh], [diem], [tong_so_cau], [so_cau_dung], [gio_cong_them], [thoi_diem_cong], [ly_do_cong]) VALUES (27, 1, 6, 8, NULL, NULL, 0, 1, 6.5, -1, 0, 0, NULL, NULL)
GO
INSERT [dbo].[chi_tiet_ca_thi] ([ma_chi_tiet_ca_thi], [ma_ca_thi], [ma_sinh_vien], [ma_de_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [da_thi], [da_hoan_thanh], [diem], [tong_so_cau], [so_cau_dung], [gio_cong_them], [thoi_diem_cong], [ly_do_cong]) VALUES (28, 1, 11, 7, NULL, NULL, 0, 0, -1, 0, 0, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[chi_tiet_ca_thi] OFF
GO
SET IDENTITY_INSERT [dbo].[chi_tiet_dot_thi] ON 
GO
INSERT [dbo].[chi_tiet_dot_thi] ([ma_chi_tiet_dot_thi], [ten_chi_tiet_dot_thi], [ma_lop_ao], [ma_dot_thi], [lan_thi]) VALUES (1, N'', 1, 1, 1)
GO
INSERT [dbo].[chi_tiet_dot_thi] ([ma_chi_tiet_dot_thi], [ten_chi_tiet_dot_thi], [ma_lop_ao], [ma_dot_thi], [lan_thi]) VALUES (2, N'Hello', 5, 3, 2)
GO
SET IDENTITY_INSERT [dbo].[chi_tiet_dot_thi] OFF
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 1, 1, 2, N'1234', 2)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 1, 2, 1, N'4321', 7)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 3, 14, N'1243', 12)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 4, 13, N'1234', 15)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 5, 3, N'4123', 20)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 6, 4, N'2341', 24)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 7, 5, N'2341', 27)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 8, 6, N'1243', 31)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 9, 7, N'3412', 33)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 10, 8, N'2413', 38)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 11, 9, N'2413', 42)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 12, 10, N'1243', 48)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 13, 11, N'4123', 50)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 2, 14, 12, N'2314', 53)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 3, 15, 16, N'1234', 58)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 3, 16, 15, N'1234', 64)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 4, 17, 17, N'3214', 65)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 4, 18, 18, N'3412', 70)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 5, 19, 19, N'3214', 73)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 5, 20, 20, N'1234', 77)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 5, 21, 21, N'1234', 82)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 5, 22, 22, N'4123', 88)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 5, 23, 23, N'2314', 92)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 24, 24, N'123', 93)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 25, 26, N'123', 97)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 26, 28, N'213', 100)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 27, 30, N'123', 103)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 28, 31, N'321', 107)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 29, 25, N'123', 108)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 30, 27, N'132', 111)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (1, 6, 31, 29, N'213', 115)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 1, 1, 1, N'1234', 2)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 1, 2, 2, N'4321', 7)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 3, 3, N'1243', 12)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 4, 4, N'1234', 15)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 5, 5, N'4123', 20)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 6, 6, N'2341', 24)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 7, 7, N'2341', 27)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 8, 8, N'1243', 31)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 9, 9, N'3412', 33)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 10, 10, N'2413', 38)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 11, 11, N'2413', 42)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 12, 12, N'1243', 48)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 13, 13, N'4123', 50)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 2, 14, 14, N'2314', 53)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 3, 15, 15, N'1234', 58)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 3, 16, 16, N'1234', 64)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 4, 17, 17, N'3214', 65)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 4, 18, 18, N'3412', 70)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 5, 19, 27, N'3214', 73)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 5, 20, 28, N'1234', 77)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 5, 21, 29, N'1234', 82)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 5, 22, 30, N'4123', 88)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 5, 23, 31, N'2314', 92)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 24, 19, N'123', 93)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 25, 20, N'123', 97)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 26, 21, N'213', 100)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 27, 22, N'123', 103)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 28, 23, N'321', 107)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 29, 24, N'123', 108)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 30, 25, N'132', 111)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (2, 6, 31, 26, N'213', 115)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 1, 1, 31, N'1234', 2)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 1, 2, 30, N'4321', 7)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 3, 29, N'1243', 12)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 4, 28, N'1234', 15)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 5, 27, N'4123', 20)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 6, 26, N'2341', 24)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 7, 25, N'2341', 27)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 8, 24, N'1243', 31)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 9, 23, N'3412', 33)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 10, 22, N'2413', 38)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 11, 21, N'2413', 42)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 12, 20, N'1243', 48)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 13, 19, N'4123', 50)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 2, 14, 18, N'2314', 53)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 3, 15, 17, N'1234', 58)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 3, 16, 16, N'1234', 64)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 4, 17, 15, N'3214', 65)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 4, 18, 14, N'3412', 70)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 5, 19, 9, N'3214', 73)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 5, 20, 10, N'1234', 77)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 5, 21, 11, N'1234', 82)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 5, 22, 12, N'4123', 88)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 5, 23, 13, N'2314', 92)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 24, 8, N'123', 93)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 25, 7, N'123', 97)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 26, 6, N'213', 100)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 27, 5, N'123', 103)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 28, 4, N'321', 107)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 29, 3, N'123', 108)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 30, 2, N'132', 111)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (3, 6, 31, 1, N'213', 115)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 1, 1, 2, N'1234', 2)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 1, 2, 1, N'4321', 7)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 3, 14, N'1243', 12)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 4, 13, N'1234', 15)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 5, 12, N'4123', 20)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 6, 11, N'2341', 24)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 7, 10, N'2341', 27)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 8, 9, N'1243', 31)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 9, 8, N'3412', 33)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 10, 7, N'2413', 38)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 11, 6, N'2413', 42)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 12, 5, N'1243', 48)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 13, 4, N'4123', 50)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 2, 14, 3, N'2314', 53)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 3, 15, 15, N'1234', 58)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 3, 16, 16, N'1234', 64)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 4, 17, 18, N'3214', 65)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 4, 18, 17, N'3412', 70)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 5, 19, 27, N'3214', 73)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 5, 20, 28, N'1234', 77)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 5, 21, 29, N'1234', 82)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 5, 22, 30, N'4123', 88)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 5, 23, 31, N'2314', 92)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 24, 26, N'123', 93)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 25, 25, N'123', 97)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 26, 19, N'213', 100)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 27, 20, N'123', 103)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 28, 21, N'321', 107)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 29, 22, N'123', 108)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 30, 23, N'132', 111)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (4, 6, 31, 24, N'213', 115)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 7, 32, 10, N'1234', 118)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 7, 33, 9, N'2314', 122)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 8, 34, 1, N'1234', 126)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 8, 35, 24, N'2314', 131)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 8, 36, 23, N'1234', 134)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 8, 37, 25, N'1432', 138)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 9, 38, 27, N'1234', 143)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 10, 39, 37, N'2314', 146)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 10, 40, 36, N'2314', 149)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 10, 41, 35, N'1234', 156)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 10, 42, 38, N'1234', 157)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 43, 11, N'1432', 162)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 44, 12, N'3214', 165)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 45, 13, N'1234', 171)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 46, 14, N'1432', 174)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 47, 15, N'3214', 179)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 48, 16, N'1234', 181)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 49, 17, N'1234', 186)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 50, 18, N'2314', 192)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 51, 19, N'3214', 194)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 52, 20, N'1234', 199)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 53, 21, N'1432', 201)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 12, 54, 22, N'3214', 208)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 55, 30, N'1234', 209)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 56, 31, N'1234', 214)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 57, 33, N'3214', 220)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 58, 34, N'1234', 224)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 59, 28, N'2314', 226)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 60, 32, N'1432', 229)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 13, 61, 29, N'3214', 234)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 62, 3, N'1234', 238)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 63, 7, N'3214', 244)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 64, 2, N'3214', 246)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 65, 6, N'1234', 251)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 66, 4, N'1234', 256)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 67, 1, N'1234', 257)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 68, 8, N'2314', 262)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 14, 69, 5, N'3214', 268)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 15, 70, 40, N'3214', 270)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (5, 15, 71, 39, N'1234', 274)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 7, 32, 1, N'1234', 118)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 7, 33, 2, N'3214', 122)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 8, 34, 38, N'1234', 126)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 8, 35, 40, N'1234', 131)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 8, 36, 37, N'3214', 134)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 8, 37, 39, N'1234', 138)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 9, 38, 34, N'3214', 143)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 10, 39, 25, N'3214', 146)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 10, 40, 22, N'1234', 149)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 10, 41, 24, N'2314', 156)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 10, 42, 23, N'1234', 157)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 43, 10, N'2314', 162)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 44, 11, N'1432', 165)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 45, 12, N'3214', 171)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 46, 13, N'1234', 174)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 47, 14, N'1432', 179)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 48, 15, N'3214', 181)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 49, 16, N'1234', 186)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 50, 17, N'1234', 192)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 51, 18, N'2314', 194)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 52, 19, N'3214', 199)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 53, 20, N'1234', 201)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 12, 54, 21, N'1432', 208)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 55, 4, N'3214', 209)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 56, 3, N'1234', 214)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 57, 7, N'1234', 220)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 58, 5, N'1234', 224)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 59, 6, N'2314', 226)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 60, 8, N'3214', 229)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 13, 61, 9, N'1234', 234)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 62, 29, N'1432', 238)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 63, 28, N'1234', 244)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 64, 31, N'1234', 246)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 65, 27, N'1234', 251)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 66, 32, N'3214', 256)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 67, 26, N'1234', 257)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 68, 30, N'2314', 262)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 14, 69, 33, N'1432', 268)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 15, 70, 35, N'2314', 270)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (6, 15, 71, 36, N'2314', 274)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 7, 32, 7, N'1234', 118)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 7, 33, 6, N'2314', 122)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 8, 34, 37, N'1234', 126)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 8, 35, 38, N'1234', 131)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 8, 36, 40, N'3214', 134)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 8, 37, 39, N'1234', 138)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 9, 38, 1, N'1234', 143)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 10, 39, 5, N'3214', 146)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 10, 40, 3, N'3214', 149)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 10, 41, 2, N'1234', 156)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 10, 42, 4, N'1234', 157)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 43, 17, N'1234', 162)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 44, 18, N'2314', 165)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 45, 19, N'3214', 171)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 46, 20, N'1234', 174)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 47, 21, N'1432', 179)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 48, 22, N'3214', 181)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 49, 23, N'1234', 186)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 50, 24, N'2314', 192)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 51, 25, N'1234', 194)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 52, 26, N'1432', 199)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 53, 27, N'1234', 201)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 12, 54, 28, N'1234', 208)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 55, 15, N'2314', 209)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 56, 12, N'1432', 214)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 57, 14, N'3214', 220)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 58, 13, N'1234', 224)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 59, 16, N'1432', 226)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 60, 10, N'3214', 229)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 13, 61, 11, N'1234', 234)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 62, 33, N'1234', 238)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 63, 34, N'3214', 244)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 64, 35, N'1234', 246)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 65, 36, N'2314', 251)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 66, 29, N'1432', 256)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 67, 32, N'3214', 257)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 68, 31, N'2314', 262)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 14, 69, 30, N'2314', 268)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 15, 70, 9, N'3214', 270)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 15, 71, 8, N'1234', 274)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (7, 19, 74, 43, N'1', 5)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 7, 32, 12, N'1432', 118)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 7, 33, 11, N'3214', 122)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 8, 34, 2, N'1234', 126)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 8, 35, 3, N'3214', 131)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 8, 36, 4, N'3214', 134)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 8, 37, 1, N'1234', 138)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 9, 38, 40, N'1234', 143)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 10, 39, 7, N'1234', 146)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 10, 40, 8, N'1234', 149)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 10, 41, 6, N'2314', 156)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 10, 42, 5, N'3214', 157)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 43, 13, N'1234', 162)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 44, 14, N'1432', 165)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 45, 15, N'3214', 171)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 46, 16, N'1234', 174)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 47, 17, N'1234', 179)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 48, 18, N'2314', 181)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 49, 19, N'3214', 186)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 50, 20, N'1234', 192)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 51, 21, N'1432', 194)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 52, 22, N'3214', 199)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 53, 23, N'1234', 201)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 12, 54, 24, N'2314', 208)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 55, 26, N'1234', 209)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 56, 31, N'1432', 214)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 57, 30, N'1234', 220)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 58, 27, N'1234', 224)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 59, 28, N'1234', 226)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 60, 25, N'3214', 229)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 13, 61, 29, N'1234', 234)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 62, 33, N'2314', 238)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 63, 35, N'1432', 244)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 64, 36, N'3214', 246)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 65, 34, N'2314', 251)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 66, 38, N'2314', 256)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 67, 39, N'1234', 257)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 68, 32, N'1234', 262)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 14, 69, 37, N'3214', 268)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 15, 70, 9, N'1234', 270)
GO
INSERT [dbo].[ChiTietDeThiHoanVi] ([MaDeHV], [MaNhom], [MaCauHoi], [ThuTu], [HoanViTraLoi], [DapAn]) VALUES (8, 15, 71, 10, N'2314', 274)
GO
SET IDENTITY_INSERT [dbo].[CLO] ON 
GO
INSERT [dbo].[CLO] ([MaCLO], [MaMonHoc], [MaSoCLO], [TieuDe], [NoiDung], [TieuChi(%)], [SoCau]) VALUES (1, 1, N'CLO1', N'Nhận biết cơ bản', N'Mức độ cơ bản nhất. Học sinh chỉ cần ghi nhớ, nhận ra kiến thức đã được học.', 40, 16)
GO
INSERT [dbo].[CLO] ([MaCLO], [MaMonHoc], [MaSoCLO], [TieuDe], [NoiDung], [TieuChi(%)], [SoCau]) VALUES (2, 1, N'CLO2', N'Thông hiểu, thông dụng', N'Học sinh cần hiểu được bản chất, giải thích được kiến thức đã học.', 35, 14)
GO
INSERT [dbo].[CLO] ([MaCLO], [MaMonHoc], [MaSoCLO], [TieuDe], [NoiDung], [TieuChi(%)], [SoCau]) VALUES (3, 1, N'CLO3', N'Vận dụng', N'Học sinh phải biết áp dụng kiến thức vào tình huống mới, giải quyết vấn đề đơn giản.', 15, 6)
GO
INSERT [dbo].[CLO] ([MaCLO], [MaMonHoc], [MaSoCLO], [TieuDe], [NoiDung], [TieuChi(%)], [SoCau]) VALUES (4, 1, N'CLO4', N'Vận dụng nâng cao', N'Mức độ khó nhất. Học sinh cần tư duy logic, sáng tạo, tổng hợp kiến thức để giải quyết bài toán khó, tình huống phức tạp.', 10, 4)
GO
SET IDENTITY_INSERT [dbo].[CLO] OFF
GO
SET IDENTITY_INSERT [dbo].[DeThi] ON 
GO
INSERT [dbo].[DeThi] ([MaDeThi], [MaMonHoc], [TenDeThi], [NgayTao], [NguoiTao], [GhiChu], [LuuTam], [DaDuyet], [TongSoDeHoanVi], [BoChuongPhan]) VALUES (1, 1, N'ĐỀ THI-TIẾNG_ANH-2024_04_12', CAST(N'2024-11-04T00:00:00.000' AS DateTime), -1, NULL, 0, 1, 4, 1)
GO
INSERT [dbo].[DeThi] ([MaDeThi], [MaMonHoc], [TenDeThi], [NgayTao], [NguoiTao], [GhiChu], [LuuTam], [DaDuyet], [TongSoDeHoanVi], [BoChuongPhan]) VALUES (2, 1, N'ĐỀ THI-TỔNG-HỢP-MÔN-2024_06_19', CAST(N'2024-06-19T00:00:00.000' AS DateTime), -1, NULL, 1, 0, 4, 0)
GO
SET IDENTITY_INSERT [dbo].[DeThi] OFF
GO
SET IDENTITY_INSERT [dbo].[DeThiHoanVi] ON 
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (1, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (2, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (3, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (4, 1, N'', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (5, 2, N'TTNN001', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (6, 2, N'TTNN002', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000001')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (7, 2, N'TTNN003', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000002')
GO
INSERT [dbo].[DeThiHoanVi] ([MaDeHV], [MaDeThi], [KyHieuDe], [NgayTao], [Guid]) VALUES (8, 2, N'TTNN004', CAST(N'2024-06-25T00:00:00.000' AS DateTime), N'00000000-0000-0000-0000-000000000003')
GO
SET IDENTITY_INSERT [dbo].[DeThiHoanVi] OFF
GO
SET IDENTITY_INSERT [dbo].[dot_thi] ON 
GO
INSERT [dbo].[dot_thi] ([ma_dot_thi], [ten_dot_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [NamHoc]) VALUES (1, N'THI ĐỒ ÁN CƠ SỞ', CAST(N'2024-04-11T00:00:00.000' AS DateTime), CAST(N'2024-04-13T00:00:00.000' AS DateTime), 2024)
GO
INSERT [dbo].[dot_thi] ([ma_dot_thi], [ten_dot_thi], [thoi_gian_bat_dau], [thoi_gian_ket_thuc], [NamHoc]) VALUES (3, N'Hello', CAST(N'2025-06-03T00:00:00.000' AS DateTime), CAST(N'2026-03-02T00:00:00.000' AS DateTime), 2025)
GO
SET IDENTITY_INSERT [dbo].[dot_thi] OFF
GO
SET IDENTITY_INSERT [dbo].[khoa] ON 
GO
INSERT [dbo].[khoa] ([ma_khoa], [ten_khoa], [ngay_thanh_lap]) VALUES (1, N'CNTT', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[khoa] OFF
GO
SET IDENTITY_INSERT [dbo].[lop] ON 
GO
INSERT [dbo].[lop] ([ma_lop], [ten_lop], [ngay_bat_dau], [ma_khoa]) VALUES (1, N'21DTHD2', CAST(N'2024-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[lop] ([ma_lop], [ten_lop], [ngay_bat_dau], [ma_khoa]) VALUES (2, N'21DTHD3', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[lop] OFF
GO
SET IDENTITY_INSERT [dbo].[lop_ao] ON 
GO
INSERT [dbo].[lop_ao] ([ma_lop_ao], [ten_lop_ao], [ngay_bat_dau], [ma_mon_hoc]) VALUES (1, N'E1-04.04', CAST(N'2024-04-11T22:12:49.787' AS DateTime), 1)
GO
INSERT [dbo].[lop_ao] ([ma_lop_ao], [ten_lop_ao], [ngay_bat_dau], [ma_mon_hoc]) VALUES (2, N'E2-05.06', CAST(N'2024-04-11T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[lop_ao] ([ma_lop_ao], [ten_lop_ao], [ngay_bat_dau], [ma_mon_hoc]) VALUES (4, N'E1.08-08 2', CAST(N'2025-04-10T00:00:00.000' AS DateTime), 4)
GO
INSERT [dbo].[lop_ao] ([ma_lop_ao], [ten_lop_ao], [ngay_bat_dau], [ma_mon_hoc]) VALUES (5, N'E12.09-09', CAST(N'2025-06-03T00:00:00.000' AS DateTime), 4)
GO
SET IDENTITY_INSERT [dbo].[lop_ao] OFF
GO
SET IDENTITY_INSERT [dbo].[mon_hoc] ON 
GO
INSERT [dbo].[mon_hoc] ([ma_mon_hoc], [ma_so_mon_hoc], [ten_mon_hoc]) VALUES (1, N'ENC106', N'Tổng Hợp Ngôn Ngữ')
GO
INSERT [dbo].[mon_hoc] ([ma_mon_hoc], [ma_so_mon_hoc], [ten_mon_hoc]) VALUES (2, N'SUM165', N'Tổng Hợp')
GO
INSERT [dbo].[mon_hoc] ([ma_mon_hoc], [ma_so_mon_hoc], [ten_mon_hoc]) VALUES (3, N'SUKA203', N'Ielts')
GO
INSERT [dbo].[mon_hoc] ([ma_mon_hoc], [ma_so_mon_hoc], [ten_mon_hoc]) VALUES (4, N'PINODAT', N'chiêm tinh 2')
GO
SET IDENTITY_INSERT [dbo].[mon_hoc] OFF
GO
SET IDENTITY_INSERT [dbo].[NhomCauHoi] ON 
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (1, 1, N'Phần 1: phát âm', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet to indicate the word whose underlined part differs from the other three in pronunciation in each of the following questions.</b>', 2, 0, 1, -1, 2, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (2, 1, N'Phần 2: ngữ pháp', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet to indicate the correct answer to each of the following questions. </b>', 12, 0, 2, -1, 12, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (3, 1, N'Phần 3: từ trái nghĩa', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet to indicate the word(s) OPPOSITE in meaning to the underlined word(s) in each of the following questions.</b>', 2, 0, 3, -1, 2, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (4, 1, N'Phần 4: từ đồng nghĩa', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet to indicate the word(s) CLOSEST in meaning to the underlined word(s) in each of the following questions.</b>', 2, 0, 4, -1, 2, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (5, 1, N'Phần 5: câu hỏi nhóm', 1, N'"<b>Read the following passage and mark the letter A, B, C, or D on your answer sheet to indicate the correct word or phrase that best fits each of the numbered blanks.<b><p>Urbanisation programmes are being ___(28)___ in many parts of the world, especially in densely populated regions with limited land and resources. It is the natural outcome of economic development and ___(29)___.  It has brought a lot of benefits to our society. ___(30)___, it also poses various problems for local authorities and town planners in the process of maintaining sustainable urbanisation, especially in developing countries.</p>
<p>When too many people cram into a small area, urban infrastructure can''t be effective. There will be a ___(31)___ of livable housing, energy and water supply. This will create overcrowded urban districts with no proper facilities.</p>
<p>Currently, fast urbanisation is taking place predominantly in developing countries where sustainable urbanisation has little relevance to people''s lives. Their houses are just shabby slums with poor sanitation. Their children only manage to get basic education. Hence, the struggle for survival is their first priority rather than anything else. Only when the quality of their existence is improved can they seek ___(32)___ other high values in their life.</p>"
', 5, 1, 5, -1, 5, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (6, 1, N'Phần 6: nghe', 2, N'<b>Listen to the presentation about unusual British festivals and choose the correct option to complete the sentence.</b><br/><audio controls><source src="./audio/helloim-going-to-talk-about-b1672025838.mp3"/> </audio>', 8, 1, 6, -1, 8, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (7, 2, N'Nhóm 1: Ngôn ngữ', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet for pronunciation', 2, 1, 2, -1, 2, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (8, 2, N'Nhóm 2: Database', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet to indicate the correct answer to each of the following questions "DATABASE". </b>', 4, 1, 3, -1, 4, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (9, 2, N'Nhóm 3: Phát âm', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet to indicate the word whose underlined part differs from the other three in pronunciation in each of the following questions.</b>', 1, 1, 4, -1, 1, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (10, 2, N'Nhóm 4: Dạng chuẩn', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet for BCN', 4, 1, 5, -1, 4, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (11, 2, N'Nhóm 5: Các nhóm câu hỏi điền khuyết', -1, N'<b>Read the following passage and mark the letter A, B, C or D to indicate the correct word or phrase that best fits each of the numbered blanks.</b>
', 1, 1, 6, -1, 1, 0)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (12, 2, N'Nhóm 5.1: Nội dung câu hỏi nhóm', 1, N'<p>Nowadays, everybody knows Apples and (*) ____ everybody knows that the company was founded by Steve Jobs, an American inventor and entrepreneur. He is (*) ____ recognized as a pioneer in the field of microcomputer revolution. He helped design the first Macintosh computer, (*) ____ a small computer graphics company into Pixar, the company behind Toy Story and The Monster Inc. </p><p>His countercultural lifestyle and philosophy was a product of the time and place of his (*) ____. Jobs was adopted and raised in San Francisco Bay Area during the 1960s. In 1972, Jobs attended Reed College from which he (*) ____ in next to no time. Jobs co-founded Apple in 1976 in order to sell Apple I personal computer. At that moment, he might (*) ____ imagine that only a year later the company tasted impressive victory with Apple II, one of the first highly successful (*) ____ personal computers. (*) ____, in 1985, following a long power struggle, Jobs was forced out of Apple. After leaving Apple, Jobs took (*) ____ of its members with him to found NeXT, a computer development company which was then bought by Apple. The purchase (*) ____ Jobs to become the company''s CEO once again.</p><p>Steve Jobs died in 2011 after battling with pancreatic cancer (*) ____ nearly a decade. Millions first learned of Job''s death on a (*) ____ which had been invented by himself.</p>
', 12, 0, 7, 11, 12, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (13, 2, N'Nhóm 6: Toán học', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet', 7, 1, 8, -1, 7, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (14, 2, N'Nhóm 7: Nghe', 2, N'<b>Listen to the presentation about unusual British festivals and choose the correct option to complete the sentence.</b><br/><audio controls><source src="./audio/What_are_you_going_to_do.mp3"/> </audio>', 8, 1, 9, -1, 8, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (15, 2, N'Nhóm 8: Hóa', -1, N'<b> Mark the letter A, B, C, or D on your answer sheet<br/><audio controls><source src="./audio/helloim-going-to-talk-about-b1672025838.mp3"/> </audio>', 2, 1, 10, -1, 2, 1)
GO
INSERT [dbo].[NhomCauHoi] ([MaNhom], [MaDeThi], [TenNhom], [KieuNoiDung], [NoiDung], [SoCauHoi], [HoanVi], [ThuTu], [MaNhomCha], [SoCauLay], [LaCauHoiNhom]) VALUES (17, 2, N'Nhóm 9: Trả lời đúng sai', -1, N'<b>Mark the letter A, B with A is true and B is false</b>', 2, 1, 11, -1, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[NhomCauHoi] OFF
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (1, 1, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (1, 2, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (1, 3, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (1, 4, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (1, 5, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (1, 6, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (2, 1, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (2, 2, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (2, 3, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (2, 4, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (2, 5, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (2, 6, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (3, 1, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (3, 2, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (3, 3, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (3, 4, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (3, 5, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (3, 6, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (4, 1, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (4, 2, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (4, 3, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (4, 4, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (4, 5, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (4, 6, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 7, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 8, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 9, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 10, 7)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 11, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 12, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 13, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 14, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (5, 15, 8)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 7, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 8, 8)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 9, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 10, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 11, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 12, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 13, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 14, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (6, 15, 7)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 7, 3)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 8, 8)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 9, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 10, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 11, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 12, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 13, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 14, 7)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 15, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 17, 9)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (7, 19, 10)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 7, 4)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 8, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 9, 8)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 10, 2)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 11, 5)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 12, 1)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 13, 6)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 14, 7)
GO
INSERT [dbo].[NhomCauHoiHoanVi] ([MaDeHV], [MaNhom], [ThuTu]) VALUES (8, 15, 3)
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([MaRole], [TenRole], [MoTa]) VALUES (1, N'Admin', N'Admin')
GO
INSERT [dbo].[Role] ([MaRole], [TenRole], [MoTa]) VALUES (2, N'KhaoThi', N'Phòng Khảo Thí')
GO
INSERT [dbo].[Role] ([MaRole], [TenRole], [MoTa]) VALUES (3, N'DaoTao', N'Phòng Đào Tạo')
GO
INSERT [dbo].[Role] ([MaRole], [TenRole], [MoTa]) VALUES (4, N'CNTT', N'Trung tâm CNTT')
GO
INSERT [dbo].[Role] ([MaRole], [TenRole], [MoTa]) VALUES (5, N'GiamThi', N'Giám thị')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[sinh_vien] ON 
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (1, N'Cao Hiển ', N'Đạt', 1, CAST(N'2003-03-18T00:00:00.000' AS DateTime), NULL, N'18/6B B?c Lân, Bà Ði?m, Hóc Môn, TP.HCM', N'hiendatcao13@gmail.com', N'0342429410', N'2180608276', N'd07c9341-52de-4e49-a3fc-b2fd3c83fe39', 0, CAST(N'2025-06-27T09:31:27.750' AS DateTime), CAST(N'2025-06-27T09:31:33.450' AS DateTime), NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (2, N'Đỗ Thùy', N'Dung', 0, CAST(N'2003-11-11T00:00:00.000' AS DateTime), 1, NULL, N'dothuydung14@gmail.com', NULL, N'2180607359', N'21c952e2-bb45-4cbc-be49-3d4089e1ec1d', 0, CAST(N'2025-05-14T18:05:39.950' AS DateTime), CAST(N'2025-06-25T09:17:29.223' AS DateTime), NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (3, N'Đặng Duy', N'Linh', 1, CAST(N'2003-07-15T00:00:00.000' AS DateTime), 1, NULL, N'dangduylinh15@gmail.com', NULL, N'2180608877', N'29cff326-4ba2-426c-847b-d2270d4e5720', 1, CAST(N'2025-06-25T17:10:42.953' AS DateTime), CAST(N'2025-03-26T22:39:11.950' AS DateTime), NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (4, N'Vương Khả', N'Thạch', 1, CAST(N'2003-09-08T00:00:00.000' AS DateTime), 1, NULL, N'vuongkhathach16@gmail.com', NULL, N'2180608012', N'47c11564-5180-4b24-8577-f610cf0ba4bb', 0, CAST(N'2024-10-30T09:52:22.377' AS DateTime), CAST(N'2024-10-30T09:52:36.297' AS DateTime), NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (5, N'Hello', N'Dat', 1, NULL, NULL, NULL, NULL, NULL, N'1234567', N'65011e14-e53c-47d6-9453-862eb8f49966', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (6, N'Pino', N'Đat', 1, NULL, NULL, NULL, NULL, NULL, N'123456', N'27891f56-d285-4759-822c-a52e84bb4035', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (7, N'Unknown', N'Me', 1, CAST(N'2003-03-19T00:00:00.000' AS DateTime), 1, N'18/6B', N'datcao@gmail.com', N'0342429410', N'2180601111', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (11, N'Trần Văn', N'Mười', 1, NULL, 1, NULL, NULL, NULL, N'2180607777', N'0fefcaac-3745-4f47-9b5a-ed20385acfee', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (12, N'Nguyễn Văn', N'Cẩm', 0, NULL, 1, NULL, NULL, NULL, N'2180601234', N'af4f1b86-b218-4a00-85db-a0dc99fa3989', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[sinh_vien] ([ma_sinh_vien], [ho_va_ten_lot], [ten_sinh_vien], [gioi_tinh], [ngay_sinh], [ma_lop], [dia_chi], [email], [dien_thoai], [ma_so_sinh_vien], [student_id], [is_logged_in], [last_logged_in], [last_logged_out], [Photo]) VALUES (13, N'Nguyễn Thị', N'Hòa', 0, NULL, 1, NULL, NULL, NULL, N'2180609999', N'8f7bd75e-7eee-4f18-a5d7-4145cd7d8da3', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[sinh_vien] OFF
GO
INSERT [dbo].[User] ([UserId], [LoginName], [Email], [Name], [Password], [MaRole], [DateCreated], [IsDeleted], [IsLockedOut], [LastActivityDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPwdAttemptCount], [FailedPwdAttemptWindowStart], [FailedPwdAnswerCount], [FailedPwdAnswerWindowStart], [PasswordSalt], [Comment], [IsBuildInUser]) VALUES (N'47b854c1-1a8d-487a-881b-13c4442de60c', N'khaothi', N'khaothi@examsuite.vn', N'Phòng Khảo Thí', N'$2a$12$DRDHpP8efDp4alPZhISj7.5W9FqxgSjNOx1Ywjt/vICAKQa6l3RIm', 2, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([UserId], [LoginName], [Email], [Name], [Password], [MaRole], [DateCreated], [IsDeleted], [IsLockedOut], [LastActivityDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPwdAttemptCount], [FailedPwdAttemptWindowStart], [FailedPwdAnswerCount], [FailedPwdAnswerWindowStart], [PasswordSalt], [Comment], [IsBuildInUser]) VALUES (N'f4a86e92-737d-4214-b6b2-520a76e71fc2', N'daotao', N'daotao@examsuite.vn', N'Phòng Đào tạo', N'$2a$12$my1zS/WaMFBVhGZP.OJeeeJf0foghg3TpmLB4uWld9hD2i4vbx50O', 3, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', 1)
GO
INSERT [dbo].[User] ([UserId], [LoginName], [Email], [Name], [Password], [MaRole], [DateCreated], [IsDeleted], [IsLockedOut], [LastActivityDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPwdAttemptCount], [FailedPwdAttemptWindowStart], [FailedPwdAnswerCount], [FailedPwdAnswerWindowStart], [PasswordSalt], [Comment], [IsBuildInUser]) VALUES (N'21bf922b-cc11-448c-a9c6-c98acab7c085', N'admin', N'admin@examsuite.vn', N'Administrator', N'$2a$12$G2VuZtMA/rQ72BbCq5wQYOgub7w2MYWafokCwF7E5t15J2q6D9WvG', 1, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 0, NULL, CAST(N'2025-06-27T09:32:09.847' AS DateTime), NULL, CAST(N'2025-04-08T17:20:15.417' AS DateTime), 0, NULL, NULL, CAST(N'2025-04-08T17:20:05.850' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[User] ([UserId], [LoginName], [Email], [Name], [Password], [MaRole], [DateCreated], [IsDeleted], [IsLockedOut], [LastActivityDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPwdAttemptCount], [FailedPwdAttemptWindowStart], [FailedPwdAnswerCount], [FailedPwdAnswerWindowStart], [PasswordSalt], [Comment], [IsBuildInUser]) VALUES (N'3ac347e7-0d07-451d-bbf4-d56370a8804a', N'ttcntt', N'ttcntt@examsuite.vn', N'TT CNTT', N'$2a$12$6S/V54uiykBISR/rt8k0eOUbkHb6/wz60/.esof6tGDA5YsV1knZG', 4, CAST(N'2010-03-25T17:30:18.677' AS DateTime), 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', 1)
GO
/****** Object:  Index [idex_MaChiTietCaThi]    Script Date: 6/27/2025 2:26:36 PM ******/
CREATE NONCLUSTERED INDEX [idex_MaChiTietCaThi] ON [dbo].[AudioListened]
(
	[MaChiTietCaThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChiTietBaiThi_MaChiTietCaThi]    Script Date: 6/27/2025 2:26:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChiTietBaiThi_MaChiTietCaThi] ON [dbo].[chi_tiet_bai_thi]
(
	[ma_chi_tiet_ca_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IDX_chi_tiet_ca_thi_ma_ca_thi]    Script Date: 6/27/2025 2:26:36 PM ******/
CREATE NONCLUSTERED INDEX [IDX_chi_tiet_ca_thi_ma_ca_thi] ON [dbo].[chi_tiet_ca_thi]
(
	[ma_ca_thi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_MSSV]    Script Date: 6/27/2025 2:26:36 PM ******/
ALTER TABLE [dbo].[sinh_vien] ADD  CONSTRAINT [UQ_MSSV] UNIQUE NONCLUSTERED 
(
	[ma_so_sinh_vien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
/****** Object:  StoredProcedure [dbo].[AudioListened_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AudioListened_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AudioListened_Save]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AudioListened_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AudioListened_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_Activate]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_CanInsertStudent]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_ForceDelete]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_ForceDelete]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

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
/****** Object:  StoredProcedure [dbo].[ca_thi_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_ForceRemove]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        -- Bảng tạm lưu ID liên quan
        DECLARE @ChiTietCaThi TABLE (ma_chi_tiet_ca_thi INT)

        -- Lấy dữ liệu vào bảng tạm
        INSERT INTO @ChiTietCaThi (ma_chi_tiet_ca_thi)
        SELECT ma_chi_tiet_ca_thi FROM chi_tiet_ca_thi WHERE ma_ca_thi = @ma_ca_thi

        -- Xoá theo thứ tự phụ thuộc
        DELETE FROM chi_tiet_bai_thi
        WHERE ma_chi_tiet_ca_thi IN (SELECT ma_chi_tiet_ca_thi FROM @ChiTietCaThi)

        DELETE FROM chi_tiet_ca_thi
        WHERE ma_ca_thi = @ma_ca_thi

        DELETE FROM ca_thi
        WHERE ma_ca_thi = @ma_ca_thi

        COMMIT
    END TRY
    BEGIN CATCH
        ROLLBACK

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_GetAll1]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_GetCount]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_HuyKichHoat]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_HuyKichHoat]
	@ma_ca_thi [int]
WITH EXECUTE AS CALLER
AS
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
GO
/****** Object:  StoredProcedure [dbo].[ca_thi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ca_thi_Insert]
	@ten_ca_thi [nvarchar](50),
	@ma_chi_tiet_dot_thi [int],
	@thoi_gian_bat_dau [datetime],
	@MaDeThi [int],
	@ThoiGianThi [int]
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
	[ThoiGianThi]
) 
VALUES 
(
	@ten_ca_thi,
	@ma_chi_tiet_dot_thi,
	@thoi_gian_bat_dau,
	@MaDeThi,
	--@IsActivated,
	@ThoiGianThi
)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[ca_thi_IsExists]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_Ketthuc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_Ketthuc1]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
    SELECT *
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_ma_chi_tiet_dot_thi_Search_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
    SELECT *
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_MaDotThi_MaLop]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectBy_MaDotThi_MaLop_LanThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectByMonHoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectByMonThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectCongGioMax]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectKetThuc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectPage]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectPaged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectResult]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_SelectRunning]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ca_thi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
	@ThoiGianThi [int]
WITH EXECUTE AS CALLER
AS
UPDATE [ca_thi] 
SET
	[ten_ca_thi] = @ten_ca_thi,
	[ma_chi_tiet_dot_thi] = @ma_chi_tiet_dot_thi,
	[thoi_gian_bat_dau] = @thoi_gian_bat_dau,
	[MaDeThi] = @MaDeThi,
	--[IsActivated ]=@IsActivated,
	[ThoiGianThi]=@ThoiGianThi
WHERE
	[ma_ca_thi] = @ma_ca_thi

GO
/****** Object:  StoredProcedure [dbo].[ca_thi_UpdateApproved]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_ForceDelete]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CauHoi_ForceDelete]
	@MaCauHoi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
/****** Object:  StoredProcedure [dbo].[CauHoi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectBy_MaNhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectDapAn]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauHoi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_CountBy_MaCauHoi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_SelectBy_MaCauHoi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CauTraLoi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_DaThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Insert_Batch]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Insert_Batch]
	@Data ChiTietBaiThiType READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
BEGIN TRANSACTION;
	SET NOCOUNT ON;

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
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Save]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Save_Batch]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_bai_thi_Save_Batch]
    @Data ChiTietBaiThiType READONLY
AS
BEGIN
BEGIN TRANSACTION;
	SET NOCOUNT ON;

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
END
GO
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi_Count]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectBy_ma_chi_tiet_ca_thi_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_SelectOne_v2]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_bai_thi_Update_v2]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_CongGio]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_ca_thi_ForceRemove]
	@ma_chi_tiet_ca_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Insert_Batch]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Save_Batch]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Select_GioCongThem]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Count]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_CountForSearch]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Page]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi_Search_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_ca_thi1]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_de_thi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_ma_sinh_vien]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_MaCaThi_MaSinhVien]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectBy_MaSinhVienThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_UpdateBatDau]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_ca_thi_UpdateKetThuc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[chi_tiet_dot_thi_ForceRemove]
	@ma_chi_tiet_dot_thi [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_ma_dot_thi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_ma_dot_thi_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_ma_lop_ao]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaCTDT_MaDotThi_MaLopAo]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaCTDT_MaLopAo_LanThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaDotThi_MaLopAo_LanThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectBy_MaLopAo_LanThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[chi_tiet_dot_thi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Insert_Batch]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChiTietDeThiHoanVi_Insert_Batch]
	@MaDeThi [int],
	@KyHieuDe [nvarchar](50) = NULL,
	@DanhSachThongTinDeThi DeThiType READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION;
	SET NOCOUNT ON;
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
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	THROW;
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_DapAn]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaCauHoi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaCauHoi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v2]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v3]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectBy_MaNhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectNhom1By_MaDeHV]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ChiTietDeThiHoanVi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Clo_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Clo_ForceRemove]
	@MaCLO [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
/****** Object:  StoredProcedure [dbo].[Clo_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Clo_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Clo_SelectBy_MaMonHoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Clo_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Clo_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_GetDeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_LayMaThongTinDeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_LayMaThongTinDeThiTheoNhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeCauHoi_SelectBy_DeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
            / COUNT(*) , 2) AS PhanTram
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
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeCauHoi_SelectBy_Nhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeDiem_SelectBy_DeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Custom_ThongKeNhom_SelectBy_DeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[CustomThongKeCapBacSV_SelectBy_DeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
    DT.GhiChu,
    DT.TenDeThi,
    CT.thoi_gian_bat_dau AS NgayThi,
    COUNT(DISTINCT CTCT.ma_chi_tiet_ca_thi) AS TongSV
  FROM DeThi DT
  JOIN DeThiHoanVi DTHV ON DT.MaDeThi = DTHV.MaDeThi
  JOIN chi_tiet_ca_thi CTCT ON CTCT.ma_de_thi = DTHV.MaDeHV AND CTCT.da_hoan_thanh = 1
  JOIN ca_thi CT ON CT.MaDeThi = DT.MaDeThi
  WHERE DT.MaDeThi = @MaDeThi
  GROUP BY DT.GhiChu, DT.TenDeThi, CT.thoi_gian_bat_dau;

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
      CTBT.MaCauHoi,
      COUNT(DISTINCT CTBT.ma_chi_tiet_ca_thi) AS SoSVTrong27Percent,
      SUM(CASE WHEN CTBT.KetQua = 1 THEN 1 ELSE 0 END) AS SoSVDungTrong27Percent
    FROM TopBottom TB
    JOIN chi_tiet_bai_thi CTBT
      ON TB.ma_chi_tiet_ca_thi = CTBT.ma_chi_tiet_ca_thi
    WHERE TB.category IS NOT NULL
    GROUP BY TB.category, CTBT.MaCauHoi
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
  
  -- Top group
  SUM(CASE WHEN Q.category = 'Top' THEN Q.SoSVTrong27Percent ELSE 0 END) AS SVTop,
  SUM(CASE WHEN Q.category = 'Top' THEN Q.SoSVDungTrong27Percent ELSE 0 END) AS SVTopDung,
  
  -- Bottom group
  SUM(CASE WHEN Q.category = 'Bottom' THEN Q.SoSVTrong27Percent ELSE 0 END) AS SVBottom,
  SUM(CASE WHEN Q.category = 'Bottom' THEN Q.SoSVDungTrong27Percent ELSE 0 END) AS SVBottomDung,

  -- Tổng toàn bộ
  ISNULL(ASCS.TongSoSVTraLoi, 0) AS TongSoSVTraLoi,
  ISNULL(ASCS.TongSoSVDung, 0) AS TongSoSVDung
  FROM Questions Q
  LEFT JOIN AllStudentsAnswers ASCS
    ON Q.MaCauHoi = ASCS.MaCauHoi
  GROUP BY Q.MaCauHoi, ASCS.TongSoSVTraLoi, ASCS.TongSoSVDung
  ORDER BY Q.MaCauHoi;
END
GO
/****** Object:  StoredProcedure [dbo].[DeThi_AutoUpdate_Stats]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_DecreamentTongSoDeHoanVi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_DeleteAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_ForceDelete]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThi_ForceDelete]
	@MaDeThi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

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
/****** Object:  StoredProcedure [dbo].[DeThi_IncreamentTongSoDeHoanVi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectBy_ma_de_hv]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectByMonHoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectByMonHoc_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_SelectTongSoDeHV]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThi_Update_Stats]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_DapAn]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_ForceDelete]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeThiHoanVi_ForceDelete]
	@MaDeHV [bigint]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectBy_MaDeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeThiHoanVi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dot_thi_ForceRemove]
    @ma_dot_thi INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
/****** Object:  StoredProcedure [dbo].[dot_thi_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_GetAll_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_SelectByMaNamHoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[dot_thi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[khoa_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[khoa_ForceRemove]
	@ma_khoa INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
/****** Object:  StoredProcedure [dbo].[khoa_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[khoa_GetAll_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[khoa_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[khoa_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[khoa_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[khoa_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ao_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ao_ForceRemove]
	@ma_lop_ao INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

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
/****** Object:  StoredProcedure [dbo].[lop_ao_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ao_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ao_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ao_SelectBy_ma_mon_hoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ao_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ao_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[lop_ForceRemove]
	@ma_lop [int]
WITH EXECUTE AS CALLER
AS
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

	COMMIT
END TRY
BEGIN CATCH
	ROLLBACK

	DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[lop_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_SelectBy_ma_khoa]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_SelectBy_ma_khoa_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_SelectBy_ten_lop]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[lop_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[mon_hoc_ForceRemove]
	@MaMonHoc [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

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
/****** Object:  StoredProcedure [dbo].[mon_hoc_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_GetAll_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_SelectBy_MaSoMonHoc]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[mon_hoc_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_ForceDelete]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NhomCauHoi_ForceDelete]
	@ma_nhom INT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;

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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_IsCauHoiNhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectAllBy_MaDeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectBy_MaDeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectBy_MaNhomCha]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectHoanViBy_MaDeThi]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_CountBy_MaNhomCha]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaDeHV]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhom]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectBy_MaNhomCha]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_SelectPageBy_MaNhomCha]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[NhomCauHoiHoanVi_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_ForceRemove]    Script Date: 6/27/2025 2:26:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sinh_vien_ForceRemove]
	@ma_sinh_vien [bigint]
WITH EXECUTE AS CALLER
AS
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
GO
/****** Object:  StoredProcedure [dbo].[sinh_vien_GetAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_Insert_Batch]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_Login]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_Logout]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_Remove]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_khoa]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_khoa_ma_lop]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_lop]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_lop_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_lop_Search_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_so_sinh_vien]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ma_so_sinh_vien1]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_student_id]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectBy_ten]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sinh_vien_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_CheckName]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_GetAll_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_GetAll_Search_Paged]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_Login]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_LoginFail]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_LoginSuccess]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_SelectAll]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_SelectByLoginName]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_SelectOne]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_UpdateLastActivity]    Script Date: 6/27/2025 2:26:36 PM ******/
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
/****** Object:  StoredProcedure [dbo].[User_UpdatePassword]    Script Date: 6/27/2025 2:26:36 PM ******/
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
ALTER DATABASE [HutechExam2024] SET  READ_WRITE 
GO
