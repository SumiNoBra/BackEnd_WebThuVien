using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Nhanvien
{
    public int Manhanvien { get; set; }

    public int? Manguoidung { get; set; }

    public string? Chucvu { get; set; }

    public DateOnly? Ngayvaolam { get; set; }

    public virtual Nguoidung? ManguoidungNavigation { get; set; }
}
