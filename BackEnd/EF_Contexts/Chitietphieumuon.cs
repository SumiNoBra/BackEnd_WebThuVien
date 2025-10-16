using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Chitietphieumuon
{
    public int Maphieumuon { get; set; }

    public int Masach { get; set; }

    public int? Soluongmuon { get; set; }

    public virtual Phieumuon? MaphieumuonNavigation { get; set; } = null!;

    public virtual Sach? MasachNavigation { get; set; } = null!;
}
