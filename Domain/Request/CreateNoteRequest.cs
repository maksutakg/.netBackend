using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class CreateNoteRequest
    {
       // public int? UserId { get; set; }
        public string? text { get; set; }
        public int? MahalleId { get; set; }
    }
}
