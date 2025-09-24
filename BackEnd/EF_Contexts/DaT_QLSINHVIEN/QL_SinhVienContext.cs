using Microsoft.EntityFrameworkCore;

public class QL_SinhVienContext : DbContext
{
    public QL_SinhVienContext(DbContextOptions<QL_SinhVienContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Diem>()
            .ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<SinhVien>()
            .ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<Diem>()
            .HasKey(d => new { d.MaSV, d.MaMH, d.LanThi }); 
    }
    public DbSet<SinhVien> SinhViens { get; set; }
    public DbSet<Lop> Lops { get; set; }
    public DbSet<Khoa> Khoas { get; set; }
    public DbSet<MonHoc> MHs { get; set; }
    public DbSet<Diem> Diems { get; set; }
    public DbSet<GiangDay> GiangDays { get; set; }
    public DbSet<GiangVien> GiangViens { get; set; }
    public DbSet<ThanNhan> ThanNhans { get; set; }
    public DbSet<QUANHE> QuanHes { get; set; }
}
