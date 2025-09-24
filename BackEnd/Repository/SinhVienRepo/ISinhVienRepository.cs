using WebApplication1.Models.DTo;

namespace MainAPI.Repository
{
    public interface ISinhVienRepository
    {
        public Task<List<SinhVien>> GetAllSV();
        public Task<string> UpdateSV(string id,SinhVienDTo _sv);
        public Task<string> CreateSV(SinhVien _sv);
        public Task<string> DeleteSV(string id);
    }
}
