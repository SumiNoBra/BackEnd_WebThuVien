using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Domain.Entities;

public partial class TaiLieu
{
    public int MaTaiLieu { get; set; }

    public string TenSach { get; set; } = null!;

    public string? TomTat { get; set; }

    public string? NgonNgu { get; set; }

    public decimal? GiaBan { get; set; }

    public decimal? PhiMuon { get; set; }

    public int? NamXuatBan { get; set; }

    public int? MaNxb { get; set; }

    public int? SoLuong { get; set; }

    public int? SoLuongCon { get; set; }

    public string? AnhBia { get; set; }
    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietDatTruoc> ChiTietDatTruocs { get; set; } = new List<ChiTietDatTruoc>();

    public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new List<ChiTietPhieuMuon>();
    public virtual ICollection<DanhGiaBinhLuan> DanhGiaBinhLuans { get; set; } = new List<DanhGiaBinhLuan>();
    public virtual NhaXuatBan? MaNxbNavigation { get; set; }
    public virtual ICollection<TacGia> MaTacGia { get; set; } = new List<TacGia>();
    public virtual ICollection<Theloai> MaTheLoais { get; set; } = new List<Theloai>();
}
