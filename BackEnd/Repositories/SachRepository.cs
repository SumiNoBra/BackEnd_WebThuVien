using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class SachRepository : ISachRepository
    {
        protected readonly QlThuvienContext _context;
        public SachRepository(QlThuvienContext context)
        {
            _context = context;
        }

        public async Task AddSachAsync(Sach sach)
        {
            if (sach == null)
                throw new ArgumentNullException(nameof(sach));
            if (string.IsNullOrWhiteSpace(sach.Tensach))
                throw new ArgumentException("Tên sách (Tensach) không được để trống.");

            if (sach.Soluong.HasValue && sach.Soluong < 0)
                throw new ArgumentException("Số lượng (Soluong) phải >= 0.");

            if (sach.Namxuatban.HasValue)
            {
                var thisYear = DateTime.Now.Year;
                if (sach.Namxuatban < 0 || sach.Namxuatban > thisYear)
                    throw new ArgumentException($"Năm xuất bản (Namxuatban) phải nằm trong khoảng 0..{thisYear}.");
            }

            if (sach.Matheloai.HasValue)
            {
                var theLoaiExists = await _context.Set<Theloai>()
                                                  .AnyAsync(t => t.Matheloai == sach.Matheloai.Value);
                if (!theLoaiExists)
                    throw new ArgumentException($"Thể loại (Matheloai = {sach.Matheloai}) không tồn tại.");
            }

            if (sach.Manxb.HasValue)
            {
                var nxbExists = await _context.Set<Nhaxuatban>()
                                              .AnyAsync(n => n.Manxb == sach.Manxb.Value);
                if (!nxbExists)
                    throw new ArgumentException($"Nhà xuất bản (Manxb = {sach.Manxb}) không tồn tại.");
            }
            sach.Tensach = sach.Tensach.Trim();
            sach.Tacgia = string.IsNullOrWhiteSpace(sach.Tacgia) ? null : sach.Tacgia.Trim();
            sach.Vitrisach = string.IsNullOrWhiteSpace(sach.Vitrisach) ? null : sach.Vitrisach.Trim();
            sach.Anhbia = string.IsNullOrWhiteSpace(sach.Anhbia) ? null : sach.Anhbia.Trim();
            sach.Maqr = string.IsNullOrWhiteSpace(sach.Maqr) ? null : sach.Maqr.Trim();
            sach.Trangthai = string.IsNullOrWhiteSpace(sach.Trangthai) ? null : sach.Trangthai.Trim();

            if (!sach.Soluong.HasValue)
            {
                sach.Soluong = 0;
            }
            try
            {
                await _context.Saches.AddAsync(sach);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Lỗi khi thêm sách vào database: " + dbEx.Message, dbEx);
            }
        }

        public  async Task<List<Sach>> GetAllSachesAsync()
        {
            return await _context.Saches.ToListAsync();
        }

        public async Task<Sach> GetSachByMasachAsync(int Masach)
        {
           return await _context.Saches.FirstOrDefaultAsync(s => s.Masach == Masach);
        }

        public Task<List<Sach>> GetSachesByNhaXuatBanAsync(int nxbId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sach>> GetSachesByTheLoaiAsync(int matheloai)
        {
            return await _context.Saches.Where(s => s.Matheloai == matheloai).ToListAsync();
        }

        public async Task<List<Sach>> SearchSachesAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {

                return new List<Sach>();
            }

            keyword = keyword.ToLower();

            return await _context.Saches
                .Where(s => s.Tensach.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task UpdateSachAsync(SachDTO sach)
        {
            if (sach == null)
                throw new ArgumentNullException(nameof(sach));

            if (sach.Masach == null)
                throw new ArgumentException("Masach phải được cung cấp.", nameof(sach.Masach));


            var existing = await _context.Saches
                                         .FirstOrDefaultAsync(s => s.Masach == sach.Masach);
            if (existing == null)
                throw new ArgumentNullException($"Không tìm thấy sách có Masach = {sach.Masach}.");
            if (sach.Tensach != null)
            {
                if (string.IsNullOrWhiteSpace(sach.Tensach))
                    throw new ArgumentException("Tên sách (Tensach) không được để trống nếu được gửi.");
                existing.Tensach = sach.Tensach.Trim();
            }
            if (sach.Tacgia != null)
            {
                existing.Tacgia = string.IsNullOrWhiteSpace(sach.Tacgia) ? null : sach.Tacgia.Trim();
            }
            if (sach.Matheloai.HasValue)
            {
                var theLoaiExists = await _context.Theloais
                                                 .AnyAsync(t => t.Matheloai == sach.Matheloai.Value);
                if (!theLoaiExists)
                    throw new ArgumentException($"Thể loại (Matheloai = {sach.Matheloai}) không tồn tại.");
                existing.Matheloai = sach.Matheloai;
            }
            if (sach.Manxb.HasValue)
            {
                var nxbExists = await _context.Nhaxuatbans
                                              .AnyAsync(n => n.Manxb == sach.Manxb.Value);
                if (!nxbExists)
                    throw new ArgumentException($"Nhà xuất bản (Manxb = {sach.Manxb}) không tồn tại.");
                existing.Manxb = sach.Manxb;
            }
            if (sach.Namxuatban.HasValue)
            {
                var thisYear = DateTime.Now.Year;
                if (sach.Namxuatban < 0 || sach.Namxuatban > thisYear)
                    throw new ArgumentException($"Năm xuất bản (Namxuatban) phải nằm trong khoảng 0..{thisYear}.");
                existing.Namxuatban = sach.Namxuatban;
            }
            if (sach.Soluong.HasValue)
            {
                if (sach.Soluong < 0)
                    throw new ArgumentException("Số lượng (Soluong) phải >= 0.");
                existing.Soluong = sach.Soluong;
            }
            if (sach.Vitrisach != null)
                existing.Vitrisach = string.IsNullOrWhiteSpace(sach.Vitrisach) ? null : sach.Vitrisach.Trim();

            if (sach.Anhbia != null)
                existing.Anhbia = string.IsNullOrWhiteSpace(sach.Anhbia) ? null : sach.Anhbia.Trim();

            if (sach.Trangthai != null)
                existing.Trangthai = string.IsNullOrWhiteSpace(sach.Trangthai) ? null : sach.Trangthai.Trim();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Lỗi khi cập nhật database: " + dbEx.Message, dbEx);
            }
        }

    }
}
