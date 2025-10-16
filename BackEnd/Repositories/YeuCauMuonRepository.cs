using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class YeuCauMuonRepository : IYeuCauMuonRepository
    {
        private readonly QlThuvienContext _context;
        public YeuCauMuonRepository(QlThuvienContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsDocGiaId(int madocgia)
        {
            return await _context.Docgias.Where(y => y.Madocgia == madocgia).AnyAsync();
        }
        public async Task<bool> Delete(int mayeucau)
        {
            var yeucaumuon = await _context.Yeucaumuons.FirstOrDefaultAsync(y => y.Mayeucau == mayeucau);
            if (yeucaumuon == null)
            {
                return false;
            }
            _context.Yeucaumuons.Remove(yeucaumuon);
            return true;
        }
        public async Task<bool> ExistID(int mayeucau)
        {
            return await _context.Yeucaumuons.Where(y => y.Mayeucau == mayeucau).AnyAsync();
        }
        public async Task<List<YeuCauMuonDTO>> GetAll()
        {
            return await _context.Yeucaumuons
                .Include(y => y.Chitietyeucaumuons)
                .AsNoTracking()
                .Select(y => new YeuCauMuonDTO
                {
                    Mayeucau = y.Mayeucau,
                    Madocgia = y.Madocgia,
                    Ngayyeucau = y.Ngayyeucau,
                    Trangthai = y.Trangthai,
                    ChiTietYeuCauMuons = y.Chitietyeucaumuons.Select(ct => new ChiTietYeuCauMuonDTO
                    {
                        Masach = ct.Masach,
                        Soluongmuon = ct.Soluongmuon
                    }).ToList()
                })
                .ToListAsync();
        }
        public async Task<YeuCauMuonDTO?> GetById(int mayeucau)
        {
            return await _context.Yeucaumuons.Include(y => y.Chitietyeucaumuons)
                .AsNoTracking()
                .Select(y => new YeuCauMuonDTO
                {
                    Mayeucau = y.Mayeucau,
                    Madocgia = y.Madocgia,
                    Ngayyeucau = y.Ngayyeucau,
                    Trangthai = y.Trangthai,
                    ChiTietYeuCauMuons = y.Chitietyeucaumuons.Select(ct => new ChiTietYeuCauMuonDTO
                    {
                        Masach = ct.Masach,
                        Soluongmuon = ct.Soluongmuon
                    }).ToList()
                })
                .FirstOrDefaultAsync(y => y.Mayeucau == mayeucau);
        }

        public async Task<bool> UpdateTrangThai(int mayeucau, string trangthai)
        {
            Yeucaumuon? existingYeuCauMuon = await _context.Yeucaumuons.FirstOrDefaultAsync(y => y.Mayeucau == mayeucau);
            if (existingYeuCauMuon == null)
            {
                return false;
            }
            existingYeuCauMuon.Trangthai = trangthai;
            return true;
        }

        public async Task<List<YeuCauMuonDTO>> GetByTrangThai(string trangthai)
        {
            return await _context.Yeucaumuons
                .Include(y => y.Chitietyeucaumuons)
                .AsNoTracking()
                .Select(y => new YeuCauMuonDTO
                {
                    Mayeucau = y.Mayeucau,
                    Madocgia = y.Madocgia,
                    Ngayyeucau = y.Ngayyeucau,
                    Trangthai = y.Trangthai,
                    ChiTietYeuCauMuons = y.Chitietyeucaumuons.Select(ct => new ChiTietYeuCauMuonDTO
                    {
                        Masach = ct.Masach,
                        Soluongmuon = ct.Soluongmuon
                    }).ToList()
                })
                .Where(y => y.Trangthai == trangthai)
                .ToListAsync();
        }
        public async Task<Yeucaumuon> Create(Yeucaumuon yeucaumuon)
        {
            await _context.Yeucaumuons.AddAsync(yeucaumuon);
            return yeucaumuon;
        }

    }
}
