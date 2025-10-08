using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class Thongbao
{
    public int Mathongbao { get; set; }

    public int? Manguoidung { get; set; }

    public string? Loai { get; set; }

    public string? Tieude { get; set; }

    public string? Noidung { get; set; }

    public DateTime? Ngaygui { get; set; }

    public string? Trangthai { get; set; }

    public virtual Nguoidung? ManguoidungNavigation { get; set; }
}
