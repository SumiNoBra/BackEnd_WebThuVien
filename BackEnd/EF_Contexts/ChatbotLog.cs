using System;
using System.Collections.Generic;

namespace BackEnd.EF_Contexts;

public partial class ChatbotLog
{
    public int Malog { get; set; }

    public int? Manguoidung { get; set; }

    public string? Cauhoi { get; set; }

    public string? Traloi { get; set; }

    public DateTime? Ngayhoi { get; set; }

    public string? Nguon { get; set; }

    public virtual Nguoidung? ManguoidungNavigation { get; set; }
}
