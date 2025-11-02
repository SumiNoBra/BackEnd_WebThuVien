using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDocGiaRepository
    {
        public Task<List<Docgia>> GetAll();
        public Task Create(Docgia docgia);
        public Task<bool> ExistNguoiDungID(int id);
        public Task<bool> Update(Docgia docgia);
    }
}
