using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Sach
{
    public int Masach { get; set; }

    public string Tensach { get; set; } = null!;

    public string? Tacgia { get; set; }

    public int? Matheloai { get; set; }

    public int? Manxb { get; set; }

    public int? Namxuatban { get; set; }

    public int? Soluong { get; set; }

    public string? Vitrisach { get; set; }

    public string? Anhbia { get; set; }

    public string? Maqr { get; set; }

    public string? Trangthai { get; set; }

    public virtual ICollection<Chitietphieumuon> Chitietphieumuons { get; set; } = new List<Chitietphieumuon>();

    public virtual ICollection<Chitietyeucaumuon> Chitietyeucaumuons { get; set; } = new List<Chitietyeucaumuon>();

    public virtual Nhaxuatban? ManxbNavigation { get; set; }

    public virtual Theloai? MatheloaiNavigation { get; set; }
}
