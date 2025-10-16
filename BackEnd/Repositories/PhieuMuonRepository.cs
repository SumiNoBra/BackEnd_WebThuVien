using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class PhieuMuonRepository : IPhieuMuonRepository
    {
        private readonly QlThuvienContext _context;
        public PhieuMuonRepository(QlThuvienContext context)
        {
            _context = context;
        }

        public async Task<Phieumuon> Create(Phieumuon phieumuon)
        {
            await _context.Phieumuons.AddAsync(phieumuon);
            return phieumuon;
        }

        public async Task<List<PhieuMuonDTO>> GetAll()
        {
            return await _context.Phieumuons
                .Include(y => y.Chitietphieumuons)
                .AsNoTracking()
                .Select(y => new PhieuMuonDTO
                {
                    Maphieumuon = y.Maphieumuon,
                    Madocgia = y.Madocgia,
                    Ngaymuon = y.Ngaymuon,
                    Hantra = y.Hantra,
                    Ngaytra = y.Ngaytra,
                    Trangthai = y.Trangthai,
                    Sotienphat = y.Sotienphat,
                    Chitietphieumuons = y.Chitietphieumuons.Select(e => new ChiTietPhieuMuonDTO
                    {
                        Masach = e.Masach,
                        Soluongmuon = e.Soluongmuon
                    }).ToList()
                }).ToListAsync();
        }
        public async Task<bool> ExsitsDocGiaID(int madocgia)
        {
            return await _context.Docgias.Where(y => y.Madocgia == madocgia).AnyAsync();
        }

        public async Task<bool> UpdateTrangThai(int maphieumuon, string trangthai)
        {
            Phieumuon? phieumuon = await _context.Phieumuons.FirstOrDefaultAsync(y => y.Maphieumuon == maphieumuon);
            if (phieumuon == null)
            {
                return false;
            }
            phieumuon.Trangthai = trangthai;
            return true;
        }

        public async Task<bool> ExistID(int maphieumuon)
        {
            return await _context.Phieumuons.Where(y => y.Maphieumuon == maphieumuon).AnyAsync();
        }

        public async Task<List<PhieuMuonDTO>> GetByTrangThai(string trangthai)
        {
            return await _context.Phieumuons
                .Include(y => y.Chitietphieumuons)
                .AsNoTracking()
                .Select(y => new PhieuMuonDTO
                {
                    Maphieumuon = y.Maphieumuon,
                    Madocgia = y.Madocgia,
                    Ngaymuon = y.Ngaymuon,
                    Hantra = y.Hantra,
                    Ngaytra = y.Ngaytra,
                    Trangthai = y.Trangthai,
                    Sotienphat = y.Sotienphat,
                    Chitietphieumuons = y.Chitietphieumuons.Select(e => new ChiTietPhieuMuonDTO
                    {
                        Masach = e.Masach,
                        Soluongmuon = e.Soluongmuon
                    }).ToList()
                })
                .Where(y => y.Trangthai == trangthai)
                .ToListAsync();
        }
        public async Task<List<PhieuMuonDTO>> KiemTraVaTaoPhatQuaHan()
        {
            List<PhieuMuonDTO> cacPhieuDangMuon = await _context.Phieumuons
                .Include(y => y.Chitietphieumuons)
                .Where(e => e.Trangthai == "Đang mượn")
                .Select(y => new PhieuMuonDTO
                {
                    Maphieumuon = y.Maphieumuon,
                    Madocgia = y.Madocgia,
                    Ngaymuon = y.Ngaymuon,
                    Hantra = y.Hantra,
                    Ngaytra = y.Ngaytra,
                    Trangthai = y.Trangthai,
                    Sotienphat = y.Sotienphat,
                    Chitietphieumuons = y.Chitietphieumuons.Select(e => new ChiTietPhieuMuonDTO
                    {
                        Masach = e.Masach,
                        Soluongmuon = e.Soluongmuon
                    }).ToList()
                }).ToListAsync();

            List<PhieuMuonDTO> phieubiphat = new List<PhieuMuonDTO>();
            DateOnly dateOnlyNow = DateOnly.FromDateTime(DateTime.Now);

            foreach (PhieuMuonDTO pm in cacPhieuDangMuon)
            {
                if (pm.Hantra != null &&
                    pm.Hantra < dateOnlyNow)
                {
                    pm.Trangthai = "Qua thoi han";
                    phieubiphat.Add(pm);
                }
            }

            foreach (PhieuMuonDTO pm in phieubiphat)
            {
                if (!await ExistPhatIDMaphieumuon(pm.Maphieumuon))
                {

                    int sluong = pm.Chitietphieumuons?.Sum(e => e.Soluongmuon ?? 0) ?? 0;
                    Phat phat = new Phat
                    {
                        Maphieumuon = pm.Maphieumuon,
                        Sotien = sluong * 10000,
                        Dathanhtoan = false,
                        Ngayphat = dateOnlyNow,
                    };
                    _context.Phats.Add(phat);
                }
            }
            return cacPhieuDangMuon;
        }
        public async Task<bool> ExistPhatIDMaphieumuon(int idphieumuon)
        {
            return await _context.Phats.AsNoTracking().AnyAsync(e => e.Maphieumuon == idphieumuon);
        }
        public async Task<bool> UpdateDaTraSach(int id)
        {
            Phieumuon? PhieuDangMuon = await _context.Phieumuons
                                    .Where(e => e.Maphieumuon == id)
                                    .FirstOrDefaultAsync();

            if( PhieuDangMuon == null)
            {
                throw new ArgumentNullException();
            }
            PhieuDangMuon.Trangthai = "da tra";
            DateOnly dateOnlyNow = DateOnly.FromDateTime(DateTime.Now);
            PhieuDangMuon.Ngaytra = dateOnlyNow;
            Phat? phat = await _context.Phats.FirstOrDefaultAsync(e => e.Maphieumuon == id);
            if( phat != null)
            {
                phat.Dathanhtoan = true;
            }
            return true;
        }
    }
}
