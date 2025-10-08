using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Phat
{
    public int Maphat { get; set; }

    public int? Maphieumuon { get; set; }

    public decimal? Sotien { get; set; }

    public string? Lydo { get; set; }

    public bool? Dathanhtoan { get; set; }

    public DateOnly? Ngayphat { get; set; }

    public virtual Phieumuon? MaphieumuonNavigation { get; set; }
}
