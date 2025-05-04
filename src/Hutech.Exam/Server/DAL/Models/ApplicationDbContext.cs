using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hutech.Exam.Shared.Models;

public partial class ApplicationDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<AudioListened> AudioListeneds { get; set; }

    public virtual DbSet<CaThi> CaThis { get; set; }

    public virtual DbSet<CauHoi> CauHois { get; set; }

    public virtual DbSet<CauTraLoi> CauTraLois { get; set; }

    public virtual DbSet<ChiTietBaiThi> ChiTietBaiThis { get; set; }

    public virtual DbSet<ChiTietCaThi> ChiTietCaThis { get; set; }

    public virtual DbSet<ChiTietDeThi> ChiTietDeThis { get; set; }

    public virtual DbSet<ChiTietDeThiHoanVi> ChiTietDeThiHoanVis { get; set; }

    public virtual DbSet<ChiTietDotThi> ChiTietDotThis { get; set; }

    public virtual DbSet<Clo> Clos { get; set; }

    public virtual DbSet<DeThi> DeThis { get; set; }

    public virtual DbSet<DeThiHoanVi> DeThiHoanVis { get; set; }

    public virtual DbSet<DotThi> DotThis { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<LoaiCauHoi> LoaiCauHois { get; set; }

    public virtual DbSet<LoiDeThi> LoiDeThis { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<LopAo> LopAos { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<NhomCauHoi> NhomCauHois { get; set; }

    public virtual DbSet<NhomCauHoiHoanVi> NhomCauHoiHoanVis { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<SinhVienDuPhong> SinhVienDuPhongs { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AudioListened>(entity =>
        {
            entity.HasKey(e => e.ListenId);

            entity.ToTable("AudioListened");

            entity.Property(e => e.ListenId).HasColumnName("ListenID");
        });

        modelBuilder.Entity<CaThi>(entity =>
        {
            entity.HasKey(e => e.MaCaThi);

            entity.ToTable("ca_thi");

            entity.Property(e => e.MaCaThi).HasColumnName("ma_ca_thi");
            entity.Property(e => e.ActivatedDate).HasColumnType("datetime");
            entity.Property(e => e.ApprovedComments).HasMaxLength(500);
            entity.Property(e => e.ApprovedDate).HasColumnType("date");
            entity.Property(e => e.MaChiTietDotThi).HasColumnName("ma_chi_tiet_dot_thi");
            entity.Property(e => e.MatMa).HasMaxLength(128);
            entity.Property(e => e.TenCaThi)
                .HasMaxLength(50)
                .HasColumnName("ten_ca_thi");
            entity.Property(e => e.ThoiDiemKetThuc).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");

            entity.HasOne(d => d.MaChiTietDotThiNavigation).WithMany(p => p.CaThis)
                .HasForeignKey(d => d.MaChiTietDotThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ca_thi_chi_tiet_dot_thi");
        });

        modelBuilder.Entity<CauHoi>(entity =>
        {
            entity.HasKey(e => e.MaCauHoi);

            entity.ToTable("CauHoi");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.HoanVi)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.KieuNoiDung).HasDefaultValueSql("((-1))");
            entity.Property(e => e.MaClo).HasColumnName("MaCLO");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.TieuDe).HasMaxLength(250);

            entity.HasOne(d => d.MaCloNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.MaClo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CauHoi_CLO");

            entity.HasOne(d => d.MaNhomNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.MaNhom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CauHoi_NhomCauHoi");
        });

        modelBuilder.Entity<CauTraLoi>(entity =>
        {
            entity.HasKey(e => e.MaCauTraLoi);

            entity.ToTable("CauTraLoi");

            entity.Property(e => e.MaCauHoi).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaCauHoiNavigation).WithMany(p => p.CauTraLois)
                .HasForeignKey(d => d.MaCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CauTraLoi_CauHoi");
        });

        modelBuilder.Entity<ChiTietBaiThi>(entity =>
        {
            entity.HasKey(e => e.MaChiTietBaiThi);

            entity.ToTable("chi_tiet_bai_thi");

            entity.Property(e => e.MaChiTietCaThi).HasColumnName("ma_chi_tiet_ca_thi");
            entity.Property(e => e.MaClo).HasColumnName("MaCLO");
            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaChiTietCaThiNavigation).WithMany(p => p.ChiTietBaiThis)
                .HasForeignKey(d => d.MaChiTietCaThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chi_tiet_bai_thi_chi_tiet_ca_thi");
        });

        modelBuilder.Entity<ChiTietCaThi>(entity =>
        {
            entity.HasKey(e => e.MaChiTietCaThi);

            entity.ToTable("chi_tiet_ca_thi");

            entity.Property(e => e.MaChiTietCaThi).HasColumnName("ma_chi_tiet_ca_thi");
            entity.Property(e => e.DaHoanThanh).HasColumnName("da_hoan_thanh");
            entity.Property(e => e.DaThi).HasColumnName("da_thi");
            entity.Property(e => e.Diem)
                .HasDefaultValueSql("((-1))")
                .HasColumnName("diem");
            entity.Property(e => e.GioCongThem).HasColumnName("gio_cong_them");
            entity.Property(e => e.LyDoCong).HasColumnName("ly_do_cong");
            entity.Property(e => e.MaCaThi).HasColumnName("ma_ca_thi");
            entity.Property(e => e.MaDeThi).HasColumnName("ma_de_thi");
            entity.Property(e => e.MaSinhVien).HasColumnName("ma_sinh_vien");
            entity.Property(e => e.SoCauDung)
                .HasDefaultValueSql("((0))")
                .HasColumnName("so_cau_dung");
            entity.Property(e => e.ThoiDiemCong)
                .HasColumnType("datetime")
                .HasColumnName("thoi_diem_cong");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_ket_thuc");
            entity.Property(e => e.TongSoCau)
                .HasDefaultValueSql("((0))")
                .HasColumnName("tong_so_cau");

            entity.HasOne(d => d.MaCaThiNavigation).WithMany(p => p.ChiTietCaThis)
                .HasForeignKey(d => d.MaCaThi)
                .HasConstraintName("FK_chi_tiet_ca_thi_ca_thi");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.ChiTietCaThis)
                .HasForeignKey(d => d.MaSinhVien)
                .HasConstraintName("FK_chi_tiet_ca_thi_sinh_vien");
        });

        modelBuilder.Entity<ChiTietDeThi>(entity =>
        {
            entity.HasKey(e => new { e.MaNhom, e.MaCauHoi }).HasName("PK_tbl_ChiTietDeThi");

            entity.ToTable("ChiTietDeThi");
        });

        modelBuilder.Entity<ChiTietDeThiHoanVi>(entity =>
        {
            entity.HasKey(e => new { e.MaDeHv, e.MaNhom, e.MaCauHoi });

            entity.ToTable("ChiTietDeThiHoanVi");

            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.HoanViTraLoi).HasMaxLength(4);
            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaCauHoiNavigation).WithMany(p => p.ChiTietDeThiHoanVis)
                .HasForeignKey(d => d.MaCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDeThiHoanVi_CauHoi");

            entity.HasOne(d => d.Ma).WithMany(p => p.ChiTietDeThiHoanVis)
                .HasForeignKey(d => new { d.MaDeHv, d.MaNhom })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDeThiHoanVi_NhomCauHoiHoanVi");
        });

        modelBuilder.Entity<ChiTietDotThi>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDotThi);

            entity.ToTable("chi_tiet_dot_thi");

            entity.Property(e => e.MaChiTietDotThi).HasColumnName("ma_chi_tiet_dot_thi");
            entity.Property(e => e.LanThi)
                .HasMaxLength(200)
                .HasColumnName("lan_thi");
            entity.Property(e => e.MaDotThi).HasColumnName("ma_dot_thi");
            entity.Property(e => e.MaLopAo).HasColumnName("ma_lop_ao");
            entity.Property(e => e.TenChiTietDotThi)
                .HasMaxLength(200)
                .HasColumnName("ten_chi_tiet_dot_thi");

            entity.HasOne(d => d.MaDotThiNavigation).WithMany(p => p.ChiTietDotThis)
                .HasForeignKey(d => d.MaDotThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chi_tiet_dot_thi_dot_thi1");

            entity.HasOne(d => d.MaLopAoNavigation).WithMany(p => p.ChiTietDotThis)
                .HasForeignKey(d => d.MaLopAo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chi_tiet_dot_thi_lop_ao");
        });

        modelBuilder.Entity<Clo>(entity =>
        {
            entity.HasKey(e => e.MaClo);

            entity.ToTable("CLO");

            entity.Property(e => e.MaClo).HasColumnName("MaCLO");
            entity.Property(e => e.MaSoClo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MaSoCLO");
            entity.Property(e => e.TieuChi).HasColumnName("TieuChi(%)");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Clos)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLO_mon_hoc");
        });

        modelBuilder.Entity<DeThi>(entity =>
        {
            entity.HasKey(e => e.MaDeThi);

            entity.ToTable("DeThi");

            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.LuuTam)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.MaMonHoc).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiTao).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TenDeThi).HasMaxLength(250);
        });

        modelBuilder.Entity<DeThiHoanVi>(entity =>
        {
            entity.HasKey(e => e.MaDeHv);

            entity.ToTable("DeThiHoanVi");

            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.KyHieuDe).HasMaxLength(50);
            entity.Property(e => e.MaDeThi).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaDeThiNavigation).WithMany(p => p.DeThiHoanVis)
                .HasForeignKey(d => d.MaDeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeThiHoanVi_DeThi1");
        });

        modelBuilder.Entity<DotThi>(entity =>
        {
            entity.HasKey(e => e.MaDotThi);

            entity.ToTable("dot_thi");

            entity.Property(e => e.MaDotThi).HasColumnName("ma_dot_thi");
            entity.Property(e => e.TenDotThi)
                .HasMaxLength(150)
                .HasColumnName("ten_dot_thi");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_ket_thuc");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa);

            entity.ToTable("khoa");

            entity.Property(e => e.MaKhoa).HasColumnName("ma_khoa");
            entity.Property(e => e.NgayThanhLap)
                .HasColumnType("datetime")
                .HasColumnName("ngay_thanh_lap");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(30)
                .HasColumnName("ten_khoa");
        });

        modelBuilder.Entity<LoaiCauHoi>(entity =>
        {
            entity.HasKey(e => e.MaLoaiCauHoi);

            entity.ToTable("LoaiCauHoi");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
        });

        modelBuilder.Entity<LoiDeThi>(entity =>
        {
            entity.HasKey(e => e.MaLoi);

            entity.ToTable("LoiDeThi");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop);

            entity.ToTable("lop");

            entity.Property(e => e.MaLop).HasColumnName("ma_lop");
            entity.Property(e => e.MaKhoa).HasColumnName("ma_khoa");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.TenLop)
                .HasMaxLength(50)
                .HasColumnName("ten_lop");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK_lop_khoa");
        });

        modelBuilder.Entity<LopAo>(entity =>
        {
            entity.HasKey(e => e.MaLopAo);

            entity.ToTable("lop_ao");

            entity.Property(e => e.MaLopAo).HasColumnName("ma_lop_ao");
            entity.Property(e => e.MaMonHoc).HasColumnName("ma_mon_hoc");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.TenLopAo)
                .HasMaxLength(200)
                .HasColumnName("ten_lop_ao");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.LopAos)
                .HasForeignKey(d => d.MaMonHoc)
                .HasConstraintName("FK_lop_ao_mon_hoc");
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.MaMonHoc);

            entity.ToTable("mon_hoc");

            entity.Property(e => e.MaMonHoc).HasColumnName("ma_mon_hoc");
            entity.Property(e => e.MaSoMonHoc)
                .HasMaxLength(50)
                .HasColumnName("ma_so_mon_hoc");
            entity.Property(e => e.TenMonHoc)
                .HasMaxLength(200)
                .HasColumnName("ten_mon_hoc");
        });

        modelBuilder.Entity<NhomCauHoi>(entity =>
        {
            entity.HasKey(e => e.MaNhom);

            entity.ToTable("NhomCauHoi");

            entity.Property(e => e.LaCauHoiNhom).HasDefaultValueSql("((0))");
            entity.Property(e => e.MaDeThi).HasDefaultValueSql("((-1))");
            entity.Property(e => e.MaNhomCha).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.SoCauLay).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TenNhom).HasMaxLength(250);

            entity.HasOne(d => d.MaDeThiNavigation).WithMany(p => p.NhomCauHois)
                .HasForeignKey(d => d.MaDeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhomCauHoi_DeThi");
        });

        modelBuilder.Entity<NhomCauHoiHoanVi>(entity =>
        {
            entity.HasKey(e => new { e.MaDeHv, e.MaNhom }).HasName("PK_NhomHoanVi");

            entity.ToTable("NhomCauHoiHoanVi");

            entity.Property(e => e.MaDeHv).HasColumnName("MaDeHV");
            entity.Property(e => e.ThuTu).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaDeHvNavigation).WithMany(p => p.NhomCauHoiHoanVis)
                .HasForeignKey(d => d.MaDeHv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhomCauHoiHoanVi_DeThiHoanVi");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.MaRole);

            entity.ToTable("Role");

            entity.Property(e => e.TenRole).HasMaxLength(250);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSinhVien);

            entity.ToTable("sinh_vien");

            entity.Property(e => e.MaSinhVien).HasColumnName("ma_sinh_vien");
            entity.Property(e => e.DiaChi)
                .HasColumnType("text")
                .HasColumnName("dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .HasColumnName("dien_thoai");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.GioiTinh).HasColumnName("gioi_tinh");
            entity.Property(e => e.HoVaTenLot)
                .HasMaxLength(300)
                .HasColumnName("ho_va_ten_lot");
            entity.Property(e => e.IsLoggedIn).HasColumnName("is_logged_in");
            entity.Property(e => e.LastLoggedIn)
                .HasColumnType("datetime")
                .HasColumnName("last_logged_in");
            entity.Property(e => e.LastLoggedOut)
                .HasColumnType("datetime")
                .HasColumnName("last_logged_out");
            entity.Property(e => e.MaLop).HasColumnName("ma_lop");
            entity.Property(e => e.MaSoSinhVien)
                .HasMaxLength(50)
                .HasColumnName("ma_so_sinh_vien");
            entity.Property(e => e.NgaySinh)
                .HasColumnType("datetime")
                .HasColumnName("ngay_sinh");
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TenSinhVien)
                .HasMaxLength(50)
                .HasColumnName("ten_sinh_vien");
        });

        modelBuilder.Entity<SinhVienDuPhong>(entity =>
        {
            entity.HasKey(e => e.MaSinhVienDuPhong);

            entity.ToTable("SinhVien_DuPhong");

            entity.Property(e => e.MaSinhVienDuPhong).HasColumnName("ma_sinh_vien_du_phong");
            entity.Property(e => e.MaCaThi).HasColumnName("ma_ca_thi");
            entity.Property(e => e.MaLopAo).HasColumnName("ma_lop_ao");
            entity.Property(e => e.MaSinhVien).HasColumnName("ma_sinh_vien");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.MaThongBao);

            entity.ToTable("ThongBao");

            entity.Property(e => e.MaThongBao).ValueGeneratedNever();
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasColumnType("ntext");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FailedPwdAnswerWindowStart).HasColumnType("datetime");
            entity.Property(e => e.FailedPwdAttemptWindowStart).HasColumnType("datetime");
            entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
            entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");
            entity.Property(e => e.LoginName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);

            entity.HasOne(d => d.MaRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.MaRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
