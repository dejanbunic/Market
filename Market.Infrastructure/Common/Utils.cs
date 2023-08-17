using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Market.Infrastructure.Common
{
    public static class Utils
    {
        public static string Generate(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] salt = { 12, 15, 11, 13, 4, 7, 9, 1, 2, 1, 7, 22, 44, 55, 55, 22 };
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
