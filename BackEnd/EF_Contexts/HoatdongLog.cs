using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class HoatdongLog
{
    public int Mahdlog { get; set; }

    public int? Manguoidung { get; set; }

    public string? Hanhdong { get; set; }

    public string? Doituong { get; set; }

    public string? Noidung { get; set; }

    public DateTime? Thoigian { get; set; }

    public virtual Nguoidung? ManguoidungNavigation { get; set; }
}
