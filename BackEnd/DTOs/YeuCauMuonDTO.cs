namespace BackEnd.DTOs
{
    public class YeuCauMuonDTO
    {
        public int Mayeucau { get; set; }
        public int? Madocgia { get; set; }
        public DateTime? Ngayyeucau { get; set; }
        public string? Trangthai { get; set; }

        public List<ChiTietYeuCauMuonDTO>? ChiTietYeuCauMuons { get; set; }
    }
}

