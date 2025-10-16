using System;
using System.Collections.Generic;
namespace BackEnd.EF_Contexts;

public partial class Yeucaumuon
{
    public int Mayeucau { get; set; }

    public int? Madocgia { get; set; }

    public DateTime? Ngayyeucau { get; set; }

    public string? Trangthai { get; set; }

    public virtual ICollection<Chitietyeucaumuon> Chitietyeucaumuons { get; set; } = new List<Chitietyeucaumuon>();

    public virtual Docgia? MadocgiaNavigation { get; set; }
}
