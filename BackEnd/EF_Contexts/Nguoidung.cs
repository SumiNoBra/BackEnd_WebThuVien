using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Nguoidung
{
    public int Manguoidung { get; set; }

    public string Hoten { get; set; } = null!;

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public string Matkhau { get; set; } = null!;

    public string Vaitro { get; set; } = null!;

    public string? Trangthai { get; set; }

    public DateTime? Ngaytao { get; set; }

    public virtual ICollection<ChatbotLog> ChatbotLogs { get; set; } = new List<ChatbotLog>();

    public virtual Docgium? Docgium { get; set; }

    public virtual ICollection<HoatdongLog> HoatdongLogs { get; set; } = new List<HoatdongLog>();

    public virtual ICollection<LoginLog> LoginLogs { get; set; } = new List<LoginLog>();

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual ICollection<Thongbao> Thongbaos { get; set; } = new List<Thongbao>();
}
