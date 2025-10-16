using AutoMapper;
using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
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
        public async Task AddAsync(Sach sach)
        {
            if (sach.Matheloai.HasValue || sach.Manxb.HasValue)
            {
                var validationQuery = new
                {
                    TheLoaiExists = sach.Matheloai.HasValue
                        ? await ExistTheloai(sach.Matheloai.Value)
                        : (bool?)null,
                    NxbExists = sach.Manxb.HasValue
                        ? await ExistNXB(sach.Manxb.Value)
                        : (bool?)null
                };

                if (validationQuery.TheLoaiExists == false)
                    throw new ArgumentException($"Thể loại (Matheloai = {sach.Matheloai}) không tồn tại.");

                if (validationQuery.NxbExists == false)
                    throw new ArgumentException($"Nhà xuất bản (Manxb = {sach.Manxb}) không tồn tại.");
            }

            if (sach.Soluong.HasValue && sach.Soluong < 0)
            {

                throw new ArgumentException("Số lượng (Soluong) phải >= 0.");
            }
            if (sach.Namxuatban.HasValue)
            {
                int thisYear = DateTime.Now.Year;
                if (sach.Namxuatban < 1000 || sach.Namxuatban > thisYear)
                    throw new ArgumentException($"Năm xuất bản (Namxuatban) phải nằm trong khoảng 0..{thisYear}.");
            }
            try
            {
                await _context.Saches.AddAsync(sach);
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Lỗi khi lưu sách vào CSDL.", dbEx);
            }
        }

        public async Task<bool> ExistMaSach(int masach)
        {
            return await _context.Saches.AsNoTracking().Where(s => s.Masach == masach).AnyAsync();
        }

        public async Task<bool> ExistNXB(int manxb)
        {
            return await _context.Nhaxuatbans.AsNoTracking().AnyAsync(n => n.Manxb == manxb);
        }

        public async Task<bool> ExistTheloai(int matheloai)
        {
            return await _context.Theloais.AsNoTracking().AnyAsync(t => t.Matheloai == matheloai);
        }

        public async Task<List<Sach>> GetAllAsync()
        {
            return await _context.Saches.AsNoTracking().ToListAsync();
        }

        public async Task<Sach> GetByIDAsync(int Masach)
        {
            return await _context.Saches.FirstOrDefaultAsync(s => s.Masach == Masach);
        }

        public Task<List<Sach>> GetByNXBIDAsync(int nxbId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sach>> GetByTheLoaiIDAsync(int matheloai)
        {
            return await _context.Saches.AsNoTracking().Where(s => s.Matheloai == matheloai).ToListAsync();
        }

        public async Task<List<Sach>> GetByNameAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {

                return new List<Sach>();
            }

            keyword = keyword.ToLower();

            return await _context.Saches.AsNoTracking()
                .Where(s => s.Tensach.ToLower().Contains(keyword))
                .ToListAsync();
        }
        public Task<List<Sach>> GetByNamAsync(int nam)
        {
            return _context.Saches.AsNoTracking().Where(s => s.Namxuatban == nam).ToListAsync();
        }
        public async Task UpdateAsync(Sach sach)
        {
            if (sach.Matheloai.HasValue)
            {
                var theLoaiExists = await ExistTheloai(sach.Matheloai.Value);
                if (!theLoaiExists)
                    throw new ArgumentException($"Thể loại (Matheloai = {sach.Matheloai}) không tồn tại.");
            }

            if (sach.Manxb.HasValue)
            {
                var nxbExists = await ExistNXB(sach.Manxb.Value);
                if (!nxbExists)
                    throw new ArgumentException($"Nhà xuất bản (Manxb = {sach.Manxb}) không tồn tại.");
            }

            if (sach.Soluong.HasValue && sach.Soluong < 0)
            {
                throw new ArgumentException("Số lượng (Soluong) phải >= 0.");
            }
            if (sach.Namxuatban.HasValue)
            {
                var thisYear = DateTime.Now.Year;
                if (sach.Namxuatban < 1000 || sach.Namxuatban > thisYear)
                    throw new ArgumentException($"Năm xuất bản (Namxuatban) phải nằm trong khoảng 0..{thisYear}.");
            }
            var existingSach = await _context.Saches.Where(s => s.Masach == sach.Masach).FirstOrDefaultAsync();
            if (existingSach == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sách có Masach = {sach.Masach}.");
            }
            if (!string.IsNullOrEmpty(sach.Tensach))
            {
                existingSach.Tensach = sach.Tensach;
            }
            if (!string.IsNullOrEmpty(sach.Tacgia))
            {
                existingSach.Tacgia = sach.Tacgia;
            }
            if (sach.Matheloai.HasValue)
            {
                existingSach.Matheloai = sach.Matheloai.Value;
            }
            if (sach.Manxb.HasValue)
            {
                existingSach.Manxb = sach.Manxb.Value;
            }
            if (sach.Soluong.HasValue)
            {
                existingSach.Soluong = sach.Soluong.Value;
            }
            if (sach.Namxuatban.HasValue)
            {
                existingSach.Namxuatban = sach.Namxuatban.Value;
            }
            try
            {

            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Lỗi khi cập nhật database.", dbEx);
            }
        }

        public async Task<List<Sach>> GetByTrangThai(string trangthai)
        {
            return await _context.Saches.AsNoTracking().Where(s => s.Trangthai == trangthai).ToListAsync();
        }
    }
}
