using BackEnd.DTo;

namespace BackEnd.Repositories
{
    public interface INguoiDungRepository
    {
        public  Task<int> DangNhap(string email, string matkhau,string vaitro);
        public Task<int> DangKy(RegisterDTO registerDTO);
    }
}
