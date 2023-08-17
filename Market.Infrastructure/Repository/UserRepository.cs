using Market.Application.Models;
using Market.Application.Repository;
using Market.Domain;
using Market.Domain.Entity;
using Market.Infrastructure.Errors;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using Constants = Market.Infrastructure.Common.Constants;


namespace Market.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MarketContext _userContext;

        public UserRepository(MarketContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var userExists = _userContext.Users.Any(u => u.Email == user.Email);
            if (!userExists)
            {
                user.Id = Guid.NewGuid();
                /*                user.IsActive = true;*/
                user.Password = this.PassGenerate(user.Password);
                _userContext.Users.Add(user);
                await _userContext.SaveChangesAsync();

                return user;
            
            }
            else
            {
                throw new ServiceException()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    ErrorMessage = Constants.USER_EXISTS
                };
            }

            

        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            User user = await this.GetByIdAsync(userId);
            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();
            return true;
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

        public async Task<IEnumerable<User>> GetAllAsync(UserQueryRequest userQueryRequest)
        {
            var users = _userContext.Users;
            return await users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {

                var user = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if(user == null)
                {
                    throw new UserNotFoundException();
                }
                return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            User userDb = await this.GetByIdAsync(user.Id);
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.PhoneNumber = user.PhoneNumber;

            await _userContext.SaveChangesAsync();
            return userDb;
        }
    }
}