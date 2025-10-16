namespace BackEnd.Interfaces
{
    public interface IUnitOfWork
    {
        public ISachRepository Saches { get; }
        public INguoiDungRepository NguoiDungs { get; }
        public IYeuCauMuonRepository YeuCauMuons { get; }
        public IPhieuMuonRepository PhieuMuons { get; }
        public IDocGiaRepository DocGias { get; }
        public Task CompleteAsync();
    }
}
