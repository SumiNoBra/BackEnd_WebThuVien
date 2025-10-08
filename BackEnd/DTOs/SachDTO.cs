using BackEnd.EF_Contexts;

namespace BackEnd.DTOs
{
    public class SachDTO
    {
        public int Masach { get; set; }

        public string? Tensach { get; set; } = null!;

        public string? Tacgia { get; set; }

        public int? Matheloai { get; set; }

        public int? Manxb { get; set; }

        public int? Namxuatban { get; set; }

        public int? Soluong { get; set; }

        public string? Vitrisach { get; set; }

        public string? Anhbia { get; set; }

        public string? Maqr { get; set; }

        public string? Trangthai { get; set; }

    }
}
