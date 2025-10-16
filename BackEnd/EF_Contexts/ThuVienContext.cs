using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.EF_Contexts;

public partial class QlThuvienContext : DbContext
{
    public QlThuvienContext()
    {
    }

    public QlThuvienContext(DbContextOptions<QlThuvienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baocao> Baocaos { get; set; }

    public virtual DbSet<ChatbotFaq> ChatbotFaqs { get; set; }

    public virtual DbSet<ChatbotLog> ChatbotLogs { get; set; }

    public virtual DbSet<Chitietphieumuon> Chitietphieumuons { get; set; }

    public virtual DbSet<Chitietyeucaumuon> Chitietyeucaumuons { get; set; }

    public virtual DbSet<Docgia> Docgias { get; set; }

    public virtual DbSet<HoatdongLog> HoatdongLogs { get; set; }

    public virtual DbSet<LoginLog> LoginLogs { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Nhaxuatban> Nhaxuatbans { get; set; }

    public virtual DbSet<Phat> Phats { get; set; }

    public virtual DbSet<Phieumuon> Phieumuons { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<Theloai> Theloais { get; set; }

    public virtual DbSet<Thongbao> Thongbaos { get; set; }

    public virtual DbSet<Yeucaumuon> Yeucaumuons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baocao>(entity =>
        {
            entity.HasKey(e => e.Mabaocao).HasName("PK__BAOCAO__AA13419B3593FA18");

            entity.ToTable("BAOCAO");

            entity.Property(e => e.Mabaocao).HasColumnName("MABAOCAO");
            entity.Property(e => e.Loaibaocao)
                .HasMaxLength(100)
                .HasColumnName("LOAIBAOCAO");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
        });

        modelBuilder.Entity<ChatbotFaq>(entity =>
        {
            entity.HasKey(e => e.Mafaq).HasName("PK__CHATBOT___784D155A58137699");

            entity.ToTable("CHATBOT_FAQ");

            entity.Property(e => e.Mafaq).HasColumnName("MAFAQ");
            entity.Property(e => e.Cauhoi)
                .HasMaxLength(500)
                .HasColumnName("CAUHOI");
            entity.Property(e => e.Traloi).HasColumnName("TRALOI");
            entity.Property(e => e.Tukhoa)
                .HasMaxLength(200)
                .HasColumnName("TUKHOA");
        });

        modelBuilder.Entity<ChatbotLog>(entity =>
        {
            entity.HasKey(e => e.Malog).HasName("PK__CHATBOT___7A3DE268834CD6E7");

            entity.ToTable("CHATBOT_LOG");

            entity.Property(e => e.Malog).HasColumnName("MALOG");
            entity.Property(e => e.Cauhoi).HasColumnName("CAUHOI");
            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Ngayhoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYHOI");
            entity.Property(e => e.Nguon)
                .HasMaxLength(50)
                .HasColumnName("NGUON");
            entity.Property(e => e.Traloi).HasColumnName("TRALOI");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.ChatbotLogs)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK__CHATBOT_L__MANGU__693CA210");
        });

        modelBuilder.Entity<Chitietphieumuon>(entity =>
        {
            entity.HasKey(e => new { e.Maphieumuon, e.Masach }).HasName("PK__CHITIETP__EC6B8F8F881B5047");

            entity.ToTable("CHITIETPHIEUMUON");

            entity.Property(e => e.Maphieumuon).HasColumnName("MAPHIEUMUON");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Soluongmuon).HasColumnName("SOLUONGMUON");

            entity.HasOne(d => d.MaphieumuonNavigation).WithMany(p => p.Chitietphieumuons)
                .HasForeignKey(d => d.Maphieumuon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPH__MAPHI__797309D9");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.Chitietphieumuons)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPH__MASAC__7A672E12");
        });

        modelBuilder.Entity<Chitietyeucaumuon>(entity =>
        {
            entity.HasKey(e => new { e.Mayeucau, e.Masach }).HasName("PK__CHITIETY__93E7FEFA0CDF8F2A");

            entity.ToTable("CHITIETYEUCAUMUON");

            entity.Property(e => e.Mayeucau).HasColumnName("MAYEUCAU");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Soluongmuon).HasColumnName("SOLUONGMUON");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.Chitietyeucaumuons)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETYE__MASAC__75A278F5");

            entity.HasOne(d => d.MayeucauNavigation).WithMany(p => p.Chitietyeucaumuons)
                .HasForeignKey(d => d.Mayeucau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETYE__MAYEU__74AE54BC");
        });

        modelBuilder.Entity<Docgia>(entity =>
        {
            entity.HasKey(e => e.Madocgia).HasName("PK__DOCGIA__8CA726FC418C0ED6");

            entity.ToTable("DOCGIA");

            entity.HasIndex(e => e.Manguoidung, "UQ__DOCGIA__F6A55C8F108CF98F").IsUnique();

            entity.Property(e => e.Madocgia).HasColumnName("MADOCGIA");
            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Ngaycap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAYCAP");
            entity.Property(e => e.Ngayhethan).HasColumnName("NGAYHETHAN");
            entity.Property(e => e.Tinhtrangthe)
                .HasMaxLength(20)
                .HasDefaultValue("Còn hạn")
                .HasColumnName("TINHTRANGTHE");

            entity.HasOne(d => d.ManguoidungNavigation).WithOne(p => p.Docgium)
                .HasForeignKey<Docgia>(d => d.Manguoidung)
                .HasConstraintName("FK__DOCGIA__MANGUOID__3E52440B");
        });

        modelBuilder.Entity<HoatdongLog>(entity =>
        {
            entity.HasKey(e => e.Mahdlog).HasName("PK__HOATDONG__CC883D5DE6710459");

            entity.ToTable("HOATDONG_LOG");

            entity.Property(e => e.Mahdlog).HasColumnName("MAHDLOG");
            entity.Property(e => e.Doituong)
                .HasMaxLength(50)
                .HasColumnName("DOITUONG");
            entity.Property(e => e.Hanhdong)
                .HasMaxLength(100)
                .HasColumnName("HANHDONG");
            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
            entity.Property(e => e.Thoigian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("THOIGIAN");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.HoatdongLogs)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK__HOATDONG___MANGU__70DDC3D8");
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.HasKey(e => e.Mabanglhi).HasName("PK__LOGIN_LO__FF3D7A51162CC568");

            entity.ToTable("LOGIN_LOG");

            entity.Property(e => e.Mabanglhi).HasColumnName("MABANGLHI");
            entity.Property(e => e.DiaChiIp)
                .HasMaxLength(50)
                .HasColumnName("DIA_CHI_IP");
            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Thietbi)
                .HasMaxLength(100)
                .HasColumnName("THIETBI");
            entity.Property(e => e.Thoigian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("THOIGIAN");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.LoginLogs)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK__LOGIN_LOG__MANGU__6D0D32F4");
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.Manguoidung).HasName("PK__NGUOIDUN__F6A55C8E68D86F3D");

            entity.ToTable("NGUOIDUNG");

            entity.HasIndex(e => e.Email, "UQ_NGUOIDUNG_EMAIL_NOTNULL")
                .IsUnique()
                .HasFilter("([EMAIL] IS NOT NULL)");

            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValue("Hoạt động")
                .HasColumnName("TRANGTHAI");
            entity.Property(e => e.Vaitro)
                .HasMaxLength(20)
                .HasColumnName("VAITRO");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manhanvien).HasName("PK__NHANVIEN__7E46DD9109DB437D");

            entity.ToTable("NHANVIEN");

            entity.HasIndex(e => e.Manguoidung, "UQ__NHANVIEN__F6A55C8F64E63E6E").IsUnique();

            entity.Property(e => e.Manhanvien).HasColumnName("MANHANVIEN");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(50)
                .HasColumnName("CHUCVU");
            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Ngayvaolam)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAYVAOLAM");

            entity.HasOne(d => d.ManguoidungNavigation).WithOne(p => p.Nhanvien)
                .HasForeignKey<Nhanvien>(d => d.Manguoidung)
                .HasConstraintName("FK__NHANVIEN__MANGUO__4316F928");
        });

        modelBuilder.Entity<Nhaxuatban>(entity =>
        {
            entity.HasKey(e => e.Manxb).HasName("PK__NHAXUATB__7ABD9EF2069087C8");

            entity.ToTable("NHAXUATBAN");

            entity.Property(e => e.Manxb).HasColumnName("MANXB");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.Tennxb)
                .HasMaxLength(200)
                .HasColumnName("TENNXB");
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .HasColumnName("WEBSITE");
        });

        modelBuilder.Entity<Phat>(entity =>
        {
            entity.HasKey(e => e.Maphat).HasName("PK__PHAT__54001981BB2AD573");

            entity.ToTable("PHAT");

            entity.Property(e => e.Maphat).HasColumnName("MAPHAT");
            entity.Property(e => e.Dathanhtoan)
                .HasDefaultValue(false)
                .HasColumnName("DATHANHTOAN");
            entity.Property(e => e.Lydo)
                .HasMaxLength(255)
                .HasColumnName("LYDO");
            entity.Property(e => e.Maphieumuon).HasColumnName("MAPHIEUMUON");
            entity.Property(e => e.Ngayphat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAYPHAT");
            entity.Property(e => e.Sotien)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("SOTIEN");

            entity.HasOne(d => d.MaphieumuonNavigation).WithMany(p => p.Phats)
                .HasForeignKey(d => d.Maphieumuon)
                .HasConstraintName("FK__PHAT__MAPHIEUMUO__5BE2A6F2");
        });

        modelBuilder.Entity<Phieumuon>(entity =>
        {
            entity.HasKey(e => e.Maphieumuon).HasName("PK__PHIEUMUO__3F97C76BA1A33381");

            entity.ToTable("PHIEUMUON");

            entity.Property(e => e.Maphieumuon).HasColumnName("MAPHIEUMUON");
            entity.Property(e => e.Hantra).HasColumnName("HANTRA");
            entity.Property(e => e.Madocgia).HasColumnName("MADOCGIA");
            entity.Property(e => e.Ngaymuon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAYMUON");
            entity.Property(e => e.Ngaytra).HasColumnName("NGAYTRA");
            entity.Property(e => e.Sotienphat)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("SOTIENPHAT");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValue("Đang mượn")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.MadocgiaNavigation).WithMany(p => p.Phieumuons)
                .HasForeignKey(d => d.Madocgia)
                .HasConstraintName("FK__PHIEUMUON__MADOC__52593CB8");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__SACH__3FC48E4C28B848AB");

            entity.ToTable("SACH");

            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Anhbia)
                .HasMaxLength(255)
                .HasColumnName("ANHBIA");
            entity.Property(e => e.Manxb).HasColumnName("MANXB");
            entity.Property(e => e.Maqr)
                .HasMaxLength(100)
                .HasColumnName("MAQR");
            entity.Property(e => e.Matheloai).HasColumnName("MATHELOAI");
            entity.Property(e => e.Namxuatban).HasColumnName("NAMXUATBAN");
            entity.Property(e => e.Soluong)
                .HasDefaultValue(0)
                .HasColumnName("SOLUONG");
            entity.Property(e => e.Tacgia)
                .HasMaxLength(100)
                .HasColumnName("TACGIA");
            entity.Property(e => e.Tensach)
                .HasMaxLength(200)
                .HasColumnName("TENSACH");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValue("Có sẵn")
                .HasColumnName("TRANGTHAI");
            entity.Property(e => e.Vitrisach)
                .HasMaxLength(50)
                .HasColumnName("VITRISACH");

            entity.HasOne(d => d.ManxbNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Manxb)
                .HasConstraintName("FK__SACH__MANXB__4CA06362");

            entity.HasOne(d => d.MatheloaiNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Matheloai)
                .HasConstraintName("FK__SACH__MATHELOAI__4BAC3F29");
        });

        modelBuilder.Entity<Theloai>(entity =>
        {
            entity.HasKey(e => e.Matheloai).HasName("PK__THELOAI__AC8D7C2B6B922CDF");

            entity.ToTable("THELOAI");

            entity.Property(e => e.Matheloai).HasColumnName("MATHELOAI");
            entity.Property(e => e.Tentheloai)
                .HasMaxLength(100)
                .HasColumnName("TENTHELOAI");
        });

        modelBuilder.Entity<Thongbao>(entity =>
        {
            entity.HasKey(e => e.Mathongbao).HasName("PK__THONGBAO__3F155D4A7A072A24");

            entity.ToTable("THONGBAO");

            entity.Property(e => e.Mathongbao).HasColumnName("MATHONGBAO");
            entity.Property(e => e.Loai)
                .HasMaxLength(20)
                .HasColumnName("LOAI");
            entity.Property(e => e.Manguoidung).HasColumnName("MANGUOIDUNG");
            entity.Property(e => e.Ngaygui)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYGUI");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
            entity.Property(e => e.Tieude)
                .HasMaxLength(200)
                .HasColumnName("TIEUDE");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValue("Chờ gửi")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.Thongbaos)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK__THONGBAO__MANGUO__60A75C0F");
        });

        modelBuilder.Entity<Yeucaumuon>(entity =>
        {
            entity.HasKey(e => e.Mayeucau).HasName("PK__YEUCAUMU__401BB61E8FB053AC");

            entity.ToTable("YEUCAUMUON");

            entity.Property(e => e.Mayeucau).HasColumnName("MAYEUCAU");
            entity.Property(e => e.Madocgia).HasColumnName("MADOCGIA");
            entity.Property(e => e.Ngayyeucau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYYEUCAU");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValue("Chờ duyệt")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.MadocgiaNavigation).WithMany(p => p.Yeucaumuons)
                .HasForeignKey(d => d.Madocgia)
                .HasConstraintName("FK__YEUCAUMUO__MADOC__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
