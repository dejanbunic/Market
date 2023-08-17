using Market.Application.Models;

namespace Market.Application.Services
{
    public interface ILoginService
    {
        public Task<Dictionary<string,string>> LoginAsync(Credentials credentials);
        public Task<bool> LogoutAsync(Guid userId);
    }
}
