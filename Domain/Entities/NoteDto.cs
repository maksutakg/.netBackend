using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NoteDto
    {
        public string? text { get; set; }
        public int? UserId { get; set; }
    }
}
