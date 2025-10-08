using BackEnd.DTOs;
using BackEnd.EF_Contexts;
namespace BackEnd.Repositories
{
    public interface ISachRepository
    {
        public Task<List<Sach>> GetAllSachesAsync();
        public Task<Sach> GetSachByMasachAsync(int id);
        public Task<List<Sach>> GetSachesByTheLoaiAsync(int theloaiId);
        public Task<List<Sach>> GetSachesByNhaXuatBanAsync(int nxbId);
        public Task<List<Sach>> SearchSachesAsync(string keyword);
        public Task AddSachAsync(Sach sach);
        public Task UpdateSachAsync(SachDTO sach);

    }
}
