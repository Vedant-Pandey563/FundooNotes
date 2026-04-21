using LabelService.Application.DTOs;
using LabelService.Application.Features.Label.Commands.CreateLabel;
using LabelService.Application.Interfaces;
using LabelService.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundooNotes.Tests.LabelTests
{
    [TestClass]
    public class CreateLabelTests
    {
        private class FakeLabelRepository : ILabelRepository
        {
            public Task<int> CreateAsync(Label label)
                => Task.FromResult(1);

            public Task<List<Label>> GetAllAsync(int ownerUserId)
                => Task.FromResult(new List<Label>());

            public Task<Label?> GetByIdAsync(int id, int ownerUserId)
                => Task.FromResult<Label?>(null);

            public Task<Label?> GetByNameAsync(string name, int ownerUserId)
                => Task.FromResult<Label?>(null);

            public Task<bool> UpdateAsync(Label label)
                => Task.FromResult(true);

            public Task<bool> DeleteAsync(int id, int ownerUserId)
                => Task.FromResult(true);

            public Task<bool> AssignToNoteAsync(NoteLabel noteLabel)
                => Task.FromResult(true);

            public Task<bool> RemoveFromNoteAsync(string noteId, int labelId, int ownerUserId)
                => Task.FromResult(true);

            public Task<List<Label>> GetLabelsByNoteIdAsync(string noteId, int ownerUserId)
                => Task.FromResult(new List<Label>());
        }

        [TestMethod]
        public async Task CreateLabel_Should_Work()
        {
            var repo = new FakeLabelRepository();
            var handler = new CreateLabelHandler(repo);

            var result = await handler.Handle(
                new CreateLabelCommand(1, new CreateLabelDto("Work")),
                CancellationToken.None
            );

            Assert.IsTrue(result > 0); // int
        }
    }
}