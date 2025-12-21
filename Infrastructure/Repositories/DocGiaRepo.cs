using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DocGiaRepo : IDocGiaRepo
    {
        QuanlythuvienContext _context;
        public DocGiaRepo(QuanlythuvienContext context)
        {
            _context = context;
        }

        public async Task CreateDocGia(DocGia docGia)
        {
            await _context.DocGia.AddAsync(docGia); 
            await _context.SaveChangesAsync();
        }

        public async Task<DocGia?> ExistDocGia(string email, string matkhau)
        {
            DocGia? a = await _context.DocGia.FirstOrDefaultAsync(e => e.Email == email && e.MatKhau == matkhau);
            return a;
        }

        public async Task<bool> ExistDocGia(int id)
        {
            return await _context.DocGia.AnyAsync(e => e.MaDocGia == id);
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _context.DocGia.AnyAsync(e => e.Email == email); 
        }

        public async Task<DocGia?> GetByEmailAsync(string email)
        {
            return await _context.DocGia.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<DocGia?> GetDocGia(int id)
        {
            return await _context.DocGia.FirstOrDefaultAsync(e => e.MaDocGia == id); 
        }

        public async Task<List<DocGia>> GetDocGias()
        {
            return await _context.DocGia.AsNoTracking().ToListAsync();
        }

        public async Task<DocGia?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.DocGia.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        }

        public async Task SaveRefreshTokenAsync(int maDocGia, string refreshToken, DateTime expiryDate)
        {
            var user = await _context.DocGia.FirstOrDefaultAsync(u => u.MaDocGia == maDocGia);

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = expiryDate;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateDocGia(DocGia docgia)
        {
            var docGia = await _context.DocGia.FirstOrDefaultAsync(e=>e.MaDocGia==docgia.MaDocGia);

            if (docGia == null) return false;

            if (!string.IsNullOrWhiteSpace(docgia.HoTen))
                docGia.HoTen = docgia.HoTen;

            if (docgia.NgaySinh.HasValue)
                docGia.NgaySinh = docgia.NgaySinh;

            if (!string.IsNullOrWhiteSpace(docgia.GioiTinh))
                docGia.GioiTinh = docgia.GioiTinh;

            if (!string.IsNullOrWhiteSpace(docgia.VaiTro))
                docGia.VaiTro = docgia.VaiTro;

            if (!string.IsNullOrWhiteSpace(docgia.SoDienThoai))
                docGia.SoDienThoai = docgia.SoDienThoai;

            if (!string.IsNullOrWhiteSpace(docgia.MatKhau))
                docGia.MatKhau = docgia.MatKhau;

            if (docgia.TrangThaiTk.HasValue)
                docGia.TrangThaiTk = docgia.TrangThaiTk;

            if (!string.IsNullOrWhiteSpace(docgia.DiaChi))
                docGia.DiaChi = docgia.DiaChi;

            if (!string.IsNullOrWhiteSpace(docgia.Email))
                docGia.Email = docgia.Email;

            if (!string.IsNullOrWhiteSpace(docgia.GhiChu))
                docGia.GhiChu = docgia.GhiChu;

            if (!string.IsNullOrWhiteSpace(docgia.AnhDaiDien))
                docGia.AnhDaiDien = docgia.AnhDaiDien;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
