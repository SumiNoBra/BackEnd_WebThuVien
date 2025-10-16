using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Phieumuon
{
    public int Maphieumuon { get; set; }

    public int? Madocgia { get; set; }

    public DateOnly? Ngaymuon { get; set; }

    public DateOnly? Hantra { get; set; }

    public DateOnly? Ngaytra { get; set; }

    public string? Trangthai { get; set; }

    public decimal? Sotienphat { get; set; }

    public virtual ICollection<Chitietphieumuon>? Chitietphieumuons { get; set; } = new List<Chitietphieumuon>();

    public virtual Docgia? MadocgiaNavigation { get; set; }

    public virtual ICollection<Phat> Phats { get; set; } = new List<Phat>();
}
