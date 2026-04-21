using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserService.Application.Features.Auth.Commands.Register;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace FundooNotes.Tests.UserServiceTests
{
    [TestClass]
    public class RegisterTests
    {
        // ✅ Fake repo matches interface EXACTLY
        private class FakeUserRepository : IUserRepository
        {
            public Task<User?> GetByEmailAsync(string email)
                => Task.FromResult<User?>(null);

            public Task CreateAsync(User user) // ✅ FIX: Task (not int)
                => Task.CompletedTask;

            public Task<User?> GetByIdAsync(int id)
                => Task.FromResult<User?>(null);

            public Task UpdateAsync(User user)
                => Task.CompletedTask;
        }

        [TestMethod]
        public async Task Register_Should_Create_User()
        {
            var repo = new FakeUserRepository();
            var handler = new RegisterHandler(repo);

            var command = new RegisterCommand(
                new RegisterDto("John", "Doe", "john@test.com", "123456")
            );

            var result = await handler.Handle(command, CancellationToken.None);

            // ✅ Since CreateAsync returns Task → handler likely returns bool/int
            Assert.IsTrue(result);
        }
    }
}