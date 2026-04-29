using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserService.Application.DTOs;
using UserService.Application.Events;
using UserService.Application.Features.Auth.Commands.Register;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace FundooNotes.Tests.UserServiceTests
{
    [TestClass]
    public class RegisterTests
    {
        private sealed class FakeUserRepository : IUserRepository
        {
            public Task<User?> GetByEmailAsync(string email)
                => Task.FromResult<User?>(null);

            public Task CreateAsync(User user)
                => Task.CompletedTask;

            public Task<User?> GetByIdAsync(int id)
                => Task.FromResult<User?>(null);

            public Task UpdateAsync(User user)
                => Task.CompletedTask;
        }

        private sealed class FakeMessagePublisher : IMessagePublisher
        {
            public Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default)
                => Task.CompletedTask;
        }

        [TestMethod]
        public async Task Register_Should_Create_User()
        {
            var repo = new FakeUserRepository();
            var publisher = new FakeMessagePublisher();
            var handler = new RegisterHandler(repo, publisher);

            var command = new RegisterCommand(
                new RegisterDto("John", "Doe", "john@test.com", "123456")
            );

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
        }
    }
}