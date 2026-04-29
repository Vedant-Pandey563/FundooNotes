namespace EmailService.API.Models
{
    // Payload shape published by CollaboratorService.
    public class CollaboratorAddedEvent
    {
        public int CollaboratorId { get; set; }
        public string NoteId { get; set; } = string.Empty;
        public int OwnerUserId { get; set; }
        public int CollaboratorUserId { get; set; }
    }
}
