using MediatR;

public record DeleteNoteCommand(string Id) : IRequest<bool>;