using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Domain.Entities
{
    public class NoteLabel
    {
        public int Id { get; set; }
        public string NoteId { get; set; } = string.Empty;
        public int LabelId { get; set; }
        public int OwnerUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
