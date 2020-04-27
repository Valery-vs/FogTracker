namespace FogTracker.Web.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Contracts.Repositories;
    using Contracts.Services;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Model;

    public class AuthService : IAuthService
    {
        private readonly IFogRepository fogRepository;
        private readonly Settings settings;

        public AuthService(IFogRepository fogRepository, IOptions<Settings> options)
        {
            this.fogRepository = fogRepository;
            this.settings = options.Value;
        }

        public string Authenticate(string username, string password)
        {
            var user = this.fogRepository.Users.FindByCondition(x => x.UserId == username).FirstOrDefault();

            // return null if user not found
            if (user == null)
            {
                throw new ArgumentException("Incorrect user name");
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
