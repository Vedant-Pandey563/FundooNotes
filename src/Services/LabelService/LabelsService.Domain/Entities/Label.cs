using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Domain.Entities
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OwnerUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
