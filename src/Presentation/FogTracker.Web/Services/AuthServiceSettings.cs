namespace FogTracker.Web.Services
{
    using Contracts.Services;

    public class AuthServiceSettings : IAuthServiceSettings
    {
        public string TokenSecret { get; set; }
    }
}