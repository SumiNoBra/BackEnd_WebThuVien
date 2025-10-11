using BackEnd.DTo;
using BackEnd.EF_Contexts;

namespace BackEnd.Interfaces
{
    public interface INguoiDungRepository
    {
        public  Task<int> ExistNguoiDungAsync(string emaill,string matkhau,string vaitro);
        public Task AddAsync(Nguoidung user);
        public Task<bool> ExistIDAsync(int ID);
        public Task<bool> ExistEmail(string email);
    }
}
