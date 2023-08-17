using Market.Application.Models;
using Market.Domain.Entity;

namespace Market.Application.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(UserQueryRequest userQueryRequest);
        Task<User> GetByIdAsync(Guid userId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);

    }
}
