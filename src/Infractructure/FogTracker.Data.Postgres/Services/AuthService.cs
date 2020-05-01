namespace FogTracker.Data.Postgres.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Authentication;
    using System.Security.Claims;
    using System.Text;
    using Contracts.Repositories;
    using Contracts.Services;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService : IAuthService
    {
        private readonly IFogRepository fogRepository;
        private readonly IAuthServiceSettings settings;
        private readonly IPasswordHashService passwordService;

        public AuthService(IFogRepository fogRepository, IAuthServiceSettings settings, IPasswordHashService passwordService)
        {
            this.fogRepository = fogRepository;
            this.settings = settings;
            this.passwordService = passwordService;
        }

        public string Authenticate(string username, string password)
        {
            var user = this.fogRepository.Users
                .FindByCondition(x => string.Equals(x.Username.ToLower(), username.ToLower()))
                .FirstOrDefault();

            // return null if user not found
            if (user == null)
            {
                throw new AuthenticationException();
            }

            this.passwordService.VerifyHashedPassword(user.PasswordHash, password);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.settings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
