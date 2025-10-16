using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Chitietyeucaumuon
{
    public int Mayeucau { get; set; }

    public int Masach { get; set; }

    public int? Soluongmuon { get; set; }

    public virtual Sach? MasachNavigation { get; set; } = null!;

    public virtual Yeucaumuon? MayeucauNavigation { get; set; } = null!;
}
