using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollaboratorService.Application.Features.Collaborators.Commands.AddCollaborator;
using CollaboratorService.Application.DTOs;
using CollaboratorService.Application.Interfaces;
using CollaboratorService.Domain.Entites;

namespace FundooNotes.Tests.CollaboratorTests
{
    [TestClass] // ✅ Required
    public class AddCollaboratorTests
    {
        // ✅ Fake repository inside class
        private class FakeCollaboratorRepository : ICollaboratorRepository
        {
            public Task<int> AddAsync(Collaborator collaborator)
                => Task.FromResult(1);

            public Task<List<Collaborator>> GetAllAsync()
                => Task.FromResult(new List<Collaborator>());

            public Task<Collaborator?> GetByIdAsync(int id)
                => Task.FromResult<Collaborator?>(null);

            public Task<bool> UpdateAsync(Collaborator collaborator)
                => Task.FromResult(true);

            public Task<bool> RemoveAsync(int id)
                => Task.FromResult(true);
        }

        [TestMethod] // ✅ REQUIRED
        public async Task AddCollaborator_Should_Work()
        {
            var repo = new FakeCollaboratorRepository();
            var handler = new AddCollaboratorHandler(repo);

            var dto = new AddCollaboratorDto("note1", 2);

            var result = await handler.Handle(
                new AddCollaboratorCommand(1, dto),
                CancellationToken.None
            );

            Assert.IsTrue(result>0); // returns int not bool
        }
    }
}