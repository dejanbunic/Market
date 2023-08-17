using Market.Application.Models;
using Market.Application.Repository;
using Market.Domain.Entity;
using Market.Infrastructure.Services;
using Moq;

namespace MarketTest
{
    public class Tests
    {
        private UserService userService;
        private Mock<IUserRepository> userRepoMock;

        IEnumerable<User> users = new List<User>()
            {
                new User()
                {
                    FirstName = "FirstName1"
                },
                new User()
                {
                    FirstName = "FirstName2"
                }
            };

        [SetUp]
        public void Setup()
        {
            userRepoMock = new Mock<IUserRepository>();
            userService = new UserService(userRepoMock.Object);
        }

        [Test]
        public async Task GetAll()
        {
            userRepoMock.Setup(x => x.GetAllAsync(It.IsAny<UserQueryRequest>()))
                        .Returns(Task.FromResult(this.users));

            var users = await userService.GetAllAsync(new UserQueryRequest());

            Assert.AreEqual(users.First(), this.users.First());
            Assert.AreEqual(users.Count(), this.users.Count());
        }

        [Test]
        public async Task CreateUser()
        {
            var user = new User()
            {
                FirstName = "FirstName"
            };

            userRepoMock.Setup(x => x.CreateUserAsync(user))
                        .Returns(Task.FromResult(user));

            var result = userService.CreateUserAsync(user);

            userRepoMock.Verify(x => x.CreateUserAsync(user), Times.Once());

            Assert.AreEqual(user, result.Result);
            Assert.AreEqual(user.FirstName, user.FirstName);

        }
    }
}