using AutoMapper;
using BackEnd.DTo;
using BackEnd.EF_Contexts;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Repositories
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        public QlThuvienContext _context;
        private IMapper _mapper;
        public NguoiDungRepository(QlThuvienContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> DangNhap(string email, string matkhau, string vaitro)
        {
            if (email == null || matkhau == null)
            {
                throw new ArgumentNullException("Email hoặc mật khẩu không được để trống");
            }
            Nguoidung? isHas = await _context.Nguoidungs.FirstOrDefaultAsync(x => x.Email == email && x.Matkhau == matkhau);

            if (isHas == null)
            {
                return -1;
            }

            vaitro = isHas.Vaitro;
            return isHas.Manguoidung;
        }
        public async Task<int> DangKy(RegisterDTO registerDTO)
        {
            if (registerDTO.Email == null || registerDTO.MatKhau == null
                || registerDTO.Hoten == null || registerDTO.Sdt == null)
            {
                throw new ArgumentNullException("Khong duoc de trong");
            }
            Nguoidung? isHas = await _context.Nguoidungs.FirstOrDefaultAsync(x => x.Email.Trim() == registerDTO.Email.Trim()
                                    && x.Matkhau.Trim() == registerDTO.MatKhau.Trim());
            if (isHas != null)
            {
                throw new ArgumentException("dã tồn tại");
            }
            Nguoidung nguoidung = _mapper.Map<Nguoidung>(registerDTO);
            nguoidung.Vaitro = "User";
            nguoidung.Ngaytao = DateTime.Now;
            try
            {
                await _context.Nguoidungs.AddAsync(nguoidung);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("Lỗi khi thêm người dùng vào database: " + dbEx.Message, dbEx);
            }
        }
    }
}
