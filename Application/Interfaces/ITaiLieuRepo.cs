using Domain.Entities;
namespace Application.Interfaces
{
    public interface ITaiLieuRepo
    {
        public Task<List<TaiLieu>> GetTaiLieus();
        public Task<List<TaiLieu>> GetByTacGia(int id);
        public Task<List<TaiLieu>> GetByTheLoai(int id);
        public Task<List<TaiLieu>> GetByNXB(int id);
        public Task<bool> ExistID(int id);
        public Task<List<TaiLieu>> GetByName(string name);
        public Task<List<TaiLieu>> GetAdvanced(TaiLieu tailieu);
        public Task<TaiLieu?> GetByID(int id);
        public Task Create(TaiLieu tailieu, List<int> tacgia, List<int> theloai);
        public Task<bool> ExistNXB(int id);
        public Task<bool> ExistTacGia(int id);
        public Task<bool> ExistTheLoai(int id);
        public Task<bool> Delete(int id);
        public Task<bool> Update(TaiLieu tailieu, List<int> tacgia, List<int> theloai);
    }

}
