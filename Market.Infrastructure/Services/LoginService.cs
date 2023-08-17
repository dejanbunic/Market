using Market.Application.Models;
using Market.Application.Repository;
using Market.Application.Services;
using Market.Infrastructure.Common;
using Market.Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Market.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;

        public LoginService( IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Dictionary<string,string>> LoginAsync(Credentials credentials)
        {
            var user = await _userService.CheckCredentialsAsync(credentials);
            if (user == null)
            {
                var dictionary = new Dictionary<string, string>();
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.JWT_PASSWORD)), SecurityAlgorithms.HmacSha256Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var response = new Dictionary<string, string>();
            response.Add("token", tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));

            await _userService.UpdateUserAsync(user);

            return response;
        }

    }
}
