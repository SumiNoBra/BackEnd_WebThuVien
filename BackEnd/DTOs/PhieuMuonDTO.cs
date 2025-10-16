using BackEnd.EF_Contexts;

namespace BackEnd.DTOs
{
    public class PhieuMuonDTO
    {
        public int Maphieumuon { get; set; }

        public int? Madocgia { get; set; }

        public DateOnly? Ngaymuon { get; set; }

        public DateOnly? Hantra { get; set; }

        public DateOnly? Ngaytra { get; set; }

        public string? Trangthai { get; set; }

        public decimal? Sotienphat { get; set; }

        public  List<ChiTietPhieuMuonDTO>? Chitietphieumuons { get; set; }

    }
}
