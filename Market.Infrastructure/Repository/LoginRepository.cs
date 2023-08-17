using Market.Application.Models;
using Market.Application.Repository;
using Market.Domain;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Market.Infrastructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MarketContext _loginContext;

        public LoginRepository(MarketContext loginContext)
        {
            _loginContext = loginContext;
        }
        public async Task<Dictionary<string,string>> LoginAsync(Credentials credentials)
        {
       /*     var hashPass = PassGenerate(credentials.Password);*/
            var user = await _loginContext.Users.FirstOrDefaultAsync(u=>u.Email == credentials.Email && u.Password==PassGenerate(credentials.Password));
            if (user == null)
            {
                var dictionary = new Dictionary<string,string>();
                dictionary.Add("Error", "Credentials don't match ");
                return (dictionary);
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, credentials.Email)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("M0j_KaoTaJnI-kLjuC123")), SecurityAlgorithms.HmacSha256Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var response = new Dictionary<string, string>();
            response.Add("token", tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
            user.isActive = true;
            _loginContext.SaveChanges();
            return response;
        }

        public Task<bool> LogoutAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        private string PassGenerate(string pass)
        {

            byte[] salt = { 12, 15, 11, 13, 4, 7, 9, 1, 2, 1, 7, 22, 44, 55, 55, 22 }; 

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
