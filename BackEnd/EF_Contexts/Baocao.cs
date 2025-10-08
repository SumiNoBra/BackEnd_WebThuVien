using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Baocao
{
    public int Mabaocao { get; set; }

    public string? Loaibaocao { get; set; }

    public string? Noidung { get; set; }

    public DateTime? Ngaytao { get; set; }
}
