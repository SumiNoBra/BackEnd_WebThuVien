using AutoMapper;
using BackEnd.DTo;
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
            Nguoidung ? isHas = await _context.Nguoidungs.AsNoTracking().Where(x => x.Email == email && x.Matkhau == matkhau).FirstOrDefaultAsync();
            if (isHas==null)
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
    }
}
