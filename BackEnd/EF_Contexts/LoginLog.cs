using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class LoginLog
{
    public int Mabanglhi { get; set; }

    public int? Manguoidung { get; set; }

    public DateTime? Thoigian { get; set; }

    public string? DiaChiIp { get; set; }

    public string? Thietbi { get; set; }

    public string? Trangthai { get; set; }

    public virtual Nguoidung? ManguoidungNavigation { get; set; }
}
