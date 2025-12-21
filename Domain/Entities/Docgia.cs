using System;
using System.Collections.Generic;


namespace Domain.Entities;

public partial class DocGia
{
    public int MaDocGia { get; set; }

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? VaiTro { get; set; }

    public string? SoDienThoai { get; set; }

    public string MatKhau { get; set; } = null!;

    public byte? TrangThaiTk { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? GhiChu { get; set; }

    public string? AnhDaiDien { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual ICollection<DanhGiaBinhLuan> DanhGiaBinhLuans { get; set; } = new List<DanhGiaBinhLuan>();

    public virtual ICollection<DatMuonTruoc> DatMuonTruocs { get; set; } = new List<DatMuonTruoc>();

    public virtual ICollection<GiaoDichThanhToan> GiaoDichThanhToans { get; set; } = new List<GiaoDichThanhToan>();

    public virtual NhanVien? NhanVien { get; set; }

    public virtual TheBanDoc? TheBanDoc { get; set; }
}
