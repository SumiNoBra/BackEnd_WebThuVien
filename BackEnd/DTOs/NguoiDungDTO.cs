using BackEnd.EF_Contexts;

namespace BackEnd.DTOs
{
    public class NguoiDungDTO
    {
        public int Manguoidung { get; set; }

        public string? Hoten { get; set; } = null!;

        public string? Email { get; set; }

        public string? Sdt { get; set; }

        public string? Matkhau { get; set; } = null!;

        public string? Vaitro { get; set; } = null!;

        public string? Trangthai { get; set; }

        public DateTime? Ngaytao { get; set; }

    }
}
