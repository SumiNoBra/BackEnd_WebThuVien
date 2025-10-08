using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Yeucaumuon
{
    public int Mayeucau { get; set; }

    public int? Madocgia { get; set; }

    public int? Masach { get; set; }

    public DateTime? Ngayyeucau { get; set; }

    public string? Trangthai { get; set; }

    public virtual Docgium? MadocgiaNavigation { get; set; }

    public virtual Sach? MasachNavigation { get; set; }
}
