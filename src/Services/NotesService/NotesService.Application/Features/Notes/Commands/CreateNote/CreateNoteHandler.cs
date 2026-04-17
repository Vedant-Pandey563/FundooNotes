using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;

namespace NotesService.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, string>
    {
        private readonly INoteRepository _repo;

        public CreateNoteHandler(INoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Dto.Title,
                Description = request.Dto.Description,
                Color = request.Dto.Color
            };

            return await _repo.CreateAsync(note);
        }
    }
}
