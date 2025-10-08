using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Docgium
{
    public int Madocgia { get; set; }

    public int? Manguoidung { get; set; }

    public string? Tinhtrangthe { get; set; }

    public DateOnly? Ngaycap { get; set; }

    public DateOnly? Ngayhethan { get; set; }

    public virtual Nguoidung? ManguoidungNavigation { get; set; }

    public virtual ICollection<Phieumuon> Phieumuons { get; set; } = new List<Phieumuon>();

    public virtual ICollection<Yeucaumuon> Yeucaumuons { get; set; } = new List<Yeucaumuon>();
}
