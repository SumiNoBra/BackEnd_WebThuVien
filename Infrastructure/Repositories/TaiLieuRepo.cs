using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
namespace Infrastructure.Repositories
{
    public class TaiLieuRepo : ITaiLieuRepo
    {
        private readonly QuanlythuvienContext _context;
        public TaiLieuRepo(QuanlythuvienContext context) { 
            _context = context;
        }

        public async Task Create(TaiLieu tailieu, List<int> tacgia, List<int> theloai)
        {
            await _context.TaiLieus.AddAsync(tailieu);
            await _context.SaveChangesAsync();

            List<TacGia> tacgias = await _context.TacGia
                .Where(t => tacgia.Contains(t.MaTacGia))
                .ToListAsync();
            if (tacgias.Count != tacgia.Count)
            {
                throw new ArgumentNullException();
            }

            foreach (var tg in tacgias)
            {
                tailieu.MaTacGia.Add(tg);
            }
            List<Theloai> theloais = await _context.Theloais
                .Where(t => theloai.Contains(t.MaTheLoai))
                .ToListAsync();
            if (theloais.Count != theloai.Count)
            {
                throw new ArgumentNullException();
            }

            foreach (var tl in theloais)
            {
                tailieu.MaTheLoais.Add(tl);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Delete(int id)
        {
            var tailieu = await _context.TaiLieus.FirstOrDefaultAsync(e=>e.MaTaiLieu==id);
            if (tailieu == null)
            {
                return false;
            }
            tailieu.TrangThai = "Không lưu thông";
            _context.TaiLieus.Update(tailieu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistID(int id)
        {
            return await _context.TaiLieus.AnyAsync(e => e.MaTaiLieu == id);
        }

        public async Task<bool> ExistNXB(int id)
        {
            return await _context.NhaXuatBans.AnyAsync(e=>e.MaNxb == id);
        }

        public async Task<bool> ExistTacGia(int id)
        {
            return await _context.TacGia.AnyAsync(e=>e.MaTacGia == id);
        }

        public async Task<bool> ExistTheLoai(int id)
        {
            return await _context.Theloais.AnyAsync(e=>e.MaTheLoai == id);
        }

        public async Task<List<TaiLieu>> GetAdvanced(TaiLieu tailieu)
        {
            IQueryable<TaiLieu> query = _context.TaiLieus.AsQueryable();

            if (tailieu.MaTaiLieu != 0)
                query = query.Where(x => x.MaTaiLieu == tailieu.MaTaiLieu);

            if (!string.IsNullOrEmpty(tailieu.TenSach))
                query = query.Where(x => x.TenSach.Contains(tailieu.TenSach));

            if (!string.IsNullOrEmpty(tailieu.NgonNgu))
                query = query.Where(x => x.NgonNgu == tailieu.NgonNgu);

            if (tailieu.GiaBan != null)
                query = query.Where(x => x.GiaBan == tailieu.GiaBan);

            if (tailieu.PhiMuon != null)
                query = query.Where(x => x.PhiMuon == tailieu.PhiMuon);

            if (tailieu.NamXuatBan != null)
                query = query.Where(x => x.NamXuatBan == tailieu.NamXuatBan);

            if (tailieu.MaNxb != null)
                query = query.Where(x => x.MaNxb == tailieu.MaNxb);

            if (tailieu.SoLuong != null)
                query = query.Where(x => x.SoLuong == tailieu.SoLuong);

            if (tailieu.SoLuongCon != null)
                query = query.Where(x => x.SoLuongCon == tailieu.SoLuongCon);

            if (!string.IsNullOrEmpty(tailieu.AnhBia))
                query = query.Where(x => x.AnhBia == tailieu.AnhBia);

             return await query.ToListAsync();
        }

        public async Task<TaiLieu?> GetByID(int id)
        {
            return await _context.TaiLieus.FirstOrDefaultAsync(e => e.MaTaiLieu == id);
        }

        public async Task<List<TaiLieu>> GetByName(string name)
        {
            return await _context.TaiLieus.AsNoTracking().Where(e=>e.TenSach.Contains(name)).ToListAsync();
        }

        public async Task<List<TaiLieu>> GetByNXB(int id)
        {
            var nxb = await _context.NhaXuatBans
                .AsNoTracking()
                .Include(e => e.TaiLieus)
                .Select(e=>new NhaXuatBan
                {
                    MaNxb = e.MaNxb,
                    TenNxb = e.TenNxb,
                    DiaChi = e.DiaChi,
                    SoDienThoai = e.SoDienThoai,
                    TaiLieus =e.TaiLieus.Select(y=>new TaiLieu
                    {
                        MaTaiLieu = y.MaTaiLieu,
                        TenSach = y.TenSach,
                        TomTat = y.TomTat,
                        NgonNgu = y.NgonNgu,
                        GiaBan = y.GiaBan,
                        PhiMuon = y.PhiMuon,
                        NamXuatBan = y.NamXuatBan,
                        MaNxb = y.MaNxb,
                        SoLuong = y.SoLuong,
                    }).ToList()
                })
                .Where(e => e.MaNxb == id)
                .ToListAsync();
            return nxb.SelectMany(e => e.TaiLieus).ToList();
        }

        public async Task<List<TaiLieu>> GetByTacGia(int id)
        {
            var tacgias = await _context.TacGia.AsNoTracking()
                .Include(e => e.MaTaiLieus)
                .Select(e=>new TacGia
                {
                    TenTacGia=e.TenTacGia,
                    MaTacGia = e.MaTacGia,
                    MaTaiLieus = e.MaTaiLieus.Select(y => new TaiLieu
                    {
                        MaTaiLieu = y.MaTaiLieu,
                        TenSach = y.TenSach,
                        TomTat = y.TomTat,
                        NgonNgu = y.NgonNgu,
                        GiaBan = y.GiaBan,
                        PhiMuon = y.PhiMuon,
                        NamXuatBan = y.NamXuatBan,
                        MaNxb = y.MaNxb,
                        SoLuong = y.SoLuong,
                    }).ToList()
                })
                .Where(e => e.MaTacGia == id)
                .ToListAsync();
            return tacgias.SelectMany(e => e.MaTaiLieus).ToList();
        }

        public async Task<List<TaiLieu>> GetByTheLoai(int id)
        {
            var tailieus = await _context.Theloais.AsNoTracking()
                .Include(e => e.MaTaiLieus)
                .Select(e=>new Theloai
                {
                    TenTheLoai=e.TenTheLoai,
                    MaTheLoai=e.MaTheLoai,
                    MaTaiLieus=e.MaTaiLieus.Select(y=>new TaiLieu
                    {
                        MaTaiLieu=y.MaTaiLieu,
                        TenSach=y.TenSach,
                        TomTat=y.TomTat,
                        NgonNgu=y.NgonNgu,
                        GiaBan=y.GiaBan,
                        PhiMuon=y.PhiMuon,
                        NamXuatBan=y.NamXuatBan,
                        MaNxb=y.MaNxb,
                        SoLuong=y.SoLuong,
                    }).ToList()
                })
                .Where(e => e.MaTheLoai == id)
                .ToListAsync();
            return tailieus.SelectMany(e => e.MaTaiLieus).ToList();
        }

        public async Task<List<TaiLieu>> GetTaiLieus()
        {
            return await _context.TaiLieus.AsNoTracking()
                .Include(e => e.MaTacGia)
                .Include(e => e.MaTheLoais)
                .Include(e => e.MaNxbNavigation)
                .Include(e => e.DanhGiaBinhLuans)
                .ToListAsync();
        }

        public async Task<bool> Update(TaiLieu tailieu, List<int> tacgia, List<int> theloai)
        {
            var ishas = await _context.TaiLieus.Include(e => e.MaTacGia).Include(e => e.MaTheLoais).FirstOrDefaultAsync(e => e.MaTaiLieu == tailieu.MaTaiLieu);
            if (ishas == null)
            {
                return false;
            }
            ishas.MaTacGia.Clear();
            ishas.MaTheLoais.Clear();
            List<TacGia> tacgias = await _context.TacGia
                .Where(t => tacgia.Contains(t.MaTacGia))
                .ToListAsync();
            if (tacgias.Count != tacgia.Count)
            {
                throw new ArgumentNullException();
            }

            foreach (var tg in tacgias)
            {
                ishas.MaTacGia.Add(tg);
            }
            List<Theloai> theloais = await _context.Theloais
                .Where(t => theloai.Contains(t.MaTheLoai))
                .ToListAsync();
            if (theloais.Count != theloai.Count)
            {
                throw new ArgumentNullException();
            }

            foreach (var tl in theloais)
            {
                ishas.MaTheLoais.Add(tl);
            }
            if(tailieu.TenSach != null)
            {
                ishas.TenSach = tailieu.TenSach;
            }
            if(tailieu.TomTat != null)
            {
                ishas.TomTat = tailieu.TomTat;
            }
            if(tailieu.NgonNgu != null)
            {
                ishas.NgonNgu = tailieu.NgonNgu;
            }
            if(tailieu.GiaBan >=0 && tailieu.GiaBan !=null)
            {
                ishas.GiaBan = tailieu.GiaBan;
            }
            if(tailieu.PhiMuon >=0 && tailieu.PhiMuon !=null)
            {
                ishas.PhiMuon = tailieu.PhiMuon;
            }
            if(tailieu.NamXuatBan != null && tailieu.NamXuatBan <= DateTime.Now.Year)
            {
                ishas.NamXuatBan = tailieu.NamXuatBan;
            }
            if (tailieu.SoLuong>=0 && tailieu.SoLuong != null)
            {
                ishas.SoLuong = tailieu.SoLuong;
            }
            if(tailieu.SoLuongCon >= 0 && tailieu.SoLuongCon != null)
            {
                ishas.SoLuongCon = tailieu.SoLuongCon;
            }
            if(tailieu.AnhBia != null)
            {
                ishas.AnhBia = tailieu.AnhBia;
            }
            if(!string.IsNullOrEmpty(tailieu.TrangThai))
            {
                ishas.TrangThai = tailieu.TrangThai;
            }
            _context.TaiLieus.Update(ishas);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
