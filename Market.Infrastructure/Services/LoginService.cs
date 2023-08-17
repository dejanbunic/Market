using Market.Application.Models;
using Market.Application.Repository;
using Market.Application.Services;
using Market.Infrastructure.Repository;

namespace Market.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Dictionary<string,string>> LoginAsync(Credentials credentials)
        {
            return await _loginRepository.LoginAsync(credentials);
        }

        public async Task<bool> LogoutAsync(Guid userId)
        {
            return await _loginRepository.LogoutAsync(userId);
        }
    }
}
