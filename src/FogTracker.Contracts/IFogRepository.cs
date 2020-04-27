namespace FogTracker.Contracts
{
    public interface IFogRepository
    {
        IUserRepository Users { get; }

        void Save();
    }
}