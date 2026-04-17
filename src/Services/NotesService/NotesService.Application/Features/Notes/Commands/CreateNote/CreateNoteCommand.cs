
using MediatR;
using NotesService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesService.Application.Features.Notes.Commands.CreateNote
{

    public record CreateNoteCommand(int UserId, CreateNoteDto Dto) : IRequest<string>;
    //irequest
}
