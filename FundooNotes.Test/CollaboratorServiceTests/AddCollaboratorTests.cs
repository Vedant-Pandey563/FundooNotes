//using CollaboratorService.Application.DTOs;
//using CollaboratorService.Application.Features.Collaborators.Commands.AddCollaborator;
//using CollaboratorService.Application.Interfaces;
//using CollaboratorService.Domain.Entites;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace FundooNotes.Tests.CollaboratorTests
//{
//    [TestClass]
//    public class AddCollaboratorTests
//    {
//        private class FakeCollaboratorRepository : ICollaboratorRepository
//        {
//            public Task<int> AddAsync(Collaborator collaborator)
//                => Task.FromResult(1);

//            public Task<List<Collaborator>> GetAllAsync()
//                => Task.FromResult(new List<Collaborator>());

//            public Task<Collaborator?> GetByIdAsync(int id)
//                => Task.FromResult<Collaborator?>(null);

//            public Task<bool> UpdateAsync(Collaborator collaborator)
//                => Task.FromResult(true);

//            public Task<bool> RemoveAsync(int id)
//                => Task.FromResult(true);
//        }

//        [TestMethod]
//        public async Task AddCollaborator_Should_Work()
//        {
//            var repo = new FakeCollaboratorRepository();
//            var handler = new AddCollaboratorHandler(repo);

//            var dto = new AddCollaboratorDto("note1", 2);

//            var result = await handler.Handle(
//                new AddCollaboratorCommand(1, dto),
//                CancellationToken.None
//            );

//            // MSTest analyzer prefers comparison assertions over Assert.IsTrue(...)
//            // when you are checking a numeric value.
//            Assert.IsGreaterThan(0, result);
//        }
//    }

//    private class FakeDaprClient : DaprClient
//    {
//        public override Task PublishEventAsync<T>(
//            string pubsubName,
//            string topicName,
//            T data,
//            CancellationToken cancellationToken = default)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}