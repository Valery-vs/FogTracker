namespace FogTracker.Web.Services
{
    using Contracts.Services;

    public class PasswordHashServiceSettings : IPasswordHashServiceSettings
    {
        public string HashSecret { get; set; }
    }
}