using Market.Application.Models;
using Market.Application.Repository;
using Market.Application.Services;
using Market.Domain.Entity;

namespace Market.Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {

            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllAsync(UserQueryRequest userQueryRequest)
        {
            return await _userRepository.GetAllAsync(userQueryRequest);
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }
    }
}
