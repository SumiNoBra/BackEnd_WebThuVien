using Domain.Entities;

namespace API.DTOs
{
    public class CreatePhieuMuonDTO
    {
        public int MaSoThe { get; set; }
        public int MaNV { get; set; }
        public List<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new();
        public PhieuMuon phieumuon { get; set; } = new();
    }
    public class ChiTietPhieuMuonDTO
    {
        public int MaPhieuMuon { get; set; }

        public int MaTaiLieu { get; set; }

        public int? SoLuong { get; set; }

        public decimal? PhiMuonTaiThoiDiem { get; set; }
    }
    public class UpdateDocGia
    {
        public string matkhaucu { get; set; }
        public DocGia docgia { get; set; }

    }
    public class CreateDatMuonTruocDTO
    {
        public DatMuonTruoc DatMuonTruoc { get; set; } = new();
        public List<ChiTietDatTruoc> ChiTietDatTruocs { get; set; } = new();
    }
    public class CreateTaiLieuDTO
    {
        public TaiLieu tailieu { get; set; } = new();
        public List<int> tacgias { get; set; } = new();
        public List<int> theloais { get; set; } = new();
    }
    public class UpdateTaiLieuDTO
    {
        public TaiLieu tailieu { get; set; } = new();
        public List<int> tacgias { get; set; } = new();
        public List<int> theloais { get; set; } = new();
    }
}
