using Domain.Entities;

namespace Application.Interfaces
{
    public interface IYeuCauMuonRepository
    {
        public Task<List<Yeucaumuon>> GetAll();
        public Task<Yeucaumuon> Create(Yeucaumuon yeucaumuon);
        public Task<bool> ExistID(int mayeucau);
        public Task<Yeucaumuon?> GetById(int mayeucau);
        public Task<bool> UpdateTrangThai(int mayeucau, string trangthai);
        public Task<bool> Delete(int mayeucau);
        public Task<bool> ExistsDocGiaId(int madocgia);
        public Task<List<Yeucaumuon>> GetByTrangThai(string trangthai);
    }
}
