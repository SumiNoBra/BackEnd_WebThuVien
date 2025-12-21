using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDocGiaRepo
    {
        public Task<List<DocGia>> GetDocGias();
        public Task<DocGia?> GetDocGia(int id);
        public Task CreateDocGia(DocGia docGia);
        public Task<bool> ExistEmail(string email);
        public Task<DocGia?> ExistDocGia(string email, string matkhau);
        public Task<bool> ExistDocGia(int id);
        public Task<bool> UpdateDocGia(DocGia docgia);
        public Task<DocGia?> GetByEmailAsync(string email);
        public Task SaveRefreshTokenAsync(int maDocGia, string refreshToken, DateTime expiryDate);
        public Task<DocGia?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
