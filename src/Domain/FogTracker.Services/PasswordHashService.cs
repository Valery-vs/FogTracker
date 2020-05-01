namespace FogTracker.Services
{
    using System;
    using System.Security.Authentication;
    using System.Text;
    using Contracts.Services;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public class PasswordHashService : IPasswordHashService
    {
        private readonly IPasswordHashServiceSettings settings;

        public PasswordHashService(IPasswordHashServiceSettings settings)
        {
            this.settings = settings;
        }

        public string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = Encoding.ASCII.GetBytes(this.settings.HashSecret);
 
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public void VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var providedPasswordHash = this.HashPassword(providedPassword);
            if (hashedPassword != providedPasswordHash)
            {
                throw new AuthenticationException();
            }
        }
    }
}