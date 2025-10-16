using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class DocGiaRepository : IDocGiaRepository
    {
        private readonly QlThuvienContext _context;
        public DocGiaRepository(QlThuvienContext context)
        {
            _context = context;
        }
        public async Task Create(Docgia docgia)
        {
            await _context.Docgias.AddAsync(docgia);
        }
        public async Task<bool> ExistNguoiDungID(int id)
        {
            return await _context.Docgias.AnyAsync(e=>e.Manguoidung == id);
        }
        public async Task<List<DocGiaDTO>> GetAll()
        {
            return await _context.Docgias
            .Include(y => y.Phieumuons)
            .Include(y => y.Yeucaumuons)
            .AsNoTracking()
            .Select (y => new DocGiaDTO
            {
                Madocgia = y.Madocgia,
                Manguoidung = y.Manguoidung,
                Tinhtrangthe = y.Tinhtrangthe,
                Ngaycap = y.Ngaycap,
                Ngayhethan = y.Ngayhethan,
                Phieumuons = y.Phieumuons.Select(e=>new PhieuMuonDTO
                {
                    Maphieumuon = e.Maphieumuon,
                    Madocgia = e.Madocgia,
                    Ngaymuon = e.Ngaymuon,
                    Hantra = e.Hantra,
                    Ngaytra = e.Ngaytra,
                    Trangthai = e.Trangthai,
                    Sotienphat = e.Sotienphat
                }).ToList(),
                Yeucaumuons = y.Yeucaumuons.Select(e=>new YeuCauMuonDTO
                {
                    Mayeucau = e.Mayeucau,
                    Madocgia = e.Madocgia,
                    Ngayyeucau = e.Ngayyeucau,
                    Trangthai = e.Trangthai
                }).ToList()
            })
            .ToListAsync();
        }
        public async Task<bool> Update(Docgia docgia)
        {
            Docgia? existdocgia = await _context.Docgias.FirstOrDefaultAsync(e => e.Madocgia == docgia.Madocgia);
            if (existdocgia == null)
            {
                throw new ArgumentNullException();
            }
            if (docgia.Tinhtrangthe != null ) { 
                existdocgia.Tinhtrangthe=docgia.Tinhtrangthe;
            }
            if (docgia.Ngayhethan != null)
            {
                existdocgia.Ngayhethan = docgia.Ngayhethan;
            }
            if (docgia.Ngaycap != null)
            {
                existdocgia.Ngaycap = docgia.Ngaycap;
            }
            return true;
        }
    }
}
