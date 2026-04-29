using MediatR;

public record AddLabelToNoteCommand(string NoteId, int LabelId)
    : IRequest<bool>;