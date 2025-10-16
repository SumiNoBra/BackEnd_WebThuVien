using BackEnd.DTOs;
using BackEnd.EF_Contexts;

namespace BackEnd.Interfaces
{
    public interface IYeuCauMuonRepository
    {
        public Task<List<YeuCauMuonDTO>> GetAll();
        public Task<Yeucaumuon> Create(Yeucaumuon yeucaumuon);
        public Task<bool> ExistID(int mayeucau);
        public Task<YeuCauMuonDTO?> GetById(int mayeucau);
        public Task<bool> UpdateTrangThai(int mayeucau, string trangthai);
        public Task<bool> Delete(int mayeucau);
        public Task<bool> ExistsDocGiaId(int madocgia);
        public Task<List<YeuCauMuonDTO>> GetByTrangThai(string trangthai);
    }
}
