namespace FogTracker.Contracts.Repositories
{
    public interface IFogRepository
    {
        IUserRepository Users { get; }

        void Save();
    }
}