namespace FogTracker.Contracts.Services
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);

        void VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}