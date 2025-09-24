using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("SINHVIEN")]
public class SinhVien
{
    [Key]
    [Required]
    public string MaSV { get; set; }
    public string? HoTen { get; set; }
    public DateTime? NGSINH { get; set; }
    public string? GTinh { get; set; }
    public string? DChi { get; set; }
    public string? MaLop { get; set; }
    public string? Password { get; set; }

}
[Table("LOP")]
public class Lop
{
    [Key]
    public string MaLop { get; set; }
    public string TenLop { get; set; }
    public int SiSo { get; set; }
    public string? LopTruong { get; set; }
    public string MaKH { get; set; }
}
[Table("KHOA")]
public class Khoa
{
    [Key]
    public string MaKH { get; set; }
    public string TenKH { get; set; }
}
[Table("MONHOC")]
public class MonHoc
{
    [Key]
    public string MaMH { get; set; }
    public string TenMH { get; set; }
    public int SoTC { get; set; }
}
[Table("DIEM")]
public class Diem
{
    [Key]
    public string MaSV { get; set; }
    [Key]
    public string MaMH { get; set; }
    [Key]
    public double DiemThi { get; set; }
    public int LanThi { get; set; }
}
[Table("GiangDay")]
public class GiangDay
{
    [Key]
    public string MaGV { get; set; }
    public string MaMH { get; set; }
    public string NamHoc { get; set; }
    public int HocKy { get; set; }
}

[Table("GiangVien")]
public class GiangVien
{
    [Key]
    public string MaGV { get; set; }
    public string TENGV { get; set; }
    public string MaKH { get; set; }
}

[Table("ThanNhan")]
public class ThanNhan
{
    [Key]
    public string MaTN { get; set; }
    public string HoTen { get; set; }
    public string GioiTinh { get; set; }
}

[Table("QUANHE")]
public class QUANHE
{
    [Key]
    public string MaSV { get; set; }
    public string MaTN { get; set; }
    public string QuanHe { get; set; }
}
