using BackEnd.DTOs;
using BackEnd.EF_Contexts;
namespace BackEnd.Interfaces
{
    public interface ISachRepository
    {

        public Task<List<Sach>> GetAllAsync();
        public Task<Sach> GetByIDAsync(int id);
        public Task<List<Sach>> GetByTheLoaiIDAsync(int theloaiId);
        public Task<List<Sach>> GetByNXBIDAsync(int nxbId);
        public Task<List<Sach>> GetByNameAsync(string keyword);
        public Task<List<Sach>> GetByNamAsync(int nam);
        public Task AddAsync(Sach sach);
        public Task UpdateAsync(Sach sach);
        public Task<bool> ExistTheloai(int mastheloai);
        public Task<bool> ExistNXB(int manxb);
        public Task<bool> ExistMaSach(int masach);
        public Task<List<Sach>> GetByTrangThai(string trangthai);

    }
}
