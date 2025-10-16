using AutoMapper;
using BackEnd.DTo;
using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Repositories
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        public QlThuvienContext _context;

        public NguoiDungRepository(QlThuvienContext context)
        {
            _context = context;
        }
        public async Task<int> ExistNguoiDungAsync(string email, string matkhau, string vaitro)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matkhau))
            {
                throw new ArgumentNullException("Email hoặc mật khẩu không được để trống");
            }
            Nguoidung? isHas = await _context.Nguoidungs.AsNoTracking().Where(x => x.Email == email && x.Matkhau == matkhau).FirstOrDefaultAsync();
            if (isHas == null)
            {
                return -1;
            }
            vaitro = isHas.Vaitro;
            return 1;
        }
        public async Task AddAsync(Nguoidung user)
        {
            if (user.Hoten == null || user.Sdt == null)
            {
                throw new ArgumentNullException("Khong duoc de trong");
            }
            user.Vaitro = "User";
            user.Ngaytao = DateTime.Now;
            await _context.Nguoidungs.AddAsync(user);
            return;

        }

        public async Task<bool> ExistIDAsync(int ID)
        {
            return await _context.Nguoidungs.AsNoTracking()
                                 .AnyAsync(x => x.Manguoidung == ID);
        }
        public async Task<bool> ExistEmail(string email)
        {
            return await _context.Nguoidungs.AsNoTracking().AnyAsync(x => x.Email == email);
        }

        public async Task<NguoiDungDTO?> GetByIdAsync(int ID)
        {
            return await _context.Nguoidungs
                .AsNoTracking()
                .Where(x => x.Manguoidung == ID)
                .Select(y => new NguoiDungDTO
                {
                    Manguoidung = y.Manguoidung,
                    Email = y.Email,
                    Sdt = y.Sdt,
                    Hoten = y.Hoten,
                    Matkhau = y.Matkhau,
                    Vaitro = y.Vaitro,
                    Trangthai = y.Trangthai,
                    Ngaytao = y.Ngaytao,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateById(Nguoidung user)
        {
            Nguoidung? nguoidung = await _context.Nguoidungs.FirstOrDefaultAsync(x => x.Manguoidung == user.Manguoidung);
            if (nguoidung == null)
            {
                return false;
            }
            if (user.Hoten != null)
            {
                nguoidung.Hoten = user.Hoten;
            }
            if (user.Email != null && !string.IsNullOrEmpty(user.Email) && !await ExistEmail(user.Email))
            {
                nguoidung.Email = user.Email;
            }
            if (user.Sdt != null)
            {
                nguoidung.Sdt = user.Sdt;
            }
            if (user.Matkhau != null && !string.IsNullOrEmpty(user.Matkhau))
            {
                nguoidung.Matkhau = user.Matkhau;
            }
            _context.Nguoidungs.Update(nguoidung);
            return true;
        }

        public async Task<List<NguoiDungDTO>> GetAll()
        {
            return await _context.Nguoidungs.AsNoTracking()
                .Select(y => new NguoiDungDTO
                {
                    Manguoidung = y.Manguoidung,
                    Email = y.Email,
                    Sdt = y.Sdt,
                    Hoten = y.Hoten,
                    Matkhau = y.Matkhau,
                    Vaitro = y.Vaitro,
                    Trangthai = y.Trangthai,
                    Ngaytao = y.Ngaytao,
                }).ToListAsync();
        }
    }
}
