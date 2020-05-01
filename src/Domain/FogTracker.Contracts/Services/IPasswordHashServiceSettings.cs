namespace FogTracker.Contracts.Services
{
    public interface IPasswordHashServiceSettings
    {
        string HashSecret { get; }
    }
}