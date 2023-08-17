

using Market.Application.Models;
using Market.Domain.Entity;

namespace Market.Application.Services
{
    public interface IUserService
    {
        public Task<User> GetByIdAsync(Guid userId);
        public Task<IEnumerable<User>> GetAllAsync(UserQueryRequest userQueryRequest);
        public Task<User> CreateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task<User> UpdateUserAsync(User user);
        public Task<User> CheckCredentialsAsync(Credentials credentials);
    }
}
