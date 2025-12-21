using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Context;

public partial class QuanlythuvienContext : DbContext
{
    public QuanlythuvienContext()
    {
    }

    public QuanlythuvienContext(DbContextOptions<QuanlythuvienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDatTruoc> ChiTietDatTruocs { get; set; }

    public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

    public virtual DbSet<DanhGiaBinhLuan> DanhGiaBinhLuans { get; set; }

    public virtual DbSet<DatMuonTruoc> DatMuonTruocs { get; set; }

    public virtual DbSet<DocGia> DocGia { get; set; }

    public virtual DbSet<GiaoDichThanhToan> GiaoDichThanhToans { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<PhieuPhat> PhieuPhats { get; set; }

    public virtual DbSet<TacGia> TacGia { get; set; }

    public virtual DbSet<TaiLieu> TaiLieus { get; set; }

    public virtual DbSet<TheBanDoc> TheBanDocs { get; set; }

    public virtual DbSet<Theloai> Theloais { get; set; }

    public virtual DbSet<XuLyGiaHan> XuLyGiaHans { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDatTruoc>(entity =>
        {
            entity.HasKey(e => new { e.MaDatTruoc, e.MaTaiLieu }).HasName("PK__ChiTietD__EE304EF7F9FDDA4B");

            entity.ToTable("ChiTietDatTruoc");

            entity.HasOne(d => d.MaDatTruocNavigation).WithMany(p => p.ChiTietDatTruocs)
                .HasForeignKey(d => d.MaDatTruoc)
                .HasConstraintName("FK__ChiTietDa__MaDat__160F4887");

            entity.HasOne(d => d.MaTaiLieuNavigation).WithMany(p => p.ChiTietDatTruocs)
                .HasForeignKey(d => d.MaTaiLieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDa__MaTai__17036CC0");
        });

        modelBuilder.Entity<ChiTietPhieuMuon>(entity =>
        {
            entity.HasKey(e => new { e.MaPhieuMuon, e.MaTaiLieu }).HasName("PK__ChiTietP__AB19A84734A8F9E6");

            entity.ToTable("ChiTietPhieuMuon");

            entity.Property(e => e.PhiMuonTaiThoiDiem)
                .HasDefaultValue(0m)
                .HasColumnType("money");

            entity.HasOne(d => d.MaPhieuMuonNavigation).WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(d => d.MaPhieuMuon)
                .HasConstraintName("FK__ChiTietPh__MaPhi__7C4F7684");

            entity.HasOne(d => d.MaTaiLieuNavigation).WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(d => d.MaTaiLieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPh__MaTai__7D439ABD");
        });

        modelBuilder.Entity<DanhGiaBinhLuan>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGia___AA9515BFF06CF157");

            entity.ToTable("DanhGia_BinhLuan");

            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaDocGiaNavigation).WithMany(p => p.DanhGiaBinhLuans)
                .HasForeignKey(d => d.MaDocGia)
                .HasConstraintName("FK__DanhGia_B__MaDoc__6E01572D");

            entity.HasOne(d => d.MaTaiLieuNavigation).WithMany(p => p.DanhGiaBinhLuans)
                .HasForeignKey(d => d.MaTaiLieu)
                .HasConstraintName("FK__DanhGia_B__MaTai__6EF57B66");
        });

        modelBuilder.Entity<DatMuonTruoc>(entity =>
        {
            entity.HasKey(e => e.MaDatTruoc).HasName("PK__DatMuonT__81E1C492188E5406");

            entity.ToTable("DatMuonTruoc");

            entity.Property(e => e.HanLaySach).HasColumnType("datetime");
            entity.Property(e => e.MaNvduyet).HasColumnName("MaNVDuyet");
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Chờ xác nhận");

            entity.HasOne(d => d.MaDocGiaNavigation).WithMany(p => p.DatMuonTruocs)
                .HasForeignKey(d => d.MaDocGia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DatMuonTr__MaDoc__114A936A");

            entity.HasOne(d => d.MaNvduyetNavigation).WithMany(p => p.DatMuonTruocs)
                .HasForeignKey(d => d.MaNvduyet)
                .HasConstraintName("FK__DatMuonTr__MaNVD__123EB7A3");
        });

        modelBuilder.Entity<DocGia>(entity =>
        {
            entity.HasKey(e => e.MaDocGia).HasName("PK__DocGia__F165F945A037950F");

            entity.HasIndex(e => e.SoDienThoai, "UQ__DocGia__0389B7BD73644D17").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__DocGia__A9D10534A4F226E4").IsUnique();

            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(255)
                .HasDefaultValue("default_avatar.png");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(4);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken)
        .HasMaxLength(500)
        .IsUnicode(false);

            entity.Property(e => e.RefreshTokenExpiryTime)
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.TrangThaiTk)
                .HasDefaultValue((byte)1)
                .HasColumnName("TrangThaiTK");
            entity.Property(e => e.VaiTro)
                .HasMaxLength(3)
                .HasDefaultValue("Độc giả");
        });

        modelBuilder.Entity<GiaoDichThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaGd).HasName("PK__GiaoDich__2725AE81A78B2FC4");

            entity.ToTable("GiaoDichThanhToan");

            entity.Property(e => e.MaGd).HasColumnName("MaGD");
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.LoaiGiaoDich).HasMaxLength(50);
            entity.Property(e => e.NgayGiaoDich)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoTien).HasColumnType("money");

            entity.HasOne(d => d.MaDocGiaNavigation).WithMany(p => p.GiaoDichThanhToans)
                .HasForeignKey(d => d.MaDocGia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichT__MaDoc__1CBC4616");
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaNxb).HasName("PK__NhaXuatB__3A19482C000CEE11");

            entity.ToTable("NhaXuatBan");

            entity.HasIndex(e => e.TenNxb, "UQ__NhaXuatB__CCE3868D5959265C").IsUnique();

            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenNxb)
                .HasMaxLength(100)
                .HasColumnName("TenNXB");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70AA4EFD09E");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.MaDocGia, "UQ__NhanVien__F165F944B5F8F037").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.ChucVu)
                .HasMaxLength(50)
                .HasDefaultValue("Thủ thư");
            entity.Property(e => e.NgayVaoLam).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MaDocGiaNavigation).WithOne(p => p.NhanVien)
                .HasForeignKey<NhanVien>(d => d.MaDocGia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__MaDocG__4CA06362");
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.MaPhieuMuon).HasName("PK__PhieuMuo__C4C8222208FE4743");

            entity.ToTable("PhieuMuon");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayMuon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayThucTra).HasColumnType("datetime");
            entity.Property(e => e.NgayTra).HasColumnType("datetime");
            entity.Property(e => e.TongTien)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Đang mượn");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__MaNV__76969D2E");

            entity.HasOne(d => d.MaSoTheNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.MaSoThe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__MaSoT__778AC167");
        });

        modelBuilder.Entity<PhieuPhat>(entity =>
        {
            entity.HasKey(e => e.MaPhieuPhat).HasName("PK__PhieuPha__E874D25112DED8D0");

            entity.ToTable("PhieuPhat");

            entity.Property(e => e.LyDoPhat).HasMaxLength(100);
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhiPhat).HasColumnType("money");
            entity.Property(e => e.TrangThaiThanhToan).HasDefaultValue(false);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.PhieuPhats)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuPhat__MaNV__03F0984C");

            entity.HasOne(d => d.MaPhieuMuonNavigation).WithMany(p => p.PhieuPhats)
                .HasForeignKey(d => d.MaPhieuMuon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuPhat__MaPhi__02FC7413");
        });

        modelBuilder.Entity<TacGia>(entity =>
        {
            entity.HasKey(e => e.MaTacGia).HasName("PK__TacGia__F24E6756D4FD3E29");

            entity.HasIndex(e => e.TenTacGia, "UQ__TacGia__2D926E30C4224951").IsUnique();

            entity.Property(e => e.TenTacGia).HasMaxLength(100);
        });

        modelBuilder.Entity<TaiLieu>(entity =>
        {
            entity.HasKey(e => e.MaTaiLieu).HasName("PK__TaiLieu__FD18A65717B9E1EE");

            entity.ToTable("TaiLieu");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Lưu thông");
            entity.Property(e => e.AnhBia)
                .HasMaxLength(255)
                .HasDefaultValue("default_book.png");
            entity.Property(e => e.GiaBan)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.NgonNgu)
                .HasMaxLength(20)
                .HasDefaultValue("Tiếng Việt");
            entity.Property(e => e.PhiMuon)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.SoLuong).HasDefaultValue(0);
            entity.Property(e => e.SoLuongCon).HasDefaultValue(0);
            entity.Property(e => e.TenSach).HasMaxLength(100);

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.TaiLieus)
                .HasForeignKey(d => d.MaNxb)
                .HasConstraintName("FK_TaiLieu_NhaXuatBan");

            entity.HasMany(d => d.MaTacGia).WithMany(p => p.MaTaiLieus)
                .UsingEntity<Dictionary<string, object>>(
                    "TaiLieuTacGium",
                    r => r.HasOne<TacGia>().WithMany()
                        .HasForeignKey("MaTacGia")
                        .HasConstraintName("FK__TaiLieu_T__MaTac__656C112C"),
                    l => l.HasOne<TaiLieu>().WithMany()
                        .HasForeignKey("MaTaiLieu")
                        .HasConstraintName("FK__TaiLieu_T__MaTai__6477ECF3"),
                    j =>
                    {
                        j.HasKey("MaTaiLieu", "MaTacGia").HasName("PK__TaiLieu___823C4022CF7FC4D7");
                        j.ToTable("TaiLieu_TacGia");
                    });

            entity.HasMany(d => d.MaTheLoais).WithMany(p => p.MaTaiLieus)
                .UsingEntity<Dictionary<string, object>>(
                    "TaiLieuTheLoai",
                    r => r.HasOne<Theloai>().WithMany()
                        .HasForeignKey("MaTheLoai")
                        .HasConstraintName("FK__TaiLieu_T__MaThe__693CA210"),
                    l => l.HasOne<TaiLieu>().WithMany()
                        .HasForeignKey("MaTaiLieu")
                        .HasConstraintName("FK__TaiLieu_T__MaTai__68487DD7"),
                    j =>
                    {
                        j.HasKey("MaTaiLieu", "MaTheLoai").HasName("PK__TaiLieu___406B59639FE7F8A8");
                        j.ToTable("TaiLieu_TheLoai");
                    });
        });

        modelBuilder.Entity<TheBanDoc>(entity =>
        {
            entity.HasKey(e => e.MaSoThe).HasName("PK__TheBanDo__7125D40B1F7DB7D7");

            entity.ToTable("TheBanDoc");

            entity.HasIndex(e => e.MaDocGia, "UQ__TheBanDo__F165F944F44EB6BC").IsUnique();

            entity.Property(e => e.NgayCap).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NgayHetHan).HasDefaultValueSql("(dateadd(year,(1),getdate()))");
            entity.Property(e => e.TinhTrangThe)
                .HasMaxLength(10)
                .HasDefaultValue("Hoạt động");

            entity.HasOne(d => d.MaDocGiaNavigation).WithOne(p => p.TheBanDoc)
                .HasForeignKey<TheBanDoc>(d => d.MaDocGia)
                .HasConstraintName("FK__TheBanDoc__MaDoc__45F365D3");
        });

        modelBuilder.Entity<Theloai>(entity =>
        {
            entity.HasKey(e => e.MaTheLoai).HasName("PK__THELOAI__D73FF34AC145DC9E");

            entity.ToTable("THELOAI");

            entity.HasIndex(e => e.TenTheLoai, "UQ__THELOAI__327F958FD6E35F90").IsUnique();

            entity.Property(e => e.TenTheLoai).HasMaxLength(40);
        });

        modelBuilder.Entity<XuLyGiaHan>(entity =>
        {
            entity.HasKey(e => e.MaGiaHan).HasName("PK__XuLyGiaH__C3260BA4734604F7");

            entity.ToTable("XuLyGiaHan");

            entity.Property(e => e.MaNvduyet).HasColumnName("MaNVDuyet");
            entity.Property(e => e.NgayGiaHanMoi).HasColumnType("datetime");
            entity.Property(e => e.NgayYeuCau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThaiDuyet)
                .HasMaxLength(20)
                .HasDefaultValue("Chờ duyệt");

            entity.HasOne(d => d.MaNvduyetNavigation).WithMany(p => p.XuLyGiaHans)
                .HasForeignKey(d => d.MaNvduyet)
                .HasConstraintName("FK__XuLyGiaHa__MaNVD__0B91BA14");

            entity.HasOne(d => d.MaPhieuMuonNavigation).WithMany(p => p.XuLyGiaHans)
                .HasForeignKey(d => d.MaPhieuMuon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__XuLyGiaHa__MaPhi__0A9D95DB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
