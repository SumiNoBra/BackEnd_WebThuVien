using BackEnd.EF_Contexts;

namespace BackEnd.DTOs
{
    public class DocGiaDTO
    {
        public int Madocgia { get; set; }

        public int? Manguoidung { get; set; }

        public string? Tinhtrangthe { get; set; }

        public DateOnly? Ngaycap { get; set; }

        public DateOnly? Ngayhethan { get; set; }

        public virtual List<PhieuMuonDTO>? Phieumuons { get; set; }

        public virtual List<YeuCauMuonDTO>? Yeucaumuons { get; set; } 

    }
}
