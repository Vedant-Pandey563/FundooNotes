using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Domain.Entites
{
    // Pure domain model
    public class Collaborator
    {
        public int Id { get; set; }

        public string NoteId { get; set; } = string.Empty;

        public int OwnerUserId { get; set; }

        public int CollaboratorUserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
