namespace BackEnd.Interfaces
{
    public interface IUnitOfWork
    {
        public ISachRepository Saches { get; }
        public INguoiDungRepository NguoiDungs { get; }
        public Task CompleteAsync();
    }
}
