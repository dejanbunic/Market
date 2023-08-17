

using Market.Application.Models;

namespace Market.Application.Repository
{
    public  interface ILoginRepository
    {
        public Task<Dictionary<string,string>> LoginAsync(Credentials credentials);
        public Task<bool> LogoutAsync(Guid userId);
    }
}
