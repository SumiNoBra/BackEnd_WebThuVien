using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Nhaxuatban
{
    public int Manxb { get; set; }

    public string Tennxb { get; set; } = null!;

    public string? Diachi { get; set; }

    public string? Sdt { get; set; }

    public string? Website { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
