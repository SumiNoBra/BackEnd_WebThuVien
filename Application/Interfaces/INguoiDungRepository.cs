using Domain.Entities;
namespace Application.Interfaces
{
    public interface INguoiDungRepository
    {
        public  Task<int> ExistNguoiDungAsync(string emaill,string matkhau,string vaitro);
        public Task AddAsync(Nguoidung user);
        public Task<bool> ExistIDAsync(int ID);
        public Task<bool> ExistEmail(string email);
        public Task<Nguoidung> GetByIdAsync(int ID);
        public Task<List<Nguoidung>> GetAll();
        public Task<bool> UpdateById(Nguoidung user);
    }
}
